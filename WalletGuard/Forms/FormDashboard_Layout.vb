Imports System.Drawing
Imports System.Windows.Forms

Namespace WalletGuard.Presentacion

    ''' <summary>
    ''' Extensión parcial de FormDashboard para manejar el layout responsivo.
    ''' Separado para no mezclar con la lógica de negocio ni con el Designer.
    ''' </summary>
    Partial Public Class FormDashboard

        Private Sub FormDashboard_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
            AjustarLayout()
        End Sub

        Private Sub FormDashboard_Layout(sender As Object, e As LayoutEventArgs) Handles MyBase.Layout
            AjustarLayout()
        End Sub

        ''' <summary>
        ''' Distribuye los paneles según el tamaño actual del formulario.
        ''' Panel izquierdo = calendario (~62%).  Panel derecho = gráficos (~36%).
        ''' </summary>
        Private Sub AjustarLayout()
            Dim w As Integer = Me.ClientSize.Width - 40   ' descontar Padding L+R
            Dim h As Integer = Me.ClientSize.Height - 32   ' descontar Padding T+B

            If w <= 0 OrElse h <= 0 Then Return

            ' ── Tarjetas de resumen ───────────────────────────────────────────
            Dim tarjetaW As Integer = Math.Min(190, (w - 260) \ 3)
            tarjetaW = Math.Max(140, tarjetaW)
            PnlTarjetas.Width = tarjetaW * 3 + 24
            PnlTarjetas.Height = 76

            PnlCardIngresos.Width = tarjetaW
            PnlCardEgresos.Width = tarjetaW
            PnlCardBalance.Width = tarjetaW
            PnlCardEgresos.Left = tarjetaW + 12
            PnlCardBalance.Left = (tarjetaW + 12) * 2

            ' ── Panel derecho (gráficos) ──────────────────────────────────────
            Dim derechoW As Integer = Math.Max(230, Math.Min(280, w - PnlTarjetas.Width - 20))
            Dim derechoTop As Integer = 110
            Dim derechoH As Integer = h - derechoTop

            Me.PnlDerecho.Location = New Point(w - derechoW, derechoTop)
            Me.PnlDerecho.Size = New Size(derechoW, derechoH)

            ' Donut
            Dim donutH As Integer = Math.Min(175, derechoH \ 2 - 30)
            Me.PnlDonut.Size = New Size(derechoW - 4, donutH)

            ' Barras
            Me.LblTituloBarras.Location = New Point(0, donutH + 30)
            Me.PnlBarras.Location = New Point(0, donutH + 50)
            Me.PnlBarras.Size = New Size(derechoW - 4, derechoH - donutH - 55)

            ' ── Panel izquierdo (calendario) ──────────────────────────────────
            Dim izqW As Integer = w - derechoW - 16
            Dim izqH As Integer = h - 110
            Me.PnlIzquierdo.Location = New Point(0, 110)
            Me.PnlIzquierdo.Size = New Size(izqW, izqH)

            ' Ajustar ancho del LblMesAnio según espacio disponible
            Me.LblMesAnio.Size = New Size(izqW - 80, 36)
            Me.BtnMesSiguiente.Left = Me.LblMesAnio.RightToLeft

            ' Ajustar días de la semana
            Dim cellW As Integer = Math.Max(40, (izqW - 8) \ 7)
            For i As Integer = 0 To PnlDiasSemana.Controls.Count - 1
                PnlDiasSemana.Controls(i).Bounds = New Rectangle(i * cellW + 4, 5, cellW - 2, 20)
            Next

            ' Actualizar tamaño de celdas del FlowPanel
            For Each ctrl As Control In FlowDias.Controls
                ctrl.Size = New Size(cellW, 72)
            Next
        End Sub

    End Class

End Namespace
