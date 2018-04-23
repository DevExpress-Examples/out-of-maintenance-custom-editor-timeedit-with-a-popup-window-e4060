Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Drawing

Namespace TimeEditControl

	Public Class CellViewInfo
		Public Enum CellType
			Hour
			Minute
			Format
		End Enum
		Private privateisHotPointed As Boolean
		Public Property isHotPointed() As Boolean
			Get
				Return privateisHotPointed
			End Get
			Set(ByVal value As Boolean)
				privateisHotPointed = value
			End Set
		End Property
		Private _isSelected As Boolean
		Public Property isSelected() As Boolean
			Get
				Return _isSelected
			End Get
			Set(ByVal value As Boolean)
				isHotPointed = False
				_isSelected = value
			End Set
		End Property
		Private privateType As CellType
		Public Property Type() As CellType
			Get
				Return privateType
			End Get
			Set(ByVal value As CellType)
				privateType = value
			End Set
		End Property
		Private privatecellTextFont As Font
		Public Property cellTextFont() As Font
			Get
				Return privatecellTextFont
			End Get
			Set(ByVal value As Font)
				privatecellTextFont = value
			End Set
		End Property
		Private privateCellText As String
		Public Property CellText() As String
			Get
				Return privateCellText
			End Get
			Set(ByVal value As String)
				privateCellText = value
			End Set
		End Property

		Public cellBounds As Rectangle = Rectangle.Empty

		Public Sub New(ByVal _cellTextFont As Font, ByVal _CellText As String, ByVal rectangle As Rectangle, ByVal enumType As CellType)
			cellTextFont = _cellTextFont
			CellText = _CellText
			cellBounds = rectangle
			Type = enumType
		End Sub
		Public Sub New()
		End Sub

		Protected Overrides Sub Finalize()
			Clear()
		End Sub

		Private Sub Clear()
			isHotPointed = False
			isSelected = False
			cellTextFont = Nothing
			cellBounds = Rectangle.Empty
			CellText = Nothing
		End Sub
	End Class

End Namespace
