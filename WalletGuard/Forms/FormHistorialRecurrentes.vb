Imports DL_WalletGuard.WalletGuard.AccesoDatos
Imports System.Windows.Forms
Imports DL_WalletGuard.WalletGuard.Entidades

Namespace WalletGuard.Presentacion

    ''' <summary>
    ''' LÓGICA del formulario de Historial de Egresos Recurrentes.
    ''' Muestra todos los egresos generados automáticamente.
    ''' Los controles están declarados en FormHistorialRecurrentes.Designer.vb.
    ''' </summary>
    Partial Public Class FormHistorialRecurrentes
        Inherits Form

#Region "Campos"

        Private ReadOnly _egresoDAL As EgresoDAL

#End Region

#Region "Constructor"

        Public Sub New()
            InitializeComponent()
            _egresoDAL = New EgresoDAL()
        End Sub

#End Region

#Region "Eventos"

        Private Sub FormHistorialRecurrentes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            CargarHistorial()
        End Sub

        Private Sub BtnRefrescar_Click(sender As Object, e As EventArgs) Handles BtnRefrescar.Click
            CargarHistorial()
        End Sub

#End Region

#Region "Carga de datos"

        Private Sub CargarHistorial()
            Try
                Dim lista As List(Of Egreso) = _egresoDAL.ObtenerRecurrentes()
                GridHistorial.Rows.Clear()

                Dim totalAcumulado As Decimal = 0

                For Each eg As Egreso In lista
                    GridHistorial.Rows.Add(
                        eg.IdEgreso,
                        eg.Fecha.ToString("dd/MM/yyyy"),
                        eg.Monto.ToString("C2"),
                        eg.IdRecurrente?.ToString(),
                        eg.Descripcion
                    )
                    totalAcumulado += eg.Monto
                Next

                LblTotalRegistros.Text = $"Registros: {lista.Count}   |   Total acumulado: {totalAcumulado:C2}"

            Catch ex As Exception
                MessageBox.Show($"Error al cargar el historial:{Environment.NewLine}{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

    End Class

End Namespace
