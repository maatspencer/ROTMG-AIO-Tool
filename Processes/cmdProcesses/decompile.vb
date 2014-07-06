Public Class decompile
    ' Decompile
    Public Shared Sub start(startLocation As String)

        Dim FILE_NAME As String = startLocation & "\decompile.bat"
        Dim aryText(4) As String
        aryText(0) = "cd " & startLocation
        aryText(1) = "swfdecompress client.swf"
        aryText(2) = "abcexport client.swf"
        aryText(3) = "rabcdasm client-1.abc"
        batchProcess.start(aryText, FILE_NAME)
    End Sub
End Class
