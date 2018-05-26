Imports System.IO

Namespace StarDat
    Module StarDat
        Private isFrist As Boolean = False
        Structure FrameData
            Public ExtractRect As System.Drawing.Rectangle
            Public offset As Point
        End Structure
        Structure ImageData
            Public Frames As List(Of FrameData)
        End Structure



        Public SDFrameData As New List(Of ImageData)
        Public HDFrameData As New List(Of ImageData)


        Public Sub Init()
            If isFrist = False Then
                isFrist = True

                For i = 0 To 998
                    Dim tempimagedata As New ImageData
                    tempimagedata.Frames = New List(Of FrameData)
                    If My.Computer.FileSystem.FileExists(Tool.Dir("Data\graphics\unit\SD\image" & i & "_frameinfo")) Then
                        Dim fileStream As New FileStream(Tool.Dir("Data\graphics\unit\SD\image" & i & "_frameinfo"), FileMode.Open)
                        Dim br As New BinaryReader(fileStream)

                        Dim framecount As Integer = fileStream.Length \ 16

                        For f = 0 To framecount - 1
                            Dim tFrameData As New FrameData


                            Dim x As Integer = br.ReadUInt16()
                            Dim y As Integer = br.ReadUInt16()

                            Dim xoff As Integer = br.ReadUInt16()
                            Dim yoff As Integer = br.ReadUInt16()


                            Dim width As Integer = br.ReadUInt16()
                            Dim height As Integer = br.ReadUInt16()

                            'unknown
                            br.ReadUInt16()
                            br.ReadUInt16()


                            tFrameData.ExtractRect = New System.Drawing.Rectangle(x, y, width, height)
                            tFrameData.offset = New Point(xoff, yoff)

                            tempimagedata.Frames.Add(tFrameData)
                        Next




                        br.Close()
                        fileStream.Close()
                    End If
                    SDFrameData.Add(tempimagedata)
                Next

                For i = 0 To 998
                    Dim tempimagedata As New ImageData
                    tempimagedata.Frames = New List(Of FrameData)
                    If My.Computer.FileSystem.FileExists(Tool.Dir("Data\graphics\unit\HD\image" & i & "_frameinfo")) Then
                        Dim fileStream As New FileStream(Tool.Dir("Data\graphics\unit\HD\image" & i & "_frameinfo"), FileMode.Open)
                        Dim br As New BinaryReader(fileStream)

                        Dim framecount As Integer = fileStream.Length \ 16

                        For f = 0 To framecount - 1
                            Dim tFrameData As New FrameData


                            Dim x As Integer = br.ReadUInt16()
                            Dim y As Integer = br.ReadUInt16()

                            Dim xoff As Integer = br.ReadUInt16()
                            Dim yoff As Integer = br.ReadUInt16()


                            Dim width As Integer = br.ReadUInt16()
                            Dim height As Integer = br.ReadUInt16()

                            'unknown
                            br.ReadUInt16()
                            br.ReadUInt16()


                            tFrameData.ExtractRect = New System.Drawing.Rectangle(x, y, width, height)
                            tFrameData.offset = New Point(xoff, yoff)

                            tempimagedata.Frames.Add(tFrameData)
                        Next




                        br.Close()
                        fileStream.Close()
                    End If
                    HDFrameData.Add(tempimagedata)
                Next
            End If
        End Sub


    End Module
End Namespace
