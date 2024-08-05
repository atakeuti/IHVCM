using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cIKrsl : cElement
    {
        private Pd.TIdx Ixxkr;
        private Pd.TIdx Ixxkr_inf;
        private Pd.TIdx Ixxkr_tau;
        private Pd.TIdx IxgKr;

        public double xkr;
        public double xkr_inf;
        public double xkr_tau;

        public double gKr = 0.035; // nS/pF

        public cIKrsl()  //Constructor
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

            Lf.DGViews_Initialize(Lf.tpIKrsl_ListView, NumOfIx);

            xkr = myTVc[Pd.IdxIKr_xkr_sl];
            Itc = myTVc[Pd.InxIKrsl_Itc];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Vm = tvY[Pd.IdxVm];
            xkr_inf = 1.0 / (1.0 + Math.Exp((Vm + 10) / -5));
            xkr_tau = (550 / (1 + Math.Exp((-22 - Vm) / 9))) * (6 / (1 + Math.Exp((Vm + 11) / 9))) + (230 / (1 + Math.Exp((Vm + 40) / 20)));

            xkr = tvY[Pd.IdxIKr_xkr_sl];
            tvDYdt[Pd.IdxIKr_xkr_sl] = (xkr_inf - xkr) / xkr_tau;

            double rkr = 1 / (1 + Math.Exp((Vm + 74) / 24));

            Itc = SF * myCell.SL.fraction_sl * gKr * Math.Sqrt(myCell.ExtCell.Ko / 5.4) * xkr * rkr * (Vm - myCell.Rp.K);
            myCell.TVc[Pd.InxIKrsl_Itc] = Itc;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIKrsl_ListView;
            ListView.LVDispValue("IKrsl", IxSF, ref SF);
            ListView.LVDispValue("IKrsl", IxItc, ref Itc);
            ListView.LVDispValue("IKrsl", IxgKr, ref gKr);

            ListView.LVDispValue("IKrsl", Ixxkr, ref myTVc[Pd.IdxIKr_xkr_sl]);
            ListView.LVDispValue("IKrsl", Ixxkr_inf, ref xkr_inf);
            ListView.LVDispValue("IKrsl", Ixxkr_tau, ref xkr_tau);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIKrsl_ListView;
            ListView.LVModiValue("IKrsl", IxSF, ref SF);
            ListView.LVModiValue("IKrsl", IxItc, ref Itc);
            ListView.LVModiValue("IKrsl", IxgKr, ref gKr);

            ListView.LVModiValue("IKrsl", Ixxkr, ref myTVc[Pd.IdxIKr_xkr_sl]);
            ListView.LVModiValue("IKrsl", Ixxkr_inf, ref xkr_inf);
            ListView.LVModiValue("IKrsl", Ixxkr_tau, ref xkr_tau);
        }
    }
}
