using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HumanVentricularCell
{
    public partial class DispFormParent : Form
    {
        public DispFormParent()
        {
            InitializeComponent();
        }

        virtual public void PlotSimDisp(double SimTime, cCell myCell)
        {
        }

        virtual public void DispXmax(double TimeMax)
        {
            ucPicBox1.lbl_Xmax.Text = Convert.ToString(TimeMax);
            ucPicBox2.lbl_Xmax.Text = Convert.ToString(TimeMax);
            ucPicBox3.lbl_Xmax.Text = Convert.ToString(TimeMax);
            ucPicBox4.lbl_Xmax.Text = Convert.ToString(TimeMax);
        }

        virtual public void GetNewSettings()
        {
            ucPicBox1.GetNewSetting();
            ucPicBox2.GetNewSetting();
            ucPicBox3.GetNewSetting();
            ucPicBox4.GetNewSetting();
        }

        virtual public void EraseGraphs()
        {
            ucPicBox1.EraseGraph();
            ucPicBox2.EraseGraph();
            ucPicBox3.EraseGraph();
            ucPicBox4.EraseGraph();
        }

        virtual public void DispLabels(cCell myCell)
        {
        }

        virtual public void SendImageBmpToPbox()
        {
            ucPicBox1.LoadBmpToPbox();
            ucPicBox2.LoadBmpToPbox();
            ucPicBox3.LoadBmpToPbox();
            ucPicBox4.LoadBmpToPbox();
        }

        private void DispFormParent_Load(object sender, EventArgs e)
        {
            ucPicBox1.lbl_Var1Str.Text = "";
            ucPicBox1.lbl_Var2Str.Text = "";
            ucPicBox1.lbl_Var3Str.Text = "";
            ucPicBox1.lbl_Var4Str.Text = "";
        }

        private void DispFormParent_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
        }
    }
}
