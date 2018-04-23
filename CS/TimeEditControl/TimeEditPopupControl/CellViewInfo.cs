using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace TimeEditControl
{

    public class CellViewInfo
    {
        public enum CellType { Hour, Minute, Format };
        public bool isHotPointed { get; set; }
        private bool _isSelected;
        public bool isSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                isHotPointed = false;
                _isSelected = value;
            }
        }
        public CellType Type { get; set; }
        public Font cellTextFont { get; set; }
        public string CellText { get; set; }

        public Rectangle cellBounds = Rectangle.Empty;

        public CellViewInfo(Font _cellTextFont, string _CellText, Rectangle rectangle, CellType enumType)
        {
            cellTextFont = _cellTextFont;
            CellText = _CellText;
            cellBounds = rectangle;
            Type = enumType;
        }
        public CellViewInfo() { }

        ~CellViewInfo()
        {
            Clear();
        }

        private void Clear()
        {
            isHotPointed = false;
            isSelected = false;
            cellTextFont = null;
            cellBounds = Rectangle.Empty;
            CellText = null;
        }
    }

}
