Public Class frmMain

    '---Game Line---
    'DungCoi - Lê Nguyên Dũng
    'dungcoi@virusvn.com
    '---------------

    Structure Point
        Dim x As Integer
        Dim y As Integer
    End Structure

    'Khai báo cấu trúc danh sách các điểm kề (Đường có thể đi)
    Structure listWalk
        Dim pos1 As Integer
        Dim pos2 As Integer
        'Dim lTrongSo As Long
        'Ở đây trọng số không cần dùng
    End Structure
    Structure m_listWalk
        'Khai báo danh sách các cạnh
        'Có tất cả 181 cạnh tương ứng cho ma trận 10x10
        'Việc lưu danh sách CÓ/KHÔNG giúp tối ưu tính toán cũng như lưu trữ
        '9x9x2+10+9 = 181
        Dim lWalk() As Boolean
    End Structure
    'Dim wWalk As m_listWalk
    Dim wWalk(189) As Boolean

    Structure PointNext
        Dim iColor As Integer
        Dim pos As Point
    End Structure

    Structure pathPoint
        Dim iLen As Integer
        Dim pos() As Point
    End Structure

    Private currentPos As Point
    Private posNext(2) As PointNext
    Private unitStep(3) As Point

    Private pathDo As pathPoint
    Private iGo(9, 9) As Boolean

    Private eatRule(3) As Point
    Private pPixel(100) As PictureBox
    Private pNext(2) As PictureBox

    Private iPixel(9, 9) As Integer
    Private iScore As Integer

    Private Const nColor As Integer = 4
    Dim trace As pathPoint

    '---Xu ly các pixel---
    Private Function index2pos(ByVal iIndex As Integer) As Point
        index2pos.x = iIndex Mod 10
        index2pos.y = iIndex \ 10
    End Function
    Private Function pos2index(ByVal pPos As Point) As Integer
        pos2index = pPos.y * 10 + pPos.x
    End Function
    Private Sub setPixel(ByVal pos As Point, ByVal iValue As Integer)
        Dim iIndex As Integer
        Dim pNext As Point
        Dim iCount As Integer

        iIndex = pos2index(pos)
        If iValue <= -1 Then
            pPixel(iIndex).Image = Nothing
            iPixel(pos.x, pos.y) = -1

            'Khởi tạo thêm các giá trị điểm kề của điểm bắt đầu
            For iCount = 0 To 3
                pNext.x = pos.x + unitStep(iCount).x
                pNext.y = pos.y + unitStep(iCount).y

                If (pNext.x >= 0) And (pNext.x <= 9) And (pNext.y >= 0) And (pNext.y <= 9) Then
                    If (iPixel(pNext.x, pNext.y) < 0) Then
                        With wWalk
                            lstGraph1.Items.Add(pos2index(pos))
                            lstGraph2.Items.Add(pos2index(pNext))
                        End With
                    End If
                End If
            Next iCount
        Else
            pPixel(iIndex).Image = imgList.Images.Item(iValue)
            iPixel(pos.x, pos.y) = iValue

            Dim iX As Integer
            Dim iTmp As Integer

            'Xóa cạnh trong list
            iTmp = pos2index(pos)
            For iX = lstGraph1.Items.Count - 1 To 0 Step -1
                If (lstGraph1.Items(iX) = iTmp) Or (lstGraph2.Items(iX) = iTmp) Then
                    lstGraph1.Items.RemoveAt(iX)
                    lstGraph2.Items.RemoveAt(iX)
                End If
            Next
        End If
    End Sub
    Private Function getPixel(ByVal pos As Point) As Integer
        getPixel = iPixel(pos.x, pos.y)
    End Function
    Private Sub movePixel(ByVal pFrom As Point, ByVal pTo As Point)
        setPixel(pTo, getPixel(pFrom))
        setPixel(pFrom, -1)
    End Sub
    '---------------------
    Private Sub initGame()
        iScore = 0
        lblScore.Text = iScore
        '---Khoi tao luat di chuyen---
        unitStep(0).x = 0
        unitStep(0).y = 1

        unitStep(1).x = 1
        unitStep(1).y = 0

        unitStep(2).x = 0
        unitStep(2).y = -1

        unitStep(3).x = -1
        unitStep(3).y = 0
        '---Khoi tao luat an---
        '-- -
        eatRule(0).x = 1
        eatRule(0).y = 0
        '-- |
        eatRule(1).x = 0
        eatRule(1).y = 1
        '-- /
        eatRule(2).x = 1
        eatRule(2).y = 1
        '-- \
        eatRule(3).x = 1
        eatRule(3).y = -1
        '---
        ReDim pathDo.pos(101)

        Dim pTmp As Point
        '---
        Dim x As Integer
        Dim y As Integer
        For x = 0 To 9
            For y = 0 To 9
                pTmp.x = x
                pTmp.y = y
                setPixel(pTmp, -1)
            Next y
        Next x
        Dim iCount As Integer
        Dim iColor As Integer

        For iCount = 0 To 4
            pTmp = randomAPoint()
            iColor = randomColor()
            setPixel(pTmp, iColor)
        Next
        randomNextPixel()
        currentPos.x = -1
        currentPos.y = -1

        initWalk()

    End Sub
    Private Sub randomNextPixel()
        Dim iCount As Integer
        Dim pTmp As Point
        Dim iIndex As Integer
        Dim iColor As Integer

        For iCount = 0 To 2
            If isFull() = False Then
                pTmp = randomAPoint()
                iColor = randomColor()
                iIndex = pos2index(pTmp)

                pNext(iCount).Image = imgList.Images.Item(iColor)
                pPixel(iIndex).Image = imgSmall.Images.Item(iColor)
                posNext(iCount).pos = pTmp
                posNext(iCount).iColor = iColor
            End If
        Next
    End Sub
    Private Function randomAPoint() As Point
        If isFull() = False Then
            Randomize()
            Dim x As Integer
            Dim y As Integer
            x = Rnd() * 9 '+ 1
            y = Rnd() * 9 '+ 1
            Do While iPixel(x, y) > -1
                x = Rnd() * 9 '+ 1
                y = Rnd() * 9 '+ 1
            Loop
            randomAPoint.x = x
            randomAPoint.y = y
        End If
    End Function
    Private Function randomColor() As Integer
        Randomize()
        randomColor = (nColor - 1) * Rnd()
    End Function
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblScore.Text = "0"
        createGrid()
        initGame()
    End Sub
    Private Sub createGrid()
        Dim iHeight As Integer = pnGame.Height / 10
        Dim iWidth As Integer = pnGame.Width / 10

        Dim iCol As Integer
        Dim iRow As Integer

        Dim iIndex As Integer

        For iRow = 1 To 10
            For iCol = 1 To 10
                iIndex = (iRow - 1) * 10 + (iCol - 1)
                pPixel(iIndex) = New PictureBox()
                With pPixel(iIndex)
                    .Width = iWidth
                    .Height = iHeight
                    .Left = iWidth * (iCol - 1)
                    .Top = iHeight * (iRow - 1)
                    .BackgroundImage = picBgr.BackgroundImage
                    .BackgroundImageLayout = ImageLayout.Stretch
                    '.BackColor = System.Drawing.Color.FromArgb(CType(CType(206, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(253, Byte), Integer))
                    'Color.Pink
                    .BorderStyle = BorderStyle.None
                    .SizeMode = PictureBoxSizeMode.StretchImage
                End With

                pnGame.Controls.Add(pPixel(iIndex))
                AddHandler pPixel(iIndex).Click, AddressOf picPixel_Click
            Next iCol
        Next iRow

        For iCol = 0 To 2
            pNext(iCol) = New PictureBox()
            With pNext(iCol)
                .Width = 74
                .Height = 74
                .Top = 3
                .Left = .Width * iCol + 3
                '.BorderStyle = BorderStyle.Fixed3D
                .SizeMode = PictureBoxSizeMode.StretchImage
            End With
            pnInfo.Controls.Add(pNext(iCol))
        Next
    End Sub

    Private isTake As Boolean
    Private Sub picPixel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer
        Dim iTmp As Integer
        iIndex = pnGame.Controls.GetChildIndex(sender)

        'New pos
        Dim newPos As Point
        newPos = index2pos(iIndex)
        iTmp = pos2index(currentPos)

        Dim iRet As Integer

        If iIndex <> iTmp Then
            If (iPixel(newPos.x, newPos.y) = -1) And (currentPos.x <> -1) Then
                pathDo.iLen = 0

                '--Khoi tao gia tri buoc di---
                Dim iX As Integer
                Dim iY As Integer
                For iX = 0 To 9
                    For iY = 0 To 9
                        iGo(iX, iY) = False
                    Next
                Next
                '-----

                Dim objPlayer As New System.Media.SoundPlayer
                Dim curPath As String = Environment.CurrentDirectory()

                findPath(currentPos, newPos)
                iRet = trace.iLen

                If iRet > 0 Then
                    'Di chuyển

                    If Microsoft.VisualBasic.FileIO.FileSystem.FileExists(objPlayer.SoundLocation = curPath & "\Sound\move.wav") = True Then
                        objPlayer.SoundLocation = curPath & "\Sound\move.wav"
                        objPlayer.Play()
                    End If
                    movePixel(currentPos, newPos)
                    currentPos.x = -1
                    currentPos.y = -1

                    '---Note
                    isTake = checkTake()
                    TimerGo.Enabled = True
                Else
                    iTmp = -1
                    If Microsoft.VisualBasic.FileIO.FileSystem.FileExists(objPlayer.SoundLocation = curPath & "\Sound\cantmove.wav") = True Then
                        objPlayer.SoundLocation = curPath & "\Sound\cantmove.wav"
                        objPlayer.Play()
                    End If
                    'MessageBox.Show("Không thể di chuyển tới đó nhé", "Oh no", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                If iPixel(newPos.x, newPos.y) > -1 Then
                    pPixel(iIndex).BackgroundImage = picBgrSel.BackgroundImage
                    'pPixel(iIndex).BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(169, Byte), Integer))
                    'Color.Aqua
                    currentPos = newPos
                End If
            End If

            If iTmp > -1 Then
                pPixel(iTmp).BackgroundImage = picBgr.BackgroundImage
                'pPixel(iTmp).BackColor = System.Drawing.Color.FromArgb(CType(CType(206, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(253, Byte), Integer))
                'Color.Pink
            End If
        End If
    End Sub

    Private Sub nextStep()
        Dim iCount As Integer
        Dim iX As Integer
        Dim iTmp As Integer
        For iCount = 0 To 2
            If iPixel(posNext(iCount).pos.x, posNext(iCount).pos.y) <= -1 Then
                setPixel(posNext(iCount).pos, posNext(iCount).iColor)

                'Xóa cạnh trong list
                iTmp = pos2index(posNext(iCount).pos)
                For iX = lstGraph1.Items.Count - 1 To 0 Step -1
                    If (lstGraph1.Items(iX) = iTmp) Or (lstGraph2.Items(iX) = iTmp) Then
                        lstGraph1.Items.RemoveAt(iX)
                        lstGraph2.Items.RemoveAt(iX)
                    End If
                Next
            Else
                Dim pTmp As Point
                Dim iColor As Integer
                pTmp = randomAPoint()
                iColor = randomColor()
                setPixel(pTmp, iColor)
            End If
        Next
        randomNextPixel()

        If isFull() = True Then
            MessageBox.Show("Bye bye, bạn thua rồi, luyện thêm đi bạn", "You lost", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    '---Xu ly ma tran---
    Private Function isFull() As Boolean
        isFull = True
        Dim iCol As Integer
        Dim iRow As Integer

        For iRow = 0 To 9
            For iCol = 0 To 9
                If iPixel(iRow, iCol) = -1 Then Return False
            Next iCol
        Next iRow
    End Function
    Private Function isLienThong1(ByVal pFrom As Point, ByVal pTo As Point) As Integer
        If (pFrom.x = pTo.x) And (pFrom.y = pTo.y) Then Return 0
        iGo(pFrom.x, pFrom.y) = True

        Dim iRet As Integer
        Dim iCount As Integer
        Dim pNext As Point
        For iCount = 0 To 3
            pNext.x = pFrom.x + unitStep(iCount).x
            pNext.y = pFrom.y + unitStep(iCount).y

            If (pNext.x >= 0) And (pNext.x <= 9) And (pNext.y >= 0) And (pNext.y <= 9) Then
                If iPixel(pNext.x, pNext.y) < 0 Then
                    If iGo(pNext.x, pNext.y) = False Then
                        iRet = isLienThong(pNext, pTo)
                        If iRet >= 0 Then
                            Return iRet + 1
                        End If
                    End If
                End If
            End If
        Next iCount
        Return -1
    End Function
    Private Function checkTake() As Boolean
        checkTake = False

        Dim iX As Integer
        Dim iY As Integer

        Dim posCur As Point

        Dim isHave As Integer
        isHave = False

        For iX = 0 To 9
            For iY = 0 To 9
                If iPixel(iX, iY) < 0 Then Continue For
                posCur.x = iX
                posCur.y = iY

                isHave = isHave + getEatRow(posCur)
                isHave = isHave + getEatCol(posCur)
                isHave = isHave + getEatCheo1(posCur)
                isHave = isHave + getEatCheo2(posCur)
            Next iY
        Next iX

        If isHave > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    Private Function getEatRow(ByVal posCur As Point) As Integer
        getEatRow = 0
        If iPixel(posCur.x, posCur.y) < 0 Then Return False

        Dim iFirst As Integer
        Dim iLast As Integer
        Dim iLen As Integer
        Dim posFirst As Point
        Dim posLast As Point

        Dim iX As Integer
        Dim iY As Integer
        iX = posCur.x
        iY = posCur.y

        '--Kiem tra phia truoc
        iFirst = 1
        If (iX - iFirst >= 0) And (iX - iFirst <= 9) Then
            Do While (iPixel(iX, iY) = iPixel(iX - iFirst, iY))
                iFirst = iFirst + 1
                If (iX - iFirst < 0) Or (iX - iFirst > 9) Then
                    Exit Do
                End If
            Loop
        End If

        iX = posCur.x
        iY = posCur.y
        '--Kiem tra phia sau
        iLast = 1
        If (iX + iLast >= 0) And (iX + iLast <= 9) Then
            Do While (iPixel(iX, iY) = iPixel(iX + iLast, iY))
                iLast = iLast + 1
                If (iX + iLast < 0) Or (iX + iLast > 9) Then
                    Exit Do
                End If
            Loop
        End If

        iLen = iLast + iFirst - 1

        If iLen >= 5 Then
            'Diem

            Dim objPlayer As New System.Media.SoundPlayer
            Dim curPath As String = Environment.CurrentDirectory()

            If Microsoft.VisualBasic.FileIO.FileSystem.FileExists(objPlayer.SoundLocation = curPath & "\Sound\destroy.wav") = True Then
                objPlayer.SoundLocation = curPath & "\Sound\destroy.wav"
                objPlayer.Play()
            End If

            posFirst.x = iX - iFirst + 1
            posFirst.y = iY '- iFirst + iStepY
            posLast.x = iX + iLast - 1
            posLast.y = iY '+ iLast - iStepY

            Dim i As Integer
            Dim iRet As Integer
            Dim iRetI As Integer
            iRetI = 0
            iRet = 0

            Dim pTmp As Point
            pTmp.y = iY

            Dim iVarBak As Integer
            iVarBak = iPixel(posFirst.x, posFirst.y)

            delPixels(posFirst, posLast)
            For i = posFirst.x To posLast.x
                pTmp.x = i
                iRetI = 0

                iPixel(pTmp.x, pTmp.y) = iVarBak

                iRetI = iRetI + getEatCol(pTmp)
                iPixel(pTmp.x, pTmp.y) = iVarBak

                iRetI = iRetI + getEatCheo1(pTmp)
                iPixel(pTmp.x, pTmp.y) = iVarBak

                iRetI = iRetI + getEatCheo2(pTmp)

                iRet = iRet + iRetI
                iPixel(pTmp.x, pTmp.y) = -1
            Next


            iScore = iScore + getScore(iLen + iRet)

            lblScore.Text = iScore.ToString
            getEatRow = iLen
        End If
    End Function
    Private Function getEatCol(ByVal posCur As Point) As Integer
        getEatCol = 0
        If iPixel(posCur.x, posCur.y) < 0 Then Return False

        Dim iFirst As Integer
        Dim iLast As Integer
        Dim iLen As Integer
        Dim posFirst As Point
        Dim posLast As Point

        Dim iX As Integer
        Dim iY As Integer
        iX = posCur.x
        iY = posCur.y

        '--Kiem tra phia tren
        iFirst = 1
        If (iY - iFirst >= 0) And (iY - iFirst <= 9) Then
            Do While (iPixel(iX, iY) = iPixel(iX, iY - iFirst))
                iFirst = iFirst + 1
                If (iY - iFirst < 0) Or (iY - iFirst > 9) Then
                    Exit Do
                End If
            Loop
        End If
        '--Kiem tra phia duoi
        iX = posCur.x
        iY = posCur.y
        iLast = 1
        If (iY + iLast >= 0) And (iY + iLast <= 9) Then
            Do While (iPixel(iX, iY) = iPixel(iX, iY + iLast))
                iLast = iLast + 1
                If (iY + iLast < 0) Or (iY + iLast > 9) Then
                    Exit Do
                End If
            Loop
        End If

        iLen = iLast + iFirst - 1

        If iLen >= 5 Then
            'Diem

            Dim objPlayer As New System.Media.SoundPlayer
            Dim curPath As String = Environment.CurrentDirectory()

            If Microsoft.VisualBasic.FileIO.FileSystem.FileExists(objPlayer.SoundLocation = curPath & "\Sound\destroy.wav") = True Then
                objPlayer.SoundLocation = curPath & "\Sound\destroy.wav"
                objPlayer.Play()
            End If

            posFirst.x = iX '- iStepX * iFirst + iStepX
            posFirst.y = iY - iFirst + 1
            posLast.x = iX '+ iStepX * iLast - iStepX
            posLast.y = iY + iLast - 1

            Dim i As Integer
            Dim iRet As Integer
            Dim iRetI As Integer
            iRetI = 0
            iRet = 0

            Dim pTmp As Point
            pTmp.x = iX

            Dim iVarBak As Integer
            iVarBak = iPixel(posFirst.x, posFirst.y)

            delPixels(posFirst, posLast)
            For i = posFirst.y To posLast.y
                pTmp.y = i
                iRetI = 0

                iPixel(pTmp.x, pTmp.y) = iVarBak

                iRetI = iRetI + getEatRow(pTmp)
                iPixel(pTmp.x, pTmp.y) = iVarBak

                iRetI = iRetI + getEatCheo1(pTmp)
                iPixel(pTmp.x, pTmp.y) = iVarBak

                iRetI = iRetI + getEatCheo2(pTmp)

                iRet = iRet + iRetI
                iPixel(pTmp.x, pTmp.y) = -1
            Next

            iScore = iScore + getScore(iLen + iRet)

            lblScore.Text = iScore.ToString
            getEatCol = iLen
        End If
    End Function
    Private Function getEatCheo1(ByVal posCur As Point) As Integer
        '\
        getEatCheo1 = 0
        If iPixel(posCur.x, posCur.y) < 0 Then Return False

        Dim iFirst As Integer
        Dim iLast As Integer
        Dim iLen As Integer
        Dim posFirst As Point
        Dim posLast As Point

        Dim iX As Integer
        Dim iY As Integer
        iX = posCur.x
        iY = posCur.y

        '--Kiem tra phia tren
        iFirst = 1
        If (iY - iFirst >= 0) And (iY - iFirst <= 9) And (iX - iFirst >= 0) And (iX - iFirst <= 9) Then
            Do While (iPixel(iX, iY) = iPixel(iX - iFirst, iY - iFirst))
                iFirst = iFirst + 1
                If (iY - iFirst < 0) Or (iY - iFirst > 9) Or (iX - iFirst < 0) Or (iX - iFirst > 9) Then
                    Exit Do
                End If
            Loop
        End If
        iX = posCur.x
        iY = posCur.y
        '--Kiem tra phia duoi
        iLast = 1
        If (iY + iLast >= 0) And (iY + iLast <= 9) And (iX + iLast >= 0) And (iX + iLast <= 9) Then
            Do While (iPixel(iX, iY) = iPixel(iX + iLast, iY + iLast))
                iLast = iLast + 1
                If (iY + iLast < 0) Or (iY + iLast > 9) Or (iX + iLast < 0) Or (iX + iLast > 9) Then
                    Exit Do
                End If
            Loop
        End If

        iLen = iLast + iFirst - 1

        If iLen >= 5 Then
            'Diem

            Dim objPlayer As New System.Media.SoundPlayer
            Dim curPath As String = Environment.CurrentDirectory()
            If Microsoft.VisualBasic.FileIO.FileSystem.FileExists(objPlayer.SoundLocation = curPath & "\Sound\destroy.wav") = True Then
                objPlayer.SoundLocation = curPath & "\Sound\destroy.wav"
                objPlayer.Play()
            End If

            posFirst.x = iX - iFirst + 1
            posFirst.y = iY - iFirst + 1
            posLast.x = iX + iLast - 1
            posLast.y = iY + iLast - 1

            Dim iRet As Integer
            iRet = 0
            Dim iCount As Integer
            Dim pTmp As Point

            Dim iRetI As Integer
            iRetI = 0
            Dim iVarBak As Integer
            iVarBak = iPixel(posFirst.x, posFirst.y)

            '---Delete pixel---
            For iCount = 0 To iLen - 1
                pTmp.x = posFirst.x + iCount
                pTmp.y = posFirst.y + iCount
                setPixel(pTmp, -1)
            Next

            For iCount = 0 To iLen - 1
                pTmp.x = posFirst.x + iCount
                pTmp.y = posFirst.y + iCount

                iRetI = 0
                iPixel(pTmp.x, pTmp.y) = iVarBak
                iRetI = iRetI + getEatRow(pTmp)
                iPixel(pTmp.x, pTmp.y) = iVarBak

                iRetI = iRetI + getEatCol(pTmp)
                iPixel(pTmp.x, pTmp.y) = iVarBak

                iPixel(pTmp.x, pTmp.y) = iVarBak
                iRetI = iRetI + getEatCheo2(pTmp)

                iRet = iRet + iRetI

                iPixel(pTmp.x, pTmp.y) = -1
            Next

            iScore = iScore + getScore(iLen + iRet)

            lblScore.Text = iScore.ToString
            getEatCheo1 = iLen
        End If
    End Function
    Private Function getEatCheo2(ByVal posCur As Point) As Integer
        '/
        getEatCheo2 = 0
        If iPixel(posCur.x, posCur.y) < 0 Then Return False

        Dim iFirst As Integer
        Dim iLast As Integer
        Dim iLen As Integer
        Dim posFirst As Point
        Dim posLast As Point

        Dim iX As Integer
        Dim iY As Integer
        iX = posCur.x
        iY = posCur.y

        '--Kiem tra phia tren
        iFirst = 1
        If (iY + iFirst >= 0) And (iY + iFirst <= 9) And (iX - iFirst >= 0) And (iX - iFirst <= 9) Then
            Do While (iPixel(iX, iY) = iPixel(iX - iFirst, iY + iFirst))
                'Giam x, tang y
                iFirst = iFirst + 1
                If (iY + iFirst < 0) Or (iY + iFirst > 9) Or (iX - iFirst < 0) Or (iX - iFirst > 9) Then
                    Exit Do
                End If
            Loop
        End If
        iX = posCur.x
        iY = posCur.y
        '--Kiem tra phia duoi
        iLast = 1
        If (iY - iLast >= 0) And (iY - iLast <= 9) And (iX + iLast >= 0) And (iX + iLast <= 9) Then
            Do While (iPixel(iX, iY) = iPixel(iX + iLast, iY - iLast))
                'Tang x, giam y
                iLast = iLast + 1
                If (iY - iLast < 0) Or (iY - iLast > 9) Or (iX + iLast < 0) Or (iX + iLast > 9) Then
                    Exit Do
                End If
            Loop
        End If

        iLen = iLast + iFirst - 1

        If iLen >= 5 Then
            'Diem

            Dim objPlayer As New System.Media.SoundPlayer
            Dim curPath As String = Environment.CurrentDirectory()
            If Microsoft.VisualBasic.FileIO.FileSystem.FileExists(objPlayer.SoundLocation = curPath & "\Sound\destroy.wav") = True Then
                objPlayer.SoundLocation = curPath & "\Sound\destroy.wav"
                objPlayer.Play()
            End If

            posFirst.x = iX - iFirst + 1
            posFirst.y = iY + iFirst - 1

            posLast.x = iX + iLast - 1
            posLast.y = iY - iLast + 1

            '---Delete pixel---
            Dim iCount As Integer
            Dim pTmp As Point

            Dim iRet As Integer
            iRet = 0
            Dim iRetI As Integer
            iRetI = 0
            Dim iVarBak As Integer
            iVarBak = iPixel(posFirst.x, posFirst.y)

            For iCount = 0 To iLen - 1
                'Tang x, giam y
                pTmp.x = posFirst.x + iCount
                pTmp.y = posFirst.y - iCount
                setPixel(pTmp, -1)
            Next

            For iCount = 0 To iLen - 1
                'Tang x, giam y
                pTmp.x = posFirst.x + iCount
                pTmp.y = posFirst.y - iCount

                iRetI = 0
                iPixel(pTmp.x, pTmp.y) = iVarBak
                iRetI = iRetI + getEatRow(pTmp)
                iPixel(pTmp.x, pTmp.y) = iVarBak

                iRetI = iRetI + getEatCol(pTmp)
                iPixel(pTmp.x, pTmp.y) = iVarBak

                iPixel(pTmp.x, pTmp.y) = iVarBak
                iRetI = iRetI + getEatCheo1(pTmp)

                iRet = iRet + iRetI
                iPixel(pTmp.x, pTmp.y) = -1
            Next

            iScore = iScore + getScore(iLen + iRet)

            lblScore.Text = iScore.ToString
            getEatCheo2 = iLen
        End If
    End Function

    Private Sub delPixels(ByVal pFrom As Point, ByVal pTo As Point)
        'pFrom < pTo
        Dim iX As Integer
        Dim iY As Integer
        Dim pTmp As Point

        Dim iStepX As Integer
        Dim iStepY As Integer
        iStepX = 1
        iStepY = 1

        If pTo.x >= pFrom.x Then
            iStepX = 1
        Else
            iStepX = -1
        End If

        If pTo.y >= pFrom.y Then
            iStepY = 1
        Else
            iStepY = -1
        End If

        For iX = pFrom.x To pTo.x Step iStepX
            For iY = pFrom.y To pTo.y Step iStepY
                pTmp.x = iX
                pTmp.y = iY
                setPixel(pTmp, -1)
            Next
        Next
    End Sub
    Private Function getScore(ByVal lLenPixel) As Integer
        Dim iRet As Integer
        If lLenPixel < 5 Then
            iRet = 0
        Else
            iRet = lLenPixel * (lLenPixel - 4)
        End If
        Return iRet
    End Function

    Private Sub mnuGame_New_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGame_New.Click
        initGame()
    End Sub

    Private Sub mnuFile_Exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        End
    End Sub

    Private Sub mnuInfo_Author_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInfo_Author.Click
        MessageBox.Show("Lê Nguyên Dũng" & vbNewLine & "Email : dungcoi@virusvn.com", "Author", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub mnuFile_Exit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFile_Exit.Click
        End
    End Sub

    '---Code xu ly thuat toan xu ly ma tran---
    Dim last As Integer
    Dim first As Integer
    Dim queue(100) As Point
    Dim visited(9, 9) As Boolean

    Private Sub initMatrix()
        Dim i, j As Integer
        For i = 0 To 9
            For j = 0 To 9
                visited(i, j) = False
            Next
        Next
    End Sub

    '---Queue---
    Private Sub empty_queue()
        first = 1
        last = 0
    End Sub
    Private Sub add_queue(ByVal x As Point)
        last = last + 1
        queue(last) = x
    End Sub
    Private Function get_queue() As Point
        get_queue = queue(first)
        first = first + 1
    End Function
    '-----------
    Private Sub BFS_visit(ByVal s As Point)
        'Bat dau tim kiem tu diem s
        Dim x As Point
        'Dim y As Point
        initMatrix()
        empty_queue()
        add_queue(s)
        visited(s.x, s.y) = True

        Dim iCount As Integer
        Dim pNext As Point

        Do While (last >= first)
            x = get_queue()

            For iCount = 0 To 3
                pNext.x = x.x + unitStep(iCount).x
                pNext.y = x.y + unitStep(iCount).y

                If (pNext.x >= 0) And (pNext.x <= 9) And (pNext.y >= 0) And (pNext.y <= 9) Then
                    If (iPixel(pNext.x, pNext.y) < 0) And (Not visited(pNext.x, pNext.y)) Then
                        visited(pNext.x, pNext.y) = True
                        add_queue(pNext)
                    End If
                End If
            Next iCount
        Loop
    End Sub
    Private Function isLienThong(ByVal pFrom As Point, ByVal pTo As Point) As Integer
        BFS_visit(pFrom)
        findPath(pFrom, pTo)

        If visited(pTo.x, pTo.y) = True Then Return 1
        Return 0
    End Function
    Private Sub findPath1(ByVal pFrom As Point, ByVal pTo As Point)
        'Tìm kiếm sử dụng loang trên ma trận
        Dim x As Point

        Dim tim As Long
        tim = 1

        Dim pathVisited(9, 9) As Integer
        Dim i, j As Integer
        For i = 0 To 9
            For j = 0 To 9
                pathVisited(i, j) = 0
            Next
        Next

        x = pFrom

        Dim isFounded As Boolean
        isFounded = False

        Dim pNext As Point
        Dim iCount As Integer

        Dim sum As Long
        Dim lastsum As Long

        pathVisited(x.x, x.y) = 1

        Do While isFounded = False
            For i = 0 To 9
                For j = 0 To 9
                    If pathVisited(i, j) = tim Then
                        For iCount = 0 To 3
                            pNext.x = i + unitStep(iCount).x
                            pNext.y = j + unitStep(iCount).y

                            If (pNext.x >= 0) And (pNext.x <= 9) And (pNext.y >= 0) And (pNext.y <= 9) Then
                                If (iPixel(pNext.x, pNext.y) < 0) And (pathVisited(pNext.x, pNext.y) = 0) Then
                                    'visited(pNext.x, pNext.y) = True
                                    pathVisited(pNext.x, pNext.y) = tim + 1
                                End If
                                If (pNext.x = pTo.x) And (pNext.y = pTo.y) Then
                                    isFounded = True
                                End If
                            End If
                        Next iCount
                    End If
                Next
            Next

            If isFounded = False Then
                sum = 0
                Dim i1, j1 As Long
                For i1 = 0 To 9
                    For j1 = 0 To 9
                        sum = sum + pathVisited(i1, j1)
                    Next j1
                Next i1

                If sum = lastsum Then
                    'MsgBox("thu@")
                    Exit Do
                Else
                    lastsum = sum
                End If
            End If

            tim = tim + 1
        Loop

        If isFounded = True Then
            'Nếu tìm được đường đi

            trace.iLen = tim - 1
            ReDim trace.pos(trace.iLen)

            Dim curPos As Point
            curPos = pTo

            Do While pathVisited(curPos.x, curPos.y) > 1
                For iCount = 0 To 3
                    pNext.x = curPos.x + unitStep(iCount).x
                    pNext.y = curPos.y + unitStep(iCount).y

                    If (pNext.x >= 0) And (pNext.x <= 9) And (pNext.y >= 0) And (pNext.y <= 9) Then
                        If pathVisited(pNext.x, pNext.y) = tim - 1 Then
                            trace.pos(tim - 1) = pNext
                            curPos = pNext
                            tim = tim - 1
                            Exit For
                        End If
                    End If
                Next iCount
            Loop

            'Tiến hành vẽ cờ di chuyển
            Dim iIndex As Integer
            For iCount = 1 To trace.iLen
                iIndex = pos2index(trace.pos(iCount))
                pPixel(iIndex).Image = imgGo.Images.Item(0)
            Next
        Else
            trace.iLen = 0
        End If
    End Sub
    Private Sub findPath3(ByVal pFrom As Point, ByVal pTo As Point)
        'Tìm kiếm sử dụng mảng lưu trữ tĩnh
        initWalk3()

        Dim pNext As Point
        Dim iCount As Integer

        Dim lCount As Long
        lCount = -1

        'Khởi tạo thêm các giá trị điểm kề của điểm bắt đầu
        For iCount = 0 To 3
            pNext.x = pFrom.x + unitStep(iCount).x
            pNext.y = pFrom.y + unitStep(iCount).y

            If (pNext.x >= 0) And (pNext.x <= 9) And (pNext.y >= 0) And (pNext.y <= 9) Then
                If (iPixel(pNext.x, pNext.y) < 0) Then
                    wWalk(pos2canh(pFrom, pNext)) = pos2index(pFrom)
                End If
            End If
        Next iCount

        'Bắt đầu tìm kiếm từ iFrom
        Dim iFrom As Integer

        'Điểm đích iTo
        Dim iTo As Integer
        iTo = pos2index(pTo)

        Dim tim As Long
        tim = 1

        Dim pathVisited(99) As Integer
        Dim i As Integer
        For i = 0 To 99
            pathVisited(i) = 0
        Next

        iFrom = pos2index(pFrom)

        Dim isFounded As Boolean
        isFounded = False

        Dim sum As Long
        Dim lastsum As Long

        pathVisited(iFrom) = 1

        Dim iNext As Integer
        Do While isFounded = False
            For i = 0 To 99
                If pathVisited(i) = tim Then
                    For iCount = 0 To 3
                        If (pos2canhKe(i, iCount, iNext) >= 0) Then
                            'Tìm điểm kề điểm hiện tại

                            If pathVisited(iNext) = 0 Then
                                pathVisited(iNext) = tim + 1
                            End If
                            If (iNext = iTo) Then
                                isFounded = True
                            End If
                        End If
                    Next iCount
                End If
            Next

            If isFounded = False Then
                sum = 0
                Dim i1 As Long
                For i1 = 0 To 99
                    sum = sum + pathVisited(i1)
                Next i1

                If sum = lastsum Then
                    'MsgBox("thu@")
                    Exit Do
                Else
                    lastsum = sum
                End If
            End If

            tim = tim + 1
        Loop

        If isFounded = True Then
            'Nếu tìm được đường đi

            trace.iLen = tim - 1
            ReDim trace.pos(trace.iLen)

            Dim curIndex As Integer
            curIndex = iTo

            Do While pathVisited(curIndex) > 1
                For iCount = 0 To 3
                    If (pos2canhKe(curIndex, iCount, iNext) >= 0) Then

                        If pathVisited(iNext) = tim - 1 Then
                            trace.pos(tim - 1) = index2pos(iNext)
                            curIndex = iNext
                            tim = tim - 1
                            Exit For
                        End If
                    End If
                Next iCount
            Loop

            'Tiến hành vẽ cờ di chuyển
            Dim iIndex As Integer
            For iCount = 1 To trace.iLen
                iIndex = pos2index(trace.pos(iCount))
                pPixel(iIndex).Image = imgGo.Images.Item(0)
            Next
        Else
            trace.iLen = 0
        End If
    End Sub
    Private Function pos2canh(ByVal pos1 As Point, ByVal pos2 As Point) As Long
        'Hàm chuyển đổi từ vị trí 2 tọa độ liền kề ra chỉ số cạnh tương ứng
        'pos2 > pos1
        'Hàm chỉ chuyển đổi 2 tọa độ kề :
        '   - Chênh 1 nhau trị hàng-cột
        '   - Bằng nhau 1 trị hàng-cột

        Dim pPos1 As Point
        Dim pPos2 As Point
        'Xác định tọa lại thứ tự tọa độ
        If (pos1.x > pos2.x) Or (pos1.y > pos2.y) Then
            pPos1.x = pos2.x
            pPos1.y = pos2.y
            pPos2.x = pos1.x
            pPos2.y = pos1.y
        Else
            pPos1.x = pos1.x
            pPos1.y = pos1.y
            pPos2.x = pos2.x
            pPos2.y = pos2.y
        End If

        'Chưa xử lý lỗi (Cho các tọa độ nhập vào luôn đúng)
        Dim lRes As Long
        If pPos1.x = pPos2.x Then
            'Dọc
            lRes = pPos1.y * 10 * 2 + 10 + pPos1.x
        Else
            'Ngang
            lRes = pPos1.y * 10 * 2 + pPos1.x
        End If

        pos2canh = lRes
    End Function
    Private Sub canh2pos(ByVal lCanh As Long, ByRef pos1 As Point, ByRef pos2 As Point)
        'Hàm chuyển đổi từ giá trị cạnh để tìm ra 2 tọa độ tương ứng
        Dim lRes As Long
        lRes = lCanh Mod 20

        If lRes > 10 Then
            'Dọc
            pos1.y = lCanh \ 20
            pos2.y = lCanh \ 20 + 1
            pos1.x = lRes
            pos2.x = lRes
        Else
            'Ngang
            lRes = pos2index(pos1)
            pos1.y = lCanh \ 20
            pos2.y = lCanh \ 20
            pos1.x = lRes - 10
            pos2.x = lRes - 10
        End If
    End Sub
    Private Function pos2canhKe(ByVal iPoint As Long, ByVal lVal As Long, ByRef iRetPoint As Long) As Long
        'Hàm được gọi để trả về các cạnh tương ứng có thể có của 1 điểm
        'lVal : Vị trí cạnh
        '               1 : Trên
        '   0 : Trước   Point   2 : Sau
        '               3 : Dưới

        Dim pos As Point
        pos = index2pos(iPoint)
        Dim pNext As Point

        Dim lRes As Long
        If lVal = 2 Then
            lRes = pos.y * 10 * 2 + pos.x
            pNext.x = pos.x + 1
            pNext.y = pos.y
        ElseIf lVal = 3 Then
            lRes = pos.y * 10 * 2 + 10 + pos.x
            pNext.x = pos.x
            pNext.y = pos.y + 1
        ElseIf lVal = 0 Then
            lRes = pos.y * 10 * 2 + pos.x - 1
            pNext.x = pos.x - 1
            pNext.y = pos.y
        ElseIf lVal = 1 Then
            lRes = (pos.y - 1) * 10 * 2 + 10 + pos.x
            pNext.x = pos.x
            pNext.y = pos.y - 1
        Else
            Return -1
        End If

        iRetPoint = pos2index(pNext)
        If ((lRes >= 0) And (lRes <= 189)) And (iRetPoint >= 0) And (iRetPoint <= 99) Then

            Return lRes
        Else
            Return -1
        End If
    End Function
    Private Sub initWalk3()
        'Thiết lập danh sách kề
        Dim iX As Integer
        Dim iY As Integer

        For iX = 0 To 189
            wWalk(iX) = False
        Next

        Dim iCount As Integer

        Dim curPos As Point
        Dim pNext As Point

        For iX = 0 To 9
            For iY = 0 To 9
                If iPixel(iX, iY) < 0 Then
                    'Nếu quân cờ đang xét hiện nay là rỗng

                    curPos.x = iX
                    curPos.y = iY
                    For iCount = 0 To 1
                        pNext.x = curPos.x + unitStep(iCount).x
                        pNext.y = curPos.y + unitStep(iCount).y

                        If (pNext.x >= 0) And (pNext.x <= 9) And (pNext.y >= 0) And (pNext.y <= 9) Then
                            'Kiểm tra tính hợp lệ của điểm kề (tiến) được xét
                            If iPixel(pNext.x, pNext.y) < 0 Then
                                'Nếu là rỗng
                                wWalk(pos2canh(curPos, pNext)) = True
                            End If
                        End If
                    Next iCount
                End If
            Next
        Next
    End Sub
    '---------------------------
    Private Sub TimerGo_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerGo.Tick
        Dim iIndex As Integer
        Dim iCount As Integer
        For iCount = 1 To trace.iLen
            iIndex = pos2index(trace.pos(iCount))
            pPixel(iIndex).Image = Nothing
        Next
        TimerGo.Enabled = False
        If isTake = False Then nextStep()

        'Hiện lại các quân cờ con (Tồn tại khả năng bị xóa sau khi vẽ đường đi)

        For iCount = 0 To 2
            iIndex = pos2index(posNext(iCount).pos)
            pPixel(iIndex).Image = imgSmall.Images.Item(posNext(iCount).iColor)
        Next

        checkTake()
        'initWalk()
    End Sub
    Private Sub initWalk()
        'Thiết lập danh sách kề
        lstGraph1.Items.Clear()
        lstGraph2.Items.Clear()
        Dim iX As Integer
        Dim iY As Integer

        For iX = 0 To 189
            wWalk(iX) = False
        Next

        Dim iCount As Integer

        Dim curPos As Point
        Dim pNext As Point

        For iX = 0 To 9
            For iY = 0 To 9
                If iPixel(iX, iY) < 0 Then
                    'Nếu quân cờ đang xét hiện nay là rỗng

                    curPos.x = iX
                    curPos.y = iY
                    For iCount = 0 To 1
                        pNext.x = curPos.x + unitStep(iCount).x
                        pNext.y = curPos.y + unitStep(iCount).y

                        If (pNext.x >= 0) And (pNext.x <= 9) And (pNext.y >= 0) And (pNext.y <= 9) Then
                            'Kiểm tra tính hợp lệ của điểm kề (tiến) được xét
                            If iPixel(pNext.x, pNext.y) < 0 Then
                                'Nếu là rỗng
                                lstGraph1.Items.Add(pos2index(curPos))
                                lstGraph2.Items.Add(pos2index(pNext))
                            End If
                        End If
                    Next iCount
                End If
            Next
        Next
    End Sub
    Private Sub findPath4(ByVal pFrom As Point, ByVal pTo As Point)
        'Tìm kiếm loang sử dụng ListBox

        Dim pNext As Point
        Dim iCount As Integer

        'Khởi tạo thêm các giá trị điểm kề của điểm bắt đầu
        For iCount = 0 To 3
            pNext.x = pFrom.x + unitStep(iCount).x
            pNext.y = pFrom.y + unitStep(iCount).y

            If (pNext.x >= 0) And (pNext.x <= 9) And (pNext.y >= 0) And (pNext.y <= 9) Then
                If (iPixel(pNext.x, pNext.y) < 0) Then
                    With wWalk
                        lstGraph1.Items.Add(pos2index(pFrom))
                        lstGraph2.Items.Add(pos2index(pNext))
                    End With
                End If
            End If
        Next iCount

        'Bắt đầu tìm kiếm từ iFrom
        Dim iFrom As Integer

        'Điểm đích iTo
        Dim iTo As Integer
        iTo = pos2index(pTo)

        Dim tim As Long
        tim = 1

        Dim pathVisited(99) As Integer
        Dim i As Integer
        For i = 0 To 99
            pathVisited(i) = 0
        Next

        iFrom = pos2index(pFrom)

        Dim isFounded As Boolean
        isFounded = False

        Dim sum As Long
        Dim lastsum As Long

        pathVisited(iFrom) = 1

        Dim iNext As Integer
        Do While isFounded = False
            For i = 0 To 99
                If pathVisited(i) = tim Then
                    For iCount = 0 To lstGraph1.Items.Count - 1
                        If (lstGraph1.Items(iCount) = i) Or (lstGraph2.Items(iCount) = i) Then
                            'Tìm điểm kề điểm hiện tại
                            If lstGraph1.Items(iCount) = i Then
                                iNext = Val(lstGraph2.Items(iCount))
                            Else
                                iNext = Val(lstGraph1.Items(iCount))
                            End If

                            If pathVisited(iNext) = 0 Then
                                pathVisited(iNext) = tim + 1
                            End If
                            If (iNext = iTo) Then
                                isFounded = True
                            End If
                        End If
                    Next iCount
                End If
            Next

            If isFounded = False Then
                sum = 0
                Dim i1 As Long
                For i1 = 0 To 99
                    sum = sum + pathVisited(i1)
                Next i1

                If sum = lastsum Then
                    'MsgBox("thu@")
                    Exit Do
                Else
                    lastsum = sum
                End If
            End If

            tim = tim + 1
        Loop

        If isFounded = True Then
            'Nếu tìm được đường đi

            trace.iLen = tim - 1
            ReDim trace.pos(trace.iLen)

            Dim curIndex As Integer
            curIndex = iTo

            Do While pathVisited(curIndex) > 1
                For iCount = 0 To lstGraph1.Items.Count - 1
                    If (lstGraph1.Items(iCount) = curIndex) Or (lstGraph2.Items(iCount) = curIndex) Then
                        'Tìm điểm kề điểm hiện tại
                        If lstGraph1.Items(iCount) = curIndex Then
                            iNext = Val(lstGraph2.Items(iCount))
                        Else
                            iNext = Val(lstGraph1.Items(iCount))
                        End If

                        If pathVisited(iNext) = tim - 1 Then
                            trace.pos(tim - 1) = index2pos(iNext)
                            curIndex = iNext
                            tim = tim - 1
                            Exit For
                        End If
                    End If
                Next iCount
            Loop

            'Tiến hành vẽ cờ di chuyển
            Dim iIndex As Integer
            For iCount = 1 To trace.iLen
                iIndex = pos2index(trace.pos(iCount))
                pPixel(iIndex).Image = imgGo.Images.Item(0)
            Next
        Else
            trace.iLen = 0
            Dim iX As Integer
            Dim iTmp As Integer

            'Xóa cạnh trong list
            iTmp = pos2index(pFrom)
            For iX = lstGraph1.Items.Count - 1 To 0 Step -1
                If (lstGraph1.Items(iX) = iTmp) Or (lstGraph2.Items(iX) = iTmp) Then
                    lstGraph1.Items.RemoveAt(iX)
                    lstGraph2.Items.RemoveAt(iX)
                End If
            Next
        End If

    End Sub

    '---Stack---
    Dim stack(99) As Long
    Dim curPosStack As Long
    Private Sub initStack()
        curPosStack = -1
    End Sub
    Private Function isEmptyStack() As Boolean
        If curPosStack = -1 Then
            Return True
        End If
        Return False
    End Function
    Private Sub pushStack(ByVal lVal As Long)
        curPosStack = curPosStack + 1
        stack(curPosStack) = lVal
    End Sub
    Private Function popStack() As Long
        If isEmptyStack() = False Then
            Dim lRes As Long
            lRes = stack(curPosStack)
            curPosStack = curPosStack - 1
            Return lRes
        Else
            Return -1
        End If
    End Function
    '-----------
    Private Function isHaveCanh(ByVal iPos1 As Integer, ByVal iPos2 As Integer) As Boolean
        'Kiểm tra xem giữa 2 điểm có cạnh kề hay không
        Dim iCount As Integer

        For iCount = 0 To lstGraph1.Items.Count - 1
            If ((lstGraph1.Items(iCount) = iPos1) And (lstGraph2.Items(iCount) = iPos2)) Or ((lstGraph1.Items(iCount) = iPos2) And (lstGraph2.Items(iCount) = iPos1)) Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Sub findPath(ByVal pFrom As Point, ByVal pTo As Point)
        'Tìm kiếm sử dụng heuristic

        Dim pNext As Point
        Dim iCount As Integer

        'Khởi tạo thêm các giá trị điểm kề của điểm bắt đầu
        For iCount = 0 To 3
            pNext.x = pFrom.x + unitStep(iCount).x
            pNext.y = pFrom.y + unitStep(iCount).y

            If (pNext.x >= 0) And (pNext.x <= 9) And (pNext.y >= 0) And (pNext.y <= 9) Then
                If (iPixel(pNext.x, pNext.y) < 0) Then
                    With wWalk
                        lstGraph1.Items.Add(pos2index(pFrom))
                        lstGraph2.Items.Add(pos2index(pNext))
                    End With
                End If
            End If
        Next iCount

        'Bắt đầu tìm kiếm từ iFrom
        Dim iFrom As Integer

        'Điểm đích iTo
        Dim iTo As Integer
        iTo = pos2index(pTo)

        Dim tim As Long
        tim = 1

        Dim pathVisited(99) As Integer
        Dim i As Integer
        For i = 0 To 99
            pathVisited(i) = 0
        Next

        iFrom = pos2index(pFrom)

        Dim isFounded As Boolean
        isFounded = False

        Dim iNext As Integer

        'Khởi tạo stack
        initStack()
        'Điểm thêm điểm khởi đầu vào stack
        pushStack(iFrom)

        Dim curIndex As Integer
        Dim curPos As Point

        'Mảng lưu ưu tiên hệ số
        Dim arrUnit(4) As Integer
        pathVisited(iFrom) = 1

        Do While isEmptyStack() = False
            curIndex = popStack()

            If curIndex = iTo Then
                isFounded = True
                Exit Do
            End If

            '---Hàm heuristic---
            curPos = index2pos(curIndex)

            'Tính toán vị trí pTo nằm trong phần gốc nào so với curPos
            '   |
            '---O--------------> X
            '   |   1   |   2
            '   |-----curPos----
            '   |   4   |   3
            '   |
            '   Y

            If (curPos.x >= pTo.x) And (curPos.y >= pTo.y) Then
                'Góc 1
                If (curPos.x - pTo.x) >= (curPos.y - pTo.y) Then
                    '   |x   |2      
                    '   |1----x----4
                    '   |    |3

                    arrUnit(4) = 1
                    arrUnit(3) = 0
                    arrUnit(2) = 2
                    arrUnit(1) = 3
                Else
                    '   |    x|1      
                    '   |2----x----3
                    '   |     |4

                    arrUnit(4) = 0
                    arrUnit(3) = 1
                    arrUnit(2) = 3
                    arrUnit(1) = 2
                End If
            ElseIf (curPos.x < pTo.x) And (curPos.y > pTo.y) Then
                'Góc 2
                If (pTo.x - curPos.x) >= (curPos.y - pTo.y) Then
                    '   |    |2   x   
                    '   |4----x----1
                    '   |    |3

                    arrUnit(4) = 3
                    arrUnit(3) = 0
                    arrUnit(2) = 2
                    arrUnit(1) = 1
                Else
                    '   |    |1x   
                    '   |3----x----2
                    '   |    |4
                    arrUnit(4) = 0
                    arrUnit(3) = 3
                    arrUnit(2) = 1
                    arrUnit(1) = 2
                End If
            ElseIf (curPos.x <= pTo.x) And (curPos.y <= pTo.y) Then
                'Góc 3
                If (pTo.x - curPos.x) >= (pTo.y - curPos.y) Then
                    '   |    |3    
                    '   |4----x----1
                    '   |    |2   x

                    arrUnit(4) = 3
                    arrUnit(3) = 2
                    arrUnit(2) = 0
                    arrUnit(1) = 1
                Else
                    '   |    |4   
                    '   |3----x----2
                    '   |    |1x
                    arrUnit(4) = 2
                    arrUnit(3) = 3
                    arrUnit(2) = 1
                    arrUnit(1) = 0
                End If
            ElseIf (curPos.x > pTo.x) And (curPos.y < pTo.y) Then
                'Góc 4
                If (curPos.x - pTo.x) >= (pTo.y - curPos.y) Then
                    '   |    |3    
                    '   |1----x----4
                    '   |x   |2   

                    arrUnit(4) = 1
                    arrUnit(3) = 2
                    arrUnit(2) = 0
                    arrUnit(1) = 3
                Else
                    '   |    |4   
                    '   |2----x----3
                    '   |   x|1
                    arrUnit(4) = 2
                    arrUnit(3) = 1
                    arrUnit(2) = 3
                    arrUnit(1) = 0
                End If
            End If

            For iCount = 4 To 1 Step -1
                pNext.x = curPos.x + unitStep(arrUnit(iCount)).x
                pNext.y = curPos.y + unitStep(arrUnit(iCount)).y
                If isHaveCanh(curIndex, pos2index(pNext)) = True Then
                    If pathVisited(pos2index(pNext)) = 0 Then
                        pushStack(pos2index(pNext))
                        pathVisited(pos2index(pNext)) = pathVisited(pos2index(curPos)) + 1
                    End If
                End If
            Next

            '---Hết hàm Heuristic---
        Loop

        tim = pathVisited(pos2index(pTo))
        If isFounded = True Then
            'Nếu tìm được đường đi

            trace.iLen = tim
            ReDim trace.pos(trace.iLen)

            curIndex = iTo

            Do While pathVisited(curIndex) > 1
                For iCount = 0 To lstGraph1.Items.Count - 1
                    If (lstGraph1.Items(iCount) = curIndex) Or (lstGraph2.Items(iCount) = curIndex) Then
                        'Tìm điểm kề điểm hiện tại
                        If lstGraph1.Items(iCount) = curIndex Then
                            iNext = Val(lstGraph2.Items(iCount))
                        Else
                            iNext = Val(lstGraph1.Items(iCount))
                        End If

                        If pathVisited(iNext) = tim - 1 Then
                            trace.pos(tim - 1) = index2pos(iNext)
                            curIndex = iNext
                            tim = tim - 1
                            Exit For
                        End If
                    End If
                Next iCount
            Loop

            'Tiến hành vẽ cờ di chuyển
            Dim iIndex As Integer
            For iCount = 1 To trace.iLen - 1
                iIndex = pos2index(trace.pos(iCount))
                pPixel(iIndex).Image = imgGo.Images.Item(0)
            Next
        Else
            trace.iLen = 0
            Dim iX As Integer
            Dim iTmp As Integer

            'Xóa cạnh trong list
            iTmp = pos2index(pFrom)
            For iX = lstGraph1.Items.Count - 1 To 0 Step -1
                If (lstGraph1.Items(iX) = iTmp) Or (lstGraph2.Items(iX) = iTmp) Then
                    lstGraph1.Items.RemoveAt(iX)
                    lstGraph2.Items.RemoveAt(iX)
                End If
            Next
        End If
    End Sub
End Class
