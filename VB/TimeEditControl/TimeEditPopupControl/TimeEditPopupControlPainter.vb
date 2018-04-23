Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Skins
Imports System.Drawing
Imports DevExpress.Utils.Drawing

Namespace TimeEditControl

	Public Class TimeEditPopupControlPainter
		Private PopupViewInfo As TimeEditPopupControlViewInfo
		Private drawPen As New Pen(New SolidBrush(Color.Black))

		Public Sub New(ByVal viewInfo As TimeEditPopupControlViewInfo)
			PopupViewInfo = viewInfo
		End Sub

		Protected Overrides Sub Finalize()
			drawPen = Nothing
			PopupViewInfo = Nothing
		End Sub

		Public Sub Draw(ByVal cache As GraphicsCache)
			DrawContent(cache)
		End Sub

		Private Sub DrawContent(ByVal cache As GraphicsCache)
			DrawLabels(cache)
			DrawCells(cache)
		End Sub

		Protected Overridable Sub DrawLabels(ByVal cache As GraphicsCache)
			drawPen.Color =PopupViewInfo.PopupAppearance.BackColor
			Dim HourSize As SizeF = cache.Graphics.MeasureString("Hour", PopupViewInfo.PopupAppearance.Font)
			Dim MinuteSize As SizeF = cache.Graphics.MeasureString("Minute", PopupViewInfo.PopupAppearance.Font)

			Dim HourPoint As PointF = CalcLabelPoint(PopupViewInfo.CellsList(1), HourSize)
			Dim MinutePoint As PointF = CalcLabelPoint(PopupViewInfo.CellsList(13), MinuteSize)

			cache.Graphics.DrawString("Hour", PopupViewInfo.PopupAppearance.Font, drawPen.Brush, HourPoint)
			cache.Graphics.DrawString("Minute", PopupViewInfo.PopupAppearance.Font, drawPen.Brush, MinutePoint)

		End Sub

		Protected Overridable Sub DrawCells(ByVal cache As GraphicsCache)
			For Each cellInfo As CellViewInfo In PopupViewInfo.CellsList
				drawPen.Color = PopupViewInfo.PopupAppearance.BackColor
				If cellInfo.isSelected Then
					ObjectPainter.DrawObject(cache, SkinElementPainter.Default, GetCellElementInfo(cellInfo, cache, True))
					drawPen.Color = PopupViewInfo.PopupAppearance.BackColor2
				End If
				If cellInfo.isHotPointed Then
					ObjectPainter.DrawObject(cache, SkinElementPainter.Default, GetCellElementInfo(cellInfo, cache, False))
					drawPen.Color = PopupViewInfo.PopupAppearance.BackColor2
				End If
				cache.Graphics.DrawString(cellInfo.CellText, cellInfo.cellTextFont,drawPen.Brush, SetTextPoint(cellInfo, cache.Graphics))
			Next cellInfo
		End Sub

		Private Function SetTextPoint(ByVal cellInfo As CellViewInfo, ByVal g As Graphics) As PointF
			Dim textSize As SizeF = g.MeasureString(cellInfo.CellText, PopupViewInfo.PopupAppearance.Font)
			Dim x As Single = cellInfo.cellBounds.X + cellInfo.cellBounds.Width - 3 - textSize.Width
			Dim y As Single = cellInfo.cellBounds.Y + cellInfo.cellBounds.Height - 1 - textSize.Height

			Return New PointF(x, y)
		End Function

		Protected Overridable Function GetCellElementInfo(ByVal cell As CellViewInfo, ByVal cache As GraphicsCache, ByVal selected As Boolean) As SkinElementInfo
			Dim skin As Skin = CommonSkins.GetSkin(PopupViewInfo.ownerControl.OwnerEdit.LookAndFeel.ActiveLookAndFeel)
			Dim skinInfo As SkinElementInfo
			If selected Then
				skinInfo = New SkinElementInfo(skin(CommonSkins.SkinSelection), cell.cellBounds)
			Else
				skinInfo = New SkinElementInfo(skin(CommonSkins.SkinHighlightedItem), cell.cellBounds)
			End If
			skinInfo.Cache = cache
			Return skinInfo
		End Function

		Private Function CalcLabelPoint(ByVal cell As CellViewInfo, ByVal textSize As SizeF) As PointF
			Const indent As Integer = 2
			Dim x As Single = cell.cellBounds.X + cell.cellBounds.Width \ 2 - textSize.Width \ 2
			Dim y As Single = cell.cellBounds.Y - textSize.Height - indent
			Return New PointF(x, y)
		End Function
	End Class
End Namespace
