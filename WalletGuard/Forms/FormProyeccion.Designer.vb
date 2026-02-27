Imports System.Drawing
Imports System.Windows.Forms

Namespace WalletGuard.Presentacion

    ''' <summary>
    ''' DESIGNER de FormProyeccion.
    ''' Sin lógica. Sin bloques With. Asignaciones línea por línea.
    ''' </summary>
    Partial Public Class FormProyeccion

        Private LblTitulo              As Label
        Private LblSubtitulo           As Label
        Private PnlToolbar             As Panel
        Friend WithEvents BtnRefrescar As Button
        Friend  LblResumenProyeccion   As Label

        ' Alerta de déficit
        Friend PnlAlerta    As Panel
        Private PnlAlertAcc As Panel
        Friend  LblAlerta   As Label

        ' Gráfico
        Friend WithEvents PnlGrafico As Panel

        ' Grid
        Friend GridProyeccion As DataGridView

        Private _disposed As Boolean = False

        Protected Overrides Sub Dispose(disposing As Boolean)
            If Not _disposed Then
                If disposing Then
                    GridProyeccion?.Dispose()
                End If
                _disposed = True
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
            LblTitulo = New Label()
            LblSubtitulo = New Label()
            PnlToolbar = New Panel()
            BtnRefrescar = New Button()
            LblResumenProyeccion = New Label()
            PnlAlerta = New Panel()
            PnlAlertAcc = New Panel()
            LblAlerta = New Label()
            PnlGrafico = New Panel()
            GridProyeccion = New DataGridView()
            colTipo = New DataGridViewTextBoxColumn()
            colPeriodo = New DataGridViewTextBoxColumn()
            colIng = New DataGridViewTextBoxColumn()
            colEgr = New DataGridViewTextBoxColumn()
            colRec = New DataGridViewTextBoxColumn()
            colBal = New DataGridViewTextBoxColumn()
            PnlToolbar.SuspendLayout()
            PnlAlerta.SuspendLayout()
            CType(GridProyeccion, System.ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' LblTitulo
            ' 
            LblTitulo.AutoSize = True
            LblTitulo.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
            LblTitulo.ForeColor = Color.FromArgb(CByte(230), CByte(235), CByte(245))
            LblTitulo.Location = New Point(0, 0)
            LblTitulo.Name = "LblTitulo"
            LblTitulo.Size = New Size(313, 32)
            LblTitulo.TabIndex = 0
            LblTitulo.Text = "🔮  Proyección Financiera"
            ' 
            ' LblSubtitulo
            ' 
            LblSubtitulo.AutoSize = True
            LblSubtitulo.Font = New Font("Segoe UI", 9.5F)
            LblSubtitulo.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblSubtitulo.Location = New Point(2, 42)
            LblSubtitulo.Name = "LblSubtitulo"
            LblSubtitulo.Size = New Size(374, 17)
            LblSubtitulo.TabIndex = 1
            LblSubtitulo.Text = "3 meses reales + 3 meses proyectados basados en tu historial"
            ' 
            ' PnlToolbar
            ' 
            PnlToolbar.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            PnlToolbar.BackColor = Color.Transparent
            PnlToolbar.Controls.Add(BtnRefrescar)
            PnlToolbar.Location = New Point(0, 68)
            PnlToolbar.Name = "PnlToolbar"
            PnlToolbar.Size = New Size(877, 44)
            PnlToolbar.TabIndex = 2
            ' 
            ' BtnRefrescar
            ' 
            BtnRefrescar.BackColor = Color.FromArgb(CByte(60), CByte(120), CByte(200))
            BtnRefrescar.Cursor = Cursors.Hand
            BtnRefrescar.FlatAppearance.BorderSize = 0
            BtnRefrescar.FlatStyle = FlatStyle.Flat
            BtnRefrescar.Font = New Font("Segoe UI", 9.5F)
            BtnRefrescar.ForeColor = Color.White
            BtnRefrescar.Location = New Point(0, 5)
            BtnRefrescar.Name = "BtnRefrescar"
            BtnRefrescar.Size = New Size(130, 34)
            BtnRefrescar.TabIndex = 0
            BtnRefrescar.Text = "↻  Actualizar"
            BtnRefrescar.UseVisualStyleBackColor = False
            ' 
            ' LblResumenProyeccion
            ' 
            LblResumenProyeccion.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            LblResumenProyeccion.AutoSize = True
            LblResumenProyeccion.Font = New Font("Segoe UI", 8.5F, FontStyle.Bold)
            LblResumenProyeccion.ForeColor = Color.FromArgb(CByte(80), CByte(210), CByte(120))
            LblResumenProyeccion.Location = New Point(0, 120)
            LblResumenProyeccion.Name = "LblResumenProyeccion"
            LblResumenProyeccion.Size = New Size(0, 15)
            LblResumenProyeccion.TabIndex = 3
            ' 
            ' PnlAlerta
            ' 
            PnlAlerta.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            PnlAlerta.BackColor = Color.FromArgb(CByte(50), CByte(25), CByte(20))
            PnlAlerta.Controls.Add(PnlAlertAcc)
            PnlAlerta.Controls.Add(LblAlerta)
            PnlAlerta.Location = New Point(0, 140)
            PnlAlerta.Name = "PnlAlerta"
            PnlAlerta.Size = New Size(877, 34)
            PnlAlerta.TabIndex = 4
            PnlAlerta.Visible = False
            ' 
            ' PnlAlertAcc
            ' 
            PnlAlertAcc.BackColor = Color.FromArgb(CByte(200), CByte(60), CByte(60))
            PnlAlertAcc.Location = New Point(0, 0)
            PnlAlertAcc.Name = "PnlAlertAcc"
            PnlAlertAcc.Size = New Size(4, 34)
            PnlAlertAcc.TabIndex = 0
            ' 
            ' LblAlerta
            ' 
            LblAlerta.AutoSize = True
            LblAlerta.Font = New Font("Segoe UI", 8.5F)
            LblAlerta.ForeColor = Color.FromArgb(CByte(220), CByte(130), CByte(130))
            LblAlerta.Location = New Point(12, 9)
            LblAlerta.Name = "LblAlerta"
            LblAlerta.Size = New Size(0, 15)
            LblAlerta.TabIndex = 1
            ' 
            ' PnlGrafico
            ' 
            PnlGrafico.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            PnlGrafico.BackColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            PnlGrafico.Location = New Point(0, 182)
            PnlGrafico.Name = "PnlGrafico"
            PnlGrafico.Size = New Size(877, 210)
            PnlGrafico.TabIndex = 5
            ' 
            ' GridProyeccion
            ' 
            GridProyeccion.AllowUserToAddRows = False
            GridProyeccion.AllowUserToDeleteRows = False
            GridProyeccion.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            GridProyeccion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            GridProyeccion.BackgroundColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            GridProyeccion.BorderStyle = BorderStyle.None
            GridProyeccion.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(20), CByte(25), CByte(40))
            DataGridViewCellStyle1.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            DataGridViewCellStyle1.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
            DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
            GridProyeccion.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            GridProyeccion.ColumnHeadersHeight = 38
            GridProyeccion.Columns.AddRange(New DataGridViewColumn() {colTipo, colPeriodo, colIng, colEgr, colRec, colBal})
            DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            DataGridViewCellStyle2.Font = New Font("Segoe UI", 9.5F)
            DataGridViewCellStyle2.ForeColor = Color.FromArgb(CByte(210), CByte(220), CByte(240))
            DataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(CByte(60), CByte(120), CByte(200))
            DataGridViewCellStyle2.SelectionForeColor = Color.White
            DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
            GridProyeccion.DefaultCellStyle = DataGridViewCellStyle2
            GridProyeccion.EnableHeadersVisualStyles = False
            GridProyeccion.Font = New Font("Segoe UI", 9.5F)
            GridProyeccion.GridColor = Color.FromArgb(CByte(40), CByte(50), CByte(75))
            GridProyeccion.Location = New Point(0, 404)
            GridProyeccion.MultiSelect = False
            GridProyeccion.Name = "GridProyeccion"
            GridProyeccion.ReadOnly = True
            GridProyeccion.RowHeadersVisible = False
            GridProyeccion.RowTemplate.Height = 40
            GridProyeccion.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            GridProyeccion.Size = New Size(917, 542)
            GridProyeccion.TabIndex = 6
            ' 
            ' colTipo
            ' 
            colTipo.FillWeight = 14.0F
            colTipo.HeaderText = "Tipo"
            colTipo.Name = "colTipo"
            colTipo.ReadOnly = True
            ' 
            ' colPeriodo
            ' 
            colPeriodo.FillWeight = 20.0F
            colPeriodo.HeaderText = "Período"
            colPeriodo.Name = "colPeriodo"
            colPeriodo.ReadOnly = True
            ' 
            ' colIng
            ' 
            colIng.FillWeight = 18.0F
            colIng.HeaderText = "Ingresos"
            colIng.Name = "colIng"
            colIng.ReadOnly = True
            ' 
            ' colEgr
            ' 
            colEgr.FillWeight = 18.0F
            colEgr.HeaderText = "Egresos"
            colEgr.Name = "colEgr"
            colEgr.ReadOnly = True
            ' 
            ' colRec
            ' 
            colRec.FillWeight = 16.0F
            colRec.HeaderText = "Recurrentes"
            colRec.Name = "colRec"
            colRec.ReadOnly = True
            ' 
            ' colBal
            ' 
            colBal.FillWeight = 14.0F
            colBal.HeaderText = "Balance"
            colBal.Name = "colBal"
            colBal.ReadOnly = True
            ' 
            ' FormProyeccion
            ' 
            BackColor = Color.FromArgb(CByte(22), CByte(27), CByte(44))
            ClientSize = New Size(961, 653)
            Controls.Add(LblTitulo)
            Controls.Add(LblSubtitulo)
            Controls.Add(PnlToolbar)
            Controls.Add(LblResumenProyeccion)
            Controls.Add(PnlAlerta)
            Controls.Add(PnlGrafico)
            Controls.Add(GridProyeccion)
            FormBorderStyle = FormBorderStyle.None
            Name = "FormProyeccion"
            Padding = New Padding(20, 16, 20, 16)
            PnlToolbar.ResumeLayout(False)
            PnlAlerta.ResumeLayout(False)
            PnlAlerta.PerformLayout()
            CType(GridProyeccion, System.ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)
            PerformLayout()

        End Sub

        Friend WithEvents colTipo As DataGridViewTextBoxColumn
        Friend WithEvents colPeriodo As DataGridViewTextBoxColumn
        Friend WithEvents colIng As DataGridViewTextBoxColumn
        Friend WithEvents colEgr As DataGridViewTextBoxColumn
        Friend WithEvents colRec As DataGridViewTextBoxColumn
        Friend WithEvents colBal As DataGridViewTextBoxColumn

    End Class

End Namespace
