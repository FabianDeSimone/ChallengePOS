Imports System.ComponentModel
Imports System.Configuration
Imports System.Data.SqlClient
Imports Entities

Public Class DACliente
    Private ReadOnly connectionString As String

    Public Sub New()
        connectionString = ConfigurationManager.ConnectionStrings("Conexion").ConnectionString
    End Sub

    Public Function ObtenerClientes() As List(Of Cliente)
        Dim listaClientes As New List(Of Cliente)
        Dim query As String = "SELECT * from clientes"

        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, con)
                Try
                    con.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim cliente As New Cliente(
                            reader("ID"),
                            reader("Cliente").ToString(),
                            reader("Telefono").ToString(),
                            reader("Correo").ToString()
                        )
                            listaClientes.Add(cliente)
                        End While
                    End Using
                    con.Close()
                Catch ex As Exception
                    MessageBox.Show("Error al obtener clientes: " & ex.Message)
                End Try
            End Using
        End Using
        Return listaClientes
    End Function

    Public Function AgregarCliente(clienteAAgregar As Cliente) As Boolean
        Try
            Dim query As String = "INSERT INTO clientes (Cliente, Telefono, Correo) values (@Cliente, @Telefono, @Correo)"
            Using con As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@Cliente", clienteAAgregar.Cliente)
                    cmd.Parameters.AddWithValue("@Telefono", clienteAAgregar.Telefono)
                    cmd.Parameters.AddWithValue("@Correo", clienteAAgregar.Correo)
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using

            MessageBox.Show("Cliente agregado correctamente.")
            Return True
        Catch ex As SqlException
            MessageBox.Show("Error en la base de datos: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error: " & ex.Message)
        End Try
        Return False
    End Function

    Public Function ModificarCliente(clienteAModificar As Cliente) As Boolean
        Try
            Dim query As String = "UPDATE clientes SET Cliente = @Cliente, Telefono = @Telefono, Correo = @Correo WHERE ID = @ID"
            Using con As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@Cliente", clienteAModificar.Cliente)
                    cmd.Parameters.AddWithValue("@Telefono", clienteAModificar.Telefono)
                    cmd.Parameters.AddWithValue("@Correo", clienteAModificar.Correo)
                    cmd.Parameters.AddWithValue("@ID", clienteAModificar.ID)
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using

            MessageBox.Show("Cliente modificado correctamente.")
            Return True
        Catch ex As SqlException
            MessageBox.Show("Error en la base de datos: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error: " & ex.Message)
        End Try
        Return False
    End Function

    Public Function EliminarCliente(idAEliminar As Integer) As Boolean
        Try
            Dim query As String = "DELETE FROM clientes Where ID = @ID"
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
