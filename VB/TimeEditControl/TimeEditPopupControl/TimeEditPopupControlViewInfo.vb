Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Drawing
Imports DevExpress.XtraEditors
Imports DevExpress.Utils

Namespace TimeEditControl
	Public Class TimeEditPopupControlViewInfo
		Private privatePopupAppearance As AppearanceObject
		Public Property PopupAppearance() As AppearanceObject
			Get
				Return privatePopupAppearance
			End Get
			Set(ByVal value As AppearanceObject)
				privatePopupAppearance = value
			End Set
		End Property
		Private privateBounds As Rectangle
		Public Property Bounds() As Rectangle
			Get
				Return privateBounds
			End Get
			Set(ByVal value As Rectangle)
				privateBounds = value
			End Set
		End Property
		Public ownerControl As TimeEditPopupControl

		Public CellsList As New List(Of CellViewInfo)()
		Private _hotPointedCell As CellViewInfo
		Public Property HotPointedCell() As CellViewInfo
			Get
				Return _hotPointedCell
			End Get
			Set(ByVal value As CellViewInfo)
				If value Is _hotPointedCell Then
					Return
				End If
				If _hotPointedCell IsNot Nothing Then
					_hotPointedCell.isHotPointed = False
					RefreshCell(_hotPointedCell.cellBounds)
				End If
				If value IsNot Nothing AndAlso (Not value.isSelected) Then
					_hotPointedCell = value
					_hotPointedCell.isHotPointed = True
					RefreshCell(_hotPointedCell.cellBounds)
				End If
			End Set
		End Property
		Private _focusedHour As CellViewInfo
		Public Property FocusedHour() As CellViewInfo
			Get
				Return _focusedHour
			End Get
			Set(ByVal value As CellViewInfo)
				If value Is _focusedHour Then
					Return
				End If
				If _focusedHour IsNot Nothing Then
					_focusedHour.isSelected = False
					RefreshCell(_focusedHour.cellBounds)
				End If
				If value IsNot Nothing Then
					_focusedHour = value
					_focusedHour.isSelected = True
					RefreshCell(_focusedHour.cellBounds)
				End If
			End Set
		End Property
		Private _focusedMinute As CellViewInfo
		Public Property FocusedMinute() As CellViewInfo
			Get
				Return _focusedMinute
			End Get
			Set(ByVal value As CellViewInfo)
				If value Is _focusedHour Then
					Return
				End If
				If _focusedMinute IsNot Nothing Then
					_focusedMinute.isSelected = False
					RefreshCell(_focusedMinute.cellBounds)
				End If
				If value IsNot Nothing Then
					_focusedMinute = value
					_focusedMinute.isSelected = True
					RefreshCell(_focusedMinute.cellBounds)
				End If
			End Set
		End Property
		Private _format As CellViewInfo
		Public Property Format() As CellViewInfo
			Get
				Return _format
			End Get
			Set(ByVal value As CellViewInfo)
				If value Is _format Then
					Return
				End If
				If _format IsNot Nothing Then
					_format.isSelected = False
					RefreshCell(_format.cellBounds)
				End If
				If value IsNot Nothing Then
					_format = value
					_format.isSelected = True
					RefreshCell(_format.cellBounds)
				End If
			End Set
		End Property

		Public Sub New(ByVal ownerEdit As TimeEdit, ByVal timeEditPopupControl As TimeEditPopupControl)
			ownerControl = timeEditPopupControl
			UpdateAppearance(ownerEdit)
			Bounds = ownerControl.ClientRectangle
			CreateCellsList()
		End Sub

		Protected Overrides Sub Finalize()
			Clear()
		End Sub

		Public Overridable Sub CalcCurrentCells(ByVal dt As DateTime)
			Dim hour As Integer = dt.Hour
			If hour > 11 Then
				hour -= 12
				Format = CellsList(CellsList.Count - 1)
			Else
				Format = CellsList(CellsList.Count - 2)
			End If
			hour = If(hour = 0, 12, hour)
			FocusedHour = CellsList(hour - 1)

			Dim minute As Double = dt.Minute
			Dim temp As Double = minute / 5
			Dim minuteString() As String = temp.ToString().Split("."c)
			Dim minuteIndex As Integer = Integer.Parse(minuteString(0))
			If minuteString.Length > 1 Then
				Dim leftover As Integer = Integer.Parse(minuteString(1))
				If leftover > 5 Then
					minuteIndex += 1
				End If
			End If
			FocusedMinute = CellsList(12 + minuteIndex)
		End Sub

		Protected Overridable Sub UpdateAppearance(ByVal ownerEdit As TimeEdit)
			PopupAppearance = New AppearanceObject(AppearanceObject.EmptyAppearance)
			PopupAppearance.BackColor = Color.Black
			PopupAppearance.BackColor2 = Color.DarkBlue
			PopupAppearance.BorderColor = Color.LightBlue
		End Sub

		Private Sub RefreshCell(ByVal rec As Rectangle)
			If ownerControl Is Nothing Then
				Return
			End If
			ownerControl.Invalidate(rec)
		End Sub

		Protected Overridable Sub CreateCellsList()
			Const midIndent As Integer = 6
			AddCellsBlock(New Point(3, 20), CellViewInfo.CellType.Hour)
			Dim upperRightCell As CellViewInfo = CellsList(2)
			AddCellsBlock(New Point(upperRightCell.cellBounds.X + upperRightCell.cellBounds.Width + midIndent, upperRightCell.cellBounds.Y), CellViewInfo.CellType.Minute)
			AddAmPmCells()
		End Sub

		Private Sub AddAmPmCells()
			Const indent As Integer = 3
			Dim lowerLeftCell As CellViewInfo = CellsList(9)
			Dim startPoint As New Point(lowerLeftCell.cellBounds.X, lowerLeftCell.cellBounds.Y + lowerLeftCell.cellBounds.Height + indent)
			Dim cellRect As New Rectangle(startPoint, New Size(30, 20))
			CellsList.Add(New CellViewInfo(PopupAppearance.Font, "AM", cellRect, CellViewInfo.CellType.Format))
			cellRect.Location = New Point(cellRect.Location.X + CellsList(CellsList.Count - 1).cellBounds.Width, cellRect.Location.Y)
			CellsList.Add(New CellViewInfo(PopupAppearance.Font, "PM", cellRect, CellViewInfo.CellType.Format))
		End Sub

		Private Sub AddCellsBlock(ByVal startPoint As Point, ByVal Type As CellViewInfo.CellType)
			Dim r As Rectangle = Rectangle.Empty
			For i As Integer = 0 To 3
				For j As Integer = 0 To 2

					If i = 0 AndAlso j = 0 Then
						r = New Rectangle(startPoint, New Size(26, 18))
					Else
						r.Location = New Point(r.Location.X + r.Width, r.Y)
					End If
					If Type = CellViewInfo.CellType.Hour Then
						CellsList.Add(New CellViewInfo(PopupAppearance.Font, (i * 3 + j + 1).ToString(), r, Type))
					Else
						Dim x As Integer = (i * 3 + j) * 5
						Dim result As String = If(x < 10, String.Format("0{0}", x), x.ToString())
						CellsList.Add(New CellViewInfo(PopupAppearance.Font, result, r, Type))
					End If
				Next j
				r.X = startPoint.X - r.Width
				r.Y += r.Height
			Next i
		End Sub

		Private Sub Clear()
			PopupAppearance.Dispose()
			CellsList.Clear()
			Bounds = Rectangle.Empty
			ownerControl = Nothing
			HotPointedCell = Nothing
			FocusedMinute = Nothing
			FocusedHour = Nothing
			Format = Nothing

		End Sub
	End Class
End Namespace
