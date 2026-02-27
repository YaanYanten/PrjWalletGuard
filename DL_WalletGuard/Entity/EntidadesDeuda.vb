Namespace WalletGuard.Entidades

    ''' <summary>
    ''' Mapea la tabla sr_deudas tras la migración V1.1.
    '''
    ''' Columnas originales conservadas:
    '''   id_deuda, acreedor, monto_total, saldo_actual, tasa_interes,
    '''   pago_minimo, fecha_inicio, estado, fecha_creacion
    '''
    ''' Columnas agregadas en V1.1:
    '''   nombre, fecha_vencimiento
    '''
    ''' Decisión de diseño:
    '''   - saldo_actual existe en BD pero NO se usa en la lógica de negocio;
    '''     el saldo se calcula dinámicamente desde sr_pagos_deuda.
    '''   - estado es varchar(20): valores esperados 'Activo' | 'Pagado' | 'Mora'
    ''' </summary>
    Public Class Deuda

        Public Property IdDeuda As Integer
        Public Property Nombre As String = String.Empty   ' V1.1 — "Tarjeta Visa"
        Public Property Acreedor As String = String.Empty   ' "Banco Nacional"
        Public Property MontoTotal As Decimal                  ' Deuda original pactada
        Public Property TasaInteres As Decimal                  ' % mensual
        Public Property PagoMinimo As Decimal                  ' Cuota mínima mensual
        Public Property FechaInicio As Date
        Public Property FechaVencimiento As Date?                    ' V1.1 — Nullable
        Public Property Estado As String = "Activo"       ' 'Activo'|'Pagado'|'Mora'
        Public Property FechaCreacion As Date = Date.Now

    End Class

    ''' <summary>Valores válidos para Deuda.Estado.</summary>
    Public Module EstadoDeuda
        Public Const Activo As String = "Activo"
        Public Const Pagado As String = "Pagado"
        Public Const Mora As String = "Mora"
    End Module

    ''' <summary>
    ''' Mapea la tabla sr_pagos_deuda.
    ''' Cada fila es un abono realizado contra una deuda.
    ''' </summary>
    Public Class PagoDeuda

        Public Property IdPago       As Integer
        Public Property IdDeuda      As Integer
        Public Property Fecha        As Date
        Public Property Monto        As Decimal
        Public Property interes_pagado As Decimal
        Public Property capital_pagado As Decimal

    End Class

    ''' <summary>
    ''' DTO de resumen calculado para una deuda individual.
    ''' Usado por la capa de presentación sin exponer lógica de negocio.
    ''' </summary>
    Public Class ResumenDeuda

        Public Property IdDeuda          As Integer
        Public Property Nombre           As String  = String.Empty
        Public Property Acreedor         As String  = String.Empty
        Public Property MontoOriginal    As Decimal
        Public Property TotalPagado      As Decimal
        Public Property SaldoPendiente   As Decimal
        Public Property PorcentajePagado As Decimal   ' 0 a 100
        Public Property FechaVencimiento As Date?
        Public Property Activa           As Boolean
        Public Property MesesRestantes   As Integer?  ' Proyección al ritmo actual
        Public Property Estado As String = String.Empty
        Public Property PagoMinimo As Decimal
    End Class

    ''' <summary>
    ''' DTO para una línea del reporte de flujo de caja.
    ''' </summary>
    Public Class LineaFlujoCaja

        Public Property Periodo        As String  = String.Empty   ' Ej: "Enero 2025"
        Public Property Anio           As Integer
        Public Property Mes            As Integer
        Public Property TotalIngresos  As Decimal
        Public Property TotalEgresos   As Decimal
        Public Property Balance        As Decimal
        Public Property BalanceAcumulado As Decimal

    End Class

    ''' <summary>
    ''' DTO para proyección mensual futura.
    ''' </summary>
    Public Class LineaProyeccion

        Public Property Periodo          As String  = String.Empty
        Public Property IngresosProyectados As Decimal
        Public Property EgresosProyectados  As Decimal
        Public Property RecurrentesActivos  As Decimal
        Public Property BalanceProyectado   As Decimal
        Public Property EsProyeccion        As Boolean = True   ' False = dato real

    End Class

End Namespace
