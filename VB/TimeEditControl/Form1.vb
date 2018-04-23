Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.Skins

Namespace TimeEditControl
	Partial Public Class Form1
		Inherits Form
		Private skinList As List(Of String)

		Public Sub New()
			InitializeComponent()
			CreateSkinList()

			'lookUpEdit1
			lookUpEdit1.Properties.DataSource = skinList
			lookUpEdit1.Properties.NullText = "Choose skin"
			AddHandler lookUpEdit1.EditValueChanged, AddressOf lookUpEdit1_EditValueChanged

			'popupTimeEdit1
			popupTimeEdit1.Properties.LookAndFeel.UseDefaultLookAndFeel = True
		End Sub

		Private Sub CreateSkinList()
			skinList = New List(Of String)()
			For Each skin As SkinContainer In SkinManager.Default.Skins
				skinList.Add(skin.SkinName)
			Next skin
		End Sub

		Private Sub lookUpEdit1_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs)
			Dim edit As LookUpEdit = TryCast(sender, LookUpEdit)
			defaultLookAndFeel1.LookAndFeel.SkinName = TryCast(edit.EditValue, String)
		End Sub

		Protected Overrides Sub Finalize()
			RemoveHandler lookUpEdit1.EditValueChanged, AddressOf lookUpEdit1_EditValueChanged
		End Sub
	End Class
End Namespace
