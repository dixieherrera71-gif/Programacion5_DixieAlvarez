Public Class CooperativaServicio

    Private ReadOnly cuentas As New List(Of Cuenta)

    Public Function ObtenerCuentas() As List(Of Cuenta)
        Return cuentas
    End Function

    Public Sub AgregarCuenta(cuenta As Cuenta)

        If cuenta Is Nothing Then
            Throw New ArgumentNullException(NameOf(cuenta))
        End If

        Dim existe As Boolean = cuentas.Any(
            Function(c) String.Equals(
                c.Numero,
                cuenta.Numero,
                StringComparison.OrdinalIgnoreCase
            )
        )

        If existe Then
            Throw New InvalidOperationException(
                "Ya existe una cuenta con ese número."
            )
        End If

        cuentas.Add(cuenta)

    End Sub

End Class