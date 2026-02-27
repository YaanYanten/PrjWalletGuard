Imports System.Drawing
Imports System.Windows.Forms

Namespace WalletGuard.Models

    ''' <summary>
    ''' Modelo de datos de un ítem de navegación del Sidebar.
    ''' Preparado para futuro sistema de permisos por rol y carga dinámica.
    ''' </summary>
    Public Class SidebarItem

        ''' <summary>Texto que se muestra cuando el sidebar está expandido.</summary>
        Public Property Title As String

        ''' <summary>Carácter Unicode usado como ícono (Segoe UI Symbol).</summary>
        Public Property Icon As Bitmap

        ''' <summary>Fábrica que crea el formulario hijo correspondiente.</summary>
        Public Property FormFactory As Func(Of Form)

        ''' <summary>Texto del tooltip cuando el sidebar está colapsado.</summary>
        Public Property Tooltip As String

        ''' <summary>(Futuro) Roles con acceso. Vacío = acceso libre.</summary>
        Public Property RequiredRoles As String() = Array.Empty(Of String)()

        ''' <summary>(Futuro) Clave única del módulo para carga dinámica.</summary>
        Public Property ModuleKey As String = String.Empty

        Public Sub New(title As String, icon As Bitmap,
                       formFactory As Func(Of Form),
                       Optional tooltip As String = "")
            Me.Title = title
            Me.Icon = icon
            Me.FormFactory = formFactory
            Me.Tooltip = If(String.IsNullOrWhiteSpace(tooltip), title, tooltip)
        End Sub

    End Class

End Namespace
