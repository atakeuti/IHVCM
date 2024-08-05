using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace HumanVentricularCell
{
    public class cContraction : cElement
    {
        private Pd.TIdx IxTrn_total;
        private Pd.TIdx Ixa_cm;
        private Pd.TIdx Ixb_cm;
        private Pd.TIdx Ixf_cm;
        private Pd.TIdx Ixg_cm;

        private Pd.TIdx IxB_eff;
        private Pd.TIdx Ixh_c;
        private Pd.TIdx IxA_Fb;
        private Pd.TIdx IxK_PE;
        private Pd.TIdx IxK_PL;
        private Pd.TIdx IxD;
        private Pd.TIdx IxL0;
        private Pd.TIdx IxM_hsmL;

        private Pd.TIdx IxFb;
        private Pd.TIdx IxTwitch;
        private Pd.TIdx IxF_ext;

        private Pd.TIdx IxJ_Ca_contraction;
        private Pd.TIdx IxdATPuse_contraction;
        //////private Pd.TIdx IxYd;

        private Pd.TIdx IxTrnCa;
        private Pd.TIdx IxTrnCa_cb;
        private Pd.TIdx IxTrn_cb;
        private Pd.TIdx IxhsmX;
        private Pd.TIdx IxhsmL;
        private Pd.TIdx IxV_hsmL;

        private Pd.TIdx IxFb_Min;
        private Pd.TIdx IxFb_Max;
        private Pd.TIdx IxhsmL_Min;
        private Pd.TIdx IxhsmL_Max;

        public double Trn_total = 0.05;   //Total amount of Troponin [mM]
        public double a_cm = (1 / 2.5) * 32.0;        // =12.8, Rate Constant of Y1   [/mM /ms]

        public double b_cm = 0.054;        //Rate Constant of Z1   [/ms]
        public double f_cm = 0.0000851;        //Rate Constant of Y2   [/ms]
        public double g_cm = 0.000649;        //Rate Constant of Z2   [/ms]
        public double B_eff = -0.001887;       //Rate Constant of dXdt [um /ms] // originally -1.887 by Shim et al., 2007, but shoudl be 1/1000
        public double h_c = 0.005;      //equilibrium length of the cross bridge [um]

        public double A_Fb = 380000.0;        //coefficient of Fb  [mN /mm^2 /um /mM troponin concentration] 

        public double K_PE = 3.0;        //Rate Constant of Fp [mN /mm^2]
        public double K_PL = 30.0;        //Rate Constant of Fp [mN /mm^2]
        public double D = 10.0;           //Coefficient of Fp   [no dimension]

        public double L0 = 0.965;          //const [um]

        public double M_hsmL = 10; // [mN * ms^2 / mm^2 / um]

        public double Fb;
        public double Twitch;
        public double F_ext;

        public double J_Ca_contraction;
        public double dATPuse_contraction;
        public double Yd;

        public double TrnCa;
        public double TrnCa_cb;
        public double Trn_cb;
        public double hsmX;
        public double hsmL;
        public double V_hsmL;

        public double Externalload_;
        public double EL, EL0, ELinf, EL02;

        public double Fb_Min;
        public double Fb_Max;
        public double hsmL_Min;
        public double hsmL_Max;

        public cContraction()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxTrn_total, ref i, Pd.IntPar, "Trn_total");
            SetIx(ref Ixa_cm, ref i, Pd.IntPar, "a_cm");
            SetIx(ref Ixb_cm, ref i, Pd.IntPar, "b_cm");
            SetIx(ref Ixf_cm, ref i, Pd.IntPar, "f_cm");
            SetIx(ref Ixg_cm, ref i, Pd.IntPar, "g_cm");

            SetIx(ref IxB_eff, ref i, Pd.IntPar, "B_eff");
            SetIx(ref Ixh_c, ref i, Pd.IntPar, "h_c");
            SetIx(ref IxA_Fb, ref i, Pd.IntPar, "A_Fb");
            SetIx(ref IxK_PE, ref i, Pd.IntPar, "K_PE");
            SetIx(ref IxK_PL, ref i, Pd.IntPar, "K_PL");
            SetIx(ref IxD, ref i, Pd.IntPar, "D");
            SetIx(ref IxL0, ref i, Pd.IntPar, "L0");
            SetIx(ref IxM_hsmL, ref i, Pd.IntPar, "M_hsmL");

            SetIx(ref IxFb, ref i, Pd.IntVar, "Fb");
            SetIx(ref IxTwitch, ref i, Pd.IntVar, "Twitch");
            SetIx(ref IxF_ext, ref i, Pd.IntVar, "F_ext");

            SetIx(ref IxJ_Ca_contraction, ref i, Pd.IntVar, "J_Ca_contraction");
            SetIx(ref IxdATPuse_contraction, ref i, Pd.IntVar, "dATPuse_contraction");

            SetIx(ref IxTrnCa, ref i, Pd.IntVar, "TrnCa");
            SetIx(ref IxTrnCa_cb, ref i, Pd.IntVar, "TrnCa_cb");
            SetIx(ref IxTrn_cb, ref i, Pd.IntVar, "Trn_cb");
            SetIx(ref IxhsmX, ref i, Pd.IntVar, "hsmX");
            SetIx(ref IxhsmL, ref i, Pd.IntVar, "hsmL");
            SetIx(ref IxV_hsmL, ref i, Pd.IntVar, "V_hsmL");

            // 24Apr09
            SetIx(ref IxFb_Min, ref i, Pd.IntVar, "FbMin");
            SetIx(ref IxFb_Max, ref i, Pd.IntVar, "FbMax");

            // 24Apr11
            SetIx(ref IxhsmL_Min, ref i, Pd.IntVar, "hsmLMin");
            SetIx(ref IxhsmL_Max, ref i, Pd.IntVar, "hsmLMax");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpContraction_ListView, NumOfIx);

            Fb = myTVc[Pd.InxFb];
            Twitch = myTVc[Pd.InxTwitch];
            F_ext = myTVc[Pd.InxF_ext];

            J_Ca_contraction = myTVc[Pd.InxJ_Ca_contraction];
            dATPuse_contraction = myTVc[Pd.InxdATPuse_contraction];

            TrnCa = myTVc[Pd.IdxTrnCa];
            TrnCa_cb = myTVc[Pd.IdxTrnCa_cb];
            Trn_cb = myTVc[Pd.IdxTrn_cb];
            hsmX = myTVc[Pd.IdxhsmX];
            hsmL = myTVc[Pd.IdxhsmL];
            V_hsmL = myTVc[Pd.IdxV_hsmL];

            // EL, EL0, ELinf, EL02 are initialized at MainForm
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Cai = tvY[Pd.IdxCai];
            double Trn = Trn_total - tvY[Pd.IdxTrnCa] - tvY[Pd.IdxTrnCa_cb] - tvY[Pd.IdxTrn_cb];         //var Concentration of free troponin (Trn)           [mM]

            double S_func = 1 / (1 + Math.Exp((tvY[Pd.IdxhsmL] - 0.85) / (-0.08))) / (1 + Math.Exp((tvY[Pd.IdxhsmL] - 1.4) / 0.06));  // Coefficient of f_23 & f_3 [no dimension]
 
            double Fp;

            Fb = A_Fb * S_func * (tvY[Pd.IdxTrnCa_cb] + tvY[Pd.IdxTrn_cb]) * (tvY[Pd.IdxhsmL] - tvY[Pd.IdxhsmX]);

            if (tvY[Pd.IdxhsmL] >= L0)
            {
                Fp = K_PE * (Math.Exp(D * (tvY[Pd.IdxhsmL] / L0 - 1)) - 1);
            }
            else
            {
                Fp = -K_PL * (1 - (tvY[Pd.IdxhsmL] / L0));
            }
            double f_23 = S_func * (tvY[Pd.IdxTrnCa] + tvY[Pd.IdxTrnCa_cb] + tvY[Pd.IdxTrn_cb]) / Trn_total;
            double f_3 = S_func * (tvY[Pd.IdxTrnCa_cb] + tvY[Pd.IdxTrn_cb]) / Trn_total;

            double Y1 = a_cm * Math.Pow(Cai, 0.81);   

            double Z1 = b_cm * (1 + f_23 * (Math.Exp(-2.09) - 1)) * (1 + f_23 * (Math.Exp(-2.09) - 1)) * (1 + f_3 * (Math.Exp(0.73) - 1)) * (1 + f_3 * (Math.Exp(0.73) - 1));
            double Y2 = f_cm * (1 + f_23 * (Math.Exp(2.96) - 1)) * (1 + f_23 * (Math.Exp(2.96) - 1)) * (1 + f_3 * (Math.Exp(-2.1) - 1)) * (1 + f_3 * (Math.Exp(-2.1) - 1));
            double Z2 = g_cm * (1 + f_3 * (Math.Exp(-0.26) - 1)) * (1 + f_3 * (Math.Exp(-0.26) - 1));
            double Y3 = Z1;
            double Z3 = 40 * Y1;
            double Y4 = 0.24;

            Twitch = Fb + Fp;

            tvDYdt[Pd.IdxhsmX] = B_eff * (Math.Exp((h_c - tvY[Pd.IdxhsmL] + tvY[Pd.IdxhsmX]) / 0.00225) - 1);

            if (tvDYdt[Pd.IdxhsmX] > 0)
            {
                Yd = 180.0 * tvDYdt[Pd.IdxhsmX] * tvDYdt[Pd.IdxhsmX];
            }
            else
            {
                Yd = 9000.0 * tvDYdt[Pd.IdxhsmX] * tvDYdt[Pd.IdxhsmX];
            }

            J_Ca_contraction = Z1 * tvY[Pd.IdxTrnCa] - Y1 * Trn + Yd * tvY[Pd.IdxTrnCa_cb] + Y3 * tvY[Pd.IdxTrnCa_cb] - Z3 * tvY[Pd.IdxTrn_cb];
            myCell.TVc[Pd.InxJ_Ca_contraction] = J_Ca_contraction;

            tvDYdt[Pd.IdxTrnCa] = Y1 * Trn + Z2 * tvY[Pd.IdxTrnCa_cb] - (Z1 + Y2) * tvY[Pd.IdxTrnCa];
            tvDYdt[Pd.IdxTrnCa_cb] = Y2 * tvY[Pd.IdxTrnCa] + Z3 * tvY[Pd.IdxTrn_cb] - (Z2 + Y3 + Yd) * tvY[Pd.IdxTrnCa_cb];
            tvDYdt[Pd.IdxTrn_cb] = Y3 * tvY[Pd.IdxTrnCa_cb] - (Z3 + Y4 + Yd) * tvY[Pd.IdxTrn_cb];

            dATPuse_contraction = 1.2 * A_Fb * 2 * 0.00001 * (Z2 * tvY[Pd.IdxTrnCa_cb] + Y4 * tvY[Pd.IdxTrn_cb]);

            myCell.TVc[Pd.InxdATPuse_contraction] = dATPuse_contraction;

            if (MainForm.BlnSProtocolOn == false)  //
            {
                EL = MainForm.ExternalLoad; // EL0;  //EL0は間違い 24Mar23
                Externalload_ = EL;
            }
            else  //EL protocol
            {
                if (myCell.IntELChangeProtcol == 0)
                {
                    EL = EL0;
                }
                else if (myCell.IntELChangeProtcol == 1)
                {
                    EL = EL0 + (ELinf - EL0) * (1 - Math.Exp(-(MainForm.TimeElp - MainForm.T_StimChangeOn) / MainForm.tau));
                    EL02 = EL;
                }
                else if (myCell.IntELChangeProtcol == 2)
                {
                    EL = EL0 + (EL02 - EL0) * (Math.Exp(-(MainForm.TimeElp - MainForm.T_StimChangeOff) / MainForm.tau));
                }
                Externalload_ = EL;
            }


            if (MainForm.ContState == 0)
            {
                F_ext = Externalload_;// MainForm.ExternalLoad;
                tvDYdt[Pd.IdxV_hsmL] = 0;
                tvDYdt[Pd.IdxhsmL] = (Externalload_ - (Fb + Fp)) / M_hsmL; // (MainForm.ExternalLoad - (Fb + Fp)) / M_hsmL;
            }
            else if (MainForm.ContState == 1)
            {
                double deltaL = 0.00000001;
                double EPS = 0.000000000000001;
                double Limit = 100;

                double h_length = tvY[Pd.IdxhsmL] - tvY[Pd.IdxhsmX];
                double NewHalfSL = tvY[Pd.IdxhsmL];

                for (int i = 0; i <= Limit; i++)
                {
                    if (NewHalfSL >= L0)
                    {
                        Fp = K_PE * (Math.Exp(D * (NewHalfSL / L0 - 1)) - 1);
                    }
                    else
                    {
                        Fp = -K_PL * (1 - (NewHalfSL / L0));
                    }

                    double ProductA = Externalload_ - Fp; // MainForm.ExternalLoad - Fp;
                    double hsmL_prev = NewHalfSL;

                    if ((NewHalfSL + deltaL) >= L0)
                    {
                        Fp = K_PE * (Math.Exp(D * ((NewHalfSL + deltaL) / L0 - 1)) - 1);
                    }
                    else
                    {
                        Fp = -K_PL * (1 - ((NewHalfSL + deltaL) / L0));
                    }

                    double ProductB = (Externalload_ - Fp);//(MainForm.ExternalLoad - Fp);
                    double deltaA__deltat = (ProductB - ProductA) / deltaL;

                    if (deltaA__deltat == 0.0)
                    {
                        break;
                    }

                    NewHalfSL = hsmL_prev - ProductA / deltaA__deltat;

                    if (Math.Abs(NewHalfSL - hsmL_prev) < Math.Abs(hsmL_prev) * EPS)
                    {
                        break;
                    }
                }

                S_func = 1 / (1 + Math.Exp((NewHalfSL - 0.85) / (-0.08))) / (1 + Math.Exp((NewHalfSL - 1.4) / 0.06));

                Fb = A_Fb * S_func * (tvY[Pd.IdxTrnCa_cb] + tvY[Pd.IdxTrn_cb]) * (NewHalfSL - tvY[Pd.IdxhsmX]);

                tvY[Pd.IdxhsmL] = NewHalfSL;
                tvY[Pd.IdxhsmX] = NewHalfSL - h_length;
                F_ext = Fb + Fp + Externalload_;//Fb + Fp + MainForm.ExternalLoad;

                tvDYdt[Pd.IdxV_hsmL] = 0.0; 
                tvDYdt[Pd.IdxhsmL] = 0.0;
            }

            myCell.TVc[Pd.InxFb] = Fb;
            myCell.TVc[Pd.InxTwitch] = Twitch;
            myCell.TVc[Pd.InxF_ext] = F_ext;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpContraction_ListView;
            ListView.LVDispValue("Contraction", IxTrn_total, ref Trn_total);
            ListView.LVDispValue("Contraction", Ixa_cm, ref a_cm);
            ListView.LVDispValue("Contraction", Ixb_cm, ref b_cm);
            ListView.LVDispValue("Contraction", Ixf_cm, ref f_cm);
            ListView.LVDispValue("Contraction", Ixg_cm, ref g_cm);

            ListView.LVDispValue("Contraction", IxB_eff, ref B_eff);
            ListView.LVDispValue("Contraction", Ixh_c, ref h_c);
            ListView.LVDispValue("Contraction", IxA_Fb, ref A_Fb);
            ListView.LVDispValue("Contraction", IxK_PE, ref K_PE);
            ListView.LVDispValue("Contraction", IxK_PL, ref K_PL);
            ListView.LVDispValue("Contraction", IxD, ref D);
            ListView.LVDispValue("Contraction", IxL0, ref L0);
            ListView.LVDispValue("Contraction", IxM_hsmL, ref M_hsmL);

            ListView.LVDispValue("Contraction", IxFb, ref Fb);
            ListView.LVDispValue("Contraction", IxTwitch, ref Twitch);
            ListView.LVDispValue("Contraction", IxF_ext, ref F_ext);

            ListView.LVDispValue("Contraction", IxJ_Ca_contraction, ref J_Ca_contraction);
            ListView.LVDispValue("Contraction", IxdATPuse_contraction, ref dATPuse_contraction);

            ListView.LVDispValue("Contraction", IxTrnCa, ref myTVc[Pd.IdxTrnCa]);
            ListView.LVDispValue("Contraction", IxTrnCa_cb, ref myTVc[Pd.IdxTrnCa_cb]);
            ListView.LVDispValue("Contraction", IxTrn_cb, ref myTVc[Pd.IdxTrn_cb]);
            ListView.LVDispValue("Contraction", IxhsmX, ref myTVc[Pd.IdxhsmX]);
            ListView.LVDispValue("Contraction", IxhsmL, ref myTVc[Pd.IdxhsmL]);
            ListView.LVDispValue("Contraction", IxV_hsmL, ref myTVc[Pd.IdxV_hsmL]);

            ListView.LVDispValue("Contraction", IxFb_Min, ref Fb_Min);
            ListView.LVDispValue("Contraction", IxFb_Max, ref Fb_Max);
            ListView.LVDispValue("Contraction", IxhsmL_Min, ref hsmL_Min);
            ListView.LVDispValue("Contraction", IxhsmL_Max, ref hsmL_Max);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpContraction_ListView;
            ListView.LVModiValue("Contraction", IxTrn_total, ref Trn_total);
            ListView.LVModiValue("Contraction", Ixa_cm, ref a_cm);
            ListView.LVModiValue("Contraction", Ixb_cm, ref b_cm);
            ListView.LVModiValue("Contraction", Ixf_cm, ref f_cm);
            ListView.LVModiValue("Contraction", Ixg_cm, ref g_cm);

            ListView.LVModiValue("Contraction", IxB_eff, ref B_eff);
            ListView.LVModiValue("Contraction", Ixh_c, ref h_c);
            ListView.LVModiValue("Contraction", IxA_Fb, ref A_Fb);
            ListView.LVModiValue("Contraction", IxK_PE, ref K_PE);
            ListView.LVModiValue("Contraction", IxK_PL, ref K_PL);
            ListView.LVModiValue("Contraction", IxD, ref D);
            ListView.LVModiValue("Contraction", IxL0, ref L0);
            ListView.LVModiValue("Contraction", IxM_hsmL, ref M_hsmL);

            ListView.LVModiValue("Contraction", IxFb, ref Fb);
            ListView.LVModiValue("Contraction", IxTwitch, ref Twitch);
            ListView.LVModiValue("Contraction", IxF_ext, ref F_ext);

            ListView.LVModiValue("Contraction", IxJ_Ca_contraction, ref J_Ca_contraction);
            ListView.LVModiValue("Contraction", IxdATPuse_contraction, ref dATPuse_contraction);

            ListView.LVModiValue("Contraction", IxTrnCa, ref myTVc[Pd.IdxTrnCa]);
            ListView.LVModiValue("Contraction", IxTrnCa_cb, ref myTVc[Pd.IdxTrnCa_cb]);
            ListView.LVModiValue("Contraction", IxTrn_cb, ref myTVc[Pd.IdxTrn_cb]);
            ListView.LVModiValue("Contraction", IxhsmX, ref myTVc[Pd.IdxhsmX]);
            ListView.LVModiValue("Contraction", IxhsmL, ref myTVc[Pd.IdxhsmL]);
            ListView.LVModiValue("Contraction", IxV_hsmL, ref myTVc[Pd.IdxV_hsmL]);

            ListView.LVModiValue("Contraction", IxFb_Min, ref Fb_Min);
            ListView.LVModiValue("Contraction", IxFb_Max, ref Fb_Max);
            ListView.LVModiValue("Contraction", IxhsmL_Min, ref hsmL_Min);
            ListView.LVModiValue("Contraction", IxhsmL_Max, ref hsmL_Max);
        }
    }
}
