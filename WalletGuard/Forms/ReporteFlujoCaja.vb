Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports DL_WalletGuard.WalletGuard.Entidades
Imports BL_WalletGuard.WalletGuard.LogicaNegocio

Namespace WalletGuard.Presentacion

    ''' <summary>
    ''' LÓGICA del Reporte de Flujo de Caja.
    ''' Muestra: tabla mes a mes con Ingresos / Egresos / Balance / Acumulado
    ''' + gráfico de barras apiladas GDI+ puro.
    ''' Los controles están en ReporteFlujoCaja.Designer.vb.
    ''' </summary>
    Partial Public Class ReporteFlujoCaja
        Inherits Form

#Region "Campos"

        Private ReadOnly _servicio    As FlujoCajaService
        Private _datosCargados        As List(Of LineaFlujoCaja)

        ' Colores del gráfico
        Private ReadOnly ClrIngresos  As Color = Color.FromArgb(60, 180, 100)
        Private ReadOnly ClrEgresos   As Color = Color.FromArgb(200, 60, 60)
        Private ReadOnly ClrBalance   As Color = Color.FromArgb(80, 160, 240)
        Private ReadOnly ClrAcum      As Color = Color.FromArgb(240, 180, 0)

#End Region

#Region "Constructor"

        Public Sub New()
            InitializeComponent()
            _servicio = New FlujoCajaService()
        End Sub

#End Region

#Region "Carga inicial"

        Private Sub ReporteFlujoCaja_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            ' Rango por defecto: enero del año actual hasta mes actual
            DtpDesde.Value = New Date(Date.Today.Year, 1, 1)
            DtpHasta.Value = Date.Today
            EjecutarReporte()
        End Sub

#End Region

