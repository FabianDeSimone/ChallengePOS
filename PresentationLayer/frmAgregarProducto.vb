﻿Imports System.Text.RegularExpressions
Imports BusinessLayer
Imports Entities

Public Class frmAgregarProducto
    Private Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click
        If Not validaciones() Then
            Return
        End If

        Dim productoAAgregar As New Producto(0, txtNombre.Text, Convert.ToDecimal(txtPrecio.Text), txtCategoria.Text)
        Dim productoBL As New ProductoCRUD()
        Dim resultado = productoBL.Create(productoAAgregar)
        Me.Close()
        Dim frmProductos As frmProductos = Application.OpenForms.OfType(Of frmProductos)().FirstOrDefault()
        frmProductos.actualizarDGV()


    End Sub


    Private Function validaciones() As Boolean
        If String.IsNullOrWhiteSpace(txtNombre.Text) Then
            MessageBox.Show("El nombre del producto es obligatorio.")
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtPrecio.Text) Then
            MessageBox.Show("El precio es obligatorio.")
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtCategoria.Text) Then
            MessageBox.Show("La categoria es obligatorio.")
            Return False
        End If
        Dim precioRegex As New Regex("^\d+(\.\d{2})?$")
        If Not precioRegex.IsMatch(txtPrecio.Text) Then
            MessageBox.Show("Precio inválido")
            Return False
        End If
        Return True

    End Function

    Private Sub txtPrecio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrecio.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If


        If e.KeyChar = "."c AndAlso txtPrecio.Text.Contains(".") Then
            e.Handled = True
        End If

        If e.KeyChar = "."c Then
            If txtPrecio.Text.Contains(".") Then
                Dim parteDecimal As String = txtPrecio.Text.Substring(txtPrecio.Text.LastIndexOf("."c) + 1)
                If parteDecimal.Length >= 2 Then
                    e.Handled = True
                End If
            End If
        End If

        If Char.IsDigit(e.KeyChar) AndAlso txtPrecio.Text.Contains(".") Then
            Dim parteDecimal As String = txtPrecio.Text.Substring(txtPrecio.Text.LastIndexOf("."c) + 1)
            If parteDecimal.Length >= 2 Then
                e.Handled = True
            End If
        End If
    End Sub







    Private Sub frmAgregarProducto_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

End Class