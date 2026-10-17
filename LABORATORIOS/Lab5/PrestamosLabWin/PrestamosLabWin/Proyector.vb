Public Class Proyector
    Inherits Equipo

    Public Sub New(codigo As String, descripcion As String)
        MyBase.New(codigo, descripcion)
    End Sub
    Public Overrides ReadOnly Property TarifaPorHora As Decimal
        Get
            Return 1000D
        End Get
    End Property
End Class
