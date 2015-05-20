Imports System
Imports System.CodeDom.Compiler
Imports System.Configuration
Imports System.Runtime.CompilerServices

Namespace PolyLoader.Properties
	<CompilerGenerated>
	<GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")>
	Friend NotInheritable Class Settings
		Inherits ApplicationSettingsBase
		Private Shared defaultInstance As Settings

		Public ReadOnly Shared Property [Default] As Settings
			Get
				Return Settings.defaultInstance
			End Get
		End Property

		Shared Sub New()
			Settings.defaultInstance = DirectCast(SettingsBase.Synchronized(New Settings()), Settings)
		End Sub

		Public Sub New()
			MyBase.New()
		End Sub
	End Class
End Namespace