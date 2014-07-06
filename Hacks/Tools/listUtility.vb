Public Class listUtility
    Public Shared Function isSelected(name As String) As Boolean
        Dim bool As Boolean = False
        For i = 0 To AddHacks.ListBox1.Items.Count - 1
            If AddHacks.ListBox1.Items(i).ToString.Contains(name) Then
                If AddHacks.ListBox1.GetSelected(i) = True Then
                    bool = True
                End If
            End If
        Next
        Return bool
    End Function
End Class
