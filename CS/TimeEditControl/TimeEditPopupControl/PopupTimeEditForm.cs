using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TimeEditControl
{
    public partial class PopupTimeEditForm : XtraForm
    {
        public TimeEditPopupControl popupControl;
        public PanelControl panel;

        public PopupTimeEditForm(PopupTimeEdit ownerEdit)
        {
            InitializeComponent();
            SetLocation(ownerEdit);

            CreatePopupControl(ownerEdit);
            CreatePanel();

            ClientSize = panel.ClientSize;                     
            Controls.Add(panel);
        }

        private void CreatePanel()
        {
            const int bordersWidth = 2;
            panel = new PanelControl();
            panel.ClientSize = new Size(popupControl.Bounds.Width + bordersWidth, popupControl.Bounds.Height + bordersWidth);
            panel.Controls.Add(popupControl); 
        }

        private void CreatePopupControl(PopupTimeEdit ownerEdit)
        {
            popupControl = new TimeEditPopupControl(ownerEdit);
            popupControl.Location = new Point(1, 1);
        }

        ~PopupTimeEditForm()
        {
            popupControl.Dispose();
            panel.Dispose();
            GC.Collect();
        }

        public void SetLocation(PopupTimeEdit ownerEdit)
        {
            Point editorLocation = ownerEdit.PointToScreen(Point.Empty);
            editorLocation.Y += ownerEdit.Size.Height;
            Location = editorLocation ;
        }
    }
}
