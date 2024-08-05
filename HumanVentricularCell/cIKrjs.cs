using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cIKrjs : cElement
    {
        private Pd.TIdx Ixxkr;
        private Pd.TIdx Ixxkr_inf;
        private Pd.TIdx Ixxkr_tau;
        private Pd.TIdx IxgKr;

        public double xkr;
        public double xkr_inf;
        public double xkr_tau;

        public double gKr = 0.035; // nS/pF

        public cIKrjs()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");
            SetIx(ref IxgKr, ref i, Pd.IntPar, "gKr");

            SetIx(ref Ixxkr, ref i, Pd.IntVar, "xkr");
            SetIx(ref Ixxkr_inf, ref i, Pd.IntVar, "xkr_inf");
            SetIx(ref Ixxkr_tau, ref i, Pd.IntVar, "xkr_tau");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpIKrjs_ListView, NumOfIx);

            xkr = myTVc[Pd.IdxIKr_xkr_js];
            Itc = myTVc[Pd.InxIKrjs_Itc];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Vm = tvY[Pd.IdxVm];
            xkr_inf = 1.0 / (1.0 + Math.Exp((Vm + 10) / -5));
            xkr_tau = (550 / (1 + Math.Exp((-22 - Vm) / 9))) * (6 / (1 + Math.Exp((Vm + 11) / 9))) + (230 / (1 + Math.Exp((Vm + 40) / 20)));

            xkr = tvY[Pd.IdxIKr_xkr_js];
            tvDYdt[Pd.IdxIKr_xkr_js] = (xkr_inf - xkr) / xkr_tau;

            double rkr = 1 / (1 + Math.Exp((Vm + 74) / 24));

            Itc = SF * myCell.JS.fraction_js * gKr * Math.Sqrt(myCell.ExtCell.Ko / 5.4) * xkr * rkr * (Vm - myCell.Rp.K);
            myCell.TVc[Pd.InxIKrjs_Itc] = Itc;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIKrjs_ListView;
            ListView.LVDispValue("IKrjs", IxSF, ref SF);
            ListView.LVDispValue("IKrjs", IxItc, ref Itc);
            ListView.LVDispValue("IKrjs", IxgKr, ref gKr);

            ListView.LVDispValue("IKrjs", Ixxkr, ref myTVc[Pd.IdxIKr_xkr_js]);
            ListView.LVDispValue("IKrjs", Ixxkr_inf, ref xkr_inf);
            ListView.LVDispValue("IKrjs", Ixxkr_tau, ref xkr_tau);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIKrjs_ListView;
            ListView.LVModiValue("IKrjs", IxSF, ref SF);
            ListView.LVModiValue("IKrjs", IxItc, ref Itc);
            ListView.LVModiValue("IKrjs", IxgKr, ref gKr);

            ListView.LVModiValue("IKrjs", Ixxkr, ref myTVc[Pd.IdxIKr_xkr_js]);
            ListView.LVModiValue("IKrjs", Ixxkr_inf, ref xkr_inf);
            ListView.LVModiValue("IKrjs", Ixxkr_tau, ref xkr_tau);
        }
    }
}
