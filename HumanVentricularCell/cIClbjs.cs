using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cIClbjs : cElement
    {
        private Pd.TIdx IxgClb;

        public double gClb = 0.009; // nS/pF for ventricular cell

        public cIClbjs()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");
            SetIx(ref IxgClb, ref i, Pd.IntPar, "gClb");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpIClbjs_ListView, NumOfIx);

            Itc = myTVc[Pd.InxIClbjs_Itc];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            Itc = SF * myCell.JS.fraction_js * gClb * (tvY[Pd.IdxVm] - myCell.Rp.Cl);
            myCell.TVc[Pd.InxIClbjs_Itc] = Itc;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIClbjs_ListView;
            ListView.LVDispValue("IClbjs", IxSF, ref SF);
            ListView.LVDispValue("IClbjs", IxItc, ref Itc);
            ListView.LVDispValue("IClbjs", IxgClb, ref gClb);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIClbjs_ListView;
            ListView.LVModiValue("IClbjs", IxSF, ref SF);
            ListView.LVModiValue("IClbjs", IxItc, ref Itc);
            ListView.LVModiValue("IClbjs", IxgClb, ref gClb);
        }
    }
}
