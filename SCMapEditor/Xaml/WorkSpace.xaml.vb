Public Class WorkSapce
    Public MapData As MapData


    Private Sub Newfile(sender As Object, e As RoutedEventArgs)
        MsgBox("미친")

        If IsFocused Then
            MsgBox("활성!")
        End If
    End Sub

    Private Sub UserControl_GotFocus(sender As Object, e As RoutedEventArgs)
        MsgBox("포커스")
        'threadLocalStatus = True
    End Sub

    Private Sub UserControl_LostFocus(sender As Object, e As RoutedEventArgs)
        MsgBox("포커스 잃음")
        'threadLocalStatus = False
    End Sub

    Private Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)
        sampleControl.Width = System.Windows.Forms.SystemInformation.VirtualScreen.Width
        sampleControl.Height = System.Windows.Forms.SystemInformation.VirtualScreen.Height
    End Sub

    Private Sub SDHDbtnOn(sender As Object, e As RoutedEventArgs)
        sampleControl.IsHD = True
    End Sub

    Private Sub SDHDbtnOff(sender As Object, e As RoutedEventArgs)
        sampleControl.IsHD = False
    End Sub

    Public Sub Close()


    End Sub
End Class
