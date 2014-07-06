Public Class recompile
    ' Recompile
    Public Shared Sub start(startLocation As String)

        Dim FILE_NAME As String = startLocation & "\recompile.bat"
        Dim aryText(3) As String
        aryText(0) = "cd " & startLocation
        aryText(1) = "rabcasm client-1\client-1.main.asasm"
        aryText(2) = "abcreplace client.swf 1 client-1\client-1.main.abc"
        batchProcess.start(aryText, FILE_NAME)
    End Sub
End Class
