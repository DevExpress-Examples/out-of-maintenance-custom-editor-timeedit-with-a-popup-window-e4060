Imports Microsoft.VisualBasic
Imports System
Namespace TimeEditControl
	Partial Public Class TimeEditPopupControl
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

		#Region "Component Designer generated code"

		''' <summary> 
		''' Required method for Designer support - do not modify 
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.SuspendLayout()
			' 
			' TimeEditPopupControl
			' 
			'this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			'this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Me.Name = "TimeEditPopupControl"
			Me.Size = New System.Drawing.Size(373, 267)
			Me.ResumeLayout(False)

		End Sub

		#End Region
	End Class
End Namespace
