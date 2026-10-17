Public Class HistorialCrediticioServicio

    Public Async Function TieneMorosidadAsync(cedula As String) As Task(Of Boolean)

        If String.IsNullOrWhiteSpace(cedula) Then
            Throw New ArgumentException("Debe ingresar una cédula.")
        End If

        Await Task.Delay(3000)

        Return cedula.Trim().EndsWith("5")

    End Function

End Class