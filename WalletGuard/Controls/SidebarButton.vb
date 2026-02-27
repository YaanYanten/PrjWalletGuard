Imports System.Drawing
Imports System.Windows.Forms
Imports WalletGuard.WalletGuard.Theme

Namespace WalletGuard.Controls

    ''' <summary>
    ''' Botón de navegación del Sidebar con renderizado GDI+ personalizado.
    ''' ──────────────────────────────────────────────────────────────────
    ''' SEPARACIÓN DE RESPONSABILIDADES
    '''   SidebarButton.vb          → Lógica, propiedades, eventos, pintado
    '''   SidebarButton.Designer.vb → Declaración de campos e InitializeComponent
    ''' </summary>
    Partial Public Class SidebarButton
        Inherits Control

#Region "Propiedades públicas"

        Private _icon As Bitmap = Nothing
        Private _title As String = ""
        Private _isActive As Boolean = False
        Private _isHovered As Boolean = False
        Private _showText As Boolean = True

        Public Property Icon As Bitmap
            Get
                Return _icon : End Get
            Set(value As Bitmap)
                _icon = value
                Invalidate()
            End Set
        End Property

        Public Property Title As String
            Get
                Return _title : End Get
            Set(value As String)
                _title = value
                Invalidate()
            End Set
        End Property

        Public Property IsActive As Boolean
            Get
                Return _isActive : End Get
            Set(value As Boolean)
                _isActive = value
                Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' True = sidebar expandido (muestra texto + ícono).
        ''' False = sidebar colapsado (solo ícono).
        ''' </summary>
        Public Property ShowText As Boolean
            Get
                Return _showText : End Get
            Set(value As Boolean)
                _showText = value
                Invalidate()
            End Set
        End Property

#End Region

#Region "Constructor"

        Public Sub New()
            InitializeComponent()
        End Sub

#End Region

#Region "Eventos de ratón"

        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            MyBase.OnMouseEnter(e)
            _isHovered = True
            Invalidate()
        End Sub

        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            MyBase.OnMouseLeave(e)
            _isHovered = False
            Invalidate()
        End Sub

#End Region

#Region "Pintado personalizado"

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)

            Dim g As Graphics = e.Graphics
            g.SmoothingMode       = Drawing2D.SmoothingMode.AntiAlias
            g.TextRenderingHint   = Drawing.Text.TextRenderingHint.ClearTypeGridFit

            PaintBackground(g)
            PaintActiveAccent(g)
            PaintIcon(g)
            If _showText Then PaintText(g)
        End Sub

        ''' <summary>Rellena el fondo según el estado (activo / hover / normal).</summary>
        Private Sub PaintBackground(g As Graphics)
            Dim bgColor As Color
            If _isActive Then
                bgColor = AppTheme.SidebarItemActive
            ElseIf _isHovered Then
                bgColor = AppTheme.SidebarItemHover
            Else
                bgColor = AppTheme.SidebarBackground
            End If

            Dim offsetX As Integer = If(_isActive, 4, 0)
            Using br As New SolidBrush(bgColor)
                g.FillRectangle(br, offsetX, 0, Width - offsetX, Height)
            End Using
        End Sub

        ''' <summary>Dibuja la barra de acento izquierda cuando el ítem está activo.</summary>
        Private Sub PaintActiveAccent(g As Graphics)
            If Not _isActive Then Return
            Using br As New SolidBrush(AppTheme.AccentColor)
                g.FillRectangle(br, 0, 0, 4, Height)
            End Using
        End Sub

        ''' <summary>Dibuja el ícono centrado dentro de los primeros 60px.</summary>
        Private Sub PaintIcon(g As Graphics)
            If _icon Is Nothing Then Return

            Dim iconSize As Integer = 20
            Dim iconRect As New Rectangle(
        (AppTheme.SidebarCollapsedWidth - iconSize) \ 2,
        (Height - iconSize) \ 2,
        iconSize,
        iconSize)

            ' Dibujar siempre con colores originales sin ninguna ColorMatrix
            g.DrawImage(_icon, iconRect)
        End Sub

        ''' <summary>Dibuja el texto a la derecha del ícono (solo en modo expandido).</summary>
        Private Sub PaintText(g As Graphics)
            If String.IsNullOrWhiteSpace(_title) Then Return

            Dim textColor As Color = If(_isActive, Color.White, AppTheme.SidebarText)
            Dim textRect  As New Rectangle(AppTheme.SidebarCollapsedWidth, 0,
                                           Width - AppTheme.SidebarCollapsedWidth - 8,
                                           Height)
            Dim fnt As Font = If(_isActive, AppTheme.FontBold, AppTheme.FontNormal)

            Using br  As New SolidBrush(textColor)
            Using fmt As New StringFormat()
                fmt.Alignment     = StringAlignment.Near
                fmt.LineAlignment = StringAlignment.Center
                fmt.Trimming      = StringTrimming.EllipsisCharacter
                g.DrawString(_title, fnt, br, textRect, fmt)
            End Using
            End Using
        End Sub

#End Region

    End Class

End Namespace
