Public Class Cliente
    Private _nombre As String
    Public ReadOnly Property Cedula As String

    Public Property Sueldo As Double

    Public Property Nombre As String
        Get
            Return _nombre
        End Get
        Set(value As String)
            If String.IsNullOrWhiteSpace(value) Then
                Throw New ArgumentException("El nombre es obligatorio")
            End If
            _nombre = value.Trim()
        End Set
    End Property

    Public Sub New(cedula As String, nombre As String)
        If String.IsNullOrWhiteSpace(cedula) Then
            Throw New ArgumentException("La cédula es obligatoria.")
        End If

        Me.Cedula = cedula.Trim()
        Me.Nombre = nombre
    End Sub

    Public Overrides Function ToString() As String
        Return $"{Cedula} - {Nombre}"
    End Function


End Class
