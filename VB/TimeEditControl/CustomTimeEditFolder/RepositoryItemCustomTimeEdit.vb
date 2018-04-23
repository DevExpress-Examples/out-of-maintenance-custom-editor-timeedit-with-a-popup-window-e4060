Imports Microsoft.VisualBasic
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraEditors.Registrator
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ViewInfo
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq

Namespace TimeEditControl
	<System.ComponentModel.DesignerCategory(""), UserRepositoryItem("Register")> _
	Public Class RepositoryItemPopupTimeEdit
		Inherits RepositoryItemTimeEdit
		Friend Const EditorName As String = "PopupTimeEdit"

		Shared Sub New()
			Register()
		End Sub
		Public Sub New()
		End Sub

		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return EditorName
			End Get
		End Property

		Private Sub AddDropDownButton()
			Dim dropDown As New EditorButton()
			dropDown.Caption = "DropDown"
			dropDown.Kind = ButtonPredefines.DropDown
			dropDown.IsDefaultButton = True
			Buttons.Add(dropDown)
		End Sub

		Public Overrides Sub CreateDefaultButton()
			MyBase.CreateDefaultButton()
			AddDropDownButton()
		End Sub

		Public Shared Sub Register()
			EditorRegistrationInfo.Default.Editors.Add(New EditorClassInfo(EditorName, GetType(PopupTimeEdit), GetType(RepositoryItemPopupTimeEdit), GetType(BaseSpinEditViewInfo), New ButtonEditPainter(), True))
		End Sub
	End Class
End Namespace
