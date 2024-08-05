using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cIClbsl : cElement
    {
        private Pd.TIdx IxgClb;

        public double gClb = 0.009; // nS/pF for ventricular cell

        public cIClbsl()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");
            SetIx(ref IxgClb, ref i, Pd.IntPar, "gClb");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpIClbsl_ListView, NumOfIx);

            Itc = myTVc[Pd.InxIClbsl_Itc];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            Itc = SF * myCell.SL.fraction_sl * gClb * (tvY[Pd.IdxVm] - myCell.Rp.Cl);
            myCell.TVc[Pd.InxIClbsl_Itc] = Itc;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIClbsl_ListView;
            ListView.LVDispValue("IClbsl", IxSF, ref SF);
            ListView.LVDispValue("IClbsl", IxItc, ref Itc);
            ListView.LVDispValue("IClbsl", IxgClb, ref gClb);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIClbsl_ListView;
            ListView.LVModiValue("IClbsl", IxSF, ref SF);
            ListView.LVModiValue("IClbsl", IxItc, ref Itc);
            ListView.LVModiValue("IClbsl", IxgClb, ref gClb);
        }
    }
}
