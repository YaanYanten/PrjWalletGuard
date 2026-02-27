Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports DL_WalletGuard.WalletGuard.AccesoDatos
Imports DL_WalletGuard.WalletGuard.Entidades

Namespace WalletGuard.Presentacion
    Partial Public Class FormDashboard
        Inherits Form

#Region "Campos de estado"

        ' Mes/año actualmente visible en el calendario
        Private _mesActual As Integer
        Private _anioActual As Integer

        ' Cache de movimientos del mes para no releer en cada repaint
        Private _movimientosMes As List(Of MovimientoCalendario)

        ' DALs
        Private ReadOnly _ingresoDAL As IngresoDAL
        Private ReadOnly _egresoDAL As EgresoDAL

        ' Colores fijos de los puntos del calendario
        Private ReadOnly ColorIngreso As Color = Color.FromArgb(80, 200, 120)   ' verde
        Private ReadOnly ColorEgreso As Color = Color.FromArgb(220, 70, 70)    ' rojo
        Private ReadOnly ColorRecurrente As Color = Color.FromArgb(240, 180, 0)  ' amarillo

#End Region

#Region "Constructor"

        Public Sub New()
            InitializeComponent()
            _ingresoDAL = New IngresoDAL()
            _egresoDAL = New EgresoDAL()
        End Sub

#End Region

