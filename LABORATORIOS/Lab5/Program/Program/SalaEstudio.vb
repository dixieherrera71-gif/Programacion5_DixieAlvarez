Public Class SalaEstudio
    Implements IPrestable

    Public ReadOnly Property Codigo As String Implements IPrestable.Codigo

    Public ReadOnly Property Descripcion As String Implements IPrestable.Descripcion

    Public ReadOnly Property Capacidad As Integer
    Private Const MultaPorHora As Decimal = 15000

    Public Sub New(codigo As String, descripcion As String, capacidad As String)

        If (capacidad <= 0) Then
            Throw New ArgumentOutOfRangeException("La Capacidad debe ser mayor que cero")
        End If

        Me.Codigo = codigo
        Me.Descripcion = descripcion
        Me.Capacidad = capacidad
    End Sub


    Public Function CalcularMulta(horas As Integer) As Decimal Implements IPrestable.CalcularMulta
        Throw New NotImplementedException()
        If horas < 0 Then
            Throw New ArgumentOutOfRangeException(NameOf(horas))
        End If

        Return horas = MultaPorHora

    End Function
End Class
