Imports System.Runtime.CompilerServices
Imports BusinessLayer
Imports Entities
Public Class frmAgregarCliente
    Private Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click
        If Not validaciones() Then
            Return
        End If

        Dim clienteAAgregar As New Cliente(0, txtCliente.Text, txtTelefono.Text, txtCorreo.Text)
        Dim clienteBL As New ClienteCRUD()
        Dim resultado = clienteBL.Create(clienteAAgregar)
        Me.Close()
        Dim formClientes As frmClientes = Application.OpenForms.OfType(Of frmClientes)().FirstOrDefault()
        formClientes.actualizarDGV()

    End Sub

    Private Function validaciones() As Boolean
        If String.IsNullOrWhiteSpace(txtCliente.Text) Then
            MessageBox.Show("El nombre del cliente es obligatorio.")
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtTelefono.Text) Then
            MessageBox.Show("El teléfono es obligatorio.")
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtCorreo.Text) Then
            MessageBox.Show("El correo es obligatorio.")
            Return False
        End If

        Dim correoRegex As New System.Text.RegularExpressions.Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
        If Not correoRegex.IsMatch(txtCorreo.Text) Then
            MessageBox.Show("El correo electrónico ingresado no es válido.")
            Return False
        End If
        Return True
    End Function

    Private Sub frmAgregarCliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtTelefono_TextChanged(sender As Object, e As EventArgs) Handles txtTelefono.TextChanged

    End Sub

    Private Sub txtTelefono_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTelefono.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class