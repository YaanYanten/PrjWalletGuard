Imports Microsoft.Data.SqlClient
Imports DL_WalletGuard.WalletGuard.Entidades

Namespace WalletGuard.AccesoDatos

    ''' <summary>
    ''' Acceso a datos para sr_deudas y sr_pagos_deuda.
    ''' Corregido para usar los nombres de columna reales de la tabla
    ''' más las columnas agregadas en la migración V1.1.
    '''
    ''' Cambios respecto a la versión anterior:
    '''   activa/activo   → estado (varchar)
    '''   monto_original  → monto_total
    '''   nombre          → agregado en V1.1 (antes no existía)
    '''   fecha_vencimiento → agregada en V1.1 (antes no existía)
    '''   saldo_actual    → existe en BD pero saldo se calcula desde pagos
    ''' </summary>
    Public Class DeudaDAL

#Region "Consultas — Deudas"

        Public Function ObtenerTodas() As List(Of Deuda)
            Dim lista As New List(Of Deuda)()
            Dim sql As String =
                "SELECT id_deuda, nombre, acreedor, monto_total, tasa_interes, " &
                "       pago_minimo, fecha_inicio, fecha_vencimiento, " &
                "       estado, fecha_creacion " &
                "FROM   sr_deudas " &
                "ORDER  BY CASE estado WHEN 'Activo' THEN 0 " &
                "                      WHEN 'Mora'   THEN 1 " &
                "                      ELSE 2 END, nombre"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        Do While dr.Read()
                            lista.Add(MapearDeuda(dr))
                        Loop
                    End Using
                End Using
            End Using
            Return lista
        End Function

        Public Function ObtenerActivas() As List(Of Deuda)
            Dim lista As New List(Of Deuda)()
            Dim sql As String =
                "SELECT id_deuda, nombre, acreedor, monto_total, tasa_interes, " &
                "       pago_minimo, fecha_inicio, fecha_vencimiento, " &
                "       estado, fecha_creacion " &
                "FROM   sr_deudas " &
                "WHERE  estado IN ('Activo', 'Mora') " &
                "ORDER  BY nombre"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        Do While dr.Read()
                            lista.Add(MapearDeuda(dr))
                        Loop
                    End Using
                End Using
            End Using
            Return lista
        End Function

        ''' <summary>
        ''' Total pagado acumulado para una deuda, calculado desde sr_pagos_deuda.
        ''' No usa saldo_actual de la tabla (decisión de diseño acordada).
        ''' </summary>
        Public Function ObtenerTotalPagado(idDeuda As Integer) As Decimal
            Dim sql As String =
                "SELECT ISNULL(SUM(monto_pagado), 0) " &
                "FROM   sr_pagos_deuda " &
                "WHERE  id_deuda = @id"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", idDeuda)
                    Return Convert.ToDecimal(cmd.ExecuteScalar())
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Promedio mensual de pagos en los últimos 3 meses.
        ''' Usado para proyectar cuántos meses restan para liquidar la deuda.
        ''' </summary>
        Public Function ObtenerPromedioMensualPagos(idDeuda As Integer) As Decimal
            Dim sql As String =
                "SELECT ISNULL(AVG(total_mes), 0) " &
                "FROM ( " &
                "   SELECT SUM(monto_pagado) AS total_mes " &
                "   FROM   sr_pagos_deuda " &
                "   WHERE  id_deuda = @id " &
                "     AND  fecha_pago  >= DATEADD(MONTH, -3, GETDATE()) " &
                "   GROUP  BY YEAR(fecha_pago ), MONTH(fecha_pago ) " &
                ") t"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", idDeuda)
                    Return Convert.ToDecimal(cmd.ExecuteScalar())
                End Using
            End Using
        End Function

#End Region

#Region "Consultas — Pagos"

        Public Function ObtenerPagosPorDeuda(idDeuda As Integer) As List(Of PagoDeuda)
            Dim lista As New List(Of PagoDeuda)()
            Dim sql As String =
                "SELECT id_pago, id_deuda, fecha_pago, monto_pagado, interes_pagado, capital_pagado " &
                "FROM   sr_pagos_deuda " &
                "WHERE  id_deuda = @id " &
                "ORDER  BY fecha DESC"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", idDeuda)
                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        Do While dr.Read()
                            Dim p As New PagoDeuda()
                            p.IdPago = dr.GetInt32(dr.GetOrdinal("id_pago"))
                            p.IdDeuda = dr.GetInt32(dr.GetOrdinal("id_deuda"))
                            p.Fecha = dr.GetDateTime(dr.GetOrdinal("fecha_pago"))
                            p.Monto = dr.GetDecimal(dr.GetOrdinal("monto_pagado"))
                            p.interes_pagado = dr.GetDecimal(dr.GetOrdinal("interes_pagado"))
                            p.capital_pagado = dr.GetDecimal(dr.GetOrdinal("capital_pagado"))
                            lista.Add(p)
                        Loop
                    End Using
                End Using
            End Using
            Return lista
        End Function

#End Region

