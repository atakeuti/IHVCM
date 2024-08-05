using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cINaKsl : cElement
    {
        private Pd.TIdx IxKmKo;
        private Pd.TIdx IxKmNai;
        private Pd.TIdx IxANaK;
        private Pd.TIdx IxdATPuse_INaKsl;

        public double KmKo = 1.5; // mM
        public double KmNai = 11.0; // mM

        public double ANaK = 0.9 * 1.8; // =1.62 pA/pF for ventricular cell
        // beta-AR stimulation was incorporated according to Kurata et al., Front Physiol, 2020
        public double ANaK_;

        public double dATPuse_INaKsl;

        public double A, A0, Ainf, A02;

        public cINaKsl()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");
            SetIx(ref IxANaK, ref i, Pd.IntPar, "ANaK");
            SetIx(ref IxKmKo, ref i, Pd.IntVar, "KmKo");
            SetIx(ref IxKmNai, ref i, Pd.IntVar, "KmNai");

            SetIx(ref IxdATPuse_INaKsl, ref i, Pd.IntVar, "dATPuse_INaKsl");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpINaKsl_ListView, NumOfIx);

            Itc = myTVc[Pd.InxINaKsl_Itc];

            dATPuse_INaKsl = myTVc[Pd.InxdATPuse_INaKsl];

            A0 = 1.0;
            A = 1.0;

            double alpha = 1.0; // 1.0, satndard bAR stimulation
            Ainf = 1 + alpha * 0.2;
            A02 = Ainf;
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double VFRT = tvY[Pd.IdxVm] / Pd.RTF;
            double sigma = (Math.Exp(myCell.ExtCell.Nao / 67.3) - 1) / 7;
            double fNaK = 1 / (1 + 0.1245 * Math.Exp(-0.1 * VFRT) + 0.0365 * sigma * Math.Exp(-VFRT));
            double KmNaiOverNai = KmNai / tvY[Pd.IdxNasl];

            if (MainForm.BlnSProtocolOn == false)  //bAR stim on
            {
                if (MainForm.betaARState == 0)
                {
                    ANaK_ = A0 * ANaK;
                }
                else if (MainForm.betaARState == 1)
                {
                    ANaK_ = Ainf * ANaK;
                }
            }
            else  //AR protocol
            {
                if (myCell.IntbARstimProtcol == 0)
                {
                    ANaK_ = A0 * ANaK;
                }
                else if (myCell.IntbARstimProtcol == 1)
                {
                    A = A0 + (Ainf - A0) * (1 - Math.Exp(-(MainForm.TimeElp - MainForm.T_StimChangeOn) / MainForm.tau));
                    A02 = A;
                    ANaK_ = A * ANaK;
                }
                else if (myCell.IntbARstimProtcol == 2)
                {
                    A = A0 + (A02 - A0) * (Math.Exp(-(MainForm.TimeElp - MainForm.T_StimChangeOff) / MainForm.tau));
                    ANaK_ = A * ANaK;
                }
            }

            // beta-AR stimulation was incorporated according to Kurata et al., Front Physiol, 2020
            Itc = SF * myCell.SL.fraction_sl * ANaK_ * fNaK * (myCell.ExtCell.Ko / (myCell.ExtCell.Ko + KmKo)) * (1 / (1 + KmNaiOverNai * KmNaiOverNai * KmNaiOverNai * KmNaiOverNai));
            myCell.TVc[Pd.InxINaKsl_Itc] = Itc;

            dATPuse_INaKsl = Itc * myCell.Cm / (myCell.Vi_diff * Pd.F); // 3Na+ : 2K+ : 1ATP
            myCell.TVc[Pd.InxdATPuse_INaKsl] = dATPuse_INaKsl;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpINaKsl_ListView;
            ListView.LVDispValue("INaKsl", IxSF, ref SF);
            ListView.LVDispValue("INaKsl", IxItc, ref Itc);
            ListView.LVDispValue("INaKsl", IxANaK, ref ANaK);
            ListView.LVDispValue("INaKsl", IxKmKo, ref KmKo);
            ListView.LVDispValue("INaKsl", IxKmNai, ref KmNai);

            ListView.LVDispValue("INaKsl", IxdATPuse_INaKsl, ref dATPuse_INaKsl);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpINaKsl_ListView;
            ListView.LVModiValue("INaKsl", IxSF, ref SF);
            ListView.LVModiValue("INaKsl", IxItc, ref Itc);
            ListView.LVModiValue("INaKsl", IxANaK, ref ANaK);
            ListView.LVModiValue("INaKsl", IxKmKo, ref KmKo);
            ListView.LVModiValue("INaKsl", IxKmNai, ref KmNai);

            ListView.LVModiValue("INaKsl", IxdATPuse_INaKsl, ref dATPuse_INaKsl);
        }
    }
}
