Namespace Tool
    Module Tool
        Public mainWindows As New List(Of MainWindow)


        Public Sub CreateDir(dirname As String)
            If Not My.Computer.FileSystem.DirectoryExists(Dir(dirname)) Then
                My.Computer.FileSystem.CreateDirectory(Dir(dirname))
            End If
        End Sub

        Public Function Dir(name As String) As String
            Return PData.exePos & name
        End Function



        Public Function GetMainWindow() As MainWindow
            Return mainWindows(0)

            'For i = 0 To mainWindows.Count - 1 '모든 윈도우로 부터 전수 조사.
            '    MsgBox(i & " 번째 윈도우 조사 시작")
            '    'StarPage와 WorkSpace로 갈림.
            '    If CheckWindow(mainWindows(i).main.Content, control) Then
            '        Return mainWindows(i)
            '    End If
            'Next

            'Return Nothing
            'Dim obj As Object = control
            'For i = 0 To 10
            '    If obj Is Nothing Then
            '        Return Nothing
            '    End If

            '    Select Case obj.GetType
            '        Case GetType(StarPage)
            '            Dim temp As StarPage = CType(obj, StarPage)

            '            obj = temp.Parent
            '        Case GetType(TabItem)
            '            Dim temp As TabItem = CType(obj, TabItem)

            '            obj = temp.Parent
            '        Case GetType(Dragablz.TabablzControl)
            '            Dim temp As Dragablz.TabablzControl = CType(obj, Dragablz.TabablzControl)

            '            obj = temp.Parent
            '        Case GetType(Dragablz.Dockablz.Layout)
            '            Dim temp As Dragablz.Dockablz.Layout = CType(obj, Dragablz.Dockablz.Layout)

            '            obj = temp.Parent
            '        Case GetType(MainWindow)
            '            Dim temp As MainWindow = CType(obj, MainWindow)

            '            Return temp
            '    End Select
            'Next
        End Function

        'Private Function CheckWindow(Layout As Object, compareobj As Object) As Boolean
        '    If Layout.GetType = GetType(Dragablz.Dockablz.Branch) Then '아랫가지가 있을 경우.
        '        Dim branch As Dragablz.Dockablz.Branch = Layout

        '        Dim returnval1 As Boolean
        '        Dim returnval2 As Boolean
        '        returnval1 = CheckWindow(branch.FirstItem, compareobj)
        '        returnval2 = CheckWindow(branch.SecondItem, compareobj)


        '        Return (returnval1 Or returnval2)
        '    ElseIf Layout.GetType = GetType(Dragablz.TabablzControl) Then
        '        Dim tabs As Dragablz.TabablzControl = Layout
        '        For i = 0 To tabs.Items.Count - 1
        '            Dim obj As Object = CType(tabs.Items(0), TabItem).Content

        '            If obj.GetHashCode = compareobj.GetHashCode Then
        '                MsgBox("찾았다")
        '                Return True
        '            End If
        '        Next
        '        Return False
        '    End If
        '    Return False
        'End Function
    End Module
End Namespace