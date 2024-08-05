using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cNabuffjs : cElement
    {
        private Pd.TIdx IxbNabuffjs;
        private Pd.TIdx Ixkonjs;
        private Pd.TIdx Ixkoffjs;
        private Pd.TIdx IxNabuffjs_tot;
        private Pd.TIdx IxJ_Nabuffjs;

        public double bNabuffjs;  // bound Na buffer in JS space
        public double konjs = 0.1e-3; // /mM*msec
        public double koffjs = 1e-3; // /msec
        public double Nabuffjs_tot = 7.561; // mM
        public double J_Nabuffjs;

        public cNabuffjs()  //Constructor 
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxbNabuffjs, ref i, Pd.IntVar, "bNabuffjs");
            SetIx(ref Ixkonjs, ref i, Pd.IntPar, "konjs");
            SetIx(ref Ixkoffjs, ref i, Pd.IntPar, "koffjs");
            SetIx(ref IxNabuffjs_tot, ref i, Pd.IntPar, "Nabuffjs_tot");
            SetIx(ref IxJ_Nabuffjs, ref i, Pd.IntVar, "J_Nabuffjs");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpNabuffjs_ListView, NumOfIx);

            bNabuffjs = myTVc[Pd.IdxbNabuffjs];
            J_Nabuffjs = myTVc[Pd.InxJ_Nabuffjs];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            tvDYdt[Pd.IdxbNabuffjs] = konjs * tvY[Pd.IdxNajs] * (Nabuffjs_tot - tvY[Pd.IdxbNabuffjs]) - koffjs * tvY[Pd.IdxbNabuffjs];
            J_Nabuffjs = tvDYdt[Pd.IdxbNabuffjs];

            myCell.TVc[Pd.InxJ_Nabuffjs] = J_Nabuffjs;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpNabuffjs_ListView;
            ListView.LVDispValue("Nabuffjs", IxbNabuffjs, ref myTVc[Pd.IdxbNabuffjs]);
            ListView.LVDispValue("Nabuffjs", Ixkonjs, ref konjs);
            ListView.LVDispValue("Nabuffjs", Ixkoffjs, ref koffjs);
            ListView.LVDispValue("Nabuffjs", IxNabuffjs_tot, ref Nabuffjs_tot);

            ListView.LVDispValue("Nabuffjs", IxJ_Nabuffjs, ref J_Nabuffjs);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpNabuffjs_ListView;
            ListView.LVModiValue("Nabuffjs", IxbNabuffjs, ref myTVc[Pd.IdxbNabuffjs]);
            ListView.LVModiValue("Nabuffjs", Ixkonjs, ref konjs);
            ListView.LVModiValue("Nabuffjs", Ixkoffjs, ref koffjs);
            ListView.LVModiValue("Nabuffjs", IxNabuffjs_tot, ref Nabuffjs_tot);

            ListView.LVModiValue("Nabuffjs", IxJ_Nabuffjs, ref J_Nabuffjs);
        }
    }
}
