Imports System.Drawing
Imports System.Windows.Forms
Imports DL_WalletGuard.WalletGuard.Entidades
Imports BL_WalletGuard.WalletGuard.LogicaNegocio

Namespace WalletGuard.Presentacion

    ''' <summary>
    ''' LÓGICA de FormDeudas — corregida para la estructura real de sr_deudas.
    '''
    ''' Cambios respecto a la versión anterior:
    '''   - activa (Boolean)  → Estado (String: 'Activo'|'Pagado'|'Mora')
    '''   - MontoOriginal     → MontoTotal (mapeado al DTO como MontoOriginal para la UI)
    '''   - Se muestra PagoMinimo en el detalle derecho
    '''   - ComboBox de estado para cambiar entre Activo / Mora / Pagado
    '''   - Formulario de nueva deuda incluye campo PagoMinimo
    ''' </summary>
    Partial Public Class FormDeudas
        Inherits Form

#Region "Campos"

        Private ReadOnly _servicio       As DeudaService
        Private _deudaSeleccionada       As ResumenDeuda = Nothing

#End Region

#Region "Constructor"

        Public Sub New()
            InitializeComponent()
            _servicio = New DeudaService()
        End Sub

#End Region

#Region "Carga inicial"

        Private Sub FormDeudas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            CargarResumenes()
        End Sub

        Private Sub CargarResumenes()
            Try
                Dim resumenes As List(Of ResumenDeuda) = _servicio.ObtenerResumenes()
                PnlListaDeudas.Controls.Clear()
                PnlListaDeudas.SuspendLayout()

                For Each r As ResumenDeuda In resumenes
                    PnlListaDeudas.Controls.Add(CrearCardDeuda(r))
                Next

                PnlListaDeudas.ResumeLayout()

                ' Totales — solo deudas activas y en mora
                Dim activas As List(Of ResumenDeuda) =
                    resumenes.Where(Function(x) x.Activa).ToList()

                LblTotalPendiente.Text = $"Total pendiente: {activas.Sum(Function(x) x.SaldoPendiente):C2}"
                LblTotalPagado.Text    = $"Total pagado: {activas.Sum(Function(x) x.TotalPagado):C2}"

            Catch ex As Exception
                MessageBox.Show($"Error al cargar deudas:{Environment.NewLine}{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region "Cards de deuda"

        Private Function CrearCardDeuda(r As ResumenDeuda) As Panel
            ' Color del acento izquierdo según estado
            Dim colorAcento As Color
            Select Case r.Estado
                Case EstadoDeuda.Mora   : colorAcento = Color.FromArgb(220, 150, 0)
                Case EstadoDeuda.Pagado : colorAcento = Color.FromArgb(60, 180, 90)
                Case Else               : colorAcento = Color.FromArgb(60, 120, 200)
            End Select

            Dim card As New Panel()
            card.Size      = New Size(PnlListaDeudas.Width - 12, 96)
            card.BackColor = Color.FromArgb(32, 40, 62)
            card.Margin    = New Padding(4, 0, 4, 8)
            card.Cursor    = Cursors.Hand
            card.Tag       = r

            Dim pnlAccent As New Panel()
            pnlAccent.BackColor = colorAcento
            pnlAccent.Bounds    = New Rectangle(0, 0, 4, 96)

            ' Nombre de la deuda
            Dim lblNombre As New Label()
            lblNombre.Text      = r.Nombre
            lblNombre.ForeColor = Color.FromArgb(220, 228, 245)
            lblNombre.Font      = New Font("Segoe UI", 10.0F, FontStyle.Bold)
            lblNombre.AutoSize  = True
            lblNombre.Location  = New Point(12, 8)

            ' Acreedor
            Dim lblAcreedor As New Label()
            lblAcreedor.Text      = r.Acreedor
            lblAcreedor.ForeColor = Color.FromArgb(120, 135, 165)
            lblAcreedor.Font      = New Font("Segoe UI", 8.0F, FontStyle.Regular)
            lblAcreedor.AutoSize  = True
            lblAcreedor.Location  = New Point(12, 28)

            ' Estado (badge)
            Dim lblEstado As New Label()
            lblEstado.Text      = r.Estado
            lblEstado.ForeColor = colorAcento
            lblEstado.Font      = New Font("Segoe UI", 7.5F, FontStyle.Bold)
            lblEstado.AutoSize  = True
            lblEstado.Location  = New Point(card.Width - 72, 10)

            ' Saldo pendiente
            Dim lblSaldo As New Label()
            lblSaldo.Text      = If(r.SaldoPendiente = 0, "✅ Liquidada", $"Saldo: {r.SaldoPendiente:C2}")
            lblSaldo.ForeColor = If(r.SaldoPendiente = 0,
                Color.FromArgb(80, 200, 120), Color.FromArgb(220, 90, 90))
            lblSaldo.Font      = New Font("Segoe UI", 9.5F, FontStyle.Bold)
            lblSaldo.AutoSize  = True
            lblSaldo.Location  = New Point(12, 46)

            ' Barra de progreso
            Dim pnlBarra As New Panel()
            pnlBarra.BackColor = Color.FromArgb(20, 26, 42)
            pnlBarra.Bounds    = New Rectangle(12, 72, card.Width - 80, 10)

            Dim anchoProgreso As Integer = CInt((card.Width - 80) * CDbl(r.PorcentajePagado) / 100.0)
            Dim pnlProgreso   As New Panel()
            pnlProgreso.BackColor = colorAcento
            pnlProgreso.Bounds    = New Rectangle(0, 0, anchoProgreso, 10)
            pnlBarra.Controls.Add(pnlProgreso)

            Dim lblPct As New Label()
            lblPct.Text      = $"{r.PorcentajePagado:N0}%"
            lblPct.ForeColor = Color.FromArgb(140, 155, 185)
            lblPct.Font      = New Font("Segoe UI", 8.0F, FontStyle.Regular)
            lblPct.AutoSize  = True
            lblPct.Location  = New Point(card.Width - 62, 68)

            card.Controls.Add(pnlAccent)
            card.Controls.Add(lblNombre)
            card.Controls.Add(lblAcreedor)
            card.Controls.Add(lblEstado)
            card.Controls.Add(lblSaldo)
            card.Controls.Add(pnlBarra)
            card.Controls.Add(lblPct)

            AddHandler card.Click, Sub(s, e) SeleccionarDeuda(r)
            AddHandler card.MouseEnter, Sub(s, e) card.BackColor = Color.FromArgb(42, 52, 78)
            AddHandler card.MouseLeave, Sub(s, e) card.BackColor = Color.FromArgb(32, 40, 62)
            For Each ctrl As Control In card.Controls
                AddHandler ctrl.Click, Sub(s, e) SeleccionarDeuda(r)
            Next

            Return card
        End Function

        Private Sub SeleccionarDeuda(r As ResumenDeuda)
            _deudaSeleccionada = r
            CargarDetalleDerecho(r)
        End Sub

#End Region

#Region "Panel derecho — detalle"

        Private Sub CargarDetalleDerecho(r As ResumenDeuda)
            PnlDetalle.Visible   = True
            PnlFormDeuda.Visible = False

            LblDetalleNombre.Text   = r.Nombre
            LblDetalleAcreedor.Text = $"Acreedor: {r.Acreedor}"
            LblDetalleOriginal.Text = $"Monto total: {r.MontoOriginal:C2}"
            LblDetallePagado.Text   = $"Pagado: {r.TotalPagado:C2}"
            LblDetalleSaldo.Text    = $"Saldo: {r.SaldoPendiente:C2}"
            LblDetallePct.Text      = $"{r.PorcentajePagado:N1}% liquidado"
            LblDetallePagoMin.Text  = $"Pago mínimo: {r.PagoMinimo:C2}"

            ' Estado con color
            LblDetalleEstado.Text = $"Estado: {r.Estado}"
            LblDetalleEstado.ForeColor = ObtenerColorEstado(r.Estado)

            ' Proyección de meses restantes
            If r.MesesRestantes.HasValue Then
                LblDetalleProyeccion.Text    = $"⏱ Al ritmo actual: {r.MesesRestantes} mes(es) más para liquidar"
                LblDetalleProyeccion.Visible = True
            Else
                LblDetalleProyeccion.Visible = False
            End If

            CargarGridPagos(r.IdDeuda)
        End Sub

        Private Function ObtenerColorEstado(estado As String) As Color
            Select Case estado
                Case EstadoDeuda.Mora   : Return Color.FromArgb(220, 150, 0)
                Case EstadoDeuda.Pagado : Return Color.FromArgb(80, 200, 120)
                Case Else               : Return Color.FromArgb(80, 140, 220)
            End Select
        End Function

        Private Sub CargarGridPagos(idDeuda As Integer)
            Try
                GridPagos.Rows.Clear()
                Dim pagos As List(Of PagoDeuda) = _servicio.ObtenerPagos(idDeuda)
                For Each p As PagoDeuda In pagos
                    GridPagos.Rows.Add(
                        p.IdPago,
                        p.Fecha.ToString("dd/MM/yyyy"),
                        p.Monto.ToString("C2"),
                        p.capital_pagado.ToString("C2"))
                Next
            Catch ex As Exception
                ' Sin pagos registrados
            End Try
        End Sub

#End Region

#Region "Botones principales"

        Private Sub BtnNuevaDeuda_Click(sender As Object, e As EventArgs) Handles BtnNuevaDeuda.Click
            LimpiarFormDeuda()
            PnlDetalle.Visible   = False
            PnlFormDeuda.Visible = True
            LblFormDeudaTitulo.Text = "Nueva Deuda"
            TxtNombreDeuda.Focus()
        End Sub

        Private Sub BtnRegistrarPago_Click(sender As Object, e As EventArgs) Handles BtnRegistrarPago.Click
            If _deudaSeleccionada Is Nothing Then
                MessageBox.Show("Selecciona una deuda para registrar un pago.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            If Not _deudaSeleccionada.Activa Then
                MessageBox.Show("Esta deuda ya está liquidada.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Using dlg As New FormPagoDeuda(
                _deudaSeleccionada.IdDeuda,
                _deudaSeleccionada.Nombre,
                _deudaSeleccionada.SaldoPendiente)

                If dlg.ShowDialog(Me) = DialogResult.OK Then
                    CargarResumenes()
                    ' Re-seleccionar la deuda para refrescar el detalle
                    Dim actualizado As ResumenDeuda =
                        _servicio.ObtenerResumenes().
                        FirstOrDefault(Function(x) x.IdDeuda = _deudaSeleccionada.IdDeuda)
                    If actualizado IsNot Nothing Then
                        _deudaSeleccionada = actualizado
                        CargarDetalleDerecho(actualizado)
                    End If
                End If
            End Using
        End Sub

        Private Sub BtnCambiarEstado_Click(sender As Object, e As EventArgs) Handles BtnCambiarEstado.Click
            If _deudaSeleccionada Is Nothing Then Return

            Dim estados As String() = {EstadoDeuda.Activo, EstadoDeuda.Mora, EstadoDeuda.Pagado}
            Dim actual  As String   = _deudaSeleccionada.Estado
            Dim mensaje As String   = $"Cambiar estado de '{_deudaSeleccionada.Nombre}' ({actual}):" &
                                      $"{Environment.NewLine}  A → Activo{Environment.NewLine}" &
                                      $"  M → Mora{Environment.NewLine}  P → Pagado"

            Dim entrada As String = Microsoft.VisualBasic.InputBox(mensaje, "Cambiar Estado", actual)
            entrada = entrada.Trim()

            If String.IsNullOrWhiteSpace(entrada) Then Return

            ' Aceptar inicial o nombre completo
            Dim mapa As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase) From {
                {"A", EstadoDeuda.Activo}, {"Activo", EstadoDeuda.Activo},
                {"M", EstadoDeuda.Mora},   {"Mora",   EstadoDeuda.Mora},
                {"P", EstadoDeuda.Pagado}, {"Pagado", EstadoDeuda.Pagado}
            }

            If Not mapa.ContainsKey(entrada) Then
                MessageBox.Show("Valor inválido. Ingresa A, M o P.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Try
                _servicio.CambiarEstado(_deudaSeleccionada.IdDeuda, mapa(entrada))
                CargarResumenes()
                Dim actualizado As ResumenDeuda =
                    _servicio.ObtenerResumenes().
                    FirstOrDefault(Function(x) x.IdDeuda = _deudaSeleccionada.IdDeuda)
                If actualizado IsNot Nothing Then
                    _deudaSeleccionada = actualizado
                    CargarDetalleDerecho(actualizado)
                End If
            Catch ex As Exception
                MessageBox.Show($"Error al cambiar estado:{Environment.NewLine}{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region "Formulario nueva deuda"

        Private Sub BtnGuardarDeuda_Click(sender As Object, e As EventArgs) Handles BtnGuardarDeuda.Click
            Try
                Dim d As New Deuda()
                d.Nombre       = TxtNombreDeuda.Text.Trim()
                d.Acreedor     = TxtAcreedor.Text.Trim()
                d.MontoTotal   = NumMontoDeuda.Value
                d.TasaInteres  = NumTasa.Value
                d.PagoMinimo   = NumPagoMinimo.Value
                d.FechaInicio  = DtpInicioDeuda.Value.Date
                d.FechaVencimiento = If(ChkSinVencimiento.Checked,
                    CType(Nothing, Date?), DtpVencimiento.Value.Date)
                d.Estado = EstadoDeuda.Activo

                _servicio.GuardarDeuda(d)
                MessageBox.Show("Deuda guardada correctamente.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                PnlFormDeuda.Visible = False
                CargarResumenes()

            Catch ex As ArgumentException
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                MessageBox.Show($"Error al guardar:{Environment.NewLine}{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub BtnCancelarDeuda_Click(sender As Object, e As EventArgs) Handles BtnCancelarDeuda.Click
            PnlFormDeuda.Visible = False
            PnlDetalle.Visible   = (_deudaSeleccionada IsNot Nothing)
        End Sub

        Private Sub ChkSinVencimiento_CheckedChanged(sender As Object, e As EventArgs) _
            Handles ChkSinVencimiento.CheckedChanged
            DtpVencimiento.Enabled = Not ChkSinVencimiento.Checked
        End Sub

        Private Sub LimpiarFormDeuda()
            TxtNombreDeuda.Text       = String.Empty
            TxtAcreedor.Text          = String.Empty
            NumMontoDeuda.Value       = 0
            NumTasa.Value             = 0
            NumPagoMinimo.Value       = 0
            DtpInicioDeuda.Value      = Date.Today
            ChkSinVencimiento.Checked = True
            DtpVencimiento.Enabled    = False
        End Sub

#End Region

#Region "Eliminar pago"

        Private Sub BtnEliminarPago_Click(sender As Object, e As EventArgs) Handles BtnEliminarPago.Click
            If GridPagos.SelectedRows.Count = 0 Then Return

            Dim conf As DialogResult = MessageBox.Show(
                "¿Eliminar este pago? El saldo de la deuda se recalculará.",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If conf = DialogResult.Yes Then
                Try
                    Dim idPago As Integer = Convert.ToInt32(
                        GridPagos.SelectedRows(0).Cells("ColPagoId").Value)
                    _servicio.EliminarPago(idPago)
                    CargarResumenes()
                    Dim actualizado As ResumenDeuda =
                        _servicio.ObtenerResumenes().
                        FirstOrDefault(Function(x) x.IdDeuda = _deudaSeleccionada.IdDeuda)
                    If actualizado IsNot Nothing Then
                        _deudaSeleccionada = actualizado
                        CargarDetalleDerecho(actualizado)
                    End If
                Catch ex As Exception
                    MessageBox.Show($"Error al eliminar pago:{Environment.NewLine}{ex.Message}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Sub

#End Region

    End Class

End Namespace
