Imports System

Module Program
    Sub Main(args As String())

        Dim laptop As New Labtop("LAB-001", "Laptop del laboratorio 2")
        Dim proyector As New Proyector("Pro-007", "Proyector del aula 2")
        Console.WriteLine(laptop)
        Console.WriteLine(proyector)

        Try
            laptop.Descripcion = "  "

        Catch ex As Exception
            Console.WriteLine($"Validacion:  {ex.Message}")

        End Try

        Try
            Proyector.CalcularMulta(-2)

        Catch ex As Exception

        End Try

        Console.WriteLine($"{laptop}: {laptop.CalcularMulta(9)}")
        Console.WriteLine($"{Proyector}: {Proyector.CalcularMulta(20)}")
    End Sub
End Module
