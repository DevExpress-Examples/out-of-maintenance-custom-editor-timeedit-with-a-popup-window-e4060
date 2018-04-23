Imports Microsoft.VisualBasic
Imports DevExpress.XtraEditors
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq

Namespace TimeEditControl
	Partial Public Class PopupTimeEditForm
		Inherits XtraForm
		Public popupControl As TimeEditPopupControl
		Public panel As PanelControl

		Public Sub New(ByVal ownerEdit As PopupTimeEdit)
			InitializeComponent()
			SetLocation(ownerEdit)

			CreatePopupControl(ownerEdit)
			CreatePanel()

			ClientSize = panel.ClientSize
			Controls.Add(panel)
		End Sub

		Private Sub CreatePanel()
			Const bordersWidth As Integer = 2
			panel = New PanelControl()
			panel.ClientSize = New Size(popupControl.Bounds.Width + bordersWidth, popupControl.Bounds.Height + bordersWidth)
			panel.Controls.Add(popupControl)
		End Sub

		Private Sub CreatePopupControl(ByVal ownerEdit As PopupTimeEdit)
			popupControl = New TimeEditPopupControl(ownerEdit)
			popupControl.Location = New Point(1, 1)
		End Sub

		Protected Overrides Sub Finalize()
			popupControl.Dispose()
			panel.Dispose()
			GC.Collect()
		End Sub

		Public Sub SetLocation(ByVal ownerEdit As PopupTimeEdit)
			Dim editorLocation As Point = ownerEdit.PointToScreen(Point.Empty)
			editorLocation.Y += ownerEdit.Size.Height
			Location = editorLocation
		End Sub
	End Class
End Namespace
