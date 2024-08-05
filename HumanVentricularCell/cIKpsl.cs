using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cIKpsl : cElement
    {
        private Pd.TIdx IxgKp;
        public double gKp = 0.0020; // nS/pF

        public cIKpsl()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");
            SetIx(ref IxgKp, ref i, Pd.IntPar, "gKp");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpIKpsl_ListView, NumOfIx);

            Itc = myTVc[Pd.InxIKpsl_Itc];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            Itc = SF * myCell.SL.fraction_sl * gKp * (tvY[Pd.IdxVm] - myCell.Rp.K) / (1.0 + Math.Exp(7.488 - tvY[Pd.IdxVm] / 5.98));
            myCell.TVc[Pd.InxIKpsl_Itc] = Itc;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIKpsl_ListView;
            ListView.LVDispValue("IKpsl", IxSF, ref SF);
            ListView.LVDispValue("IKpsl", IxItc, ref Itc);
            ListView.LVDispValue("IKpsl", IxgKp, ref gKp);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIKpsl_ListView;
            ListView.LVModiValue("IKpsl", IxSF, ref SF);
            ListView.LVModiValue("IKpsl", IxItc, ref Itc);
            ListView.LVModiValue("IKpsl", IxgKp, ref gKp);
        }
    }
}
