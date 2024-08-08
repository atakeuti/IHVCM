using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HumanVentricularCell
{
    public partial class DispForm6 : HumanVentricularCell.DispFormParent
    {
        MainForm Mf;
        public DispForm6(MainForm f)
        {
            Mf = f;
            InitializeComponent();
        }
        override public void PlotSimDisp(double SimTime, cCell myCell)
        {
            bool BlnFirstPlot_LDisp = Pd.BlnFirstPlot_LDisp;
            //************************************************************************
            ucPicBox1.PlotSimData(SimTime, myCell.TVc[Pd.IdxdPsi], ref ucPicBox1.myPoint[0], BlnFirstPlot_LDisp);

            //************************************************************************
            ucPicBox2.PlotSimData(SimTime, myCell.TVc[Pd.IdxATPmitTotal], ref ucPicBox2.myPoint[0], BlnFirstPlot_LDisp);
            ucPicBox2.PlotSimData(SimTime, myCell.TVc[Pd.InxADPmitTotal], ref ucPicBox2.myPoint[1], BlnFirstPlot_LDisp);
            ucPicBox2.PlotSimData(SimTime, myCell.TVc[Pd.IdxPImit], ref ucPicBox2.myPoint[2], BlnFirstPlot_LDisp);

            //************************************************************************
            ucPicBox3.PlotSimData(SimTime, myCell.TVc[Pd.InxJ_C1], ref ucPicBox3.myPoint[0], BlnFirstPlot_LDisp);
            ucPicBox3.PlotSimData(SimTime, myCell.TVc[Pd.InxJ_C3], ref ucPicBox3.myPoint[1], BlnFirstPlot_LDisp);
            ucPicBox3.PlotSimData(SimTime, myCell.TVc[Pd.InxJ_C4], ref ucPicBox3.myPoint[2], BlnFirstPlot_LDisp);

            //************************************************************************
            ucPicBox4.PlotSimData(SimTime, myCell.TVc[Pd.InxJ_ATPsyn], ref ucPicBox4.myPoint[0], BlnFirstPlot_LDisp);
            ucPicBox4.PlotSimData(SimTime, myCell.TVc[Pd.InxJ_ANT], ref ucPicBox4.myPoint[1], BlnFirstPlot_LDisp);
            ucPicBox4.PlotSimData(SimTime, myCell.TVc[Pd.InxJ_PiC], ref ucPicBox4.myPoint[2], BlnFirstPlot_LDisp);
            ucPicBox4.PlotSimData(SimTime, myCell.TVc[Pd.InxJ_HLeak], ref ucPicBox4.myPoint[3], BlnFirstPlot_LDisp);
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
            //ucPicBox1.lbl_Var1.Text = myCell.TVc[Pd.IdxdPsi].ToString("0.0E0");

            ////************************************************************************
            //ucPicBox2.lbl_Var1.Text = myCell.TVc[Pd.IdxATPmitTotal].ToString("0.0E0");
            //ucPicBox2.lbl_Var2.Text = myCell.TVc[Pd.InxADPmitTotal].ToString("0.0E0");
            //ucPicBox2.lbl_Var3.Text = myCell.TVc[Pd.IdxPImit].ToString("0.0E0");

            ////************************************************************************
            //ucPicBox3.lbl_Var1.Text = myCell.TVc[Pd.InxJ_C1].ToString("0.0E0");
            //ucPicBox3.lbl_Var2.Text = myCell.TVc[Pd.InxJ_C3].ToString("0.0E0");
            //ucPicBox3.lbl_Var3.Text = myCell.TVc[Pd.InxJ_C4].ToString("0.0E0");

            ////************************************************************************
            //ucPicBox4.lbl_Var1.Text = myCell.TVc[Pd.InxJ_ATPsyn].ToString("0.0E0");
            //ucPicBox4.lbl_Var2.Text = myCell.TVc[Pd.InxJ_ANT].ToString("0.0E0");
            //ucPicBox4.lbl_Var3.Text = myCell.TVc[Pd.InxJ_PiC].ToString("0.0E0");
            //ucPicBox4.lbl_Var4.Text = myCell.TVc[Pd.InxJ_HLeak].ToString("0.0E0");
        }
        override public void SendImageBmpToPbox()
        {
            base.SendImageBmpToPbox();
        }
        private void DispForm6_Load(object sender, EventArgs e)
        {
            //************************************************************************
            ucPicBox1.lbl_Var1Str.Text = "dPsi";
            ucPicBox1.txb_Ymax.Text = "0";
            ucPicBox1.txb_Ymin.Text = "-185";

            //************************************************************************
            ucPicBox2.lbl_Var1Str.Text = "ATPm";
            ucPicBox2.lbl_Var2Str.Text = "ADPm";
            ucPicBox2.lbl_Var3Str.Text = "PIm";
            ucPicBox2.txb_Ymax.Text = "5E1";
            ucPicBox2.txb_Ymin.Text = "0";

            //************************************************************************
            ucPicBox3.lbl_Var1Str.Text = "C1";
            ucPicBox3.lbl_Var2Str.Text = "C3";
            ucPicBox3.lbl_Var3Str.Text = "C4";
            ucPicBox3.txb_Ymax.Text = "1E-2";
            ucPicBox3.txb_Ymin.Text = "0";

            //************************************************************************
            ucPicBox4.lbl_Var1Str.Text = "SN";
            ucPicBox4.lbl_Var2Str.Text = "ANT";
            ucPicBox4.lbl_Var3Str.Text = "PiC";
            ucPicBox4.lbl_Var4Str.Text = "HLeak";
            ucPicBox4.txb_Ymax.Text = "1E-2";
            ucPicBox4.txb_Ymin.Text = "-1E-2";

            Top = 0;
            Left = Mf.DispForm1.Width;
            Height = Mf.DispForm1.Height;
            Width = Mf.DispForm1.Width;
        }
    }
}
