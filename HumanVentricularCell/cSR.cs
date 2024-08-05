using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cSR : cElement
    {
        public cSERCA SERCA = new cSERCA();
        public cRyR RyR = new cRyR();
        public cLeak Leak = new cLeak();
        public cCalsequestrin Calsequestrin = new cCalsequestrin();

        private Pd.TIdx IxVsr;
        private Pd.TIdx IxVsr_part;
        private Pd.TIdx IxCasr;
        private Pd.TIdx Ixfraction_freeSERCA;

        private Pd.TIdx IxCasr_Min;
        private Pd.TIdx IxCasr_Max;

        public double Vsr;
        public double Vsr_part = 0.035; // part of cell volume occupied with SR
        public double Casr;  // Ca concentration in SR (mM)

        public double fraction_freeSERCA = 0.7;

        public double Casr_Min;
        public double Casr_Max;

        public cSR()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            Casr = myTVc[Pd.IdxCasr];
            Vsr = Vsr_part * myCell.Vcell;

            SERCA.Initialize(ref myTVc, ref myCell, ref Lf);
            RyR.Initialize(ref myTVc, ref myCell, ref Lf);
            Leak.Initialize(ref myTVc, ref myCell, ref Lf);
            Calsequestrin.Initialize(ref myTVc, ref myCell, ref Lf);

            int i = 0;

            SetIx(ref IxVsr, ref i, Pd.IntVar, "Vsr");
            SetIx(ref IxVsr_part, ref i, Pd.IntVar, "Vsr_part");
            SetIx(ref IxCasr, ref i, Pd.IntVar, "Casr");
            SetIx(ref Ixfraction_freeSERCA, ref i, Pd.IntVar, "fraction_freeSERCA");
            SetIx(ref IxCasr_Min, ref i, Pd.IntVar, "Casr_Min");
            SetIx(ref IxCasr_Max, ref i, Pd.IntVar, "Casr_Max");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpSR_ListView, NumOfIx);
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            SERCA.dydt(dt, ref tvDYdt, ref tvY, myCell);
            RyR.dydt(dt, ref tvDYdt, ref tvY, myCell);
            Leak.dydt(dt, ref tvDYdt, ref tvY, myCell);
            Calsequestrin.dydt(dt, ref tvDYdt, ref tvY, myCell);

            tvDYdt[Pd.IdxCasr] = myCell.SR.SERCA.J_SERCA - (myCell.SR.Leak.F_Leak / Vsr + myCell.SR.RyR.J_RyR)
                - myCell.Mitochondria.F_NmSC / Vsr
                - tvDYdt[Pd.IdxbCSQN];
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpSR_ListView;
            ListView.LVDispValue("SR", IxVsr, ref Vsr);
            ListView.LVDispValue("SR", IxVsr_part, ref Vsr_part);
            ListView.LVDispValue("SR", IxCasr, ref myTVc[Pd.IdxCasr]);
            ListView.LVDispValue("SR", Ixfraction_freeSERCA, ref fraction_freeSERCA);

            ListView.LVDispValue("SR", IxCasr_Min, ref Casr_Min);
            ListView.LVDispValue("SR", IxCasr_Max, ref Casr_Max);

            SERCA.DispValues(ref Lf, ref myTVc);
            RyR.DispValues(ref Lf, ref myTVc);
            Leak.DispValues(ref Lf, ref myTVc);
            Calsequestrin.DispValues(ref Lf, ref myTVc);
        }
        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpSR_ListView;
            ListView.LVModiValue("SR", IxVsr, ref Vsr);
            ListView.LVModiValue("SR", IxVsr_part, ref Vsr_part);
            ListView.LVModiValue("SR", IxCasr, ref myTVc[Pd.IdxCasr]);
            ListView.LVModiValue("SR", Ixfraction_freeSERCA, ref fraction_freeSERCA);

            ListView.LVModiValue("SR", IxCasr_Min, ref Casr_Min);
            ListView.LVModiValue("SR", IxCasr_Max, ref Casr_Max);

            SERCA.ModiValues(ref Lf, ref myTVc);
            RyR.ModiValues(ref Lf, ref myTVc);
            Leak.ModiValues(ref Lf, ref myTVc);
            Calsequestrin.ModiValues(ref Lf, ref myTVc);
        }
    }
}
