Imports System.Drawing
Imports System.Windows.Forms

Namespace WalletGuard.Presentacion

    ''' <summary>
    ''' DESIGNER de FormGastosRecurrentes.
    ''' Solo declaración de controles e InitializeComponent.
    ''' Sin lógica, sin bloques With.
    ''' </summary>
    Partial Public Class FormGastosRecurrentes

        ' ── Encabezado ────────────────────────────────────────────────────────
        Private LblTitulo    As Label
        Private LblSubtitulo As Label

        ' ── Barra de botones ──────────────────────────────────────────────────
        Private PnlToolbar    As Panel
        Friend WithEvents BtnNuevo As Button
        Friend WithEvents BtnEditar As Button
        Friend WithEvents BtnDesactivar As Button

        ' ── Grid de recurrentes ───────────────────────────────────────────────
        Friend GridRecurrentes As DataGridView

        ' ── Panel del formulario de edición ───────────────────────────────────
        Private PnlFormEdicion  As Panel
        Private LblFormTitulo   As Label

        Private LblNombre       As Label
        Friend  TxtNombre       As TextBox

        Private LblMonto        As Label
        Friend  NumMonto        As NumericUpDown

        Private LblDiaCorte     As Label
        Friend  NumDiaCorte     As NumericUpDown

        Private LblFechaInicio  As Label
        Friend  DtpFechaInicio  As DateTimePicker

        Friend WithEvents ChkSinFin As CheckBox

        Private LblFechaFin     As Label
        Friend  DtpFechaFin     As DateTimePicker

        Friend  ChkActivo       As CheckBox

        Friend WithEvents BtnGuardar As Button
        Friend WithEvents BtnCancelar As Button

        Private _disposed As Boolean = False

        Protected Overrides Sub Dispose(disposing As Boolean)
            If Not _disposed Then
                If disposing Then
                    GridRecurrentes?.Dispose()
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
            PnlToolbar = New Panel()
            BtnNuevo = New Button()
            BtnEditar = New Button()
            BtnDesactivar = New Button()
            GridRecurrentes = New DataGridView()
            colId = New DataGridViewTextBoxColumn()
            colNombre = New DataGridViewTextBoxColumn()
            colMonto = New DataGridViewTextBoxColumn()
            colDia = New DataGridViewTextBoxColumn()
            colInicio = New DataGridViewTextBoxColumn()
            colFin = New DataGridViewTextBoxColumn()
            colUltimo = New DataGridViewTextBoxColumn()
            colEstado = New DataGridViewTextBoxColumn()
            PnlFormEdicion = New Panel()
            LblFormTitulo = New Label()
            LblNombre = New Label()
            TxtNombre = New TextBox()
            LblMonto = New Label()
            NumMonto = New NumericUpDown()
            LblDiaCorte = New Label()
            NumDiaCorte = New NumericUpDown()
            LblFechaInicio = New Label()
            DtpFechaInicio = New DateTimePicker()
            ChkSinFin = New CheckBox()
            LblFechaFin = New Label()
            DtpFechaFin = New DateTimePicker()
            ChkActivo = New CheckBox()
            BtnGuardar = New Button()
            BtnCancelar = New Button()
            PnlToolbar.SuspendLayout()
            CType(GridRecurrentes, System.ComponentModel.ISupportInitialize).BeginInit()
            PnlFormEdicion.SuspendLayout()
            CType(NumMonto, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(NumDiaCorte, System.ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' LblTitulo
            ' 
            LblTitulo.AutoSize = True
            LblTitulo.Font = New Font("Segoe UI", 18F, FontStyle.Bold)
            LblTitulo.ForeColor = Color.FromArgb(CByte(230), CByte(235), CByte(245))
            LblTitulo.Location = New Point(0, 0)
            LblTitulo.Name = "LblTitulo"
            LblTitulo.Size = New Size(268, 32)
            LblTitulo.TabIndex = 0
            LblTitulo.Text = "↺  Gastos Recurrentes"
            ' 
            ' LblSubtitulo
            ' 
            LblSubtitulo.AutoSize = True
            LblSubtitulo.Font = New Font("Segoe UI", 9.5F)
            LblSubtitulo.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblSubtitulo.Location = New Point(2, 42)
            LblSubtitulo.Name = "LblSubtitulo"
            LblSubtitulo.Size = New Size(254, 17)
            LblSubtitulo.TabIndex = 1
            LblSubtitulo.Text = "Gestión de gastos automáticos mensuales"
            ' 
            ' PnlToolbar
            ' 
            PnlToolbar.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            PnlToolbar.BackColor = Color.Transparent
            PnlToolbar.Controls.Add(BtnNuevo)
            PnlToolbar.Controls.Add(BtnEditar)
            PnlToolbar.Controls.Add(BtnDesactivar)
            PnlToolbar.Location = New Point(0, 72)
            PnlToolbar.Name = "PnlToolbar"
            PnlToolbar.Size = New Size(1463, 44)
            PnlToolbar.TabIndex = 2
            ' 
            ' BtnNuevo
            ' 
            BtnNuevo.BackColor = Color.FromArgb(CByte(60), CByte(120), CByte(200))
            BtnNuevo.Cursor = Cursors.Hand
            BtnNuevo.FlatAppearance.BorderSize = 0
            BtnNuevo.FlatStyle = FlatStyle.Flat
            BtnNuevo.Font = New Font("Segoe UI", 9.5F)
            BtnNuevo.ForeColor = Color.White
            BtnNuevo.Location = New Point(0, 4)
            BtnNuevo.Name = "BtnNuevo"
            BtnNuevo.Size = New Size(110, 36)
            BtnNuevo.TabIndex = 0
            BtnNuevo.Text = "+  Nuevo"
            BtnNuevo.UseVisualStyleBackColor = False
            ' 
            ' BtnEditar
            ' 
            BtnEditar.BackColor = Color.FromArgb(CByte(45), CByte(55), CByte(80))
            BtnEditar.Cursor = Cursors.Hand
            BtnEditar.FlatAppearance.BorderSize = 0
            BtnEditar.FlatStyle = FlatStyle.Flat
            BtnEditar.Font = New Font("Segoe UI", 9.5F)
            BtnEditar.ForeColor = Color.White
            BtnEditar.Location = New Point(118, 4)
            BtnEditar.Name = "BtnEditar"
            BtnEditar.Size = New Size(110, 36)
            BtnEditar.TabIndex = 1
            BtnEditar.Text = "✏  Editar"
            BtnEditar.UseVisualStyleBackColor = False
            ' 
            ' BtnDesactivar
            ' 
            BtnDesactivar.BackColor = Color.FromArgb(CByte(160), CByte(50), CByte(50))
            BtnDesactivar.Cursor = Cursors.Hand
            BtnDesactivar.FlatAppearance.BorderSize = 0
            BtnDesactivar.FlatStyle = FlatStyle.Flat
            BtnDesactivar.Font = New Font("Segoe UI", 9.5F)
            BtnDesactivar.ForeColor = Color.White
            BtnDesactivar.Location = New Point(236, 4)
            BtnDesactivar.Name = "BtnDesactivar"
            BtnDesactivar.Size = New Size(130, 36)
            BtnDesactivar.TabIndex = 2
            BtnDesactivar.Text = "⛔  Desactivar"
            BtnDesactivar.UseVisualStyleBackColor = False
            ' 
            ' GridRecurrentes
            ' 
            GridRecurrentes.AllowUserToAddRows = False
            GridRecurrentes.AllowUserToDeleteRows = False
            DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(38), CByte(47), CByte(70))
            GridRecurrentes.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
            GridRecurrentes.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            GridRecurrentes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            GridRecurrentes.BackgroundColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            GridRecurrentes.BorderStyle = BorderStyle.None
            GridRecurrentes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(20), CByte(25), CByte(40))
            DataGridViewCellStyle2.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
            DataGridViewCellStyle2.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
            DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
            DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
            GridRecurrentes.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
            GridRecurrentes.ColumnHeadersHeight = 40
            GridRecurrentes.Columns.AddRange(New DataGridViewColumn() {colId, colNombre, colMonto, colDia, colInicio, colFin, colUltimo, colEstado})
            DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = Color.FromArgb(CByte(32), CByte(40), CByte(60))
            DataGridViewCellStyle3.Font = New Font("Segoe UI", 9.5F)
            DataGridViewCellStyle3.ForeColor = Color.FromArgb(CByte(230), CByte(235), CByte(245))
            DataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(CByte(60), CByte(120), CByte(200))
            DataGridViewCellStyle3.SelectionForeColor = Color.White
            DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
            GridRecurrentes.DefaultCellStyle = DataGridViewCellStyle3
            GridRecurrentes.EnableHeadersVisualStyles = False
            GridRecurrentes.Font = New Font("Segoe UI", 9.5F)
            GridRecurrentes.GridColor = Color.FromArgb(CByte(45), CByte(55), CByte(80))
            GridRecurrentes.Location = New Point(0, 124)
            GridRecurrentes.MultiSelect = False
            GridRecurrentes.Name = "GridRecurrentes"
            GridRecurrentes.ReadOnly = True
            GridRecurrentes.RowHeadersVisible = False
            GridRecurrentes.RowTemplate.Height = 44
            GridRecurrentes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            GridRecurrentes.Size = New Size(883, 375)
            GridRecurrentes.TabIndex = 3
            ' 
            ' colId
            ' 
            colId.FillWeight = 5F
            colId.HeaderText = "ID"
            colId.Name = "colId"
            colId.ReadOnly = True
            colId.Visible = False
            ' 
            ' colNombre
            ' 
            colNombre.FillWeight = 25F
            colNombre.HeaderText = "Nombre"
            colNombre.Name = "colNombre"
            colNombre.ReadOnly = True
            ' 
            ' colMonto
            ' 
            colMonto.FillWeight = 12F
            colMonto.HeaderText = "Monto"
            colMonto.Name = "colMonto"
            colMonto.ReadOnly = True
            ' 
            ' colDia
            ' 
            colDia.FillWeight = 8F
            colDia.HeaderText = "Día Corte"
            colDia.Name = "colDia"
            colDia.ReadOnly = True
            ' 
            ' colInicio
            ' 
            colInicio.FillWeight = 12F
            colInicio.HeaderText = "Inicio"
            colInicio.Name = "colInicio"
            colInicio.ReadOnly = True
            ' 
            ' colFin
            ' 
            colFin.FillWeight = 14F
            colFin.HeaderText = "Fecha Fin"
            colFin.Name = "colFin"
            colFin.ReadOnly = True
            ' 
            ' colUltimo
            ' 
            colUltimo.FillWeight = 14F
            colUltimo.HeaderText = "Último Proc."
            colUltimo.Name = "colUltimo"
            colUltimo.ReadOnly = True
            ' 
            ' colEstado
            ' 
            colEstado.FillWeight = 10F
            colEstado.HeaderText = "Estado"
            colEstado.Name = "colEstado"
            colEstado.ReadOnly = True
            ' 
            ' PnlFormEdicion
            ' 
            PnlFormEdicion.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
            PnlFormEdicion.BackColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            PnlFormEdicion.Controls.Add(LblFormTitulo)
            PnlFormEdicion.Controls.Add(LblNombre)
            PnlFormEdicion.Controls.Add(TxtNombre)
            PnlFormEdicion.Controls.Add(LblMonto)
            PnlFormEdicion.Controls.Add(NumMonto)
            PnlFormEdicion.Controls.Add(LblDiaCorte)
            PnlFormEdicion.Controls.Add(NumDiaCorte)
            PnlFormEdicion.Controls.Add(LblFechaInicio)
            PnlFormEdicion.Controls.Add(DtpFechaInicio)
            PnlFormEdicion.Controls.Add(ChkSinFin)
            PnlFormEdicion.Controls.Add(LblFechaFin)
            PnlFormEdicion.Controls.Add(DtpFechaFin)
            PnlFormEdicion.Controls.Add(ChkActivo)
            PnlFormEdicion.Controls.Add(BtnGuardar)
            PnlFormEdicion.Controls.Add(BtnCancelar)
            PnlFormEdicion.Location = New Point(1227, 124)
            PnlFormEdicion.Name = "PnlFormEdicion"
            PnlFormEdicion.Padding = New Padding(16)
            PnlFormEdicion.Size = New Size(300, 659)
            PnlFormEdicion.TabIndex = 4
            ' 
            ' LblFormTitulo
            ' 
            LblFormTitulo.AutoSize = True
            LblFormTitulo.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
            LblFormTitulo.ForeColor = Color.FromArgb(CByte(230), CByte(235), CByte(245))
            LblFormTitulo.Location = New Point(0, 0)
            LblFormTitulo.Name = "LblFormTitulo"
            LblFormTitulo.Size = New Size(149, 19)
            LblFormTitulo.TabIndex = 0
            LblFormTitulo.Text = "Datos del Recurrente"
            ' 
            ' LblNombre
            ' 
            LblNombre.AutoSize = True
            LblNombre.Font = New Font("Segoe UI", 9F)
            LblNombre.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblNombre.Location = New Point(0, 32)
            LblNombre.Name = "LblNombre"
            LblNombre.Size = New Size(54, 15)
            LblNombre.TabIndex = 1
            LblNombre.Text = "Nombre:"
            ' 
            ' TxtNombre
            ' 
            TxtNombre.BackColor = Color.FromArgb(CByte(40), CByte(48), CByte(72))
            TxtNombre.BorderStyle = BorderStyle.FixedSingle
            TxtNombre.Enabled = False
            TxtNombre.Font = New Font("Segoe UI", 9.5F)
            TxtNombre.ForeColor = Color.FromArgb(CByte(230), CByte(235), CByte(245))
            TxtNombre.Location = New Point(0, 52)
            TxtNombre.Name = "TxtNombre"
            TxtNombre.Size = New Size(264, 24)
            TxtNombre.TabIndex = 2
            ' 
            ' LblMonto
            ' 
            LblMonto.AutoSize = True
            LblMonto.Font = New Font("Segoe UI", 9F)
            LblMonto.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblMonto.Location = New Point(0, 92)
            LblMonto.Name = "LblMonto"
            LblMonto.Size = New Size(46, 15)
            LblMonto.TabIndex = 3
            LblMonto.Text = "Monto:"
            ' 
            ' NumMonto
            ' 
            NumMonto.BackColor = Color.FromArgb(CByte(40), CByte(48), CByte(72))
            NumMonto.BorderStyle = BorderStyle.FixedSingle
            NumMonto.DecimalPlaces = 2
            NumMonto.Enabled = False
            NumMonto.Font = New Font("Segoe UI", 9.5F)
            NumMonto.ForeColor = Color.FromArgb(CByte(230), CByte(235), CByte(245))
            NumMonto.Location = New Point(0, 112)
            NumMonto.Maximum = New Decimal(New Integer() {9999999, 0, 0, 0})
            NumMonto.Name = "NumMonto"
            NumMonto.Size = New Size(264, 24)
            NumMonto.TabIndex = 4
            ' 
            ' LblDiaCorte
            ' 
            LblDiaCorte.AutoSize = True
            LblDiaCorte.Font = New Font("Segoe UI", 9F)
            LblDiaCorte.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblDiaCorte.Location = New Point(0, 152)
            LblDiaCorte.Name = "LblDiaCorte"
            LblDiaCorte.Size = New Size(107, 15)
            LblDiaCorte.TabIndex = 5
            LblDiaCorte.Text = "Día de corte (1-31):"
            ' 
            ' NumDiaCorte
            ' 
            NumDiaCorte.BackColor = Color.FromArgb(CByte(40), CByte(48), CByte(72))
            NumDiaCorte.BorderStyle = BorderStyle.FixedSingle
            NumDiaCorte.Enabled = False
            NumDiaCorte.Font = New Font("Segoe UI", 9.5F)
            NumDiaCorte.ForeColor = Color.FromArgb(CByte(230), CByte(235), CByte(245))
            NumDiaCorte.Location = New Point(0, 172)
            NumDiaCorte.Maximum = New Decimal(New Integer() {31, 0, 0, 0})
            NumDiaCorte.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            NumDiaCorte.Name = "NumDiaCorte"
            NumDiaCorte.Size = New Size(100, 24)
            NumDiaCorte.TabIndex = 6
            NumDiaCorte.Value = New Decimal(New Integer() {1, 0, 0, 0})
            ' 
            ' LblFechaInicio
            ' 
            LblFechaInicio.AutoSize = True
            LblFechaInicio.Font = New Font("Segoe UI", 9F)
            LblFechaInicio.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblFechaInicio.Location = New Point(0, 212)
            LblFechaInicio.Name = "LblFechaInicio"
            LblFechaInicio.Size = New Size(73, 15)
            LblFechaInicio.TabIndex = 7
            LblFechaInicio.Text = "Fecha inicio:"
            ' 
            ' DtpFechaInicio
            ' 
            DtpFechaInicio.Enabled = False
            DtpFechaInicio.Font = New Font("Segoe UI", 9.5F)
            DtpFechaInicio.Format = DateTimePickerFormat.Short
            DtpFechaInicio.Location = New Point(0, 232)
            DtpFechaInicio.Name = "DtpFechaInicio"
            DtpFechaInicio.Size = New Size(180, 24)
            DtpFechaInicio.TabIndex = 8
            ' 
            ' ChkSinFin
            ' 
            ChkSinFin.AutoSize = True
            ChkSinFin.Checked = True
            ChkSinFin.CheckState = CheckState.Checked
            ChkSinFin.Enabled = False
            ChkSinFin.Font = New Font("Segoe UI", 9F)
            ChkSinFin.ForeColor = Color.FromArgb(CByte(200), CByte(210), CByte(230))
            ChkSinFin.Location = New Point(0, 272)
            ChkSinFin.Name = "ChkSinFin"
            ChkSinFin.Size = New Size(107, 19)
            ChkSinFin.TabIndex = 9
            ChkSinFin.Text = "Sin fecha de fin"
            ' 
            ' LblFechaFin
            ' 
            LblFechaFin.AutoSize = True
            LblFechaFin.Font = New Font("Segoe UI", 9F)
            LblFechaFin.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblFechaFin.Location = New Point(0, 296)
            LblFechaFin.Name = "LblFechaFin"
            LblFechaFin.Size = New Size(58, 15)
            LblFechaFin.TabIndex = 10
            LblFechaFin.Text = "Fecha fin:"
            ' 
            ' DtpFechaFin
            ' 
            DtpFechaFin.Enabled = False
            DtpFechaFin.Font = New Font("Segoe UI", 9.5F)
            DtpFechaFin.Format = DateTimePickerFormat.Short
            DtpFechaFin.Location = New Point(0, 316)
            DtpFechaFin.Name = "DtpFechaFin"
            DtpFechaFin.Size = New Size(180, 24)
            DtpFechaFin.TabIndex = 11
            ' 
            ' ChkActivo
            ' 
            ChkActivo.AutoSize = True
            ChkActivo.Checked = True
            ChkActivo.CheckState = CheckState.Checked
            ChkActivo.Enabled = False
            ChkActivo.Font = New Font("Segoe UI", 9F)
            ChkActivo.ForeColor = Color.FromArgb(CByte(200), CByte(210), CByte(230))
            ChkActivo.Location = New Point(0, 356)
            ChkActivo.Name = "ChkActivo"
            ChkActivo.Size = New Size(60, 19)
            ChkActivo.TabIndex = 12
            ChkActivo.Text = "Activo"
            ' 
            ' BtnGuardar
            ' 
            BtnGuardar.BackColor = Color.FromArgb(CByte(40), CByte(160), CByte(80))
            BtnGuardar.Cursor = Cursors.Hand
            BtnGuardar.Enabled = False
            BtnGuardar.FlatAppearance.BorderSize = 0
            BtnGuardar.FlatStyle = FlatStyle.Flat
            BtnGuardar.Font = New Font("Segoe UI", 9.5F)
            BtnGuardar.ForeColor = Color.White
            BtnGuardar.Location = New Point(0, 390)
            BtnGuardar.Name = "BtnGuardar"
            BtnGuardar.Size = New Size(126, 36)
            BtnGuardar.TabIndex = 13
            BtnGuardar.Text = "💾  Guardar"
            BtnGuardar.UseVisualStyleBackColor = False
            ' 
            ' BtnCancelar
            ' 
            BtnCancelar.BackColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
            BtnCancelar.Cursor = Cursors.Hand
            BtnCancelar.Enabled = False
            BtnCancelar.FlatAppearance.BorderSize = 0
            BtnCancelar.FlatStyle = FlatStyle.Flat
            BtnCancelar.Font = New Font("Segoe UI", 9.5F)
            BtnCancelar.ForeColor = Color.White
            BtnCancelar.Location = New Point(134, 390)
            BtnCancelar.Name = "BtnCancelar"
            BtnCancelar.Size = New Size(126, 36)
            BtnCancelar.TabIndex = 14
            BtnCancelar.Text = "✖  Cancelar"
            BtnCancelar.UseVisualStyleBackColor = False
            ' 
            ' FormGastosRecurrentes
            ' 
            BackColor = Color.FromArgb(CByte(30), CByte(35), CByte(55))
            ClientSize = New Size(887, 500)
            Controls.Add(LblTitulo)
            Controls.Add(LblSubtitulo)
            Controls.Add(PnlToolbar)
            Controls.Add(GridRecurrentes)
            Controls.Add(PnlFormEdicion)
            Name = "FormGastosRecurrentes"
            Padding = New Padding(24)
            PnlToolbar.ResumeLayout(False)
            CType(GridRecurrentes, System.ComponentModel.ISupportInitialize).EndInit()
            PnlFormEdicion.ResumeLayout(False)
            PnlFormEdicion.PerformLayout()
            CType(NumMonto, System.ComponentModel.ISupportInitialize).EndInit()
            CType(NumDiaCorte, System.ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)
            PerformLayout()

        End Sub

        Friend WithEvents colId As DataGridViewTextBoxColumn
        Friend WithEvents colNombre As DataGridViewTextBoxColumn
        Friend WithEvents colMonto As DataGridViewTextBoxColumn
        Friend WithEvents colDia As DataGridViewTextBoxColumn
        Friend WithEvents colInicio As DataGridViewTextBoxColumn
        Friend WithEvents colFin As DataGridViewTextBoxColumn
        Friend WithEvents colUltimo As DataGridViewTextBoxColumn
        Friend WithEvents colEstado As DataGridViewTextBoxColumn

    End Class

End Namespace
