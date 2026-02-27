Imports System.Drawing
Imports System.Windows.Forms
Imports BL_WalletGuard.WalletGuard.LogicaNegocio
Imports DL_WalletGuard.WalletGuard.Entidades
Imports WalletGuard.WalletGuard.Controls
Imports WalletGuard.WalletGuard.Models
Imports WalletGuard.WalletGuard.Presentacion
Imports WalletGuard.WalletGuard.Theme

Namespace WalletGuard

    ''' <summary>
    ''' LÓGICA del formulario principal.
    ''' ──────────────────────────────────────────────────────────────────────
    ''' Contiene: navegación, carga de formularios hijos, animación del sidebar,
    '''           gestión de estado activo y limpieza de recursos.
    '''
    ''' La declaración de todos los controles está en FormMain.Designer.vb.
    ''' </summary>
    Partial Public Class FormMain
        Inherits Form

#Region "Campos de estado"

        Private _isSidebarExpanded As Boolean = True
        Private _targetWidth As Integer = AppTheme.SidebarExpandedWidth
        Private _activeButton As SidebarButton = Nothing
        Private _currentChildForm As Form = Nothing
        Private _sidebarItems As List(Of SidebarItem)
        Private _navButtons As New List(Of SidebarButton)()

#End Region

#Region "Constructor"

        Public Sub New()
            InitializeComponent()   ' ← declarado en el Designer
            BuildSidebarItems()
            PopulateSidebarButtons()
            WireEvents()
            ApplyTheme()
        End Sub

#End Region

#Region "Eventos del formulario"

        Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            ProcesarRecurrentesAlIniciar()
        End Sub

#End Region

