using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cINabsl : cElement
    {
        private Pd.TIdx IxgNab;
        public double gNab = 5.97e-4; // nS/pF

        public cINabsl()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");
            SetIx(ref IxgNab, ref i, Pd.IntPar, "gNab");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpINabsl_ListView, NumOfIx);

            Itc = myTVc[Pd.InxINabsl_Itc];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            Itc = SF * myCell.SL.fraction_sl * gNab * (tvY[Pd.IdxVm] - myCell.Rp.Nasl);
            myCell.TVc[Pd.InxINabsl_Itc] = Itc;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpINabsl_ListView;
            ListView.LVDispValue("INabsl", IxSF, ref SF);
            ListView.LVDispValue("INabsl", IxItc, ref Itc);
            ListView.LVDispValue("INabsl", IxgNab, ref gNab);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpINabsl_ListView;
            ListView.LVModiValue("INabsl", IxSF, ref SF);
            ListView.LVModiValue("INabsl", IxItc, ref Itc);
            ListView.LVModiValue("INabsl", IxgNab, ref gNab);
        }
    }
}
