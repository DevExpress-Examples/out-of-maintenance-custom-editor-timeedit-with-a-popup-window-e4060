using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TimeEditControl
{
    public partial class TimeEditPopupControl : XtraUserControl
    {
        TimeEditPopupControlViewInfo ViewInfo;
        TimeEditPopupControlPainter PopupPainter;
        public PopupTimeEdit OwnerEdit;
        private DateTime _currentTime;
        public DateTime CurrentTime
        {
            get
            {
                return _currentTime;
            }
            set
            {
                _currentTime = value;
                ViewInfo.CalcCurrentCells(value);
            }
        }
        SimpleButton buttonOK;
        CellViewInfo emptyCell;
        bool isReady = false;
        const int indent = 3;

        public TimeEditPopupControl(PopupTimeEdit ownerEdit)
        {
            DoubleBuffered = true;
            Appearance.Assign(ownerEdit.Properties.Appearance);
            InitializeComponent();

            OwnerEdit = ownerEdit;
            emptyCell = new CellViewInfo();

            CreateViewInfo();
            CreatePainter();

            AddOkButton();
            CalcClientSize();
            
            LostFocus += TimeEditPopupControl_LostFocus;          
        }

        ~TimeEditPopupControl()
        {
            Clear();
        }

        private void Clear()
        {
            emptyCell = null;
            OwnerEdit = null;
            isReady = false;
            LostFocus -= TimeEditPopupControl_LostFocus;
            buttonOK.Click -= buttonOK_Click;
            buttonOK.Dispose();
        }

        protected virtual void CreateViewInfo()
        {
            ViewInfo = new TimeEditPopupControlViewInfo(OwnerEdit, this);            
        }

        protected virtual void CreatePainter()
        {
            PopupPainter = new TimeEditPopupControlPainter(ViewInfo);            
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (ViewInfo != null)
                ViewInfo.Bounds = ClientRectangle;
        }        

        protected override void OnPaint(PaintEventArgs e)
        {
            GraphicsCache cache = new GraphicsCache(e.Graphics);
            PopupPainter.Draw(cache);
            isReady = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            foreach (CellViewInfo cellInfo in ViewInfo.CellsList)
            {
                if (cellInfo.cellBounds.Contains(e.Location))
                {
                    ViewInfo.HotPointedCell = cellInfo;
                    return;
                }
            }
            ViewInfo.HotPointedCell = emptyCell;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            foreach (CellViewInfo cellInfo in ViewInfo.CellsList)
            {
                if (cellInfo.cellBounds.Contains(e.Location))
                {
                    if (cellInfo.Type == CellViewInfo.CellType.Hour)
                        ViewInfo.FocusedHour = cellInfo;
                    if (cellInfo.Type == CellViewInfo.CellType.Minute)
                        ViewInfo.FocusedMinute = cellInfo;
                    if (cellInfo.Type == CellViewInfo.CellType.Format)
                        ViewInfo.Format = cellInfo;
                    return;
                }
            }
        }

        private void AddOkButton()
        {
            CellViewInfo lowerRightCell = ViewInfo.CellsList[23];
            buttonOK = new SimpleButton();
            buttonOK.LookAndFeel.Assign(OwnerEdit.LookAndFeel);
            buttonOK.Size = new Size(30, 20);
            buttonOK.AllowFocus = false;
            buttonOK.Text = "OK";
            buttonOK.Location = new Point(lowerRightCell.cellBounds.X + lowerRightCell.cellBounds.Width - buttonOK.Width,
                lowerRightCell.cellBounds.Y + lowerRightCell.cellBounds.Height + indent);
            buttonOK.Click += buttonOK_Click;
            Controls.Add(buttonOK);
        }

        private void CalcClientSize()
        {
            CellViewInfo upperRightCell = ViewInfo.CellsList[14];
            int controlWidth = upperRightCell.cellBounds.X + upperRightCell.cellBounds.Width + indent;
            int controlHeight = buttonOK.Location.Y + buttonOK.Height + indent;
            ClientSize = new Size(controlWidth, controlHeight);
        }

        protected virtual void buttonOK_Click(object sender, EventArgs e)
        {
            DateTime previous = DateTime.Now;
            try
            { previous = (DateTime)OwnerEdit.EditValue; }
            catch (Exception) { }
            int minute = 0, hour = 0;

            hour = CalcHour(previous, hour, ViewInfo);
            minute = CalcMinute(previous, minute, ViewInfo);

            DateTime result = new DateTime(
                previous.Year,
                previous.Month,
                previous.Day,
                hour,
                minute,
                0);

            OwnerEdit.EditValue = result;
            
            Form f = Parent.Parent as Form;
            f.Hide();
            OwnerEdit.isPopupOpen = false;
            
        }

        protected virtual int CalcHour(DateTime source, int hour,TimeEditPopupControlViewInfo viewInfo)
        {
            if (viewInfo.FocusedHour != null)
                hour = int.Parse(viewInfo.FocusedHour.CellText);
            else
                hour = source.Hour;

            if (viewInfo.Format != null && viewInfo.Format.CellText == "PM")
                hour += 12;            

            if (viewInfo.Format != null && viewInfo.Format.CellText == "AM")
                hour = hour > 11 ? hour -= 12 : hour;
            

            if (hour >= 24)
                hour -= 24;

            return hour;
        }
        protected virtual int CalcMinute(DateTime source, int minute, TimeEditPopupControlViewInfo viewInfo)
        {
            if (ViewInfo.FocusedMinute != null)
                minute = int.Parse(viewInfo.FocusedMinute.CellText);
            else
                minute = source.Minute;
            return minute;
        }

        void TimeEditPopupControl_LostFocus(object sender, EventArgs e)
        {
            if (isReady)
            {
                Point x = OwnerEdit.Parent.PointToClient(Cursor.Position);
                if (OwnerEdit.Bounds.Contains(x))
                    return;
                Form f = Parent.Parent as Form;
                f.Hide();
                OwnerEdit.isPopupOpen = false;
            }
        }
    }
}
