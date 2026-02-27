Imports DL_WalletGuard.WalletGuard.AccesoDatos
Imports DL_WalletGuard.WalletGuard.Entidades

Namespace WalletGuard.LogicaNegocio

    ''' <summary>
    ''' Servicio de negocio para gestión de deudas.
    ''' Corregido para usar los campos reales de la tabla:
    '''   monto_total  (antes monto_original)
    '''   estado       (antes activo/activa Boolean)
    '''   pago_minimo  (campo nuevo incluido)
    ''' </summary>
    Public Class DeudaService

        Private ReadOnly _dal As DeudaDAL

        Public Sub New()
            _dal = New DeudaDAL()
        End Sub

#Region "Consultas"

        ''' <summary>
        ''' Devuelve todas las deudas con sus indicadores calculados.
        ''' El saldo se obtiene dinámicamente: monto_total - SUM(pagos).
        ''' </summary>
        Public Function ObtenerResumenes() As List(Of ResumenDeuda)
            Dim deudas As List(Of Deuda) = _dal.ObtenerTodas()
            Dim resumenes As New List(Of ResumenDeuda)()

            For Each d As Deuda In deudas
                Dim totalPagado As Decimal = _dal.ObtenerTotalPagado(d.IdDeuda)
                Dim saldo As Decimal = Math.Max(0, d.MontoTotal - totalPagado)
                Dim pct As Decimal = If(d.MontoTotal > 0,
                    Math.Min(100, totalPagado / d.MontoTotal * 100), 0)

                ' Proyección: cuántos meses al ritmo promedio de los últimos 3 meses
                Dim promedio As Decimal = _dal.ObtenerPromedioMensualPagos(d.IdDeuda)
                Dim mesesRest As Integer? = Nothing
                If promedio > 0 AndAlso saldo > 0 Then
                    mesesRest = CInt(Math.Ceiling(CDbl(saldo) / CDbl(promedio)))
                End If

                Dim r As New ResumenDeuda()
                r.IdDeuda = d.IdDeuda
                r.Nombre = d.Nombre
                r.Acreedor = d.Acreedor
                r.MontoOriginal = d.MontoTotal        ' DTO conserva nombre MontoOriginal para la UI
                r.TotalPagado = totalPagado
                r.SaldoPendiente = saldo
                r.PorcentajePagado = pct
                r.FechaVencimiento = d.FechaVencimiento
                r.Activa = (d.Estado = EstadoDeuda.Activo OrElse d.Estado = EstadoDeuda.Mora)
                r.MesesRestantes = mesesRest
                r.Estado = d.Estado
                r.PagoMinimo = d.PagoMinimo

                resumenes.Add(r)
            Next

            Return resumenes
        End Function

        Public Function ObtenerPagos(idDeuda As Integer) As List(Of PagoDeuda)
            Return _dal.ObtenerPagosPorDeuda(idDeuda)
        End Function

#End Region

#Region "Comandos"

        Public Sub GuardarDeuda(d As Deuda)
            ValidarDeuda(d)
            If d.IdDeuda = 0 Then
                _dal.InsertarDeuda(d)
            Else
                _dal.ActualizarDeuda(d)
            End If
        End Sub

        ''' <summary>
        ''' Registra un pago y, si el saldo resultante llega a 0,
        ''' actualiza automáticamente el estado de la deuda a 'Pagado'.
        ''' </summary>
        Public Sub RegistrarPago(p As PagoDeuda)
            If p.Monto <= 0 Then
                Throw New ArgumentException("El monto del pago debe ser mayor a cero.")
            End If

            _dal.InsertarPago(p)

            ' Verificar si la deuda quedó saldada
            Dim totalPagado As Decimal = _dal.ObtenerTotalPagado(p.IdDeuda)
            Dim deudas As List(Of Deuda) = _dal.ObtenerTodas()
            Dim deuda As Deuda = deudas.FirstOrDefault(Function(x) x.IdDeuda = p.IdDeuda)

            If deuda IsNot Nothing AndAlso totalPagado >= deuda.MontoTotal Then
                _dal.ActualizarEstado(p.IdDeuda, EstadoDeuda.Pagado)
            End If
        End Sub

        Public Sub EliminarPago(idPago As Integer)
            _dal.EliminarPago(idPago)
        End Sub

        Public Sub CambiarEstado(idDeuda As Integer, nuevoEstado As String)
            _dal.ActualizarEstado(idDeuda, nuevoEstado)
        End Sub

#End Region

#Region "Validaciones"

        Private Sub ValidarDeuda(d As Deuda)
            If String.IsNullOrWhiteSpace(d.Nombre) Then
                Throw New ArgumentException("El nombre de la deuda es obligatorio.")
            End If
            If String.IsNullOrWhiteSpace(d.Acreedor) Then
                Throw New ArgumentException("El acreedor es obligatorio.")
            End If
            If d.MontoTotal <= 0 Then
                Throw New ArgumentException("El monto total debe ser mayor a cero.")
            End If
            If d.TasaInteres < 0 Then
                Throw New ArgumentException("La tasa de interés no puede ser negativa.")
            End If
            If d.PagoMinimo < 0 Then
                Throw New ArgumentException("El pago mínimo no puede ser negativo.")
            End If
            If d.FechaVencimiento.HasValue AndAlso d.FechaVencimiento.Value < d.FechaInicio Then
                Throw New ArgumentException("La fecha de vencimiento no puede ser anterior al inicio.")
            End If
            Dim estadosValidos As String() = {EstadoDeuda.Activo, EstadoDeuda.Pagado, EstadoDeuda.Mora}
            If Not estadosValidos.Contains(d.Estado) Then
                Throw New ArgumentException("Estado inválido. Valores permitidos: Activo, Pagado, Mora.")
            End If
        End Sub

#End Region

    End Class

End Namespace