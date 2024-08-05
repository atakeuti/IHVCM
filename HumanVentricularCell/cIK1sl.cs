using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cIK1sl : cElement
    {
        private Pd.TIdx Ixgate;
        private Pd.TIdx IxgK1;

        public double gate;

        public double gK1 = 0.35; // nS/pF for ventricular cell

        public cIK1sl()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");
            SetIx(ref IxgK1, ref i, Pd.IntPar, "gK1");

            SetIx(ref Ixgate, ref i, Pd.IntVar, "gate");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpIK1sl_ListView, NumOfIx);

            gate = myTVc[Pd.InxIK1_gate_sl];
            Itc = myTVc[Pd.InxIK1sl_Itc];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double eK1 = tvY[Pd.IdxVm] - myCell.Rp.K;

            double alpha = 1.02 / (1.0 + Math.Exp(0.2385 * (eK1 - 59.215)));
            double beta = (0.49124 * Math.Exp(0.08032 * (eK1 + 5.476)) + Math.Exp(0.06175 * (eK1 - 594.31))) / (1.0 + Math.Exp(-0.5143 * (eK1 + 4.753)));

            gate = alpha / (alpha + beta);

            Itc = SF * myCell.SL.fraction_sl * gK1 * gate * Math.Sqrt(myCell.ExtCell.Ko / 5.4) * eK1;

            myCell.TVc[Pd.InxIK1_gate_sl] = gate;
            myCell.TVc[Pd.InxIK1sl_Itc] = Itc;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIK1sl_ListView;
            ListView.LVDispValue("IK1sl", IxSF, ref SF);
            ListView.LVDispValue("IK1sl", IxItc, ref Itc);
            ListView.LVDispValue("IK1sl", IxgK1, ref gK1);

            ListView.LVDispValue("IK1sl", Ixgate, ref gate);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIK1sl_ListView;
            ListView.LVModiValue("IK1sl", IxSF, ref SF);
            ListView.LVModiValue("IK1sl", IxItc, ref Itc);
            ListView.LVModiValue("IK1sl", IxgK1, ref gK1);

            ListView.LVModiValue("IK1sl", Ixgate, ref gate);
        }
    }
}
