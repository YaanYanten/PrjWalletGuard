Imports System.Drawing

Namespace WalletGuard.Theme

    ''' <summary>
    ''' Sistema de temas centralizado.
    ''' Preparado para soporte futuro de tema oscuro / claro:
    ''' bastará con intercambiar las propiedades en tiempo de ejecución.
    ''' </summary>
    Public Module AppTheme

        ' ── Colores ──────────────────────────────────────────────────────────
        Public ReadOnly Property SidebarBackground As Color = Color.FromArgb(20, 25, 40)
        Public ReadOnly Property SidebarItemHover   As Color = Color.FromArgb(40, 50, 75)
        Public ReadOnly Property SidebarItemActive  As Color = Color.FromArgb(60, 120, 200)
        Public ReadOnly Property SidebarText        As Color = Color.FromArgb(200, 210, 230)
        Public ReadOnly Property SidebarIcon        As Color = Color.FromArgb(120, 160, 220)

        Public ReadOnly Property MainBackground     As Color = Color.FromArgb(30, 35, 55)
        Public ReadOnly Property HeaderBackground   As Color = Color.FromArgb(15, 20, 35)
        Public ReadOnly Property AccentColor        As Color = Color.FromArgb(60, 120, 200)
        Public ReadOnly Property CardBackground     As Color = Color.FromArgb(40, 48, 72)

        Public ReadOnly Property TextPrimary        As Color = Color.FromArgb(230, 235, 245)
        Public ReadOnly Property TextSecondary      As Color = Color.FromArgb(140, 155, 185)

        ' ── Tipografía ───────────────────────────────────────────────────────
        Public ReadOnly Property FontNormal  As New Font("Segoe UI", 9.5F,  FontStyle.Regular)
        Public ReadOnly Property FontBold    As New Font("Segoe UI", 9.5F,  FontStyle.Bold)
        Public ReadOnly Property FontSmall   As New Font("Segoe UI", 8.0F,  FontStyle.Regular)
        Public ReadOnly Property FontTitle   As New Font("Segoe UI", 11.0F, FontStyle.Bold)
        Public ReadOnly Property FontLarge   As New Font("Segoe UI", 18.0F, FontStyle.Bold)

        ' ── Medidas del Sidebar ───────────────────────────────────────────────
        Public Const SidebarExpandedWidth   As Integer = 220
        Public Const SidebarCollapsedWidth  As Integer = 60
        Public Const SidebarItemHeight      As Integer = 48
        Public Const SidebarAnimationStep   As Integer = 12   ' px / tick
        Public Const SidebarAnimationMs     As Integer = 8    ' ms / tick ≈ 120 fps

    End Module

End Namespace
