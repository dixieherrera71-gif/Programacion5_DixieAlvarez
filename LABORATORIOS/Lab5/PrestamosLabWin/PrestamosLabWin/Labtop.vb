Public Class Labtop
    Inherits Equipo

    Private Const TopeMulta As Decimal = 5000D

    Public Sub New(codigo As String, descripcion As String)
        MyBase.New(codigo, descripcion)

    End Sub
    Public Overrides ReadOnly Property TarifaPorHora As Decimal
        Get

            Return 5000D
        End Get
    End Property
    Public Overrides Function CalcularMulta(horasAtraso As Integer) As Decimal
        Return Math.Min(MyBase.CalcularMulta(horasAtraso), TopeMulta)
    End Function
End Class
