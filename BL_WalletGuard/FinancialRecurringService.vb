Imports Microsoft.Data.SqlClient
Imports DL_WalletGuard.WalletGuard.AccesoDatos
Imports DL_WalletGuard.WalletGuard.Entidades
Imports WalletGuard.WalletGuard.Entidades

Namespace WalletGuard.LogicaNegocio

    ''' <summary>
    ''' Servicio de procesamiento de gastos recurrentes.
    ''' ─────────────────────────────────────────────────────────────────────
    ''' RESPONSABILIDAD ÚNICA: detectar meses pendientes para cada gasto
    ''' recurrente activo y generar los egresos correspondientes una sola vez
    ''' por ejecución de la aplicación, sin duplicados.
    '''
    ''' FLUJO:
    '''   1. Cargar todos los recurrentes activos (DAL).
    '''   2. Por cada recurrente, calcular la lista de meses pendientes.
    '''   3. Por cada mes pendiente, verificar que no exista ya un egreso.
    '''   4. Insertar egresos pendientes y actualizar fecha_ultimo_procesado.
    '''   5. Todo dentro de una transacción SQL por recurrente.
    '''   6. Devolver ResultadoProcesamiento a la capa de presentación.
    ''' </summary>
    Public Class FinancialRecurringService

#Region "Dependencias"

        Private ReadOnly _recurrenteDAL As GastoRecurrenteDAL
        Private ReadOnly _egresoDAL As EgresoDAL

        Public Sub New()
            _recurrenteDAL = New GastoRecurrenteDAL()
            _egresoDAL = New EgresoDAL()
        End Sub

        ''' <summary>Constructor con inyección de dependencias (para pruebas unitarias).</summary>
        Public Sub New(recurrenteDAL As GastoRecurrenteDAL, egresoDAL As EgresoDAL)
            _recurrenteDAL = recurrenteDAL
            _egresoDAL = egresoDAL
        End Sub

#End Region

