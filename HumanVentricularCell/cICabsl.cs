using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cICabsl : cElement
    {
        private Pd.TIdx IxgCab;
        public double gCab = 5.513e-4; // nS/pF

        public cICabsl()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");
            SetIx(ref IxgCab, ref i, Pd.IntPar, "gCab");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpICabsl_ListView, NumOfIx);

            Itc = myTVc[Pd.InxICabsl_Itc];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            Itc = SF * myCell.SL.fraction_sl * gCab * (tvY[Pd.IdxVm] - myCell.Rp.Casl);
            myCell.TVc[Pd.InxICabsl_Itc] = Itc;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpICabsl_ListView;
            ListView.LVDispValue("ICabsl", IxSF, ref SF);
            ListView.LVDispValue("ICabsl", IxItc, ref Itc);
            ListView.LVDispValue("ICabsl", IxgCab, ref gCab);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpICabsl_ListView;
            ListView.LVModiValue("ICabsl", IxSF, ref SF);
            ListView.LVModiValue("ICabsl", IxItc, ref Itc);
            ListView.LVModiValue("ICabsl", IxgCab, ref gCab);
        }
    }
}
