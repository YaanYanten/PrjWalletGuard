Imports System.Drawing
Imports System.Windows.Forms
Imports WalletGuard.WalletGuard.Theme

Namespace WalletGuard.Controls

    ''' <summary>
    ''' DESIGNER de MetricCard.
    ''' Solo declaración de sub-controles e InitializeComponent.
    ''' </summary>
    Partial Public Class MetricCard

        ' ── Sub-controles ─────────────────────────────────────────────────────
        ' Friend: visibles desde MetricCard.vb para asignar valores.
        Friend LblTitle As Label
        Friend LblValue As Label
        Friend LblTrend As Label
        Private PnlAccent As Panel

        ' ── Disposición ───────────────────────────────────────────────────────
        Private _disposed As Boolean = False

        Protected Overrides Sub Dispose(disposing As Boolean)
            If Not _disposed Then
                _disposed = True
            End If
            MyBase.Dispose(disposing)
        End Sub

        ' ── InitializeComponent ───────────────────────────────────────────────
        Private Sub InitializeComponent()

            ' ── Instanciación ─────────────────────────────────────────────────
            Me.PnlAccent = New Panel()
            Me.LblTitle = New Label()
            Me.LblValue = New Label()
            Me.LblTrend = New Label()

            ' ── Panel base ────────────────────────────────────────────────────
            Me.Size = New Size(200, 110)
            Me.BackColor = AppTheme.CardBackground
            Me.Cursor = Cursors.Hand

            ' ── PnlAccent ─────────────────────────────────────────────────────
            Me.PnlAccent.Name = "PnlAccent"
            Me.PnlAccent.BackColor = AppTheme.AccentColor
            Me.PnlAccent.Bounds = New Rectangle(0, 0, 4, 110)

            ' ── LblTitle ──────────────────────────────────────────────────────
            Me.LblTitle.Name = "LblTitle"
            Me.LblTitle.Text = String.Empty
            Me.LblTitle.ForeColor = AppTheme.TextSecondary
            Me.LblTitle.Font = AppTheme.FontSmall
            Me.LblTitle.AutoSize = False
            Me.LblTitle.Bounds = New Rectangle(16, 14, 168, 20)
            Me.LblTitle.TextAlign = ContentAlignment.MiddleLeft

            ' ── LblValue ──────────────────────────────────────────────────────
            Me.LblValue.Name = "LblValue"
            Me.LblValue.Text = String.Empty
            Me.LblValue.ForeColor = AppTheme.TextPrimary
            Me.LblValue.Font = New Font("Segoe UI", 22.0F, FontStyle.Bold)
            Me.LblValue.AutoSize = False
            Me.LblValue.Bounds = New Rectangle(14, 34, 172, 38)
            Me.LblValue.TextAlign = ContentAlignment.MiddleLeft

            ' ── LblTrend ──────────────────────────────────────────────────────
            Me.LblTrend.Name = "LblTrend"
            Me.LblTrend.Text = String.Empty
            Me.LblTrend.ForeColor = Color.FromArgb(80, 200, 140)
            Me.LblTrend.Font = AppTheme.FontSmall
            Me.LblTrend.AutoSize = False
            Me.LblTrend.Bounds = New Rectangle(16, 74, 168, 20)
            Me.LblTrend.TextAlign = ContentAlignment.MiddleLeft

            ' ── Composición ───────────────────────────────────────────────────
            Me.Controls.Add(Me.PnlAccent)
            Me.Controls.Add(Me.LblTitle)
            Me.Controls.Add(Me.LblValue)
            Me.Controls.Add(Me.LblTrend)

        End Sub

    End Class

End Namespace