#Region "Método principal"

        ''' <summary>
        ''' Procesa todos los gastos recurrentes pendientes.
        ''' Debe llamarse UNA SOLA VEZ en el evento FormMain_Load.
        ''' </summary>
        ''' <returns>Resumen con cantidad de egresos generados y detalle por recurrente.</returns>
        Public Function ProcessPendingRecurring() As ResultadoProcesamiento
            Dim resultado As New ResultadoProcesamiento()

            Try
                Dim recurrentes As List(Of GastoRecurrente) = _recurrenteDAL.ObtenerActivos()

                For Each gr As GastoRecurrente In recurrentes
                    Dim detalle As DetalleRecurrente = ProcesarRecurrente(gr)
                    If detalle.MesesGenerados > 0 Then
                        resultado.Detalle.Add(detalle)
                        resultado.TotalGenerados += detalle.MesesGenerados
                    End If
                Next

            Catch ex As Exception
                resultado.HuboErrores = True
                resultado.MensajeError = ex.Message
            End Try

            Return resultado
        End Function

#End Region

#Region "Lógica de procesamiento por recurrente"

        ''' <summary>
        ''' Procesa un recurrente individual dentro de su propia transacción.
        ''' Si falla, el rollback es solo para ese recurrente; los demás no se ven afectados.
        ''' </summary>
        Private Function ProcesarRecurrente(gr As GastoRecurrente) As DetalleRecurrente
            Dim detalle As New DetalleRecurrente()
            detalle.Nombre = gr.Nombre
            detalle.MontoPorMes = gr.Monto

            ' Calcular rango de meses a procesar
            Dim mesesPendientes As List(Of Date) = CalcularMesesPendientes(gr)

            If mesesPendientes.Count = 0 Then
                Return detalle
            End If

            ' Procesar dentro de una transacción
            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using tran As SqlTransaction = conn.BeginTransaction()
                    Try
                        Dim ultimaFechaProcesada As Date = Date.MinValue

                        For Each fechaMes As Date In mesesPendientes
                            ' Verificar duplicado antes de insertar
                            Dim existe As Boolean = _egresoDAL.ExisteEgresoRecurrente(
                                gr.IdRecurrente, fechaMes.Year, fechaMes.Month, conn, tran)

                            If Not existe Then
                                Dim egreso As New Egreso()
                                egreso.Fecha = FechaEfectiva(fechaMes, gr.DiaCorte)
                                egreso.Monto = gr.Monto
                                egreso.IdCategoria = gr.IdCategoria
                                egreso.Tipo = TipoEgreso.Recurrente
                                egreso.IdRecurrente = gr.IdRecurrente
                                egreso.Descripcion = $"Gasto recurrente: {gr.Nombre} ({fechaMes:MM/yyyy})"

                                _egresoDAL.InsertarEnTransaccion(egreso, conn, tran)
                                detalle.MesesGenerados += 1
                                ultimaFechaProcesada = egreso.Fecha
                            End If
                        Next

                        ' Actualizar fecha_ultimo_procesado solo si se generó algo
                        If detalle.MesesGenerados > 0 Then
                            _recurrenteDAL.ActualizarFechaUltimoProcesado(
                                gr.IdRecurrente, ultimaFechaProcesada, conn, tran)
                        End If

                        tran.Commit()

                    Catch ex As Exception
                        tran.Rollback()
                        Throw New Exception($"Error procesando '{gr.Nombre}': {ex.Message}", ex)
                    End Try
                End Using
            End Using

            detalle.TotalGenerado = detalle.MontoPorMes * detalle.MesesGenerados
            Return detalle
        End Function

#End Region

#Region "Cálculo de meses pendientes"

        ''' <summary>
        ''' Calcula la lista de meses que deben generarse para un recurrente.
        ''' ─────────────────────────────────────────────────────────────────
        ''' REGLAS:
        '''   - Si fecha_ultimo_procesado es NULL → partir desde fecha_inicio.
        '''   - Generar un egreso por cada mes desde el punto de inicio hasta hoy.
        '''   - Respetar fecha_fin si está definida.
        '''   - No generar meses futuros.
        ''' </summary>
        Private Function CalcularMesesPendientes(gr As GastoRecurrente) As List(Of Date)
            Dim meses As New List(Of Date)()
            Dim hoy As Date = Date.Today

            ' Punto de partida: primer mes después del último procesado, o fecha_inicio
            Dim desdeAnio As Integer
            Dim desMes As Integer

            If gr.FechaUltimoProcesado.HasValue Then
                ' Continuar desde el mes siguiente al último procesado
                Dim siguiente As Date = gr.FechaUltimoProcesado.Value.AddMonths(1)
                desdeAnio = siguiente.Year
                desMes = siguiente.Month
            Else
                ' Nunca procesado: empezar desde fecha_inicio
                desdeAnio = gr.FechaInicio.Year
                desMes = gr.FechaInicio.Month
            End If

            ' Límite superior: mes actual (no generar futuros)
            Dim hastaAnio As Integer = hoy.Year
            Dim hastaMes As Integer = hoy.Month

            ' Iterar mes a mes
            Dim cursor As New Date(desdeAnio, desMes, 1)
            Dim limite As New Date(hastaAnio, hastaMes, 1)

            Do While cursor <= limite
                ' Verificar que no supere la fecha_fin del recurrente
                If gr.FechaFin.HasValue Then
                    Dim finMes As New Date(gr.FechaFin.Value.Year, gr.FechaFin.Value.Month, 1)
                    If cursor > finMes Then Exit Do
                End If

                meses.Add(cursor)
                cursor = cursor.AddMonths(1)
            Loop

            Return meses
        End Function

        ''' <summary>
        ''' Calcula la fecha efectiva del egreso para un mes dado.
        ''' Si el día de corte no existe en ese mes (ej: 31 en febrero),
        ''' usa el último día del mes.
        ''' </summary>
        Private Function FechaEfectiva(mesPrimero As Date, diaCorte As Integer) As Date
            Dim diasEnMes As Integer = Date.DaysInMonth(mesPrimero.Year, mesPrimero.Month)
            Dim diaFinal As Integer = Math.Min(diaCorte, diasEnMes)
            Return New Date(mesPrimero.Year, mesPrimero.Month, diaFinal)
        End Function

#End Region

    End Class

End Namespace
