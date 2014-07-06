Imports System.IO

Public Class findStr
    Public Shared Function start(fileName As String, aryText() As String) As String
        Dim Folder As String = Application.StartupPath & "\AddHacks"
        Dim Quote = """"
        Dim stringArray() As String

        Array.Resize(stringArray, aryText.Length + 1)
        stringArray(0) = aryText(0) = "cd " & Folder
        For i = 1 To stringArray.Length - 1
            Select Case i
                Case 1
                    stringArray(1) = "findstr /s /m " & Quote & aryText(0) & Quote & " *class.asasm >results1.txt"
                Case Else
                    stringArray(i) = "findstr /m /f:results" & i - 1 & ".txt " & Quote & aryText(i - 1) & Quote & " *class.asasm >results" & i & ".txt"
            End Select
        Next

        ' Run Batch Process
        batchProcess.start(stringArray, fileName)

        ' Read result
        Dim Result As String = File.ReadAllText(Folder & "\results3.txt")
        Result = Result.Replace(Environment.NewLine, "")

        Return Result
    End Function
End Class
