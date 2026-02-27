Imports System.Drawing
Imports System.Windows.Forms
Imports WalletGuard.WalletGuard.Theme

Namespace WalletGuard.Controls

    ''' <summary>
    ''' Tarjeta de métrica reutilizable para el Dashboard.
    ''' Control compuesto: encapsula ícono+título, valor y tendencia.
    ''' 
    ''' SEPARACIÓN:
    '''   MetricCard.vb          → Lógica, propiedades, pintado, hover
    '''   MetricCard.Designer.vb → Declaración de sub-controles e InitializeComponent
    ''' </summary>
    Partial Public Class MetricCard
        Inherits Panel

#Region "Constructor"

        Public Sub New()
            InitializeComponent()   ' ← definido en el Designer
            WireHoverEvents()
        End Sub

#End Region

#Region "API pública"

        ''' <summary>
        ''' Asigna los valores visibles de la tarjeta.
        ''' Llamado desde la lógica del formulario padre, no desde el Designer.
        ''' </summary>
        Public Sub SetValues(title As String, value As String, trend As String)
            LblTitle.Text = title
            LblValue.Text = value
            LblTrend.Text = trend
        End Sub

#End Region

#Region "Eventos"

        ''' <summary>Suscribe los efectos de hover. Solo comportamiento, sin lógica de negocio.</summary>
        Private Sub WireHoverEvents()
            AddHandler Me.MouseEnter,   AddressOf OnCardMouseEnter
            AddHandler Me.MouseLeave,   AddressOf OnCardMouseLeave
            ' Propagar hover desde sub-controles al panel
            For Each ctrl As Control In Me.Controls
                AddHandler ctrl.MouseEnter, AddressOf OnCardMouseEnter
                AddHandler ctrl.MouseLeave, AddressOf OnCardMouseLeave
            Next
        End Sub

        Private Sub OnCardMouseEnter(sender As Object, e As EventArgs)
            Me.BackColor = Color.FromArgb(50, 60, 88)
        End Sub

        Private Sub OnCardMouseLeave(sender As Object, e As EventArgs)
            ' Verificar que el cursor realmente salió del card completo
            If Not Me.ClientRectangle.Contains(Me.PointToClient(Cursor.Position)) Then
                Me.BackColor = AppTheme.CardBackground
            End If
        End Sub

#End Region

    End Class

End Namespace
