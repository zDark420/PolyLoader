Imports System
Imports System.Windows.Forms

Namespace PolyLoader
	Friend Class Program
		<STAThread>
		Private Shared Sub Main()
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			Application.Run(New Form1())
		End Sub
	End Class
End Namespace