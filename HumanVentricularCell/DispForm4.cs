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
    public partial class DispForm4 : HumanVentricularCell.DispFormParent
    {
        MainForm Mf;
        public DispForm4(MainForm f)
        {
            Mf = f; 
            InitializeComponent();
        }

        override public void PlotSimDisp(double SimTime, cCell myCell)
        {
            bool BlnFirstPlot_LDisp = Pd.BlnFirstPlot_LDisp;
            //************************************************************************
            ucPicBox1.PlotSimData(SimTime, myCell.INaCasl.Itc, ref ucPicBox1.myPoint[0], BlnFirstPlot_LDisp);
            ucPicBox1.PlotSimData(SimTime, myCell.INaKsl.Itc, ref ucPicBox1.myPoint[1], BlnFirstPlot_LDisp);
            ucPicBox1.PlotSimData(SimTime, myCell.IK1sl.Itc, ref ucPicBox1.myPoint[2], BlnFirstPlot_LDisp);

            //************************************************************************

            ucPicBox2.PlotSimData(SimTime, myCell.Itossl.Itc, ref ucPicBox2.myPoint[0], BlnFirstPlot_LDisp);
            ucPicBox2.PlotSimData(SimTime, myCell.Itofsl.Itc, ref ucPicBox2.myPoint[1], BlnFirstPlot_LDisp);
            ucPicBox2.PlotSimData(SimTime, myCell.IClCasl.Itc, ref ucPicBox2.myPoint[2], BlnFirstPlot_LDisp);

            //************************************************************************
            ucPicBox3.PlotSimData(SimTime, myCell.INasl.Itc, ref ucPicBox3.myPoint[0], BlnFirstPlot_LDisp);
            ucPicBox3.PlotSimData(SimTime, myCell.IKpsl.Itc, ref ucPicBox3.myPoint[1], BlnFirstPlot_LDisp);
            ucPicBox3.PlotSimData(SimTime, myCell.IpCasl.Itc, ref ucPicBox3.myPoint[2], BlnFirstPlot_LDisp);
            ucPicBox3.PlotSimData(SimTime, myCell.ICabsl.Itc, ref ucPicBox3.myPoint[3], BlnFirstPlot_LDisp);

            //************************************************************************
            ucPicBox4.PlotSimData(SimTime, myCell.IKrsl.Itc, ref ucPicBox4.myPoint[0], BlnFirstPlot_LDisp);
            ucPicBox4.PlotSimData(SimTime, myCell.IKssl.Itc, ref ucPicBox4.myPoint[1], BlnFirstPlot_LDisp);
            ucPicBox4.PlotSimData(SimTime, myCell.ICaLsl.Itc, ref ucPicBox4.myPoint[2], BlnFirstPlot_LDisp);
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
            ucPicBox1.lbl_Var1.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxINaCasl_Itc]);
            ucPicBox1.lbl_Var2.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxINaKsl_Itc]);
            ucPicBox1.lbl_Var3.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxIK1sl_Itc]);

            //************************************************************************
            ucPicBox2.lbl_Var1.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxItossl_Itc]);
            ucPicBox2.lbl_Var2.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxItofsl_Itc]);
            ucPicBox2.lbl_Var3.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxIClCasl_Itc]);

            //************************************************************************
            ucPicBox3.lbl_Var1.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxINasl_Itc]);
            ucPicBox3.lbl_Var2.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxIKpsl_Itc]);
            ucPicBox3.lbl_Var3.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxIpCasl_Itc]);
            ucPicBox3.lbl_Var4.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxICabsl_Itc]);

            //************************************************************************
            ucPicBox3.lbl_Var1.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxIKrsl_Itc]);
            ucPicBox3.lbl_Var2.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxIKssl_Itc]);
            ucPicBox3.lbl_Var3.Text = String.Format("{0,6:#.#0}", myCell.TVc[Pd.InxICaLsl_Itc]);

        }

        override public void SendImageBmpToPbox()
        {
            base.SendImageBmpToPbox();
        }

        private void DispForm4_Load(object sender, EventArgs e)
        {
            //************************************************************************
            ucPicBox1.lbl_Var1Str.Text = "INaCasl";
            ucPicBox1.lbl_Var2Str.Text = "INaKsl";
            ucPicBox1.lbl_Var3Str.Text = "IK1sl";
            ucPicBox1.txb_Ymax.Text = "1.5";
            ucPicBox1.txb_Ymin.Text = "-1.5";

            //************************************************************************
            ucPicBox2.lbl_Var1Str.Text = "Itossl";
            ucPicBox2.lbl_Var2Str.Text = "Itofsl";
            ucPicBox2.lbl_Var3Str.Text = "IClCasl";
            ucPicBox2.txb_Ymax.Text = "8.0";
            ucPicBox2.txb_Ymin.Text = "0.0";

            //************************************************************************
            ucPicBox3.lbl_Var1Str.Text = "INasl";
            ucPicBox3.lbl_Var2Str.Text = "IKpsl";
            ucPicBox3.lbl_Var3Str.Text = "IpCasl";
            ucPicBox3.lbl_Var4Str.Text = "ICabsl";
            ucPicBox3.txb_Ymax.Text = "0.2";
            ucPicBox3.txb_Ymin.Text = "-0.2";

            //************************************************************************
            ucPicBox4.lbl_Var1Str.Text = "IKrsl";
            ucPicBox4.lbl_Var2Str.Text = "IKssl";
            ucPicBox4.lbl_Var3Str.Text = "ICaLsl";
            ucPicBox4.txb_Ymax.Text = "0.2";
            ucPicBox4.txb_Ymin.Text = "-1.0";

            Top = 0;
            Left = Mf.DispForm1.Width;
            Height = Mf.DispForm1.Height;
            Width = Mf.DispForm1.Width;
        }
    }
}
