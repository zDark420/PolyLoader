Imports Microsoft.CSharp
Imports PolyLoader
Imports PolyLoader.Encryption
Imports PolyLoader.JunkCode
Imports System.CodeDom.Compiler
Imports System.Collections.Generic
Imports System.Text.RegularExpressions '//regex
Imports System.Windows.Forms
Imports System
Imports System.String
Imports Microsoft.VisualBasic.Strings

Namespace PolyLoader.Randomization
    Friend MustInherit Class BaseFile
        Protected _pJunkMinLength As Integer = 5
        Protected _pJunkMaxLength As Integer = 10
        Protected _parseStack As New Stack(Of BaseFile.ParseItem)()
        Protected blocks As List(Of String)() = New List(Of String)(99) {}
        Protected _curBlockLayer As Integer = -1
        Protected _pJunkEnabled As Boolean
        Protected _pJunkDeclaresOnly As Boolean
        Protected _pEscapeString As Boolean
        Protected _fileName As String
        Protected _fileData As String
        Protected _cursor As Integer
        Protected Function match(str As String) As Boolean
            For index As Integer = 0 To str.Length - 1
                If Convert.ToInt32(Me._fileData(Me._cursor + index)) <> Convert.ToInt32(str(index)) Then
                    Return False
                End If
            Next
            Return True
        End Function

        Protected Function matchWildcard(str As String) As Boolean
            Dim num As Integer = Me._fileData.IndexOf(Convert.ToString(num), Me._cursor)
            If num = -1 Then
                Return False
            End If
            Dim input As String = Me._fileData.Substring(Me._cursor, num - Me._cursor)
            Return New Regex("^" + Regex.Escape(str).Replace("\*", ".*").Replace("\?", ".") + "$").IsMatch(input)
        End Function

        Protected Function ToLiteral(input As String) As String
            Return TryCast(New CSharpCodeProvider().CompileAssemblyFromSource(New CompilerParameters() With { _
                .GenerateExecutable = False, _
                .GenerateInMemory = True _
            }, (Convert.ToString(Environment.NewLine & Environment.NewLine & "            namespace tmp" & Environment.NewLine & Environment.NewLine & "            {" & Environment.NewLine & Environment.NewLine & "                public class tmpClass" & Environment.NewLine & Environment.NewLine & "                {" & Environment.NewLine & Environment.NewLine & "                    public static string GetValue()" & Environment.NewLine & Environment.NewLine & "                    {" & Environment.NewLine & Environment.NewLine & "                            return """) & input) + """;" & Environment.NewLine & Environment.NewLine & "                    }" & Environment.NewLine & Environment.NewLine & "                }" & Environment.NewLine & Environment.NewLine & "            }").CompiledAssembly.[GetType]("tmp.tmpClass").GetMethod("GetValue").Invoke(DirectCast(Nothing, Object), DirectCast(Nothing, Object())), String)
        End Function

        Protected Sub safeRemove(start As Integer, length As Integer)
            Me._fileData = Me._fileData.Remove(start, length)
        End Sub

        Protected Sub safeInsert(start As Integer, toInsert As String)
            Me._fileData = Me._fileData.Insert(start, toInsert)
            Me._cursor += toInsert.Length
        End Sub

        Protected Function getFromStack(shouldBe As String) As BaseFile.ParseItem
            On Error Resume Next
            Dim parseItem As BaseFile.ParseItem = Me._parseStack.Pop()
            If parseItem.tag.ToLower() <> shouldBe Then
                Clipboard.SetText(Me._fileData)
                MessageBox.Show("Starting tag " & parseItem.tag + " does not match " & shouldBe + " at " + Me._cursor.ToString() + Environment.NewLine + "File data has been copied to the clipboard!", "Parse error!")
                Application.[Exit]()
            End If
            Return parseItem
        End Function

        Protected Function isTag(s As String) As Boolean
            Select Case s
                Case "junk_enable", "add_junk", "junk_disable", "junk_enable_declares]", "junk_function", "enc_string_enable", _
                    "enc_string_disable", "swap_lines", "/swap_lines", "swap_blocks", "/swap_blocks", "block", _
                    "/block"
                    Return True
                Case Else
                    Return False
            End Select
        End Function

        Protected Sub checkAll()
            If Me.match("""") Then
                Dim num As Integer = Me._cursor
                Dim flag As Boolean
                flag = False
                While Not flag
                    num = Me._fileData.IndexOf("""", num + 1)
                    If num = -1 Then
                        Exit While
                    End If

                    flag = Convert.ToInt32(Me._fileData(num - 1)) <> 92
                End While
                If flag Then
                    Dim txt As String = Me.ToLiteral(Me._fileData.Substring(Me._cursor + 1, num - Me._cursor - 1))
                    If Me._pEscapeString Then
                        Dim str As String = "Decrypt(" + txt.Length.ToString()
                        Dim numArray As Integer() = StringEncryption.encrypt(txt)
                        For index As Integer = 0 To txt.Length - 1 'As integer = 0 To txt.Length - 1
                            str = str & ", " & Convert.ToString(numArray(index).ToString) '.ToString
                        Next
                        Dim toInsert As String = str & ")"
                        Me.safeRemove(Me._cursor, num - Me._cursor + 1)
                        Me.safeInsert(Me._cursor, toInsert)
                    Else
                        Me._cursor = num + 1
                    End If
                End If
            End If
            If Me.match("[") Then
                '/=0/=1[=2 our tag is at 3 
                Dim num As Integer = Me._fileData.IndexOf("]", Me._cursor + 1)
                Dim str1 As String = Me._fileData.Substring(Me._cursor + 1, num - Me._cursor - 1)
                Dim strArray1 As String() = str1.Split(" "c)
                Dim str2 As String = strArray1(0)

                '  Dim num As Integer = Me._fileData.IndexOf("]", Me._cursor + 1)
                '  Dim str1 As String = Me._fileData.Substring(Me._cursor + 1, num - Me._cursor - 1)
                '  Dim strArray1 As String() = str1.Split(" "c)
                '  Dim str2 As String = strArray1(0)
                If Me.isTag(str2) Then
                    Me.safeRemove(Me._cursor, num - Me._cursor + 1)
                    Dim flag As Boolean = Not str1.EndsWith("/") AndAlso Not str1.StartsWith("[/")
                    Select Case str2
                        Case "junk_enable"
                            Me._pJunkEnabled = True
                            Me._pJunkDeclaresOnly = False
                            If strArray1.Length = 3 Then
                                Me._pJunkMinLength = Integer.Parse(strArray1(1))
                                Me._pJunkMaxLength = Me._pJunkMinLength
                                Exit Select
                            End If
                            If strArray1.Length = 4 Then
                                Me._pJunkMinLength = Integer.Parse(strArray1(1))
                                Me._pJunkMaxLength = Integer.Parse(strArray1(2))
                                Exit Select
                            End If
                            Exit Select
                        Case "add_junk"
                            Dim min1 As Integer = 5
                            Dim max1 As Integer = 10
                            If strArray1.Length = 3 Then
                                min1 = Integer.Parse(strArray1(1))
                                max1 = min1
                            ElseIf strArray1.Length = 4 Then
                                min1 = Integer.Parse(strArray1(1))
                                max1 = Integer.Parse(strArray1(2))
                            End If
                            Me.safeInsert(Me._cursor, Junk.generateCode(min1, max1))
                            Exit Select
                        Case "junk_disable"
                            Me._pJunkEnabled = False
                            Exit Select
                        Case "junk_enable_declares"
                            Me._pJunkDeclaresOnly = True
                            Exit Select
                        Case "junk_function"
                            Dim min2 As Integer = 2
                            Dim max2 As Integer = 5
                            If strArray1.Length = 3 Then
                                min2 = Integer.Parse(strArray1(1))
                                max2 = min2
                            ElseIf strArray1.Length = 4 Then
                                min2 = Integer.Parse(strArray1(1))
                                max2 = Integer.Parse(strArray1(2))
                            End If
                            Me.safeInsert(Me._cursor, Junk.generateFunctions(min2, max2))
                            Exit Select
                        Case "enc_string_enable"
                            Me._pEscapeString = True
                            Exit Select
                        Case "enc_string_disable"
                            Me._pEscapeString = False
                            Exit Select
                        Case "/swap_lines"
                            Dim fromStack1 As BaseFile.ParseItem = Me.getFromStack("swap_lines")
                            Dim strArray2 As String() = Me._fileData.Substring(fromStack1.pos, Me._cursor - fromStack1.pos).Split(New String(0) {Environment.NewLine}, StringSplitOptions.None)
                            Dim arr1 As String() = New String(strArray2.Length - 3) {}
                            For index As Integer = 1 To strArray2.Length - 2
                                arr1(index - 1) = strArray2(index)
                            Next
                            Dim toInsert1 As String = Environment.NewLine + String.Join(Environment.NewLine, RandomStringArrayTool.RandomizeStrings(arr1)) + Environment.NewLine
                            Me.safeRemove(fromStack1.pos, Me._fileData.Substring(fromStack1.pos, Me._cursor - fromStack1.pos).Length)
                            Me._cursor = fromStack1.pos
                            Me.safeInsert(fromStack1.pos, toInsert1)
                            Exit Select
                        Case "swap_blocks"
                            Me._curBlockLayer += 1
                            Exit Select
                        Case "/block"
                            Dim fromStack2 As BaseFile.ParseItem = Me.getFromStack("block")
                            Me.blocks(Me._curBlockLayer).Add(Me._fileData.Substring(fromStack2.pos, Me._cursor - fromStack2.pos))
                            Exit Select
                        Case "/swap_blocks"
                            Dim fromStack3 As BaseFile.ParseItem = Me.getFromStack("swap_blocks")
                            Dim arr2 As String() = Me.blocks(Me._curBlockLayer).ToArray()
                            Me.blocks(Me._curBlockLayer).Clear()
                            Me._curBlockLayer -= 1
                            Dim toInsert2 As String = String.Join(Environment.NewLine, RandomStringArrayTool.RandomizeStrings(arr2))
                            Me.safeRemove(fromStack3.pos, Me._cursor - fromStack3.pos)
                            Me.safeInsert(fromStack3.pos, toInsert2)
                            Exit Select
                    End Select
                    If Not flag Then
                        Return
                    End If
                    Me._parseStack.Push(New BaseFile.ParseItem(str2, Me._cursor))
                    Return
                End If
            End If
            If Not Me.match(";") Then
                Return
            End If
            Dim num1 As Integer = Me._fileData.IndexOf(Environment.NewLine, Me._cursor + 1)
            If Not Me._pJunkEnabled Then
                Return
            End If
            Me._cursor = num1 + 1
            Me.safeInsert(Me._cursor, Me.addCode())
        End Sub

        Protected Sub nextChar()
            Me.checkAll()
            Me._cursor += 1
        End Sub

        Public Sub addJunkcode()
            While Me._cursor < Me._fileData.Length
                Me.nextChar()
            End While
        End Sub

        Protected MustOverride Function addCode() As String

        Public Function getData() As String
            Return Me._fileData
        End Function

        Public Structure ParseItem
            Public tag As String
            Public pos As Integer

            Public Sub New(t As String, p As Integer)
                Me.tag = t
                Me.pos = p
            End Sub
        End Structure
    End Class
End Namespace
