Imports Microsoft.Data.SqlClient
Imports DL_WalletGuard.WalletGuard.Entidades

Namespace WalletGuard.AccesoDatos

    ''' <summary>
    ''' Acceso a datos para la tabla sr_egresos.
    ''' Solo operaciones CRUD puras, sin lógica de negocio.
    ''' </summary>
    Public Class EgresoDAL

#Region "Consultas"

        ''' <summary>
        ''' Verifica si ya existe un egreso recurrente para un mes/año específico.
        ''' Usado para evitar duplicados durante el procesamiento.
        ''' </summary>
        Public Function ExisteEgresoRecurrente(idRecurrente As Integer,
                                               anio As Integer,
                                               mes As Integer,
                                               conn As SqlConnection,
                                               tran As SqlTransaction) As Boolean
            ' Buscar dentro del mes/año sin importar el día exacto
            Dim sql As String =
                "SELECT COUNT(1) " &
                "FROM   sr_egresos " &
                "WHERE  id_recurrente = @idRec " &
                "  AND  tipo         = 'Recurrente' " &
                "  AND  YEAR(fecha)  = @anio " &
                "  AND  MONTH(fecha) = @mes"

            Using cmd As New SqlCommand(sql, conn, tran)
                cmd.Parameters.AddWithValue("@idRec", idRecurrente)
                cmd.Parameters.AddWithValue("@anio", anio)
                cmd.Parameters.AddWithValue("@mes", mes)
                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        End Function

        ''' <summary>Obtiene todos los egresos ordenados por fecha descendente.</summary>
        Public Function ObtenerTodos() As List(Of Egreso)
            Dim lista As New List(Of Egreso)()

            Dim sql As String =
                "SELECT id_egreso, fecha, monto, id_categoria, tipo, id_recurrente, descripcion " &
                "FROM   sr_egresos " &
                "ORDER  BY fecha DESC"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    Using dr As SqlDataReader = cmd.ExecuteReader()

                        Do While dr.Read()
                            Dim e As New Egreso()
                            e.IdEgreso = dr.GetInt32(dr.GetOrdinal("id_egreso"))
                            e.Fecha = dr.GetDateTime(dr.GetOrdinal("fecha"))
                            e.Monto = dr.GetDecimal(dr.GetOrdinal("monto"))
                            e.IdCategoria = dr.GetInt32(dr.GetOrdinal("id_categoria"))

                            Dim tipoStr As String = dr.GetString(dr.GetOrdinal("tipo"))
                            e.Tipo = CType([Enum].Parse(GetType(TipoEgreso), tipoStr), TipoEgreso)

                            Dim ordRec As Integer = dr.GetOrdinal("id_recurrente")
                            e.IdRecurrente = If(dr.IsDBNull(ordRec), CType(Nothing, Integer?), dr.GetInt32(ordRec))
                            e.Descripcion = If(dr.IsDBNull(dr.GetOrdinal("descripcion")), String.Empty, dr.GetString(dr.GetOrdinal("descripcion")))

                            lista.Add(e)
                        Loop

                    End Using
                End Using
            End Using

            Return lista
        End Function

        ''' <summary>Obtiene egresos de tipo Recurrente para una pantalla de historial.</summary>
        Public Function ObtenerRecurrentes() As List(Of Egreso)
            Dim lista As New List(Of Egreso)()

            Dim sql As String =
                "SELECT id_egreso, fecha, monto, id_categoria, tipo, id_recurrente, descripcion " &
                "FROM   sr_egresos " &
                "WHERE  tipo = 'Recurrente' " &
                "ORDER  BY fecha DESC"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    Using dr As SqlDataReader = cmd.ExecuteReader()

                        Do While dr.Read()
                            Dim e As New Egreso()
                            e.IdEgreso = dr.GetInt32(dr.GetOrdinal("id_egreso"))
                            e.Fecha = dr.GetDateTime(dr.GetOrdinal("fecha"))
                            e.Monto = dr.GetDecimal(dr.GetOrdinal("monto"))
                            e.IdCategoria = dr.GetInt32(dr.GetOrdinal("id_categoria"))
                            e.Tipo = TipoEgreso.Recurrente

                            Dim ordRec As Integer = dr.GetOrdinal("id_recurrente")
                            e.IdRecurrente = If(dr.IsDBNull(ordRec), CType(Nothing, Integer?), dr.GetInt32(ordRec))
                            e.Descripcion = If(dr.IsDBNull(dr.GetOrdinal("descripcion")), String.Empty, dr.GetString(dr.GetOrdinal("descripcion")))

                            lista.Add(e)
                        Loop

                    End Using
                End Using
            End Using

            Return lista
        End Function

#End Region

