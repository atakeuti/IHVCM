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
    public partial class DispForm2 : HumanVentricularCell.DispFormParent
    {
        MainForm Mf;
        public DispForm2(MainForm f)
        {
            Mf = f;
            InitializeComponent();
        }

        override public void PlotSimDisp(double SimTime, cCell myCell)
        {
            bool BlnFirstPlot_LDisp = Pd.BlnFirstPlot_LDisp;
            //************************************************************************
            ucPicBox1.PlotSimData(SimTime, myCell.TVc[Pd.IdxCasr], ref ucPicBox1.myPoint[0], BlnFirstPlot_LDisp);

            //************************************************************************
            ucPicBox2.PlotSimData(SimTime, myCell.TVc[Pd.InxCamit] * 1000, ref ucPicBox2.myPoint[0], BlnFirstPlot_LDisp);

            ucPicBox3.PlotSimData(SimTime, myCell.TVc[Pd.IdxNai], ref ucPicBox3.myPoint[0], BlnFirstPlot_LDisp);
           // ucPicBox3.PlotSimData(SimTime, myCell.TVc[Pd.IdxNajs], ref ucPicBox3.myPoint[1], BlnFirstPlot_LDisp);
           // ucPicBox3.PlotSimData(SimTime, myCell.TVc[Pd.IdxNasl], ref ucPicBox3.myPoint[2], BlnFirstPlot_LDisp);

            //************************************************************************
            ucPicBox4.PlotSimData(SimTime, myCell.TVc[Pd.InxFb], ref ucPicBox4.myPoint[0], BlnFirstPlot_LDisp);
            //ucPicBox4.PlotSimData(SimTime, myCell.TVc[Pd.InxTwitch], ref ucPicBox4.myPoint[1], BlnFirstPlot_LDisp);
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
            ////ucPicBox1.lbl_Var1.Text = myCell.SR.Casr.ToString("0.0E0");
            //ucPicBox1.lbl_Var1.Text = myCell.TVc[Pd.IdxCasr].ToString("0.0E0");

            ////************************************************************************
            ////ucPicBox2.lbl_Var1.Text = myCell.Mitochondria.Camit.ToString("0.0E0");
            //ucPicBox2.lbl_Var1.Text = myCell.TVc[Pd.InxCamit].ToString("0.0E0");

            ////************************************************************************
            ////ucPicBox3.lbl_Var1.Text = myCell.Cyt.Nai.ToString("0.0E0");
            ////ucPicBox3.lbl_Var2.Text = myCell.JS.Najs.ToString("0.0E0");
            ////ucPicBox3.lbl_Var3.Text = myCell.SL.Nasl.ToString("0.0E0");
            //ucPicBox3.lbl_Var1.Text = myCell.TVc[Pd.IdxNai].ToString("0.0E0");
            ////ucPicBox3.lbl_Var2.Text = myCell.TVc[Pd.IdxNajs].ToString("0.0E0");
            ////ucPicBox3.lbl_Var3.Text = myCell.TVc[Pd.IdxNasl].ToString("0.0E0");

            ////************************************************************************
            ////ucPicBox4.lbl_Var1.Text = myCell.Contraction.Fb.ToString("0.0E0");
            ////ucPicBox4.lbl_Var2.Text = myCell.Contraction.Twitch.ToString("0.0E0");
            //ucPicBox4.lbl_Var1.Text = myCell.TVc[Pd.InxFb].ToString("0.0E0");
            //ucPicBox4.lbl_Var2.Text = myCell.TVc[Pd.InxTwitch].ToString("0.0E0");
        }



        override public void SendImageBmpToPbox()
        {
            base.SendImageBmpToPbox();
        }

        private void DispForm2_Load(object sender, EventArgs e)
        {
            //************************************************************************
            ucPicBox1.lbl_Var1Str.Text = "Casr";
            ucPicBox1.txb_Ymax.Text = "1.0";
            ucPicBox1.txb_Ymin.Text = "0.0";

            //************************************************************************
            ucPicBox2.lbl_Var1Str.Text = "Camit(uM)";
            ucPicBox2.txb_Ymax.Text = "1";
            ucPicBox2.txb_Ymin.Text = "0";

            //************************************************************************
            ucPicBox3.lbl_Var1Str.Text = "Nai";
            //ucPicBox3.lbl_Var2Str.Text = "Najs";
            //ucPicBox3.lbl_Var3Str.Text = "Nasl";
            ucPicBox3.txb_Ymax.Text = "9.0";
            ucPicBox3.txb_Ymin.Text = "8.0";

            //************************************************************************
            ucPicBox4.lbl_Var1Str.Text = "Fb";
            ucPicBox4.lbl_Var2Str.Text = "Twitch";
            ucPicBox4.txb_Ymax.Text = "15";
            ucPicBox4.txb_Ymin.Text = "0";

            Top = 0;
            Left = Mf.DispForm1.Width;
            Height = Mf.DispForm1.Height;
            Width = Mf.DispForm1.Width;
        }
    }
}
