Imports PolyLoader.Encryption
Imports PolyLoader.JunkCode
Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Resources
Imports System.Threading
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports System.Text
Imports PolyLoader

Public Class Form3
    Private compileThread As Thread

    Private compiling As Boolean

    Private Sub Form3_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False

        txtCompilerPath.Text = "C:\Program Files (x86)\Microsoft Visual Studio 12.0\VC\bin\cl.exe" '"C:\Program Files (x86)\Microsoft Visual Studio 12.0\VC\bin\cl.exe"
    End Sub



    Private Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click


        Dim openFileDialog As System.Windows.Forms.OpenFileDialog = New System.Windows.Forms.OpenFileDialog() With
                {
                    .Title = "Select hack file.",
                    .Filter = "Hack file | *.zip"
                }
        openFileDialog.InitialDirectory = "C:\csgohack\New folder"
        If (openFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
            Me.txtHackPath.Text = openFileDialog.FileName
        End If
    End Sub



    Private Sub compile(data As Object)
        On Error Resume Next
        Dim ms As Integer = 4
        Me.setProgressbar(0, ms)
        Me.setStatus("Unzipping")
        Dim hackFile As HackFile = DirectCast(data, HackFile)
        Dim str1 As String = Path.GetFileNameWithoutExtension(Me.txtHackPath.Text) + "/"
        Dim ch As Char = Junk.randomString(1).ToCharArray()(0)
        hackFile.unzip(str1, ch)
        StringEncryption.setKey(ch)
        MsgBox("Compiling:" & vbLf & vbLf + File.ReadAllText(str1 & Convert.ToString("msg.txt")), , "Hack info")
        Me.setProgressbar(1, ms)
        Me.setStatus("Randomizing")
        Dim str2 As String = Path.GetFileNameWithoutExtension(Me.txtHackPath.Text) + "_R/"
        hackFile.randomizeAllFiles(str1, str2)
        Me.setProgressbar(2, ms)
        Me.setStatus("Compiling")
        Dim str3 As String = Path.GetDirectoryName(Me.txtCompilerPath.Text) + "\"
        Dim startInfo As New ProcessStartInfo()
        startInfo.FileName = "cmd.exe"
        Dim str4 As String = (Convert.ToString((Convert.ToString((Convert.ToString("/C " + "cd ") & str2) + "&" + """") & str3) + "vcvars32.bat""&" + """") & str3) + "cl.exe"" " + File.ReadAllText(str1 & Convert.ToString("command.txt"))
        If Me.checkCompOutput.Checked Then
            str4 += "&pause"
        Else
            startInfo.UseShellExecute = False
            startInfo.RedirectStandardInput = True
            startInfo.WindowStyle = ProcessWindowStyle.Hidden
            startInfo.CreateNoWindow = True
        End If
        startInfo.Arguments = str4
        Process.Start(startInfo).WaitForExit()
        Me.setProgressbar(3, ms)
        Me.setStatus("Cleaning up")
        Dim strArray As String() = File.ReadAllText(str1 & Convert.ToString("inout.txt")).Split(New String(0) {Environment.NewLine}, StringSplitOptions.None)
        If File.Exists(strArray(1)) Then
            '    File.Delete(strArray(1))
        End If
        Dim flag As Boolean = False
        If File.Exists(str2 + strArray(0)) Then
            File.Move(str2 + strArray(0), strArray(1))
            flag = True
        End If
        '   Directory.Delete(str1, True)
        '  Directory.Delete(str2, True)
        Me.setProgressbar(4, ms)
        Me.setStatus("Done!")
        If flag Then
            MsgBox("File saved as: " + strArray(1), , "Compiling Complete!")
        Else
            MsgBox("Something went wrong when compiling the randomized source!", , "Error")
        End If
        Me.compiling = False
    End Sub

    '=======================================================
    'Service provided by Telerik (www.telerik.com)
    'Conversion powered by NRefactory.
    'Twitter: @telerik
    'Facebook: facebook.com/telerik
    '=======================================================



    Private Sub findCompiler()
        Me.setStatus("Finding compiler path...")
        Me.txtCompilerPath.Text = Me.findCompilerPath()
        If (Me.txtCompilerPath.Text = "") Then
            Me.setStatus("Couldn't locate the path of the compiler.")
            Return
        End If
        Me.setStatus("Found compiler path.")
    End Sub

    Private Function findCompilerPath() As String
        Dim str As String = ""
        str = Me.findRecursive(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), 0, 3)
        If (str <> "") Then
            Return String.Concat(str, "\cl.exe")
        End If
        str = Me.findRecursive(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), 0, 3)
        If (str <> "") Then
            Return String.Concat(str, "\cl.exe")
        End If
        Dim drives As System.IO.DriveInfo() = System.IO.DriveInfo.GetDrives()
        Dim num As Integer = 0
        Do
            Dim driveInfo As System.IO.DriveInfo = drives(num)
            If (driveInfo.ToString() <> "" AndAlso driveInfo.IsReady) Then
                str = Me.findRecursive(driveInfo.ToString(), 0, 4)
                If (str <> "") Then
                    Return String.Concat(str, "\cl.exe")
                End If
            End If
            num = num + 1
        Loop While num < CInt(drives.Length)
        Return str
    End Function


    Private Function findRecursive(ByVal dir As String, ByVal depth As Integer, ByVal maxDepth As Integer) As String
        Dim str As String = ""
        Dim flag As Boolean = False
        Dim flag1 As Boolean = False
        If (maxDepth <> 0 AndAlso depth >= maxDepth) Then
            Return ""
        End If
        Try
            Dim directories As String() = Directory.GetDirectories(dir)
            Dim num As Integer = 0
            Do
                Dim str1 As String = directories(num)
                Dim files As String() = Directory.GetFiles(str1, "cl.exe")
                If (str1.IndexOf("12.0") = -1 OrElse CInt(files.Length) <= 0) Then
                    Dim str2 As String = Me.findRecursive(str1, depth + 1, maxDepth)
                    If (str2 <> "") Then
                        str = str2
                        flag = True
                    End If
                ElseIf (CInt(Directory.GetFiles(str1, "vcvars32.bat").Length) > 0) Then
                    str = str1
                    flag = True
                End If
                If (flag) Then
                    Exit Do
                End If
                If (flag) Then
                    Exit Do
                End If
                num = num + 1
            Loop While num < CInt(directories.Length)
            If (Not flag) Then
                If (Not flag) Then
                    flag1 = True
                End If
            End If
        Catch
            flag1 = True
        End Try
        If (flag OrElse Not flag1) Then
            flag = False
            Return str
        End If
        flag1 = False
        Return ""
    End Function


    Private Sub setProgressbar(ByVal cs As Integer, ByVal ms As Integer)
        ProgressBar1.Value = CInt((CSng(cs) / CSng(ms) * 100.0!))
    End Sub

    Private Sub setStatus(ByVal m As String)
        Label1.Text = m
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim hackFile As PolyLoader.HackFile = New PolyLoader.HackFile(Me.txtHackPath.Text)
        If (Not hackFile.checkFile()) Then
            MsgBox("Hack File is not valid!", MsgBoxStyle.Critical)
            Return
        End If
        If (compiling) Then
            MsgBox("I am still compiling :(", MsgBoxStyle.Information)
            Return
        End If
        Me.compiling = True
        Me.compileThread = New Thread(New ParameterizedThreadStart(AddressOf compile))
        Me.compileThread.Start(hackFile)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim openFileDialog As System.Windows.Forms.OpenFileDialog = New System.Windows.Forms.OpenFileDialog() With
{
    .Title = "Select compiler exe.",
    .Filter = "C/C++ Compiler | cl.exe"
}
        If (Me.txtCompilerPath.Text <> "") Then
            openFileDialog.InitialDirectory = Me.txtCompilerPath.Text
        End If
        If (openFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
            Me.txtCompilerPath.Text = openFileDialog.FileName
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim builder As New StringBuilder()
        Dim xTc As String()
        xTc = Convert.ToString(Junk.randomString(1).ToCharArray()(0))
        ListBox1.Items.AddRange(xTc)
        ' For Each value As Integer In xh 'Junk.randomString(1).ToCharArray()(0)
        '
        ' builder.Append(value)
        '  Next
        '   Dim result As String = builder.ToString()

        ' Dim finalValue
        ' For i As Integer = 3 To 0 Step -1
        'arrValue(i).appendTo.finalValue()
        ' Next

    End Sub

    Private Sub Text1_TextChanged(sender As Object, e As EventArgs)

    End Sub
End Class