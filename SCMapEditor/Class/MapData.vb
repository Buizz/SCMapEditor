Public Class MapData
    Private WorkSpace As WorkSapce


    Private _mappath As String
    Public Property MapName As String
        Get
            Return _mappath
        End Get
        Set(value As String)
            _mappath = value
        End Set
    End Property

    Public ReadOnly Property SafeMapName As String
        Get
            Return Parser.GetSafeFileName(_mappath)
        End Get
    End Property


    Public Sub New(mappath As String, _WorkSpace As WorkSapce) '맵 파일 읽어오기
        WorkSpace = _WorkSpace
        MapName = mappath
    End Sub
    Public Sub New(_WorkSpace As WorkSapce) '빈 맵 파일 생성
        WorkSpace = _WorkSpace
    End Sub

    Public Function CloseMap() As Boolean
        If (MsgBox("끌꺼냐", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok) Then
            WorkSpace.Close()
            Return True
        Else
            Return False
        End If
    End Function

End Class
