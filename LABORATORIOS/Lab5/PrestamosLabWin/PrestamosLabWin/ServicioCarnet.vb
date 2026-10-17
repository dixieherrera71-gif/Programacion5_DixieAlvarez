Public Class ServicioCarnet

    Public Async Function EstaActivoAsync(carnet As String) As Task(Of Boolean)
        Await Task.Delay(3000)
        If (String.IsNullOrWhiteSpace(carnet)) Then
            Return False
        End If
        Return Not carnet.Trim().EndsWith("0")
    End Function

End Class
