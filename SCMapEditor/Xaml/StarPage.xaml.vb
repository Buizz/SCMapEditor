Class StarPage
    Public mainwindow As MainWindow


    Private Sub OpenFile(sender As Object, e As RoutedEventArgs)
        Dim temp As MainWindow = Tool.GetMainWindow()
        If temp IsNot Nothing Then
            mainwindow = temp
        End If


        mainwindow.workmanager.OpenMapStart()



        'Dim _next As Control = Me.Parent
        'Dim workmanager As WorkManager = Nothing


        'While (True)
        '    MsgBox(_next.ToString)
        '    _next = _next.Parent
        '    If _next.GetType = GetType(MainWindow) Then
        '        workmanager = CType(_next, MainWindow).workmanager
        '        Exit While
        '    End If
        'End While

        'workmanager.OpenMapStart()
    End Sub


    Private Sub CloseStartPage()
        Dim temp As MainWindow = Tool.GetMainWindow()
        If temp IsNot Nothing Then
            MainWindow = temp
        End If

        mainwindow.tabs.Items.Remove(Me.Parent)
    End Sub

    Private Sub OnLoad(sender As Object, e As RoutedEventArgs)
        TamplatePanel.Children.Add(New TemplateMap)
    End Sub
End Class
