Imports System.Drawing
Imports System.Windows.Forms
Imports DL_WalletGuard.WalletGuard.AccesoDatos
Imports DL_WalletGuard.WalletGuard.Entidades
Imports WalletGuard.Entidades

Namespace WalletGuard.Presentacion

    ''' <summary>
    ''' LÓGICA del formulario de registro de movimiento financiero.
    ''' Se abre desde el calendario al hacer clic en un día.
    ''' Permite registrar un Ingreso o un Egreso para la fecha seleccionada.
    ''' Los controles están en FormRegistroMovimiento.Designer.vb.
    ''' </summary>
    Partial Public Class FormRegistroMovimiento
        Inherits Form

#Region "Campos"

        Private ReadOnly _fechaSeleccionada As Date
        Private ReadOnly _ingresoDAL As IngresoDAL
        Private ReadOnly _egresoDAL As EgresoDAL

#End Region

#Region "Constructor"

        ''' <summary>
        ''' Recibe la fecha del día pulsado en el calendario.
        ''' </summary>
        Public Sub New(fecha As Date)
            InitializeComponent()
            _fechaSeleccionada = fecha
            _ingresoDAL = New IngresoDAL()
            _egresoDAL = New EgresoDAL()
        End Sub

#End Region

#Region "Carga inicial"

        Private Sub FormRegistroMovimiento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            LblFechaSeleccionada.Text = _fechaSeleccionada.ToString("dddd, dd 'de' MMMM 'de' yyyy")
            DtpFecha.Value = _fechaSeleccionada
            RbIngreso.Checked = True
            CambiarTipoMovimiento()
            CargarCategorias()
        End Sub

#End Region

#Region "Cambio de tipo (Ingreso / Egreso)"

        Private Sub RbIngreso_CheckedChanged(sender As Object, e As EventArgs) Handles RbIngreso.CheckedChanged
            If RbIngreso.Checked Then CambiarTipoMovimiento()
        End Sub

        Private Sub RbEgreso_CheckedChanged(sender As Object, e As EventArgs) Handles RbEgreso.CheckedChanged
            If RbEgreso.Checked Then CambiarTipoMovimiento()
        End Sub

        Private Sub CambiarTipoMovimiento()
            If RbIngreso.Checked Then
                PnlIndicador.BackColor = Color.FromArgb(60, 180, 90)
                LblTipoActual.Text = "📥  Registrar Ingreso"
                LblTipoActual.ForeColor = Color.FromArgb(80, 220, 130)
                NumMonto.ForeColor = Color.FromArgb(80, 220, 130)
                BtnGuardar.BackColor = Color.FromArgb(40, 140, 70)
            Else
                PnlIndicador.BackColor = Color.FromArgb(200, 60, 60)
                LblTipoActual.Text = "📤  Registrar Egreso"
                LblTipoActual.ForeColor = Color.FromArgb(220, 90, 90)
                NumMonto.ForeColor = Color.FromArgb(220, 90, 90)
                BtnGuardar.BackColor = Color.FromArgb(160, 40, 40)
            End If
        End Sub

#End Region

#Region "Carga de categorías"

        Private Sub CargarCategorias()
            Try
                Dim cats As List(Of Categoria) = New CategoriaDAL().ObtenerTodas()
                CmbCategoria.Items.Clear()
                For Each cat As Categoria In cats
                    CmbCategoria.Items.Add(cat)
                Next
                If CmbCategoria.Items.Count > 0 Then
                    CmbCategoria.SelectedIndex = 0
                End If
            Catch ex As Exception
                ' Continúa sin categorías si falla la BD
            End Try
        End Sub

#End Region

#Region "Guardar movimiento"

        Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
            If Not ValidarFormulario() Then Return

            Try
                If RbIngreso.Checked Then
                    GuardarIngreso()
                Else
                    GuardarEgreso()
                End If

                Me.DialogResult = DialogResult.OK
                Me.Close()

            Catch ex As Exception
                MessageBox.Show($"Error al guardar el movimiento:{Environment.NewLine}{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub GuardarIngreso()
            Dim ing As New Ingreso()
            ing.Fecha = DtpFecha.Value.Date
            ing.Monto = NumMonto.Value
            ing.IdCategoria = ObtenerIdCategoriaSeleccionada()
            ing.Descripcion = TxtDescripcion.Text.Trim()
            _ingresoDAL.Insertar(ing)
        End Sub

        Private Sub GuardarEgreso()
            Dim eg As New Egreso()
            eg.Fecha = DtpFecha.Value.Date
            eg.Monto = NumMonto.Value
            eg.IdCategoria = ObtenerIdCategoriaSeleccionada()
            eg.Tipo = TipoEgreso.Normal
            eg.Descripcion = TxtDescripcion.Text.Trim()
            _egresoDAL.Insertar(eg)
        End Sub

        Private Function ObtenerIdCategoriaSeleccionada() As Integer
            If CmbCategoria.SelectedItem IsNot Nothing Then
                Dim cat As Categoria = DirectCast(CmbCategoria.SelectedItem, Categoria)
                Return cat.IdCategoria
            End If
            Return 1  ' categoría por defecto
        End Function

#End Region

#Region "Validaciones"

        Private Function ValidarFormulario() As Boolean
            If NumMonto.Value <= 0 Then
                MostrarError("El monto debe ser mayor a cero.")
                NumMonto.Focus()
                Return False
            End If
            If CmbCategoria.SelectedItem Is Nothing Then
                MostrarError("Selecciona una categoría.")
                CmbCategoria.Focus()
                Return False
            End If
            Return True
        End Function

        Private Sub MostrarError(mensaje As String)
            LblError.Text = "⚠  " & mensaje
            LblError.Visible = True
        End Sub

        Private Sub LimpiarError(sender As Object, e As EventArgs) _
            Handles NumMonto.ValueChanged, CmbCategoria.SelectedIndexChanged,
                    TxtDescripcion.TextChanged
            LblError.Visible = False
        End Sub

#End Region

#Region "Cancelar"

        Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

#End Region

    End Class

End Namespace
