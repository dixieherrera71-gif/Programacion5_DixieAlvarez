Public Class Form1

    Private ReadOnly cooperativa As New CooperativaServicio()
    Private ReadOnly historialServicio As New HistorialCrediticioServicio()


    Private Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click

        Try

            If cboTipoCuenta.SelectedIndex = -1 Then
                MessageBox.Show("Debe seleccionar un tipo de cuenta.")
                Return
            End If

            Dim numero As String = txtNumeroCuenta.Text.Trim()
            Dim nombre As String = txtNombre.Text.Trim()

            Dim saldo As Decimal

            If Not Decimal.TryParse(txtSaldoInicial.Text, saldo) Then
                MessageBox.Show("El saldo inicial debe ser un número válido.")
                Return
            End If

            Dim nuevaCuenta As Cuenta

            If cboTipoCuenta.SelectedItem.ToString() = "A la vista" Then

                nuevaCuenta = New CuentaVista(numero, nombre, saldo)

            Else

                nuevaCuenta = New CuentaNavidena(numero, nombre, saldo)

            End If

            cooperativa.AgregarCuenta(nuevaCuenta)

            ActualizarLista()

            MessageBox.Show("Cuenta registrada correctamente.")

            LimpiarRegistro()

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub


    Private Sub btnInteres_Click(sender As Object, e As EventArgs) Handles btnInteres.Click

        If lstCuentas.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar una cuenta.")
            Return
        End If

        Dim cuentaSeleccionada As Cuenta =
            DirectCast(lstCuentas.SelectedItem, Cuenta)

        Dim interes As Decimal =
            cuentaSeleccionada.CalcularInteresAnual()

        lblInteres.Text = "₡" & interes.ToString("N2")

    End Sub


    Private Sub btnRetirar_Click(sender As Object, e As EventArgs) Handles btnRetirar.Click

        Try

            If lstCuentas.SelectedIndex = -1 Then
                MessageBox.Show("Debe seleccionar una cuenta.")
                Return
            End If

            If cboMes.SelectedIndex = -1 Then
                MessageBox.Show("Debe seleccionar un mes.")
                Return
            End If

            Dim monto As Decimal

            If Not Decimal.TryParse(txtMontoRetiro.Text, monto) Then
                MessageBox.Show("Ingrese un monto válido.")
                Return
            End If

            Dim cuentaSeleccionada As Cuenta =
                DirectCast(lstCuentas.SelectedItem, Cuenta)

            Dim mes As Integer = cboMes.SelectedIndex + 1

            cuentaSeleccionada.Retirar(monto, mes)

            MessageBox.Show("Retiro realizado correctamente.")

            ActualizarLista()

            txtMontoRetiro.Clear()
            cboMes.SelectedIndex = -1

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub


    Private Async Sub btnConsultarHistorial_Click(sender As Object, e As EventArgs) Handles btnConsultarHistorial.Click

        Try

            btnConsultarHistorial.Enabled = False

            lblResultadoHistorial.Text = "Consultando..."

            Dim tieneMorosidad As Boolean =
                Await historialServicio.TieneMorosidadAsync(txtCedula.Text)

            If tieneMorosidad Then

                lblResultadoHistorial.Text = "Resultado: Con morosidad"

            Else

                lblResultadoHistorial.Text = "Resultado: Sin morosidad"

            End If

        Catch ex As Exception

            MessageBox.Show(ex.Message)
            lblResultadoHistorial.Text = "Resultado: Error"

        Finally

            btnConsultarHistorial.Enabled = True

        End Try

    End Sub


    Private Sub ActualizarLista()

        lstCuentas.Items.Clear()

        For Each cuenta As Cuenta In cooperativa.ObtenerCuentas()

            lstCuentas.Items.Add(cuenta)

        Next

    End Sub


    Private Sub LimpiarRegistro()

        txtNumeroCuenta.Clear()
        txtNombre.Clear()
        txtSaldoInicial.Clear()

        cboTipoCuenta.SelectedIndex = -1

        txtNumeroCuenta.Focus()

    End Sub

End Class