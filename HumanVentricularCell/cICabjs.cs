using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cICabjs : cElement
    {
        private Pd.TIdx IxgCab;
        public double gCab = 5.513e-4; // nS/pF

        public cICabjs()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");
            SetIx(ref IxgCab, ref i, Pd.IntPar, "gCab");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpICabjs_ListView, NumOfIx);

            Itc = myTVc[Pd.InxICabjs_Itc];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            Itc = SF * myCell.JS.fraction_js * gCab * (tvY[Pd.IdxVm] - myCell.Rp.Cajs);
            myCell.TVc[Pd.InxICabjs_Itc] = Itc;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpICabjs_ListView;
            ListView.LVDispValue("ICabjs", IxSF, ref SF);
            ListView.LVDispValue("ICabjs", IxItc, ref Itc);
            ListView.LVDispValue("ICabjs", IxgCab, ref gCab);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpICabjs_ListView;
            ListView.LVModiValue("ICabjs", IxSF, ref SF);
            ListView.LVModiValue("ICabjs", IxItc, ref Itc);
            ListView.LVModiValue("ICabjs", IxgCab, ref gCab);
        }
    }
}
