Imports System.Drawing.Text
Imports BusinessLayer
Imports Entities

Public Class frmClientes
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs)
        Dim frmAgregarCliente As New frmAgregarCliente
        frmAgregarCliente.Show()
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs)
        Dim resultado As DialogResult
        resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar este cliente?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If resultado Then
            MessageBox.Show("Cliente Eliminado correctamente")
        End If
    End Sub

    Private Sub frmClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        actualizarDGV()
        dgvClientes.ClearSelection()
        cboxFiltro.SelectedIndex = 0
    End Sub

    Private Sub btnAgregar_Click_1(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim frmAgregarCliente As New frmAgregarCliente()
        frmAgregarCliente.Show()
    End Sub

    Public Sub actualizarDGV()
        Dim clienteCRUD As New ClienteCRUD()
        Dim listaClientes As List(Of Cliente) = clienteCRUD.Read(0)
        dgvClientes.Rows.Clear()
        For Each cliente As Cliente In listaClientes
            Dim row As String() = New String() {cliente.ID, cliente.Cliente, cliente.Telefono, cliente.Correo}
            dgvClientes.Rows.Add(row)
        Next
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If dgvClientes.SelectedRows.Count = 0 Then
            MessageBox.Show("Debe seleccionar un cliente")
            Return
        End If
        Dim clienteSeleccionado As DataGridViewRow = dgvClientes.SelectedRows(0)
        Dim clienteAModificar As New Cliente(clienteSeleccionado.Cells("ID").Value, clienteSeleccionado.Cells("Cliente").Value.ToString(),
        clienteSeleccionado.Cells("Telefono").Value.ToString(),
        clienteSeleccionado.Cells("Correo").Value.ToString())
        Dim clienteCRUD As New ClienteCRUD()
        Dim formModificarCliente As New frmModificarCliente(clienteAModificar)
        formModificarCliente.Show()
    End Sub

    Private Sub btnEliminar_Click_1(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim result As DialogResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este cliente?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Dim clienteCRUD As New ClienteCRUD
            Dim clienteSeleccionado As DataGridViewRow = dgvClientes.SelectedRows(0)
            Dim idAEliminar As Integer = clienteSeleccionado.Cells("ID").Value
            clienteCRUD.Delete(idAEliminar)
            actualizarDGV()
        Else
            MessageBox.Show("Eliminación cancelada.")
        End If
    End Sub

    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        Dim filtro As String = txtBuscar.Text.Trim().ToLower()
        Dim columnaFiltro As String = cboxFiltro.SelectedItem.ToString()

        For Each fila As DataGridViewRow In dgvClientes.Rows
            If fila.IsNewRow Then Continue For

            Dim valorCelda As String = fila.Cells(columnaFiltro).Value.ToString().ToLower()
            fila.Visible = valorCelda.Contains(filtro)
        Next
    End Sub

    Private Sub cboxFiltro_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboxFiltro.SelectedIndexChanged
        txtBuscar.Clear()
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs)

    End Sub
End Class