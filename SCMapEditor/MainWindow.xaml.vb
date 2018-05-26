Class MainWindow
    Public workmanager As WorkManager


    Private Sub OnLoad(ByVal sender As Object, ByVal e As RoutedEventArgs)
        'WorkManager.MainTab = tabs
        'workmanager.Init()
        workmanager = New WorkManager(tabs)
        Tool.mainWindows.Add(Me)
        InitProgram()
        'MsgBox("제발")
        'For i = 0 To tabs.Items.Count - 1
        '    Dim obj As Object = CType(tabs.Items(i), TabItem).Content
        '    MsgBox(tabs.Items(i).ToString)

        '    If obj.GetType = GetType(WorkSapce) Then
        '        Dim workspacetab As WorkSapce = CType(obj, WorkSapce)
        '        workspacetab.mainwindow = Me
        '    Else
        '        Dim StartPagetab As StarPage = CType(obj, StarPage)

        '        StartPagetab.mainwindow = Me
        '    End If
        'Next
    End Sub

    Private Sub OpenStartPage(sender As Object, e As RoutedEventArgs)
        Dim b1 As TabItem = New TabItem()
        b1.Header = "시작 페이지"

        Dim page As StarPage = New StarPage()
        page.mainwindow = Me

        page.Tag = tabs.Items.Count
        b1.Content = page
        tabs.Items.Add(b1)

        tabs.SelectedIndex = tabs.Items.Count - 1
    End Sub

    Private Sub ShowSetting(sender As Object, e As RoutedEventArgs)
        SettingFlyout.IsOpen = Not SettingFlyout.IsOpen
    End Sub

    Private Sub _Closing(sender As Object, e As ComponentModel.CancelEventArgs)
        e.Cancel = CloseTabs(main.Content)
        If e.Cancel = False Then
            Tool.mainWindows.Remove(Me)
        End If

        'If main.Content.GetType = GetType(Dragablz.Dockablz.Branch) Then '아랫가지가 있을 경우.
        '    Dim branch As Dragablz.Dockablz.Branch = main.Content



        '    MsgBox(branch.FirstItem.ToString())
        'End If







        'For i = 0 To tabs.Items.Count - 1
        '    Dim obj As Object = CType(tabs.Items(0), TabItem).Content

        '    If obj.GetType = GetType(WorkSapce) Then
        '        tabs.SelectedIndex = 0

        '        Dim workspacetab As WorkSapce = CType(obj, WorkSapce)

        '        If workspacetab.MapData.CloseMap() Then
        '            workspacetab.threadStatus = False
        '            tabs.Items.RemoveAt(0)
        '        Else
        '            e.Cancel = True
        '            Exit Sub
        '        End If
        '    Else
        '        tabs.Items.RemoveAt(0)
        '    End If
        'Next
    End Sub

    Private Function CloseTabs(Layout As Object) As Boolean
        If Layout.GetType = GetType(Dragablz.Dockablz.Branch) Then '아랫가지가 있을 경우.
            Dim branch As Dragablz.Dockablz.Branch = Layout

            Dim returnval1 As Boolean
            Dim returnval2 As Boolean
            returnval1 = CloseTabs(branch.FirstItem)
            returnval2 = CloseTabs(branch.SecondItem)


            Return (returnval1 Or returnval2)
        ElseIf Layout.GetType = GetType(Dragablz.TabablzControl) Then
            Dim tabs As Dragablz.TabablzControl = Layout

            For i = 0 To tabs.Items.Count - 1
                Dim obj As Object = CType(tabs.Items(0), TabItem).Content

                If obj.GetType = GetType(WorkSapce) Then
                    tabs.SelectedIndex = 0

                    Dim workspacetab As WorkSapce = CType(obj, WorkSapce)

                    If workspacetab.MapData.CloseMap() Then
                        'workspacetab.threadStatus = 
                        tabs.Items.RemoveAt(0)
                    Else
                        'e.Cancel = True
                        Return True
                    End If
                Else
                    tabs.Items.RemoveAt(0)
                End If
            Next
            Return False
        End If
        Return False
    End Function

    Private Sub temp(sender As Object, e As RoutedEventArgs)
        For Each win As MainWindow In Tool.mainWindows
            MsgBox(win.ToString)
        Next
    End Sub
End Class
