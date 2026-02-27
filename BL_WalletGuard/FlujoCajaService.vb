Imports DL_WalletGuard.WalletGuard.AccesoDatos
Imports DL_WalletGuard.WalletGuard.Entidades

Namespace WalletGuard.LogicaNegocio

    ''' <summary>
    ''' Servicio de negocio para el Reporte de Flujo de Caja y Proyecciones.
    ''' Separa los cálculos de proyección del acceso a datos.
    ''' </summary>
    Public Class FlujoCajaService

        Private ReadOnly _dal As FlujoCajaDAL
        Private Const MesesBase As Integer = 3   ' meses históricos para calcular promedio

        Public Sub New()
            _dal = New FlujoCajaDAL()
        End Sub

#Region "Flujo de caja histórico"

        ''' <summary>
        ''' Devuelve el flujo de caja mes a mes para el rango indicado.
        ''' El balance acumulado parte desde cero en el primer mes del rango.
        ''' </summary>
        Public Function ObtenerFlujo(desdeAnio As Integer, desdeMes As Integer,
                                      hastaAnio As Integer, hastaMes As Integer) As List(Of LineaFlujoCaja)
            Return _dal.ObtenerFlujoPorMes(desdeAnio, desdeMes, hastaAnio, hastaMes)
        End Function

#End Region

#Region "Proyección futura"

        ''' <summary>
        ''' Genera N meses de proyección futura a partir del mes siguiente al actual.
        ''' Algoritmo:
        '''   - Ingresos proyectados  = promedio de los últimos 3 meses de ingresos reales.
        '''   - Egresos proyectados   = promedio de los últimos 3 meses de egresos reales.
        '''   - Recurrentes activos   = suma fija de todos los recurrentes activos.
        '''   - Egreso total proj.    = MAX(egresos proyectados, recurrentes activos)
        '''     (evita subestimar si los recurrentes superan el promedio histórico).
        ''' </summary>
        Public Function GenerarProyeccion(mesesFuturos As Integer) As List(Of LineaProyeccion)
            Dim lista As New List(Of LineaProyeccion)()

            Dim promedioIngresos    As Decimal = _dal.ObtenerPromedioIngresosMeses(MesesBase)
            Dim promedioEgresos     As Decimal = _dal.ObtenerPromedioEgresosMeses(MesesBase)
            Dim recurrentesActivos  As Decimal = _dal.ObtenerTotalRecurrentesActivos()

            ' Egresos proyectados: usar el mayor entre el promedio histórico y los recurrentes
            Dim egresosProyectados As Decimal = Math.Max(promedioEgresos, recurrentesActivos)

            Dim cursor As New Date(Date.Today.Year, Date.Today.Month, 1)
            cursor = cursor.AddMonths(1)   ' empezar desde el próximo mes

            For i As Integer = 1 To mesesFuturos
                Dim linea As New LineaProyeccion()
                linea.Periodo               = cursor.ToString("MMMM yyyy")
                linea.IngresosProyectados   = promedioIngresos
                linea.EgresosProyectados    = promedioEgresos
                linea.RecurrentesActivos    = recurrentesActivos
                linea.BalanceProyectado     = promedioIngresos - egresosProyectados
                linea.EsProyeccion          = True
                lista.Add(linea)
                cursor = cursor.AddMonths(1)
            Next

            Return lista
        End Function

        ''' <summary>
        ''' Combina datos reales (últimos 3 meses) + proyección (3 meses futuros)
        ''' para la vista unificada del reporte.
        ''' </summary>
        Public Function ObtenerVistaUnificada() As List(Of LineaProyeccion)
            Dim lista As New List(Of LineaProyeccion)()

            ' Datos reales — últimos 3 meses
            Dim hoy    As Date    = Date.Today
            Dim desde3 As Date    = hoy.AddMonths(-2)
            Dim historial As List(Of LineaFlujoCaja) =
                _dal.ObtenerFlujoPorMes(desde3.Year, desde3.Month, hoy.Year, hoy.Month)

            For Each h As LineaFlujoCaja In historial
                Dim linea As New LineaProyeccion()
                linea.Periodo             = h.Periodo
                linea.IngresosProyectados = h.TotalIngresos
                linea.EgresosProyectados  = h.TotalEgresos
                linea.RecurrentesActivos  = 0
                linea.BalanceProyectado   = h.Balance
                linea.EsProyeccion        = False
                lista.Add(linea)
            Next

            ' Proyección — próximos 3 meses
            lista.AddRange(GenerarProyeccion(3))

            Return lista
        End Function

#End Region

    End Class

End Namespace
