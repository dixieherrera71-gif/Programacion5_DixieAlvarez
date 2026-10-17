Public Class Proyector
    Inherits Equipo

    Private Const MultaPorHoras As Decimal = 1000D

    Public Sub New(Codigo As String, descripcion As String)
        MyBase.New(Codigo, descripcion)
    End Sub

    Protected Overrides Function MontoPorRetraso(horas As Integer) As Decimal
        Return MultaPorHoras * horas
    End Function
End Class
