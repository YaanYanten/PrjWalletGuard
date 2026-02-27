Imports Microsoft.Data.SqlClient
Imports DL_WalletGuard.WalletGuard.Entidades

Namespace WalletGuard.AccesoDatos

    ''' <summary>
    ''' Acceso a datos para sr_ingresos.
    ''' Incluye los nuevos métodos requeridos por el Dashboard.
    ''' Agrega este archivo o fusiona con tu IngresoDAL existente.
    ''' </summary>
    Public Class IngresoDAL

#Region "Consultas para el Dashboard"

        ''' <summary>Suma total de ingresos de un mes/año.</summary>
        Public Function ObtenerTotalMes(mes As Integer, anio As Integer) As Decimal
            Dim sql As String =
                "SELECT ISNULL(SUM(monto), 0) " &
                "FROM   sr_ingresos " &
                "WHERE  MONTH(fecha) = @mes AND YEAR(fecha) = @anio"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@mes", mes)
                    cmd.Parameters.AddWithValue("@anio", anio)
                    Return Convert.ToDecimal(cmd.ExecuteScalar())
                End Using
            End Using
        End Function

        ''' <summary>Devuelve todos los ingresos de un mes/año para el calendario.</summary>
        Public Function ObtenerPorMes(mes As Integer, anio As Integer) As List(Of Ingreso)
            Dim lista As New List(Of Ingreso)()

            Dim sql As String =
                "SELECT id_ingreso, fecha, monto, id_categoria, descripcion " &
                "FROM   sr_ingresos " &
                "WHERE  MONTH(fecha) = @mes AND YEAR(fecha) = @anio"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@mes", mes)
                    cmd.Parameters.AddWithValue("@anio", anio)

                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        Do While dr.Read()
                            Dim ing As New Ingreso()
                            ing.IdIngreso = dr.GetInt32(dr.GetOrdinal("id_ingreso"))
                            ing.Fecha = dr.GetDateTime(dr.GetOrdinal("fecha"))
                            ing.Monto = dr.GetDecimal(dr.GetOrdinal("monto"))
                            ing.IdCategoria = dr.GetInt32(dr.GetOrdinal("id_categoria"))
                            Dim ordDesc As Integer = dr.GetOrdinal("descripcion")
                            ing.Descripcion = If(dr.IsDBNull(ordDesc), String.Empty, dr.GetString(ordDesc))
                            lista.Add(ing)
                        Loop
                    End Using
                End Using
            End Using

            Return lista
        End Function

#End Region

#Region "Comandos"

        ''' <summary>Inserta un ingreso.</summary>
        Public Function Insertar(ing As Ingreso) As Integer
            Dim sql As String =
                "INSERT INTO sr_ingresos (fecha, monto, id_categoria, descripcion) " &
                "VALUES (@fecha, @monto, @idCat, @desc); " &
                "SELECT SCOPE_IDENTITY();"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@fecha", ing.Fecha)
                    cmd.Parameters.AddWithValue("@monto", ing.Monto)
                    cmd.Parameters.AddWithValue("@idCat", ing.IdCategoria)
                    cmd.Parameters.AddWithValue("@desc",
                        If(String.IsNullOrWhiteSpace(ing.Descripcion), DBNull.Value, CObj(ing.Descripcion)))
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        End Function

#End Region

    End Class

End Namespace
