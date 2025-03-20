Imports Entities
Imports System.Configuration
Imports System.Data.SqlClient

Public Class DAProducto
    Private ReadOnly connectionString As String

    Public Sub New()
        connectionString = ConfigurationManager.ConnectionStrings("Conexion").ConnectionString
    End Sub

    Public Function ObtenerProductos() As List(Of Producto)
        Dim listaProductos As New List(Of Producto)
        Dim query As String = "SELECT * from productos"

        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, con)
                Try
                    con.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim producto As New Producto(
                            reader("ID"),
                            reader("Nombre").ToString(),
                            Convert.ToDecimal(reader("Precio")),
                            reader("Categoria").ToString()
                        )
                            listaProductos.Add(producto)
                        End While
                    End Using
                    con.Close()
                Catch ex As Exception
                    MessageBox.Show("Error al obtener productos: " & ex.Message)
                End Try
            End Using
        End Using
        Return listaProductos
    End Function

    Public Function agregarProducto(productoAAgregar As Producto) As Boolean
        Try
            Dim query As String = "INSERT INTO productos (Nombre, Precio, Categoria) values (@Nombre, @Precio, @Categoria)"
            Using con As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@Nombre", productoAAgregar.Nombre)
                    cmd.Parameters.AddWithValue("@Precio", productoAAgregar.Precio)
                    cmd.Parameters.AddWithValue("@Categoria", productoAAgregar.Categoria)
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using

            MessageBox.Show("Producto agregado correctamente.")
            Return True
        Catch ex As SqlException
            MessageBox.Show("Error en la base de datos: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error: " & ex.Message)
        End Try
        Return False
    End Function

    Public Function modificarProducto(productoAModificar As Producto) As Boolean
        Try
            Dim query As String = "UPDATE productos SET Nombre = @Nombre, Precio = @Precio, Categoria = @Categoria WHERE ID = @ID"
            Using con As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@Nombre", productoAModificar.Nombre)
                    cmd.Parameters.AddWithValue("@Precio", productoAModificar.Precio)
                    cmd.Parameters.AddWithValue("@Categoria", productoAModificar.Categoria)
                    cmd.Parameters.AddWithValue("@ID", productoAModificar.ID)
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using

            MessageBox.Show("Producto modificado correctamente.")
            Return True
        Catch ex As SqlException
            MessageBox.Show("Error en la base de datos: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error: " & ex.Message)
        End Try
        Return False
    End Function

    Public Function eliminarProducto(idAEliminar As Integer) As Boolean
        Try
            Dim query As String = "DELETE FROM productos Where ID = @ID"
            Using con As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@ID", idAEliminar)
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using

            MessageBox.Show("Cliente eliminado correctamente.")
            Return True
        Catch ex As SqlException
            MessageBox.Show("Error en la base de datos: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error: " & ex.Message)
        End Try
        Return False
    End Function
End Class
