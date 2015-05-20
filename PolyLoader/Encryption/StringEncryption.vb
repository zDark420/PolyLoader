Imports System
Imports Microsoft.VisualBasic.Strings

Namespace PolyLoader.Encryption
    Friend Class StringEncryption
        Protected Shared _encKey As Char

        Public Shared Sub setKey(k As Char)
            StringEncryption._encKey = k
        End Sub

        Public Shared Function getKey() As Char
            Return StringEncryption._encKey
        End Function

        Public Shared Function encrypt(txt As String) As Integer()
            Dim chArray As Char() = txt.ToCharArray()
            Dim numArray As Integer() = New Integer(chArray.Length - 1) {}
            For index As Integer = 0 To chArray.Length - 1
                numArray(index) = Convert.ToInt32(chArray(index)) Xor (Convert.ToInt32(StringEncryption._encKey) + index) Mod CInt(Byte.MaxValue)
            Next
            Return numArray
        End Function
    End Class
End Namespace