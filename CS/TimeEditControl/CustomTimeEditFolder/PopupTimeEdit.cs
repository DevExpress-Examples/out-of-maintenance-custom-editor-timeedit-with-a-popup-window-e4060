using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TimeEditControl
{
    [System.ComponentModel.DesignerCategory("")]
    public class PopupTimeEdit : TimeEdit
    {
        public bool isPopupOpen;
        PopupTimeEditForm popupForm;

        static PopupTimeEdit() { RepositoryItemPopupTimeEdit.Register(); }
        public PopupTimeEdit() 
        {
            MaskBox.MouseClick += CustomTimeEdit_MouseClick;
        }

        ~PopupTimeEdit()
        {
            isPopupOpen = false;
            UnsubscribeEvents();
            if (popupForm!=null)
                popupForm.Dispose();
        }

        public override string EditorTypeName { get { return RepositoryItemPopupTimeEdit.EditorName; } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemPopupTimeEdit Properties
        {
            get { return base.Properties as RepositoryItemPopupTimeEdit; }
        }

        protected override void OnPressButton(DevExpress.XtraEditors.Drawing.EditorButtonObjectInfoArgs buttonInfo)
        {
            if (isPopupOpen)
                ClosePopup();
            else
            {
                if (buttonInfo.Button.Index == 1)                
                        ShowPopup();                
                else
                    base.OnPressButton(buttonInfo);
            }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            if (Parent != null)
                Parent.LocationChanged -= Parent_LocationChanged;
            base.OnParentChanged(e);
            if (Parent != null)
                Parent.LocationChanged += Parent_LocationChanged;
        }

        void Parent_LocationChanged(object sender, EventArgs e)
        {
            if (popupForm!=null)
                popupForm.SetLocation(this);
        }

        public virtual void ShowPopup()
        {
            popupForm = GetPopupForm(this);
            isPopupOpen = true;                        
            popupForm.Show();
            popupForm.Focus();
        }

        protected virtual PopupTimeEditForm GetPopupForm(PopupTimeEdit edit)
        {
            if (popupForm == null)
            {
                popupForm = new PopupTimeEditForm(edit);
            }

            DateTime newTime = ParseCurrentTime(Text);             
            popupForm.popupControl.CurrentTime = newTime;
            return popupForm;            
        }

        private DateTime ParseCurrentTime(string text)
        {
            DateTime result = DateTime.Now;
            try
            {
                result = DateTime.Parse(text);
            }
            catch (Exception) { }
            return result;

        }
        void CustomTimeEdit_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (isPopupOpen)
                ClosePopup();
        }

        protected virtual void ClosePopup()
        {
            isPopupOpen = false;
            popupForm.Hide();
        }        

        private void UnsubscribeEvents()
        {
            if (MaskBox != null)
                MaskBox.MouseClick -= CustomTimeEdit_MouseClick;
        }
    }
}
