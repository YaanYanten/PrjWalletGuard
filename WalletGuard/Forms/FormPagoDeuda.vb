Imports System.Windows.Forms
Imports DL_WalletGuard.WalletGuard.Entidades
Imports BL_WalletGuard.WalletGuard.LogicaNegocio

Namespace WalletGuard.Presentacion

    ''' <summary>
    ''' LÓGICA de FormPagoDeuda.
    ''' Diálogo modal que se abre desde FormDeudas al pulsar "Registrar Pago".
    ''' Registra un abono contra una deuda específica, calculando el desglose
    ''' entre capital e interés en función de la tasa mensual de la deuda.
    ''' Los controles están en FormPagoDeuda.Designer.vb.
    ''' </summary>
    Partial Public Class FormPagoDeuda
        Inherits Form

#Region "Campos"

        Private ReadOnly _idDeuda As Integer
        Private ReadOnly _nombreDeuda As String
        Private ReadOnly _saldoActual As Decimal
        Private ReadOnly _tasaInteresMensual As Decimal   ' tasa mensual (%) para calcular interés
        Private ReadOnly _servicio As DeudaService

#End Region

#Region "Constructor"

        Public Sub New(idDeuda As Integer, nombreDeuda As String, saldoActual As Decimal,
                       Optional tasaInteresMensual As Decimal = 0)
            InitializeComponent()
            _idDeuda            = idDeuda
            _nombreDeuda        = nombreDeuda
            _saldoActual        = saldoActual
            _tasaInteresMensual = tasaInteresMensual
            _servicio           = New DeudaService()
        End Sub

#End Region

#Region "Carga inicial"

        Private Sub FormPagoDeuda_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            LblNombreDeuda.Text = _nombreDeuda
            LblSaldoActual.Text = $"Saldo actual: {_saldoActual:C2}"
            DtpFechaPago.Value  = Date.Today
            NumMontoPago.Maximum = _saldoActual

            ' Mostrar interés estimado del primer pago si hay tasa
            If _tasaInteresMensual > 0 Then
                Dim interes As Decimal = Math.Round(_saldoActual * (_tasaInteresMensual / 100D), 2)
                LblSaldoNuevo.Text    = $"Interés estimado este mes: {interes:C2}"
                LblSaldoNuevo.Visible = True
            End If
        End Sub

#End Region

#Region "Guardar pago"

        Private Sub BtnGuardarPago_Click(sender As Object, e As EventArgs) Handles BtnGuardarPago.Click
            If NumMontoPago.Value <= 0 Then
                LblErrorPago.Text    = "⚠  El monto debe ser mayor a cero."
                LblErrorPago.Visible = True
                NumMontoPago.Focus()
                Return
            End If

            Try
                Dim montoPago As Decimal = NumMontoPago.Value

                ' ── Desglose interés / capital ───────────────────────────────
                ' Interés del período = saldo * tasa mensual
                ' Capital = monto pagado − interés (mínimo 0)
                Dim interesPagado As Decimal = 0D
                If _tasaInteresMensual > 0 Then
                    interesPagado = Math.Round(_saldoActual * (_tasaInteresMensual / 100D), 2)
                    ' Si el pago no cubre ni el interés, todo va a interés
                    If interesPagado > montoPago Then interesPagado = montoPago
                End If
                Dim capitalPagado As Decimal = montoPago - interesPagado

                Dim pago As New PagoDeuda()
                pago.IdDeuda        = _idDeuda
                pago.Fecha          = DtpFechaPago.Value.Date
                pago.Monto          = montoPago
                pago.interes_pagado = interesPagado
                pago.capital_pagado = capitalPagado

                _servicio.RegistrarPago(pago)

                Me.DialogResult = DialogResult.OK
                Me.Close()

            Catch ex As Exception
                LblErrorPago.Text    = $"⚠  {ex.Message}"
                LblErrorPago.Visible = True
            End Try
        End Sub

        Private Sub BtnCancelarPago_Click(sender As Object, e As EventArgs) Handles BtnCancelarPago.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub NumMontoPago_ValueChanged(sender As Object, e As EventArgs) Handles NumMontoPago.ValueChanged
            LblErrorPago.Visible = False
            If NumMontoPago.Value <= 0 Then
                LblSaldoNuevo.Visible = False
                Return
            End If

            Dim montoPago     As Decimal = NumMontoPago.Value
            Dim saldoNuevo    As Decimal = Math.Max(0D, _saldoActual - montoPago)

            If _tasaInteresMensual > 0 Then
                Dim interes  As Decimal = Math.Round(_saldoActual * (_tasaInteresMensual / 100D), 2)
                If interes > montoPago Then interes = montoPago
                Dim capital  As Decimal = montoPago - interes
                LblSaldoNuevo.Text = $"Saldo nuevo: {saldoNuevo:C2}  |  Capital: {capital:C2}  |  Interés: {interes:C2}"
            Else
                LblSaldoNuevo.Text = $"Saldo después del pago: {saldoNuevo:C2}"
            End If
            LblSaldoNuevo.Visible = True
        End Sub

#End Region

    End Class

End Namespace
