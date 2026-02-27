Imports System.Drawing
Imports System.Windows.Forms
Imports WalletGuard.WalletGuard.Controls
Imports WalletGuard.WalletGuard.Theme

Namespace WalletGuard

    ''' <summary>
    ''' DESIGNER del formulario principal.
    ''' ──────────────────────────────────────────────────────────────────────
    ''' REGLA DE ORO: este archivo solo contiene:
    '''   1. Declaración de variables de control (WithEvents / sin lógica).
    '''   2. InitializeComponent con instanciación y propiedades visuales.
    '''   3. Dispose de componentes.
    '''
    ''' PROHIBIDO aquí: manejadores de eventos, cálculos, llamadas a métodos
    '''                 de negocio, condicionales de flujo, animaciones.
    ''' </summary>
    Partial Public Class FormMain

        ' ── Controles del Sidebar ─────────────────────────────────────────────
        Private PnlSidebar As Panel
        Private PnlSidebarHeader As Panel
        Private BtnToggle As Controls.SidebarButton
        Private FlowSidebarItems As FlowLayoutPanel
        Private LblAppName As Label

        ' ── Panel principal ───────────────────────────────────────────────────
        Private PnlMain As Panel

        ' ── Componentes de infraestructura ────────────────────────────────────
        Private WithEvents TimerAnimation As Timer   ' WithEvents permite Handles en .vb
        Private ToolTipSidebar As ToolTip

        ' ── Disposición de recursos ───────────────────────────────────────────
        Private _disposed As Boolean = False

        Protected Overrides Sub Dispose(disposing As Boolean)
            If Not _disposed Then
                If disposing Then
                    TimerAnimation?.Dispose()
                    ToolTipSidebar?.Dispose()
                End If
                _disposed = True
            End If
            MyBase.Dispose(disposing)
        End Sub

        ' ── InitializeComponent ───────────────────────────────────────────────
        ''' <summary>
        ''' Instancia, configura y compone la jerarquía de controles.
        ''' Solo propiedades estáticas (sin lógica de negocio).
        ''' </summary>
        Private Sub InitializeComponent()
            components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
            PnlSidebar = New Panel()
            FlowSidebarItems = New FlowLayoutPanel()
            PnlSidebarHeader = New Panel()
            LblAppName = New Label()
            BtnToggle = New SidebarButton()
            PnlMain = New Panel()
            btnClose = New Button()
            TimerAnimation = New Timer(components)
            ToolTipSidebar = New ToolTip(components)
            TableLayoutPanel1 = New TableLayoutPanel()
            PnlSidebar.SuspendLayout()
            PnlSidebarHeader.SuspendLayout()
            TableLayoutPanel1.SuspendLayout()
            SuspendLayout()
            ' 
            ' PnlSidebar
            ' 
            PnlSidebar.BackColor = Color.FromArgb(CByte(20), CByte(25), CByte(40))
            PnlSidebar.Controls.Add(FlowSidebarItems)
            PnlSidebar.Controls.Add(PnlSidebarHeader)
            PnlSidebar.Dock = DockStyle.Left
            PnlSidebar.Location = New Point(0, 0)
            PnlSidebar.Name = "PnlSidebar"
            PnlSidebar.Size = New Size(220, 704)
            PnlSidebar.TabIndex = 1
            ' 
            ' FlowSidebarItems
            ' 
            FlowSidebarItems.BackColor = Color.FromArgb(CByte(20), CByte(25), CByte(40))
            FlowSidebarItems.Dock = DockStyle.Fill
            FlowSidebarItems.FlowDirection = FlowDirection.TopDown
            FlowSidebarItems.Location = New Point(0, 56)
            FlowSidebarItems.Name = "FlowSidebarItems"
            FlowSidebarItems.Padding = New Padding(0, 8, 0, 0)
            FlowSidebarItems.Size = New Size(220, 648)
            FlowSidebarItems.TabIndex = 0
            FlowSidebarItems.WrapContents = False
            ' 
            ' PnlSidebarHeader
            ' 
            PnlSidebarHeader.BackColor = Color.FromArgb(CByte(15), CByte(20), CByte(35))
            PnlSidebarHeader.Controls.Add(LblAppName)
            PnlSidebarHeader.Controls.Add(BtnToggle)
            PnlSidebarHeader.Dock = DockStyle.Top
            PnlSidebarHeader.Location = New Point(0, 0)
            PnlSidebarHeader.Name = "PnlSidebarHeader"
            PnlSidebarHeader.Size = New Size(220, 56)
            PnlSidebarHeader.TabIndex = 1
            ' 
            ' LblAppName
            ' 
            LblAppName.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
            LblAppName.ForeColor = Color.FromArgb(CByte(230), CByte(235), CByte(245))
            LblAppName.Location = New Point(68, 0)
            LblAppName.Name = "LblAppName"
            LblAppName.Size = New Size(220, 56)
            LblAppName.TabIndex = 0
            LblAppName.Text = "WalletGuard"
            LblAppName.TextAlign = ContentAlignment.MiddleLeft
            ' 
            ' BtnToggle
            ' 
            BtnToggle.BackColor = Color.FromArgb(CByte(20), CByte(25), CByte(40))
            BtnToggle.Dock = DockStyle.Left
            BtnToggle.Icon = CType(resources.GetObject("BtnToggle.Icon"), Bitmap)
            BtnToggle.IsActive = False
            BtnToggle.Location = New Point(0, 0)
            BtnToggle.Name = "BtnToggle"
            BtnToggle.ShowText = False
            BtnToggle.Size = New Size(60, 56)
            BtnToggle.TabIndex = 1
            BtnToggle.Title = ""
            ' 
            ' PnlMain
            ' 
            PnlMain.BackColor = Color.FromArgb(CByte(30), CByte(35), CByte(55))
            PnlMain.Location = New Point(3, 39)
            PnlMain.Name = "PnlMain"
            PnlMain.Size = New Size(1024, 704)
            PnlMain.TabIndex = 0
            ' 
            ' btnClose
            ' 
            btnClose.Anchor = AnchorStyles.Top Or AnchorStyles.Right
            btnClose.BackColor = Color.FromArgb(CByte(20), CByte(25), CByte(40))
            btnClose.Cursor = Cursors.Hand
            btnClose.FlatStyle = FlatStyle.Flat
            btnClose.Image = My.Resources.Resources.cerrar
            btnClose.Location = New Point(993, 3)
            btnClose.Name = "btnClose"
            btnClose.Size = New Size(34, 30)
            btnClose.TabIndex = 0
            btnClose.TextImageRelation = TextImageRelation.TextBeforeImage
            btnClose.UseVisualStyleBackColor = False
            ' 
            ' TimerAnimation
            ' 
            TimerAnimation.Interval = 8
            ' 
            ' TableLayoutPanel1
            ' 
            TableLayoutPanel1.ColumnCount = 1
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
            TableLayoutPanel1.Controls.Add(btnClose, 0, 0)
            TableLayoutPanel1.Controls.Add(PnlMain, 0, 1)
            TableLayoutPanel1.Dock = DockStyle.Fill
            TableLayoutPanel1.Location = New Point(220, 0)
            TableLayoutPanel1.Name = "TableLayoutPanel1"
            TableLayoutPanel1.RowCount = 2
            TableLayoutPanel1.RowStyles.Add(New RowStyle())
            TableLayoutPanel1.RowStyles.Add(New RowStyle())
            TableLayoutPanel1.Size = New Size(1030, 704)
            TableLayoutPanel1.TabIndex = 0
            ' 
            ' FormMain
            ' 
            AutoScaleMode = AutoScaleMode.None
            AutoSize = True
            BackColor = Color.FromArgb(CByte(30), CByte(35), CByte(55))
            ClientSize = New Size(1250, 704)
            ControlBox = False
            Controls.Add(TableLayoutPanel1)
            Controls.Add(PnlSidebar)
            FormBorderStyle = FormBorderStyle.None
            MaximizeBox = False
            MinimizeBox = False
            MinimumSize = New Size(700, 500)
            Name = "FormMain"
            StartPosition = FormStartPosition.CenterScreen
            Text = "WalletGuard"
            PnlSidebar.ResumeLayout(False)
            PnlSidebarHeader.ResumeLayout(False)
            TableLayoutPanel1.ResumeLayout(False)
            ResumeLayout(False)

        End Sub

        Private components As System.ComponentModel.IContainer
        Friend WithEvents btnClose As Button
        Friend WithEvents TableLayoutPanel1 As TableLayoutPanel

    End Class

End Namespace
