Imports DL_WalletGuard.WalletGuard.AccesoDatos
Imports DL_WalletGuard.WalletGuard.Entidades

Namespace WalletGuard.LogicaNegocio

    ''' <summary>
    ''' Servicio de negocio para gestión CRUD de gastos recurrentes.
    ''' Separa las validaciones de negocio del acceso a datos.
    ''' </summary>
    Public Class GastoRecurrenteService

        Private ReadOnly _dal As GastoRecurrenteDAL

        Public Sub New()
            _dal = New GastoRecurrenteDAL()
        End Sub

#Region "Consultas"

        Public Function ObtenerTodos() As List(Of GastoRecurrente)
            Return _dal.ObtenerTodos()
        End Function

#End Region

#Region "Comandos con validación"

        ''' <summary>Guarda (insert o update) con validaciones de negocio.</summary>
        Public Sub Guardar(gr As GastoRecurrente)
            ValidarRecurrente(gr)
            If gr.IdRecurrente = 0 Then
                _dal.Insertar(gr)
            Else
                _dal.Actualizar(gr)
            End If
        End Sub

        Public Sub Desactivar(idRecurrente As Integer)
            _dal.DesactivarRecurrente(idRecurrente)
        End Sub

#End Region

#Region "Validaciones"

        Private Sub ValidarRecurrente(gr As GastoRecurrente)
            If String.IsNullOrWhiteSpace(gr.Nombre) Then
                Throw New ArgumentException("El nombre del gasto recurrente es obligatorio.")
            End If
            If gr.Monto <= 0 Then
                Throw New ArgumentException("El monto debe ser mayor a cero.")
            End If
            If gr.DiaCorte < 1 OrElse gr.DiaCorte > 31 Then
                Throw New ArgumentException("El día de corte debe estar entre 1 y 31.")
            End If
            If gr.FechaFin.HasValue AndAlso gr.FechaFin.Value < gr.FechaInicio Then
                Throw New ArgumentException("La fecha de fin no puede ser anterior a la fecha de inicio.")
            End If
        End Sub

#End Region

    End Class

End Namespace
