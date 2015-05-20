Imports System

Namespace PolyLoader.JunkCode
	Friend Class JunkChar
		Inherits JunkType
		Private _val As String

		Public Sub New()
			MyBase.New()
			Me._name = Junk.getUnusedName()
			Me._val = Junk.randomString(1)
		End Sub

		Public Overrides Function [declare]() As String
			Return "char"
		End Function

		Public Overrides Function define() As String
			Dim str As String = String.Concat("", Me.[declare]())
			str = String.Concat(str, " ")
			str = String.Concat(str, MyBase.name())
			str = String.Concat(str, " = ")
			Return String.Concat(str, Me.value())
		End Function

		Public Overrides Function value() As String
			Return String.Concat("'", Me._val, "'")
		End Function
	End Class
End Namespace