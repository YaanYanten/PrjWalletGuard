Namespace WalletGuard.Entidades

    ''' <summary>
    ''' Representa un gasto recurrente configurado por el usuario.
    ''' Mapea directamente la tabla sr_gastos_recurrentes.
    ''' </summary>
    Public Class GastoRecurrente

        Public Property IdRecurrente       As Integer
        Public Property Nombre             As String = String.Empty
        Public Property Monto              As Decimal
        Public Property DiaCorte           As Integer
        Public Property IdCategoria        As Integer
        Public Property Activo             As Boolean
        Public Property FechaInicio        As Date
        Public Property FechaFin           As Date?             ' Nullable: sin fin = recurrente indefinido
        Public Property FechaUltimoProcesado As Date?           ' Nullable: NULL = nunca procesado

    End Class

End Namespace
