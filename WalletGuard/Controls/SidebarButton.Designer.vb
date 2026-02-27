Imports System.Windows.Forms
Imports WalletGuard.WalletGuard.Theme

Namespace WalletGuard.Controls

    ''' <summary>
    ''' DESIGNER — Solo declaración de controles e InitializeComponent.
    ''' NO debe contener lógica de negocio ni manejo de eventos.
    ''' </summary>
    Partial Public Class SidebarButton

        ' ── Disposición de recursos ──────────────────────────────────────────
        Private _disposed As Boolean = False

        Protected Overrides Sub Dispose(disposing As Boolean)
            If Not _disposed Then
                If disposing Then
                    ' Aquí se liberarían componentes generados (Timer, etc.)
                End If
                _disposed = True
            End If
            MyBase.Dispose(disposing)
        End Sub

        ' ── InitializeComponent ──────────────────────────────────────────────
        ''' <summary>
        ''' Configura las propiedades base del control.
        ''' Solo propiedades visuales y de comportamiento del propio control;
        ''' sin lógica de negocio.
        ''' </summary>
        Private Sub InitializeComponent()
            ' Habilitar doble buffer y pintado personalizado
            SetStyle(ControlStyles.AllPaintingInWmPaint  Or
                     ControlStyles.UserPaint             Or
                     ControlStyles.OptimizedDoubleBuffer Or
                     ControlStyles.ResizeRedraw, True)

            ' Dimensiones y cursor
            Me.Height    = AppTheme.SidebarItemHeight
            Me.Cursor    = Cursors.Hand
            Me.BackColor = AppTheme.SidebarBackground
        End Sub

    End Class

End Namespace
