Imports BusinessLayer
Imports Entities

Public Class frmModificarCliente

    Public Property ClienteRecibido As Cliente
    Public Sub New(cliente As Cliente)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Me.ClienteRecibido = cliente
    End Sub

    Private Sub frmModificarCliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtCliente.Text = ClienteRecibido.Cliente
        txtCorreo.Text = ClienteRecibido.Correo
        txtTelefono.Text = ClienteRecibido.Telefono
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

    Private Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click
        If Not validaciones() Then
            Return
        End If
        Dim clienteBL As New ClienteCRUD()
        Dim clienteModificado As New Cliente(ClienteRecibido.ID, txtCliente.Text, txtTelefono.Text, txtCorreo.Text)
        Dim resultado = clienteBL.Update(clienteModificado)
        Me.Close()
        Dim formClientes As frmClientes = Application.OpenForms.OfType(Of frmClientes)().FirstOrDefault()
        formClientes.actualizarDGV()

    End Sub
End Class