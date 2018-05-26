Namespace Parser
    Module Parser
        Public Function GetSafeFileName(value As String) As String
            Return value.Split("\").Last
        End Function
    End Module
End Namespace
