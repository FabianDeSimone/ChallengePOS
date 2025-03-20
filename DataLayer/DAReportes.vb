Imports System.Configuration
Imports System.Data.SqlClient
Imports Entities

Public Class DAReportes
    Private ReadOnly connectionString As String

    Public Sub New()
        connectionString = ConfigurationManager.ConnectionStrings("Conexion").ConnectionString
    End Sub

    Public Function ReporteVentasDetallado() As List(Of ReporteVentaDetalle)
        Dim listaReportes As New List(Of ReporteVentaDetalle)
        Dim query As String = "SELECT v.ID AS IDVenta, v.Fecha, v.Total, 
                                  p.Nombre AS NombreProducto, vi.PrecioUnitario, vi.Cantidad, vi.PrecioTotal 
                           FROM Ventas v
                           INNER JOIN VentaItems vi ON v.ID = vi.IDVenta
                           INNER JOIN Productos p ON vi.IDProducto = p.ID"

        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, con)
                Try
                    con.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim reporte As New ReporteVentaDetalle With {
                                .IDVenta = reader("IDVenta"),
                                .Fecha = reader("Fecha"),
                                .Total = reader("Total"),
                                .NombreProducto = reader("NombreProducto"),
                                .PrecioUnitario = reader("PrecioUnitario"),
                                .Cantidad = reader("Cantidad"),
                                .PrecioTotal = reader("PrecioTotal")
                            }
                            listaReportes.Add(reporte)
                        End While
                    End Using
                    con.Close()
                Catch ex As Exception
                    MessageBox.Show("Error al obtener el reporte de ventas: " & ex.Message)
                End Try
            End Using
        End Using

        Return listaReportes
    End Function



End Class
