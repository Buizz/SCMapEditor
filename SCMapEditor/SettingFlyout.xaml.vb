Imports MaterialDesignThemes.Wpf

Public Class SettingFlyout

    Private Sub DataLoad(sender As Object, e As RoutedEventArgs)
        PData.LoadDataFromStarCraft()
    End Sub

    Private Sub SetStarDir(sender As Object, e As RoutedEventArgs)
        PData.SetStarPath()
        StarPath.Text = PData.StarDIR
    End Sub

    Private Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)
        StarPath.Text = PData.StarDIR
    End Sub
End Class
