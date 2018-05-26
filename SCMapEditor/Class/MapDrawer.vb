Imports SharpDX
Imports SharpDX.Direct2D1
Imports SharpDX.IO
Imports SharpDX.Mathematics.Interop
Imports SharpDX.WIC
Imports System

Class MapDrawer
    Inherits D2dControl.D2dControl


    Public Property IsHD As Boolean = False
    Public Property ViewSize As Double = 1.0





    'Private x As Single = 0
    'Private y As Single = 0
    'Private w As Single = 10
    'Private h As Single = 10
    'Private dx As Single = 1
    'Private dy As Single = 1


    Private textFormat As SharpDX.DirectWrite.TextFormat

    'Private rnd As Random = New Random()



    Private Sub LoadRes(filename As String, key As String)
        Dim imagingFactory As ImagingFactory
        Dim fileStream As NativeFileStream
        Dim bitmapDecoder As BitmapDecoder
        Dim frame As BitmapFrameDecode
        Dim converter As FormatConverter

        imagingFactory = New ImagingFactory()
        fileStream = New NativeFileStream(Tool.Dir(filename), NativeFileMode.Open, NativeFileAccess.Read)
        bitmapDecoder = New BitmapDecoder(imagingFactory, fileStream, DecodeOptions.CacheOnDemand)

        frame = bitmapDecoder.GetFrame(0)

        converter = New FormatConverter(imagingFactory)
        converter.Initialize(frame, WIC.PixelFormat.Format32bppPRGBA)

        resCache.Add(key, Function(t) Bitmap1.FromWicBitmap(t, converter))
    End Sub
    Private Sub LoadteamColorRes(filename As String, key As String)
        Dim imagingFactory As ImagingFactory
        Dim fileStream As NativeFileStream
        Dim bitmapDecoder As BitmapDecoder
        Dim frame As BitmapFrameDecode
        Dim converter As FormatConverter

        imagingFactory = New ImagingFactory()
        fileStream = New NativeFileStream(Tool.Dir(filename), NativeFileMode.Open, NativeFileAccess.Read)
        bitmapDecoder = New BitmapDecoder(imagingFactory, fileStream, DecodeOptions.CacheOnDemand)

        frame = bitmapDecoder.GetFrame(0)

        converter = New FormatConverter(imagingFactory)
        converter.Initialize(frame, WIC.PixelFormat.Format32bppPRGBA)

        resCache.Add(key, Function(t) Bitmap1.FromWicBitmap(t, converter))
    End Sub

    Public Sub New()
        StarDat.Init()
        resCache.Add("RedBrush", Function(t) New SolidColorBrush(t, New RawColor4(1.0F, 0.0F, 0.0F, 1.0F)))
        resCache.Add("GreenBrush", Function(t) New SolidColorBrush(t, New RawColor4(0.0F, 1.0F, 0.0F, 1.0F)))
        resCache.Add("BlueBrush", Function(t) New SolidColorBrush(t, New RawColor4(0.0F, 0.0F, 1.0F, 1.0F)))

        textFormat = New DirectWrite.TextFormat(New SharpDX.DirectWrite.Factory(), "Nanum Gothic", 12)

        LoadRes("Data\graphics\tileset\HD\" & CascLib.TileTypes(7) & "\Tile.png", "HDtile")
        LoadRes("Data\graphics\tileset\SD\" & CascLib.TileTypes(7) & "\Tile.png", "SDtile")


        'LoadRes("Data\graphics\unit\SD\image0_diffuse.png", "HDImage0")
        'LoadRes("Data\graphics\unit\HD\image0_diffuse.png", "SDImage0")



        FpsWait = 50
    End Sub


    Private Sub DrawImage(pos As Point, imageNum As Integer, frame As Integer, isHd As Boolean, Teamcolor As Byte, target As RenderTarget, drawtype As SharpDX.Direct2D1.BitmapInterpolationMode)
        Dim keystr As String
        Dim imaigeSize As Double = 1
        If isHd Then
            imaigeSize /= 4
            keystr = "HDImage" & imageNum
        Else
            keystr = "SDImage" & imageNum
        End If

        If Not resCache.Keys.Contains(keystr) Then
            If isHd Then
                LoadRes("Data\graphics\unit\HD\image" & imageNum & "_diffuse.png", keystr)
                LoadteamColorRes("Data\graphics\unit\HD\image" & imageNum & "_teamcolor.png", keystr & "color")
            Else
                LoadRes("Data\graphics\unit\SD\image" & imageNum & "_diffuse.png", keystr)
            End If
        End If


        Dim ImageData As StarDat.ImageData
        If isHd Then
            ImageData = StarDat.HDFrameData(imageNum)
        Else
            ImageData = StarDat.SDFrameData(imageNum)
        End If


        frame = frame Mod ImageData.Frames.Count
        Dim rect As System.Drawing.Rectangle = ImageData.Frames(frame).ExtractRect
        Dim point As Point = ImageData.Frames(frame).offset

        Dim tdesrect As New System.Drawing.Rectangle(pos.X + point.X * imaigeSize, pos.Y + point.Y * imaigeSize, rect.Width * imaigeSize, rect.Height * imaigeSize)


        Dim desrect As New RawRectangleF(tdesrect.Left * ViewSize, tdesrect.Top * ViewSize, tdesrect.Right * ViewSize, tdesrect.Bottom * ViewSize)


        Dim tempRect As New RawRectangleF(rect.Left, rect.Top, rect.Right, rect.Bottom)

        target.DrawBitmap(CType(resCache(keystr), Direct2D1.Bitmap), desrect, 1, drawtype, tempRect)

        If resCache.Keys.Contains(keystr & "color") Then
            Dim tbmp As Direct2D1.Bitmap = CType(resCache(keystr & "color"), Direct2D1.Bitmap)
            target.DrawBitmap(tbmp, desrect, 0.5, drawtype, tempRect)
        End If


        Dim BitmapProperties As BitmapProperties = New BitmapProperties(New SharpDX.Direct2D1.PixelFormat(DXGI.Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied))
        '                    SharpDX.Direct2D1.AlphaMode.Premultiplied));
        '        var BitmapProperties = New BitmapProperties(New SharpDX.Direct2D1.PixelFormat(Format.B8G8R8A8_UNorm,
        '                    SharpDX.Direct2D1.AlphaMode.Premultiplied));

        'If (_renderTarget2D == null) Then
        '            {
        '    Return null;
        '}

        'SharpDX.Direct2D1.Bitmap Bitmap = New SharpDX.Direct2D1.Bitmap(_renderTarget2D, New Size2(_width, _height), stream, _width * sizeof(Int), BitmapProperties);
        '[출처] C# - SharpDX + DXGI를 이용한 윈도우 화면 캡처 소스 코드 + Direct2D 출력|작성자 techshare



        Dim brush As Brush = TryCast(resCache("RedBrush"), Brush)
        target.DrawRectangle(desrect, brush)
    End Sub

    Private test As Integer

    Public Overrides Sub Render(ByVal target As RenderTarget)
        target.Clear(New RawColor4(1.0F, 1.0F, 1.0F, 1.0F))


        Dim TempBmp As Direct2D1.Bitmap
        Dim drawType As SharpDX.Direct2D1.BitmapInterpolationMode

        If IsHD Then
            TempBmp = CType(resCache("HDtile"), Direct2D1.Bitmap)
            drawType = SharpDX.Direct2D1.BitmapInterpolationMode.Linear
        Else
            TempBmp = CType(resCache("SDtile"), Direct2D1.Bitmap)
            drawType = SharpDX.Direct2D1.BitmapInterpolationMode.NearestNeighbor
        End If



        Dim Tilesize As Integer = 32 * ViewSize
        Dim BmpSize As Integer = TempBmp.Size.Width \ 16

        For ya = 0 To 32
            For xa = 0 To 32
                target.DrawBitmap(TempBmp, New RawRectangleF(Tilesize * xa, Tilesize * ya, Tilesize * (xa + 1), Tilesize * (ya + 1)), 1, drawType, New RawRectangleF(0, xa * BmpSize, BmpSize, (xa + 1) * BmpSize))
            Next
        Next

        DrawImage(New Point(32, 32), 29, test, IsHD, 0, target, drawType)
        test += 1





        Dim brush As Brush = TryCast(resCache("RedBrush"), Brush)
        'Select Case rnd.[Next](3)
        '    Case 0

        '    Case 1
        '        brush = TryCast(resCache("GreenBrush"), Brush)
        '    Case 2
        '        brush = TryCast(resCache("BlueBrush"), Brush)
        'End Select

        target.DrawText("아스나는 씹덕인가요" & Fps, textFormat, New RawRectangleF(0, 0, 150, 20), brush)


        'target.DrawRectangle(New RawRectangleF(x, y, x + w, y + h), brush)
        'x = x + dx
        'y = y + dy

        'If x >= ActualWidth - w OrElse x <= 0 Then
        '    dx = -dx
        'End If

        'If y >= ActualHeight - h OrElse y <= 0 Then
        '    dy = -dy
        'End If

        RenderWait = 25
    End Sub
End Class