Public Interface IPrestable

    ReadOnly Property Codigo As String

    ReadOnly Property Descripcion As String

    Function CalcularMulta(horas As Integer) As Decimal

End Interface
