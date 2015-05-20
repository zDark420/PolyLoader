Imports PolyLoader.JunkCode
Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace PolyLoader.Randomization
	Friend Class CppFile
		Inherits BaseFile
        Public Sub New(fn As String)
            Me._fileName = fn
            Me._fileData = File.ReadAllText(Me._fileName)
            For i As Integer = 0 To CInt(Me.blocks.Length) - 1
                Me.blocks(i) = New List(Of String)()
            Next
        End Sub

        Protected Overrides Function addCode() As String
            If (Me._pJunkDeclaresOnly) Then
                Return String.Concat(Junk.generateDeclares(Me._pJunkMinLength, Me._pJunkMaxLength), Environment.NewLine)
            End If
            Return String.Concat(Junk.generateDeclares(Me._pJunkMinLength, Me._pJunkMaxLength), Environment.NewLine)
        End Function
    End Class
End Namespace