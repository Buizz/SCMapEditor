Imports MahApps.Metro.Controls.Dialogs
Imports MaterialDesignThemes.Wpf

Namespace PData
    Module ProgramData
        Public DatMPQDirec(3) As String
        Public StarDIR As String = "D:\Game\StarCraft\"
        Public euddraftDIR As String = "ㅋ"

        Public exePos As String







        Public Sub Init()
            exePos = System.AppDomain.CurrentDomain.BaseDirectory()
        End Sub

        Public Sub SetStarPath()
            If StarExeOpenDialog.ShowDialog() = Forms.DialogResult.OK Then
                StarDIR = StarExeOpenDialog.FileName.Replace("StarCraft.exe", "")
            End If
        End Sub



        Public Sub LoadDataFromStarCraft()
            For Each windows As MainWindow In Tool.mainWindows
                windows.Hide()
            Next

            Dim doc As DataLoadDialog = New DataLoadDialog()
            doc.ShowDialog()

            For Each windows As MainWindow In Tool.mainWindows
                windows.Show()
            Next
        End Sub

    End Module
End Namespace