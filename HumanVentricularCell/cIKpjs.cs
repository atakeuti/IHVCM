using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cIKpjs : cElement
    {
        private Pd.TIdx IxgKp;
        public double gKp = 0.0020; // nS/pF

        public cIKpjs()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");
            SetIx(ref IxgKp, ref i, Pd.IntPar, "gKp");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpIKpjs_ListView, NumOfIx);

            Itc = myTVc[Pd.InxIKpjs_Itc];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            Itc = SF * myCell.JS.fraction_js * gKp * (tvY[Pd.IdxVm] - myCell.Rp.K) / (1.0 + Math.Exp(7.488 - tvY[Pd.IdxVm] / 5.98));
            myCell.TVc[Pd.InxIKpjs_Itc] = Itc;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIKpjs_ListView;
            ListView.LVDispValue("IKpjs", IxSF, ref SF);
            ListView.LVDispValue("IKpjs", IxItc, ref Itc);
            ListView.LVDispValue("IKpjs", IxgKp, ref gKp);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIKpjs_ListView;
            ListView.LVModiValue("IKpjs", IxSF, ref SF);
            ListView.LVModiValue("IKpjs", IxItc, ref Itc);
            ListView.LVModiValue("IKpjs", IxgKp, ref gKp);
        }
    }
}
