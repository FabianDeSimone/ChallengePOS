Imports BusinessLayer
Imports Entities

Public Class frmVentas


    Private listaProductos As List(Of Producto)
    Private listaClientes As List(Of Cliente)

    Private Sub frmVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        actualizarDGV()
        dgvVentas.ClearSelection()
        cboxFiltro.SelectedIndex = 0
    End Sub



    Public Sub actualizarDGV()
        Dim ventaCRUD As New VentaCRUD()
        Dim listaVentas As List(Of Venta) = ventaCRUD.Read(0)
        Dim clienteCRUD As New ClienteCRUD()
        listaClientes = clienteCRUD.Read(0)
        dgvVentas.Rows.Clear()
        For Each venta As Venta In listaVentas
            Dim cliente As Cliente = listaClientes.FirstOrDefault(Function(c) c.ID = venta.IDCliente)
            Dim nombreCliente As String = If(cliente IsNot Nothing, cliente.Cliente, "Cliente no encontrado")
            Dim row As String() = New String() {venta.ID, nombreCliente, venta.Fecha, venta.Total}
            dgvVentas.Rows.Add(row)
        Next

    End Sub

    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        Dim filtro As String = txtBuscar.Text.Trim().ToLower()
        Dim columnaFiltro As String = cboxFiltro.SelectedItem.ToString()

        For Each fila As DataGridViewRow In dgvVentas.Rows
            If fila.IsNewRow Then Continue For

            Dim valorCelda As String = fila.Cells(columnaFiltro).Value.ToString().ToLower()
            fila.Visible = valorCelda.Contains(filtro)
        Next
    End Sub

    Private Sub cboxFiltro_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboxFiltro.SelectedIndexChanged
        txtBuscar.Clear()
    End Sub

    Private Sub btnInformacion_Click(sender As Object, e As EventArgs) Handles btnInformacion.Click
        If dgvVentas.SelectedRows.Count > 0 Then
            Dim idVenta As Integer = Convert.ToInt32(dgvVentas.SelectedRows(0).Cells("ID").Value)

            Dim frmDetalleVenta As New frmDetalleVenta(idVenta)
            frmDetalleVenta.Show()
        Else
            MessageBox.Show("Por favor, seleccione una venta para ver los detalles.", "Selección de venta", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If dgvVentas.SelectedRows.Count > 0 Then
            Dim idVenta As Integer = Convert.ToInt32(dgvVentas.SelectedRows(0).Cells("ID").Value)

            Dim frmModificarVenta As New frmModificarVenta(idVenta)
            frmModificarVenta.Show()
        Else
            MessageBox.Show("Por favor, seleccione una venta para modificar.", "Modificacion de venta", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim result As DialogResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este cliente?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Dim ventaCRUD As New VentaCRUD
            Dim clienteSeleccionado As DataGridViewRow = dgvVentas.SelectedRows(0)
            Dim idAEliminar As Integer = clienteSeleccionado.Cells("ID").Value
            ventaCRUD.Delete(idAEliminar)
            Dim listaVentaItems As New List(Of VentaItem)
            Dim ventaItemCRUD As New VentaItemCRUD
            listaVentaItems = ventaItemCRUD.Read(idAEliminar)
            For Each item In listaVentaItems
                ventaItemCRUD.Delete(item.ID)
            Next
            actualizarDGV()
        Else
            MessageBox.Show("Eliminación cancelada.")
        End If
    End Sub
End Class


