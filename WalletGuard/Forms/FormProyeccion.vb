Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports DL_WalletGuard.WalletGuard.Entidades
Imports BL_WalletGuard.WalletGuard.LogicaNegocio

Namespace WalletGuard.Presentacion

    ''' <summary>
    ''' LÓGICA de FormProyeccion.
    ''' Vista unificada: 3 meses reales + 3 meses proyectados.
    ''' Gráfico GDI+ con zona proyectada sombreada + tabla comparativa.
    ''' Los controles están en FormProyeccion.Designer.vb.
    ''' </summary>
    Partial Public Class FormProyeccion
        Inherits Form

#Region "Campos"

        Private ReadOnly _servicio As FlujoCajaService
        Private _datos             As List(Of LineaProyeccion)

        Private ReadOnly ClrReal      As Color = Color.FromArgb(60, 180, 100)
        Private ReadOnly ClrProyeccion As Color = Color.FromArgb(80, 140, 220)
        Private ReadOnly ClrEgresos   As Color = Color.FromArgb(200, 60, 60)
        Private ReadOnly ClrZona      As Color = Color.FromArgb(30, 80, 140, 220)  ' semitransparente

#End Region

#Region "Constructor"

        Public Sub New()
            InitializeComponent()
            _servicio = New FlujoCajaService()
        End Sub

#End Region

