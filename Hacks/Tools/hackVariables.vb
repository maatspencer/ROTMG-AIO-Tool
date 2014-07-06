Imports System.IO

Namespace hackVariables
    Public Class Vars
        Public Shared Folder As String = Application.StartupPath & "/AddHacks"

        Public Shared myFile As String 'File being read
        Public Shared newFile As String 'Temp file being written

        Public Shared count As Integer 'Counter used in reading process
        Public Shared currentLine As String 'Current line in reader
        Public Shared lineToWrite As String 'Previously read line to write into the temp file

        Public Shared arytext() As String 'array of string used in batch processes
        Public Shared FILE_NAME As String 'file name of batch process
        Public Shared Quote As String = """"

        Public Shared current As String = Date.Today
        Public Shared today As String = current.Replace("/", "-")
        Public Shared version As String

        Public Shared hackFile As StreamWriter
        Public Shared HackTemp As String = Application.StartupPath & "/HackTemp.AIO"
        Public Shared info As StreamWriter
        Public Shared linesAndFiles As String = Application.StartupPath & "\LinesAndFiles_" & today & ".txt"

        Public Shared listBox As ListBox = AddHacks.ListBox1
        Public Shared Hacks As Integer = 25
    End Class
End Namespace
