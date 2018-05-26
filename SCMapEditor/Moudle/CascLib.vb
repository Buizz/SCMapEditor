Imports System
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text

Public Class CascLib

    <DllImport("CascLib.dll")>
    Public Shared Function CascOpenStorage(szDataPath As String, dwLocaleMask As UInteger, ByRef phStorage As IntPtr) As Boolean
    End Function

    <DllImport("CascLib.dll")>
    Public Shared Function CascOpenFile(hStorage As IntPtr, szFileName As String, dwLocale As UInteger, dwFlags As UInteger, ByRef phFile As IntPtr) As Boolean
    End Function

    <DllImport("CascLib.dll")>
    Public Shared Function CascReadFile(hFile As IntPtr, lpBuffer() As Byte, dwToRead As UInteger, ByRef pdwRead As IntPtr) As Boolean
    End Function

    <DllImport("CascLib.dll")>
    Public Shared Function CascCloseFile(hFile As IntPtr) As Boolean
    End Function

    <DllImport("CascLib.dll")>
    Public Shared Function CascCloseStorage(hStorage As IntPtr) As Boolean
    End Function



    Private StoragePath As String
    Public Property hStorage As IntPtr
    Public Property hfile As IntPtr


    Public Bytes() As Byte

    Public Sub New(_Storages As String)
        StoragePath = _Storages

        If FileHash.Count = 0 Then
            Dim strs() As String = My.Resources.ROOT.Split(vbLf)
            For i = 0 To strs.Count - 1
                FileHash.Add(strs(i).Trim.Split("|")(0), strs(i).Trim.Split("|")(1))
            Next
        End If
    End Sub


    Public Sub OpenStorage()
        CascLib.CascOpenStorage(StoragePath, &H200, hStorage)
    End Sub
    Public Sub CloseStorage()
        CascLib.CascCloseStorage(hStorage)
    End Sub


    Public Sub ReadFile(Hash As String)

        Dim memstream As New MemoryStream()

        Dim bytewriter As New BinaryWriter(memstream)
        Dim bytereader As New BinaryReader(memstream)
        Dim Buffer(1024) As Byte

        CascLib.CascOpenFile(hStorage, Hash, 0, &H1, hfile)

        While (True)
            Dim dwBytesRead As UInteger = 0
            CascLib.CascReadFile(hfile, Buffer, Buffer.Length, dwBytesRead)
            If (dwBytesRead = 0) Then
                Exit While
            End If
            bytewriter.Write(Buffer)

        End While
        memstream.Position = 0
        Bytes = memstream.ToArray

        CascLib.CascCloseFile(hfile)

        bytereader.Close()
        bytewriter.Close()
        memstream.Close()
    End Sub


    Public Shared TileTypes() As String = {"AshWorld", "install", "Jungle", "Twilight", "Ice", "Desert", "platform", "badlands"}




    Private Shared FileHash As New Dictionary(Of String, String)




    'HD2/TileSet/badlands.dds.vr4|143a7501895baf68e482796f0e96ec4d
    'HD2/TileSet/platform.dds.vr4|95dc427211a50017ccfef1513649d24b
    'HD2/TileSet/Twilight.dds.vr4|e99c8194855e44537f8c78be6229cac1
    'HD2/TileSet/Jungle.dds.vr4|ab7b87c792ba9f6286d4f5bae9342256
    'HD2/TileSet/install.dds.vr4|ece175c75a094d84b64440d0f1b3da41
    'HD2/TileSet/AshWorld.dds.vr4|a685d4d25873bab1d10adf53c7c793ae
    'HD2/TileSet/Ice.dds.vr4|b14d53434f744d2fccf4967846b65378
    'HD2/TileSet/Desert.dds.vr4|96625baeea4f69f14a7c573c22a5211f



    'SD/TileSet/Desert.dds.vr4|66a003d1874b2cfa2ad9cd3535526c76
    'SD/TileSet/Jungle_dds_vr4|951429b83a76c180b740b146732e493b
    'SD/TileSet/AshWorld_dds_vr4|739183ceea7f6349efd234b20e27fc79
    'SD/TileSet/platform_dds_vr4|9e8476b57cf151befb8e26c94f0a3834
    'SD/TileSet/Twilight_dds_vr4|9f4dff29a5bfabffeb734bfc43344c78
    'SD/TileSet/badlands_dds_vr4|929fa69a288e78bec6c701e8b240a4bd
    'SD/TileSet/install_dds_vr4|a952d06be47964456bd795b603a81505
    'SD/TileSet/Ice_dds_vr4|5133e675c45ba20568db38cdabb90b9c


    'SD/TileSet/Desert.dds.grp|9aad77d49de9c526989af38551d2589e
    'SD/TileSet/Jungle.dds.grp|fb2371d56bd5aec26c20585f6ece2db9
    'SD/TileSet/AshWorld.dds.grp|6e0ed7b2f603627f32a6236b45febd44
    'SD/TileSet/platform.dds.grp|785312f6acf331ddd0b9236c54519bc1
    'SD/TileSet/Twilight.dds.grp|cd63f53aad48aa57610efe0222136662
    'SD/TileSet/badlands.dds.grp|512c9081113e8bf604f6b9dc38793913
    'SD/TileSet/install.dds.grp|952762f685f2d9eeb5c156e927ccad2e
    'SD/TileSet/Ice.dds.grp|dbe3ed4d4c92725e5e8fb2f471b2a982



    Public Function GetFileHash(name As String) As String
        If FileHash.ContainsKey(name) Then
            Return FileHash(name)
        Else
            Return "NULL"
        End If
    End Function
End Class