#Region "Carga inicial"

        Private Sub FormProyeccion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            CargarDatos()
        End Sub

        Private Sub BtnRefrescar_Click(sender As Object, e As EventArgs) Handles BtnRefrescar.Click
            CargarDatos()
        End Sub

        Private Sub CargarDatos()
            Try
                _datos = _servicio.ObtenerVistaUnificada()
                CargarGrid()
                CargarResumen()
                PnlGrafico.Invalidate()
            Catch ex As Exception
                MessageBox.Show($"Error al cargar proyección:{Environment.NewLine}{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region "Grid"

        Private Sub CargarGrid()
            GridProyeccion.Rows.Clear()

            For Each linea As LineaProyeccion In _datos
                Dim tipoStr As String = If(linea.EsProyeccion, "📈 Proyección", "✅ Real")
                GridProyeccion.Rows.Add(
                    tipoStr,
                    linea.Periodo,
                    linea.IngresosProyectados.ToString("C2"),
                    linea.EgresosProyectados.ToString("C2"),
                    If(linea.RecurrentesActivos > 0, linea.RecurrentesActivos.ToString("C2"), "-"),
                    linea.BalanceProyectado.ToString("C2")
                )

                Dim rowIdx As Integer = GridProyeccion.Rows.Count - 1

                ' Filas proyectadas con color diferente
                If linea.EsProyeccion Then
                    GridProyeccion.Rows(rowIdx).DefaultCellStyle.BackColor = Color.FromArgb(26, 38, 65)
                    GridProyeccion.Rows(rowIdx).DefaultCellStyle.ForeColor = Color.FromArgb(140, 180, 240)
                End If

                ' Color del balance
                GridProyeccion.Rows(rowIdx).Cells("ColProjBalance").Style.ForeColor =
                    If(linea.BalanceProyectado >= 0,
                       Color.FromArgb(80, 210, 120),
                       Color.FromArgb(210, 80, 80))
            Next
        End Sub

#End Region

#Region "Resumen de proyección"

        Private Sub CargarResumen()
            If _datos Is Nothing OrElse _datos.Count = 0 Then Return

            Dim reales      As List(Of LineaProyeccion) = _datos.Where(Function(x) Not x.EsProyeccion).ToList()
            Dim proyectadas As List(Of LineaProyeccion) = _datos.Where(Function(x) x.EsProyeccion).ToList()

            Dim promedioIngReal As Decimal = If(reales.Count > 0, reales.Average(Function(x) x.IngresosProyectados), 0)
            Dim promedioEgrReal As Decimal = If(reales.Count > 0, reales.Average(Function(x) x.EgresosProyectados), 0)
            Dim balanceProxTrim As Decimal = If(proyectadas.Count > 0, proyectadas.Sum(Function(x) x.BalanceProyectado), 0)

            LblResumenProyeccion.Text =
                $"Promedio real últimos {reales.Count} meses →  " &
                $"Ingresos: {promedioIngReal:C2}  |  Egresos: {promedioEgrReal:C2}" &
                $"     ┊     " &
                $"Balance proyectado próximos {proyectadas.Count} meses: {balanceProxTrim:C2}"

            LblResumenProyeccion.ForeColor = If(balanceProxTrim >= 0,
                Color.FromArgb(80, 210, 120),
                Color.FromArgb(210, 80, 80))

            ' Alerta si el balance proyectado es negativo
            PnlAlerta.Visible = (balanceProxTrim < 0)
            If balanceProxTrim < 0 Then
                LblAlerta.Text = $"⚠  Atención: Se proyecta un déficit de {Math.Abs(balanceProxTrim):C2} " &
                                 "en los próximos meses. Considera reducir egresos o incrementar ingresos."
            End If
        End Sub

#End Region

#Region "Gráfico GDI+"

        ''' <summary>
        ''' Gráfico de líneas con zona sombreada para los meses proyectados.
        ''' Línea verde = ingresos reales/proyectados.
        ''' Línea roja  = egresos reales/proyectados.
        ''' Fondo azul semitransparente = zona de proyección.
        ''' Línea vertical punteada = separador real vs proyectado.
        ''' </summary>
        Private Sub PnlGrafico_Paint(sender As Object, e As PaintEventArgs) Handles PnlGrafico.Paint
            If _datos Is Nothing OrElse _datos.Count = 0 Then Return

            Dim g As Graphics = e.Graphics
            g.SmoothingMode     = SmoothingMode.AntiAlias
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit

            Dim w    As Integer = PnlGrafico.Width  - 60
            Dim h    As Integer = PnlGrafico.Height - 46
            Dim offX As Integer = 50
            Dim offY As Integer = 10

            Dim maxVal As Decimal = _datos.Max(Function(x) Math.Max(x.IngresosProyectados, x.EgresosProyectados))
            If maxVal = 0 Then maxVal = 1

            Dim n      As Integer = _datos.Count
            Dim groupW As Integer = w \ Math.Max(n, 1)

            ' Sombreado zona proyectada
            Dim firstProyIdx As Integer = _datos.IndexOf(_datos.FirstOrDefault(Function(x) x.EsProyeccion))
            If firstProyIdx >= 0 Then
                Dim zonaX As Integer = offX + firstProyIdx * groupW
                Using br As New SolidBrush(ClrZona)
                    g.FillRectangle(br, zonaX, offY, offX + w - zonaX, h)
                End Using
                ' Etiqueta "Proyección"
                Using fnt As New Font("Segoe UI", 8.0F, FontStyle.Italic)
                Using br  As New SolidBrush(Color.FromArgb(100, 150, 210))
                    g.DrawString("← Proyección", fnt, br, zonaX + 4, offY + 4)
                End Using
                End Using
                ' Línea separadora
                Using pen As New Pen(Color.FromArgb(80, 120, 200), 1)
                    pen.DashStyle = DashStyle.Dash
                    g.DrawLine(pen, zonaX, offY, zonaX, offY + h)
                End Using
            End If

            ' Líneas de referencia
            Using penGrid As New Pen(Color.FromArgb(38, 48, 72))
            Using fntS As New Font("Segoe UI", 7.0F)
            Using brL  As New SolidBrush(Color.FromArgb(100, 115, 145))
                For i As Integer = 0 To 3
                    Dim gy  As Integer = offY + CInt(h * i / 3)
                    Dim val As Decimal = maxVal - (maxVal * i / 3)
                    g.DrawLine(penGrid, offX, gy, offX + w, gy)
                    Dim lbl As String = val.ToString("C0")
                    Dim sz  As SizeF  = g.MeasureString(lbl, fntS)
                    g.DrawString(lbl, fntS, brL, offX - sz.Width - 4, gy - sz.Height / 2)
                Next
            End Using
            End Using
            End Using

            ' Puntos para las líneas
            Dim ptsIng As New List(Of PointF)()
            Dim ptsEgr As New List(Of PointF)()

            For i As Integer = 0 To n - 1
                Dim d  As LineaProyeccion = _datos(i)
                Dim cx As Single = offX + i * groupW + groupW \ 2
                Dim cyIng As Single = offY + h - CSng(h * CDbl(d.IngresosProyectados) / CDbl(maxVal))
                Dim cyEgr As Single = offY + h - CSng(h * CDbl(d.EgresosProyectados) / CDbl(maxVal))
                ptsIng.Add(New PointF(cx, cyIng))
                ptsEgr.Add(New PointF(cx, cyEgr))

                ' Etiqueta mes
                Using fnt As New Font("Segoe UI", 7.0F)
                Using br  As New SolidBrush(Color.FromArgb(110, 125, 155))
                    Dim lbl As String = d.Periodo.Substring(0, Math.Min(3, d.Periodo.Length)).ToUpper()
                    Dim sz  As SizeF  = g.MeasureString(lbl, fnt)
                    g.DrawString(lbl, fnt, br, cx - sz.Width / 2, offY + h + 4)
                End Using
                End Using
            Next

            ' Línea ingresos
            If ptsIng.Count > 1 Then
                Using pen As New Pen(ClrReal, 2)
                    g.DrawLines(pen, ptsIng.ToArray())
                End Using
            End If
            For Each pt As PointF In ptsIng
                Using br As New SolidBrush(ClrReal)
                    g.FillEllipse(br, pt.X - 3, pt.Y - 3, 7, 7)
                End Using
            Next

            ' Línea egresos
            If ptsEgr.Count > 1 Then
                Using pen As New Pen(ClrEgresos, 2)
                    g.DrawLines(pen, ptsEgr.ToArray())
                End Using
            End If
            For Each pt As PointF In ptsEgr
                Using br As New SolidBrush(ClrEgresos)
                    g.FillEllipse(br, pt.X - 3, pt.Y - 3, 7, 7)
                End Using
            Next

            ' Leyenda
            DibujarLeyenda(g, offX + w - 280, offY + 4)
        End Sub

        Private Sub DibujarLeyenda(g As Graphics, x As Integer, y As Integer)
            Dim items As (Color, String)() = {
                (ClrReal,       "Ingresos"),
                (ClrEgresos,    "Egresos"),
                (ClrProyeccion, "Zona proyectada")
            }
            Using fnt As New Font("Segoe UI", 7.5F)
            Using brTxt As New SolidBrush(Color.FromArgb(180, 190, 215))
                For i As Integer = 0 To items.Length - 1
                    Dim cx As Integer = x + i * 110
                    Using br As New SolidBrush(items(i).Item1)
                        g.FillRectangle(br, cx, y + 2, 10, 10)
                    End Using
                    g.DrawString(items(i).Item2, fnt, brTxt, cx + 14, y)
                Next
            End Using
            End Using
        End Sub

#End Region

    End Class

End Namespace
