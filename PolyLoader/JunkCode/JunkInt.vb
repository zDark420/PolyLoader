Imports System

Namespace PolyLoader.JunkCode
	Friend Class JunkInt
		Inherits JunkType
		Private _val As Integer

		Public Sub New()
			MyBase.New()
			Me._name = Junk.getUnusedName()
			Me._val = Junk.rnd.[Next](0, 10000)
		End Sub

		Public Overrides Function [declare]() As String
			Return "int"
		End Function

		Public Overrides Function define() As String
			Dim str As String = String.Concat("", Me.[declare]())
			str = String.Concat(str, " ")
			str = String.Concat(str, MyBase.name())
			str = String.Concat(str, " = ")
			Return String.Concat(str, Me.value())
		End Function

		Public Overrides Function value() As String
			Return Me._val.ToString()
		End Function
	End Class
End Namespace