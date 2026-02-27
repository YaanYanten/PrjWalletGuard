Namespace WalletGuard.Entidades

    ''' <summary>
    ''' Resumen devuelto por FinancialRecurringService.ProcessPendingRecurring().
    ''' Permite que la capa de presentación muestre feedback sin conocer detalles internos.
    ''' </summary>
    Public Class ResultadoProcesamiento

        ''' <summary>Cantidad total de egresos recurrentes generados en esta ejecución.</summary>
        Public Property TotalGenerados As Integer = 0

        ''' <summary>Detalle por recurrente: nombre → cantidad de meses generados.</summary>
        Public Property Detalle As New List(Of DetalleRecurrente)()

        ''' <summary>Indica si ocurrió algún error durante el proceso.</summary>
        Public Property HuboErrores As Boolean = False

        ''' <summary>Mensaje de error en caso de fallo general.</summary>
        Public Property MensajeError As String = String.Empty

    End Class

    ''' <summary>Línea de detalle para un recurrente individual.</summary>
    Public Class DetalleRecurrente
        Public Property Nombre           As String = String.Empty
        Public Property MesesGenerados   As Integer
        Public Property MontoPorMes      As Decimal
        Public Property TotalGenerado    As Decimal
    End Class

End Namespace
