Namespace WalletGuard.Entidades

    ''' <summary>Mapea la tabla sr_categorias.</summary>
    Public Class Categoria
        Public Property IdCategoria As Integer
        Public Property Nombre As String = String.Empty
        Public Property Tipo As String = String.Empty  ' 'Ingreso','Egreso','Ambos'

        ''' <summary>Usado por ComboBox para mostrar el nombre directamente.</summary>
        Public Overrides Function ToString() As String
            Return Nombre
        End Function
    End Class

    ''' <summary>DTO para el resumen de gastos por categoría (gráfico donut).</summary>
    Public Class ResumenCategoria
        Public Property NombreCategoria As String = String.Empty
        Public Property Total As Decimal
    End Class

End Namespace
