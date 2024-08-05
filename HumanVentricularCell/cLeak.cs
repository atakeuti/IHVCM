using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cLeak : cElement
    {
        private Pd.TIdx IxVmax_Leak;
        private Pd.TIdx IxJ_Leak;
        private Pd.TIdx IxF_Leak;

        public double Vmax_Leak = 2.3 * 5.348e-6;

        public double J_Leak;
        public double F_Leak;

        public cLeak()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxVmax_Leak, ref i, Pd.IntPar, "Vmax_Leak");
            SetIx(ref IxJ_Leak, ref i, Pd.IntVar, "J_Leak");
            SetIx(ref IxF_Leak, ref i, Pd.IntVar, "F_Leak");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpLeak_ListView, NumOfIx);

            J_Leak = myTVc[Pd.InxJ_Leak];
            F_Leak = myTVc[Pd.InxF_Leak];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            J_Leak = SF * Vmax_Leak * (tvY[Pd.IdxCasr] - tvY[Pd.IdxCajs]);
            F_Leak = J_Leak * myCell.Cyt.Vcyt;

            myCell.TVc[Pd.InxJ_Leak] = J_Leak;
            myCell.TVc[Pd.InxF_Leak] = F_Leak;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpLeak_ListView;
            ListView.LVDispValue("Leak", IxSF, ref SF);
            ListView.LVDispValue("Leak", IxJ_Leak, ref J_Leak);
            ListView.LVDispValue("Leak", IxF_Leak, ref F_Leak);

            ListView.LVDispValue("Leak", IxVmax_Leak, ref Vmax_Leak);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpLeak_ListView;
            ListView.LVModiValue("Leak", IxSF, ref SF);
            ListView.LVModiValue("Leak", IxJ_Leak, ref J_Leak);
            ListView.LVModiValue("Leak", IxF_Leak, ref F_Leak);

            ListView.LVModiValue("Leak", IxVmax_Leak, ref Vmax_Leak);
        }
    }
}
