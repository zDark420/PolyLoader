Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic


' Decompiled with JetBrains decompiler
' Type: PolyLoader.JunkCode.RandomStringArrayTool
' Assembly: PolyLoader Merged, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
' MVID: 93164D1E-63CA-4683-BE08-3E78E70FAFAD
' Assembly location: C:\Users\Admin\Desktop\PL\PolyLoader.exe

'Imports System.Collections.Generic
'Imports System.Linq

Namespace PolyLoader.JunkCode
    Friend NotInheritable Class RandomStringArrayTool
        Private Sub New()
        End Sub
        Public Shared Function RandomizeStrings(arr As String()) As String()
            Dim list As New List(Of KeyValuePair(Of Integer, String))()
            For Each str As String In arr
                list.Add(New KeyValuePair(Of Integer, String)(Junk.rnd.[Next](), str))
            Next
            Dim orderedEnumerable As IOrderedEnumerable(Of KeyValuePair(Of Integer, String)) = Enumerable.OrderBy(Of KeyValuePair(Of Integer, String), Integer)(DirectCast(list, IEnumerable(Of KeyValuePair(Of Integer, String))), DirectCast(Function(item) item.Key, Func(Of KeyValuePair(Of Integer, String), Integer)))
            Dim strArray As String() = New String(arr.Length - 1) {}
            Dim index As Integer = 0
            For Each keyValuePair As KeyValuePair(Of Integer, String) In DirectCast(orderedEnumerable, IEnumerable(Of KeyValuePair(Of Integer, String)))
                strArray(index) = keyValuePair.Value
                index += 1
            Next
            Return strArray
        End Function
    End Class
End Namespace

'=======================================================
'Service provided by Telerik (www.telerik.com)
'Conversion powered by NRefactory.
'Twitter: @telerik
'Facebook: facebook.com/telerik
'=======================================================
