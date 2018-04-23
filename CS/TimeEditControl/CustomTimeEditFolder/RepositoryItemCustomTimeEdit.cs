using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TimeEditControl
{
    [System.ComponentModel.DesignerCategory("")]
    [UserRepositoryItem("Register")]
    public class RepositoryItemPopupTimeEdit : RepositoryItemTimeEdit
    {
        internal const string EditorName = "PopupTimeEdit";
        
        static RepositoryItemPopupTimeEdit() { Register(); }
        public RepositoryItemPopupTimeEdit() 
        {
        }

        public override string EditorTypeName { get { return EditorName; } }

        private void AddDropDownButton()
        {
            EditorButton dropDown = new EditorButton();
            dropDown.Caption = "DropDown";
            dropDown.Kind = ButtonPredefines.DropDown;
            dropDown.IsDefaultButton = true;
            Buttons.Add(dropDown);
        }

        public override void CreateDefaultButton()
        {
            base.CreateDefaultButton();
            AddDropDownButton();
        }

        public static void Register() 
        {
            EditorRegistrationInfo.Default.Editors.Add(
                new EditorClassInfo(
                    EditorName, typeof(PopupTimeEdit), typeof(RepositoryItemPopupTimeEdit),
                    typeof(BaseSpinEditViewInfo), new ButtonEditPainter(), true));
        }
    }
}
