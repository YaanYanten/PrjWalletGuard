Namespace WalletGuard.Entidades

    ''' <summary>
    ''' Representa un egreso registrado en la tabla sr_egresos.
    ''' El campo Tipo distingue origen: Normal / Recurrente / Deuda.
    ''' </summary>
    Public Class Egreso

        Public Property IdEgreso      As Integer
        Public Property Fecha         As Date
        Public Property Monto         As Decimal
        Public Property IdCategoria   As Integer
        Public Property Tipo          As TipoEgreso
        Public Property IdRecurrente  As Integer?       ' Nullable: solo aplica cuando Tipo = Recurrente
        Public Property Descripcion   As String = String.Empty

    End Class

    ''' <summary>Tipos válidos de egreso según modelo de datos.</summary>
    Public Enum TipoEgreso
        Normal
        Recurrente
        Deuda
    End Enum

End Namespace
