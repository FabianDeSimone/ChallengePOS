Imports BusinessLayer
Imports Entities

Public Class frmNuevaVenta

    Private listaProductos As List(Of Producto)
    Private listaClientes As List(Of Cliente)

    Private Sub FrmNuevaVenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim productoCRUD As New ProductoCRUD()
        listaProductos = productoCRUD.Read(0)
        For Each producto As Producto In listaProductos
            cboxProductos.Items.Add(producto.Nombre)
        Next
        Dim clienteCRUD As New ClienteCRUD()
        listaClientes = clienteCRUD.Read(0)
        For Each cliente As Cliente In listaClientes
            cboxCliente.Items.Add(cliente.Cliente)
        Next
        txtCantidad.Focus()
        txtCantidad.SelectionStart = txtCantidad.Text.Length
        txtCantidad.ContextMenuStrip = New ContextMenuStrip()
    End Sub

    Private Sub CboxProductos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboxProductos.SelectedIndexChanged
        If cboxProductos.SelectedItem IsNot Nothing Then
            Dim productoSeleccionado As Producto = listaProductos.FirstOrDefault(Function(p) p.Nombre = cboxProductos.SelectedItem.ToString())
            If productoSeleccionado IsNot Nothing Then
                txtPrecio.Text = productoSeleccionado.Precio.ToString("C")
            End If
        End If
    End Sub

    Private Sub TxtCantidad_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCantidad.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

        If txtCantidad.Text.Length = 0 AndAlso e.KeyChar = "0"c Then
            e.Handled = True
        End If
    End Sub

    Private Sub TxtCantidad_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCantidad.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If

        If e.Shift AndAlso e.KeyCode = Keys.Insert Then
            e.SuppressKeyPress = True
        End If
    End Sub







    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtCantidad.TextChanged

    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        If Not Validaciones() Then
            Return
        End If
        Dim productoSeleccionado As Producto = listaProductos(cboxProductos.SelectedIndex)
        Dim cantidad As Integer = Convert.ToInt32(txtCantidad.Text)
        Dim subtotal As Decimal = productoSeleccionado.Precio * cantidad
        For Each row As DataGridViewRow In dgvNuevaVenta.Rows
            If row.Cells("ID").Value = productoSeleccionado.ID Then
                Dim cantidadExistente As Integer = CInt(row.Cells("Cantidad").Value)
                row.Cells("Cantidad").Value = cantidadExistente + cantidad
                row.Cells("Subtotal").Value = (cantidadExistente + cantidad) * productoSeleccionado.Precio
                ActualizarTotal()
                Return
            End If
        Next
        dgvNuevaVenta.Rows.Add(productoSeleccionado.ID, productoSeleccionado.Nombre, productoSeleccionado.Precio, cantidad, subtotal)
        ActualizarTotal()
        txtCantidad.Text = 1
        cboxProductos.SelectedIndex = -1
        txtPrecio.Clear()
    End Sub

    Private Function Validaciones()
        If cboxProductos.SelectedIndex = -1 Then
            MessageBox.Show("Seleccione un producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If txtCantidad.Text = "" OrElse Not IsNumeric(txtCantidad.Text) OrElse CInt(txtCantidad.Text) <= 0 Then
            MessageBox.Show("Ingrese una cantidad válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function

    Private Sub ActualizarTotal()
        Dim total As Decimal = 0

        For Each row As DataGridViewRow In dgvNuevaVenta.Rows
            total += CDec(row.Cells("Subtotal").Value)
        Next

        txtTotal.Text = total.ToString("0.00")
    End Sub

    Private Sub TxtLimpiar_Click(sender As Object, e As EventArgs) Handles txtLimpiar.Click
        txtCantidad.Text = 1
        cboxProductos.SelectedIndex = -1
        txtPrecio.Clear()
    End Sub

    Private Sub BtnBorrarSeleccionado_Click(sender As Object, e As EventArgs) Handles btnBorrarSeleccionado.Click

        If dgvNuevaVenta.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = dgvNuevaVenta.SelectedRows(0)
            dgvNuevaVenta.Rows.Remove(row)
            ActualizarTotal()
        Else
            MessageBox.Show("Seleccione un producto para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BtnFinalizar_Click(sender As Object, e As EventArgs) Handles btnFinalizar.Click
        If txtTotal.Text = "" Then
            MessageBox.Show("Error, no hay ningun producto seleccionado para cerrar la venta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If cboxCliente.SelectedIndex = -1 Then
            MessageBox.Show("Por favor, seleccione un cliente.")
            Return
        End If
        Dim clienteSeleccionado = listaClientes(cboxCliente.SelectedIndex)
        Dim fecha As Date = Date.Now
        Dim ventaAAgregar As New Venta(0, clienteSeleccionado.ID, fecha, Convert.ToDecimal(txtTotal.Text))
        Dim ventaBL As New VentaCRUD()
        Dim idVenta = ventaBL.Create(ventaAAgregar)
        Dim listaVentaItems As New List(Of VentaItem)
        For Each fila As DataGridViewRow In dgvNuevaVenta.Rows
            If Not fila.IsNewRow Then
                Dim nombreProducto As String = Convert.ToString(fila.Cells("Nombre").Value)
                Dim producto As Producto = listaProductos.FirstOrDefault(Function(p) p.Nombre = nombreProducto)
                If producto IsNot Nothing Then
                    Dim precioUnitario As Decimal = Convert.ToDecimal(fila.Cells("Precio").Value)
                    Dim cantidad As Integer = Convert.ToInt32(fila.Cells("Cantidad").Value)
                    Dim precioTotal As Decimal = Convert.ToDecimal(fila.Cells("Subtotal").Value)
                    Dim ventaItem As New VentaItem(0, idVenta, producto.ID, precioUnitario, cantidad, precioTotal)
                    listaVentaItems.Add(ventaItem)
                End If
            End If
        Next
        Dim ventaItemBL As New VentaItemCRUD()
        For Each itemVenta In listaVentaItems
            ventaItemBL.Create(itemVenta)
        Next
    End Sub
End Class