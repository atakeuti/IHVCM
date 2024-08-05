using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cCabuffjs : cElement
    {
        private Pd.TIdx IxbCabuffjs_low;
        private Pd.TIdx Ixkonjs_low;
        private Pd.TIdx Ixkoffjs_low;
        private Pd.TIdx IxCabuffjs_lowtot;

        private Pd.TIdx IxbCabuffjs_high;
        private Pd.TIdx Ixkonjs_high;
        private Pd.TIdx Ixkoffjs_high;
        private Pd.TIdx IxCabuffjs_hightot;

        private Pd.TIdx IxJ_Cabuff_js_tot;

        public double bCabuffjs_low;  // bound low affinity Ca buffer in JS space
        public double konjs_low = 100.0; // /mM*msec
        public double koffjs_low = 1.3; // /msec
        public double Cabuffjs_lowtot = 4.6E-4; // mM

        public double bCabuffjs_high;  // bound high affinity Ca buffer in JS space
        public double konjs_high = 100.0; // /mM*msec
        public double koffjs_high = 0.03; // /msec
        public double Cabuffjs_hightot = 1.65E-4; // mM

        public double J_Cabuff_js_tot;

        public cCabuffjs()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxbCabuffjs_low, ref i, Pd.IntVar, "bCabuffjs_low");
            SetIx(ref Ixkonjs_low, ref i, Pd.IntPar, "konjs_low");
            SetIx(ref Ixkoffjs_low, ref i, Pd.IntPar, "koffjs_low");
            SetIx(ref IxCabuffjs_lowtot, ref i, Pd.IntPar, "Cabuffjs_lowtot");

            SetIx(ref IxbCabuffjs_high, ref i, Pd.IntVar, "bCabuffjs_high");
            SetIx(ref Ixkonjs_high, ref i, Pd.IntPar, "konjs_high");
            SetIx(ref Ixkoffjs_high, ref i, Pd.IntPar, "koffjs_high");
            SetIx(ref IxCabuffjs_hightot, ref i, Pd.IntPar, "Cabuffjs_hightot");

            SetIx(ref IxJ_Cabuff_js_tot, ref i, Pd.IntVar, "J_Cabuff_js_tot");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpCabuffjs_ListView, NumOfIx);

            bCabuffjs_low = myTVc[Pd.IdxbCabuffjs_low];
            bCabuffjs_high = myTVc[Pd.IdxbCabuffjs_high];

            J_Cabuff_js_tot = myTVc[Pd.InxJ_Cabuff_js_tot];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Vi = myCell.Cyt.Vcyt;
            double Vjs = myCell.JS.Vjs;
            double volumeratio = Vi / Vjs;

            double totBlow = Cabuffjs_lowtot * volumeratio;
            double totBhigh = Cabuffjs_hightot * volumeratio;

            tvDYdt[Pd.IdxbCabuffjs_low] = konjs_low * tvY[Pd.IdxCajs] * (totBlow - tvY[Pd.IdxbCabuffjs_low]) - koffjs_low * tvY[Pd.IdxbCabuffjs_low];
            tvDYdt[Pd.IdxbCabuffjs_high] = konjs_high * tvY[Pd.IdxCajs] * (totBhigh - tvY[Pd.IdxbCabuffjs_high]) - koffjs_high * tvY[Pd.IdxbCabuffjs_high];

            J_Cabuff_js_tot = tvDYdt[Pd.IdxbCabuffjs_low] + tvDYdt[Pd.IdxbCabuffjs_high];
            myCell.TVc[Pd.InxJ_Cabuff_js_tot] = J_Cabuff_js_tot;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpCabuffjs_ListView;
            ListView.LVDispValue("Cabuffjs", IxbCabuffjs_low, ref myTVc[Pd.IdxbCabuffjs_low]);
            ListView.LVDispValue("Cabuffjs", Ixkonjs_low, ref konjs_low);
            ListView.LVDispValue("Cabuffjs", Ixkoffjs_low, ref koffjs_low);
            ListView.LVDispValue("Cabuffjs", IxCabuffjs_lowtot, ref Cabuffjs_lowtot);

            ListView.LVDispValue("Cabuffjs", IxbCabuffjs_high, ref myTVc[Pd.IdxbCabuffjs_high]);
            ListView.LVDispValue("Cabuffjs", Ixkonjs_high, ref konjs_high);
            ListView.LVDispValue("Cabuffjs", Ixkoffjs_high, ref koffjs_high);
            ListView.LVDispValue("Cabuffjs", IxCabuffjs_hightot, ref Cabuffjs_hightot);

            ListView.LVDispValue("Cabuffjs", IxJ_Cabuff_js_tot, ref J_Cabuff_js_tot);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpCabuffjs_ListView;
            ListView.LVModiValue("Cabuffjs", IxbCabuffjs_low, ref myTVc[Pd.IdxbCabuffjs_low]);
            ListView.LVModiValue("Cabuffjs", Ixkonjs_low, ref konjs_low);
            ListView.LVModiValue("Cabuffjs", Ixkoffjs_low, ref koffjs_low);
            ListView.LVModiValue("Cabuffjs", IxCabuffjs_lowtot, ref Cabuffjs_lowtot);

            ListView.LVModiValue("Cabuffjs", IxbCabuffjs_high, ref myTVc[Pd.IdxbCabuffjs_high]);
            ListView.LVModiValue("Cabuffjs", Ixkonjs_high, ref konjs_high);
            ListView.LVModiValue("Cabuffjs", Ixkoffjs_high, ref koffjs_high);
            ListView.LVModiValue("Cabuffjs", IxCabuffjs_hightot, ref Cabuffjs_hightot);

            ListView.LVModiValue("Cabuffjs", IxJ_Cabuff_js_tot, ref J_Cabuff_js_tot);
        }
    }
}
