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
    public partial class DispForm3 :HumanVentricularCell. DispFormParent
    {
        MainForm Mf;
        public DispForm3(MainForm f)
        {
            Mf = f; 
            InitializeComponent();
        }

        override public void PlotSimDisp(double SimTime, cCell myCell)
        {
            bool BlnFirstPlot_LDisp = Pd.BlnFirstPlot_LDisp;
            //************************************************************************
            ucPicBox1.PlotSimData(SimTime, myCell.INaCajs.Itc, ref ucPicBox1.myPoint[0], BlnFirstPlot_LDisp);
            ucPicBox1.PlotSimData(SimTime, myCell.INaKjs.Itc, ref ucPicBox1.myPoint[1], BlnFirstPlot_LDisp);
            ucPicBox1.PlotSimData(SimTime, myCell.IK1js.Itc, ref ucPicBox1.myPoint[2], BlnFirstPlot_LDisp);

            //************************************************************************

            ucPicBox2.PlotSimData(SimTime, myCell.Itosjs.Itc, ref ucPicBox2.myPoint[0], BlnFirstPlot_LDisp);
            ucPicBox2.PlotSimData(SimTime, myCell.Itofjs.Itc, ref ucPicBox2.myPoint[1], BlnFirstPlot_LDisp);
            ucPicBox2.PlotSimData(SimTime, myCell.IClCajs.Itc, ref ucPicBox2.myPoint[2], BlnFirstPlot_LDisp);

            //************************************************************************
            ucPicBox3.PlotSimData(SimTime, myCell.INajs.Itc, ref ucPicBox3.myPoint[0], BlnFirstPlot_LDisp);
            ucPicBox3.PlotSimData(SimTime, myCell.IKpjs.Itc, ref ucPicBox3.myPoint[1], BlnFirstPlot_LDisp);
            ucPicBox3.PlotSimData(SimTime, myCell.IpCajs.Itc, ref ucPicBox3.myPoint[2], BlnFirstPlot_LDisp);
            ucPicBox3.PlotSimData(SimTime, myCell.ICabjs.Itc, ref ucPicBox3.myPoint[3], BlnFirstPlot_LDisp);

            //************************************************************************
            ucPicBox4.PlotSimData(SimTime, myCell.IKrjs.Itc, ref ucPicBox4.myPoint[0], BlnFirstPlot_LDisp);
            ucPicBox4.PlotSimData(SimTime, myCell.IKsjs.Itc, ref ucPicBox4.myPoint[1], BlnFirstPlot_LDisp);
            ucPicBox4.PlotSimData(SimTime, myCell.ICaLjs.Itc, ref ucPicBox4.myPoint[2], BlnFirstPlot_LDisp);

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
            //ucPicBox1.lbl_Var1.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxINaCajs_Itc]);
            //ucPicBox1.lbl_Var2.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxINaKjs_Itc]);
            //ucPicBox1.lbl_Var3.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxIK1js_Itc]);

            ////************************************************************************
            //ucPicBox2.lbl_Var1.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxItosjs_Itc]);
            //ucPicBox2.lbl_Var2.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxItofjs_Itc]);
            //ucPicBox2.lbl_Var3.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxIClCajs_Itc]);

            ////************************************************************************
            //ucPicBox3.lbl_Var1.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxINajs_Itc]);
            //ucPicBox3.lbl_Var2.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxIKpjs_Itc]);
            //ucPicBox3.lbl_Var3.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxIpCajs_Itc]);
            //ucPicBox3.lbl_Var4.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxICabjs_Itc]);

            ////************************************************************************
            //ucPicBox3.lbl_Var1.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxIKrjs_Itc]);
            //ucPicBox3.lbl_Var2.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxIKsjs_Itc]);
            //ucPicBox3.lbl_Var3.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxICaLjs_Itc]);

        }

        override public void SendImageBmpToPbox()
        {
            base.SendImageBmpToPbox();
        }

        private void DispForm3_Load(object sender, EventArgs e)
        {
            //************************************************************************
            ucPicBox1.lbl_Var1Str.Text = "INaCajs";
            ucPicBox1.lbl_Var2Str.Text = "INaKjs";
            ucPicBox1.lbl_Var3Str.Text = "IK1js";
            ucPicBox1.txb_Ymax.Text = "0.2";
            ucPicBox1.txb_Ymin.Text = "-0.2";

            //************************************************************************
            ucPicBox2.lbl_Var1Str.Text = "Itosjs";
            ucPicBox2.lbl_Var2Str.Text = "Itofjs";
            ucPicBox2.lbl_Var3Str.Text = "IClCajs";
            ucPicBox2.txb_Ymax.Text = "1.0";
            ucPicBox2.txb_Ymin.Text = "0.0";

            //************************************************************************
            ucPicBox3.lbl_Var1Str.Text = "INajs";
            ucPicBox3.lbl_Var2Str.Text = "IKpjs";
            ucPicBox3.lbl_Var3Str.Text = "IpCajs";
            ucPicBox3.lbl_Var4Str.Text = "ICabjs";
            ucPicBox3.txb_Ymax.Text = "0.03";
            ucPicBox3.txb_Ymin.Text = "-0.03";

            //************************************************************************
            ucPicBox4.lbl_Var1Str.Text = "IKrjs";
            ucPicBox4.lbl_Var2Str.Text = "IKsjs";
            ucPicBox4.lbl_Var3Str.Text = "ICaLjs";
            ucPicBox4.txb_Ymax.Text = "1.0";
            ucPicBox4.txb_Ymin.Text = "-5.0";

            Top = 0;
            Left = Mf.DispForm1.Width;
            Height = Mf.DispForm1.Height;
            Width = Mf.DispForm1.Width;
        }
    }
}
