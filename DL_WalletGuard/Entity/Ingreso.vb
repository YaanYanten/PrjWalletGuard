Namespace WalletGuard.Entidades

    ''' <summary>Mapea la tabla sr_ingresos.</summary>
    Public Class Ingreso
        Public Property IdIngreso As Integer
        Public Property Fecha As Date
        Public Property Monto As Decimal
        Public Property IdCategoria As Integer
        Public Property Descripcion As String = String.Empty
    End Class

End Namespace

