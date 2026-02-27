Imports System.Drawing
Imports System.Windows.Forms
Imports WalletGuard.WalletGuard.Theme

Namespace WalletGuard

    ''' <summary>
    ''' LÓGICA del formulario hijo: Gestión de Usuarios.
    ''' ────────────────────────────────────────────────────────────────────
    ''' Contiene: carga de datos, búsqueda, eventos de UI.
    ''' La declaración de controles está en FormUsuarios.Designer.vb.
    ''' </summary>
    Partial Public Class FormUsuarios
        Inherits Form

#Region "Constructor"

        Public Sub New()
            InitializeComponent()   ' ← definido en el Designer
            WireEvents()
            LoadUsers()
        End Sub

#End Region

#Region "Suscripción a eventos"

        Private Sub WireEvents()
            AddHandler TxtSearch.Enter, AddressOf OnSearchEnter
            AddHandler TxtSearch.Leave, AddressOf OnSearchLeave
            AddHandler TxtSearch.TextChanged, AddressOf OnSearchTextChanged
            AddHandler BtnNuevo.Click, AddressOf OnNuevoClick
        End Sub

#End Region

#Region "Eventos de búsqueda"

        Private Const SearchPlaceholder As String = "🔍  Buscar usuario..."

        Private Sub OnSearchEnter(sender As Object, e As EventArgs)
            If TxtSearch.Text = SearchPlaceholder Then
                TxtSearch.Text      = ""
                TxtSearch.ForeColor = AppTheme.TextPrimary
            End If
        End Sub

        Private Sub OnSearchLeave(sender As Object, e As EventArgs)
            If String.IsNullOrWhiteSpace(TxtSearch.Text) Then
                TxtSearch.Text      = SearchPlaceholder
                TxtSearch.ForeColor = AppTheme.TextSecondary
            End If
        End Sub

        Private Sub OnSearchTextChanged(sender As Object, e As EventArgs)
            If TxtSearch.Text = SearchPlaceholder Then Return
            FilterGrid(TxtSearch.Text.Trim())
        End Sub

        ''' <summary>Filtra las filas del grid por nombre o correo.</summary>
        Private Sub FilterGrid(query As String)
            For Each row As DataGridViewRow In GridUsuarios.Rows
                Dim nombre = row.Cells("Nombre").Value?.ToString()
                Dim email = row.Cells("Email").Value?.ToString()
                row.Visible = String.IsNullOrEmpty(query) OrElse
                              nombre.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                              email.IndexOf(query,  StringComparison.OrdinalIgnoreCase) >= 0
            Next
        End Sub

#End Region

#Region "Evento: Nuevo usuario"

        Private Sub OnNuevoClick(sender As Object, e As EventArgs)
            ' Placeholder: aquí se abriría el formulario de alta de usuario
            MessageBox.Show("Aquí se abriría el formulario de alta de usuario.",
                            "Nuevo Usuario",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
        End Sub

#End Region

#Region "Carga de datos"

        ''' <summary>
        ''' Carga datos de ejemplo en el DataGridView.
        ''' En producción se sustituiría por una llamada al servicio/repositorio.
        ''' </summary>
        Private Sub LoadUsers()
            Dim data = {
                {"001", "Ana García",    "ana.garcia@corp.com",    "Admin",   "✅ Activo",   "01/01/2024"},
                {"002", "Carlos López",  "carlos.lopez@corp.com",  "Editor",  "✅ Activo",   "15/02/2024"},
                {"003", "María Torres",  "maria.torres@corp.com",  "Viewer",  "✅ Activo",   "20/03/2024"},
                {"004", "Juan Pérez",    "juan.perez@corp.com",    "Editor",  "⛔ Inactivo", "10/04/2024"},
                {"005", "Laura Sánchez", "laura.sanchez@corp.com", "Admin",   "✅ Activo",   "05/05/2024"},
                {"006", "Roberto Díaz",  "roberto.diaz@corp.com",  "Viewer",  "✅ Activo",   "22/06/2024"},
                {"007", "Patricia Ruiz", "patricia.ruiz@corp.com", "Editor",  "⛔ Inactivo", "08/07/2024"}
            }

            For Each row In data
                GridUsuarios.Rows.Add(row)
            Next
        End Sub

#End Region

    End Class

End Namespace