#Region "Comandos"

        ''' <summary>
        ''' Inserta un egreso dentro de una transacción activa.
        ''' El servicio de procesamiento llama este método por cada mes pendiente.
        ''' </summary>
        Public Sub InsertarEnTransaccion(egreso As Egreso,
                                          conn As SqlConnection,
                                          tran As SqlTransaction)
            Dim sql As String =
                "INSERT INTO sr_egresos (fecha, monto, id_categoria, tipo, id_recurrente, descripcion) " &
                "VALUES (@fecha, @monto, @idCat, @tipo, @idRec, @desc)"

            Using cmd As New SqlCommand(sql, conn, tran)
                cmd.Parameters.AddWithValue("@fecha", egreso.Fecha)
                cmd.Parameters.AddWithValue("@monto", egreso.Monto)
                cmd.Parameters.AddWithValue("@idCat", egreso.IdCategoria)
                cmd.Parameters.AddWithValue("@tipo", egreso.Tipo.ToString())
                cmd.Parameters.AddWithValue("@idRec", If(egreso.IdRecurrente.HasValue, CObj(egreso.IdRecurrente.Value), DBNull.Value))
                cmd.Parameters.AddWithValue("@desc", If(String.IsNullOrWhiteSpace(egreso.Descripcion), DBNull.Value, CObj(egreso.Descripcion)))
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        ''' <summary>Inserta un egreso con conexión propia (sin transacción externa).</summary>
        Public Function Insertar(egreso As Egreso) As Integer
            Dim sql As String =
                "INSERT INTO sr_egresos (fecha, monto, id_categoria, tipo, id_recurrente, descripcion) " &
                "VALUES (@fecha, @monto, @idCat, @tipo, @idRec, @desc); " &
                "SELECT SCOPE_IDENTITY();"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@fecha", egreso.Fecha)
                    cmd.Parameters.AddWithValue("@monto", egreso.Monto)
                    cmd.Parameters.AddWithValue("@idCat", egreso.IdCategoria)
                    cmd.Parameters.AddWithValue("@tipo", egreso.Tipo.ToString())
                    cmd.Parameters.AddWithValue("@idRec", If(egreso.IdRecurrente.HasValue, CObj(egreso.IdRecurrente.Value), DBNull.Value))
                    cmd.Parameters.AddWithValue("@desc", If(String.IsNullOrWhiteSpace(egreso.Descripcion), DBNull.Value, CObj(egreso.Descripcion)))
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        End Function

#End Region


        ''' <summary>Suma total de egresos de un mes/año.</summary>
        Public Function ObtenerTotalMes(mes As Integer, anio As Integer) As Decimal
            Dim sql As String =
                "SELECT ISNULL(SUM(monto), 0) " &
                "FROM   sr_egresos " &
                "WHERE  MONTH(fecha) = @mes AND YEAR(fecha) = @anio"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@mes", mes)
                    cmd.Parameters.AddWithValue("@anio", anio)
                    Return Convert.ToDecimal(cmd.ExecuteScalar())
                End Using
            End Using
        End Function

        ''' <summary>Devuelve todos los egresos de un mes/año para el calendario.</summary>
        Public Function ObtenerPorMes(mes As Integer, anio As Integer) As List(Of Egreso)
            Dim lista As New List(Of Egreso)()

            Dim sql As String =
                "SELECT id_egreso, fecha, monto, id_categoria, tipo, id_recurrente, descripcion " &
                "FROM   sr_egresos " &
                "WHERE  MONTH(fecha) = @mes AND YEAR(fecha) = @anio"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@mes", mes)
                    cmd.Parameters.AddWithValue("@anio", anio)

                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        Do While dr.Read()
                            Dim eg As New Egreso()
                            eg.IdEgreso = dr.GetInt32(dr.GetOrdinal("id_egreso"))
                            eg.Fecha = dr.GetDateTime(dr.GetOrdinal("fecha"))
                            eg.Monto = dr.GetDecimal(dr.GetOrdinal("monto"))
                            eg.IdCategoria = dr.GetInt32(dr.GetOrdinal("id_categoria"))

                            Dim tipoStr As String = dr.GetString(dr.GetOrdinal("tipo"))
                            eg.Tipo = CType([Enum].Parse(GetType(TipoEgreso), tipoStr), TipoEgreso)

                            Dim ordRec As Integer = dr.GetOrdinal("id_recurrente")
                            eg.IdRecurrente = If(dr.IsDBNull(ordRec), CType(Nothing, Integer?), dr.GetInt32(ordRec))

                            Dim ordDesc As Integer = dr.GetOrdinal("descripcion")
                            eg.Descripcion = If(dr.IsDBNull(ordDesc), String.Empty, dr.GetString(ordDesc))

                            lista.Add(eg)
                        Loop
                    End Using
                End Using
            End Using

            Return lista
        End Function

        ''' <summary>
        ''' Resumen de egresos agrupados por categoría para el gráfico donut.
        ''' </summary>
        Public Function ObtenerResumenPorCategoria(mes As Integer, anio As Integer) As List(Of ResumenCategoria)
            Dim lista As New List(Of ResumenCategoria)()

            Dim sql As String =
                "SELECT   c.nombre        AS nombre_categoria, " &
                "         SUM(e.monto)    AS total " &
                "FROM     sr_egresos   e " &
                "JOIN     sr_categorias c ON c.id_categoria = e.id_categoria " &
                "WHERE    MONTH(e.fecha) = @mes AND YEAR(e.fecha) = @anio " &
                "GROUP BY c.nombre " &
                "ORDER BY total DESC"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@mes", mes)
                    cmd.Parameters.AddWithValue("@anio", anio)

                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        Do While dr.Read()
                            Dim rc As New ResumenCategoria()
                            rc.NombreCategoria = dr.GetString(0)
                            rc.Total = dr.GetDecimal(1)
                            lista.Add(rc)
                        Loop
                    End Using
                End Using
            End Using

            Return lista
        End Function

    End Class

End Namespace
