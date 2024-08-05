using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cItosjs : cElement
    {
        private Pd.TIdx Ixxtos;
        private Pd.TIdx Ixxtos_inf;
        private Pd.TIdx Ixxtos_tau;
        private Pd.TIdx Ixytos;
        private Pd.TIdx Ixytos_inf;
        private Pd.TIdx Ixytos_tau;
        private Pd.TIdx Ixgtos;

        public double xtos;
        public double xtos_inf;
        public double xtos_tau;
        public double ytos;
        public double ytos_inf;
        public double ytos_tau;

        public double gtos = 0.0156; // nS/pF for ventricular cell

        public cItosjs()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");
            SetIx(ref Ixgtos, ref i, Pd.IntPar, "gtos");

            SetIx(ref Ixxtos, ref i, Pd.IntVar, "xtos");
            SetIx(ref Ixxtos_inf, ref i, Pd.IntVar, "xtos_inf");
            SetIx(ref Ixxtos_tau, ref i, Pd.IntVar, "xtos_tau");
            SetIx(ref Ixytos, ref i, Pd.IntVar, "ytos");
            SetIx(ref Ixytos_inf, ref i, Pd.IntVar, "ytos_inf");
            SetIx(ref Ixytos_tau, ref i, Pd.IntVar, "ytos_tau");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpItosjs_ListView, NumOfIx);

            xtos = myTVc[Pd.IdxItos_xtos_js];
            ytos = myTVc[Pd.IdxItos_ytos_js];
            Itc = myTVc[Pd.InxItosjs_Itc];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Vm = tvY[Pd.IdxVm];

            xtos_inf = 1.0 / (1.0 + Math.Exp((19.0 - Vm) / 13));
            xtos_tau = 0.5 + 9.0 / (1.0 + Math.Exp((Vm + 3.0) / 15));

            xtos = tvY[Pd.IdxItos_xtos_js];
            tvDYdt[Pd.IdxItos_xtos_js] = (xtos_inf - xtos) / xtos_tau;

            ytos_inf = 1.0 / (1.0 + Math.Exp((Vm + 19.5) / 5.0));
            ytos_tau = 30 + 800 / (1.0 + Math.Exp((Vm + 60.0) / 10));

            ytos = tvY[Pd.IdxItos_ytos_js];
            tvDYdt[Pd.IdxItos_ytos_js] = (ytos_inf - ytos) / ytos_tau;

            Itc = SF * myCell.JS.fraction_js * gtos * xtos * ytos * (Vm - myCell.Rp.K);
            myCell.TVc[Pd.InxItosjs_Itc] = Itc;
        }


        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpItosjs_ListView;
            ListView.LVDispValue("Itosjs", IxSF, ref SF);
            ListView.LVDispValue("Itosjs", IxItc, ref Itc);
            ListView.LVDispValue("Itosjs", Ixgtos, ref gtos);

            ListView.LVDispValue("Itosjs", Ixxtos, ref myTVc[Pd.IdxItos_xtos_js]);
            ListView.LVDispValue("Itosjs", Ixxtos_inf, ref xtos_inf);
            ListView.LVDispValue("Itosjs", Ixxtos_tau, ref xtos_tau);
            ListView.LVDispValue("Itosjs", Ixytos, ref myTVc[Pd.IdxItos_ytos_js]);
            ListView.LVDispValue("Itosjs", Ixytos_inf, ref ytos_inf);
            ListView.LVDispValue("Itosjs", Ixytos_tau, ref ytos_tau);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpItosjs_ListView;
            ListView.LVModiValue("Itosjs", IxSF, ref SF);
            ListView.LVModiValue("Itosjs", IxItc, ref Itc);
            ListView.LVModiValue("Itosjs", Ixgtos, ref gtos);

            ListView.LVModiValue("Itosjs", Ixxtos, ref myTVc[Pd.IdxItos_xtos_js]);
            ListView.LVModiValue("Itosjs", Ixxtos_inf, ref xtos_inf);
            ListView.LVModiValue("Itosjs", Ixxtos_tau, ref xtos_tau);
            ListView.LVModiValue("Itosjs", Ixytos, ref myTVc[Pd.IdxItos_ytos_js]);
            ListView.LVModiValue("Itosjs", Ixytos_inf, ref ytos_inf);
            ListView.LVModiValue("Itosjs", Ixytos_tau, ref ytos_tau);
        }
    }
}
