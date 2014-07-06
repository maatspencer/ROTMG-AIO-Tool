Public Class batchProcess
    ' Create And Run Batch Files
    Public Shared Sub start(aryText() As String, FileName As String)
        Dim objWriter As New System.IO.StreamWriter(FileName, False)
        For i = 0 To aryText.Length - 1
            objWriter.WriteLine(aryText(i))
        Next
        objWriter.Close()

        Dim Process As New Process
        Dim ps As New ProcessStartInfo(FileName)
        ps.RedirectStandardError = False
        ps.RedirectStandardOutput = True
        ps.CreateNoWindow = True
        ps.WindowStyle = ProcessWindowStyle.Hidden
        ps.UseShellExecute = False
        Process.StartInfo = ps
        Process.Start()
        Process.WaitForExit()
    End Sub
End Class
