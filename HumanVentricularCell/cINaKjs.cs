using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cINaKjs : cElement
    {
        private Pd.TIdx IxKmKo;
        private Pd.TIdx IxKmNai;
        private Pd.TIdx IxANaK;
        private Pd.TIdx IxdATPuse_INaKjs;

        public double KmKo = 1.5; // mM
        public double KmNai = 11.0; // mM

        // 24Mar05
        //public double ANaK = 1.8; // pA/pF for ventricular cell
        public double ANaK = 0.9 * 1.8; // =1.62 pA/pF for ventricular cell
        // 24Mar14 beta-AR stimulation was incorporated according to Kurata et al., Front Physiol, 2020
        public double ANaK_;

        public double dATPuse_INaKjs;

        public double A, A0, Ainf, A02;

        public cINaKjs()  //Constructor
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

            SetIx(ref IxdATPuse_INaKjs, ref i, Pd.IntVar, "dATPuse_INaKjs");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpINaKjs_ListView, NumOfIx);

            Itc = myTVc[Pd.InxINaKjs_Itc];

            dATPuse_INaKjs = myTVc[Pd.InxdATPuse_INaKjs];

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
            double KmNaiOverNai = KmNai / tvY[Pd.IdxNajs];

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
            Itc = SF * myCell.JS.fraction_js * ANaK_ * fNaK * (myCell.ExtCell.Ko / (myCell.ExtCell.Ko + KmKo)) * (1 / (1 + KmNaiOverNai * KmNaiOverNai * KmNaiOverNai * KmNaiOverNai));
            myCell.TVc[Pd.InxINaKjs_Itc] = Itc;

            dATPuse_INaKjs = Itc * myCell.Cm / (myCell.Vi_diff * Pd.F); // 3Na+ : 2K+ : 1ATP
            myCell.TVc[Pd.InxdATPuse_INaKjs] = dATPuse_INaKjs;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpINaKjs_ListView;
            ListView.LVDispValue("INaKjs", IxSF, ref SF);
            ListView.LVDispValue("INaKjs", IxItc, ref Itc);
            ListView.LVDispValue("INaKjs", IxANaK, ref ANaK);
            ListView.LVDispValue("INaKjs", IxKmKo, ref KmKo);
            ListView.LVDispValue("INaKjs", IxKmNai, ref KmNai);

            ListView.LVDispValue("INaKjs", IxdATPuse_INaKjs, ref dATPuse_INaKjs);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpINaKjs_ListView;
            ListView.LVModiValue("INaKjs", IxSF, ref SF);
            ListView.LVModiValue("INaKjs", IxItc, ref Itc);
            ListView.LVModiValue("INaKjs", IxANaK, ref ANaK);
            ListView.LVModiValue("INaKjs", IxKmKo, ref KmKo);
            ListView.LVModiValue("INaKjs", IxKmNai, ref KmNai);

            ListView.LVModiValue("INaKjs", IxdATPuse_INaKjs, ref dATPuse_INaKjs);
        }
    }
}
