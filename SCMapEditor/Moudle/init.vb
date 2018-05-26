Imports System.Windows.Forms
Imports MaterialDesignThemes.Wpf
Module init
    Private isFrist As Boolean = False

    Private PaletteHelper As New PaletteHelper
    Public MapOpenDialog As New OpenFileDialog
    Public StarExeOpenDialog As New OpenFileDialog


    Public Sub InitProgram()
        If isFrist = False Then
            isFrist = True
            MapOpenDialog.Filter = "SCX 지도 파일|*.scx|SCM 지도 파일|*.scm"
            StarExeOpenDialog.Filter = "StarCraft.exe|StarCraft.exe"

            PData.Init()
            'StarDIR = "머야"


            'PaletteHelper.SetLightDark(True)
            PaletteHelper.ReplacePrimaryColor("Indigo")
        End If
    End Sub
End Module
