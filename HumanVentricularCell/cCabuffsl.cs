using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cCabuffsl : cElement
    {
        private Pd.TIdx IxbCabuffsl_low;
        private Pd.TIdx Ixkonsl_low;
        private Pd.TIdx Ixkoffsl_low;
        private Pd.TIdx IxCabuffsl_lowtot;

        private Pd.TIdx IxbCabuffsl_high;
        private Pd.TIdx Ixkonsl_high;
        private Pd.TIdx Ixkoffsl_high;
        private Pd.TIdx IxCabuffsl_hightot;

        private Pd.TIdx IxJ_Cabuff_sl_tot;

        public double bCabuffsl_low;  // bound low affinity Ca buffer in SL space
        public double konsl_low = 100.0; // /mM*msec
        public double koffsl_low = 1.3; // /msec
        public double Cabuffsl_lowtot = 0.0374; // mM

        public double bCabuffsl_high;  // bound high affinity Ca buffer in SL space
        public double konsl_high = 100.0; // /mM*msec
        public double koffsl_high = 0.03; // /msec
        public double Cabuffsl_hightot = 0.0134; // mM

        public double J_Cabuff_sl_tot;

        public cCabuffsl()  //Constructor 
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxbCabuffsl_low, ref i, Pd.IntVar, "bCabuffsl_low");
            SetIx(ref Ixkonsl_low, ref i, Pd.IntPar, "konsl_low");
            SetIx(ref Ixkoffsl_low, ref i, Pd.IntPar, "koffsl_low");
            SetIx(ref IxCabuffsl_lowtot, ref i, Pd.IntPar, "Cabuffsl_lowtot");

            SetIx(ref IxbCabuffsl_high, ref i, Pd.IntVar, "bCabuffsl_high");
            SetIx(ref Ixkonsl_high, ref i, Pd.IntPar, "konsl_high");
            SetIx(ref Ixkoffsl_high, ref i, Pd.IntPar, "koffsl_high");
            SetIx(ref IxCabuffsl_hightot, ref i, Pd.IntPar, "Cabuffsl_hightot");

            SetIx(ref IxJ_Cabuff_sl_tot, ref i, Pd.IntVar, "J_Cabuff_sl_tot");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpCabuffsl_ListView, NumOfIx);

            bCabuffsl_low = myTVc[Pd.IdxbCabuffsl_low];
            bCabuffsl_high = myTVc[Pd.IdxbCabuffsl_high];

            J_Cabuff_sl_tot = myTVc[Pd.InxJ_Cabuff_sl_tot];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Vi = myCell.Cyt.Vcyt;
            double Vsl = myCell.SL.Vsl;
            double volumeratio = Vi / Vsl;

            double totBlow = Cabuffsl_lowtot * volumeratio;
            double totBhigh = Cabuffsl_hightot * volumeratio;

            tvDYdt[Pd.IdxbCabuffsl_low] = konsl_low * tvY[Pd.IdxCasl] * (totBlow - tvY[Pd.IdxbCabuffsl_low]) - koffsl_low * tvY[Pd.IdxbCabuffsl_low];
            tvDYdt[Pd.IdxbCabuffsl_high] = konsl_high * tvY[Pd.IdxCasl] * (totBhigh - tvY[Pd.IdxbCabuffsl_high]) - koffsl_high * tvY[Pd.IdxbCabuffsl_high];

            J_Cabuff_sl_tot = tvDYdt[Pd.IdxbCabuffsl_low] + tvDYdt[Pd.IdxbCabuffsl_high];
            myCell.TVc[Pd.InxJ_Cabuff_sl_tot] = J_Cabuff_sl_tot;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpCabuffsl_ListView;
            ListView.LVDispValue("Cabuffsl", IxbCabuffsl_low, ref myTVc[Pd.IdxbCabuffsl_low]);
            ListView.LVDispValue("Cabuffsl", Ixkonsl_low, ref konsl_low);
            ListView.LVDispValue("Cabuffsl", Ixkoffsl_low, ref koffsl_low);
            ListView.LVDispValue("Cabuffsl", IxCabuffsl_lowtot, ref Cabuffsl_lowtot);

            ListView.LVDispValue("Cabuffsl", IxbCabuffsl_high, ref myTVc[Pd.IdxbCabuffsl_high]);
            ListView.LVDispValue("Cabuffsl", Ixkonsl_high, ref konsl_high);
            ListView.LVDispValue("Cabuffsl", Ixkoffsl_high, ref koffsl_high);
            ListView.LVDispValue("Cabuffsl", IxCabuffsl_hightot, ref Cabuffsl_hightot);

            ListView.LVDispValue("Cabuffsl", IxJ_Cabuff_sl_tot, ref J_Cabuff_sl_tot);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpCabuffsl_ListView;
            ListView.LVModiValue("Cabuffsl", IxbCabuffsl_low, ref myTVc[Pd.IdxbCabuffsl_low]);
            ListView.LVModiValue("Cabuffsl", Ixkonsl_low, ref konsl_low);
            ListView.LVModiValue("Cabuffsl", Ixkoffsl_low, ref koffsl_low);
            ListView.LVModiValue("Cabuffsl", IxCabuffsl_lowtot, ref Cabuffsl_lowtot);

            ListView.LVModiValue("Cabuffsl", IxbCabuffsl_high, ref myTVc[Pd.IdxbCabuffsl_high]);
            ListView.LVModiValue("Cabuffsl", Ixkonsl_high, ref konsl_high);
            ListView.LVModiValue("Cabuffsl", Ixkoffsl_high, ref koffsl_high);
            ListView.LVModiValue("Cabuffsl", IxCabuffsl_hightot, ref Cabuffsl_hightot);

            ListView.LVModiValue("Cabuffsl", IxJ_Cabuff_sl_tot, ref J_Cabuff_sl_tot);
        }
    }
}
