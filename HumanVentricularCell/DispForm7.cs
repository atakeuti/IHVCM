using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HumanVentricularCell
{
    public partial class DispForm7 : HumanVentricularCell.DispFormParent
    {
        MainForm Mf;
        public DispForm7(MainForm f)
        {
            Mf = f;
            InitializeComponent();
        }
        override public void PlotSimDisp(double SimTime, cCell myCell)
        {
            bool BlnFirstPlot_LDisp = Pd.BlnFirstPlot_LDisp;
            //************************************************************************
            ucPicBox1.PlotSimData(SimTime, myCell.TVc[Pd.InxpHmit], ref ucPicBox1.myPoint[0], BlnFirstPlot_LDisp);
            ucPicBox1.PlotSimData(SimTime, myCell.TVc[Pd.InxpHcyt], ref ucPicBox1.myPoint[1], BlnFirstPlot_LDisp);

            //************************************************************************
            ucPicBox2.PlotSimData(SimTime, myCell.TVc[Pd.IdxNADH], ref ucPicBox2.myPoint[0], BlnFirstPlot_LDisp);

            //************************************************************************
            ucPicBox3.PlotSimData(SimTime, myCell.TVc[Pd.IdxKmit], ref ucPicBox3.myPoint[0], BlnFirstPlot_LDisp);

            //************************************************************************
            ucPicBox4.PlotSimData(SimTime, myCell.TVc[Pd.IdxNamit] , ref ucPicBox4.myPoint[0], BlnFirstPlot_LDisp);
        }
        override public void DispXmax(double TimeMax)
        {
            base.DispXmax(TimeMax);
        }

        override public void GetNewSettings()
        {
            base.GetNewSettings();
        }

        override public void EraseGraphs()
        {
            base.EraseGraphs();
        }

        override public void DispLabels(cCell myCell)
        {
            //************************************************************************           
            ucPicBox1.lbl_Var1.Text = myCell.TVc[Pd.InxpHmit].ToString("0.000E0");
            ucPicBox1.lbl_Var2.Text = myCell.TVc[Pd.InxpHcyt].ToString("0.000E0");

            //************************************************************************
            ucPicBox2.lbl_Var1.Text = myCell.TVc[Pd.IdxNADH].ToString("0.000E0");

            //************************************************************************
            ucPicBox3.lbl_Var1.Text = myCell.TVc[Pd.IdxKmit].ToString("0.000E0");

            //************************************************************************
            ucPicBox4.lbl_Var1.Text = myCell.TVc[Pd.IdxNamit].ToString("0.000E0");
        }
        override public void SendImageBmpToPbox()
        {
            base.SendImageBmpToPbox();
        }

        private void DispForm7_Load(object sender, EventArgs e)
        {
            //************************************************************************
            ucPicBox1.lbl_Var1Str.Text = "pHmit";
            ucPicBox1.lbl_Var2Str.Text = "pHcyt";
            ucPicBox1.txb_Ymax.Text = "8";
            ucPicBox1.txb_Ymin.Text = "7";

            //************************************************************************
            ucPicBox2.lbl_Var1Str.Text = "NADH";
            ucPicBox2.txb_Ymax.Text = "3.0";
            ucPicBox2.txb_Ymin.Text = "0";

            //************************************************************************
            ucPicBox3.lbl_Var1Str.Text = "Kmit";
            ucPicBox3.txb_Ymax.Text = "150";
            ucPicBox3.txb_Ymin.Text = "100";

            //************************************************************************
            ucPicBox4.lbl_Var1Str.Text = "Namit";
            ucPicBox4.txb_Ymax.Text = "2.5";
            ucPicBox4.txb_Ymin.Text = "0";

            Top = 0;
            Left = Mf.DispForm1.Width;
            Height = Mf.DispForm1.Height;
            Width = Mf.DispForm1.Width;
        }
    }
}
