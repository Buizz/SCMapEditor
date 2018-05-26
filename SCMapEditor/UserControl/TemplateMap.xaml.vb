Public Class TemplateMap

    Private Sub OpenMap(sender As Object, e As RoutedEventArgs)
        '    Dim _grid As Grid = CType(CType(CType(Me.Parent, WrapPanel).Parent, ScrollViewer).Parent, Grid).Parent
        '    Dim StarPage As StarPage = _grid.Parent

        Dim temp As MainWindow = Tool.GetMainWindow()


        temp.workmanager.NewMap()
    End Sub
End Class
