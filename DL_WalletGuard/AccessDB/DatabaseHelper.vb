Imports Microsoft.Data.SqlClient

Namespace WalletGuard.AccesoDatos

    ''' <summary>
    ''' Fábrica centralizada de conexiones SQL Server.
    ''' Punto único para cambiar la cadena de conexión o el proveedor.
    ''' </summary>
    Public Module DatabaseHelper

        ''' <summary>
        ''' Cadena de conexión de la aplicación.
        ''' En producción se puede leer desde app.config / appsettings.json.
        ''' </summary>
        Public ReadOnly Property ConnectionString As String =
            "Server=OCDESAEC07\SQLEXPRESS;Database=WalletGuard;Integrated Security=True;TrustServerCertificate=True;"

        ''' <summary>Crea y devuelve una nueva conexión abierta.</summary>
        Public Function GetConnection() As SqlConnection
            Dim conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn
        End Function

    End Module

End Namespace
