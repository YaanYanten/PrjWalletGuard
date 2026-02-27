Imports System.Drawing
Imports System.Windows.Forms

Namespace WalletGuard.Presentacion
    Partial Public Class FormRegistroMovimiento

        ' ── Header ────────────────────────────────────────────────────────────
        Private PnlHeader As Panel
        Friend PnlIndicador As Panel
        Friend LblTipoActual As Label
        Private LblFechaHeader As Label
        Friend LblFechaSeleccionada As Label

        ' ── Selección de tipo ─────────────────────────────────────────────────
        Private PnlTipo As Panel
        Friend WithEvents RbIngreso As RadioButton
        Friend WithEvents RbEgreso As RadioButton

        ' ── Campos del formulario ─────────────────────────────────────────────
        Private PnlCuerpo As Panel
        Private LblFecha As Label
        Friend DtpFecha As DateTimePicker
        Private LblMonto As Label
        Friend WithEvents NumMonto As NumericUpDown
        Private LblCategoria As Label
        Friend WithEvents CmbCategoria As ComboBox
        Private LblDesc As Label
        Friend WithEvents TxtDescripcion As TextBox
        Friend LblError As Label

        ' ── Botones ───────────────────────────────────────────────────────────
        Private PnlBotones As Panel
        Friend WithEvents BtnGuardar As Button
        Friend WithEvents BtnCancelar As Button

        Private _disposed As Boolean = False

        Protected Overrides Sub Dispose(disposing As Boolean)
            If Not _disposed Then
                _disposed = True
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            PnlHeader = New Panel()
            LblTipoActual = New Label()
            LblFechaHeader = New Label()
            LblFechaSeleccionada = New Label()
            PnlIndicador = New Panel()
            PnlTipo = New Panel()
            RbIngreso = New RadioButton()
            RbEgreso = New RadioButton()
            PnlCuerpo = New Panel()
            LblFecha = New Label()
            DtpFecha = New DateTimePicker()
            LblMonto = New Label()
            NumMonto = New NumericUpDown()
            LblCategoria = New Label()
            CmbCategoria = New ComboBox()
            LblDesc = New Label()
            TxtDescripcion = New TextBox()
            LblError = New Label()
            PnlBotones = New Panel()
            BtnGuardar = New Button()
            BtnCancelar = New Button()
            pnlSep = New Panel()
            TableLayoutPanel1 = New TableLayoutPanel()
            PnlHeader.SuspendLayout()
            PnlTipo.SuspendLayout()
            PnlCuerpo.SuspendLayout()
            CType(NumMonto, System.ComponentModel.ISupportInitialize).BeginInit()
            PnlBotones.SuspendLayout()
            TableLayoutPanel1.SuspendLayout()
            SuspendLayout()
            ' 
            ' PnlHeader
            ' 
            PnlHeader.BackColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            PnlHeader.Controls.Add(LblTipoActual)
            PnlHeader.Controls.Add(LblFechaHeader)
            PnlHeader.Controls.Add(LblFechaSeleccionada)
            PnlHeader.Controls.Add(PnlIndicador)
            PnlHeader.Dock = DockStyle.Fill
            PnlHeader.Location = New Point(3, 3)
            PnlHeader.Name = "PnlHeader"
            PnlHeader.Padding = New Padding(14, 10, 10, 10)
            PnlHeader.Size = New Size(439, 80)
            PnlHeader.TabIndex = 3
            ' 
            ' LblTipoActual
            ' 
            LblTipoActual.AutoSize = True
            LblTipoActual.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
            LblTipoActual.ForeColor = Color.FromArgb(CByte(80), CByte(220), CByte(130))
            LblTipoActual.Location = New Point(18, 12)
            LblTipoActual.Name = "LblTipoActual"
            LblTipoActual.Size = New Size(170, 21)
            LblTipoActual.TabIndex = 0
            LblTipoActual.Text = "📥  Registrar Ingreso"
            ' 
            ' LblFechaHeader
            ' 
            LblFechaHeader.AutoSize = True
            LblFechaHeader.Font = New Font("Segoe UI", 8F)
            LblFechaHeader.ForeColor = Color.FromArgb(CByte(120), CByte(135), CByte(165))
            LblFechaHeader.Location = New Point(18, 44)
            LblFechaHeader.Name = "LblFechaHeader"
            LblFechaHeader.Size = New Size(109, 13)
            LblFechaHeader.TabIndex = 1
            LblFechaHeader.Text = "Fecha seleccionada:"
            ' 
            ' LblFechaSeleccionada
            ' 
            LblFechaSeleccionada.AutoSize = True
            LblFechaSeleccionada.Font = New Font("Segoe UI", 8.5F)
            LblFechaSeleccionada.ForeColor = Color.FromArgb(CByte(200), CByte(210), CByte(230))
            LblFechaSeleccionada.Location = New Point(118, 44)
            LblFechaSeleccionada.Name = "LblFechaSeleccionada"
            LblFechaSeleccionada.Size = New Size(0, 15)
            LblFechaSeleccionada.TabIndex = 2
            ' 
            ' PnlIndicador
            ' 
            PnlIndicador.BackColor = Color.FromArgb(CByte(60), CByte(180), CByte(90))
            PnlIndicador.Location = New Point(0, 0)
            PnlIndicador.Name = "PnlIndicador"
            PnlIndicador.Size = New Size(5, 80)
            PnlIndicador.TabIndex = 3
            ' 
            ' PnlTipo
            ' 
            PnlTipo.BackColor = Color.FromArgb(CByte(22), CByte(27), CByte(44))
            PnlTipo.Controls.Add(RbIngreso)
            PnlTipo.Controls.Add(RbEgreso)
            PnlTipo.Dock = DockStyle.Fill
            PnlTipo.Location = New Point(3, 89)
            PnlTipo.Name = "PnlTipo"
            PnlTipo.Padding = New Padding(16, 10, 10, 4)
            PnlTipo.Size = New Size(439, 62)
            PnlTipo.TabIndex = 2
            ' 
            ' RbIngreso
            ' 
            RbIngreso.AutoSize = True
            RbIngreso.Cursor = Cursors.Hand
            RbIngreso.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
            RbIngreso.ForeColor = Color.FromArgb(CByte(80), CByte(220), CByte(130))
            RbIngreso.Location = New Point(16, 12)
            RbIngreso.Name = "RbIngreso"
            RbIngreso.Size = New Size(77, 23)
            RbIngreso.TabIndex = 0
            RbIngreso.Text = "Ingreso"
            ' 
            ' RbEgreso
            ' 
            RbEgreso.AutoSize = True
            RbEgreso.Cursor = Cursors.Hand
            RbEgreso.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
            RbEgreso.ForeColor = Color.FromArgb(CByte(220), CByte(90), CByte(90))
            RbEgreso.Location = New Point(130, 12)
            RbEgreso.Name = "RbEgreso"
            RbEgreso.Size = New Size(72, 23)
            RbEgreso.TabIndex = 1
            RbEgreso.Text = "Egreso"
            ' 
            ' PnlCuerpo
            ' 
            PnlCuerpo.BackColor = Color.FromArgb(CByte(22), CByte(27), CByte(44))
            PnlCuerpo.Controls.Add(LblFecha)
            PnlCuerpo.Controls.Add(DtpFecha)
            PnlCuerpo.Controls.Add(LblMonto)
            PnlCuerpo.Controls.Add(NumMonto)
            PnlCuerpo.Controls.Add(LblCategoria)
            PnlCuerpo.Controls.Add(CmbCategoria)
            PnlCuerpo.Controls.Add(LblDesc)
            PnlCuerpo.Controls.Add(TxtDescripcion)
            PnlCuerpo.Controls.Add(LblError)
            PnlCuerpo.Dock = DockStyle.Fill
            PnlCuerpo.Location = New Point(3, 157)
            PnlCuerpo.Name = "PnlCuerpo"
            PnlCuerpo.Padding = New Padding(16, 12, 16, 8)
            PnlCuerpo.Size = New Size(439, 309)
            PnlCuerpo.TabIndex = 0
            ' 
            ' LblFecha
            ' 
            LblFecha.AutoSize = True
            LblFecha.Font = New Font("Segoe UI", 8.5F)
            LblFecha.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblFecha.Location = New Point(15, 0)
            LblFecha.Name = "LblFecha"
            LblFecha.Size = New Size(38, 15)
            LblFecha.TabIndex = 0
            LblFecha.Text = "Fecha"
            ' 
            ' DtpFecha
            ' 
            DtpFecha.Font = New Font("Segoe UI", 9.5F)
            DtpFecha.Location = New Point(15, 18)
            DtpFecha.Name = "DtpFecha"
            DtpFecha.Size = New Size(200, 24)
            DtpFecha.TabIndex = 1
            ' 
            ' LblMonto
            ' 
            LblMonto.AutoSize = True
            LblMonto.Font = New Font("Segoe UI", 8.5F)
            LblMonto.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblMonto.Location = New Point(15, 58)
            LblMonto.Name = "LblMonto"
            LblMonto.Size = New Size(43, 15)
            LblMonto.TabIndex = 2
            LblMonto.Text = "Monto"
            ' 
            ' NumMonto
            ' 
            NumMonto.BackColor = Color.FromArgb(CByte(35), CByte(42), CByte(65))
            NumMonto.BorderStyle = BorderStyle.FixedSingle
            NumMonto.DecimalPlaces = 2
            NumMonto.Font = New Font("Segoe UI", 14F, FontStyle.Bold)
            NumMonto.ForeColor = Color.FromArgb(CByte(80), CByte(220), CByte(130))
            NumMonto.Location = New Point(15, 76)
            NumMonto.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
            NumMonto.Name = "NumMonto"
            NumMonto.Size = New Size(340, 32)
            NumMonto.TabIndex = 3
            NumMonto.ThousandsSeparator = True
            ' 
            ' LblCategoria
            ' 
            LblCategoria.AutoSize = True
            LblCategoria.Font = New Font("Segoe UI", 8.5F)
            LblCategoria.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblCategoria.Location = New Point(15, 128)
            LblCategoria.Name = "LblCategoria"
            LblCategoria.Size = New Size(58, 15)
            LblCategoria.TabIndex = 4
            LblCategoria.Text = "Categoría"
            ' 
            ' CmbCategoria
            ' 
            CmbCategoria.BackColor = Color.FromArgb(CByte(35), CByte(42), CByte(65))
            CmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList
            CmbCategoria.FlatStyle = FlatStyle.Flat
            CmbCategoria.Font = New Font("Segoe UI", 9.5F)
            CmbCategoria.ForeColor = Color.FromArgb(CByte(200), CByte(210), CByte(230))
            CmbCategoria.Location = New Point(15, 146)
            CmbCategoria.Name = "CmbCategoria"
            CmbCategoria.Size = New Size(340, 25)
            CmbCategoria.TabIndex = 5
            ' 
            ' LblDesc
            ' 
            LblDesc.AutoSize = True
            LblDesc.Font = New Font("Segoe UI", 8.5F)
            LblDesc.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblDesc.Location = New Point(15, 188)
            LblDesc.Name = "LblDesc"
            LblDesc.Size = New Size(129, 15)
            LblDesc.TabIndex = 6
            LblDesc.Text = "Descripción  (opcional)"
            ' 
            ' TxtDescripcion
            ' 
            TxtDescripcion.BackColor = Color.FromArgb(CByte(35), CByte(42), CByte(65))
            TxtDescripcion.BorderStyle = BorderStyle.FixedSingle
            TxtDescripcion.Font = New Font("Segoe UI", 9.5F)
            TxtDescripcion.ForeColor = Color.FromArgb(CByte(200), CByte(210), CByte(230))
            TxtDescripcion.Location = New Point(15, 206)
            TxtDescripcion.Multiline = True
            TxtDescripcion.Name = "TxtDescripcion"
            TxtDescripcion.Size = New Size(340, 56)
            TxtDescripcion.TabIndex = 7
            ' 
            ' LblError
            ' 
            LblError.AutoSize = True
            LblError.Font = New Font("Segoe UI", 8.5F)
            LblError.ForeColor = Color.FromArgb(CByte(220), CByte(120), CByte(60))
            LblError.Location = New Point(0, 270)
            LblError.Name = "LblError"
            LblError.Size = New Size(0, 15)
            LblError.TabIndex = 8
            LblError.Visible = False
            ' 
            ' PnlBotones
            ' 
            PnlBotones.BackColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            PnlBotones.Controls.Add(BtnGuardar)
            PnlBotones.Controls.Add(BtnCancelar)
            PnlBotones.Dock = DockStyle.Fill
            PnlBotones.Location = New Point(3, 472)
            PnlBotones.Name = "PnlBotones"
            PnlBotones.Padding = New Padding(16, 10, 16, 10)
            PnlBotones.Size = New Size(439, 67)
            PnlBotones.TabIndex = 4
            ' 
            ' BtnGuardar
            ' 
            BtnGuardar.BackColor = Color.FromArgb(CByte(40), CByte(140), CByte(70))
            BtnGuardar.Cursor = Cursors.Hand
            BtnGuardar.FlatAppearance.BorderSize = 0
            BtnGuardar.FlatStyle = FlatStyle.Flat
            BtnGuardar.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
            BtnGuardar.ForeColor = Color.White
            BtnGuardar.Location = New Point(16, 10)
            BtnGuardar.Name = "BtnGuardar"
            BtnGuardar.Size = New Size(160, 38)
            BtnGuardar.TabIndex = 0
            BtnGuardar.Text = "💾  Guardar"
            BtnGuardar.UseVisualStyleBackColor = False
            ' 
            ' BtnCancelar
            ' 
            BtnCancelar.BackColor = Color.FromArgb(CByte(40), CByte(48), CByte(72))
            BtnCancelar.Cursor = Cursors.Hand
            BtnCancelar.FlatAppearance.BorderSize = 0
            BtnCancelar.FlatStyle = FlatStyle.Flat
            BtnCancelar.Font = New Font("Segoe UI", 9.5F)
            BtnCancelar.ForeColor = Color.FromArgb(CByte(160), CByte(170), CByte(190))
            BtnCancelar.Location = New Point(188, 10)
            BtnCancelar.Name = "BtnCancelar"
            BtnCancelar.Size = New Size(130, 38)
            BtnCancelar.TabIndex = 1
            BtnCancelar.Text = "Cancelar"
            BtnCancelar.UseVisualStyleBackColor = False
            ' 
            ' pnlSep
            ' 
            pnlSep.BackColor = Color.FromArgb(CByte(40), CByte(48), CByte(72))
            pnlSep.Dock = DockStyle.Top
            pnlSep.Location = New Point(0, 0)
            pnlSep.Name = "pnlSep"
            pnlSep.Size = New Size(401, 1)
            pnlSep.TabIndex = 1
            ' 
            ' TableLayoutPanel1
            ' 
            TableLayoutPanel1.ColumnCount = 1
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
            TableLayoutPanel1.Controls.Add(PnlHeader, 0, 0)
            TableLayoutPanel1.Controls.Add(PnlCuerpo, 0, 2)
            TableLayoutPanel1.Controls.Add(PnlBotones, 0, 3)
            TableLayoutPanel1.Controls.Add(PnlTipo, 0, 1)
            TableLayoutPanel1.Dock = DockStyle.Fill
            TableLayoutPanel1.Location = New Point(0, 1)
            TableLayoutPanel1.Name = "TableLayoutPanel1"
            TableLayoutPanel1.RowCount = 4
            TableLayoutPanel1.RowStyles.Add(New RowStyle())
            TableLayoutPanel1.RowStyles.Add(New RowStyle())
            TableLayoutPanel1.RowStyles.Add(New RowStyle())
            TableLayoutPanel1.RowStyles.Add(New RowStyle())
            TableLayoutPanel1.Size = New Size(401, 542)
            TableLayoutPanel1.TabIndex = 9
            ' 
            ' FormRegistroMovimiento
            ' 
            BackColor = Color.FromArgb(CByte(22), CByte(27), CByte(44))
            ClientSize = New Size(401, 543)
            ControlBox = False
            Controls.Add(TableLayoutPanel1)
            Controls.Add(pnlSep)
            FormBorderStyle = FormBorderStyle.None
            MaximizeBox = False
            MinimizeBox = False
            Name = "FormRegistroMovimiento"
            StartPosition = FormStartPosition.CenterParent
            Text = "Nuevo Movimiento"
            PnlHeader.ResumeLayout(False)
            PnlHeader.PerformLayout()
            PnlTipo.ResumeLayout(False)
            PnlTipo.PerformLayout()
            PnlCuerpo.ResumeLayout(False)
            PnlCuerpo.PerformLayout()
            CType(NumMonto, System.ComponentModel.ISupportInitialize).EndInit()
            PnlBotones.ResumeLayout(False)
            TableLayoutPanel1.ResumeLayout(False)
            ResumeLayout(False)

        End Sub

        Friend WithEvents pnlSep As Panel
        Friend WithEvents TableLayoutPanel1 As TableLayoutPanel

    End Class

End Namespace
