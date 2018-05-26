Imports System.Drawing
Imports System.IO
Imports System.Threading
Imports System.Windows.Threading

Public Class DataLoadDialog
    Private forceCloas As Boolean = False
    Private DataThread As Thread


    Private Sub HDExtractPngToVr4(TilesetNum As Integer, Casc As CascLib)
        Dim resultfilePath As String = "Data\graphics\tileset\HD\" & CascLib.TileTypes(TilesetNum)
        Dim hashcode As String = Casc.GetFileHash("HD2/TileSet/" & CascLib.TileTypes(TilesetNum) & ".dds.vr4")

        If Not My.Computer.FileSystem.FileExists(Tool.Dir(resultfilePath & "\Tile.png")) Then
            Dim framecount As UInt16

            Tool.CreateDir(resultfilePath)
            Casc.ReadFile(hashcode)
            Dim memory As New MemoryStream(Casc.Bytes)
            Dim binaryReader As New BinaryReader(memory)

            binaryReader.ReadUInt32()
            framecount = binaryReader.ReadUInt16
            binaryReader.ReadUInt16()



            binaryReader.ReadUInt32()

            Dim ddsSize As New Size(binaryReader.ReadUInt16(), binaryReader.ReadUInt16())
            Dim maxsize As Integer = Math.Max(ddsSize.Width, ddsSize.Height)

            memory.Position -= 8

            '=======================비트맵 초기화=============================

            'Dim width As Integer = img1.Width + img2.Width
            'Dim height As Integer = Math.Max(img1.Height, img2.Height)

            Dim width As Integer = maxsize * 16
            Dim height As Integer = (framecount \ 16 + 1) * maxsize

            Dim img As Bitmap = New Bitmap(32 * 16, (framecount \ 16 + 1) * 32)
            Dim g As Graphics = Graphics.FromImage(img)

            g.Clear(Color.Black)
            '=======================비트맵 초기화=============================




            Dim lastValue As Integer
            For i = 0 To framecount - 1
                binaryReader.ReadUInt32()
                binaryReader.ReadUInt16()
                binaryReader.ReadUInt16()

                Dim size As UInt32 = binaryReader.ReadUInt32()

                Dim filestream As New FileStream(Tool.Dir("Data\temp\tempdds"), FileMode.Create)

                filestream.Write(binaryReader.ReadBytes(size), 0, size)
                filestream.Close()

                Dim tbmp As System.Drawing.Bitmap = DevIL.DevIL.LoadBitmap(Tool.Dir("Data\temp\tempdds"))


                g.DrawImage(tbmp, New Rectangle((i Mod 16) * 32, (i \ 16) * 32, 32, 32)) ' New Point((i Mod 16) * maxsize, (i \ 16) * maxsize))

                'tbmp.Save(Tool.Dir(CascLib.GetFileName(hash) & "\Tile" & i & ".png"), System.Drawing.Imaging.ImageFormat.Png)




                If lastValue < (i / framecount) * 100 Then
                    ChangeMiniProgress((i / framecount) * 100)
                    lastValue = (i / framecount) * 100
                End If
                'File.Delete(Tool.Dir("Data\temp\tempdds"))
            Next


            g.Dispose()

            img.Save(Tool.Dir(resultfilePath & "\Tile.png"), System.Drawing.Imaging.ImageFormat.Png)
            img.Dispose()

            binaryReader.Close()
            memory.Close()
        End If
    End Sub
    Private Sub SDExtractPngToVr4(TilesetNum As Integer, Casc As CascLib)
        Dim resultfilePath As String = "Data\graphics\tileset\SD\" & CascLib.TileTypes(TilesetNum)
        Dim hashcode As String = Casc.GetFileHash("SD/TileSet/" & CascLib.TileTypes(TilesetNum) & ".dds.vr4")

        If Not My.Computer.FileSystem.FileExists(Tool.Dir(resultfilePath & "\Tile.png")) Then
            Dim framecount As UInt16

            Tool.CreateDir(resultfilePath)
            Casc.ReadFile(hashcode)
            Dim memory As New MemoryStream(Casc.Bytes)
            Dim binaryReader As New BinaryReader(memory)

            binaryReader.ReadUInt32()
            framecount = binaryReader.ReadUInt16
            binaryReader.ReadUInt16()

            'Size X, Y
            binaryReader.ReadUInt16()
            binaryReader.ReadUInt16()


            Dim pallet(1023) As Byte



            For i = 0 To 255
                For j = 0 To 2
                    pallet(i * 4 + 2 - j) = binaryReader.ReadByte
                Next
                binaryReader.ReadByte()
            Next



            'Dim pallet() As Byte = binaryReader.ReadBytes(1024)




            '=======================비트맵 초기화=============================

            'Dim width As Integer = img1.Width + img2.Width
            'Dim height As Integer = Math.Max(img1.Height, img2.Height)

            Dim width As Integer = 32 * 16
            Dim height As Integer = (framecount \ 16 + 1) * 32

            Dim img As Bitmap = New Bitmap(width, height)
            Dim g As Graphics = Graphics.FromImage(img)



            g.Clear(Color.Black)
            '=======================비트맵 초기화=============================




            Dim lastValue As Integer
            For i = 0 To framecount - 1
                Dim memstream As New MemoryStream()
                Dim tbinaryWriter As New BinaryWriter(memstream)


                Dim bmpformating() As Byte = {&H42, &H4D, &H38, &H8, &H0, &H0, &H0, &H0, &H0, &H0, &H36, &H4, &H0, &H0, &H28, &H0, &H0, &H0, &H20, &H0, &H0, &H0, &H20, &H0, &H0, &H0, &H1, &H0, &H8, &H0, &H0, &H0, &H0, &H0, &H2, &H4, &H0, &H0, &H12, &HB, &H0, &H0, &H12, &HB, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}
                '42 4D 38 08 00 00 00 00 00 00 36 04 00 00 28 00 00 00 20 00 00 00 20 00 00 00 01 00 08 00 00 00 00 00 02 04 00 00 12 0B 00 00 12 0B 00 00 00 00 00 00 00 00 00 00

                tbinaryWriter.Write(bmpformating)
                tbinaryWriter.Write(pallet)

                Dim tempbytes() As Byte = binaryReader.ReadBytes(1024)

                For yp = 0 To 31
                    For xp = 0 To 31
                        tbinaryWriter.Write(tempbytes(xp + (31 - yp) * 32))
                    Next
                Next
                'tbinaryWriter.Write(binaryReader.ReadBytes(1024))


                tbinaryWriter.Write(CByte(0))
                tbinaryWriter.Write(CByte(0))


                'Dim tbmp As New Bitmap(memstream)



                g.DrawImage(Image.FromStream(memstream), New Rectangle((i Mod 16) * 32, (i \ 16) * 32, 32, 32))


                tbinaryWriter.Close()
                memstream.Close()


                If lastValue < (i / framecount) * 100 Then
                    ChangeMiniProgress((i / framecount) * 100)
                    lastValue = (i / framecount) * 100
                End If
                'File.Delete(Tool.Dir("Data\temp\tempdds"))
            Next

            g.Dispose()

            img.Save(Tool.Dir(resultfilePath & "\Tile.png"), System.Drawing.Imaging.ImageFormat.Png)
            img.Dispose()

            binaryReader.Close()
            memory.Close()
        End If
    End Sub

    Private Sub HDExtractImage(imagecode As Integer, Casc As CascLib)
        Dim hashcode As String = Casc.GetFileHash("anim/main_" & String.Format("{0:D3}", imagecode) & ".anim")

        If hashcode = "NULL" Or My.Computer.FileSystem.FileExists(Tool.Dir("Data\graphics\unit\HD\image" & imagecode & "_diffuse.png")) Then
            Return
        End If
        Casc.ReadFile(hashcode)
        Dim memory As New MemoryStream(Casc.Bytes)
        Dim binaryReader As New BinaryReader(memory)


        binaryReader.ReadUInt32() 'unsigned Int magic; // "ANIM"
        binaryReader.ReadUInt16() 'unsigned Short version; // Version? 0x0101, 0x0202, 0x0204
        binaryReader.ReadUInt16() 'unsigned Short unk2; // 0 -- more bytes for version?
        Dim layercount As UInt16 = binaryReader.ReadUInt16() 'unsigned Short layers; 레이어의 갯수
        binaryReader.ReadUInt16() 'unsigned Short entries;


        Dim layerstrs As New List(Of String)
        For i = 0 To 9
            Dim chars As New List(Of Char)
            chars.AddRange(binaryReader.ReadChars(32))

            Dim tstr As String = ""
            For j = 0 To 31
                If Asc(chars(j)) <> 0 Then
                    tstr = tstr & chars(j)
                End If

            Next
            layerstrs.Add(tstr)
        Next

        Dim framescount As UInt16 = binaryReader.ReadUInt16()
        binaryReader.ReadUInt16()
        binaryReader.ReadUInt32()

        Dim frameinfoptr As UInteger = binaryReader.ReadUInt32()





        Dim ddsptr As New List(Of UInteger)
        Dim ddsfilesize As New List(Of UInteger)
        For i = 0 To layercount - 1
            ddsptr.Add(binaryReader.ReadUInt32())
            ddsfilesize.Add(binaryReader.ReadUInt32())

            binaryReader.ReadUInt16()
            binaryReader.ReadUInt16()
        Next


        memory.Position = frameinfoptr

        Dim filestreama As New FileStream(Tool.Dir("Data\graphics\unit\HD\image" & imagecode & "_frameinfo"), FileMode.Create)
        Dim tbinarywriter As New BinaryWriter(filestreama)
        tbinarywriter.Write(binaryReader.ReadBytes(framescount * 16))
        tbinarywriter.Close()
        filestreama.Close()

        For i = 0 To layercount - 1
            If layerstrs(i).Trim = "diffuse" Or layerstrs(i).Trim = "teamcolor" Then
                If ddsptr(i) <> 0 Then
                    memory.Position = ddsptr(i)
                    Dim tfilestreama As New FileStream(Tool.Dir("Data\temp\tempdds"), FileMode.Create)
                    Dim ttbinarywriter As New BinaryWriter(tfilestreama)
                    ttbinarywriter.Write(binaryReader.ReadBytes(ddsfilesize(i)))
                    ttbinarywriter.Close()
                    tfilestreama.Close()

                    Dim tbmp As System.Drawing.Bitmap = DevIL.DevIL.LoadBitmap(Tool.Dir("Data\temp\tempdds"))

                    tbmp.Save(Tool.Dir("Data\graphics\unit\HD\image" & imagecode & "_" & layerstrs(i).Trim & ".png"), Imaging.ImageFormat.Png)
                End If
            End If
        Next






        binaryReader.Close()
        memory.Close()
    End Sub
    Private Sub SDExtractImage(Casc As CascLib)
        Dim hashcode As String = Casc.GetFileHash("SD/mainSD.anim")

        'If hashcode = "NULL" Or My.Computer.FileSystem.FileExists(Tool.Dir("Data\graphics\unit\SD\image_diffuse.png")) Then
        '    Return
        'End If
        Casc.ReadFile(hashcode)
        Dim memory As New MemoryStream(Casc.Bytes)
        Dim binaryReader As New BinaryReader(memory)


        binaryReader.ReadUInt32() 'unsigned Int magic; // "ANIM"
        binaryReader.ReadUInt16() 'unsigned Short version; // Version? 0x0101, 0x0202, 0x0204
        binaryReader.ReadUInt16() 'unsigned Short unk2; // 0 -- more bytes for version?
        Dim layercount As UInt16 = binaryReader.ReadUInt16() 'unsigned Short layers; 레이어의 갯수
        Dim entry As UInt16 = binaryReader.ReadUInt16() 'unsigned Short entries; 일반적으로 999개


        Dim layerstrs As New List(Of String)
        For i = 0 To 9
            Dim chars As New List(Of Char)
            chars.AddRange(binaryReader.ReadChars(32))

            Dim tstr As String = ""
            For j = 0 To 31
                If Asc(chars(j)) <> 0 Then
                    tstr = tstr & chars(j)
                End If

            Next
            layerstrs.Add(tstr)
        Next


        Dim entrypointer As New List(Of UInteger)
        For i = 0 To entry - 1
            entrypointer.Add(binaryReader.ReadUInt32())
        Next





        Dim lastValue As Integer = 0
        For entrynum = 0 To entry - 1
            ChangeText("SD 이미지 " & entrynum & " 추출 중...")
            If lastValue < (entrynum \ 998) * 100 Then
                ChangeMiniProgress((entrynum \ 998) * 100)
                lastValue = (entrynum \ 998) * 100
            End If
            If Not My.Computer.FileSystem.FileExists(Tool.Dir("Data\graphics\unit\SD\image" & entrynum & "_frameinfo")) Then
                memory.Position = entrypointer(entrynum)

                Dim framescount As UInt16 = binaryReader.ReadUInt16()
                If framescount <> 0 Then
                    binaryReader.ReadUInt16()
                    binaryReader.ReadUInt32()

                    Dim frameinfoptr As UInteger = binaryReader.ReadUInt32()





                    Dim ddsptr As New List(Of UInteger)
                    Dim ddsfilesize As New List(Of UInteger)
                    For i = 0 To layercount - 1
                        ddsptr.Add(binaryReader.ReadUInt32())
                        ddsfilesize.Add(binaryReader.ReadUInt32())

                        binaryReader.ReadUInt16()
                        binaryReader.ReadUInt16()
                    Next


                    memory.Position = frameinfoptr

                    Dim filestreama As New FileStream(Tool.Dir("Data\graphics\unit\SD\image" & entrynum & "_frameinfo"), FileMode.Create)
                    Dim tbinarywriter As New BinaryWriter(filestreama)
                    tbinarywriter.Write(binaryReader.ReadBytes(framescount * 16))
                    tbinarywriter.Close()
                    filestreama.Close()

                    For i = 0 To layercount - 1
                        If ddsptr(i) <> 0 Then
                            If layerstrs(i).Trim = "diffuse" Then
                                memory.Position = ddsptr(i)
                                Dim tfilestreama As New FileStream(Tool.Dir("Data\temp\tempdds"), FileMode.Create)
                                Dim ttbinarywriter As New BinaryWriter(tfilestreama)
                                ttbinarywriter.Write(binaryReader.ReadBytes(ddsfilesize(i)))
                                ttbinarywriter.Close()
                                tfilestreama.Close()

                                Dim tbmp As System.Drawing.Bitmap = DevIL.DevIL.LoadBitmap(Tool.Dir("Data\temp\tempdds"))

                                tbmp.Save(Tool.Dir("Data\graphics\unit\SD\image" & entrynum & "_" & layerstrs(i).Trim & ".png"), Imaging.ImageFormat.Png)

                            ElseIf layerstrs(i).Trim = "teamcolor" Then
                                memory.Position = ddsptr(i)
                                Dim tfilestreama As New FileStream(Tool.Dir("Data\graphics\unit\SD\image" & entrynum & "_" & layerstrs(i).Trim & ".bmp"), FileMode.Create)
                                Dim ttbinarywriter As New BinaryWriter(tfilestreama)
                                ttbinarywriter.Write(binaryReader.ReadBytes(ddsfilesize(i)))
                                ttbinarywriter.Close()
                                tfilestreama.Close()
                            End If
                        End If
                    Next
                End If
            End If
        Next







        binaryReader.Close()
        memory.Close()
    End Sub


    '타일 7 * 4가지.(SD HD 크립)
    '유닛 은 몇가지지...
    Private MaxCount As Integer = 19

    Private Sub DataLoadproc()
        Dim currentPercent As Integer = 0
        Dim lastValue As Integer

        ChangeText("초기화 중")
        ChangeProgress(0)
        '폴더 만들기

        Tool.CreateDir("Data\temp")

        Tool.CreateDir("Data")
        Tool.CreateDir("Data\graphics")
        Tool.CreateDir("Data\graphics\tileset")
        Tool.CreateDir("Data\graphics\tileset\HD")
        Tool.CreateDir("Data\graphics\tileset\SD")
        Tool.CreateDir("Data\graphics\unit")
        Tool.CreateDir("Data\graphics\unit\HD")
        Tool.CreateDir("Data\graphics\unit\SD")

        Dim Datapath As String = PData.StarDIR & "Data\data"
        
        Dim Casc As New CascLib(Datapath)
        Casc.OpenStorage()

        Dim SDLoad = Sub(tilesetNum As Integer)
                         currentPercent += 1
                         ChangeProgress(currentPercent / MaxCount * 100)
                         ChangeText("SD " & CascLib.TileTypes(tilesetNum) & " 지형 불러오는 중...")
                         SDExtractPngToVr4(tilesetNum, Casc)
                     End Sub
        Dim HDLoad = Sub(tilesetNum As Integer)
                         currentPercent += 1
                         ChangeProgress(currentPercent / MaxCount * 100)
                         ChangeText("HD " & CascLib.TileTypes(tilesetNum) & " 지형 불러오는 중...")
                         HDExtractPngToVr4(tilesetNum, Casc)
                     End Sub


        lastValue = 0
        currentPercent += 1
        ChangeProgress(currentPercent / MaxCount * 100)
        For i = 0 To 998
            ChangeText("HD 이미지 " & i & " 추출 중...")

            If lastValue < (i \ 998) * 100 Then
                ChangeMiniProgress((i \ 998) * 100)
                lastValue = (i \ 998) * 100
            End If
            HDExtractImage(i, Casc)
        Next

        lastValue = 1
        currentPercent += 1
        ChangeProgress(currentPercent / MaxCount * 100)
        Try
            SDExtractImage(Casc)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try




        For i = 0 To 7
            SDLoad(i)
        Next


        For i = 0 To 7
            HDLoad(i)
        Next




        Casc.CloseStorage()


        'For i = 0 To 100
        '    ChangeProgress(i)
        '    Thread.Sleep(40)
        'Next



        '끝
        Dispatcher.Invoke(DispatcherPriority.Normal, New Action(
            Sub()
                forceCloas = True
                Me.Close()
            End Sub))
    End Sub

    Private Sub ChangeProgress(val As Integer)
        Dispatcher.Invoke(DispatcherPriority.Normal, New Action(
            Sub()

                Progress2.Value = val
            End Sub))
    End Sub
    Private Sub ChangeMiniProgress(val As Integer)
        Dispatcher.Invoke(DispatcherPriority.Normal, New Action(
            Sub()

                Progress1.Value = val
            End Sub))
    End Sub
    Private Sub ChangeText(val As String)
        Dispatcher.Invoke(DispatcherPriority.Normal, New Action(
            Sub()
                Label.Text = val
            End Sub))
    End Sub




    Private Sub OnLoad(sender As Object, e As RoutedEventArgs)
        forceCloas = False
        DataThread = New Thread(AddressOf DataLoadproc)
        DataThread.IsBackground = True
        DataThread.Start()
    End Sub

    Private Sub _Closing(sender As Object, e As ComponentModel.CancelEventArgs)
        If DataThread IsNot Nothing Then
            If DataThread.IsAlive = True And forceCloas = False Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub CancelButton(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub

End Class
