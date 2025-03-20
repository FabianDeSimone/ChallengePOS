Imports System.Configuration
Imports Entities
Imports System.Data.SqlClient

Public Class DAVentaItem

    Private ReadOnly connectionString As String
    Public Sub New()
        connectionString = ConfigurationManager.ConnectionStrings("Conexion").ConnectionString
    End Sub

    Public Function AgregarVentaItem(ventaItemAAgregar As VentaItem) As Boolean

        Try
            Dim query As String = "INSERT INTO ventasitems (IDVenta, IDProducto, PrecioUnitario, Cantidad, PrecioTotal) VALUES (@IDVenta, @IDProducto, @PrecioUnitario, @Cantidad, @PrecioTotal)"
            Using con As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@IDVenta", ventaItemAAgregar.IDVenta)
                    cmd.Parameters.AddWithValue("@IDProducto", ventaItemAAgregar.IDProducto)
                    cmd.Parameters.AddWithValue("@PrecioUnitario", ventaItemAAgregar.PrecioUnitario)
                    cmd.Parameters.AddWithValue("@Cantidad", ventaItemAAgregar.Cantidad)
                    cmd.Parameters.AddWithValue("@PrecioTotal", ventaItemAAgregar.PrecioTotal)
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using

            MessageBox.Show("VentaItem agregado correctamente.")
            Return True
        Catch ex As SqlException
            MessageBox.Show("Error en la base de datos: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error: " & ex.Message)
        End Try
        Return False
    End Function

    Public Function ObtenerVentaItems(idVenta) As List(Of VentaItem)
        Dim listaVentaItems As New List(Of VentaItem)
        Dim query As String = "SELECT * from ventasitems Where IDVenta = @IDVenta"

        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, con)
                Try
                    cmd.Parameters.AddWithValue("@IDVenta", idVenta)
                    con.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim venta As New VentaItem(
                            reader("ID"),
                            reader("IDVenta"),
                            reader("IDProducto"),
                            reader("PrecioUnitario"),
                            reader("Cantidad"),
                            reader("PrecioTotal")
                        )
                            listaVentaItems.Add(venta)
                        End While
                    End Using
                    con.Close()
                Catch ex As Exception
                    MessageBox.Show("Error al obtener ventaItems: " & ex.Message)
                End Try
            End Using
        End Using
        Return listaVentaItems
    End Function

    Public Function modificarVentaItem(ventaItemAModificar As VentaItem) As Boolean
        eliminarVentaItem(ventaItemAModificar.IDVenta)
        Try
            Dim query As String = "INSERT INTO ventasitems (IDVenta, IDProducto, PrecioUnitario, Cantidad, PrecioTotal) VALUES (@IDVenta, @IDProducto, @PrecioUnitario, @Cantidad, @PrecioTotal)"
            Using con As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@IDVenta", ventaItemAModificar.IDVenta)
                    cmd.Parameters.AddWithValue("@IDProducto", ventaItemAModificar.IDProducto)
                    cmd.Parameters.AddWithValue("@PrecioUnitario", ventaItemAModificar.PrecioUnitario)
                    cmd.Parameters.AddWithValue("@Cantidad", ventaItemAModificar.Cantidad)
                    cmd.Parameters.AddWithValue("@PrecioTotal", ventaItemAModificar.PrecioTotal)
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using

            MessageBox.Show("VentaItem agregado correctamente.")
            Return True
        Catch ex As SqlException
            MessageBox.Show("Error en la base de datos: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error: " & ex.Message)
        End Try
        Return False
    End Function

    Public Function eliminarVentaItem(idAEliminar As Integer)
        Try
            Dim query As String = "DELETE FROM ventasitems Where ID = @ID"
            Using con As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@ID", idAEliminar)
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using

            MessageBox.Show("Items eliminados correctamente.")
            Return True
        Catch ex As SqlException
            MessageBox.Show("Error en la base de datos: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error: " & ex.Message)
        End Try
        Return False
    End Function
End Class
