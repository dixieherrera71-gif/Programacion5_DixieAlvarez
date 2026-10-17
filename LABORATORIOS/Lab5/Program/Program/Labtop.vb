Public Class Labtop
    Inherits Equipo

    Private Const MultaPorHora As Decimal = 5000
    Private Const MultaMaxima As Decimal = 5000D

    Public Sub New(codigo As String, descripcion As String)
        MyBase.New(codigo, descripcion)
    End Sub


    Protected Overrides Function MontoPorRetraso(horas As Integer) As Decimal
        Return Math.Min(horas * MultaPorHora, MultaMaxima)
    End Function



End Class
