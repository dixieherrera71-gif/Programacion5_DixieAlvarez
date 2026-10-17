Public MustInherit Class Equipo
    Implements IPrestable

    Private _descripcion As String

    Public ReadOnly Property Codigo As String Implements IPrestable.Codigo

    Public Property Descripcion As String Implements IPrestable.Descripcion
        Get
            Return _descripcion
        End Get
        Set(value As String)
            If (String.IsNullOrWhiteSpace(value)) Then
                Throw New ArgumentException("La descripcion es obligatoria.")

            End If
            _descripcion = value


        End Set
    End Property

    Protected Sub New(codigo As String, descripcion As String)
        If (String.IsNullOrWhiteSpace(codigo)) Then
            Throw New ArgumentException("El codigo es obligatoria.")
        End If
        Me.Codigo = codigo
        Me.Descripcion = descripcion
    End Sub

    Public Overrides Function ToString() As String
        Return $"{Codigo} - {Descripcion}"
    End Function

    Public Function CalcularMulta(horas As Integer) As Decimal _
        Implements IPrestable.CalcularMulta

        If (horas < 0) Then
            Throw New ArgumentOutOfRangeException(NameOf(horas), "Las horas de retraso no pueden ser negativas")

        End If

        If (horas = 0) Then
            Return 0D
        End If

        Return MontoPorRetraso(horas)
    End Function

    Protected MustOverride Function MontoPorRetraso(horas As Integer) As Decimal

End Class
