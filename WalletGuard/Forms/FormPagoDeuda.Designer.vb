Imports System.Drawing
Imports System.Windows.Forms

Namespace WalletGuard.Presentacion

    ''' <summary>
    ''' DESIGNER de FormPagoDeuda.
    ''' Sin lógica. Sin bloques With. Asignaciones línea por línea.
    ''' </summary>
    Partial Public Class FormPagoDeuda

        Private PnlHeader       As Panel
        Private PnlIndicador    As Panel
        Friend  LblNombreDeuda  As Label
        Friend  LblSaldoActual  As Label

        Private PnlCuerpo       As Panel
        Private LblFechaPago    As Label
        Friend  DtpFechaPago    As DateTimePicker
        Private LblMontoPago    As Label
        Friend WithEvents NumMontoPago As NumericUpDown
        Friend  LblSaldoNuevo   As Label
        Private LblDescPago     As Label
        Friend  TxtDescPago     As TextBox
        Friend  LblErrorPago    As Label

        Private PnlBotones      As Panel
        Friend WithEvents BtnGuardarPago As Button
        Friend WithEvents BtnCancelarPago As Button

        Private _disposed As Boolean = False

        Protected Overrides Sub Dispose(disposing As Boolean)
            If Not _disposed Then
                _disposed = True
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            PnlHeader = New Panel()
            LblNombreDeuda = New Label()
            LblSaldoActual = New Label()
            PnlIndicador = New Panel()
            PnlCuerpo = New Panel()
            LblFechaPago = New Label()
            DtpFechaPago = New DateTimePicker()
            LblMontoPago = New Label()
            NumMontoPago = New NumericUpDown()
            LblSaldoNuevo = New Label()
            LblDescPago = New Label()
            TxtDescPago = New TextBox()
            LblErrorPago = New Label()
            PnlBotones = New Panel()
            BtnGuardarPago = New Button()
            BtnCancelarPago = New Button()
            PnlHeader.SuspendLayout()
            PnlCuerpo.SuspendLayout()
            CType(NumMontoPago, System.ComponentModel.ISupportInitialize).BeginInit()
            PnlBotones.SuspendLayout()
            SuspendLayout()
            ' 
            ' PnlHeader
            ' 
            PnlHeader.BackColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            PnlHeader.Controls.Add(LblNombreDeuda)
            PnlHeader.Controls.Add(LblSaldoActual)
            PnlHeader.Controls.Add(PnlIndicador)
            PnlHeader.Dock = DockStyle.Top
            PnlHeader.Location = New Point(0, 0)
            PnlHeader.Name = "PnlHeader"
            PnlHeader.Size = New Size(419, 72)
            PnlHeader.TabIndex = 1
            ' 
            ' LblNombreDeuda
            ' 
            LblNombreDeuda.AutoSize = True
            LblNombreDeuda.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
            LblNombreDeuda.ForeColor = Color.FromArgb(CByte(220), CByte(228), CByte(245))
            LblNombreDeuda.Location = New Point(16, 10)
            LblNombreDeuda.Name = "LblNombreDeuda"
            LblNombreDeuda.Size = New Size(0, 21)
            LblNombreDeuda.TabIndex = 0
            ' 
            ' LblSaldoActual
            ' 
            LblSaldoActual.AutoSize = True
            LblSaldoActual.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            LblSaldoActual.ForeColor = Color.FromArgb(CByte(220), CByte(90), CByte(90))
            LblSaldoActual.Location = New Point(16, 40)
            LblSaldoActual.Name = "LblSaldoActual"
            LblSaldoActual.Size = New Size(0, 15)
            LblSaldoActual.TabIndex = 1
            ' 
            ' PnlIndicador
            ' 
            PnlIndicador.BackColor = Color.FromArgb(CByte(40), CByte(140), CByte(70))
            PnlIndicador.Location = New Point(0, 0)
            PnlIndicador.Name = "PnlIndicador"
            PnlIndicador.Size = New Size(5, 72)
            PnlIndicador.TabIndex = 2
            ' 
            ' PnlCuerpo
            ' 
            PnlCuerpo.BackColor = Color.FromArgb(CByte(22), CByte(27), CByte(44))
            PnlCuerpo.Controls.Add(LblFechaPago)
            PnlCuerpo.Controls.Add(DtpFechaPago)
            PnlCuerpo.Controls.Add(LblMontoPago)
            PnlCuerpo.Controls.Add(NumMontoPago)
            PnlCuerpo.Controls.Add(LblSaldoNuevo)
            PnlCuerpo.Controls.Add(LblDescPago)
            PnlCuerpo.Controls.Add(TxtDescPago)
            PnlCuerpo.Controls.Add(LblErrorPago)
            PnlCuerpo.Dock = DockStyle.Fill
            PnlCuerpo.Location = New Point(0, 72)
            PnlCuerpo.Name = "PnlCuerpo"
            PnlCuerpo.Padding = New Padding(16, 12, 16, 8)
            PnlCuerpo.Size = New Size(419, 287)
            PnlCuerpo.TabIndex = 0
            ' 
            ' LblFechaPago
            ' 
            LblFechaPago.AutoSize = True
            LblFechaPago.Font = New Font("Segoe UI", 8.5F)
            LblFechaPago.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblFechaPago.Location = New Point(0, 0)
            LblFechaPago.Name = "LblFechaPago"
            LblFechaPago.Size = New Size(87, 15)
            LblFechaPago.TabIndex = 0
            LblFechaPago.Text = "Fecha del pago"
            ' 
            ' DtpFechaPago
            ' 
            DtpFechaPago.Font = New Font("Segoe UI", 9.5F)
            DtpFechaPago.Format = DateTimePickerFormat.Short
            DtpFechaPago.Location = New Point(0, 18)
            DtpFechaPago.Name = "DtpFechaPago"
            DtpFechaPago.Size = New Size(200, 24)
            DtpFechaPago.TabIndex = 1
            ' 
            ' LblMontoPago
            ' 
            LblMontoPago.AutoSize = True
            LblMontoPago.Font = New Font("Segoe UI", 8.5F)
            LblMontoPago.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblMontoPago.Location = New Point(0, 58)
            LblMontoPago.Name = "LblMontoPago"
            LblMontoPago.Size = New Size(92, 15)
            LblMontoPago.TabIndex = 2
            LblMontoPago.Text = "Monto del pago"
            ' 
            ' NumMontoPago
            ' 
            NumMontoPago.BackColor = Color.FromArgb(CByte(35), CByte(42), CByte(65))
            NumMontoPago.BorderStyle = BorderStyle.FixedSingle
            NumMontoPago.DecimalPlaces = 2
            NumMontoPago.Font = New Font("Segoe UI", 14.0F, FontStyle.Bold)
            NumMontoPago.ForeColor = Color.FromArgb(CByte(80), CByte(220), CByte(130))
            NumMontoPago.Location = New Point(0, 76)
            NumMontoPago.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
            NumMontoPago.Name = "NumMontoPago"
            NumMontoPago.Size = New Size(320, 32)
            NumMontoPago.TabIndex = 3
            NumMontoPago.ThousandsSeparator = True
            ' 
            ' LblSaldoNuevo
            ' 
            LblSaldoNuevo.AutoSize = True
            LblSaldoNuevo.Font = New Font("Segoe UI", 8.5F)
            LblSaldoNuevo.ForeColor = Color.FromArgb(CByte(240), CByte(180), CByte(60))
            LblSaldoNuevo.Location = New Point(0, 122)
            LblSaldoNuevo.Name = "LblSaldoNuevo"
            LblSaldoNuevo.Size = New Size(0, 15)
            LblSaldoNuevo.TabIndex = 4
            LblSaldoNuevo.Visible = False
            ' 
            ' LblDescPago
            ' 
            LblDescPago.AutoSize = True
            LblDescPago.Font = New Font("Segoe UI", 8.5F)
            LblDescPago.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblDescPago.Location = New Point(0, 146)
            LblDescPago.Name = "LblDescPago"
            LblDescPago.Size = New Size(129, 15)
            LblDescPago.TabIndex = 5
            LblDescPago.Text = "El desglose capital/interés se calculará automáticamente."
            ' 
            ' TxtDescPago
            ' 
            TxtDescPago.BackColor = Color.FromArgb(CByte(35), CByte(42), CByte(65))
            TxtDescPago.BorderStyle = BorderStyle.FixedSingle
            TxtDescPago.Font = New Font("Segoe UI", 9.5F)
            TxtDescPago.ForeColor = Color.FromArgb(CByte(200), CByte(210), CByte(230))
            TxtDescPago.Location = New Point(0, 164)
            TxtDescPago.Multiline = True
            TxtDescPago.Name = "TxtDescPago"
            TxtDescPago.Size = New Size(320, 48)
            TxtDescPago.TabIndex = 6
            ' 
            ' LblErrorPago
            ' 
            LblErrorPago.AutoSize = True
            LblErrorPago.Font = New Font("Segoe UI", 8.5F)
            LblErrorPago.ForeColor = Color.FromArgb(CByte(220), CByte(120), CByte(60))
            LblErrorPago.Location = New Point(0, 220)
            LblErrorPago.Name = "LblErrorPago"
            LblErrorPago.Size = New Size(0, 15)
            LblErrorPago.TabIndex = 7
            LblErrorPago.Visible = False
            ' 
            ' PnlBotones
            ' 
            PnlBotones.BackColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            PnlBotones.Controls.Add(BtnGuardarPago)
            PnlBotones.Controls.Add(BtnCancelarPago)
            PnlBotones.Dock = DockStyle.Bottom
            PnlBotones.Location = New Point(0, 359)
            PnlBotones.Name = "PnlBotones"
            PnlBotones.Size = New Size(419, 58)
            PnlBotones.TabIndex = 2
            ' 
            ' BtnGuardarPago
            ' 
            BtnGuardarPago.BackColor = Color.FromArgb(CByte(40), CByte(140), CByte(70))
            BtnGuardarPago.Cursor = Cursors.Hand
            BtnGuardarPago.FlatAppearance.BorderSize = 0
            BtnGuardarPago.FlatStyle = FlatStyle.Flat
            BtnGuardarPago.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
            BtnGuardarPago.ForeColor = Color.White
            BtnGuardarPago.Location = New Point(16, 10)
            BtnGuardarPago.Name = "BtnGuardarPago"
            BtnGuardarPago.Size = New Size(170, 38)
            BtnGuardarPago.TabIndex = 0
            BtnGuardarPago.Text = "💾  Registrar Pago"
            BtnGuardarPago.UseVisualStyleBackColor = False
            ' 
            ' BtnCancelarPago
            ' 
            BtnCancelarPago.BackColor = Color.FromArgb(CByte(40), CByte(48), CByte(72))
            BtnCancelarPago.Cursor = Cursors.Hand
            BtnCancelarPago.FlatAppearance.BorderSize = 0
            BtnCancelarPago.FlatStyle = FlatStyle.Flat
            BtnCancelarPago.Font = New Font("Segoe UI", 9.5F)
            BtnCancelarPago.ForeColor = Color.FromArgb(CByte(160), CByte(170), CByte(190))
            BtnCancelarPago.Location = New Point(198, 10)
            BtnCancelarPago.Name = "BtnCancelarPago"
            BtnCancelarPago.Size = New Size(120, 38)
            BtnCancelarPago.TabIndex = 1
            BtnCancelarPago.Text = "Cancelar"
            BtnCancelarPago.UseVisualStyleBackColor = False
            ' 
            ' FormPagoDeuda
            ' 
            BackColor = Color.FromArgb(CByte(22), CByte(27), CByte(44))
            ClientSize = New Size(419, 417)
            Controls.Add(PnlCuerpo)
            Controls.Add(PnlHeader)
            Controls.Add(PnlBotones)
            FormBorderStyle = FormBorderStyle.None
            MaximizeBox = False
            MinimizeBox = False
            Name = "FormPagoDeuda"
            StartPosition = FormStartPosition.CenterParent
            Text = "Registrar Pago"
            PnlHeader.ResumeLayout(False)
            PnlHeader.PerformLayout()
            PnlCuerpo.ResumeLayout(False)
            PnlCuerpo.PerformLayout()
            CType(NumMontoPago, System.ComponentModel.ISupportInitialize).EndInit()
            PnlBotones.ResumeLayout(False)
            ResumeLayout(False)

        End Sub

    End Class

End Namespace
