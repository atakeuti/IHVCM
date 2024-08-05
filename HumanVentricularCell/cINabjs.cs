using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cINabjs : cElement
    {
        private Pd.TIdx IxgNab;
        public double gNab = 5.97e-4; // nS/pF

        public cINabjs()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");
            SetIx(ref IxgNab, ref i, Pd.IntPar, "gNab");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpINabjs_ListView, NumOfIx);

            Itc = myTVc[Pd.InxINabjs_Itc];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            Itc = SF * myCell.JS.fraction_js * gNab * (tvY[Pd.IdxVm] - myCell.Rp.Najs);
            myCell.TVc[Pd.InxINabjs_Itc] = Itc;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpINabjs_ListView;
            ListView.LVDispValue("INabjs", IxSF, ref SF);
            ListView.LVDispValue("INabjs", IxItc, ref Itc);
            ListView.LVDispValue("INabjs", IxgNab, ref gNab);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpINabjs_ListView;
            ListView.LVModiValue("INabjs", IxSF, ref SF);
            ListView.LVModiValue("INabjs", IxItc, ref Itc);
            ListView.LVModiValue("INabjs", IxgNab, ref gNab);
        }
    }
}
