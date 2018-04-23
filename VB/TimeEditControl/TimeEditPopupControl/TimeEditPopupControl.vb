Imports Microsoft.VisualBasic
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraEditors
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Namespace TimeEditControl
	Partial Public Class TimeEditPopupControl
		Inherits XtraUserControl
		Private ViewInfo As TimeEditPopupControlViewInfo
		Private PopupPainter As TimeEditPopupControlPainter
		Public OwnerEdit As PopupTimeEdit
		Private _currentTime As DateTime
		Public Property CurrentTime() As DateTime
			Get
				Return _currentTime
			End Get
			Set(ByVal value As DateTime)
				_currentTime = value
				ViewInfo.CalcCurrentCells(value)
			End Set
		End Property
		Private buttonOK As SimpleButton
		Private emptyCell As CellViewInfo
		Private isReady As Boolean = False
		Private Const indent As Integer = 3

		Public Sub New(ByVal ownerEdit As PopupTimeEdit)
			DoubleBuffered = True
			Appearance.Assign(ownerEdit.Properties.Appearance)
			InitializeComponent()

			Me.OwnerEdit = ownerEdit
			emptyCell = New CellViewInfo()

			CreateViewInfo()
			CreatePainter()

			AddOkButton()
			CalcClientSize()

			AddHandler LostFocus, AddressOf TimeEditPopupControl_LostFocus
		End Sub

		Protected Overrides Sub Finalize()
			Clear()
		End Sub

		Private Sub Clear()
			emptyCell = Nothing
			OwnerEdit = Nothing
			isReady = False
			RemoveHandler LostFocus, AddressOf TimeEditPopupControl_LostFocus
			RemoveHandler buttonOK.Click, AddressOf buttonOK_Click
			buttonOK.Dispose()
		End Sub

		Protected Overridable Sub CreateViewInfo()
			ViewInfo = New TimeEditPopupControlViewInfo(OwnerEdit, Me)
		End Sub

		Protected Overridable Overloads Sub CreatePainter()
			PopupPainter = New TimeEditPopupControlPainter(ViewInfo)
		End Sub

		Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
			MyBase.OnSizeChanged(e)
			If ViewInfo IsNot Nothing Then
				ViewInfo.Bounds = ClientRectangle
			End If
		End Sub

		Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
			Dim cache As New GraphicsCache(e.Graphics)
			PopupPainter.Draw(cache)
			isReady = True
		End Sub

		Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
			For Each cellInfo As CellViewInfo In ViewInfo.CellsList
				If cellInfo.cellBounds.Contains(e.Location) Then
					ViewInfo.HotPointedCell = cellInfo
					Return
				End If
			Next cellInfo
			ViewInfo.HotPointedCell = emptyCell
		End Sub

		Protected Overrides Sub OnMouseClick(ByVal e As MouseEventArgs)
			For Each cellInfo As CellViewInfo In ViewInfo.CellsList
				If cellInfo.cellBounds.Contains(e.Location) Then
					If cellInfo.Type = CellViewInfo.CellType.Hour Then
						ViewInfo.FocusedHour = cellInfo
					End If
					If cellInfo.Type = CellViewInfo.CellType.Minute Then
						ViewInfo.FocusedMinute = cellInfo
					End If
                    If cellInfo.Type = CellViewInfo.CellType.Format Then
                        ViewInfo.Format = cellInfo
                    End If
					Return
				End If
			Next cellInfo
		End Sub

		Private Sub AddOkButton()
			Dim lowerRightCell As CellViewInfo = ViewInfo.CellsList(23)
			buttonOK = New SimpleButton()
			buttonOK.LookAndFeel.Assign(OwnerEdit.LookAndFeel)
			buttonOK.Size = New Size(30, 20)
			buttonOK.AllowFocus = False
			buttonOK.Text = "OK"
			buttonOK.Location = New Point(lowerRightCell.cellBounds.X + lowerRightCell.cellBounds.Width - buttonOK.Width, lowerRightCell.cellBounds.Y + lowerRightCell.cellBounds.Height + indent)
			AddHandler buttonOK.Click, AddressOf buttonOK_Click
			Controls.Add(buttonOK)
		End Sub

		Private Sub CalcClientSize()
			Dim upperRightCell As CellViewInfo = ViewInfo.CellsList(14)
			Dim controlWidth As Integer = upperRightCell.cellBounds.X + upperRightCell.cellBounds.Width + indent
			Dim controlHeight As Integer = buttonOK.Location.Y + buttonOK.Height + indent
			ClientSize = New Size(controlWidth, controlHeight)
		End Sub

		Protected Overridable Sub buttonOK_Click(ByVal sender As Object, ByVal e As EventArgs)
			Dim previous As DateTime = DateTime.Now
			Try
				previous = CDate(OwnerEdit.EditValue)
			Catch e1 As Exception
			End Try
			Dim minute As Integer = 0, hour As Integer = 0

			hour = CalcHour(previous, hour, ViewInfo)
			minute = CalcMinute(previous, minute, ViewInfo)

			Dim result As New DateTime(previous.Year, previous.Month, previous.Day, hour, minute, 0)

			OwnerEdit.EditValue = result

			Dim f As Form = TryCast(Parent.Parent, Form)
			f.Hide()
			OwnerEdit.isPopupOpen = False

		End Sub

		Protected Overridable Function CalcHour(ByVal source As DateTime, ByVal hour As Integer, ByVal viewInfo As TimeEditPopupControlViewInfo) As Integer
			If viewInfo.FocusedHour IsNot Nothing Then
				hour = Integer.Parse(viewInfo.FocusedHour.CellText)
			Else
				hour = source.Hour
			End If

			If viewInfo.Format IsNot Nothing AndAlso viewInfo.Format.CellText = "PM" Then
				hour += 12
			End If

			If viewInfo.Format IsNot Nothing AndAlso viewInfo.Format.CellText = "AM" Then
                If (hour > 11) Then hour -= 12

            End If


            If hour >= 24 Then
                hour -= 24
            End If

            Return hour
		End Function
		Protected Overridable Function CalcMinute(ByVal source As DateTime, ByVal minute As Integer, ByVal viewInfo As TimeEditPopupControlViewInfo) As Integer
			If Me.ViewInfo.FocusedMinute IsNot Nothing Then
				minute = Integer.Parse(viewInfo.FocusedMinute.CellText)
			Else
				minute = source.Minute
			End If
			Return minute
		End Function

		Private Sub TimeEditPopupControl_LostFocus(ByVal sender As Object, ByVal e As EventArgs)
			If isReady Then
				Dim x As Point = OwnerEdit.Parent.PointToClient(Cursor.Position)
				If OwnerEdit.Bounds.Contains(x) Then
					Return
				End If
				Dim f As Form = TryCast(Parent.Parent, Form)
				f.Hide()
				OwnerEdit.isPopupOpen = False
			End If
		End Sub
	End Class
End Namespace
