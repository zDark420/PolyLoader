Imports System
Imports System.Collections.Generic
Imports System.Runtime.CompilerServices
Imports System.Text

Namespace PolyLoader.JunkCode
	Friend Class Junk
		Public Shared rnd As Random

		Private Shared usedNames As List(Of String)

		Shared Sub New()
			Dim guid As System.Guid = System.Guid.NewGuid()
			Junk.rnd = New Random(guid.GetHashCode())
			Junk.usedNames = New List(Of String)()
		End Sub

		Public Sub New()
			MyBase.New()
		End Sub

		Public Shared Function generateCode(ByVal min As Integer, ByVal max As Integer) As String
			Dim str As String = ""
			Dim num As Integer = Junk.rnd.[Next](min, max)
			Dim junkInts As List(Of PolyLoader.JunkCode.JunkInt) = New List(Of PolyLoader.JunkCode.JunkInt)()
			Dim num1 As Integer = 0
			Do
				If (junkInts.Count = 0 OrElse Junk.rnd.[Next](0, 3) <> 0) Then
					Dim randomType As JunkType = Junk.getRandomType()
					If (TypeOf randomType Is PolyLoader.JunkCode.JunkInt) Then
						junkInts.Add(DirectCast(randomType, PolyLoader.JunkCode.JunkInt))
					End If
					str = String.Concat(str, randomType.define())
				Else
					Dim count As Integer = junkInts.Count
					Dim item As PolyLoader.JunkCode.JunkInt = junkInts(Junk.rnd.[Next](count))
					If (Junk.rnd.[Next](0, 2) = 0) Then
						item = New PolyLoader.JunkCode.JunkInt()
						junkInts.Add(item)
						str = String.Concat(str, item.[declare](), " ")
					End If
					Dim junkInt As PolyLoader.JunkCode.JunkInt = junkInts(Junk.rnd.[Next](count))
					Dim item1 As PolyLoader.JunkCode.JunkInt = junkInts(Junk.rnd.[Next](count))
					str = String.Concat(str, item.name(), " = ", junkInt.name())
					str = If(Junk.rnd.[Next](0, 2) <> 0, String.Concat(str, " - "), String.Concat(str, " + "))
					str = String.Concat(str, item1.name())
				End If
				str = String.Concat(str, ";", Environment.NewLine)
				num1 = num1 + 1
			Loop While num1 < num
			Return str
		End Function

		Public Shared Function generateDeclares(ByVal min As Integer, ByVal max As Integer) As String
			Dim str As String = ""
			Dim num As Integer = Junk.rnd.[Next](min, max)
			Dim num1 As Integer = 0
			Do
				Dim randomType As JunkType = Junk.getRandomType()
				str = String.Concat(str, randomType.define())
				str = String.Concat(str, ";", Environment.NewLine)
				num1 = num1 + 1
			Loop While num1 < num
			Return str
		End Function

		Public Shared Function generateFunctions(ByVal min As Integer, ByVal max As Integer) As String
			Return ""
		End Function

		Private Shared Function getRandomType() As JunkType
			Select Case Junk.rnd.[Next](0, 3)
				Case 0
					Return New JunkInt()
				Case 1
					Return New JunkChar()
				Case 2
					Return New JunkString()
			End Select
			Return New JunkInt()
		End Function

		Public Shared Function getUnusedName() As String
			Dim str As String = Junk.randomString(10)
			While Junk.usedNames.Exists(Function(x As String) x = str)
				str = Junk.randomString(10)
			End While
			Return str
		End Function

		Public Shared Function randomString(Optional ByVal size As Integer = 10) As String
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			Dim num As Integer = 0
			Do
				Dim chr As Char = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Junk.rnd.NextDouble() + 65)))
				stringBuilder.Append(chr)
				num = num + 1
			Loop While num < size
			Return stringBuilder.ToString()
		End Function
	End Class
End Namespace