#Region "Procesamiento de recurrentes al iniciar"

        ''' <summary>
        ''' Ejecuta el procesamiento de gastos recurrentes UNA SOLA VEZ al iniciar.
        ''' Muestra un mensaje informativo si se generaron gastos pendientes.
        ''' </summary>
        Private Sub ProcesarRecurrentesAlIniciar()
            Try
                Dim servicio As New FinancialRecurringService()
                Dim resultado As ResultadoProcesamiento = servicio.ProcessPendingRecurring()

                If resultado.HuboErrores Then
                    MessageBox.Show(
                        $"Advertencia al procesar gastos recurrentes:{Environment.NewLine}{resultado.MensajeError}",
                        "WalletGuard",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
                    Return
                End If

                If resultado.TotalGenerados > 0 Then
                    Dim sb As New System.Text.StringBuilder()
                    sb.AppendLine($"Se generaron {resultado.TotalGenerados} gasto(s) recurrente(s) pendiente(s):")
                    sb.AppendLine()

                    For Each d As DetalleRecurrente In resultado.Detalle
                        sb.AppendLine($"  • {d.Nombre}: {d.MesesGenerados} mes(es) — Total: {d.TotalGenerado:C2}")
                    Next

                    MessageBox.Show(
                        sb.ToString(),
                        "Gastos Recurrentes Procesados",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information)
                End If

            Catch ex As Exception
                MessageBox.Show(
                    $"Error al procesar gastos recurrentes:{Environment.NewLine}{ex.Message}",
                    "WalletGuard — Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region "Definición de módulos"

        ''' <summary>
        ''' Registro de módulos de navegación.
        ''' Para agregar un módulo nuevo, añadir un SidebarItem aquí y nada más.
        ''' </summary>
        Private Sub BuildSidebarItems()
            _sidebarItems = New List(Of SidebarItem) From {
                New SidebarItem("Dashboard", My.Resources.Dashboard, Function() New FormDashboard(), "Dashboard"),
                New SidebarItem("Gastos Recurrentes", My.Resources.Gastos_Recurrente, Function() New FormGastosRecurrentes(), "Gastos Recurrentes"),
                New SidebarItem("Deudas", My.Resources.Gastos_Recurrente, Function() New FormDeudas(), "Deudas"),
                New SidebarItem("Flujo de Caja", My.Resources.Gastos_Recurrente, Function() New ReporteFlujoCaja(), "Flujo de Caja"),
                New SidebarItem("Proyeccion", My.Resources.Gastos_Recurrente, Function() New FormProyeccion(), "Proyeccion"),
                New SidebarItem("Historial", My.Resources.historial_de_transacciones, Function() New FormHistorialRecurrentes(), "Historial"),
                New SidebarItem("Ajustes", My.Resources.setting, Function() New FormUsuarios(), "Ajustes")
            }
        End Sub

        ''' <summary>
        ''' Crea un SidebarButton por cada SidebarItem y lo agrega al FlowPanel.
        ''' </summary>
        Private Sub PopulateSidebarButtons()
            For Each item In _sidebarItems
                Dim btn As New SidebarButton() With {
                    .Icon = item.Icon,
                    .Title = item.Title,
                    .ShowText = True,
                    .Width = AppTheme.SidebarExpandedWidth,
                    .Height = AppTheme.SidebarItemHeight
                }
                ToolTipSidebar.SetToolTip(btn, item.Tooltip)

                Dim capturedItem = item   ' evitar bug de closure
                AddHandler btn.Click, Sub(s, e) OnNavButtonClick(btn, capturedItem)

                _navButtons.Add(btn)
                FlowSidebarItems.Controls.Add(btn)
            Next
        End Sub

#End Region

#Region "Suscripción a eventos"

        ''' <summary>Enlaza los eventos del formulario. Llamado una sola vez en el constructor.</summary>
        Private Sub WireEvents()
            AddHandler BtnToggle.Click, AddressOf OnToggleSidebar
        End Sub

#End Region

#Region "Tema"

        ''' <summary>
        ''' Aplica los valores del tema a todos los controles.
        ''' Punto único de personalización: al cambiar AppTheme, llamar este método.
        ''' </summary>
        Private Sub ApplyTheme()
            Me.BackColor = AppTheme.MainBackground
            PnlSidebar.BackColor = AppTheme.SidebarBackground
            PnlSidebarHeader.BackColor = AppTheme.HeaderBackground
            FlowSidebarItems.BackColor = AppTheme.SidebarBackground
            PnlMain.BackColor = AppTheme.MainBackground
            LblAppName.ForeColor = AppTheme.TextPrimary
            LblAppName.Font = AppTheme.FontTitle
        End Sub

#End Region

#Region "Carga de formularios hijos"

        ''' <summary>
        ''' Carga un formulario hijo dentro del panel principal.
        ''' ─────────────────────────────────────────────────────
        ''' • Cierra y libera el formulario anterior.
        ''' • Configura TopLevel = False y Dock = Fill.
        ''' • Es el único método que debe usarse para mostrar módulos.
        ''' </summary>
        Public Sub LoadFormInPanel(form As Form)
            ' Cerrar formulario previo
            If _currentChildForm IsNot Nothing Then
                _currentChildForm.Hide()
                _currentChildForm.Close()
                _currentChildForm.Dispose()
                _currentChildForm = Nothing
            End If

            ' Incrustar el nuevo formulario
            _currentChildForm = form
            With _currentChildForm
                .TopLevel = False
                .FormBorderStyle = FormBorderStyle.None
                .Dock = DockStyle.Fill
                .BackColor = AppTheme.MainBackground
            End With

            PnlMain.Controls.Clear()
            PnlMain.Controls.Add(_currentChildForm)
            _currentChildForm.Show()
        End Sub

        ''' <summary>Manejador del clic en un botón de navegación del sidebar.</summary>
        Private Sub OnNavButtonClick(btn As SidebarButton, item As SidebarItem)
            If _activeButton IsNot Nothing Then _activeButton.IsActive = False
            btn.IsActive = True
            _activeButton = btn
            LoadFormInPanel(item.FormFactory.Invoke())
        End Sub

#End Region

#Region "Animación del Sidebar"

        ''' <summary>
        ''' Alterna el sidebar entre expandido (220px) y colapsado (60px).
        ''' Inicia la animación configurando el ancho objetivo y arrancando el Timer.
        ''' </summary>
        Private Sub OnToggleSidebar(sender As Object, e As EventArgs)
            _isSidebarExpanded = Not _isSidebarExpanded
            _targetWidth = If(_isSidebarExpanded,
                                    AppTheme.SidebarExpandedWidth,
                                    AppTheme.SidebarCollapsedWidth)
            TimerAnimation.Start()
        End Sub

        ''' <summary>
        ''' Tick del Timer de animación.
        ''' ────────────────────────────
        ''' Cada tick mueve el sidebar SidebarAnimationStep píxeles hacia _targetWidth.
        ''' Cuando la diferencia restante es menor que el paso, salta directo al objetivo
        ''' y detiene el Timer → efecto visual de "ease-out" sin librerías externas.
        ''' </summary>
        Private Sub OnAnimationTick(sender As Object, e As EventArgs) _
            Handles TimerAnimation.Tick

            Dim diff As Integer = _targetWidth - PnlSidebar.Width

            If Math.Abs(diff) <= AppTheme.SidebarAnimationStep Then
                PnlSidebar.Width = _targetWidth
                TimerAnimation.Stop()
                OnAnimationCompleted()
                Return
            End If

            PnlSidebar.Width += If(diff > 0,
                                    AppTheme.SidebarAnimationStep,
                                   -AppTheme.SidebarAnimationStep)
            SyncSidebarControls()
        End Sub

        ''' <summary>Sincronización final una vez terminada la animación.</summary>
        Private Sub OnAnimationCompleted()
            SyncSidebarControls()
            FlowSidebarItems.PerformLayout()
        End Sub

        ''' <summary>
        ''' Actualiza el ancho de cada botón y la visibilidad del texto
        ''' según el ancho actual del sidebar durante y después de la animación.
        ''' </summary>
        Private Sub SyncSidebarControls()
            Dim showText As Boolean = PnlSidebar.Width > (AppTheme.SidebarCollapsedWidth + 30)
            LblAppName.Visible = showText

            For Each btn In _navButtons
                btn.Width = PnlSidebar.Width
                btn.ShowText = showText
            Next
        End Sub

        Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
            Me.Close()
        End Sub

#End Region

    End Class

End Namespace
