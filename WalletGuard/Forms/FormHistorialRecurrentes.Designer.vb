Imports System.Drawing
Imports System.Windows.Forms

Namespace WalletGuard.Presentacion

    ''' <summary>
    ''' DESIGNER de FormHistorialRecurrentes.
    ''' Solo declaración de controles e InitializeComponent.
    ''' Sin lógica, sin bloques With.
    ''' </summary>
    Partial Public Class FormHistorialRecurrentes

        Private LblTitulo    As Label
        Private LblSubtitulo As Label
        Private PnlToolbar   As Panel
        Friend WithEvents BtnRefrescar As Button

        Friend GridHistorial As DataGridView

        Friend LblTotalRegistros As Label

        Private _disposed As Boolean = False

        Protected Overrides Sub Dispose(disposing As Boolean)
            If Not _disposed Then
                If disposing Then
                    GridHistorial?.Dispose()
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
            BtnRefrescar = New Button()
            GridHistorial = New DataGridView()
            colId = New DataGridViewTextBoxColumn()
            colFecha = New DataGridViewTextBoxColumn()
            colMonto = New DataGridViewTextBoxColumn()
            colIdRec = New DataGridViewTextBoxColumn()
            colDesc = New DataGridViewTextBoxColumn()
            LblTotalRegistros = New Label()
            PnlToolbar.SuspendLayout()
            CType(GridHistorial, System.ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' LblTitulo
            ' 
            LblTitulo.AutoSize = True
            LblTitulo.Font = New Font("Segoe UI", 18F, FontStyle.Bold)
            LblTitulo.ForeColor = Color.FromArgb(CByte(230), CByte(235), CByte(245))
            LblTitulo.Location = New Point(0, 0)
            LblTitulo.Name = "LblTitulo"
            LblTitulo.Size = New Size(432, 32)
            LblTitulo.TabIndex = 0
            LblTitulo.Text = "📋  Historial de Egresos Recurrentes"
            ' 
            ' LblSubtitulo
            ' 
            LblSubtitulo.AutoSize = True
            LblSubtitulo.Font = New Font("Segoe UI", 9.5F)
            LblSubtitulo.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblSubtitulo.Location = New Point(2, 42)
            LblSubtitulo.Name = "LblSubtitulo"
            LblSubtitulo.Size = New Size(313, 17)
            LblSubtitulo.TabIndex = 1
            LblSubtitulo.Text = "Egresos generados automáticamente por el sistema"
            ' 
            ' PnlToolbar
            ' 
            PnlToolbar.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            PnlToolbar.BackColor = Color.Transparent
            PnlToolbar.Controls.Add(BtnRefrescar)
            PnlToolbar.Location = New Point(0, 72)
            PnlToolbar.Name = "PnlToolbar"
            PnlToolbar.Size = New Size(1601, 44)
            PnlToolbar.TabIndex = 2
            ' 
            ' BtnRefrescar
            ' 
            BtnRefrescar.BackColor = Color.FromArgb(CByte(45), CByte(55), CByte(80))
            BtnRefrescar.Cursor = Cursors.Hand
            BtnRefrescar.FlatAppearance.BorderSize = 0
            BtnRefrescar.FlatStyle = FlatStyle.Flat
            BtnRefrescar.Font = New Font("Segoe UI", 9.5F)
            BtnRefrescar.ForeColor = Color.White
            BtnRefrescar.Location = New Point(0, 4)
            BtnRefrescar.Name = "BtnRefrescar"
            BtnRefrescar.Size = New Size(120, 36)
            BtnRefrescar.TabIndex = 0
            BtnRefrescar.Text = "↻  Refrescar"
            BtnRefrescar.UseVisualStyleBackColor = False
            ' 
            ' GridHistorial
            ' 
            GridHistorial.AllowUserToAddRows = False
            GridHistorial.AllowUserToDeleteRows = False
            DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(38), CByte(47), CByte(70))
            GridHistorial.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
            GridHistorial.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            GridHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            GridHistorial.BackgroundColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            GridHistorial.BorderStyle = BorderStyle.None
            GridHistorial.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(20), CByte(25), CByte(40))
            DataGridViewCellStyle2.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
            DataGridViewCellStyle2.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
            DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
            DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
            GridHistorial.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
            GridHistorial.ColumnHeadersHeight = 40
            GridHistorial.Columns.AddRange(New DataGridViewColumn() {colId, colFecha, colMonto, colIdRec, colDesc})
            DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = Color.FromArgb(CByte(32), CByte(40), CByte(60))
            DataGridViewCellStyle3.Font = New Font("Segoe UI", 9.5F)
            DataGridViewCellStyle3.ForeColor = Color.FromArgb(CByte(230), CByte(235), CByte(245))
            DataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(CByte(60), CByte(120), CByte(200))
            DataGridViewCellStyle3.SelectionForeColor = Color.White
            DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
            GridHistorial.DefaultCellStyle = DataGridViewCellStyle3
            GridHistorial.EnableHeadersVisualStyles = False
            GridHistorial.Font = New Font("Segoe UI", 9.5F)
            GridHistorial.GridColor = Color.FromArgb(CByte(45), CByte(55), CByte(80))
            GridHistorial.Location = New Point(0, 124)
            GridHistorial.MultiSelect = False
            GridHistorial.Name = "GridHistorial"
            GridHistorial.ReadOnly = True
            GridHistorial.RowHeadersVisible = False
            GridHistorial.RowTemplate.Height = 44
            GridHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            GridHistorial.Size = New Size(1014, 421)
            GridHistorial.TabIndex = 3
            ' 
            ' colId
            ' 
            colId.FillWeight = 5F
            colId.HeaderText = "ID"
            colId.Name = "colId"
            colId.ReadOnly = True
            colId.Visible = False
            ' 
            ' colFecha
            ' 
            colFecha.FillWeight = 14F
            colFecha.HeaderText = "Fecha"
            colFecha.Name = "colFecha"
            colFecha.ReadOnly = True
            ' 
            ' colMonto
            ' 
            colMonto.FillWeight = 14F
            colMonto.HeaderText = "Monto"
            colMonto.Name = "colMonto"
            colMonto.ReadOnly = True
            ' 
            ' colIdRec
            ' 
            colIdRec.FillWeight = 12F
            colIdRec.HeaderText = "ID Recurrente"
            colIdRec.Name = "colIdRec"
            colIdRec.ReadOnly = True
            ' 
            ' colDesc
            ' 
            colDesc.FillWeight = 55F
            colDesc.HeaderText = "Descripción"
            colDesc.Name = "colDesc"
            colDesc.ReadOnly = True
            ' 
            ' LblTotalRegistros
            ' 
            LblTotalRegistros.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            LblTotalRegistros.AutoSize = True
            LblTotalRegistros.Font = New Font("Segoe UI", 9F)
            LblTotalRegistros.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblTotalRegistros.Location = New Point(0, 867)
            LblTotalRegistros.Name = "LblTotalRegistros"
            LblTotalRegistros.Size = New Size(67, 15)
            LblTotalRegistros.TabIndex = 4
            LblTotalRegistros.Text = "Registros: 0"
            ' 
            ' FormHistorialRecurrentes
            ' 
            BackColor = Color.FromArgb(CByte(30), CByte(35), CByte(55))
            ClientSize = New Size(1025, 553)
            ControlBox = False
            Controls.Add(LblTitulo)
            Controls.Add(LblSubtitulo)
            Controls.Add(PnlToolbar)
            Controls.Add(GridHistorial)
            Controls.Add(LblTotalRegistros)
            FormBorderStyle = FormBorderStyle.None
            Name = "FormHistorialRecurrentes"
            Padding = New Padding(24)
            PnlToolbar.ResumeLayout(False)
            CType(GridHistorial, System.ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)
            PerformLayout()

        End Sub

        Friend WithEvents colId As DataGridViewTextBoxColumn
        Friend WithEvents colFecha As DataGridViewTextBoxColumn
        Friend WithEvents colMonto As DataGridViewTextBoxColumn
        Friend WithEvents colIdRec As DataGridViewTextBoxColumn
        Friend WithEvents colDesc As DataGridViewTextBoxColumn

    End Class

End Namespace
