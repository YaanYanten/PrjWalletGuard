Imports System.Drawing
Imports System.Windows.Forms
Imports WalletGuard.WalletGuard.Theme

Namespace WalletGuard

    ''' <summary>
    ''' DESIGNER del formulario Usuarios.
    ''' Solo declaración de controles e InitializeComponent.
    ''' Sin lógica de negocio ni manejadores de eventos.
    ''' </summary>
    Partial Public Class FormUsuarios

        ' ── Controles del encabezado ──────────────────────────────────────────
        Private LblTitle As Label
        Private LblSubtitle As Label

        ' ── Barra de herramientas ─────────────────────────────────────────────
        Private PnlToolbar As Panel
        Friend BtnNuevo As Button    ' Friend: accedido desde la lógica
        Friend TxtSearch As TextBox   ' Friend: accedido desde la lógica

        ' ── Grid ─────────────────────────────────────────────────────────────
        Friend GridUsuarios As DataGridView   ' Friend: accedido desde la lógica

        ' ── Disposición ───────────────────────────────────────────────────────
        Private _disposed As Boolean = False

        Protected Overrides Sub Dispose(disposing As Boolean)
            If Not _disposed Then
                If disposing Then
                    GridUsuarios?.Dispose()
                End If
                _disposed = True
            End If
            MyBase.Dispose(disposing)
        End Sub

        ' ── InitializeComponent ───────────────────────────────────────────────
        Private Sub InitializeComponent()
            Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
            Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
            LblTitle = New Label()
            LblSubtitle = New Label()
            PnlToolbar = New Panel()
            BtnNuevo = New Button()
            TxtSearch = New TextBox()
            GridUsuarios = New DataGridView()
            colId = New DataGridViewTextBoxColumn()
            colNombre = New DataGridViewTextBoxColumn()
            colEmail = New DataGridViewTextBoxColumn()
            colRol = New DataGridViewTextBoxColumn()
            colEstado = New DataGridViewTextBoxColumn()
            colRegistro = New DataGridViewTextBoxColumn()
            PnlToolbar.SuspendLayout()
            CType(GridUsuarios, System.ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' LblTitle
            ' 
            LblTitle.AutoSize = True
            LblTitle.Font = New Font("Segoe UI", 18F, FontStyle.Bold)
            LblTitle.ForeColor = Color.FromArgb(CByte(230), CByte(235), CByte(245))
            LblTitle.Location = New Point(0, 0)
            LblTitle.Name = "LblTitle"
            LblTitle.Size = New Size(290, 32)
            LblTitle.TabIndex = 0
            LblTitle.Text = "👤  Gestión de Usuarios"
            ' 
            ' LblSubtitle
            ' 
            LblSubtitle.AutoSize = True
            LblSubtitle.Font = New Font("Segoe UI", 9.5F)
            LblSubtitle.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblSubtitle.Location = New Point(2, 42)
            LblSubtitle.Name = "LblSubtitle"
            LblSubtitle.Size = New Size(229, 17)
            LblSubtitle.TabIndex = 1
            LblSubtitle.Text = "Administración de cuentas y permisos"
            ' 
            ' PnlToolbar
            ' 
            PnlToolbar.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            PnlToolbar.BackColor = Color.Transparent
            PnlToolbar.Controls.Add(BtnNuevo)
            PnlToolbar.Controls.Add(TxtSearch)
            PnlToolbar.Location = New Point(14, 82)
            PnlToolbar.Name = "PnlToolbar"
            PnlToolbar.Size = New Size(996, 44)
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
            BtnNuevo.Image = My.Resources.Resources.historial_de_transacciones
            BtnNuevo.Location = New Point(0, 4)
            BtnNuevo.Name = "BtnNuevo"
            BtnNuevo.Size = New Size(148, 36)
            BtnNuevo.TabIndex = 0
            BtnNuevo.Text = "+  Nuevo Usuario"
            BtnNuevo.UseVisualStyleBackColor = False
            ' 
            ' TxtSearch
            ' 
            TxtSearch.BackColor = Color.FromArgb(CByte(40), CByte(48), CByte(72))
            TxtSearch.BorderStyle = BorderStyle.None
            TxtSearch.Font = New Font("Segoe UI", 9.5F)
            TxtSearch.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            TxtSearch.Location = New Point(160, 8)
            TxtSearch.Name = "TxtSearch"
            TxtSearch.Size = New Size(220, 17)
            TxtSearch.TabIndex = 1
            TxtSearch.Text = "🔍  Buscar usuario..."
            ' 
            ' GridUsuarios
            ' 
            GridUsuarios.AllowUserToAddRows = False
            GridUsuarios.AllowUserToDeleteRows = False
            DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(38), CByte(47), CByte(70))
            GridUsuarios.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
            GridUsuarios.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            GridUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            GridUsuarios.BackgroundColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            GridUsuarios.BorderStyle = BorderStyle.None
            GridUsuarios.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(20), CByte(25), CByte(40))
            DataGridViewCellStyle2.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
            DataGridViewCellStyle2.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            DataGridViewCellStyle2.Padding = New Padding(8, 0, 0, 0)
            DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
            DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
            DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
            GridUsuarios.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
            GridUsuarios.ColumnHeadersHeight = 40
            GridUsuarios.Columns.AddRange(New DataGridViewColumn() {colId, colNombre, colEmail, colRol, colEstado, colRegistro})
            DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = Color.FromArgb(CByte(32), CByte(40), CByte(60))
            DataGridViewCellStyle3.Font = New Font("Segoe UI", 9.5F)
            DataGridViewCellStyle3.ForeColor = Color.FromArgb(CByte(230), CByte(235), CByte(245))
            DataGridViewCellStyle3.Padding = New Padding(8, 0, 0, 0)
            DataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(CByte(60), CByte(120), CByte(200))
            DataGridViewCellStyle3.SelectionForeColor = Color.White
            DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
            GridUsuarios.DefaultCellStyle = DataGridViewCellStyle3
            GridUsuarios.EnableHeadersVisualStyles = False
            GridUsuarios.Font = New Font("Segoe UI", 9.5F)
            GridUsuarios.GridColor = Color.FromArgb(CByte(45), CByte(55), CByte(80))
            GridUsuarios.Location = New Point(14, 132)
            GridUsuarios.MultiSelect = False
            GridUsuarios.Name = "GridUsuarios"
            GridUsuarios.ReadOnly = True
            GridUsuarios.RowHeadersVisible = False
            GridUsuarios.RowTemplate.Height = 44
            GridUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            GridUsuarios.Size = New Size(996, 289)
            GridUsuarios.TabIndex = 3
            ' 
            ' colId
            ' 
            colId.FillWeight = 6F
            colId.HeaderText = "ID"
            colId.Name = "colId"
            colId.ReadOnly = True
            ' 
            ' colNombre
            ' 
            colNombre.FillWeight = 22F
            colNombre.HeaderText = "Nombre"
            colNombre.Name = "colNombre"
            colNombre.ReadOnly = True
            ' 
            ' colEmail
            ' 
            colEmail.FillWeight = 28F
            colEmail.HeaderText = "Correo"
            colEmail.Name = "colEmail"
            colEmail.ReadOnly = True
            ' 
            ' colRol
            ' 
            colRol.FillWeight = 14F
            colRol.HeaderText = "Rol"
            colRol.Name = "colRol"
            colRol.ReadOnly = True
            ' 
            ' colEstado
            ' 
            colEstado.FillWeight = 12F
            colEstado.HeaderText = "Estado"
            colEstado.Name = "colEstado"
            colEstado.ReadOnly = True
            ' 
            ' colRegistro
            ' 
            colRegistro.FillWeight = 18F
            colRegistro.HeaderText = "Registro"
            colRegistro.Name = "colRegistro"
            colRegistro.ReadOnly = True
            ' 
            ' FormUsuarios
            ' 
            BackColor = Color.FromArgb(CByte(30), CByte(35), CByte(55))
            ClientSize = New Size(1016, 434)
            Controls.Add(LblTitle)
            Controls.Add(LblSubtitle)
            Controls.Add(PnlToolbar)
            Controls.Add(GridUsuarios)
            Name = "FormUsuarios"
            Padding = New Padding(24)
            PnlToolbar.ResumeLayout(False)
            PnlToolbar.PerformLayout()
            CType(GridUsuarios, System.ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)
            PerformLayout()

        End Sub

        Friend WithEvents colId As DataGridViewTextBoxColumn
        Friend WithEvents colNombre As DataGridViewTextBoxColumn
        Friend WithEvents colEmail As DataGridViewTextBoxColumn
        Friend WithEvents colRol As DataGridViewTextBoxColumn
        Friend WithEvents colEstado As DataGridViewTextBoxColumn
        Friend WithEvents colRegistro As DataGridViewTextBoxColumn

    End Class

End Namespace
