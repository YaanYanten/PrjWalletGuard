Imports Microsoft.Data.SqlClient
Imports DL_WalletGuard.WalletGuard.Entidades

Namespace WalletGuard.AccesoDatos

    ''' <summary>
    ''' Acceso a datos para la tabla sr_categorias.
    ''' Usado por FormRegistroMovimiento para poblar el ComboBox.
    ''' </summary>
    Public Class CategoriaDAL

        ''' <summary>Devuelve todas las categorías activas ordenadas por nombre.</summary>
        Public Function ObtenerTodas() As List(Of Categoria)
            Dim lista As New List(Of Categoria)()

            Dim sql As String =
                "SELECT id_categoria, nombre, ISNULL(tipo,'Ambos') AS tipo " &
                "FROM   sr_categorias " &
                "ORDER  BY nombre"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    Using dr As SqlDataReader = cmd.ExecuteReader()

                        Do While dr.Read()
                            Dim cat As New Categoria()
                            cat.IdCategoria = dr.GetInt32(dr.GetOrdinal("id_categoria"))
                            cat.Nombre = dr.GetString(dr.GetOrdinal("nombre"))
                            cat.Tipo = dr.GetString(dr.GetOrdinal("tipo"))
                            lista.Add(cat)
                        Loop

                    End Using
                End Using
            End Using

            Return lista
        End Function

    End Class

End Namespace
