using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Skins;

namespace TimeEditControl
{
    public partial class Form1 : Form
    {
        List<string> skinList;

        public Form1()
        {
            InitializeComponent();
            CreateSkinList();

            //lookUpEdit1
            lookUpEdit1.Properties.DataSource = skinList;
            lookUpEdit1.Properties.NullText = "Choose skin";
            lookUpEdit1.EditValueChanged += lookUpEdit1_EditValueChanged;            

            //popupTimeEdit1
            popupTimeEdit1.Properties.LookAndFeel.UseDefaultLookAndFeel = true;
        }

        private void CreateSkinList()
        {
            skinList = new List<string>();
            foreach (SkinContainer skin in SkinManager.Default.Skins)
            {
                skinList.Add(skin.SkinName);
            }
        }

        void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit edit = sender as LookUpEdit;
            defaultLookAndFeel1.LookAndFeel.SkinName = edit.EditValue as string; 
        }

        ~Form1()
        {
            lookUpEdit1.EditValueChanged -= lookUpEdit1_EditValueChanged;
        }
    }
}
