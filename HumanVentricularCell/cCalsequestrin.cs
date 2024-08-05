using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cCalsequestrin : cElement
    {
        private Pd.TIdx IxbCSQN;
        private Pd.TIdx IxkonCSQN;
        private Pd.TIdx IxkoffCSQN;
        private Pd.TIdx IxCSQNtot;

        private Pd.TIdx IxJ_CSQN;

        public double bCSQN;  // bound CSQN
        public double konCSQN = 100.0; // /mM*msec
        public double koffCSQN = 65.0; // /msec
        public double CSQNtot = 0.14; // mM //

        public double J_CSQN;

        public cCalsequestrin()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxbCSQN, ref i, Pd.IntVar, "bCSQN");
            SetIx(ref IxkonCSQN, ref i, Pd.IntPar, "konCSQN");
            SetIx(ref IxkoffCSQN, ref i, Pd.IntPar, "koffCSQN");
            SetIx(ref IxCSQNtot, ref i, Pd.IntPar, "CSQNtot");

            SetIx(ref IxJ_CSQN, ref i, Pd.IntVar, "J_CSQN");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpCalsequestrin_ListView, NumOfIx);

            bCSQN = myTVc[Pd.IdxbCSQN];
            J_CSQN = myTVc[Pd.InxJ_CSQN];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Vi = myCell.Cyt.Vcyt;
            double Vsr = myCell.SR.Vsr;
            double volumeratio = Vi / Vsr;

            double totB = CSQNtot * volumeratio;

            tvDYdt[Pd.IdxbCSQN] = konCSQN * tvY[Pd.IdxCasr] * (totB - tvY[Pd.IdxbCSQN]) - koffCSQN * tvY[Pd.IdxbCSQN];
            J_CSQN = tvDYdt[Pd.IdxbCSQN];
            myCell.TVc[Pd.InxJ_CSQN] = J_CSQN;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpCalsequestrin_ListView;
            ListView.LVDispValue("CSQN", IxbCSQN, ref myTVc[Pd.IdxbCSQN]);
            ListView.LVDispValue("CSQN", IxkonCSQN, ref konCSQN);
            ListView.LVDispValue("CSQN", IxkoffCSQN, ref koffCSQN);
            ListView.LVDispValue("CSQN", IxCSQNtot, ref CSQNtot);

            ListView.LVDispValue("CSQN", IxJ_CSQN, ref J_CSQN);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpCalsequestrin_ListView;
            ListView.LVModiValue("CSQN", IxbCSQN, ref myTVc[Pd.IdxbCSQN]);
            ListView.LVModiValue("CSQN", IxkonCSQN, ref konCSQN);
            ListView.LVModiValue("CSQN", IxkoffCSQN, ref koffCSQN);
            ListView.LVModiValue("CSQN", IxCSQNtot, ref CSQNtot);

            ListView.LVModiValue("CSQN", IxJ_CSQN, ref J_CSQN);
        }
    }
}
