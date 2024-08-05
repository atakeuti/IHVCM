using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HumanVentricularCell
{
    public partial class DispForm5 : HumanVentricularCell.DispFormParent
    {

        MainForm Mf;

        public DispForm5(MainForm f)
        {
            Mf = f;
            InitializeComponent();
        }

        override public void PlotSimDisp(double SimTime, cCell myCell)
        {
            bool BlnFirstPlot_LDisp = Pd.BlnFirstPlot_LDisp;
            //************************************************************************
            ucPicBox1.PlotSimData(SimTime, myCell.SR.SERCA.J_SERCA, ref ucPicBox1.myPoint[0], BlnFirstPlot_LDisp);
            ucPicBox1.PlotSimData(SimTime, myCell.SR.RyR.J_RyR, ref ucPicBox1.myPoint[1], BlnFirstPlot_LDisp);
            ucPicBox1.PlotSimData(SimTime, myCell.SR.Leak.J_Leak, ref ucPicBox1.myPoint[2], BlnFirstPlot_LDisp);

            //************************************************************************
            ucPicBox2.PlotSimData(SimTime, myCell.Mitochondria.J_CaUni_js, ref ucPicBox2.myPoint[0], BlnFirstPlot_LDisp);
            ucPicBox2.PlotSimData(SimTime, myCell.Mitochondria.J_CaUni_cyt, ref ucPicBox2.myPoint[1], BlnFirstPlot_LDisp);

            //************************************************************************
            ucPicBox3.PlotSimData(SimTime, myCell.Mitochondria.J_NmSC, ref ucPicBox3.myPoint[0], BlnFirstPlot_LDisp);
            ucPicBox3.PlotSimData(SimTime, myCell.Mitochondria.J_NCXmit, ref ucPicBox3.myPoint[1], BlnFirstPlot_LDisp);

            ////************************************************************************
            //ucPicBox4.PlotSimData(SimTime, myCell.Mitochondria.dPsi, ref ucPicBox4.myPoint[0], BlnFirstPlot_LDisp);
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
            ucPicBox1.lbl_Var1.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxJ_SERCA]);
            ucPicBox1.lbl_Var2.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxJ_RyR]);
            ucPicBox1.lbl_Var3.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxJ_Leak]);

            //************************************************************************
            ucPicBox2.lbl_Var1.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxJ_CaUni_js]);
            ucPicBox2.lbl_Var2.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxJ_CaUni_cyt]);


            //************************************************************************
            ucPicBox3.lbl_Var1.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxJ_NmSC]);
            ucPicBox3.lbl_Var2.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxJ_NCXmit]);

            //************************************************************************
            //ucPicBox4.lbl_Var1.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.IdxdPsi]);
        }

        override public void SendImageBmpToPbox()
        {
            base.SendImageBmpToPbox();
        }

        private void DispForm5_Load(object sender, EventArgs e)
        {
            //************************************************************************
            ucPicBox1.lbl_Var1Str.Text = "JSERCA";
            ucPicBox1.lbl_Var2Str.Text = "JRyR";
            ucPicBox1.lbl_Var3Str.Text = "JLeak";
            ucPicBox1.txb_Ymax.Text = "0.05";
            ucPicBox1.txb_Ymin.Text = "-0.01";

            //************************************************************************
            ucPicBox2.lbl_Var1Str.Text = "JCaUni_js";
            ucPicBox2.lbl_Var2Str.Text = "JCaUni_cyt";
            ucPicBox2.txb_Ymax.Text = "0.01";
            ucPicBox2.txb_Ymin.Text = "-0.0001";

            //************************************************************************
            ucPicBox3.lbl_Var1Str.Text = "JNmSC";
            ucPicBox3.lbl_Var2Str.Text = "JNCXmit";
            ucPicBox3.txb_Ymax.Text = "0.0001";
            ucPicBox3.txb_Ymin.Text = "-0.001";

            //************************************************************************
            //ucPicBox4.lbl_Var1Str.Text = "dPsi";
            //ucPicBox4.txb_Ymax.Text = "0";
            //ucPicBox4.txb_Ymin.Text = "-185";

            Top = 0;
            Left = Mf.DispForm1.Width;
            Height = Mf.DispForm1.Height;
            Width = Mf.DispForm1.Width;
        }
    }
}
