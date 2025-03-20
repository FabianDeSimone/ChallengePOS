Imports System.Configuration
Imports Entities
Imports System.Data.SqlClient

Public Class DAVenta

    Private ReadOnly connectionString As String

    Public Sub New()
        connectionString = ConfigurationManager.ConnectionStrings("Conexion").ConnectionString
    End Sub

    Public Function ObtenerVentas() As List(Of Venta)
        Dim listaVentas As New List(Of Venta)
        Dim query As String = "SELECT * from ventas"

        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, con)
                Try
                    con.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim venta As New Venta(
                            reader("ID"),
                            reader("IDCliente").ToString(),
                            Convert.ToDateTime(reader("Fecha")),
                            reader("Total").ToString()
                        )
                            listaVentas.Add(venta)
                        End While
                    End Using
                    con.Close()
                Catch ex As Exception
                    MessageBox.Show("Error al obtener ventas: " & ex.Message)
                End Try
            End Using
        End Using
        Return listaVentas
    End Function

    Public Function AgregarVenta(ventaAAgregar As Venta) As Integer

        Try
            Dim idVenta As Integer
            Dim query As String = "INSERT INTO ventas (IDCliente, Fecha, Total) OUTPUT INSERTED.ID VALUES (@IDCliente, @Fecha, @Total)"
            Using con As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@IDCliente", ventaAAgregar.IDCliente)
                    cmd.Parameters.AddWithValue("@Fecha", ventaAAgregar.Fecha)
                    cmd.Parameters.AddWithValue("@Total", ventaAAgregar.Total)
                    con.Open()
                    idVenta = Convert.ToInt32(cmd.ExecuteScalar())
                    con.Close()
                End Using
            End Using

            MessageBox.Show("Venta agregada correctamente.")
            Return idVenta
        Catch ex As SqlException
            MessageBox.Show("Error en la base de datos: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error: " & ex.Message)
        End Try
        Return False
    End Function

    Public Function modificarVenta(ventaAModificar As Venta) As Boolean
        Try
            Dim query As String = "UPDATE ventas SET Total = @Total WHERE ID = @ID"
            Using con As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@Total", ventaAModificar.Total)
                    cmd.Parameters.AddWithValue("@ID", ventaAModificar.ID)
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using

            MessageBox.Show("Venta modificada correctamente.")
            Return True
        Catch ex As SqlException
            MessageBox.Show("Error en la base de datos: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error: " & ex.Message)
        End Try
        Return False
    End Function
    Public Function EliminarVenta(idAEliminar As Integer)
        Try
            Dim query As String = "DELETE FROM ventas Where ID = @ID"
            Using con As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@ID", idAEliminar)
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using

            MessageBox.Show("Venta eliminada correctamente.")
            Return True
        Catch ex As SqlException
            MessageBox.Show("Error en la base de datos: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error: " & ex.Message)
        End Try
        Return False
    End Function

    Public Function ReporteVentas() As DataTable
        Dim dt As New DataTable()
        Dim query As String = "SELECT v.ID, c.Nombre AS Cliente, v.Fecha, v.Total FROM ventas v JOIN clientes c ON v.IDCliente = c.ID"

        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, con)
                Try
                    con.Open()
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                    con.Close()
                Catch ex As Exception
                    MessageBox.Show("Error al obtener ventas: " & ex.Message)
                End Try
            End Using
        End Using

        Return dt
    End Function


End Class
