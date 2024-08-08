using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HumanVentricularCell
{
    public partial class DispForm8 : HumanVentricularCell.DispFormParent
    {
        MainForm Mf;
        public DispForm8(MainForm f)
        {
            Mf = f;
            InitializeComponent();
        }
        override public void PlotSimDisp(double SimTime, cCell myCell)
        {
            bool BlnFirstPlot_LDisp = Pd.BlnFirstPlot_LDisp;
            //************************************************************************
            ucPicBox1.PlotSimData(SimTime, myCell.TVc[Pd.IdxATPiTotal], ref ucPicBox1.myPoint[0], BlnFirstPlot_LDisp);
            ucPicBox1.PlotSimData(SimTime, myCell.TVc[Pd.IdxPCr], ref ucPicBox1.myPoint[1], BlnFirstPlot_LDisp);
            ucPicBox1.PlotSimData(SimTime, myCell.TVc[Pd.InxCr], ref ucPicBox1.myPoint[2], BlnFirstPlot_LDisp);
            ucPicBox1.PlotSimData(SimTime, myCell.TVc[Pd.IdxPIi], ref ucPicBox1.myPoint[3], BlnFirstPlot_LDisp);

            //************************************************************************
            ucPicBox2.PlotSimData(SimTime, myCell.TVc[Pd.InxJ_KUni], ref ucPicBox2.myPoint[0], BlnFirstPlot_LDisp);
            ucPicBox2.PlotSimData(SimTime, myCell.TVc[Pd.InxJ_KHE], ref ucPicBox2.myPoint[1], BlnFirstPlot_LDisp);

            //************************************************************************
            //ucPicBox3.PlotSimData(SimTime, myCell.TVc[Pd.InxJNmSC], ref ucPicBox3.myPoint[0], BlnFirstPlot_LDisp);
            //ucPicBox3.PlotSimData(SimTime, myCell.TVc[Pd.InxJNCXmit], ref ucPicBox3.myPoint[1], BlnFirstPlot_LDisp);
            //ucPicBox3.PlotSimData(SimTime, myCell.TVc[Pd.InxJCaUni_js], ref ucPicBox3.myPoint[2], BlnFirstPlot_LDisp);
            //ucPicBox3.PlotSimData(SimTime, myCell.TVc[Pd.InxJCaUni_cyt], ref ucPicBox3.myPoint[3], BlnFirstPlot_LDisp);

            //************************************************************************
            ucPicBox4.PlotSimData(SimTime, myCell.TVc[Pd.InxJ_PDHC], ref ucPicBox4.myPoint[0], BlnFirstPlot_LDisp);
            ucPicBox4.PlotSimData(SimTime, myCell.TVc[Pd.InxJ_ICDH], ref ucPicBox4.myPoint[1], BlnFirstPlot_LDisp);
            ucPicBox4.PlotSimData(SimTime, myCell.TVc[Pd.InxJ_OGDH], ref ucPicBox4.myPoint[2], BlnFirstPlot_LDisp);
            ucPicBox4.PlotSimData(SimTime, myCell.TVc[Pd.InxJ_MDH], ref ucPicBox4.myPoint[3], BlnFirstPlot_LDisp);
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
            //ucPicBox1.lbl_Var1.Text = myCell.TVc[Pd.IdxATPiTotal].ToString("0.000E0");
            //ucPicBox1.lbl_Var2.Text = myCell.TVc[Pd.IdxPCr].ToString("0.000E0");
            //ucPicBox1.lbl_Var3.Text = myCell.TVc[Pd.InxCr].ToString("0.000E0");
            //ucPicBox1.lbl_Var4.Text = myCell.TVc[Pd.IdxPIi].ToString("0.000E0");

            ////************************************************************************
            //ucPicBox2.lbl_Var1.Text = myCell.TVc[Pd.InxJ_KUni].ToString("0.0E0");
            //ucPicBox2.lbl_Var2.Text = myCell.TVc[Pd.InxJ_KHE].ToString("0.0E0");

            ////************************************************************************
            ////ucPicBox3.lbl_Var1.Text = myCell.TVc[Pd.InxJNmSC].ToString("0.0E0");
            ////ucPicBox3.lbl_Var2.Text = myCell.TVc[Pd.InxJNCXmit].ToString("0.0E0");
            ////ucPicBox3.lbl_Var3.Text = myCell.TVc[Pd.InxJCaUni_js].ToString("0.0E0");
            ////ucPicBox3.lbl_Var4.Text = myCell.TVc[Pd.InxJCaUni_cyt].ToString("0.0E0");

            ////************************************************************************
            //ucPicBox4.lbl_Var1.Text = myCell.TVc[Pd.InxJ_PDHC].ToString("0.0E0");
            //ucPicBox4.lbl_Var2.Text = myCell.TVc[Pd.InxJ_ICDH].ToString("0.0E0");
            //ucPicBox4.lbl_Var3.Text = myCell.TVc[Pd.InxJ_OGDH].ToString("0.0E0");
            //ucPicBox4.lbl_Var4.Text = myCell.TVc[Pd.InxJ_MDH].ToString("0.0E0");
        }
        override public void SendImageBmpToPbox()
        {
            base.SendImageBmpToPbox();
        }
        private void DispForm8_Load(object sender, EventArgs e)
        {
            //************************************************************************
            ucPicBox1.lbl_Var1Str.Text = "ATPi";
            ucPicBox1.lbl_Var2Str.Text = "PCr";
            ucPicBox1.lbl_Var3Str.Text = "Cr";
            ucPicBox1.lbl_Var4Str.Text = "PIi";
            ucPicBox1.txb_Ymax.Text = "20";
            ucPicBox1.txb_Ymin.Text = "0";

            //************************************************************************
            ucPicBox2.lbl_Var1Str.Text = "KUni";
            ucPicBox2.lbl_Var2Str.Text = "KHE";
            ucPicBox2.txb_Ymax.Text = "1E-3";
            ucPicBox2.txb_Ymin.Text = "-1E-3";

            //************************************************************************
            //ucPicBox3.lbl_Var1Str.Text = "JNmSC";
            //ucPicBox3.lbl_Var2Str.Text = "JNCXmit";
            //ucPicBox3.lbl_Var3Str.Text = "JCaUni_js";
            //ucPicBox3.lbl_Var4Str.Text = "JCaUni_cyt";
            //ucPicBox3.txb_Ymax.Text = "1E-2";
            //ucPicBox3.txb_Ymin.Text = "-1E-2";

            //************************************************************************
            ucPicBox4.lbl_Var1Str.Text = "PDHC";
            ucPicBox4.lbl_Var2Str.Text = "ICDH";
            ucPicBox4.lbl_Var3Str.Text = "OGDH";
            ucPicBox4.lbl_Var4Str.Text = "MDH";
            ucPicBox4.txb_Ymax.Text = "2E-4";
            ucPicBox4.txb_Ymin.Text = "0";

            Top = 0;
            Left = Mf.DispForm1.Width;
            Height = Mf.DispForm1.Height;
            Width = Mf.DispForm1.Width;
        }
    }
}
