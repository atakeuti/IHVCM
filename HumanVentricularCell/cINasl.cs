using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cINasl : cElement
    {
        private Pd.TIdx Ixm;
        private Pd.TIdx Ixm_inf;
        private Pd.TIdx Ixm_tau;

        private Pd.TIdx Ixh;
        private Pd.TIdx Ixh_inf;
        private Pd.TIdx Ixh_tau;

        private Pd.TIdx Ixj;
        private Pd.TIdx Ixj_inf;
        private Pd.TIdx Ixj_tau;

        private Pd.TIdx IxgNa;

        public double m;
        public double m_inf;
        public double m_tau;

        public double h;
        public double h_inf;
        public double h_tau;

        public double j;
        public double j_inf;
        public double j_tau;

        public double gNa = 23.0; // nS/pF

        public cINasl()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");
            SetIx(ref IxgNa, ref i, Pd.IntPar, "gNa");

            SetIx(ref Ixm, ref i, Pd.IntVar, "m");
            SetIx(ref Ixm_inf, ref i, Pd.IntVar, "m_inf");
            SetIx(ref Ixm_tau, ref i, Pd.IntVar, "m_tau");
            SetIx(ref Ixh, ref i, Pd.IntVar, "h");
            SetIx(ref Ixh_inf, ref i, Pd.IntVar, "h_inf");
            SetIx(ref Ixh_tau, ref i, Pd.IntVar, "h_tau");
            SetIx(ref Ixj, ref i, Pd.IntVar, "j");
            SetIx(ref Ixj_inf, ref i, Pd.IntVar, "j_inf");
            SetIx(ref Ixj_tau, ref i, Pd.IntVar, "j_tau");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpINasl_ListView, NumOfIx);

            m = myTVc[Pd.IdxINa_m_sl];
            h = myTVc[Pd.IdxINa_h_sl];
            j = myTVc[Pd.IdxINa_j_sl];
            Itc = myTVc[Pd.InxINasl_Itc];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Vm = tvY[Pd.IdxVm];
            double Vm_Exp1 = Math.Exp((-56.86 - Vm) / 9.03) + 1.0;
            double Vm2 = (Vm + 45.79) / 15.54;
            double Vm3 = ((Vm - 4.823) / 51.12);

            // activation gate : m
            m_inf = 1.0 / (Vm_Exp1 * Vm_Exp1);
            m_tau = 0.1292 * Math.Exp(-Vm2 * Vm2) + 0.06487 * Math.Exp(-Vm3 * Vm3);

            m = tvY[Pd.IdxINa_m_sl];
            tvDYdt[Pd.IdxINa_m_sl] = (m_inf - m) / m_tau;

            if (Vm < -40)
            {
                //fast inactivation gate : h
                double alpha = 0.057 * Math.Exp((Vm + 80) / -6.8);
                double beta = 2.7 * Math.Exp(0.079 * Vm) + 310000 * Math.Exp(0.3485 * Vm);
                double denom1 = Math.Exp((Vm + 71.55) / 7.43) + 1.0;

                h_inf = 1.0 / (denom1 * denom1);
                h_tau = 1.0 / (alpha + beta);

                h = tvY[Pd.IdxINa_h_sl];
                tvDYdt[Pd.IdxINa_h_sl] = (h_inf - h) / h_tau;

                //slow inactivation gate : j
                alpha = ((-25428 * Math.Exp(0.2444 * Vm) - 0.000006948 * Math.Exp(-0.04391 * Vm)) * (Vm + 37.78)) / (1.0 + Math.Exp(0.311 * (Vm + 79.23)));
                beta = (0.02424 * Math.Exp(-0.01052 * Vm)) / (1.0 + Math.Exp(-0.1378 * (Vm + 40.14)));

                j_inf = h_inf;
                j_tau = 1.0 / (alpha + beta);

                j = tvY[Pd.IdxINa_j_sl];
                tvDYdt[Pd.IdxINa_j_sl] = (j_inf - j) / j_tau;
            }
            else
            {
                //fast inactivation gate : h
                double alpha = 0;
                double beta = 0.77 / (0.13 * (1.0 + Math.Exp(-(Vm + 10.66) / 11.1)));
                double denom1 = Math.Exp((Vm + 71.55) / 7.43) + 1.0;

                h_inf = 1.0 / (denom1 * denom1);
                h_tau = 1.0 / (alpha + beta);

                h = tvY[Pd.IdxINa_h_sl];
                tvDYdt[Pd.IdxINa_h_sl] = (h_inf - h) / h_tau;

                //slow inactivation gate : j
                alpha = 0;
                beta = 0.6 * Math.Exp(0.057 * Vm) / (1.0 + Math.Exp(-0.1 * (Vm + 32)));

                j_inf = h_inf;
                j_tau = 1.0 / (alpha + beta);

                j = tvY[Pd.IdxINa_j_sl];
                tvDYdt[Pd.IdxINa_j_sl] = (j_inf - j) / j_tau;
            }

            Itc = SF * myCell.SL.fraction_sl * gNa * m * m * m * h * j * (Vm - myCell.Rp.Nasl);
            myCell.TVc[Pd.InxINasl_Itc] = Itc;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpINasl_ListView;
            ListView.LVDispValue("INasl", IxSF, ref SF);
            ListView.LVDispValue("INasl", IxItc, ref Itc);
            ListView.LVDispValue("INasl", IxgNa, ref gNa);

            ListView.LVDispValue("INasl", Ixm, ref myTVc[Pd.IdxINa_m_sl]);
            ListView.LVDispValue("INasl", Ixm_inf, ref m_inf);
            ListView.LVDispValue("INasl", Ixm_tau, ref m_tau);

            ListView.LVDispValue("INasl", Ixh, ref myTVc[Pd.IdxINa_h_sl]);
            ListView.LVDispValue("INasl", Ixh_inf, ref h_inf);
            ListView.LVDispValue("INasl", Ixh_tau, ref h_tau);

            ListView.LVDispValue("INasl", Ixj, ref myTVc[Pd.IdxINa_j_sl]);
            ListView.LVDispValue("INasl", Ixj_inf, ref j_inf);
            ListView.LVDispValue("INasl", Ixj_tau, ref j_tau);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpINasl_ListView;
            ListView.LVModiValue("INasl", IxSF, ref SF);
            ListView.LVModiValue("INasl", IxItc, ref Itc);
            ListView.LVModiValue("INasl", IxgNa, ref gNa);

            ListView.LVModiValue("INasl", Ixm, ref myTVc[Pd.IdxINa_m_sl]);
            ListView.LVModiValue("INasl", Ixm_inf, ref m_inf);
            ListView.LVModiValue("INasl", Ixm_tau, ref m_tau);

            ListView.LVModiValue("INasl", Ixh, ref myTVc[Pd.IdxINa_h_sl]);
            ListView.LVModiValue("INasl", Ixh_inf, ref h_inf);
            ListView.LVModiValue("INasl", Ixh_tau, ref h_tau);

            ListView.LVModiValue("INasl", Ixj, ref myTVc[Pd.IdxINa_j_sl]);
            ListView.LVModiValue("INasl", Ixj_inf, ref j_inf);
            ListView.LVModiValue("INasl", Ixj_tau, ref j_tau);
        }
    }
}
