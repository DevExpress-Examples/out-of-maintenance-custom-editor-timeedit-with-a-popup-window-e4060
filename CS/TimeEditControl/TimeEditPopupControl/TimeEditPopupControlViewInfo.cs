using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.Utils;

namespace TimeEditControl
{
    public class TimeEditPopupControlViewInfo
    {       
        public AppearanceObject PopupAppearance { get; set; }
        public Rectangle Bounds { get; set; }
        public TimeEditPopupControl ownerControl;

        public List<CellViewInfo> CellsList = new List<CellViewInfo>();
        private CellViewInfo _hotPointedCell;
        public CellViewInfo HotPointedCell
        {
            get
            {
                return _hotPointedCell;
            }
            set
            {
                if (value == _hotPointedCell)
                    return;
                if (_hotPointedCell != null)
                {
                    _hotPointedCell.isHotPointed = false;
                    RefreshCell(_hotPointedCell.cellBounds);
                }
                if (value != null && !value.isSelected)
                {
                    _hotPointedCell = value;
                    _hotPointedCell.isHotPointed = true;
                    RefreshCell(_hotPointedCell.cellBounds);
                }
            }
        }
        private CellViewInfo _focusedHour;
        public CellViewInfo FocusedHour
        {
            get
            {
                return _focusedHour;
            }
            set
            {
                if (value == _focusedHour)
                    return;
                if (_focusedHour != null)
                {
                    _focusedHour.isSelected = false;
                    RefreshCell(_focusedHour.cellBounds);
                }
                if (value != null)
                {
                    _focusedHour = value;
                    _focusedHour.isSelected = true;
                    RefreshCell(_focusedHour.cellBounds);
                }
            }
        }
        private CellViewInfo _focusedMinute;
        public CellViewInfo FocusedMinute
        {
            get
            {
                return _focusedMinute;
            }
            set
            {
                if (value == _focusedHour)
                    return;
                if (_focusedMinute != null)
                {
                    _focusedMinute.isSelected = false;
                    RefreshCell(_focusedMinute.cellBounds);
                }
                if (value != null)
                {
                    _focusedMinute = value;
                    _focusedMinute.isSelected = true;
                    RefreshCell(_focusedMinute.cellBounds);
                }
            }
        }
        private CellViewInfo _format;
        public CellViewInfo Format
        {
            get
            {
                return _format;
            }
            set
            {
                if (value == _format)
                    return;
                if (_format != null)
                {
                    _format.isSelected = false;
                    RefreshCell(_format.cellBounds);
                }
                if (value != null)
                {
                    _format = value;
                    _format.isSelected = true;
                    RefreshCell(_format.cellBounds);
                }
            }
        }

        public TimeEditPopupControlViewInfo(TimeEdit ownerEdit, TimeEditPopupControl timeEditPopupControl)
        {
            ownerControl = timeEditPopupControl;
            UpdateAppearance(ownerEdit);
            Bounds = ownerControl.ClientRectangle;
            CreateCellsList();
        }

        ~TimeEditPopupControlViewInfo()
        {
            Clear();
        }

        public virtual void CalcCurrentCells(DateTime dt)
        {
            int hour = dt.Hour;
            if (hour > 11)
            {
                hour -= 12;
                Format = CellsList[CellsList.Count - 1]; 
            }
            else
                Format = CellsList[CellsList.Count - 2];
            hour = hour == 0 ? 12 : hour; 
            FocusedHour = CellsList[hour - 1];

            double minute = dt.Minute;
            double temp = minute / 5;
            string[] minuteString = temp.ToString().Split('.');
            int minuteIndex = int.Parse(minuteString[0]);
            if (minuteString.Length > 1)
            {
                int leftover = int.Parse(minuteString[1]);
                if (leftover > 5)
                    minuteIndex += 1;
            }
            FocusedMinute = CellsList[12 + minuteIndex];
        }

        protected virtual void UpdateAppearance(TimeEdit ownerEdit)
        {
            PopupAppearance = new AppearanceObject(AppearanceObject.EmptyAppearance);
            PopupAppearance.BackColor = Color.Black;
            PopupAppearance.BackColor2 = Color.DarkBlue;
            PopupAppearance.BorderColor = Color.LightBlue;
        }

        private void RefreshCell(Rectangle rec)
        {
            if (ownerControl == null)
                return;
            ownerControl.Invalidate(rec);
        }

        protected virtual void CreateCellsList()
        {
            const int midIndent = 6;
            AddCellsBlock(new Point(3, 20), CellViewInfo.CellType.Hour);
            CellViewInfo upperRightCell = CellsList[2];
            AddCellsBlock(new Point(upperRightCell.cellBounds.X + upperRightCell.cellBounds.Width + midIndent, upperRightCell.cellBounds.Y), CellViewInfo.CellType.Minute);
            AddAmPmCells();
        }

        private void AddAmPmCells()
        {
            const int indent = 3;
            CellViewInfo lowerLeftCell = CellsList[9];
            Point startPoint = new Point(lowerLeftCell.cellBounds.X, lowerLeftCell.cellBounds.Y + lowerLeftCell.cellBounds.Height + indent);
            Rectangle cellRect = new Rectangle(startPoint, new Size(30, 20));
            CellsList.Add(new CellViewInfo(PopupAppearance.Font, "AM", cellRect, CellViewInfo.CellType.Format));
            cellRect.Location = new Point(cellRect.Location.X + CellsList[CellsList.Count - 1].cellBounds.Width, cellRect.Location.Y);
            CellsList.Add(new CellViewInfo(PopupAppearance.Font, "PM", cellRect, CellViewInfo.CellType.Format));
        }

        private void AddCellsBlock(Point startPoint, CellViewInfo.CellType Type)
        {
            Rectangle r = Rectangle.Empty;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {

                    if (i == 0 && j == 0)
                        r = new Rectangle(startPoint, new Size(26, 18));
                    else
                    {
                        r.Location = new Point(r.Location.X + r.Width, r.Y);
                    }
                    if (Type == CellViewInfo.CellType.Hour)
                        CellsList.Add(new CellViewInfo(PopupAppearance.Font, (i * 3 + j + 1).ToString(), r, Type));
                    else
                    {
                        int x = (i * 3 + j) * 5;
                        string result = x < 10 ? string.Format("0{0}", x) : x.ToString();
                        CellsList.Add(new CellViewInfo(PopupAppearance.Font, result, r, Type));
                    }
                }
                r.X = startPoint.X - r.Width;
                r.Y += r.Height;
            }
        }

        private void Clear()
        {            
            PopupAppearance.Dispose();
            CellsList.Clear();
            Bounds = Rectangle.Empty;
            ownerControl = null;
            HotPointedCell = null;
            FocusedMinute = null;
            FocusedHour = null;
            Format = null;

        }
    }
}
