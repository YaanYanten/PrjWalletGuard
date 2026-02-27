Imports System.Windows.Forms
Imports BL_WalletGuard.WalletGuard.LogicaNegocio
Imports DL_WalletGuard.WalletGuard.Entidades

Namespace WalletGuard.Presentacion

    ''' <summary>
    ''' LÓGICA del formulario de Gestión de Gastos Recurrentes.
    ''' Permite ver, crear, editar y desactivar gastos recurrentes.
    ''' Los controles están declarados en FormGastosRecurrentes.Designer.vb.
    ''' </summary>
    Partial Public Class FormGastosRecurrentes
        Inherits Form

#Region "Campos"

        Private ReadOnly _servicio As GastoRecurrenteService
        Private _modoEdicion As Boolean = False
        Private _idEditando As Integer = 0

#End Region

#Region "Constructor"

        Public Sub New()
            InitializeComponent()
            _servicio = New GastoRecurrenteService()
        End Sub

#End Region

#Region "Eventos del formulario"

        Private Sub FormGastosRecurrentes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            CargarGrid()
        End Sub

#End Region

#Region "Carga del grid"

        Private Sub CargarGrid()
            Try
                Dim lista As List(Of GastoRecurrente) = _servicio.ObtenerTodos()
                GridRecurrentes.Rows.Clear()

                For Each gr As GastoRecurrente In lista
                    Dim estado As String = If(gr.Activo, "✅ Activo", "⛔ Inactivo")
                    Dim fechaFin As String = If(gr.FechaFin.HasValue, gr.FechaFin.Value.ToString("dd/MM/yyyy"), "Sin límite")
                    Dim ultimoProc As String = If(gr.FechaUltimoProcesado.HasValue, gr.FechaUltimoProcesado.Value.ToString("dd/MM/yyyy"), "Nunca")

                    GridRecurrentes.Rows.Add(
                        gr.IdRecurrente,
                        gr.Nombre,
                        gr.Monto.ToString("C2"),
                        gr.DiaCorte,
                        gr.FechaInicio.ToString("dd/MM/yyyy"),
                        fechaFin,
                        ultimoProc,
                        estado
                    )
                Next

            Catch ex As Exception
                MostrarError("Error al cargar la lista de gastos recurrentes.", ex)
            End Try
        End Sub

#End Region

#Region "Eventos de la UI"

        Private Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
            LimpiarFormulario()
            _modoEdicion = False
            _idEditando = 0
            HabilitarFormulario(True)
            TxtNombre.Focus()
        End Sub

        Private Sub BtnEditar_Click(sender As Object, e As EventArgs) Handles BtnEditar.Click
            If GridRecurrentes.SelectedRows.Count = 0 Then
                MessageBox.Show("Seleccione un gasto recurrente para editar.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim id As Integer = Convert.ToInt32(GridRecurrentes.SelectedRows(0).Cells("ColId").Value)
            Dim lista As List(Of GastoRecurrente) = _servicio.ObtenerTodos()
            Dim gr As GastoRecurrente = lista.FirstOrDefault(Function(x) x.IdRecurrente = id)

            If gr Is Nothing Then Return

            _modoEdicion = True
            _idEditando = gr.IdRecurrente
            CargarEnFormulario(gr)
            HabilitarFormulario(True)
        End Sub

        Private Sub BtnDesactivar_Click(sender As Object, e As EventArgs) Handles BtnDesactivar.Click
            If GridRecurrentes.SelectedRows.Count = 0 Then
                MessageBox.Show("Seleccione un gasto recurrente para desactivar.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim id As Integer = Convert.ToInt32(GridRecurrentes.SelectedRows(0).Cells("ColId").Value)
            Dim nombre As String = GridRecurrentes.SelectedRows(0).Cells("ColNombre").Value?.ToString()

            Dim confirmacion As DialogResult = MessageBox.Show(
                $"¿Desactivar el gasto recurrente '{nombre}'?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

            If confirmacion = DialogResult.Yes Then
                Try
                    _servicio.Desactivar(id)
                    CargarGrid()
                Catch ex As Exception
                    MostrarError("Error al desactivar el gasto recurrente.", ex)
                End Try
            End If
        End Sub

        Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
            Try
                Dim gr As GastoRecurrente = LeerDesdeFormulario()
                _servicio.Guardar(gr)

                MessageBox.Show("Gasto recurrente guardado correctamente.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

                LimpiarFormulario()
                HabilitarFormulario(False)
                CargarGrid()

            Catch ex As ArgumentException
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                MostrarError("Error al guardar el gasto recurrente.", ex)
            End Try
        End Sub

        Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
            LimpiarFormulario()
            HabilitarFormulario(False)
            _modoEdicion = False
            _idEditando = 0
        End Sub

#End Region

#Region "Helpers de UI"

        Private Sub CargarEnFormulario(gr As GastoRecurrente)
            TxtNombre.Text = gr.Nombre
            NumMonto.Value = gr.Monto
            NumDiaCorte.Value = gr.DiaCorte
            DtpFechaInicio.Value = gr.FechaInicio
            ChkSinFin.Checked = Not gr.FechaFin.HasValue
            DtpFechaFin.Enabled = gr.FechaFin.HasValue
            DtpFechaFin.Value = If(gr.FechaFin.HasValue, gr.FechaFin.Value, Date.Today.AddYears(1))
            ChkActivo.Checked = gr.Activo
        End Sub

        Private Function LeerDesdeFormulario() As GastoRecurrente
            Dim gr As New GastoRecurrente()
            gr.IdRecurrente = _idEditando
            gr.Nombre = TxtNombre.Text.Trim()
            gr.Monto = NumMonto.Value
            gr.DiaCorte = CInt(NumDiaCorte.Value)
            gr.FechaInicio = DtpFechaInicio.Value.Date
            gr.FechaFin = If(ChkSinFin.Checked, CType(Nothing, Date?), DtpFechaFin.Value.Date)
            gr.Activo = ChkActivo.Checked
            ' IdCategoria se obtendría de un ComboBox en una implementación completa
            gr.IdCategoria = 1
            Return gr
        End Function

        Private Sub LimpiarFormulario()
            TxtNombre.Text = String.Empty
            NumMonto.Value = 0
            NumDiaCorte.Value = 1
            DtpFechaInicio.Value = Date.Today
            DtpFechaFin.Value = Date.Today.AddYears(1)
            ChkSinFin.Checked = True
            ChkActivo.Checked = True
            DtpFechaFin.Enabled = False
        End Sub

        Private Sub HabilitarFormulario(habilitar As Boolean)
            TxtNombre.Enabled = habilitar
            NumMonto.Enabled = habilitar
            NumDiaCorte.Enabled = habilitar
            DtpFechaInicio.Enabled = habilitar
            ChkSinFin.Enabled = habilitar
            ChkActivo.Enabled = habilitar
            BtnGuardar.Enabled = habilitar
            BtnCancelar.Enabled = habilitar
            BtnNuevo.Enabled = Not habilitar
            BtnEditar.Enabled = Not habilitar
            BtnDesactivar.Enabled = Not habilitar
        End Sub

        Private Sub ChkSinFin_CheckedChanged(sender As Object, e As EventArgs) Handles ChkSinFin.CheckedChanged
            DtpFechaFin.Enabled = Not ChkSinFin.Checked
        End Sub

        Private Sub MostrarError(mensaje As String, ex As Exception)
            MessageBox.Show($"{mensaje}{Environment.NewLine}{ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Sub

#End Region

    End Class

End Namespace
