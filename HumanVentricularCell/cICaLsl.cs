using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cICaLsl : cElement
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

        public double A, A0, Ainf, A02;

        public cICaLsl()  //Constructor
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

            Lf.DGViews_Initialize(Lf.tpICaLsl_ListView, NumOfIx);

            d = myTVc[Pd.IdxICaL_d_sl];
            f = myTVc[Pd.IdxICaL_f_sl];
            fCaB = myTVc[Pd.IdxICaL_fCaB_sl];
            Itc = myTVc[Pd.InxICaLsl_Itc];
            ICaL_Ca = myTVc[Pd.InxICaL_Casl];
            ICaL_Na = myTVc[Pd.InxICaL_Nasl];
            ICaL_K = myTVc[Pd.InxICaL_Ksl];

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
            double Cai = tvY[Pd.IdxCasl];
            double Ki = tvY[Pd.IdxKi];
            double Nai = tvY[Pd.IdxNasl];
            double Exp_VFRT = Math.Exp(VFRT);
            double Exp_2VFRT = Math.Exp(2 * VFRT);

            d_inf = 1.0 / (1.0 + Math.Exp((-5 - Vm) / 6.0));
            d_tau = d_inf * ((1 - Math.Exp((-5 - Vm) / 6.0)) / (0.035 * (Vm + 5)));

            d = tvY[Pd.IdxICaL_d_sl];
            tvDYdt[Pd.IdxICaL_d_sl] = (d_inf - d) / d_tau;

            f_inf = 1.0 / (1.0 + Math.Exp((Vm + 35) / 9)) + 0.6 / (1.0 + Math.Exp((50 - Vm) / 20));
            f_tau = 1.0 / (0.0197 * Math.Exp(-(0.0337 * (Vm + 14.5)) * (0.0337 * (Vm + 14.5))) + 0.02);

            f = tvY[Pd.IdxICaL_f_sl];
            tvDYdt[Pd.IdxICaL_f_sl] = (f_inf - f) / f_tau;

            fCaB = tvY[Pd.IdxICaL_fCaB_sl];
            tvDYdt[Pd.IdxICaL_fCaB_sl] = 1.7 * Cai * (1 - fCaB) - (11.9E-3) * fCaB;

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

            ICaL_Ca = SF * myCell.SL.fraction_slCa * ICaL_PCa_ * d * f * (1 - fCaB) * 4 * Pd.F * VFRT * (0.341 * Cai * Exp_2VFRT - 0.341 * myCell.ExtCell.Cao) / (Exp_2VFRT - 1.0);
            ICaL_Na = SF * myCell.SL.fraction_slCa * ICaL_PNa_ * d * f * (1 - fCaB) * Pd.F * VFRT * (0.75 * Nai * Exp_VFRT - 0.75 * myCell.ExtCell.Nao) / (Exp_VFRT - 1.0);
            ICaL_K = SF * myCell.SL.fraction_slCa * ICaL_PK_ * d * f * (1 - fCaB) * Pd.F * VFRT * (0.75 * Ki * Exp_VFRT - 0.75 * myCell.ExtCell.Ko) / (Exp_VFRT - 1.0);

            Itc = ICaL_Ca + ICaL_Na + ICaL_K;

            myCell.TVc[Pd.InxICaLsl_Itc] = Itc;
            myCell.TVc[Pd.InxICaL_Casl] = ICaL_Ca;
            myCell.TVc[Pd.InxICaL_Nasl] = ICaL_Na;
            myCell.TVc[Pd.InxICaL_Ksl] = ICaL_K;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpICaLsl_ListView;
            ListView.LVDispValue("ICaLsl", IxSF, ref SF);
            ListView.LVDispValue("ICaLsl", IxItc, ref Itc);

            ListView.LVDispValue("ICaLsl", Ixd, ref myTVc[Pd.IdxICaL_d_sl]);
            ListView.LVDispValue("ICaLsl", Ixd_inf, ref d_inf);
            ListView.LVDispValue("ICaLsl", Ixd_tau, ref d_tau);

            ListView.LVDispValue("ICaLsl", Ixf, ref myTVc[Pd.IdxICaL_f_sl]);
            ListView.LVDispValue("ICaLsl", Ixf_inf, ref f_inf);
            ListView.LVDispValue("ICaLsl", Ixf_tau, ref f_tau);

            ListView.LVDispValue("ICaLsl", IxfCaB, ref myTVc[Pd.IdxICaL_fCaB_sl]);

            ListView.LVDispValue("ICaLsl", IxICaL_PCa, ref ICaL_PCa);
            ListView.LVDispValue("ICaLsl", IxICaL_PNa, ref ICaL_PNa);
            ListView.LVDispValue("ICaLsl", IxICaL_PK, ref ICaL_PK);

            ListView.LVDispValue("ICaLsl", IxICaL_Ca, ref ICaL_Ca);
            ListView.LVDispValue("ICaLsl", IxICaL_Na, ref ICaL_Na);
            ListView.LVDispValue("ICaLsl", IxICaL_K, ref ICaL_K);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpICaLsl_ListView;
            ListView.LVModiValue("ICaLsl", IxSF, ref SF);
            ListView.LVModiValue("ICaLsl", IxItc, ref Itc);

            ListView.LVModiValue("ICaLsl", Ixd, ref myTVc[Pd.IdxICaL_d_sl]);
            ListView.LVModiValue("ICaLsl", Ixd_inf, ref d_inf);
            ListView.LVModiValue("ICaLsl", Ixd_tau, ref d_tau);

            ListView.LVModiValue("ICaLsl", Ixf, ref myTVc[Pd.IdxICaL_f_sl]);
            ListView.LVModiValue("ICaLsl", Ixf_inf, ref f_inf);
            ListView.LVModiValue("ICaLsl", Ixf_tau, ref f_tau);

            ListView.LVModiValue("ICaLsl", IxfCaB, ref myTVc[Pd.IdxICaL_fCaB_sl]);

            ListView.LVModiValue("ICaLsl", IxICaL_PCa, ref ICaL_PCa);
            ListView.LVModiValue("ICaLsl", IxICaL_PNa, ref ICaL_PNa);
            ListView.LVModiValue("ICaLsl", IxICaL_PK, ref ICaL_PK);

            ListView.LVModiValue("ICaLsl", IxICaL_Ca, ref ICaL_Ca);
            ListView.LVModiValue("ICaLsl", IxICaL_Na, ref ICaL_Na);
            ListView.LVModiValue("ICaLsl", IxICaL_K, ref ICaL_K);
        }
    }
}
