Imports System.Data
Imports Microsoft.Data.SqlClient
Imports DL_WalletGuard.WalletGuard.Entidades

Namespace WalletGuard.AccesoDatos

    ''' <summary>
    ''' Acceso a datos para la tabla sr_gastos_recurrentes.
    ''' Solo operaciones CRUD puras, sin lógica de negocio.
    ''' </summary>
    Public Class GastoRecurrenteDAL

#Region "Consultas"

        ''' <summary>
        ''' Devuelve todos los gastos recurrentes activos.
        ''' Usado por el servicio para calcular pendientes.
        ''' </summary>
        Public Function ObtenerActivos() As List(Of GastoRecurrente)
            Dim lista As New List(Of GastoRecurrente)()

            Dim sql As String =
                "SELECT id_recurrente, nombre, monto, dia_corte, id_categoria, " &
                "       activo, fecha_inicio, fecha_fin, fecha_ultimo_procesado " &
                "FROM   sr_gastos_recurrentes " &
                "WHERE  activo = 1"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    Using dr As SqlDataReader = cmd.ExecuteReader()

                        Do While dr.Read()
                            Dim gr As New GastoRecurrente()
                            gr.IdRecurrente = dr.GetInt32(dr.GetOrdinal("id_recurrente"))
                            gr.Nombre = dr.GetString(dr.GetOrdinal("nombre"))
                            gr.Monto = dr.GetDecimal(dr.GetOrdinal("monto"))
                            gr.DiaCorte = dr.GetInt32(dr.GetOrdinal("dia_corte"))
                            gr.IdCategoria = dr.GetInt32(dr.GetOrdinal("id_categoria"))
                            gr.Activo = dr.GetBoolean(dr.GetOrdinal("activo"))
                            gr.FechaInicio = dr.GetDateTime(dr.GetOrdinal("fecha_inicio"))

                            Dim ordFin As Integer = dr.GetOrdinal("fecha_fin")
                            Dim ordUltimo As Integer = dr.GetOrdinal("fecha_ultimo_procesado")

                            gr.FechaFin = If(dr.IsDBNull(ordFin), CType(Nothing, Date?), dr.GetDateTime(ordFin))
                            gr.FechaUltimoProcesado = If(dr.IsDBNull(ordUltimo), CType(Nothing, Date?), dr.GetDateTime(ordUltimo))

                            lista.Add(gr)
                        Loop

                    End Using
                End Using
            End Using

            Return lista
        End Function

        ''' <summary>
        ''' Obtiene todos los recurrentes (activos e inactivos) para la pantalla de gestión.
        ''' </summary>
        Public Function ObtenerTodos() As List(Of GastoRecurrente)
            Dim lista As New List(Of GastoRecurrente)()

            Dim sql As String =
                "SELECT id_recurrente, nombre, monto, dia_corte, id_categoria, " &
                "       activo, fecha_inicio, fecha_fin, fecha_ultimo_procesado " &
                "FROM   sr_gastos_recurrentes " &
                "ORDER  BY nombre"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    Using dr As SqlDataReader = cmd.ExecuteReader()

                        Do While dr.Read()
                            Dim gr As New GastoRecurrente()
                            gr.IdRecurrente = dr.GetInt32(dr.GetOrdinal("id_recurrente"))
                            gr.Nombre = dr.GetString(dr.GetOrdinal("nombre"))
                            gr.Monto = dr.GetDecimal(dr.GetOrdinal("monto"))
                            gr.DiaCorte = dr.GetInt32(dr.GetOrdinal("dia_corte"))
                            gr.IdCategoria = dr.GetInt32(dr.GetOrdinal("id_categoria"))
                            gr.Activo = dr.GetBoolean(dr.GetOrdinal("activo"))
                            gr.FechaInicio = dr.GetDateTime(dr.GetOrdinal("fecha_inicio"))

                            Dim ordFin As Integer = dr.GetOrdinal("fecha_fin")
                            Dim ordUltimo As Integer = dr.GetOrdinal("fecha_ultimo_procesado")

                            gr.FechaFin = If(dr.IsDBNull(ordFin), CType(Nothing, Date?), dr.GetDateTime(ordFin))
                            gr.FechaUltimoProcesado = If(dr.IsDBNull(ordUltimo), CType(Nothing, Date?), dr.GetDateTime(ordUltimo))

                            lista.Add(gr)
                        Loop

                    End Using
                End Using
            End Using

            Return lista
        End Function

#End Region

#Region "Comandos"

        ''' <summary>Inserta un nuevo gasto recurrente.</summary>
        Public Function Insertar(gr As GastoRecurrente) As Integer
            Dim sql As String =
                "INSERT INTO sr_gastos_recurrentes " &
                "  (nombre, monto, dia_corte, id_categoria, activo, fecha_inicio, fecha_fin, fecha_ultimo_procesado) " &
                "VALUES " &
                "  (@nombre, @monto, @diaCorte, @idCat, @activo, @fechaInicio, @fechaFin, NULL); " &
                "SELECT SCOPE_IDENTITY();"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)

                    cmd.Parameters.AddWithValue("@nombre", gr.Nombre)
                    cmd.Parameters.AddWithValue("@monto", gr.Monto)
                    cmd.Parameters.AddWithValue("@diaCorte", gr.DiaCorte)
                    cmd.Parameters.AddWithValue("@idCat", gr.IdCategoria)
                    cmd.Parameters.AddWithValue("@activo", gr.Activo)
                    cmd.Parameters.AddWithValue("@fechaInicio", gr.FechaInicio)
                    cmd.Parameters.AddWithValue("@fechaFin", If(gr.FechaFin.HasValue, CObj(gr.FechaFin.Value), DBNull.Value))

                    Return Convert.ToInt32(cmd.ExecuteScalar())

                End Using
            End Using
        End Function

        ''' <summary>Actualiza los datos de un gasto recurrente existente.</summary>
        Public Sub Actualizar(gr As GastoRecurrente)
            Dim sql As String =
                "UPDATE sr_gastos_recurrentes " &
                "SET    nombre      = @nombre, " &
                "       monto       = @monto, " &
                "       dia_corte   = @diaCorte, " &
                "       id_categoria = @idCat, " &
                "       activo      = @activo, " &
                "       fecha_inicio = @fechaInicio, " &
                "       fecha_fin   = @fechaFin " &
                "WHERE  id_recurrente = @id"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)

                    cmd.Parameters.AddWithValue("@nombre", gr.Nombre)
                    cmd.Parameters.AddWithValue("@monto", gr.Monto)
                    cmd.Parameters.AddWithValue("@diaCorte", gr.DiaCorte)
                    cmd.Parameters.AddWithValue("@idCat", gr.IdCategoria)
                    cmd.Parameters.AddWithValue("@activo", gr.Activo)
                    cmd.Parameters.AddWithValue("@fechaInicio", gr.FechaInicio)
                    cmd.Parameters.AddWithValue("@fechaFin", If(gr.FechaFin.HasValue, CObj(gr.FechaFin.Value), DBNull.Value))
                    cmd.Parameters.AddWithValue("@id", gr.IdRecurrente)

                    cmd.ExecuteNonQuery()

                End Using
            End Using
        End Sub

        ''' <summary>
        ''' Actualiza SOLO fecha_ultimo_procesado dentro de una transacción existente.
        ''' Llamado por el servicio de procesamiento; recibe la conexión y transacción activas.
        ''' </summary>
        Public Sub ActualizarFechaUltimoProcesado(idRecurrente As Integer,
                                                   fecha As Date,
                                                   conn As SqlConnection,
                                                   tran As SqlTransaction)
            Dim sql As String =
                "UPDATE sr_gastos_recurrentes " &
                "SET    fecha_ultimo_procesado = @fecha " &
                "WHERE  id_recurrente = @id"

            Using cmd As New SqlCommand(sql, conn, tran)
                cmd.Parameters.AddWithValue("@fecha", fecha)
                cmd.Parameters.AddWithValue("@id", idRecurrente)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        ''' <summary>Elimina (lógicamente) un recurrente marcándolo inactivo.</summary>
        Public Sub DesactivarRecurrente(idRecurrente As Integer)
            Dim sql As String =
                "UPDATE sr_gastos_recurrentes SET activo = 0 WHERE id_recurrente = @id"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", idRecurrente)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

#End Region

    End Class

End Namespace
