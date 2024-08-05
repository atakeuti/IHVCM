using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HumanVentricularCell
{
    public class cICaLjs : cElement
    {
        private Pd.TIdx Ixd;
        private Pd.TIdx Ixd_inf;
        private Pd.TIdx Ixd_tau;

        private Pd.TIdx Ixf;
        private Pd.TIdx Ixf_inf;
        private Pd.TIdx Ixf_tau;

        private Pd.TIdx IxfCaB;

        private Pd.TIdx IxICaL_PCa;
        private Pd.TIdx IxICaL_PNa;
        private Pd.TIdx IxICaL_PK;

        private Pd.TIdx IxICaL_Ca;
        private Pd.TIdx IxICaL_Na;
        private Pd.TIdx IxICaL_K;

        public double d;
        public double d_inf;
        public double d_tau;

        public double f;
        public double f_inf;
        public double f_tau;

        public double fCaB;

        // beta-AR stimulation was incorporated according to Kurata et al., Front Physiol, 2020
        public double ICaL_PCa = 0.1215; // x1000 x cm/sec
        public double ICaL_PNa = 3.375E-6; // x1000 x cm/sec
        public double ICaL_PK = 6.075E-5; // x1000 x cm/sec
        public double ICaL_PCa_;
        public double ICaL_PNa_;
        public double ICaL_PK_;

        public double ICaL_Ca;
        public double ICaL_Na;
        public double ICaL_K;

        //24Mar18
        public double A, A0, Ainf, A02;

        public cICaLjs()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");

            SetIx(ref Ixd, ref i, Pd.IntVar, "d");
            SetIx(ref Ixd_inf, ref i, Pd.IntVar, "d_inf");
            SetIx(ref Ixd_tau, ref i, Pd.IntVar, "d_tau");

            SetIx(ref Ixf, ref i, Pd.IntVar, "f");
            SetIx(ref Ixf_inf, ref i, Pd.IntVar, "f_inf");
            SetIx(ref Ixf_tau, ref i, Pd.IntVar, "f_tau");

            SetIx(ref IxfCaB, ref i, Pd.IntVar, "fCaB");

            SetIx(ref IxICaL_PCa, ref i, Pd.IntPar, "ICaL_PCa");
            SetIx(ref IxICaL_PNa, ref i, Pd.IntPar, "ICaL_PNa");
            SetIx(ref IxICaL_PK, ref i, Pd.IntPar, "ICaL_PK");

            SetIx(ref IxICaL_Ca, ref i, Pd.IntVar, "ICaL_Ca");
            SetIx(ref IxICaL_Na, ref i, Pd.IntVar, "ICaL_Na");
            SetIx(ref IxICaL_K, ref i, Pd.IntVar, "ICaL_K");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpICaLjs_ListView, NumOfIx);

            d = myTVc[Pd.IdxICaL_d_js];
            f = myTVc[Pd.IdxICaL_f_js];
            fCaB = myTVc[Pd.IdxICaL_fCaB_js];
            Itc = myTVc[Pd.InxICaLjs_Itc];
            ICaL_Ca = myTVc[Pd.InxICaL_Cajs];
            ICaL_Na = myTVc[Pd.InxICaL_Najs];
            ICaL_K = myTVc[Pd.InxICaL_Kjs];

            A0 = 1.0;
            A = 1.0;
            double alpha = 1.0; // 1.0, satndard bAR stimulation
            Ainf = 1 + alpha * 0.3;
            A02 = Ainf;
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Vm = tvY[Pd.IdxVm];
            double VFRT = Vm / Pd.RTF;
            double Cai = tvY[Pd.IdxCajs];
            double Ki = tvY[Pd.IdxKi];
            double Nai = tvY[Pd.IdxNajs];

            double Exp_VFRT = Math.Exp(VFRT);
            double Exp_2VFRT = Math.Exp(2 * VFRT);

            d_inf = 1.0 / (1.0 + Math.Exp((-5 - Vm) / 6.0));
            d_tau = d_inf * ((1 - Math.Exp((-5 - Vm) / 6.0)) / (0.035 * (Vm + 5)));

            d = tvY[Pd.IdxICaL_d_js];
            tvDYdt[Pd.IdxICaL_d_js] = (d_inf - d) / d_tau;

            f_inf = 1.0 / (1.0 + Math.Exp((Vm + 35) / 9)) + 0.6 / (1.0 + Math.Exp((50 - Vm) / 20));
            f_tau = 1.0 / (0.0197 * Math.Exp(-(0.0337 * (Vm + 14.5)) * (0.0337 * (Vm + 14.5))) + 0.02);

            f = tvY[Pd.IdxICaL_f_js];
            tvDYdt[Pd.IdxICaL_f_js] = (f_inf - f) / f_tau;

            fCaB = tvY[Pd.IdxICaL_fCaB_js];
            tvDYdt[Pd.IdxICaL_fCaB_js] = 1.7 * Cai * (1 - fCaB) - (11.9E-3) * fCaB;

            // beta-AR stimulation was incorporated according to Kurata et al., Front Physiol, 2020
            if (MainForm.BlnSProtocolOn == false)  //bAR stim on
            {
                if (MainForm.betaARState == 0)
                {
                    ICaL_PCa_ = ICaL_PCa;
                    ICaL_PNa_ = ICaL_PNa;
                    ICaL_PK_ = ICaL_PK;
                }
                else if (MainForm.betaARState == 1)
                {
                    ICaL_PCa_ = Ainf * ICaL_PCa;
                    ICaL_PNa_ = Ainf * ICaL_PNa;
                    ICaL_PK_ = Ainf * ICaL_PK;
                }
            }
            else  //AR protocol
            {
                if (myCell.IntbARstimProtcol == 0)
                {
                    ICaL_PCa_ = A0 * ICaL_PCa;
                    ICaL_PNa_ = A0 * ICaL_PNa;
                    ICaL_PK_ = A0 * ICaL_PK;
                }
                else if (myCell.IntbARstimProtcol == 1)
                {
                    A = A0 + (Ainf - A0) * (1 - Math.Exp(-(MainForm.TimeElp - MainForm.T_StimChangeOn) / MainForm.tau));
                    A02 = A;
                    ICaL_PCa_ = A * ICaL_PCa;
                    ICaL_PNa_ = A * ICaL_PNa;
                    ICaL_PK_ = A * ICaL_PK;
                }
                else if (myCell.IntbARstimProtcol == 2)
                {
                    A = A0 + (A02 - A0) * (Math.Exp(-(MainForm.TimeElp - MainForm.T_StimChangeOff) / MainForm.tau));
                    ICaL_PCa_ = A * ICaL_PCa;
                    ICaL_PNa_ = A * ICaL_PNa;
                    ICaL_PK_ = A * ICaL_PK;
                }
            }

            ICaL_Ca = SF * myCell.JS.fraction_jsCa * ICaL_PCa_ * d * f * (1 - fCaB) * 4 * Pd.F * VFRT * (0.341 * Cai * Exp_2VFRT - 0.341 * myCell.ExtCell.Cao) / (Exp_2VFRT - 1.0);
            ICaL_Na = SF * myCell.JS.fraction_jsCa * ICaL_PNa_ * d * f * (1 - fCaB) * Pd.F * VFRT * (0.75 * Nai * Exp_VFRT - 0.75 * myCell.ExtCell.Nao) / (Exp_VFRT - 1.0);
            ICaL_K = SF * myCell.JS.fraction_jsCa * ICaL_PK_ * d * f * (1 - fCaB) * Pd.F * VFRT * (0.75 * Ki * Exp_VFRT - 0.75 * myCell.ExtCell.Ko) / (Exp_VFRT - 1.0);

            Itc = ICaL_Ca + ICaL_Na + ICaL_K;

            myCell.TVc[Pd.InxICaLjs_Itc] = Itc;
            myCell.TVc[Pd.InxICaL_Cajs] = ICaL_Ca;
            myCell.TVc[Pd.InxICaL_Najs] = ICaL_Na;
            myCell.TVc[Pd.InxICaL_Kjs] = ICaL_K;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpICaLjs_ListView;
            ListView.LVDispValue("ICaLjs", IxSF, ref SF);
            ListView.LVDispValue("ICaLjs", IxItc, ref Itc);

            ListView.LVDispValue("ICaLjs", Ixd, ref myTVc[Pd.IdxICaL_d_js]);
            ListView.LVDispValue("ICaLjs", Ixd_inf, ref d_inf);
            ListView.LVDispValue("ICaLjs", Ixd_tau, ref d_tau);

            ListView.LVDispValue("ICaLjs", Ixf, ref myTVc[Pd.IdxICaL_f_js]);
            ListView.LVDispValue("ICaLjs", Ixf_inf, ref f_inf);
            ListView.LVDispValue("ICaLjs", Ixf_tau, ref f_tau);

            ListView.LVDispValue("ICaLjs", IxfCaB, ref myTVc[Pd.IdxICaL_fCaB_js]);

            ListView.LVDispValue("ICaLjs", IxICaL_PCa, ref ICaL_PCa);
            ListView.LVDispValue("ICaLjs", IxICaL_PNa, ref ICaL_PNa);
            ListView.LVDispValue("ICaLjs", IxICaL_PK, ref ICaL_PK);

            ListView.LVDispValue("ICaLjs", IxICaL_Ca, ref ICaL_Ca);
            ListView.LVDispValue("ICaLjs", IxICaL_Na, ref ICaL_Na);
            ListView.LVDispValue("ICaLjs", IxICaL_K, ref ICaL_K);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpICaLjs_ListView;
            ListView.LVModiValue("ICaLjs", IxSF, ref SF);
            ListView.LVModiValue("ICaLjs", IxItc, ref Itc);

            ListView.LVModiValue("ICaLjs", Ixd, ref myTVc[Pd.IdxICaL_d_js]);
            ListView.LVModiValue("ICaLjs", Ixd_inf, ref d_inf);
            ListView.LVModiValue("ICaLjs", Ixd_tau, ref d_tau);

            ListView.LVModiValue("ICaLjs", Ixf, ref myTVc[Pd.IdxICaL_f_js]);
            ListView.LVModiValue("ICaLjs", Ixf_inf, ref f_inf);
            ListView.LVModiValue("ICaLjs", Ixf_tau, ref f_tau);

            ListView.LVModiValue("ICaLjs", IxfCaB, ref myTVc[Pd.IdxICaL_fCaB_js]);

            ListView.LVModiValue("ICaLjs", IxICaL_PCa, ref ICaL_PCa);
            ListView.LVModiValue("ICaLjs", IxICaL_PNa, ref ICaL_PNa);
            ListView.LVModiValue("ICaLjs", IxICaL_PK, ref ICaL_PK);

            ListView.LVModiValue("ICaLjs", IxICaL_Ca, ref ICaL_Ca);
            ListView.LVModiValue("ICaLjs", IxICaL_Na, ref ICaL_Na);
            ListView.LVModiValue("ICaLjs", IxICaL_K, ref ICaL_K);
        }
    }
}
