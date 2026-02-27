Imports System.Drawing
Imports System.Windows.Forms

Namespace WalletGuard.Presentacion

    ''' <summary>
    ''' DESIGNER de ReporteFlujoCaja.
    ''' Sin lógica. Sin bloques With. Asignaciones línea por línea.
    ''' </summary>
    Partial Public Class ReporteFlujoCaja

        Private LblTitulo    As Label
        Private LblSubtitulo As Label

        ' ── Filtros ───────────────────────────────────────────────────────────
        Private PnlFiltros   As Panel
        Private LblDesde     As Label
        Friend  DtpDesde     As DateTimePicker
        Private LblHasta     As Label
        Friend  DtpHasta     As DateTimePicker
        Friend WithEvents BtnFiltrar As Button

        ' ── Resumen de totales ────────────────────────────────────────────────
        Friend LblResumen    As Label

        ' ── Gráfico ───────────────────────────────────────────────────────────
        Friend WithEvents PnlGrafico As Panel

        ' ── Grid ─────────────────────────────────────────────────────────────
        Friend GridFlujo     As DataGridView

        Private _disposed As Boolean = False

        Protected Overrides Sub Dispose(disposing As Boolean)
            If Not _disposed Then
                If disposing Then
                    GridFlujo?.Dispose()
                End If
                _disposed = True
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
            LblTitulo = New Label()
            LblSubtitulo = New Label()
            PnlFiltros = New Panel()
            LblDesde = New Label()
            DtpDesde = New DateTimePicker()
            LblHasta = New Label()
            DtpHasta = New DateTimePicker()
            BtnFiltrar = New Button()
            LblResumen = New Label()
            PnlGrafico = New Panel()
            GridFlujo = New DataGridView()
            colPeriodo = New DataGridViewTextBoxColumn()
            colIngresos = New DataGridViewTextBoxColumn()
            colEgresos = New DataGridViewTextBoxColumn()
            colBalance = New DataGridViewTextBoxColumn()
            colAcumulado = New DataGridViewTextBoxColumn()
            PnlFiltros.SuspendLayout()
            CType(GridFlujo, System.ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' LblTitulo
            ' 
            LblTitulo.AutoSize = True
            LblTitulo.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
            LblTitulo.ForeColor = Color.FromArgb(CByte(230), CByte(235), CByte(245))
            LblTitulo.Location = New Point(0, 0)
            LblTitulo.Name = "LblTitulo"
            LblTitulo.Size = New Size(341, 32)
            LblTitulo.TabIndex = 0
            LblTitulo.Text = "📊  Reporte de Flujo de Caja"
            ' 
            ' LblSubtitulo
            ' 
            LblSubtitulo.AutoSize = True
            LblSubtitulo.Font = New Font("Segoe UI", 9.5F)
            LblSubtitulo.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblSubtitulo.Location = New Point(2, 42)
            LblSubtitulo.Name = "LblSubtitulo"
            LblSubtitulo.Size = New Size(328, 17)
            LblSubtitulo.TabIndex = 1
            LblSubtitulo.Text = "Ingresos y egresos mes a mes con balance acumulado"
            ' 
            ' PnlFiltros
            ' 
            PnlFiltros.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            PnlFiltros.BackColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            PnlFiltros.Controls.Add(LblDesde)
            PnlFiltros.Controls.Add(DtpDesde)
            PnlFiltros.Controls.Add(LblHasta)
            PnlFiltros.Controls.Add(DtpHasta)
            PnlFiltros.Controls.Add(BtnFiltrar)
            PnlFiltros.Location = New Point(0, 68)
            PnlFiltros.Name = "PnlFiltros"
            PnlFiltros.Padding = New Padding(10, 8, 10, 8)
            PnlFiltros.Size = New Size(656, 52)
            PnlFiltros.TabIndex = 2
            ' 
            ' LblDesde
            ' 
            LblDesde.AutoSize = True
            LblDesde.Font = New Font("Segoe UI", 9.0F)
            LblDesde.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblDesde.Location = New Point(10, 16)
            LblDesde.Name = "LblDesde"
            LblDesde.Size = New Size(42, 15)
            LblDesde.TabIndex = 0
            LblDesde.Text = "Desde:"
            ' 
            ' DtpDesde
            ' 
            DtpDesde.Font = New Font("Segoe UI", 9.5F)
            DtpDesde.Format = DateTimePickerFormat.Short
            DtpDesde.Location = New Point(60, 12)
            DtpDesde.Name = "DtpDesde"
            DtpDesde.Size = New Size(145, 24)
            DtpDesde.TabIndex = 1
            ' 
            ' LblHasta
            ' 
            LblHasta.AutoSize = True
            LblHasta.Font = New Font("Segoe UI", 9.0F)
            LblHasta.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblHasta.Location = New Point(218, 16)
            LblHasta.Name = "LblHasta"
            LblHasta.Size = New Size(40, 15)
            LblHasta.TabIndex = 2
            LblHasta.Text = "Hasta:"
            ' 
            ' DtpHasta
            ' 
            DtpHasta.Font = New Font("Segoe UI", 9.5F)
            DtpHasta.Format = DateTimePickerFormat.Short
            DtpHasta.Location = New Point(268, 12)
            DtpHasta.Name = "DtpHasta"
            DtpHasta.Size = New Size(145, 24)
            DtpHasta.TabIndex = 3
            ' 
            ' BtnFiltrar
            ' 
            BtnFiltrar.BackColor = Color.FromArgb(CByte(60), CByte(120), CByte(200))
            BtnFiltrar.Cursor = Cursors.Hand
            BtnFiltrar.FlatAppearance.BorderSize = 0
            BtnFiltrar.FlatStyle = FlatStyle.Flat
            BtnFiltrar.Font = New Font("Segoe UI", 9.5F)
            BtnFiltrar.ForeColor = Color.White
            BtnFiltrar.Location = New Point(426, 11)
            BtnFiltrar.Name = "BtnFiltrar"
            BtnFiltrar.Size = New Size(110, 30)
            BtnFiltrar.TabIndex = 4
            BtnFiltrar.Text = "▶  Aplicar"
            BtnFiltrar.UseVisualStyleBackColor = False
            ' 
            ' LblResumen
            ' 
            LblResumen.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            LblResumen.AutoSize = True
            LblResumen.Font = New Font("Segoe UI", 8.5F, FontStyle.Bold)
            LblResumen.ForeColor = Color.FromArgb(CByte(80), CByte(210), CByte(120))
            LblResumen.Location = New Point(0, 130)
            LblResumen.Name = "LblResumen"
            LblResumen.Size = New Size(0, 15)
            LblResumen.TabIndex = 3
            ' 
            ' PnlGrafico
            ' 
            PnlGrafico.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            PnlGrafico.BackColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            PnlGrafico.Location = New Point(0, 150)
            PnlGrafico.Name = "PnlGrafico"
            PnlGrafico.Size = New Size(656, 220)
            PnlGrafico.TabIndex = 4
            ' 
            ' GridFlujo
            ' 
            GridFlujo.AllowUserToAddRows = False
            GridFlujo.AllowUserToDeleteRows = False
            DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(32), CByte(40), CByte(60))
            GridFlujo.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
            GridFlujo.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            GridFlujo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            GridFlujo.BackgroundColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            GridFlujo.BorderStyle = BorderStyle.None
            GridFlujo.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(20), CByte(25), CByte(40))
            DataGridViewCellStyle2.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            DataGridViewCellStyle2.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
            DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
            DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
            GridFlujo.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
            GridFlujo.ColumnHeadersHeight = 38
            GridFlujo.Columns.AddRange(New DataGridViewColumn() {colPeriodo, colIngresos, colEgresos, colBalance, colAcumulado})
            DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            DataGridViewCellStyle3.Font = New Font("Segoe UI", 9.5F)
            DataGridViewCellStyle3.ForeColor = Color.FromArgb(CByte(210), CByte(220), CByte(240))
            DataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(CByte(60), CByte(120), CByte(200))
            DataGridViewCellStyle3.SelectionForeColor = Color.White
            DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
            GridFlujo.DefaultCellStyle = DataGridViewCellStyle3
            GridFlujo.EnableHeadersVisualStyles = False
            GridFlujo.Font = New Font("Segoe UI", 9.5F)
            GridFlujo.GridColor = Color.FromArgb(CByte(40), CByte(50), CByte(75))
            GridFlujo.Location = New Point(0, 382)
            GridFlujo.MultiSelect = False
            GridFlujo.Name = "GridFlujo"
            GridFlujo.ReadOnly = True
            GridFlujo.RowHeadersVisible = False
            GridFlujo.RowTemplate.Height = 40
            GridFlujo.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            GridFlujo.Size = New Size(696, 507)
            GridFlujo.TabIndex = 5
            ' 
            ' colPeriodo
            ' 
            colPeriodo.FillWeight = 22.0F
            colPeriodo.HeaderText = "Período"
            colPeriodo.Name = "colPeriodo"
            colPeriodo.ReadOnly = True
            ' 
            ' colIngresos
            ' 
            colIngresos.FillWeight = 20.0F
            colIngresos.HeaderText = "Ingresos"
            colIngresos.Name = "colIngresos"
            colIngresos.ReadOnly = True
            ' 
            ' colEgresos
            ' 
            colEgresos.FillWeight = 20.0F
            colEgresos.HeaderText = "Egresos"
            colEgresos.Name = "colEgresos"
            colEgresos.ReadOnly = True
            ' 
            ' colBalance
            ' 
            colBalance.FillWeight = 20.0F
            colBalance.HeaderText = "Balance"
            colBalance.Name = "colBalance"
            colBalance.ReadOnly = True
            ' 
            ' colAcumulado
            ' 
            colAcumulado.FillWeight = 18.0F
            colAcumulado.HeaderText = "Acumulado"
            colAcumulado.Name = "colAcumulado"
            colAcumulado.ReadOnly = True
            ' 
            ' ReporteFlujoCaja
            ' 
            BackColor = Color.FromArgb(CByte(22), CByte(27), CByte(44))
            ClientSize = New Size(740, 618)
            Controls.Add(LblTitulo)
            Controls.Add(LblSubtitulo)
            Controls.Add(PnlFiltros)
            Controls.Add(LblResumen)
            Controls.Add(PnlGrafico)
            Controls.Add(GridFlujo)
            FormBorderStyle = FormBorderStyle.None
            Name = "ReporteFlujoCaja"
            Padding = New Padding(20, 16, 20, 16)
            PnlFiltros.ResumeLayout(False)
            PnlFiltros.PerformLayout()
            CType(GridFlujo, System.ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)
            PerformLayout()

        End Sub

        Friend WithEvents colPeriodo As DataGridViewTextBoxColumn
        Friend WithEvents colIngresos As DataGridViewTextBoxColumn
        Friend WithEvents colEgresos As DataGridViewTextBoxColumn
        Friend WithEvents colBalance As DataGridViewTextBoxColumn
        Friend WithEvents colAcumulado As DataGridViewTextBoxColumn

    End Class

End Namespace
