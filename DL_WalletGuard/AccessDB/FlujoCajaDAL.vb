Imports Microsoft.Data.SqlClient
Imports DL_WalletGuard.WalletGuard.Entidades

Namespace WalletGuard.AccesoDatos

    ''' <summary>
    ''' Acceso a datos para el Reporte de Flujo de Caja.
    ''' Devuelve los datos agregados por mes que necesita el servicio.
    ''' </summary>
    Public Class FlujoCajaDAL

        ''' <summary>
        ''' Devuelve ingresos y egresos totales agrupados por mes
        ''' dentro del rango de fechas indicado.
        ''' </summary>
        Public Function ObtenerFlujoPorMes(desdeAnio As Integer, desdeMes As Integer,
                                            hastaAnio As Integer, hastaMes As Integer) As List(Of LineaFlujoCaja)
            Dim lista As New List(Of LineaFlujoCaja)()

            ' Construir todas las combinaciones año/mes del rango
            Dim cursor As New Date(desdeAnio, desdeMes, 1)
            Dim limite As New Date(hastaAnio, hastaMes, 1)

            ' Traer totales de ingresos del rango completo en una sola consulta
            Dim sqlIng As String =
                "SELECT YEAR(fecha) AS anio, MONTH(fecha) AS mes, SUM(monto) AS total " &
                "FROM   sr_ingresos " &
                "WHERE  fecha >= @desde AND fecha <= @hasta " &
                "GROUP  BY YEAR(fecha), MONTH(fecha)"

            ' Traer totales de egresos del rango completo en una sola consulta
            Dim sqlEgr As String =
                "SELECT YEAR(fecha) AS anio, MONTH(fecha) AS mes, SUM(monto) AS total " &
                "FROM   sr_egresos " &
                "WHERE  fecha >= @desde AND fecha <= @hasta " &
                "GROUP  BY YEAR(fecha), MONTH(fecha)"

            Dim desde As Date = New Date(desdeAnio, desdeMes, 1)
            Dim hasta As Date = New Date(hastaAnio, hastaMes,
                                         Date.DaysInMonth(hastaAnio, hastaMes))

            ' Diccionarios para lookup rápido
            Dim dictIng As New Dictionary(Of String, Decimal)()
            Dim dictEgr As New Dictionary(Of String, Decimal)()

            Using conn As SqlConnection = DatabaseHelper.GetConnection()

                Using cmd As New SqlCommand(sqlIng, conn)
                    cmd.Parameters.AddWithValue("@desde", desde)
                    cmd.Parameters.AddWithValue("@hasta", hasta)
                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        Do While dr.Read()
                            Dim key As String = $"{dr.GetInt32(0)}-{dr.GetInt32(1)}"
                            dictIng(key) = dr.GetDecimal(2)
                        Loop
                    End Using
                End Using

                Using cmd As New SqlCommand(sqlEgr, conn)
                    cmd.Parameters.AddWithValue("@desde", desde)
                    cmd.Parameters.AddWithValue("@hasta", hasta)
                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        Do While dr.Read()
                            Dim key As String = $"{dr.GetInt32(0)}-{dr.GetInt32(1)}"
                            dictEgr(key) = dr.GetDecimal(2)
                        Loop
                    End Using
                End Using

            End Using

            ' Construir una línea por cada mes del rango (incluye meses sin movimientos)
            Dim acumulado As Decimal = 0
            Do While cursor <= limite
                Dim key As String = $"{cursor.Year}-{cursor.Month}"
                Dim ingresos As Decimal = If(dictIng.ContainsKey(key), dictIng(key), 0)
                Dim egresos As Decimal = If(dictEgr.ContainsKey(key), dictEgr(key), 0)
                Dim balance As Decimal = ingresos - egresos
                acumulado += balance

                Dim linea As New LineaFlujoCaja()
                linea.Periodo = cursor.ToString("MMMM yyyy")
                linea.Anio = cursor.Year
                linea.Mes = cursor.Month
                linea.TotalIngresos = ingresos
                linea.TotalEgresos = egresos
                linea.Balance = balance
                linea.BalanceAcumulado = acumulado

                lista.Add(linea)
                cursor = cursor.AddMonths(1)
            Loop

            Return lista
        End Function

        ''' <summary>Promedio mensual de ingresos de los últimos N meses.</summary>
        Public Function ObtenerPromedioIngresosMeses(meses As Integer) As Decimal
            Dim sql As String =
                "SELECT ISNULL(AVG(total_mes), 0) " &
                "FROM ( " &
                "   SELECT SUM(monto) AS total_mes " &
                "   FROM   sr_ingresos " &
                "   WHERE  fecha >= DATEADD(MONTH, @meses, GETDATE()) " &
                "   GROUP  BY YEAR(fecha), MONTH(fecha) " &
                ") t"
            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@meses", -meses)
                    Return Convert.ToDecimal(cmd.ExecuteScalar())
                End Using
            End Using
        End Function

        ''' <summary>Promedio mensual de egresos de los últimos N meses.</summary>
        Public Function ObtenerPromedioEgresosMeses(meses As Integer) As Decimal
            Dim sql As String =
                "SELECT ISNULL(AVG(total_mes), 0) " &
                "FROM ( " &
                "   SELECT SUM(monto) AS total_mes " &
                "   FROM   sr_egresos " &
                "   WHERE  fecha >= DATEADD(MONTH, @meses, GETDATE()) " &
                "   GROUP  BY YEAR(fecha), MONTH(fecha) " &
                ") t"
            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@meses", -meses)
                    Return Convert.ToDecimal(cmd.ExecuteScalar())
                End Using
            End Using
        End Function

        ''' <summary>Suma de todos los recurrentes activos (para proyección).</summary>
        Public Function ObtenerTotalRecurrentesActivos() As Decimal
            Dim sql As String =
                "SELECT ISNULL(SUM(monto), 0) FROM sr_gastos_recurrentes WHERE activo = 1"
            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    Return Convert.ToDecimal(cmd.ExecuteScalar())
                End Using
            End Using
        End Function

    End Class

End Namespace
