Imports System.Configuration
Imports System.Data.SqlClient

Public Class DatabaseConnection

    Private ReadOnly connectionString As String

    Public Sub New()
        connectionString = ConfigurationManager.ConnectionStrings("Conexion").ConnectionString
    End Sub

    Public Function GetConnection() As SqlConnection
        Return New SqlConnection(connectionString)
    End Function

    Public Function TestConnection() As Boolean
        Try
            Using conn As SqlConnection = GetConnection()
                conn.Open()
                Return True ' Conexión exitosa
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Fallo de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False ' Fallo en la conexión
        End Try
    End Function

End Class