#Region "Carga inicial"

        Private Sub FormDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            _mesActual = Date.Today.Month
            _anioActual = Date.Today.Year
            RefrescarTodo()
        End Sub

        ''' <summary>Recarga tarjetas, calendario y gráficos para el mes actual.</summary>
        Public Sub RefrescarTodo()
            CargarMovimientosMes()
            ActualizarTarjetas()
            DibujarCalendario()
            PnlDonut.Invalidate()
            PnlBarras.Invalidate()
        End Sub

#End Region

#Region "Tarjetas de resumen"

        Private Sub ActualizarTarjetas()
            Try
                Dim ingresos As Decimal = _ingresoDAL.ObtenerTotalMes(_mesActual, _anioActual)
                Dim egresos As Decimal = _egresoDAL.ObtenerTotalMes(_mesActual, _anioActual)
                Dim balance As Decimal = ingresos - egresos

                LblMontoIngresos.Text = ingresos.ToString("C0")
                LblMontoEgresos.Text = egresos.ToString("C0")
                LblMontoBalance.Text = balance.ToString("C0")

                ' Color del balance según positivo/negativo
                LblMontoBalance.ForeColor = If(balance >= 0,
                    Color.FromArgb(80, 220, 130),
                    Color.FromArgb(220, 80, 80))

            Catch ex As Exception
                LblMontoIngresos.Text = "$0"
                LblMontoEgresos.Text = "$0"
                LblMontoBalance.Text = "$0"
            End Try
        End Sub

#End Region

#Region "Calendario"

        ''' <summary>Carga los movimientos del mes visible desde la BD.</summary>
        Private Sub CargarMovimientosMes()
            Try
                _movimientosMes = New List(Of MovimientoCalendario)()

                Dim ingresos As List(Of Ingreso) = _ingresoDAL.ObtenerPorMes(_mesActual, _anioActual)
                For Each ing As Ingreso In ingresos
                    Dim movIng As New MovimientoCalendario()
                    movIng.Dia = ing.Fecha.Day
                    movIng.Tipo = TipoMovimiento.Ingreso
                    _movimientosMes.Add(movIng)
                Next

                Dim egresos As List(Of Egreso) = _egresoDAL.ObtenerPorMes(_mesActual, _anioActual)
                For Each eg As Egreso In egresos
                    Dim tipo As TipoMovimiento = If(eg.Tipo = TipoEgreso.Recurrente,
                        TipoMovimiento.Recurrente,
                        TipoMovimiento.Egreso)
                    Dim movEg As New MovimientoCalendario()
                    movEg.Dia = eg.Fecha.Day
                    movEg.Tipo = tipo
                    _movimientosMes.Add(movEg)
                Next

            Catch ex As Exception
                _movimientosMes = New List(Of MovimientoCalendario)()
            End Try
        End Sub

        ''' <summary>
        ''' Construye dinámicamente las celdas del calendario para el mes actual.
        ''' Limpia y regenera el FlowPanel de días.
        ''' </summary>
        Private Sub DibujarCalendario()
            LblMesAnio.Text = $"{NombreMes(_mesActual)} {_anioActual}"
            FlowDias.Controls.Clear()
            FlowDias.SuspendLayout()

            Dim primerDia As New Date(_anioActual, _mesActual, 1)
            Dim diasEnMes As Integer = Date.DaysInMonth(_anioActual, _mesActual)

            ' DayOfWeek: domingo=0. Convertir a lunes=0 ... domingo=6
            Dim offsetLunes As Integer = ((CInt(primerDia.DayOfWeek) + 6) Mod 7)

            ' Celdas vacías para el offset inicial
            For i As Integer = 0 To offsetLunes - 1
                FlowDias.Controls.Add(CrearCeldaVacia())
            Next

            ' Celdas de días
            For dia As Integer = 1 To diasEnMes
                Dim pnl As Panel = CrearCeldaDia(dia)
                FlowDias.Controls.Add(pnl)
            Next

            FlowDias.ResumeLayout()
        End Sub

        ''' <summary>Crea una celda vacía de relleno para el offset del mes.</summary>
        Private Function CrearCeldaVacia() As Panel
            Dim pnl As New Panel()
            pnl.Size = New Size(78, 72)
            pnl.BackColor = Color.FromArgb(28, 34, 52)
            Return pnl
        End Function

        ''' <summary>Crea la celda visual de un día con número y puntos de movimientos.</summary>
        Private Function CrearCeldaDia(dia As Integer) As Panel
            Dim esHoy As Boolean = (dia = Date.Today.Day AndAlso
                                         _mesActual = Date.Today.Month AndAlso
                                         _anioActual = Date.Today.Year)

            Dim pnl As New Panel()
            pnl.Size = New Size(78, 72)
            pnl.BackColor = If(esHoy, Color.FromArgb(40, 60, 100), Color.FromArgb(28, 34, 52))
            pnl.Cursor = Cursors.Hand
            pnl.Tag = dia

            ' Número del día
            Dim lblDia As New Label()
            lblDia.Text = dia.ToString()
            lblDia.ForeColor = If(esHoy, Color.White, Color.FromArgb(180, 190, 210))
            lblDia.Font = New Font("Segoe UI", 8.5F, If(esHoy, FontStyle.Bold, FontStyle.Regular))
            lblDia.AutoSize = False
            lblDia.Bounds = New Rectangle(0, 4, 78, 18)
            lblDia.TextAlign = ContentAlignment.TopRight
            lblDia.Padding = New Padding(0, 0, 6, 0)
            lblDia.Tag = dia
            lblDia.Cursor = Cursors.Hand

            ' Panel de puntos
            Dim pnlPuntos As New Panel()
            pnlPuntos.BackColor = Color.Transparent
            pnlPuntos.Bounds = New Rectangle(4, 28, 70, 36)
            pnlPuntos.Tag = dia

            ' Obtener movimientos del día
            Dim movs As List(Of MovimientoCalendario) =
                _movimientosMes.Where(Function(m) m.Dia = dia).ToList()

            ' Dibujar puntos (máx 6 visibles)
            AgregarPuntosDia(pnlPuntos, movs)

            pnl.Controls.Add(pnlPuntos)
            pnl.Controls.Add(lblDia)

            ' Eventos click en el panel y la etiqueta
            AddHandler pnl.Click, Sub(s, e) AbrirRegistro(dia)
            AddHandler lblDia.Click, Sub(s, e) AbrirRegistro(dia)
            AddHandler pnlPuntos.Click, Sub(s, e) AbrirRegistro(dia)

            ' Hover
            AddHandler pnl.MouseEnter, Sub(s, e)
                                           If Not esHoy Then pnl.BackColor = Color.FromArgb(38, 48, 72)
                                       End Sub
            AddHandler pnl.MouseLeave, Sub(s, e)
                                           pnl.BackColor = If(esHoy, Color.FromArgb(40, 60, 100), Color.FromArgb(28, 34, 52))
                                       End Sub

            Return pnl
        End Function

        ''' <summary>
        ''' Agrega puntos de colores al panel de un día según sus movimientos.
        ''' Verde = ingreso, Rojo = egreso normal, Amarillo = recurrente.
        ''' </summary>
        Private Sub AgregarPuntosDia(pnlPuntos As Panel, movs As List(Of MovimientoCalendario))
            If movs.Count = 0 Then Return

            Const puntoDiam As Integer = 8
            Const espaciado As Integer = 11
            Const maxPuntos As Integer = 6
            Dim maxVis As Integer = Math.Min(movs.Count, maxPuntos)
            Dim totalAncho As Integer = maxVis * puntoDiam + (maxVis - 1) * (espaciado - puntoDiam)
            Dim startX As Integer = Math.Max(0, (70 - totalAncho) \ 2)

            For i As Integer = 0 To maxVis - 1
                Dim color As Color
                Select Case movs(i).Tipo
                    Case TipoMovimiento.Ingreso : color = ColorIngreso
                    Case TipoMovimiento.Recurrente : color = ColorRecurrente
                    Case Else : color = ColorEgreso
                End Select

                Dim x As Integer = startX + i * espaciado
                Dim punto As New Panel()
                punto.Size = New Size(puntoDiam, puntoDiam)
                punto.Location = New Point(x, 10)
                punto.BackColor = color
                punto.Tag = pnlPuntos.Tag

                ' Hacer el punto circular usando Region
                Dim path As New System.Drawing.Drawing2D.GraphicsPath()
                path.AddEllipse(0, 0, puntoDiam, puntoDiam)
                punto.Region = New Region(path)
                punto.Cursor = Cursors.Hand

                AddHandler punto.Click, Sub(s, e) AbrirRegistro(CInt(pnlPuntos.Tag))

                pnlPuntos.Controls.Add(punto)
            Next
        End Sub

        ''' <summary>Abre el formulario de registro para un día específico.</summary>
        Private Sub AbrirRegistro(dia As Integer)
            Dim fechaSeleccionada As New Date(_anioActual, _mesActual, dia)
            Using frm As New FormRegistroMovimiento(fechaSeleccionada)
                If frm.ShowDialog(Me) = DialogResult.OK Then
                    RefrescarTodo()
                End If
            End Using
        End Sub

#End Region

#Region "Navegación del calendario"

        Private Sub BtnMesAnterior_Click(sender As Object, e As EventArgs) Handles BtnMesAnterior.Click
            If _mesActual = 1 Then
                _mesActual = 12
                _anioActual -= 1
            Else
                _mesActual -= 1
            End If
            RefrescarTodo()
        End Sub

        Private Sub BtnMesSiguiente_Click(sender As Object, e As EventArgs) Handles BtnMesSiguiente.Click
            If _mesActual = 12 Then
                _mesActual = 1
                _anioActual += 1
            Else
                _mesActual += 1
            End If
            RefrescarTodo()
        End Sub

#End Region

#Region "Gráfico Donut"

        ''' <summary>
        ''' Dibuja el gráfico de dona de gastos por categoría.
        ''' GDI+ puro — sin librerías externas.
        ''' </summary>
        Private Sub PnlDonut_Paint(sender As Object, e As PaintEventArgs) Handles PnlDonut.Paint
            Dim g As Graphics = e.Graphics
            g.SmoothingMode = SmoothingMode.AntiAlias

            Dim datos As List(Of DatoDonut) = ObtenerDatosDonut()
            If datos.Count = 0 Then
                DibujarDonutVacio(g)
                Return
            End If

            Dim total As Decimal = datos.Sum(Function(d) d.Valor)
            If total = 0 Then
                DibujarDonutVacio(g)
                Return
            End If

            Dim rect As New Rectangle(10, 28, 110, 110)
            Dim startAng As Single = -90.0F

            For Each dato As DatoDonut In datos
                Dim sweep As Single = CSng(dato.Valor / total * 360.0)
                Using br As New SolidBrush(dato.Color)
                    g.FillPie(br, rect, startAng, sweep)
                End Using
                startAng += sweep
            Next

            ' Agujero interior (efecto dona)
            Dim holeRect As New Rectangle(30, 48, 70, 70)
            Using br As New SolidBrush(Color.FromArgb(28, 34, 52))
                g.FillEllipse(br, holeRect)
            End Using

            ' Leyenda a la derecha
            Dim lyX As Integer = 130
            Dim lyY As Integer = 30
            For Each dato As DatoDonut In datos
                Using br As New SolidBrush(dato.Color)
                    g.FillEllipse(br, lyX, lyY + 2, 8, 8)
                End Using
                Using br As New SolidBrush(Color.FromArgb(180, 190, 210))
                    Using fnt As New Font("Segoe UI", 7.5F)
                        g.DrawString(dato.Etiqueta, fnt, br, lyX + 12, lyY)
                    End Using
                End Using
                lyY += 18
            Next
        End Sub

        Private Sub DibujarDonutVacio(g As Graphics)
            Dim rect As New Rectangle(10, 28, 110, 110)
            Using br As New SolidBrush(Color.FromArgb(45, 55, 80))
                g.FillEllipse(br, rect)
            End Using
            Dim holeRect As New Rectangle(30, 48, 70, 70)
            Using br As New SolidBrush(Color.FromArgb(28, 34, 52))
                g.FillEllipse(br, holeRect)
            End Using
        End Sub

        Private Function ObtenerDatosDonut() As List(Of DatoDonut)
            Dim lista As New List(Of DatoDonut)()
            Dim colores As Color() = {
                Color.FromArgb(220, 70, 70),
                Color.FromArgb(60, 160, 220),
                Color.FromArgb(240, 180, 0),
                Color.FromArgb(80, 200, 120),
                Color.FromArgb(180, 80, 220)
            }

            Try
                Dim resumen As List(Of ResumenCategoria) =
                    _egresoDAL.ObtenerResumenPorCategoria(_mesActual, _anioActual)

                For i As Integer = 0 To Math.Min(resumen.Count, 5) - 1
                    Dim dd As New DatoDonut()
                    dd.Etiqueta = resumen(i).NombreCategoria
                    dd.Valor = resumen(i).Total
                    dd.Color = colores(i Mod colores.Length)
                    lista.Add(dd)
                Next
            Catch
                ' Sin datos: devuelve lista vacía
            End Try

            Return lista
        End Function

#End Region

#Region "Gráfico de Barras"

        ''' <summary>
        ''' Dibuja el gráfico de barras de balance de los últimos 6 meses.
        ''' Barras rojas = egresos, barras verdes = ingresos, línea azul = balance.
        ''' </summary>
        Private Sub PnlBarras_Paint(sender As Object, e As PaintEventArgs) Handles PnlBarras.Paint
            Dim g As Graphics = e.Graphics
            g.SmoothingMode = SmoothingMode.AntiAlias

            Dim datos As List(Of DatoMes) = ObtenerDatos6Meses()
            If datos.Count = 0 Then Return

            Dim w As Integer = PnlBarras.Width - 24
            Dim h As Integer = PnlBarras.Height - 50
            Dim offsetY As Integer = 10
            Dim offsetX As Integer = 12

            Dim maxVal As Decimal = datos.Max(Function(d) Math.Max(d.Ingresos, d.Egresos))
            If maxVal = 0 Then maxVal = 1

            Dim barW As Integer = Math.Max(6, (w \ datos.Count) - 10)
            Dim groupW As Integer = w \ datos.Count

            ' Líneas guía horizontales
            Using pen As New Pen(Color.FromArgb(45, 55, 80))
                For i As Integer = 0 To 3
                    Dim gy As Integer = offsetY + CInt(h * i / 4)
                    g.DrawLine(pen, offsetX, gy, offsetX + w, gy)
                Next
            End Using

            ' Barras + etiquetas mes
            For i As Integer = 0 To datos.Count - 1
                Dim d As DatoMes = datos(i)
                Dim cx As Integer = offsetX + i * groupW + groupW \ 2

                ' Barra egresos (roja)
                Dim hEgr As Integer = CInt(h * CDbl(d.Egresos) / CDbl(maxVal))
                Using br As New SolidBrush(Color.FromArgb(200, 60, 60))
                    g.FillRectangle(br, cx - barW - 2, offsetY + h - hEgr, barW, hEgr)
                End Using

                ' Barra ingresos (verde)
                Dim hIng As Integer = CInt(h * CDbl(d.Ingresos) / CDbl(maxVal))
                Using br As New SolidBrush(Color.FromArgb(60, 180, 100))
                    g.FillRectangle(br, cx + 2, offsetY + h - hIng, barW, hIng)
                End Using

                ' Etiqueta mes
                Using br As New SolidBrush(Color.FromArgb(120, 135, 165))
                    Using fnt As New Font("Segoe UI", 7.0F)
                        Dim lbl As String = d.Etiqueta
                        Dim sz As SizeF = g.MeasureString(lbl, fnt)
                        g.DrawString(lbl, fnt, br, cx - sz.Width / 2, offsetY + h + 4)
                    End Using
                End Using
            Next

            ' Línea de balance (azul)
            Dim pts As New List(Of PointF)()
            For i As Integer = 0 To datos.Count - 1
                Dim d As DatoMes = datos(i)
                Dim cx As Single = offsetX + i * groupW + groupW \ 2
                Dim norm As Decimal = If(maxVal > 0, (d.Ingresos - d.Egresos + maxVal) / (2 * maxVal), 0.5D)
                Dim cy As Single = offsetY + h - CSng(h * CDbl(norm))
                pts.Add(New PointF(cx, cy))
            Next

            If pts.Count > 1 Then
                Using pen As New Pen(Color.FromArgb(80, 160, 240), 2)
                    g.DrawLines(pen, pts.ToArray())
                End Using
                For Each pt As PointF In pts
                    Using br As New SolidBrush(Color.FromArgb(80, 160, 240))
                        g.FillEllipse(br, pt.X - 3, pt.Y - 3, 6, 6)
                    End Using
                Next
            End If
        End Sub

        Private Function ObtenerDatos6Meses() As List(Of DatoMes)
            Dim lista As New List(Of DatoMes)()
            Try
                Dim cursor As New Date(_anioActual, _mesActual, 1)
                cursor = cursor.AddMonths(-5)

                For i As Integer = 0 To 5
                    Dim dm As New DatoMes()
                    dm.Etiqueta = cursor.ToString("MMM").Substring(0, 3).ToUpper()
                    dm.Ingresos = _ingresoDAL.ObtenerTotalMes(cursor.Month, cursor.Year)
                    dm.Egresos = _egresoDAL.ObtenerTotalMes(cursor.Month, cursor.Year)
                    lista.Add(dm)
                    cursor = cursor.AddMonths(1)
                Next
            Catch
            End Try
            Return lista
        End Function

#End Region

#Region "Helpers"

        Private Function NombreMes(mes As Integer) As String
            Return New Date(2000, mes, 1).ToString("MMMM").ToUpper()
        End Function

#End Region

    End Class

    ' ── DTOs internos del Dashboard ───────────────────────────────────────────

    Friend Class MovimientoCalendario
        Public Property Dia As Integer
        Public Property Tipo As TipoMovimiento
    End Class

    Friend Enum TipoMovimiento
        Ingreso
        Egreso
        Recurrente
    End Enum

    Friend Class DatoDonut
        Public Property Etiqueta As String = String.Empty
        Public Property Valor As Decimal
        Public Property Color As Color
    End Class

    Friend Class DatoMes
        Public Property Etiqueta As String = String.Empty
        Public Property Ingresos As Decimal
        Public Property Egresos As Decimal
    End Class

End Namespace
