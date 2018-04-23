Imports Microsoft.VisualBasic
Imports DevExpress.XtraEditors
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq

Namespace TimeEditControl
	<System.ComponentModel.DesignerCategory("")> _
	Public Class PopupTimeEdit
		Inherits TimeEdit
		Public isPopupOpen As Boolean
		Private popupForm As PopupTimeEditForm

		Shared Sub New()
			RepositoryItemPopupTimeEdit.Register()
		End Sub
		Public Sub New()
			AddHandler MaskBox.MouseClick, AddressOf CustomTimeEdit_MouseClick
		End Sub

		Protected Overrides Sub Finalize()
			isPopupOpen = False
			UnsubscribeEvents()
			If popupForm IsNot Nothing Then
				popupForm.Dispose()
			End If
		End Sub

		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return RepositoryItemPopupTimeEdit.EditorName
			End Get
		End Property

		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public Shadows ReadOnly Property Properties() As RepositoryItemPopupTimeEdit
			Get
				Return TryCast(MyBase.Properties, RepositoryItemPopupTimeEdit)
			End Get
		End Property

		Protected Overrides Sub OnPressButton(ByVal buttonInfo As DevExpress.XtraEditors.Drawing.EditorButtonObjectInfoArgs)
			If isPopupOpen Then
				ClosePopup()
			Else
				If buttonInfo.Button.Index = 1 Then
						ShowPopup()
				Else
					MyBase.OnPressButton(buttonInfo)
				End If
			End If
		End Sub

		Protected Overrides Sub OnParentChanged(ByVal e As EventArgs)
			If Parent IsNot Nothing Then
				RemoveHandler Parent.LocationChanged, AddressOf Parent_LocationChanged
			End If
			MyBase.OnParentChanged(e)
			If Parent IsNot Nothing Then
				AddHandler Parent.LocationChanged, AddressOf Parent_LocationChanged
			End If
		End Sub

		Private Sub Parent_LocationChanged(ByVal sender As Object, ByVal e As EventArgs)
			If popupForm IsNot Nothing Then
				popupForm.SetLocation(Me)
			End If
		End Sub

		Public Overridable Sub ShowPopup()
			popupForm = GetPopupForm(Me)
			isPopupOpen = True
			popupForm.Show()
			popupForm.Focus()
		End Sub

		Protected Overridable Function GetPopupForm(ByVal edit As PopupTimeEdit) As PopupTimeEditForm
			If popupForm Is Nothing Then
				popupForm = New PopupTimeEditForm(edit)
			End If

			Dim newTime As DateTime = ParseCurrentTime(Text)
			popupForm.popupControl.CurrentTime = newTime
			Return popupForm
		End Function

		Private Function ParseCurrentTime(ByVal text As String) As DateTime
			Dim result As DateTime = DateTime.Now
			Try
				result = DateTime.Parse(text)
			Catch e1 As Exception
			End Try
			Return result

		End Function
		Private Sub CustomTimeEdit_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
			If isPopupOpen Then
				ClosePopup()
			End If
		End Sub

		Protected Overridable Sub ClosePopup()
			isPopupOpen = False
			popupForm.Hide()
		End Sub

		Private Sub UnsubscribeEvents()
			If MaskBox IsNot Nothing Then
				RemoveHandler MaskBox.MouseClick, AddressOf CustomTimeEdit_MouseClick
			End If
		End Sub
	End Class
End Namespace
