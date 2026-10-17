Public MustInherit Class Equipo

    Public ReadOnly Property Codigo As String
    Public ReadOnly Property Descripcion As String

    Protected Sub New(codigo As String, descripcion As String)

        If String.IsNullOrWhiteSpace(codigo) Then
            Throw New ArgumentException("El código es obligatorio")
        End If

        If String.IsNullOrWhiteSpace(descripcion) Then
            Throw New ArgumentException("La descripcion es obligatoria")
        End If

        Me.Codigo = codigo.Trim().ToUpper()
        Me.Descripcion = descripcion.Trim()

    End Sub

    Public MustOverride ReadOnly Property TarifaPorHora As Decimal

    Public Overridable Function CalcularMulta(horasAtraso As Integer) As Decimal
        If horasAtraso < 0 Then
            Throw New ArgumentException("Las horas no pueden ser negativas")
        End If

        Return horasAtraso * TarifaPorHora
    End Function

    Public Overrides Function ToString() As String
        Return $"{Codigo} - {Descripcion} ({Me.GetType().Name} )"
    End Function

End Class
