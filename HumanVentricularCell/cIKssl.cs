using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cIKssl : cElement
    {
        private Pd.TIdx Ixxks;
        private Pd.TIdx Ixxks_inf;
        private Pd.TIdx Ixxks_tau;
        private Pd.TIdx Ixpnak;
        private Pd.TIdx IxgKs;

        public double xks;
        public double xks_inf;
        public double xks_tau;
        public double pnak = 0.01833;

        // beta-AR stimulation was incorporated according to Kurata et al., Front Physiol, 2020
        public double gKs = 0.0035; // nS/pF
        public double gKs_;

        public double A, A0, Ainf, A02;
        public double Vshift, Vshift0, Vshiftinf, Vshift02;

        public cIKssl()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");
            SetIx(ref IxgKs, ref i, Pd.IntPar, "gKs");

            SetIx(ref Ixxks, ref i, Pd.IntVar, "xks");
            SetIx(ref Ixxks_inf, ref i, Pd.IntVar, "xks_inf");
            SetIx(ref Ixxks_tau, ref i, Pd.IntVar, "xks_tau");
            SetIx(ref Ixpnak, ref i, Pd.IntPar, "pnak");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpIKssl_ListView, NumOfIx);

            xks = myTVc[Pd.IdxIKs_xks_sl];
            Itc = myTVc[Pd.InxIKssl_Itc];

            A0 = 1.0;
            A = 1.0;

            double alpha = 1.0; // 1.0, satndard bAR stimulation
            Ainf = 1 + alpha * 11;
            A02 = Ainf;

            Vshift0 = 0.0;
            Vshift = 0.0;

            Vshiftinf = alpha * 5.0;
            Vshift02 = Vshiftinf;
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double eKs = Pd.RTF * Math.Log((myCell.ExtCell.Ko + pnak * myCell.ExtCell.Nao) / (tvY[Pd.IdxKi] + pnak * tvY[Pd.IdxNasl]));

            if (MainForm.BlnSProtocolOn == false)  //bAR stim on
            {
                if (MainForm.betaARState == 0)
                {
                    xks_inf = 1.0 / (1.0 + Math.Exp((-tvY[Pd.IdxVm] - 3.8 - Vshift0) / 14.25));
                    xks_tau = 990.1 / (1.0 + Math.Exp((-tvY[Pd.IdxVm] - 2.436 - Vshift0) / 14.12));
                    gKs_ = A0 * gKs;
                }
                else if (MainForm.betaARState == 1)
                {
                    xks_inf = 1.0 / (1.0 + Math.Exp((-tvY[Pd.IdxVm] - 3.8 - Vshiftinf) / 14.25));
                    xks_tau = 990.1 / (1.0 + Math.Exp((-tvY[Pd.IdxVm] - 2.436 - Vshiftinf) / 14.12));
                    gKs_ = Ainf * gKs;
                }
            }
            else  //AR protocol
            {
                if (myCell.IntbARstimProtcol == 0)
                {
                    xks_inf = 1.0 / (1.0 + Math.Exp((-tvY[Pd.IdxVm] - 3.8 - Vshift0) / 14.25));
                    xks_tau = 990.1 / (1.0 + Math.Exp((-tvY[Pd.IdxVm] - 2.436 - Vshift0) / 14.12));
                    gKs_ = A0 * gKs;
                }
                else if (myCell.IntbARstimProtcol == 1)
                {
                    A = A0 + (Ainf - A0) * (1 - Math.Exp(-(MainForm.TimeElp - MainForm.T_StimChangeOn) / MainForm.tau));
                    A02 = A;
                    Vshift = Vshift0 + (Vshiftinf - Vshift0) * (1 - Math.Exp(-(MainForm.TimeElp - MainForm.T_StimChangeOn) / MainForm.tau));
                    Vshift02 = Vshift;

                    xks_inf = 1.0 / (1.0 + Math.Exp((-tvY[Pd.IdxVm] - 3.8 - Vshift) / 14.25));
                    xks_tau = 990.1 / (1.0 + Math.Exp((-tvY[Pd.IdxVm] - 2.436 - Vshift) / 14.12));
                    gKs_ = A * gKs;
                }
                else if (myCell.IntbARstimProtcol == 2)
                {
                    A = A0 + (A02 - A0) * (Math.Exp(-(MainForm.TimeElp - MainForm.T_StimChangeOff) / MainForm.tau));
                    Vshift = Vshift0 + (Vshift02 - Vshift0) * (Math.Exp(-(MainForm.TimeElp - MainForm.T_StimChangeOff) / MainForm.tau));
                    xks_inf = 1.0 / (1.0 + Math.Exp((-tvY[Pd.IdxVm] - 3.8 - Vshift) / 14.25));
                    xks_tau = 990.1 / (1.0 + Math.Exp((-tvY[Pd.IdxVm] - 2.436 - Vshift) / 14.12));
                    gKs_ = A * gKs;
                }
            }

            xks = tvY[Pd.IdxIKs_xks_sl];
            tvDYdt[Pd.IdxIKs_xks_sl] = (xks_inf - xks) / xks_tau;

            // beta-AR stimulation was incorporated according to Kurata et al., Front Physiol, 2020
            Itc = SF * myCell.SL.fraction_sl * gKs_ * xks * xks * (tvY[Pd.IdxVm] - eKs);
            myCell.TVc[Pd.InxIKssl_Itc] = Itc;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIKssl_ListView;
            ListView.LVDispValue("IKssl", IxSF, ref SF);
            ListView.LVDispValue("IKssl", IxItc, ref Itc);
            ListView.LVDispValue("IKssl", IxgKs, ref gKs);

            ListView.LVDispValue("IKssl", Ixxks, ref myTVc[Pd.IdxIKs_xks_sl]);
            ListView.LVDispValue("IKssl", Ixxks_inf, ref xks_inf);
            ListView.LVDispValue("IKssl", Ixxks_tau, ref xks_tau);
            ListView.LVDispValue("IKssl", Ixpnak, ref pnak);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIKssl_ListView;
            ListView.LVModiValue("IKssl", IxSF, ref SF);
            ListView.LVModiValue("IKssl", IxItc, ref Itc);
            ListView.LVModiValue("IKssl", IxgKs, ref gKs);

            ListView.LVModiValue("IKssl", Ixxks, ref myTVc[Pd.IdxIKs_xks_sl]);
            ListView.LVModiValue("IKssl", Ixxks_inf, ref xks_inf);
            ListView.LVModiValue("IKssl", Ixxks_tau, ref xks_tau);
            ListView.LVModiValue("IKssl", Ixpnak, ref pnak);
        }
    }
}
