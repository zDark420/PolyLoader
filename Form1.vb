Imports Microsoft.VisualBasic
Public Class Form1
    Public etsy As String
    Private Sub ReactorButton1_Click(sender As Object, e As System.EventArgs) Handles ReactorButton1.Click
        On Error Resume Next
        'etsy = PolyLoader.JunkCode.Junk.generateCode(6, 6) 'generates random code strings/ints
        etsy = PolyLoader.JunkCode.Junk.generateDeclares(6, 6) + PolyLoader.JunkCode.Junk.generateCode(6, 6)
        'MsgBox(PolyLoader.JunkCode.Junk.generateCode(6, 6))
        If TextBox1.Text = "" Then
            TextBox1.Text = etsy
        Else
            TextBox1.Text = TextBox1.Text & vbNewLine & etsy
        End If
    End Sub

    Private Sub ReactorTheme1_Click(sender As Object, e As System.EventArgs)

    End Sub

    Private Sub ReactorButton2_Click(sender As Object, e As System.EventArgs) Handles ReactorButton2.Click
        TextBox1.Text = ""
    End Sub
End Class