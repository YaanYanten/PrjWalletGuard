Imports System.Drawing
Imports System.Windows.Forms

Namespace WalletGuard.Presentacion
    Partial Public Class FormDashboard

        ' ── Título ────────────────────────────────────────────────────────────
        Private LblTitulo As Label

        ' ── Tarjetas de resumen ───────────────────────────────────────────────
        Private PnlTarjetas As Panel
        Private PnlCardIngresos As Panel
        Private LblTitleIngresos As Label
        Friend LblMontoIngresos As Label
        Private PnlCardEgresos As Panel
        Private LblTitleEgresos As Label
        Friend LblMontoEgresos As Label
        Private PnlCardBalance As Panel
        Private LblTitleBalance As Label
        Friend LblMontoBalance As Label

        ' ── Panel izquierdo (calendario) ──────────────────────────────────────
        Private PnlIzquierdo As Panel

        ' ── Navegación del mes ────────────────────────────────────────────────
        Private PnlNavMes As Panel
        Friend WithEvents BtnMesAnterior As Button
        Friend LblMesAnio As Label
        Friend WithEvents BtnMesSiguiente As Button

        ' ── Cabecera de días ──────────────────────────────────────────────────
        Private PnlDiasSemana As Panel

        ' ── Grid de celdas de días ────────────────────────────────────────────
        Friend FlowDias As FlowLayoutPanel

        ' ── Panel derecho (gráficos) ──────────────────────────────────────────
        Private PnlDerecho As Panel
        Private LblTituloDonut As Label
        Friend WithEvents PnlDonut As Panel
        Private LblTituloBarras As Label
        Friend WithEvents PnlBarras As Panel

        Private _disposed As Boolean = False

        Protected Overrides Sub Dispose(disposing As Boolean)
            If Not _disposed Then
                _disposed = True
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            LblTitulo = New Label()
            PnlTarjetas = New Panel()
            PnlCardIngresos = New Panel()
            LblTitleIngresos = New Label()
            LblMontoIngresos = New Label()
            pnlAccentIng = New Panel()
            PnlCardEgresos = New Panel()
            LblTitleEgresos = New Label()
            LblMontoEgresos = New Label()
            pnlAccentEgr = New Panel()
            PnlCardBalance = New Panel()
            LblTitleBalance = New Label()
            LblMontoBalance = New Label()
            pnlAccentBal = New Panel()
            PnlIzquierdo = New Panel()
            FlowDias = New FlowLayoutPanel()
            PnlDiasSemana = New Panel()
            PnlNavMes = New Panel()
            TableLayoutPanel1 = New TableLayoutPanel()
            BtnMesAnterior = New Button()
            BtnMesSiguiente = New Button()
            LblMesAnio = New Label()
            PnlDerecho = New Panel()
            LblTituloDonut = New Label()
            PnlDonut = New Panel()
            LblTituloBarras = New Label()
            PnlBarras = New Panel()
            PnlTarjetas.SuspendLayout()
            PnlCardIngresos.SuspendLayout()
            PnlCardEgresos.SuspendLayout()
            PnlCardBalance.SuspendLayout()
            PnlIzquierdo.SuspendLayout()
            PnlNavMes.SuspendLayout()
            TableLayoutPanel1.SuspendLayout()
            PnlDerecho.SuspendLayout()
            SuspendLayout()
            ' 
            ' LblTitulo
            ' 
            LblTitulo.AutoSize = True
            LblTitulo.Font = New Font("Segoe UI", 10.0F)
            LblTitulo.ForeColor = Color.FromArgb(CByte(190), CByte(200), CByte(220))
            LblTitulo.Location = New Point(0, 0)
            LblTitulo.Name = "LblTitulo"
            LblTitulo.Size = New Size(131, 19)
            LblTitulo.TabIndex = 0
            LblTitulo.Text = "Resumen Financiero"
            ' 
            ' PnlTarjetas
            ' 
            PnlTarjetas.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            PnlTarjetas.BackColor = Color.Transparent
            PnlTarjetas.Controls.Add(PnlCardIngresos)
            PnlTarjetas.Controls.Add(PnlCardEgresos)
            PnlTarjetas.Controls.Add(PnlCardBalance)
            PnlTarjetas.Location = New Point(10, 26)
            PnlTarjetas.Name = "PnlTarjetas"
            PnlTarjetas.Size = New Size(844, 76)
            PnlTarjetas.TabIndex = 1
            ' 
            ' PnlCardIngresos
            ' 
            PnlCardIngresos.BackColor = Color.FromArgb(CByte(28), CByte(38), CByte(30))
            PnlCardIngresos.Controls.Add(LblTitleIngresos)
            PnlCardIngresos.Controls.Add(LblMontoIngresos)
            PnlCardIngresos.Controls.Add(pnlAccentIng)
            PnlCardIngresos.Location = New Point(0, 0)
            PnlCardIngresos.Name = "PnlCardIngresos"
            PnlCardIngresos.Size = New Size(190, 68)
            PnlCardIngresos.TabIndex = 0
            ' 
            ' LblTitleIngresos
            ' 
            LblTitleIngresos.AutoSize = True
            LblTitleIngresos.Font = New Font("Segoe UI", 8.5F)
            LblTitleIngresos.ForeColor = Color.FromArgb(CByte(140), CByte(200), CByte(140))
            LblTitleIngresos.Location = New Point(12, 10)
            LblTitleIngresos.Name = "LblTitleIngresos"
            LblTitleIngresos.Size = New Size(51, 15)
            LblTitleIngresos.TabIndex = 0
            LblTitleIngresos.Text = "Ingresos"
            ' 
            ' LblMontoIngresos
            ' 
            LblMontoIngresos.AutoSize = True
            LblMontoIngresos.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
            LblMontoIngresos.ForeColor = Color.FromArgb(CByte(80), CByte(220), CByte(130))
            LblMontoIngresos.Location = New Point(10, 30)
            LblMontoIngresos.Name = "LblMontoIngresos"
            LblMontoIngresos.Size = New Size(42, 32)
            LblMontoIngresos.TabIndex = 1
            LblMontoIngresos.Text = "$0"
            ' 
            ' pnlAccentIng
            ' 
            pnlAccentIng.BackColor = Color.FromArgb(CByte(60), CByte(180), CByte(90))
            pnlAccentIng.Location = New Point(0, 0)
            pnlAccentIng.Name = "pnlAccentIng"
            pnlAccentIng.Size = New Size(4, 68)
            pnlAccentIng.TabIndex = 2
            ' 
            ' PnlCardEgresos
            ' 
            PnlCardEgresos.BackColor = Color.FromArgb(CByte(38), CByte(28), CByte(28))
            PnlCardEgresos.Controls.Add(LblTitleEgresos)
            PnlCardEgresos.Controls.Add(LblMontoEgresos)
            PnlCardEgresos.Controls.Add(pnlAccentEgr)
            PnlCardEgresos.Location = New Point(202, 0)
            PnlCardEgresos.Name = "PnlCardEgresos"
            PnlCardEgresos.Size = New Size(190, 68)
            PnlCardEgresos.TabIndex = 1
            ' 
            ' LblTitleEgresos
            ' 
            LblTitleEgresos.AutoSize = True
            LblTitleEgresos.Font = New Font("Segoe UI", 8.5F)
            LblTitleEgresos.ForeColor = Color.FromArgb(CByte(210), CByte(140), CByte(140))
            LblTitleEgresos.Location = New Point(12, 10)
            LblTitleEgresos.Name = "LblTitleEgresos"
            LblTitleEgresos.Size = New Size(47, 15)
            LblTitleEgresos.TabIndex = 0
            LblTitleEgresos.Text = "Egresos"
            ' 
            ' LblMontoEgresos
            ' 
            LblMontoEgresos.AutoSize = True
            LblMontoEgresos.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
            LblMontoEgresos.ForeColor = Color.FromArgb(CByte(220), CByte(80), CByte(80))
            LblMontoEgresos.Location = New Point(10, 30)
            LblMontoEgresos.Name = "LblMontoEgresos"
            LblMontoEgresos.Size = New Size(42, 32)
            LblMontoEgresos.TabIndex = 1
            LblMontoEgresos.Text = "$0"
            ' 
            ' pnlAccentEgr
            ' 
            pnlAccentEgr.BackColor = Color.FromArgb(CByte(200), CByte(60), CByte(60))
            pnlAccentEgr.Location = New Point(0, 0)
            pnlAccentEgr.Name = "pnlAccentEgr"
            pnlAccentEgr.Size = New Size(4, 68)
            pnlAccentEgr.TabIndex = 2
            ' 
            ' PnlCardBalance
            ' 
            PnlCardBalance.BackColor = Color.FromArgb(CByte(30), CByte(35), CByte(50))
            PnlCardBalance.Controls.Add(LblTitleBalance)
            PnlCardBalance.Controls.Add(LblMontoBalance)
            PnlCardBalance.Controls.Add(pnlAccentBal)
            PnlCardBalance.Location = New Point(404, 0)
            PnlCardBalance.Name = "PnlCardBalance"
            PnlCardBalance.Size = New Size(190, 68)
            PnlCardBalance.TabIndex = 2
            ' 
            ' LblTitleBalance
            ' 
            LblTitleBalance.AutoSize = True
            LblTitleBalance.Font = New Font("Segoe UI", 8.5F)
            LblTitleBalance.ForeColor = Color.FromArgb(CByte(180), CByte(190), CByte(210))
            LblTitleBalance.Location = New Point(12, 10)
            LblTitleBalance.Name = "LblTitleBalance"
            LblTitleBalance.Size = New Size(48, 15)
            LblTitleBalance.TabIndex = 0
            LblTitleBalance.Text = "Balance"
            ' 
            ' LblMontoBalance
            ' 
            LblMontoBalance.AutoSize = True
            LblMontoBalance.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
            LblMontoBalance.ForeColor = Color.FromArgb(CByte(80), CByte(220), CByte(130))
            LblMontoBalance.Location = New Point(10, 30)
            LblMontoBalance.Name = "LblMontoBalance"
            LblMontoBalance.Size = New Size(42, 32)
            LblMontoBalance.TabIndex = 1
            LblMontoBalance.Text = "$0"
            ' 
            ' pnlAccentBal
            ' 
            pnlAccentBal.BackColor = Color.FromArgb(CByte(100), CByte(150), CByte(220))
            pnlAccentBal.Location = New Point(0, 0)
            pnlAccentBal.Name = "pnlAccentBal"
            pnlAccentBal.Size = New Size(4, 68)
            pnlAccentBal.TabIndex = 2
            ' 
            ' PnlIzquierdo
            ' 
            PnlIzquierdo.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
            PnlIzquierdo.BackColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            PnlIzquierdo.Controls.Add(FlowDias)
            PnlIzquierdo.Controls.Add(PnlDiasSemana)
            PnlIzquierdo.Controls.Add(PnlNavMes)
            PnlIzquierdo.Location = New Point(10, 110)
            PnlIzquierdo.Name = "PnlIzquierdo"
            PnlIzquierdo.Size = New Size(628, 432)
            PnlIzquierdo.TabIndex = 2
            ' 
            ' FlowDias
            ' 
            FlowDias.BackColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            FlowDias.Dock = DockStyle.Fill
            FlowDias.Location = New Point(0, 72)
            FlowDias.Name = "FlowDias"
            FlowDias.Padding = New Padding(4)
            FlowDias.Size = New Size(628, 360)
            FlowDias.TabIndex = 0
            ' 
            ' PnlDiasSemana
            ' 
            PnlDiasSemana.BackColor = Color.FromArgb(CByte(22), CByte(27), CByte(44))
            PnlDiasSemana.Dock = DockStyle.Top
            PnlDiasSemana.Location = New Point(0, 44)
            PnlDiasSemana.Name = "PnlDiasSemana"
            PnlDiasSemana.Size = New Size(628, 28)
            PnlDiasSemana.TabIndex = 1
            ' 
            ' PnlNavMes
            ' 
            PnlNavMes.BackColor = Color.FromArgb(CByte(22), CByte(27), CByte(44))
            PnlNavMes.Controls.Add(TableLayoutPanel1)
            PnlNavMes.Dock = DockStyle.Top
            PnlNavMes.Location = New Point(0, 0)
            PnlNavMes.Name = "PnlNavMes"
            PnlNavMes.Size = New Size(628, 44)
            PnlNavMes.TabIndex = 2
            ' 
            ' TableLayoutPanel1
            ' 
            TableLayoutPanel1.ColumnCount = 3
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
            TableLayoutPanel1.Controls.Add(BtnMesAnterior, 0, 0)
            TableLayoutPanel1.Controls.Add(BtnMesSiguiente, 2, 0)
            TableLayoutPanel1.Controls.Add(LblMesAnio, 1, 0)
            TableLayoutPanel1.Dock = DockStyle.Fill
            TableLayoutPanel1.Location = New Point(0, 0)
            TableLayoutPanel1.Name = "TableLayoutPanel1"
            TableLayoutPanel1.RowCount = 1
            TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
            TableLayoutPanel1.Size = New Size(628, 44)
            TableLayoutPanel1.TabIndex = 3
            ' 
            ' BtnMesAnterior
            ' 
            BtnMesAnterior.Anchor = AnchorStyles.Top Or AnchorStyles.Right
            BtnMesAnterior.BackColor = Color.Transparent
            BtnMesAnterior.Cursor = Cursors.Hand
            BtnMesAnterior.FlatAppearance.BorderSize = 0
            BtnMesAnterior.FlatStyle = FlatStyle.Flat
            BtnMesAnterior.Font = New Font("Segoe UI", 14.0F, FontStyle.Bold)
            BtnMesAnterior.ForeColor = Color.FromArgb(CByte(180), CByte(190), CByte(220))
            BtnMesAnterior.Location = New Point(170, 3)
            BtnMesAnterior.Name = "BtnMesAnterior"
            BtnMesAnterior.Size = New Size(36, 36)
            BtnMesAnterior.TabIndex = 0
            BtnMesAnterior.Text = "‹"
            BtnMesAnterior.UseVisualStyleBackColor = False
            ' 
            ' BtnMesSiguiente
            ' 
            BtnMesSiguiente.BackColor = Color.Transparent
            BtnMesSiguiente.Cursor = Cursors.Hand
            BtnMesSiguiente.FlatAppearance.BorderSize = 0
            BtnMesSiguiente.FlatStyle = FlatStyle.Flat
            BtnMesSiguiente.Font = New Font("Segoe UI", 14.0F, FontStyle.Bold)
            BtnMesSiguiente.ForeColor = Color.FromArgb(CByte(180), CByte(190), CByte(220))
            BtnMesSiguiente.Location = New Point(421, 3)
            BtnMesSiguiente.Name = "BtnMesSiguiente"
            BtnMesSiguiente.Size = New Size(36, 36)
            BtnMesSiguiente.TabIndex = 2
            BtnMesSiguiente.Text = "›"
            BtnMesSiguiente.UseVisualStyleBackColor = False
            ' 
            ' LblMesAnio
            ' 
            LblMesAnio.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
            LblMesAnio.ForeColor = Color.FromArgb(CByte(220), CByte(228), CByte(245))
            LblMesAnio.Location = New Point(212, 0)
            LblMesAnio.Name = "LblMesAnio"
            LblMesAnio.Size = New Size(203, 36)
            LblMesAnio.TabIndex = 1
            LblMesAnio.Text = "MES 2024"
            LblMesAnio.TextAlign = ContentAlignment.MiddleCenter
            ' 
            ' PnlDerecho
            ' 
            PnlDerecho.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
            PnlDerecho.BackColor = Color.FromArgb(CByte(22), CByte(27), CByte(44))
            PnlDerecho.Controls.Add(LblTituloDonut)
            PnlDerecho.Controls.Add(PnlDonut)
            PnlDerecho.Controls.Add(LblTituloBarras)
            PnlDerecho.Controls.Add(PnlBarras)
            PnlDerecho.Location = New Point(644, 110)
            PnlDerecho.Name = "PnlDerecho"
            PnlDerecho.Size = New Size(252, 432)
            PnlDerecho.TabIndex = 3
            ' 
            ' LblTituloDonut
            ' 
            LblTituloDonut.AutoSize = True
            LblTituloDonut.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            LblTituloDonut.ForeColor = Color.FromArgb(CByte(210), CByte(218), CByte(235))
            LblTituloDonut.Location = New Point(0, 0)
            LblTituloDonut.Name = "LblTituloDonut"
            LblTituloDonut.Size = New Size(122, 15)
            LblTituloDonut.TabIndex = 0
            LblTituloDonut.Text = "Gastos por Categoría"
            ' 
            ' PnlDonut
            ' 
            PnlDonut.BackColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            PnlDonut.Location = New Point(0, 22)
            PnlDonut.Name = "PnlDonut"
            PnlDonut.Size = New Size(249, 407)
            PnlDonut.TabIndex = 1
            ' 
            ' LblTituloBarras
            ' 
            LblTituloBarras.AutoSize = True
            LblTituloBarras.Font = New Font("Segoe UI", 8.5F, FontStyle.Bold)
            LblTituloBarras.ForeColor = Color.FromArgb(CByte(210), CByte(218), CByte(235))
            LblTituloBarras.Location = New Point(0, 0)
            LblTituloBarras.Name = "LblTituloBarras"
            LblTituloBarras.Size = New Size(118, 15)
            LblTituloBarras.TabIndex = 2
            LblTituloBarras.Text = "BALANCE MENSUAL"
            ' 
            ' PnlBarras
            ' 
            PnlBarras.BackColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            PnlBarras.Location = New Point(0, 0)
            PnlBarras.Name = "PnlBarras"
            PnlBarras.Size = New Size(200, 100)
            PnlBarras.TabIndex = 3
            ' 
            ' FormDashboard
            ' 
            BackColor = Color.FromArgb(CByte(22), CByte(27), CByte(44))
            ClientSize = New Size(938, 593)
            Controls.Add(LblTitulo)
            Controls.Add(PnlTarjetas)
            Controls.Add(PnlIzquierdo)
            Controls.Add(PnlDerecho)
            Name = "FormDashboard"
            Padding = New Padding(20, 16, 20, 16)
            PnlTarjetas.ResumeLayout(False)
            PnlCardIngresos.ResumeLayout(False)
            PnlCardIngresos.PerformLayout()
            PnlCardEgresos.ResumeLayout(False)
            PnlCardEgresos.PerformLayout()
            PnlCardBalance.ResumeLayout(False)
            PnlCardBalance.PerformLayout()
            PnlIzquierdo.ResumeLayout(False)
            PnlNavMes.ResumeLayout(False)
            TableLayoutPanel1.ResumeLayout(False)
            PnlDerecho.ResumeLayout(False)
            PnlDerecho.PerformLayout()
            ResumeLayout(False)
            PerformLayout()

        End Sub

        Friend WithEvents pnlAccentIng As Panel
        Friend WithEvents pnlAccentEgr As Panel
        Friend WithEvents pnlAccentBal As Panel
        Friend WithEvents TableLayoutPanel1 As TableLayoutPanel

    End Class

End Namespace
