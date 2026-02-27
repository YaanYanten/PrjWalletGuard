Imports System.Drawing
Imports System.Windows.Forms

Namespace WalletGuard.Presentacion

    ''' <summary>
    ''' DESIGNER de FormDeudas — corregido.
    ''' Cambios: agrega NumPagoMinimo, LblDetallePagoMin, LblDetalleEstado,
    ''' BtnCambiarEstado. Sin lógica. Sin bloques With.
    ''' </summary>
    Partial Public Class FormDeudas

        ' ── Encabezado ────────────────────────────────────────────────────────
        Private LblTitulo         As Label
        Private LblSubtitulo      As Label
        Private PnlToolbar        As Panel
        Friend WithEvents BtnNuevaDeuda As Button
        Friend WithEvents BtnRegistrarPago As Button
        Friend WithEvents BtnCambiarEstado As Button
        Friend  LblTotalPendiente As Label
        Friend  LblTotalPagado    As Label

        ' ── Layout ────────────────────────────────────────────────────────────
        Private PnlLayout         As Panel

        ' ── Panel izquierdo — lista ───────────────────────────────────────────
        Private PnlIzquierdo      As Panel
        Private LblTituloLista    As Label
        Friend  PnlListaDeudas    As FlowLayoutPanel

        ' ── Panel derecho ─────────────────────────────────────────────────────
        Private PnlDerecho        As Panel

        ' Detalle
        Friend  PnlDetalle             As Panel
        Friend  LblDetalleNombre       As Label
        Friend  LblDetalleAcreedor     As Label
        Friend  LblDetalleEstado       As Label      ' NUEVO — muestra estado con color
        Friend  LblDetalleOriginal     As Label
        Friend  LblDetallePagado       As Label
        Friend  LblDetalleSaldo        As Label
        Friend  LblDetallePct          As Label
        Friend  LblDetallePagoMin      As Label      ' NUEVO — pago mínimo mensual
        Friend  LblDetalleProyeccion   As Label
        Private LblTituloPagos         As Label
        Friend  GridPagos              As DataGridView
        Friend WithEvents BtnEliminarPago As Button

        ' Formulario nueva deuda
        Friend  PnlFormDeuda           As Panel
        Friend  LblFormDeudaTitulo     As Label
        Private LblNombreDeudaL        As Label
        Friend  TxtNombreDeuda         As TextBox
        Private LblAcreedorL           As Label
        Friend  TxtAcreedor            As TextBox
        Private LblMontoDeuda          As Label
        Friend  NumMontoDeuda          As NumericUpDown
        Private LblTasaL               As Label
        Friend  NumTasa                As NumericUpDown
        Private LblPagoMinimoL         As Label      ' NUEVO
        Friend  NumPagoMinimo          As NumericUpDown  ' NUEVO
        Private LblInicioDeuda         As Label
        Friend  DtpInicioDeuda         As DateTimePicker
        Friend WithEvents ChkSinVencimiento As CheckBox
        Private LblVencimiento         As Label
        Friend  DtpVencimiento         As DateTimePicker
        Friend WithEvents BtnGuardarDeuda As Button
        Friend WithEvents BtnCancelarDeuda As Button

        Private _disposed As Boolean = False

        Protected Overrides Sub Dispose(disposing As Boolean)
            If Not _disposed Then
                If disposing Then
                    GridPagos?.Dispose()
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
            BtnNuevaDeuda = New Button()
            BtnRegistrarPago = New Button()
            BtnCambiarEstado = New Button()
            LblTotalPendiente = New Label()
            LblTotalPagado = New Label()
            PnlLayout = New Panel()
            PnlDerecho = New Panel()
            PnlDetalle = New Panel()
            LblDetalleNombre = New Label()
            LblDetalleAcreedor = New Label()
            LblDetalleEstado = New Label()
            LblDetalleOriginal = New Label()
            LblDetallePagado = New Label()
            LblDetalleSaldo = New Label()
            LblDetallePct = New Label()
            LblDetallePagoMin = New Label()
            LblDetalleProyeccion = New Label()
            LblTituloPagos = New Label()
            GridPagos = New DataGridView()
            colPagoId = New DataGridViewTextBoxColumn()
            colPagoFecha = New DataGridViewTextBoxColumn()
            colPagoMonto = New DataGridViewTextBoxColumn()
            colPagoDesc = New DataGridViewTextBoxColumn()
            BtnEliminarPago = New Button()
            PnlFormDeuda = New Panel()
            LblFormDeudaTitulo = New Label()
            LblNombreDeudaL = New Label()
            TxtNombreDeuda = New TextBox()
            LblAcreedorL = New Label()
            TxtAcreedor = New TextBox()
            LblMontoDeuda = New Label()
            NumMontoDeuda = New NumericUpDown()
            LblTasaL = New Label()
            NumTasa = New NumericUpDown()
            LblPagoMinimoL = New Label()
            NumPagoMinimo = New NumericUpDown()
            LblInicioDeuda = New Label()
            DtpInicioDeuda = New DateTimePicker()
            ChkSinVencimiento = New CheckBox()
            LblVencimiento = New Label()
            DtpVencimiento = New DateTimePicker()
            BtnGuardarDeuda = New Button()
            BtnCancelarDeuda = New Button()
            PnlIzquierdo = New Panel()
            PnlListaDeudas = New FlowLayoutPanel()
            LblTituloLista = New Label()
            PnlToolbar.SuspendLayout()
            PnlLayout.SuspendLayout()
            PnlDerecho.SuspendLayout()
            PnlDetalle.SuspendLayout()
            CType(GridPagos, System.ComponentModel.ISupportInitialize).BeginInit()
            PnlFormDeuda.SuspendLayout()
            CType(NumMontoDeuda, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(NumTasa, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(NumPagoMinimo, System.ComponentModel.ISupportInitialize).BeginInit()
            PnlIzquierdo.SuspendLayout()
            SuspendLayout()
            ' 
            ' LblTitulo
            ' 
            LblTitulo.AutoSize = True
            LblTitulo.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
            LblTitulo.ForeColor = Color.FromArgb(CByte(230), CByte(235), CByte(245))
            LblTitulo.Location = New Point(0, 0)
            LblTitulo.Name = "LblTitulo"
            LblTitulo.Size = New Size(276, 32)
            LblTitulo.TabIndex = 0
            LblTitulo.Text = "💳  Gestión de Deudas"
            ' 
            ' LblSubtitulo
            ' 
            LblSubtitulo.AutoSize = True
            LblSubtitulo.Font = New Font("Segoe UI", 9.5F)
            LblSubtitulo.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblSubtitulo.Location = New Point(2, 42)
            LblSubtitulo.Name = "LblSubtitulo"
            LblSubtitulo.Size = New Size(251, 17)
            LblSubtitulo.TabIndex = 1
            LblSubtitulo.Text = "Seguimiento de deudas, abonos y estado"
            ' 
            ' PnlToolbar
            ' 
            PnlToolbar.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            PnlToolbar.BackColor = Color.Transparent
            PnlToolbar.Controls.Add(BtnNuevaDeuda)
            PnlToolbar.Controls.Add(BtnRegistrarPago)
            PnlToolbar.Controls.Add(BtnCambiarEstado)
            PnlToolbar.Controls.Add(LblTotalPendiente)
            PnlToolbar.Controls.Add(LblTotalPagado)
            PnlToolbar.Location = New Point(0, 70)
            PnlToolbar.Name = "PnlToolbar"
            PnlToolbar.Size = New Size(1616, 44)
            PnlToolbar.TabIndex = 2
            ' 
            ' BtnNuevaDeuda
            ' 
            BtnNuevaDeuda.BackColor = Color.FromArgb(CByte(60), CByte(120), CByte(200))
            BtnNuevaDeuda.Cursor = Cursors.Hand
            BtnNuevaDeuda.FlatAppearance.BorderSize = 0
            BtnNuevaDeuda.FlatStyle = FlatStyle.Flat
            BtnNuevaDeuda.Font = New Font("Segoe UI", 9.5F)
            BtnNuevaDeuda.ForeColor = Color.White
            BtnNuevaDeuda.Location = New Point(0, 4)
            BtnNuevaDeuda.Name = "BtnNuevaDeuda"
            BtnNuevaDeuda.Size = New Size(140, 36)
            BtnNuevaDeuda.TabIndex = 0
            BtnNuevaDeuda.Text = "+  Nueva Deuda"
            BtnNuevaDeuda.UseVisualStyleBackColor = False
            ' 
            ' BtnRegistrarPago
            ' 
            BtnRegistrarPago.BackColor = Color.FromArgb(CByte(40), CByte(140), CByte(70))
            BtnRegistrarPago.Cursor = Cursors.Hand
            BtnRegistrarPago.FlatAppearance.BorderSize = 0
            BtnRegistrarPago.FlatStyle = FlatStyle.Flat
            BtnRegistrarPago.Font = New Font("Segoe UI", 9.5F)
            BtnRegistrarPago.ForeColor = Color.White
            BtnRegistrarPago.Location = New Point(148, 4)
            BtnRegistrarPago.Name = "BtnRegistrarPago"
            BtnRegistrarPago.Size = New Size(150, 36)
            BtnRegistrarPago.TabIndex = 1
            BtnRegistrarPago.Text = "💲  Registrar Pago"
            BtnRegistrarPago.UseVisualStyleBackColor = False
            ' 
            ' BtnCambiarEstado
            ' 
            BtnCambiarEstado.BackColor = Color.FromArgb(CByte(100), CByte(80), CByte(20))
            BtnCambiarEstado.Cursor = Cursors.Hand
            BtnCambiarEstado.FlatAppearance.BorderSize = 0
            BtnCambiarEstado.FlatStyle = FlatStyle.Flat
            BtnCambiarEstado.Font = New Font("Segoe UI", 9.5F)
            BtnCambiarEstado.ForeColor = Color.White
            BtnCambiarEstado.Location = New Point(306, 4)
            BtnCambiarEstado.Name = "BtnCambiarEstado"
            BtnCambiarEstado.Size = New Size(110, 36)
            BtnCambiarEstado.TabIndex = 2
            BtnCambiarEstado.Text = "↕  Estado"
            BtnCambiarEstado.UseVisualStyleBackColor = False
            ' 
            ' LblTotalPendiente
            ' 
            LblTotalPendiente.AutoSize = True
            LblTotalPendiente.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            LblTotalPendiente.ForeColor = Color.FromArgb(CByte(220), CByte(90), CByte(90))
            LblTotalPendiente.Location = New Point(430, 12)
            LblTotalPendiente.Name = "LblTotalPendiente"
            LblTotalPendiente.Size = New Size(114, 15)
            LblTotalPendiente.TabIndex = 3
            LblTotalPendiente.Text = "Total pendiente: $0"
            ' 
            ' LblTotalPagado
            ' 
            LblTotalPagado.AutoSize = True
            LblTotalPagado.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            LblTotalPagado.ForeColor = Color.FromArgb(CByte(80), CByte(200), CByte(120))
            LblTotalPagado.Location = New Point(600, 12)
            LblTotalPagado.Name = "LblTotalPagado"
            LblTotalPagado.Size = New Size(97, 15)
            LblTotalPagado.TabIndex = 4
            LblTotalPagado.Text = "Total pagado: $0"
            ' 
            ' PnlLayout
            ' 
            PnlLayout.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            PnlLayout.BackColor = Color.Transparent
            PnlLayout.Controls.Add(PnlDerecho)
            PnlLayout.Controls.Add(PnlIzquierdo)
            PnlLayout.Location = New Point(0, 122)
            PnlLayout.Name = "PnlLayout"
            PnlLayout.Size = New Size(916, 388)
            PnlLayout.TabIndex = 3
            ' 
            ' PnlDerecho
            ' 
            PnlDerecho.BackColor = Color.Transparent
            PnlDerecho.Controls.Add(PnlDetalle)
            PnlDerecho.Controls.Add(PnlFormDeuda)
            PnlDerecho.Dock = DockStyle.Fill
            PnlDerecho.Location = New Point(340, 0)
            PnlDerecho.Name = "PnlDerecho"
            PnlDerecho.Padding = New Padding(8, 0, 0, 0)
            PnlDerecho.Size = New Size(576, 388)
            PnlDerecho.TabIndex = 0
            ' 
            ' PnlDetalle
            ' 
            PnlDetalle.BackColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            PnlDetalle.Controls.Add(LblDetalleNombre)
            PnlDetalle.Controls.Add(LblDetalleAcreedor)
            PnlDetalle.Controls.Add(LblDetalleEstado)
            PnlDetalle.Controls.Add(LblDetalleOriginal)
            PnlDetalle.Controls.Add(LblDetallePagado)
            PnlDetalle.Controls.Add(LblDetalleSaldo)
            PnlDetalle.Controls.Add(LblDetallePct)
            PnlDetalle.Controls.Add(LblDetallePagoMin)
            PnlDetalle.Controls.Add(LblDetalleProyeccion)
            PnlDetalle.Controls.Add(LblTituloPagos)
            PnlDetalle.Controls.Add(GridPagos)
            PnlDetalle.Controls.Add(BtnEliminarPago)
            PnlDetalle.Dock = DockStyle.Fill
            PnlDetalle.Location = New Point(8, 0)
            PnlDetalle.Name = "PnlDetalle"
            PnlDetalle.Padding = New Padding(16)
            PnlDetalle.Size = New Size(568, 388)
            PnlDetalle.TabIndex = 0
            PnlDetalle.Visible = False
            ' 
            ' LblDetalleNombre
            ' 
            LblDetalleNombre.AutoSize = True
            LblDetalleNombre.Font = New Font("Segoe UI", 14.0F, FontStyle.Bold)
            LblDetalleNombre.ForeColor = Color.FromArgb(CByte(220), CByte(228), CByte(245))
            LblDetalleNombre.Location = New Point(0, 0)
            LblDetalleNombre.Name = "LblDetalleNombre"
            LblDetalleNombre.Size = New Size(0, 25)
            LblDetalleNombre.TabIndex = 0
            ' 
            ' LblDetalleAcreedor
            ' 
            LblDetalleAcreedor.AutoSize = True
            LblDetalleAcreedor.Font = New Font("Segoe UI", 9.0F)
            LblDetalleAcreedor.ForeColor = Color.FromArgb(CByte(120), CByte(135), CByte(165))
            LblDetalleAcreedor.Location = New Point(0, 30)
            LblDetalleAcreedor.Name = "LblDetalleAcreedor"
            LblDetalleAcreedor.Size = New Size(0, 15)
            LblDetalleAcreedor.TabIndex = 1
            ' 
            ' LblDetalleEstado
            ' 
            LblDetalleEstado.AutoSize = True
            LblDetalleEstado.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            LblDetalleEstado.ForeColor = Color.FromArgb(CByte(80), CByte(140), CByte(220))
            LblDetalleEstado.Location = New Point(220, 30)
            LblDetalleEstado.Name = "LblDetalleEstado"
            LblDetalleEstado.Size = New Size(0, 15)
            LblDetalleEstado.TabIndex = 2
            ' 
            ' LblDetalleOriginal
            ' 
            LblDetalleOriginal.AutoSize = True
            LblDetalleOriginal.Font = New Font("Segoe UI", 9.5F)
            LblDetalleOriginal.ForeColor = Color.FromArgb(CByte(180), CByte(190), CByte(215))
            LblDetalleOriginal.Location = New Point(0, 54)
            LblDetalleOriginal.Name = "LblDetalleOriginal"
            LblDetalleOriginal.Size = New Size(0, 17)
            LblDetalleOriginal.TabIndex = 3
            ' 
            ' LblDetallePagado
            ' 
            LblDetallePagado.AutoSize = True
            LblDetallePagado.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
            LblDetallePagado.ForeColor = Color.FromArgb(CByte(80), CByte(200), CByte(120))
            LblDetallePagado.Location = New Point(0, 76)
            LblDetallePagado.Name = "LblDetallePagado"
            LblDetallePagado.Size = New Size(0, 17)
            LblDetallePagado.TabIndex = 4
            ' 
            ' LblDetalleSaldo
            ' 
            LblDetalleSaldo.AutoSize = True
            LblDetalleSaldo.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
            LblDetalleSaldo.ForeColor = Color.FromArgb(CByte(220), CByte(90), CByte(90))
            LblDetalleSaldo.Location = New Point(210, 76)
            LblDetalleSaldo.Name = "LblDetalleSaldo"
            LblDetalleSaldo.Size = New Size(0, 17)
            LblDetalleSaldo.TabIndex = 5
            ' 
            ' LblDetallePct
            ' 
            LblDetallePct.AutoSize = True
            LblDetallePct.Font = New Font("Segoe UI", 8.5F)
            LblDetallePct.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblDetallePct.Location = New Point(0, 98)
            LblDetallePct.Name = "LblDetallePct"
            LblDetallePct.Size = New Size(0, 15)
            LblDetallePct.TabIndex = 6
            ' 
            ' LblDetallePagoMin
            ' 
            LblDetallePagoMin.AutoSize = True
            LblDetallePagoMin.Font = New Font("Segoe UI", 9.0F)
            LblDetallePagoMin.ForeColor = Color.FromArgb(CByte(240), CByte(180), CByte(60))
            LblDetallePagoMin.Location = New Point(200, 98)
            LblDetallePagoMin.Name = "LblDetallePagoMin"
            LblDetallePagoMin.Size = New Size(0, 15)
            LblDetallePagoMin.TabIndex = 7
            ' 
            ' LblDetalleProyeccion
            ' 
            LblDetalleProyeccion.AutoSize = True
            LblDetalleProyeccion.Font = New Font("Segoe UI", 8.5F, FontStyle.Italic)
            LblDetalleProyeccion.ForeColor = Color.FromArgb(CByte(180), CByte(200), CByte(240))
            LblDetalleProyeccion.Location = New Point(0, 118)
            LblDetalleProyeccion.Name = "LblDetalleProyeccion"
            LblDetalleProyeccion.Size = New Size(0, 15)
            LblDetalleProyeccion.TabIndex = 8
            LblDetalleProyeccion.Visible = False
            ' 
            ' LblTituloPagos
            ' 
            LblTituloPagos.AutoSize = True
            LblTituloPagos.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            LblTituloPagos.ForeColor = Color.FromArgb(CByte(160), CByte(175), CByte(200))
            LblTituloPagos.Location = New Point(0, 142)
            LblTituloPagos.Name = "LblTituloPagos"
            LblTituloPagos.Size = New Size(105, 15)
            LblTituloPagos.TabIndex = 9
            LblTituloPagos.Text = "Historial de Pagos"
            ' 
            ' GridPagos
            ' 
            GridPagos.AllowUserToAddRows = False
            GridPagos.AllowUserToDeleteRows = False
            DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(32), CByte(40), CByte(60))
            GridPagos.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
            GridPagos.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            GridPagos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            GridPagos.BackgroundColor = Color.FromArgb(CByte(22), CByte(27), CByte(44))
            GridPagos.BorderStyle = BorderStyle.None
            GridPagos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(20), CByte(25), CByte(40))
            DataGridViewCellStyle2.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            DataGridViewCellStyle2.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
            DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
            DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
            GridPagos.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
            GridPagos.ColumnHeadersHeight = 36
            GridPagos.Columns.AddRange(New DataGridViewColumn() {colPagoId, colPagoFecha, colPagoMonto, colPagoDesc})
            DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            DataGridViewCellStyle3.Font = New Font("Segoe UI", 9.5F)
            DataGridViewCellStyle3.ForeColor = Color.FromArgb(CByte(210), CByte(220), CByte(240))
            DataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(CByte(60), CByte(120), CByte(200))
            DataGridViewCellStyle3.SelectionForeColor = Color.White
            DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
            GridPagos.DefaultCellStyle = DataGridViewCellStyle3
            GridPagos.EnableHeadersVisualStyles = False
            GridPagos.Font = New Font("Segoe UI", 9.5F)
            GridPagos.GridColor = Color.FromArgb(CByte(40), CByte(50), CByte(75))
            GridPagos.Location = New Point(0, 162)
            GridPagos.MultiSelect = False
            GridPagos.Name = "GridPagos"
            GridPagos.ReadOnly = True
            GridPagos.RowHeadersVisible = False
            GridPagos.RowTemplate.Height = 38
            GridPagos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            GridPagos.Size = New Size(948, 568)
            GridPagos.TabIndex = 10
            ' 
            ' colPagoId
            ' 
            colPagoId.FillWeight = 5.0F
            colPagoId.HeaderText = "ID"
            colPagoId.Name = "colPagoId"
            colPagoId.ReadOnly = True
            colPagoId.Visible = False
            ' 
            ' colPagoFecha
            ' 
            colPagoFecha.FillWeight = 22.0F
            colPagoFecha.HeaderText = "Fecha"
            colPagoFecha.Name = "colPagoFecha"
            colPagoFecha.ReadOnly = True
            ' 
            ' colPagoMonto
            ' 
            colPagoMonto.FillWeight = 22.0F
            colPagoMonto.HeaderText = "Monto"
            colPagoMonto.Name = "colPagoMonto"
            colPagoMonto.ReadOnly = True
            ' 
            ' colPagoDesc
            ' 
            colPagoDesc.FillWeight = 51.0F
            colPagoDesc.HeaderText = "Capital Pagado"
            colPagoDesc.Name = "colPagoDesc"
            colPagoDesc.ReadOnly = True
            ' 
            ' BtnEliminarPago
            ' 
            BtnEliminarPago.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            BtnEliminarPago.BackColor = Color.FromArgb(CByte(60), CByte(30), CByte(30))
            BtnEliminarPago.Cursor = Cursors.Hand
            BtnEliminarPago.FlatAppearance.BorderSize = 0
            BtnEliminarPago.FlatStyle = FlatStyle.Flat
            BtnEliminarPago.Font = New Font("Segoe UI", 8.5F)
            BtnEliminarPago.ForeColor = Color.FromArgb(CByte(200), CByte(120), CByte(120))
            BtnEliminarPago.Location = New Point(0, 742)
            BtnEliminarPago.Name = "BtnEliminarPago"
            BtnEliminarPago.Size = New Size(150, 30)
            BtnEliminarPago.TabIndex = 11
            BtnEliminarPago.Text = "🗑  Eliminar pago"
            BtnEliminarPago.UseVisualStyleBackColor = False
            ' 
            ' PnlFormDeuda
            ' 
            PnlFormDeuda.BackColor = Color.FromArgb(CByte(28), CByte(34), CByte(52))
            PnlFormDeuda.Controls.Add(LblFormDeudaTitulo)
            PnlFormDeuda.Controls.Add(LblNombreDeudaL)
            PnlFormDeuda.Controls.Add(TxtNombreDeuda)
            PnlFormDeuda.Controls.Add(LblAcreedorL)
            PnlFormDeuda.Controls.Add(TxtAcreedor)
            PnlFormDeuda.Controls.Add(LblMontoDeuda)
            PnlFormDeuda.Controls.Add(NumMontoDeuda)
            PnlFormDeuda.Controls.Add(LblTasaL)
            PnlFormDeuda.Controls.Add(NumTasa)
            PnlFormDeuda.Controls.Add(LblPagoMinimoL)
            PnlFormDeuda.Controls.Add(NumPagoMinimo)
            PnlFormDeuda.Controls.Add(LblInicioDeuda)
            PnlFormDeuda.Controls.Add(DtpInicioDeuda)
            PnlFormDeuda.Controls.Add(ChkSinVencimiento)
            PnlFormDeuda.Controls.Add(LblVencimiento)
            PnlFormDeuda.Controls.Add(DtpVencimiento)
            PnlFormDeuda.Controls.Add(BtnGuardarDeuda)
            PnlFormDeuda.Controls.Add(BtnCancelarDeuda)
            PnlFormDeuda.Dock = DockStyle.Fill
            PnlFormDeuda.Location = New Point(8, 0)
            PnlFormDeuda.Name = "PnlFormDeuda"
            PnlFormDeuda.Padding = New Padding(16)
            PnlFormDeuda.Size = New Size(568, 388)
            PnlFormDeuda.TabIndex = 1
            PnlFormDeuda.Visible = False
            ' 
            ' LblFormDeudaTitulo
            ' 
            LblFormDeudaTitulo.AutoSize = True
            LblFormDeudaTitulo.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
            LblFormDeudaTitulo.ForeColor = Color.FromArgb(CByte(220), CByte(228), CByte(245))
            LblFormDeudaTitulo.Location = New Point(0, 0)
            LblFormDeudaTitulo.Name = "LblFormDeudaTitulo"
            LblFormDeudaTitulo.Size = New Size(114, 21)
            LblFormDeudaTitulo.TabIndex = 0
            LblFormDeudaTitulo.Text = "Nueva Deuda"
            ' 
            ' LblNombreDeudaL
            ' 
            LblNombreDeudaL.AutoSize = True
            LblNombreDeudaL.Font = New Font("Segoe UI", 8.5F)
            LblNombreDeudaL.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblNombreDeudaL.Location = New Point(0, 36)
            LblNombreDeudaL.Name = "LblNombreDeudaL"
            LblNombreDeudaL.Size = New Size(115, 15)
            LblNombreDeudaL.TabIndex = 1
            LblNombreDeudaL.Text = "Nombre de la deuda"
            ' 
            ' TxtNombreDeuda
            ' 
            TxtNombreDeuda.BackColor = Color.FromArgb(CByte(35), CByte(42), CByte(65))
            TxtNombreDeuda.BorderStyle = BorderStyle.FixedSingle
            TxtNombreDeuda.Font = New Font("Segoe UI", 9.5F)
            TxtNombreDeuda.ForeColor = Color.FromArgb(CByte(220), CByte(228), CByte(245))
            TxtNombreDeuda.Location = New Point(0, 54)
            TxtNombreDeuda.Name = "TxtNombreDeuda"
            TxtNombreDeuda.Size = New Size(320, 24)
            TxtNombreDeuda.TabIndex = 2
            ' 
            ' LblAcreedorL
            ' 
            LblAcreedorL.AutoSize = True
            LblAcreedorL.Font = New Font("Segoe UI", 8.5F)
            LblAcreedorL.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblAcreedorL.Location = New Point(0, 94)
            LblAcreedorL.Name = "LblAcreedorL"
            LblAcreedorL.Size = New Size(55, 15)
            LblAcreedorL.TabIndex = 3
            LblAcreedorL.Text = "Acreedor"
            ' 
            ' TxtAcreedor
            ' 
            TxtAcreedor.BackColor = Color.FromArgb(CByte(35), CByte(42), CByte(65))
            TxtAcreedor.BorderStyle = BorderStyle.FixedSingle
            TxtAcreedor.Font = New Font("Segoe UI", 9.5F)
            TxtAcreedor.ForeColor = Color.FromArgb(CByte(220), CByte(228), CByte(245))
            TxtAcreedor.Location = New Point(0, 112)
            TxtAcreedor.Name = "TxtAcreedor"
            TxtAcreedor.Size = New Size(320, 24)
            TxtAcreedor.TabIndex = 4
            ' 
            ' LblMontoDeuda
            ' 
            LblMontoDeuda.AutoSize = True
            LblMontoDeuda.Font = New Font("Segoe UI", 8.5F)
            LblMontoDeuda.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblMontoDeuda.Location = New Point(0, 152)
            LblMontoDeuda.Name = "LblMontoDeuda"
            LblMontoDeuda.Size = New Size(70, 15)
            LblMontoDeuda.TabIndex = 5
            LblMontoDeuda.Text = "Monto total"
            ' 
            ' NumMontoDeuda
            ' 
            NumMontoDeuda.BackColor = Color.FromArgb(CByte(35), CByte(42), CByte(65))
            NumMontoDeuda.BorderStyle = BorderStyle.FixedSingle
            NumMontoDeuda.DecimalPlaces = 2
            NumMontoDeuda.Font = New Font("Segoe UI", 9.5F)
            NumMontoDeuda.ForeColor = Color.FromArgb(CByte(220), CByte(228), CByte(245))
            NumMontoDeuda.Location = New Point(0, 170)
            NumMontoDeuda.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
            NumMontoDeuda.Name = "NumMontoDeuda"
            NumMontoDeuda.Size = New Size(150, 24)
            NumMontoDeuda.TabIndex = 6
            ' 
            ' LblTasaL
            ' 
            LblTasaL.AutoSize = True
            LblTasaL.Font = New Font("Segoe UI", 8.5F)
            LblTasaL.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblTasaL.Location = New Point(162, 152)
            LblTasaL.Name = "LblTasaL"
            LblTasaL.Size = New Size(129, 15)
            LblTasaL.TabIndex = 7
            LblTasaL.Text = "Tasa interés % mensual"
            ' 
            ' NumTasa
            ' 
            NumTasa.BackColor = Color.FromArgb(CByte(35), CByte(42), CByte(65))
            NumTasa.BorderStyle = BorderStyle.FixedSingle
            NumTasa.DecimalPlaces = 2
            NumTasa.Font = New Font("Segoe UI", 9.5F)
            NumTasa.ForeColor = Color.FromArgb(CByte(220), CByte(228), CByte(245))
            NumTasa.Location = New Point(162, 170)
            NumTasa.Name = "NumTasa"
            NumTasa.Size = New Size(100, 24)
            NumTasa.TabIndex = 8
            ' 
            ' LblPagoMinimoL
            ' 
            LblPagoMinimoL.AutoSize = True
            LblPagoMinimoL.Font = New Font("Segoe UI", 8.5F)
            LblPagoMinimoL.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblPagoMinimoL.Location = New Point(0, 210)
            LblPagoMinimoL.Name = "LblPagoMinimoL"
            LblPagoMinimoL.Size = New Size(127, 15)
            LblPagoMinimoL.TabIndex = 9
            LblPagoMinimoL.Text = "Pago mínimo mensual"
            ' 
            ' NumPagoMinimo
            ' 
            NumPagoMinimo.BackColor = Color.FromArgb(CByte(35), CByte(42), CByte(65))
            NumPagoMinimo.BorderStyle = BorderStyle.FixedSingle
            NumPagoMinimo.DecimalPlaces = 2
            NumPagoMinimo.Font = New Font("Segoe UI", 9.5F)
            NumPagoMinimo.ForeColor = Color.FromArgb(CByte(220), CByte(228), CByte(245))
            NumPagoMinimo.Location = New Point(0, 228)
            NumPagoMinimo.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
            NumPagoMinimo.Name = "NumPagoMinimo"
            NumPagoMinimo.Size = New Size(150, 24)
            NumPagoMinimo.TabIndex = 10
            ' 
            ' LblInicioDeuda
            ' 
            LblInicioDeuda.AutoSize = True
            LblInicioDeuda.Font = New Font("Segoe UI", 8.5F)
            LblInicioDeuda.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblInicioDeuda.Location = New Point(0, 268)
            LblInicioDeuda.Name = "LblInicioDeuda"
            LblInicioDeuda.Size = New Size(70, 15)
            LblInicioDeuda.TabIndex = 11
            LblInicioDeuda.Text = "Fecha inicio"
            ' 
            ' DtpInicioDeuda
            ' 
            DtpInicioDeuda.Font = New Font("Segoe UI", 9.5F)
            DtpInicioDeuda.Format = DateTimePickerFormat.Short
            DtpInicioDeuda.Location = New Point(0, 286)
            DtpInicioDeuda.Name = "DtpInicioDeuda"
            DtpInicioDeuda.Size = New Size(160, 24)
            DtpInicioDeuda.TabIndex = 12
            ' 
            ' ChkSinVencimiento
            ' 
            ChkSinVencimiento.AutoSize = True
            ChkSinVencimiento.Checked = True
            ChkSinVencimiento.CheckState = CheckState.Checked
            ChkSinVencimiento.Font = New Font("Segoe UI", 9.0F)
            ChkSinVencimiento.ForeColor = Color.FromArgb(CByte(200), CByte(210), CByte(230))
            ChkSinVencimiento.Location = New Point(0, 326)
            ChkSinVencimiento.Name = "ChkSinVencimiento"
            ChkSinVencimiento.Size = New Size(159, 19)
            ChkSinVencimiento.TabIndex = 13
            ChkSinVencimiento.Text = "Sin fecha de vencimiento"
            ' 
            ' LblVencimiento
            ' 
            LblVencimiento.AutoSize = True
            LblVencimiento.Font = New Font("Segoe UI", 8.5F)
            LblVencimiento.ForeColor = Color.FromArgb(CByte(140), CByte(155), CByte(185))
            LblVencimiento.Location = New Point(0, 352)
            LblVencimiento.Name = "LblVencimiento"
            LblVencimiento.Size = New Size(107, 15)
            LblVencimiento.TabIndex = 14
            LblVencimiento.Text = "Fecha vencimiento"
            ' 
            ' DtpVencimiento
            ' 
            DtpVencimiento.Enabled = False
            DtpVencimiento.Font = New Font("Segoe UI", 9.5F)
            DtpVencimiento.Format = DateTimePickerFormat.Short
            DtpVencimiento.Location = New Point(0, 370)
            DtpVencimiento.Name = "DtpVencimiento"
            DtpVencimiento.Size = New Size(160, 24)
            DtpVencimiento.TabIndex = 15
            ' 
            ' BtnGuardarDeuda
            ' 
            BtnGuardarDeuda.BackColor = Color.FromArgb(CByte(40), CByte(140), CByte(70))
            BtnGuardarDeuda.Cursor = Cursors.Hand
            BtnGuardarDeuda.FlatAppearance.BorderSize = 0
            BtnGuardarDeuda.FlatStyle = FlatStyle.Flat
            BtnGuardarDeuda.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
            BtnGuardarDeuda.ForeColor = Color.White
            BtnGuardarDeuda.Location = New Point(0, 416)
            BtnGuardarDeuda.Name = "BtnGuardarDeuda"
            BtnGuardarDeuda.Size = New Size(140, 38)
            BtnGuardarDeuda.TabIndex = 16
            BtnGuardarDeuda.Text = "💾  Guardar"
            BtnGuardarDeuda.UseVisualStyleBackColor = False
            ' 
            ' BtnCancelarDeuda
            ' 
            BtnCancelarDeuda.BackColor = Color.FromArgb(CByte(40), CByte(48), CByte(72))
            BtnCancelarDeuda.Cursor = Cursors.Hand
            BtnCancelarDeuda.FlatAppearance.BorderSize = 0
            BtnCancelarDeuda.FlatStyle = FlatStyle.Flat
            BtnCancelarDeuda.Font = New Font("Segoe UI", 9.5F)
            BtnCancelarDeuda.ForeColor = Color.FromArgb(CByte(160), CByte(170), CByte(190))
            BtnCancelarDeuda.Location = New Point(148, 416)
            BtnCancelarDeuda.Name = "BtnCancelarDeuda"
            BtnCancelarDeuda.Size = New Size(120, 38)
            BtnCancelarDeuda.TabIndex = 17
            BtnCancelarDeuda.Text = "Cancelar"
            BtnCancelarDeuda.UseVisualStyleBackColor = False
            ' 
            ' PnlIzquierdo
            ' 
            PnlIzquierdo.BackColor = Color.Transparent
            PnlIzquierdo.Controls.Add(PnlListaDeudas)
            PnlIzquierdo.Controls.Add(LblTituloLista)
            PnlIzquierdo.Dock = DockStyle.Left
            PnlIzquierdo.Location = New Point(0, 0)
            PnlIzquierdo.Name = "PnlIzquierdo"
            PnlIzquierdo.Padding = New Padding(0, 0, 8, 0)
            PnlIzquierdo.Size = New Size(340, 388)
            PnlIzquierdo.TabIndex = 1
            ' 
            ' PnlListaDeudas
            ' 
            PnlListaDeudas.AutoScroll = True
            PnlListaDeudas.BackColor = Color.Transparent
            PnlListaDeudas.Dock = DockStyle.Fill
            PnlListaDeudas.FlowDirection = FlowDirection.TopDown
            PnlListaDeudas.Location = New Point(0, 15)
            PnlListaDeudas.Name = "PnlListaDeudas"
            PnlListaDeudas.Padding = New Padding(0, 4, 0, 4)
            PnlListaDeudas.Size = New Size(332, 373)
            PnlListaDeudas.TabIndex = 0
            PnlListaDeudas.WrapContents = False
            ' 
            ' LblTituloLista
            ' 
            LblTituloLista.AutoSize = True
            LblTituloLista.Dock = DockStyle.Top
            LblTituloLista.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            LblTituloLista.ForeColor = Color.FromArgb(CByte(160), CByte(175), CByte(200))
            LblTituloLista.Location = New Point(0, 0)
            LblTituloLista.Name = "LblTituloLista"
            LblTituloLista.Size = New Size(70, 15)
            LblTituloLista.TabIndex = 1
            LblTituloLista.Text = "Mis Deudas"
            ' 
            ' FormDeudas
            ' 
            BackColor = Color.FromArgb(CByte(22), CByte(27), CByte(44))
            ClientSize = New Size(1000, 549)
            Controls.Add(LblTitulo)
            Controls.Add(LblSubtitulo)
            Controls.Add(PnlToolbar)
            Controls.Add(PnlLayout)
            FormBorderStyle = FormBorderStyle.None
            Name = "FormDeudas"
            Padding = New Padding(20, 16, 20, 16)
            PnlToolbar.ResumeLayout(False)
            PnlToolbar.PerformLayout()
            PnlLayout.ResumeLayout(False)
            PnlDerecho.ResumeLayout(False)
            PnlDetalle.ResumeLayout(False)
            PnlDetalle.PerformLayout()
            CType(GridPagos, System.ComponentModel.ISupportInitialize).EndInit()
            PnlFormDeuda.ResumeLayout(False)
            PnlFormDeuda.PerformLayout()
            CType(NumMontoDeuda, System.ComponentModel.ISupportInitialize).EndInit()
            CType(NumTasa, System.ComponentModel.ISupportInitialize).EndInit()
            CType(NumPagoMinimo, System.ComponentModel.ISupportInitialize).EndInit()
            PnlIzquierdo.ResumeLayout(False)
            PnlIzquierdo.PerformLayout()
            ResumeLayout(False)
            PerformLayout()

        End Sub

        Friend WithEvents colPagoId As DataGridViewTextBoxColumn
        Friend WithEvents colPagoFecha As DataGridViewTextBoxColumn
        Friend WithEvents colPagoMonto As DataGridViewTextBoxColumn
        Friend WithEvents colPagoDesc As DataGridViewTextBoxColumn

    End Class

End Namespace
