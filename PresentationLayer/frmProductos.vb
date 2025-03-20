Imports BusinessLayer
Imports Entities

Public Class frmProductos


    Private Sub frmProductos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        actualizarDGV()
        dgvProductos.ClearSelection()
        cboxFiltro.SelectedIndex = 0
    End Sub
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim frmAgregarProducto As New frmAgregarProducto()
        frmAgregarProducto.Show()
    End Sub


    Public Sub actualizarDGV()
        Dim productoCRUD As New ProductoCRUD()
        Dim listaProductos As List(Of Producto) = productoCRUD.Read(1)
        dgvProductos.Rows.Clear()
        For Each producto As Producto In listaProductos
            Dim row As String() = New String() {producto.ID, producto.Nombre, producto.Precio, producto.Categoria}
            dgvProductos.Rows.Add(row)
        Next
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If dgvProductos.SelectedRows.Count = 0 Then
            MessageBox.Show("Debe seleccionar un cliente")
            Return
        End If
        Dim productoSeleccionado As DataGridViewRow = dgvProductos.SelectedRows(0)
        Dim productoAModificar As New Producto(productoSeleccionado.Cells("ID").Value, productoSeleccionado.Cells("Nombre").Value.ToString(),
        productoSeleccionado.Cells("Precio").Value(),
        productoSeleccionado.Cells("Categoria").Value.ToString())
        Dim productoCRUD As New ProductoCRUD()
        Dim formModificarProducto As New frmModificarProducto(productoAModificar)
        formModificarProducto.Show()
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim result As DialogResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este cliente?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Dim productoCRUD As New ProductoCRUD
            Dim productoSeleccionado As DataGridViewRow = dgvProductos.SelectedRows(0)
            Dim idAEliminar As Integer = productoSeleccionado.Cells("ID").Value
            productoCRUD.Delete(idAEliminar)
            actualizarDGV()
        Else
            MessageBox.Show("Eliminación cancelada.")
        End If
    End Sub

    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        Dim filtro As String = txtBuscar.Text.Trim().ToLower()
        Dim columnaFiltro As String = cboxFiltro.SelectedItem.ToString()

        For Each fila As DataGridViewRow In dgvProductos.Rows
            If fila.IsNewRow Then Continue For

            Dim valorCelda As String = fila.Cells(columnaFiltro).Value.ToString().ToLower()
            fila.Visible = valorCelda.Contains(filtro)
        Next
    End Sub

    Private Sub cboxFiltro_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboxFiltro.SelectedIndexChanged
        txtBuscar.Clear()
    End Sub
End Class