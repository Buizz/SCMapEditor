Public Class WorkManager
    Public MainTab As Dragablz.TabablzControl

    Public Sub New(tab As Dragablz.TabablzControl)
        MainTab = tab
    End Sub



    Public Sub OpenMapStart()
        If MapOpenDialog.ShowDialog() = Forms.DialogResult.OK Then
            OpenMapDialog(MapOpenDialog.FileName)
        End If
    End Sub


    Public Sub OpenMapDialog(mappath As String)
        Dim page As WorkSapce = New WorkSapce()
        Dim map As New MapData(mappath, Page)

        Dim b1 As TabItem = New TabItem()
        b1.Header = map.SafeMapName



        Page.Tag = MainTab.Items.Count
        Page.MapData = map


        b1.Content = Page
        MainTab.Items.Add(b1)

        MainTab.SelectedIndex = MainTab.Items.Count - 1
    End Sub


    Public Sub NewMap()
        Dim page As WorkSapce = New WorkSapce()
        Dim map As New MapData(page)

        Dim b1 As TabItem = New TabItem()
        b1.Header = "제목 없음"


        Page.Tag = MainTab.Items.Count
        page.MapData = map


        b1.Content = page
        MainTab.Items.Add(b1)

        MainTab.SelectedIndex = MainTab.Items.Count - 1
    End Sub
End Class