Imports Microsoft.VisualBasic
Imports System
Namespace TimeEditControl
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Dim serializableAppearanceObject1 As New DevExpress.Utils.SerializableAppearanceObject()
			Me.defaultLookAndFeel1 = New DevExpress.LookAndFeel.DefaultLookAndFeel()
			Me.lookUpEdit1 = New DevExpress.XtraEditors.LookUpEdit()
			Me.popupTimeEdit1 = New TimeEditControl.PopupTimeEdit()
			CType(Me.lookUpEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.popupTimeEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' lookUpEdit1
			' 
			Me.lookUpEdit1.Location = New System.Drawing.Point(259, 12)
			Me.lookUpEdit1.Name = "lookUpEdit1"
			Me.lookUpEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
			Me.lookUpEdit1.Size = New System.Drawing.Size(100, 20)
			Me.lookUpEdit1.TabIndex = 0
			' 
			' popupTimeEdit1
			' 
			Me.popupTimeEdit1.EditValue = New System.DateTime(2012, 6, 15, 0, 0, 0, 0)
			Me.popupTimeEdit1.Location = New System.Drawing.Point(13, 13)
			Me.popupTimeEdit1.Name = "popupTimeEdit1"
			Me.popupTimeEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton(), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.DropDown, "DropDown", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", Nothing, Nothing, True)})
			Me.popupTimeEdit1.Size = New System.Drawing.Size(100, 20)
			Me.popupTimeEdit1.TabIndex = 1
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(371, 247)
			Me.Controls.Add(Me.popupTimeEdit1)
			Me.Controls.Add(Me.lookUpEdit1)
			Me.Name = "Form1"
			Me.Text = "Form1"
			CType(Me.lookUpEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.popupTimeEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private defaultLookAndFeel1 As DevExpress.LookAndFeel.DefaultLookAndFeel
		Private lookUpEdit1 As DevExpress.XtraEditors.LookUpEdit
		Private popupTimeEdit1 As PopupTimeEdit




	End Class
End Namespace

