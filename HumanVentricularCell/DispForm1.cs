using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HumanVentricularCell
{
    public partial class DispForm1 : HumanVentricularCell.DispFormParent
    {
        MainForm Mf;
        public DispForm1(MainForm f)
        {
            Mf = f;
            InitializeComponent();
        }
        override public void PlotSimDisp(double SimTime, cCell myCell)
        {
            bool BlnFirstPlot_LDisp = Pd.BlnFirstPlot_LDisp;
            //************************************************************************
            ucPicBox1.PlotSimData(SimTime, myCell.TVc[Pd.IdxVm], ref ucPicBox1.myPoint[0], BlnFirstPlot_LDisp);

            //************************************************************************
            ucPicBox2.PlotSimData(SimTime, myCell.TVc[Pd.IdxCai] * 1000, ref ucPicBox2.myPoint[0], BlnFirstPlot_LDisp);

            //************************************************************************
            ucPicBox3.PlotSimData(SimTime, myCell.TVc[Pd.IdxCajs] * 100, ref ucPicBox3.myPoint[0], BlnFirstPlot_LDisp);
            ucPicBox3.PlotSimData(SimTime, myCell.TVc[Pd.IdxCasl] * 1000, ref ucPicBox3.myPoint[1], BlnFirstPlot_LDisp);

            //************************************************************************
            ucPicBox4.PlotSimData(SimTime, myCell.TVc[Pd.IdxhsmL], ref ucPicBox4.myPoint[0], BlnFirstPlot_LDisp);
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
            ////************************************************************************           
            ////ucPicBox1.lbl_Var1.Text = myCell.Vm.ToString("0.0E0");
            //ucPicBox1.lbl_Var1.Text = myCell.TVc[Pd.IdxVm].ToString("0.0E0");

            ////************************************************************************
            ////ucPicBox2.lbl_Var1.Text = myCell.Cyt.Cai.ToString("0.0E0");
            //ucPicBox2.lbl_Var1.Text = myCell.TVc[Pd.IdxCai].ToString("0.0E0");

            ////************************************************************************
            ////ucPicBox3.lbl_Var1.Text = myCell.JS.Cajs.ToString("0.0E0");
            ////ucPicBox3.lbl_Var2.Text = myCell.SL.Casl.ToString("0.0E0");
            //ucPicBox3.lbl_Var1.Text = myCell.TVc[Pd.IdxCajs].ToString("0.0E0");
            //ucPicBox3.lbl_Var2.Text = myCell.TVc[Pd.IdxCasl].ToString("0.0E0");

            ////************************************************************************
            ////ucPicBox4.lbl_Var1.Text = myCell.Contraction.hsmL.ToString("0.0E0");
            //ucPicBox4.lbl_Var1.Text = myCell.TVc[Pd.IdxhsmL].ToString("0.000E0");
        }

        private void DispForm1_Load(object sender, EventArgs e)
        {
            //************************************************************************
            ucPicBox1.lbl_Var1Str.Text = "Vm";
            ucPicBox1.txb_Ymax.Text = "50";
            ucPicBox1.txb_Ymin.Text = "-100";

            //************************************************************************
            ucPicBox2.lbl_Var1Str.Text = "Cai*1000";
            ucPicBox2.txb_Ymax.Text = "0.7";
            ucPicBox2.txb_Ymin.Text = "0";

            //************************************************************************
            ucPicBox3.lbl_Var1Str.Text = "Cajs*100";
            ucPicBox3.lbl_Var2Str.Text = "Casl*1000";
            ucPicBox3.txb_Ymax.Text = "6";
            ucPicBox3.txb_Ymin.Text = "0";

            //************************************************************************
            ucPicBox4.lbl_Var1Str.Text = "hsmL";
            ucPicBox4.txb_Ymax.Text = "1.2";
            ucPicBox4.txb_Ymin.Text = "0.8";

            Top = 0;
            Left = 0;
            Height = 400;
            Width = 350;
        }

        override public void SendImageBmpToPbox()
        {
            base.SendImageBmpToPbox();
        }
    }
}
