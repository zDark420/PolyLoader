Imports System

Namespace PolyLoader.JunkCode
	Friend MustInherit Class JunkType
		Protected _name As String

		Protected Sub New()
			MyBase.New()
		End Sub

		Public MustOverride Function [declare]() As String

		Public MustOverride Function define() As String

		Public Function name() As String
			Return Me._name
		End Function

		Public MustOverride Function value() As String
	End Class
End Namespace