#Region "Comandos — Deudas"

        Public Function InsertarDeuda(d As Deuda) As Integer
            Dim sql As String =
                "INSERT INTO sr_deudas " &
                "  (nombre, acreedor, monto_total, saldo_actual, tasa_interes, " &
                "   pago_minimo, fecha_inicio, fecha_vencimiento, estado, fecha_creacion) " &
                "VALUES " &
                "  (@nombre, @acreedor, @montoTotal, @montoTotal, @tasa, " &
                "   @pagoMin, @inicio, @vence, @estado, GETDATE()); " &
                "SELECT SCOPE_IDENTITY();"
            ' Nota: saldo_actual se inicializa igual a monto_total al insertar.
            ' Los pagos futuros lo reducirán si se decide actualizar ese campo también.

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@nombre", d.Nombre)
                    cmd.Parameters.AddWithValue("@acreedor", d.Acreedor)
                    cmd.Parameters.AddWithValue("@montoTotal", d.MontoTotal)
                    cmd.Parameters.AddWithValue("@tasa", d.TasaInteres)
                    cmd.Parameters.AddWithValue("@pagoMin", d.PagoMinimo)
                    cmd.Parameters.AddWithValue("@inicio", d.FechaInicio)
                    cmd.Parameters.AddWithValue("@vence", If(d.FechaVencimiento.HasValue,
                        CObj(d.FechaVencimiento.Value), DBNull.Value))
                    cmd.Parameters.AddWithValue("@estado", d.Estado)
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        End Function

        Public Sub ActualizarDeuda(d As Deuda)
            Dim sql As String =
                "UPDATE sr_deudas " &
                "SET    nombre            = @nombre, " &
                "       acreedor          = @acreedor, " &
                "       monto_total       = @montoTotal, " &
                "       tasa_interes      = @tasa, " &
                "       pago_minimo       = @pagoMin, " &
                "       fecha_inicio      = @inicio, " &
                "       fecha_vencimiento = @vence, " &
                "       estado            = @estado " &
                "WHERE  id_deuda = @id"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@nombre", d.Nombre)
                    cmd.Parameters.AddWithValue("@acreedor", d.Acreedor)
                    cmd.Parameters.AddWithValue("@montoTotal", d.MontoTotal)
                    cmd.Parameters.AddWithValue("@tasa", d.TasaInteres)
                    cmd.Parameters.AddWithValue("@pagoMin", d.PagoMinimo)
                    cmd.Parameters.AddWithValue("@inicio", d.FechaInicio)
                    cmd.Parameters.AddWithValue("@vence", If(d.FechaVencimiento.HasValue,
                        CObj(d.FechaVencimiento.Value), DBNull.Value))
                    cmd.Parameters.AddWithValue("@estado", d.Estado)
                    cmd.Parameters.AddWithValue("@id", d.IdDeuda)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ''' <summary>
        ''' Actualiza el estado de la deuda.
        ''' Se llama desde DeudaService cuando el saldo llega a 0.
        ''' </summary>
        Public Sub ActualizarEstado(idDeuda As Integer, estado As String)
            Dim sql As String =
                "UPDATE sr_deudas SET estado = @estado WHERE id_deuda = @id"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@estado", estado)
                    cmd.Parameters.AddWithValue("@id", idDeuda)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

#End Region

#Region "Comandos — Pagos"

        Public Function InsertarPago(p As PagoDeuda) As Integer
            Dim sql As String =
                "INSERT INTO sr_pagos_deuda (id_deuda, fecha_pago, monto_pagado, interes_pagado, capital_pagado) " &
                "VALUES (@idDeuda, @fecha, @monto, @interes_pagado, @capital_pagado); " &
                "SELECT SCOPE_IDENTITY();"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@idDeuda", p.IdDeuda)
                    cmd.Parameters.AddWithValue("@fecha", p.Fecha)
                    cmd.Parameters.AddWithValue("@monto", p.Monto)
                    cmd.Parameters.AddWithValue("@interes_pagado", p.interes_pagado)
                    cmd.Parameters.AddWithValue("@capital_pagado", p.capital_pagado)

                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        End Function

        Public Sub EliminarPago(idPago As Integer)
            Dim sql As String = "DELETE FROM sr_pagos_deuda WHERE id_pago = @id"

            Using conn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", idPago)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

#End Region

#Region "Mapeo privado"

        Private Function MapearDeuda(dr As SqlDataReader) As Deuda
            Dim d As New Deuda()

            d.IdDeuda = dr.GetInt32(dr.GetOrdinal("id_deuda"))
            d.Nombre = dr.GetString(dr.GetOrdinal("nombre"))
            d.Acreedor = dr.GetString(dr.GetOrdinal("acreedor"))
            d.MontoTotal = dr.GetDecimal(dr.GetOrdinal("monto_total"))
            d.TasaInteres = dr.GetDecimal(dr.GetOrdinal("tasa_interes"))
            d.PagoMinimo = dr.GetDecimal(dr.GetOrdinal("pago_minimo"))
            d.FechaInicio = dr.GetDateTime(dr.GetOrdinal("fecha_inicio"))
            d.Estado = dr.GetString(dr.GetOrdinal("estado"))

            Dim ordFecha As Integer = dr.GetOrdinal("fecha_creacion")
            d.FechaCreacion = If(dr.IsDBNull(ordFecha), Date.Now, dr.GetDateTime(ordFecha))

            Dim ordVence As Integer = dr.GetOrdinal("fecha_vencimiento")
            d.FechaVencimiento = If(dr.IsDBNull(ordVence),
                CType(Nothing, Date?), dr.GetDateTime(ordVence))

            Return d
        End Function

#End Region

    End Class

End Namespace