using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cItofjs : cElement
    {
        private Pd.TIdx Ixxtof;
        private Pd.TIdx Ixxtof_inf;
        private Pd.TIdx Ixxtof_tau;
        private Pd.TIdx Ixytof;
        private Pd.TIdx Ixytof_inf;
        private Pd.TIdx Ixytof_tau;
        private Pd.TIdx Ixgtof;

        public double xtof;
        public double xtof_inf;
        public double xtof_tau;
        public double ytof;
        public double ytof_inf;
        public double ytof_tau;

        public double gtof = 0.1144; // nS/pF for ventricular cell

        public cItofjs()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");
            SetIx(ref Ixgtof, ref i, Pd.IntPar, "gtof");

            SetIx(ref Ixxtof, ref i, Pd.IntVar, "xtof");
            SetIx(ref Ixxtof_inf, ref i, Pd.IntVar, "xtof_inf");
            SetIx(ref Ixxtof_tau, ref i, Pd.IntVar, "xtof_tau");
            SetIx(ref Ixytof, ref i, Pd.IntVar, "ytof");
            SetIx(ref Ixytof_inf, ref i, Pd.IntVar, "ytof_inf");
            SetIx(ref Ixytof_tau, ref i, Pd.IntVar, "ytof_tau");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpItofjs_ListView, NumOfIx);

            xtof = myTVc[Pd.IdxItof_xtof_js];
            ytof = myTVc[Pd.IdxItof_ytof_js];
            Itc = myTVc[Pd.InxItofjs_Itc];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Vm = tvY[Pd.IdxVm];

            xtof_inf = 1.0 / (1.0 + Math.Exp((19.0 - Vm) / 13));
            xtof_tau = 0.5 + 8.5 * Math.Exp(-((Vm + 45) / 50) * ((Vm + 45) / 50));

            xtof = tvY[Pd.IdxItof_xtof_js];
            tvDYdt[Pd.IdxItof_xtof_js] = (xtof_inf - xtof) / xtof_tau;

            ytof_inf = 1.0 / (1.0 + Math.Exp((Vm + 19.5) / 5.0));
            ytof_tau = 7.0 + 85 * Math.Exp(-(Vm + 40) * (Vm + 40) / 220);

            ytof = tvY[Pd.IdxItof_ytof_js];
            tvDYdt[Pd.IdxItof_ytof_js] = (ytof_inf - ytof) / ytof_tau;

            Itc = SF * myCell.JS.fraction_js * gtof * xtof * ytof * (Vm - myCell.Rp.K);
            myCell.TVc[Pd.InxItofjs_Itc] = Itc;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpItofjs_ListView;
            ListView.LVDispValue("Itofjs", IxSF, ref SF);
            ListView.LVDispValue("Itofjs", IxItc, ref Itc);
            ListView.LVDispValue("Itofjs", Ixgtof, ref gtof);

            ListView.LVDispValue("Itofjs", Ixxtof, ref myTVc[Pd.IdxItof_xtof_js]);
            ListView.LVDispValue("Itofjs", Ixxtof_inf, ref xtof_inf);
            ListView.LVDispValue("Itofjs", Ixxtof_tau, ref xtof_tau);
            ListView.LVDispValue("Itofjs", Ixytof, ref myTVc[Pd.IdxItof_ytof_js]);
            ListView.LVDispValue("Itofjs", Ixytof_inf, ref ytof_inf);
            ListView.LVDispValue("Itofjs", Ixytof_tau, ref ytof_tau);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpItofjs_ListView;
            ListView.LVModiValue("Itofjs", IxSF, ref SF);
            ListView.LVModiValue("Itofjs", IxItc, ref Itc);
            ListView.LVModiValue("Itofjs", Ixgtof, ref gtof);

            ListView.LVModiValue("Itofjs", Ixxtof, ref myTVc[Pd.IdxItof_xtof_js]);
            ListView.LVModiValue("Itofjs", Ixxtof_inf, ref xtof_inf);
            ListView.LVModiValue("Itofjs", Ixxtof_tau, ref xtof_tau);
            ListView.LVModiValue("Itofjs", Ixytof, ref myTVc[Pd.IdxItof_ytof_js]);
            ListView.LVModiValue("Itofjs", Ixytof_inf, ref ytof_inf);
            ListView.LVModiValue("Itofjs", Ixytof_tau, ref ytof_tau);
        }
    }
}
