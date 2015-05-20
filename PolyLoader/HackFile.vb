Imports ICSharpCode.SharpZipLib.Zip
Imports PolyLoader.Properties
Imports PolyLoader.Randomization
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Resources

Namespace PolyLoader
    Friend Class HackFile
        Private _fileName As String

        Private _zip As ZipFile

        Public Sub New(ByVal fn As String)
            MyBase.New()
            Me._fileName = fn
            Me._zip = Nothing
        End Sub

        Public Function checkFile() As Boolean
            If (Not File.Exists(Me._fileName)) Then
                Return False
            End If
            Me._zip = New ZipFile(File.OpenRead(Me._fileName))
            If (Me._zip.GetEntry("src/") Is Nothing) Then
                Me._zip.IsStreamOwner = True
                Me._zip.Close()
                Return False
            End If
            If (Me._zip.GetEntry("command.txt") Is Nothing) Then
                Me._zip.IsStreamOwner = True
                Me._zip.Close()
                Return False
            End If
            If (Me._zip.GetEntry("inout.txt") Is Nothing) Then
                Me._zip.IsStreamOwner = True
                Me._zip.Close()
                Return False
            End If
            If (Me._zip.GetEntry("msg.txt") Is Nothing) Then
                Me._zip.IsStreamOwner = True
                Me._zip.Close()
                Return False
            End If
            Me._zip.IsStreamOwner = True
            Me._zip.Close()
            Return True
        End Function

        Public Function getFilename() As String
            Return Me._fileName
        End Function

        Public Sub randomizeAllFiles(ByVal dir As String, ByVal outDir As String)
            For Each str As String In Directory.EnumerateFiles(String.Concat(dir, "src/"), "*.h", SearchOption.AllDirectories)
                Dim str1 As String = str.Replace(String.Concat(dir, "src/"), "")
                Directory.CreateDirectory(String.Concat(outDir, Path.GetDirectoryName(str1)))
                Dim headerFile As PolyLoader.Randomization.HeaderFile = New PolyLoader.Randomization.HeaderFile(str)
                headerFile.addJunkcode()
                File.WriteAllText(String.Concat(outDir, str1), headerFile.getData())
            Next
            For Each str2 As String In Directory.EnumerateFiles(String.Concat(dir, "src/"), "*.cpp", SearchOption.AllDirectories)
                Dim str3 As String = str2.Replace(String.Concat(dir, "src/"), "")
                Directory.CreateDirectory(String.Concat(outDir, Path.GetDirectoryName(str3)))
                Dim cppFile As PolyLoader.Randomization.CppFile = New PolyLoader.Randomization.CppFile(str2)
                cppFile.addJunkcode()
                File.WriteAllText(String.Concat(outDir, str3), cppFile.getData())
            Next
        End Sub

        Public Sub unzip(ByVal dir As String, ByVal encryptionKey As Char)
            If (Not Directory.Exists(dir)) Then
                Directory.CreateDirectory(dir)
            End If
            Dim fastzip1 As New FastZip()
            fastzip1.ExtractZip(Me._fileName, dir, Nothing)


            
            File.WriteAllText(String.Concat(dir, "/src/Decrypt.h"), My.Resources.DecryptHeader.Replace("%ENC_KEY%", encryptionKey.ToString()))
            File.WriteAllText(String.Concat(dir, "/src/Decrypt.cpp"), My.Resources.DecryptSource)
        End Sub
    End Class
End Namespace