#Region "Ejecutar reporte"

        Private Sub BtnFiltrar_Click(sender As Object, e As EventArgs) Handles BtnFiltrar.Click
            EjecutarReporte()
        End Sub

        Private Sub EjecutarReporte()
            Try
                Dim desde As Date = DtpDesde.Value
                Dim hasta As Date = DtpHasta.Value

                If desde > hasta Then
                    MessageBox.Show("La fecha de inicio no puede ser posterior a la de fin.",
                                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                _datosCargados = _servicio.ObtenerFlujo(
                    desde.Year, desde.Month, hasta.Year, hasta.Month)

                CargarGrid()
                ActualizarTotales()
                PnlGrafico.Invalidate()

            Catch ex As Exception
                MessageBox.Show($"Error al generar el reporte:{Environment.NewLine}{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region "Grid"

        Private Sub CargarGrid()
            GridFlujo.Rows.Clear()

            For Each linea As LineaFlujoCaja In _datosCargados
                Dim balanceColor As String = If(linea.Balance >= 0, "▲", "▼")
                Dim acumColor    As String = If(linea.BalanceAcumulado >= 0, "▲", "▼")

                GridFlujo.Rows.Add(
                    linea.Periodo,
                    linea.TotalIngresos.ToString("C2"),
                    linea.TotalEgresos.ToString("C2"),
                    $"{balanceColor} {linea.Balance:C2}",
                    $"{acumColor} {linea.BalanceAcumulado:C2}"
                )

                ' Colorear la celda de balance
                Dim rowIdx As Integer = GridFlujo.Rows.Count - 1
                GridFlujo.Rows(rowIdx).Cells("ColBalance").Style.ForeColor =
                    If(linea.Balance >= 0, Color.FromArgb(80, 210, 120), Color.FromArgb(210, 80, 80))
                GridFlujo.Rows(rowIdx).Cells("ColAcumulado").Style.ForeColor =
                    If(linea.BalanceAcumulado >= 0, Color.FromArgb(240, 180, 0), Color.FromArgb(210, 80, 80))
            Next
        End Sub

#End Region

#Region "Totales del período"

        Private Sub ActualizarTotales()
            If _datosCargados Is Nothing OrElse _datosCargados.Count = 0 Then
                LblResumen.Text = "Sin datos para el período seleccionado."
                Return
            End If

            Dim totalIng  As Decimal = _datosCargados.Sum(Function(x) x.TotalIngresos)
            Dim totalEgr  As Decimal = _datosCargados.Sum(Function(x) x.TotalEgresos)
            Dim balance   As Decimal = _datosCargados.Last().BalanceAcumulado
            Dim meses     As Integer = _datosCargados.Count

            Dim promedioIng As Decimal = If(meses > 0, totalIng / meses, 0)
            Dim promedioEgr As Decimal = If(meses > 0, totalEgr / meses, 0)

            LblResumen.Text =
                $"Período: {meses} meses   |   " &
                $"Ingresos totales: {totalIng:C2}   |   " &
                $"Egresos totales: {totalEgr:C2}   |   " &
                $"Balance acumulado: {balance:C2}   |   " &
                $"Promedio mensual: Ing {promedioIng:C2}  /  Egr {promedioEgr:C2}"

            LblResumen.ForeColor = If(balance >= 0,
                Color.FromArgb(80, 210, 120),
                Color.FromArgb(210, 80, 80))
        End Sub

#End Region

#Region "Gráfico GDI+"

        ''' <summary>
        ''' Dibuja el gráfico de barras agrupadas + línea de balance acumulado.
        ''' Ingresos = barras verdes, Egresos = barras rojas,
        ''' Balance acumulado = línea amarilla con puntos.
        ''' </summary>
        Private Sub PnlGrafico_Paint(sender As Object, e As PaintEventArgs) Handles PnlGrafico.Paint
            If _datosCargados Is Nothing OrElse _datosCargados.Count = 0 Then
                DibujarMensajeVacio(e.Graphics)
                Return
            End If

            Dim g As Graphics = e.Graphics
            g.SmoothingMode     = SmoothingMode.AntiAlias
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit

            Dim w       As Integer = PnlGrafico.Width  - 60
            Dim h       As Integer = PnlGrafico.Height - 50
            Dim offX    As Integer = 50
            Dim offY    As Integer = 10

            ' Calcular máximo para escalar
            Dim maxVal As Decimal = _datosCargados.Max(Function(x) Math.Max(x.TotalIngresos, x.TotalEgresos))
            Dim minAcum As Decimal = _datosCargados.Min(Function(x) x.BalanceAcumulado)
            Dim maxAcum As Decimal = _datosCargados.Max(Function(x) x.BalanceAcumulado)
            Dim rango As Decimal = Math.Max(maxVal, Math.Abs(minAcum))
            If rango = 0 Then rango = 1

            ' Líneas horizontales de referencia con etiquetas
            Using penGrid As New Pen(Color.FromArgb(38, 48, 72))
            Using fntSmall As New Font("Segoe UI", 7.0F)
            Using brLabel As New SolidBrush(Color.FromArgb(100, 115, 145))
                For i As Integer = 0 To 4
                    Dim gy  As Integer = offY + CInt(h * i / 4)
                    Dim val As Decimal = rango - (rango * 2 * i / 4)
                    g.DrawLine(penGrid, offX, gy, offX + w, gy)
                    Dim lbl As String = val.ToString("C0")
                    Dim sz  As SizeF  = g.MeasureString(lbl, fntSmall)
                    g.DrawString(lbl, fntSmall, brLabel, offX - sz.Width - 4, gy - sz.Height / 2)
                Next
            End Using
            End Using
            End Using

            ' Línea cero
            Dim ceroY As Integer = offY + CInt(h * CDbl(rango) / CDbl(rango * 2))
            Using penCero As New Pen(Color.FromArgb(60, 70, 100), 1)
                penCero.DashStyle = DashStyle.Dash
                g.DrawLine(penCero, offX, ceroY, offX + w, ceroY)
            End Using

            ' Barras
            Dim n       As Integer = _datosCargados.Count
            Dim groupW  As Integer = w \ Math.Max(n, 1)
            Dim barW    As Integer = Math.Max(4, Math.Min(22, groupW \ 3))

            For i As Integer = 0 To n - 1
                Dim d   As LineaFlujoCaja = _datosCargados(i)
                Dim cx  As Integer = offX + i * groupW + groupW \ 2

                ' Barra ingresos
                Dim hIng As Integer = CInt(h * CDbl(d.TotalIngresos) / CDbl(rango * 2))
                Using br As New SolidBrush(ClrIngresos)
                    g.FillRectangle(br, cx - barW - 1, ceroY - hIng, barW, hIng)
                End Using

                ' Barra egresos
                Dim hEgr As Integer = CInt(h * CDbl(d.TotalEgresos) / CDbl(rango * 2))
                Using br As New SolidBrush(ClrEgresos)
                    g.FillRectangle(br, cx + 1, ceroY - hEgr, barW, hEgr)
                End Using

                ' Etiqueta mes
                Using fnt As New Font("Segoe UI", 7.0F)
                Using br  As New SolidBrush(Color.FromArgb(110, 125, 155))
                    Dim lbl As String = d.Periodo.Substring(0, Math.Min(3, d.Periodo.Length)).ToUpper()
                    Dim sz  As SizeF  = g.MeasureString(lbl, fnt)
                    g.DrawString(lbl, fnt, br, cx - sz.Width / 2, offY + h + 4)
                End Using
                End Using
            Next

            ' Línea de balance acumulado (amarilla)
            Dim ptsAcum As New List(Of PointF)()
            For i As Integer = 0 To n - 1
                Dim d  As LineaFlujoCaja = _datosCargados(i)
                Dim cx As Single = offX + i * groupW + groupW \ 2
                Dim norm As Double = CDbl(rango + d.BalanceAcumulado) / CDbl(rango * 2)
                Dim cy   As Single = offY + h - CSng(h * norm)
                ptsAcum.Add(New PointF(cx, cy))
            Next

            If ptsAcum.Count > 1 Then
                Using pen As New Pen(ClrAcum, 2)
                    g.DrawLines(pen, ptsAcum.ToArray())
                End Using
            End If
            For Each pt As PointF In ptsAcum
                Using br As New SolidBrush(ClrAcum)
                    g.FillEllipse(br, pt.X - 3, pt.Y - 3, 7, 7)
                End Using
            Next

            ' Leyenda
            DibujarLeyenda(g, offX + w - 300, offY + 4)
        End Sub

        Private Sub DibujarLeyenda(g As Graphics, x As Integer, y As Integer)
            Dim items As (Color, String)() = {
                (ClrIngresos, "Ingresos"),
                (ClrEgresos,  "Egresos"),
                (ClrAcum,     "Acumulado")
            }
            Using fnt As New Font("Segoe UI", 7.5F)
            Using brTxt As New SolidBrush(Color.FromArgb(180, 190, 215))
                For i As Integer = 0 To items.Length - 1
                    Dim cx As Integer = x + i * 105
                    Using br As New SolidBrush(items(i).Item1)
                        g.FillRectangle(br, cx, y + 2, 10, 10)
                    End Using
                    g.DrawString(items(i).Item2, fnt, brTxt, cx + 14, y)
                Next
            End Using
            End Using
        End Sub

        Private Sub DibujarMensajeVacio(g As Graphics)
            Using br  As New SolidBrush(Color.FromArgb(80, 90, 115))
            Using fnt As New Font("Segoe UI", 10.0F)
                Dim msg As String = "Aplica el filtro para ver el gráfico"
                Dim sz  As SizeF  = g.MeasureString(msg, fnt)
                g.DrawString(msg, fnt, br,
                    (PnlGrafico.Width - sz.Width) / 2,
                    (PnlGrafico.Height - sz.Height) / 2)
            End Using
            End Using
        End Sub

#End Region

    End Class

End Namespace
