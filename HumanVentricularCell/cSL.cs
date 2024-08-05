using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cSL : cElement
    {
        public cCabuffsl Cabuffsl = new cCabuffsl();
        public cNabuffsl Nabuffsl = new cNabuffsl();

        private Pd.TIdx IxVsl;
        private Pd.TIdx IxVsl_part;
        private Pd.TIdx IxCasl;
        private Pd.TIdx IxNasl;
        private Pd.TIdx Ixfraction_sl;
        private Pd.TIdx Ixfraction_slCa;
        private Pd.TIdx IxNasl_Min;
        private Pd.TIdx IxNasl_Max; 
        private Pd.TIdx IxCasl_Min;
        private Pd.TIdx IxCasl_Max;

        public double Vsl;
        public double Vsl_part = 0.02; // part of cell volume occupied with subsarcolemmal space
        public double Casl;  // Ca concentration in subsarcolemmal space (mM)
        public double Nasl;  // Na concentration in subsarcolemmal space (mM)

        public double fraction_sl = 0.89;
        public double fraction_slCa = 0.1;
        public double Nasl_Min;
        public double Nasl_Max;
        public double Casl_Min;
        public double Casl_Max;

        public cSL()  //Constructor 
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            Casl = myTVc[Pd.IdxCasl];
            Nasl = myTVc[Pd.IdxNasl];
            Vsl = Vsl_part * myCell.Vcell;

            Cabuffsl.Initialize(ref myTVc, ref myCell, ref Lf);
            Nabuffsl.Initialize(ref myTVc, ref myCell, ref Lf);

            int i = 0;

            SetIx(ref IxVsl, ref i, Pd.IntVar, "Vsl");
            SetIx(ref IxVsl_part, ref i, Pd.IntVar, "Vsl_part");
            SetIx(ref IxCasl, ref i, Pd.IntVar, "Casl");
            SetIx(ref IxNasl, ref i, Pd.IntVar, "Nasl");
            SetIx(ref Ixfraction_sl, ref i, Pd.IntPar, "fraction_sl");
            SetIx(ref Ixfraction_slCa, ref i, Pd.IntPar, "fraction_slCa");
            SetIx(ref IxNasl_Min, ref i, Pd.IntVar, "Nasl_Min");
            SetIx(ref IxNasl_Max, ref i, Pd.IntVar, "Nasl_Max"); 
            SetIx(ref IxCasl_Min, ref i, Pd.IntVar, "Casl_Min");
            SetIx(ref IxCasl_Max, ref i, Pd.IntVar, "Casl_Max");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpSL_ListView, NumOfIx);
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            Cabuffsl.dydt(dt, ref tvDYdt, ref tvY, myCell);
            Nabuffsl.dydt(dt, ref tvDYdt, ref tvY, myCell);
            tvDYdt[Pd.IdxCasl] = -(myCell.ICaLsl.ICaL_Ca + myCell.IpCasl.Itc + myCell.ICabsl.Itc - 2 * myCell.INaCasl.Itc) * (myCell.Cm / (Vsl * 2 * Pd.F))
                                    - myCell.Cadiff_jssl.J_Cadif_jssl / Vsl - myCell.Cadiff_cytsl.J_Cadif_cytsl / Vsl - Cabuffsl.J_Cabuff_sl_tot;

            tvDYdt[Pd.IdxNasl] = -(myCell.INasl.Itc + myCell.INabsl.Itc + 3 * myCell.INaKsl.Itc + 3 * myCell.INaCasl.Itc + myCell.ICaLsl.ICaL_Na) * (myCell.Cm / (Vsl * Pd.F))
                                    - myCell.Nadiff_jssl.J_Nadif_jssl / Vsl - myCell.Nadiff_cytsl.J_Nadif_cytsl / Vsl - Nabuffsl.J_Nabuffsl;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpSL_ListView;
            ListView.LVDispValue("SL", IxVsl, ref Vsl);
            ListView.LVDispValue("SL", IxVsl_part, ref Vsl_part);
            ListView.LVDispValue("SL", IxCasl, ref myTVc[Pd.IdxCasl]);
            ListView.LVDispValue("SL", IxNasl, ref myTVc[Pd.IdxNasl]);

            ListView.LVDispValue("SL", Ixfraction_sl, ref fraction_sl);
            ListView.LVDispValue("SL", Ixfraction_slCa, ref fraction_slCa);
            ListView.LVDispValue("SL", IxNasl_Min, ref Nasl_Min);
            ListView.LVDispValue("SL", IxNasl_Max, ref Nasl_Max);
            ListView.LVDispValue("SL", IxCasl_Min, ref Casl_Min);
            ListView.LVDispValue("SL", IxCasl_Max, ref Casl_Max);


            Cabuffsl.DispValues(ref Lf, ref myTVc);
            Nabuffsl.DispValues(ref Lf, ref myTVc);
        }
        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpSL_ListView;
            ListView.LVModiValue("SL", IxVsl, ref Vsl);
            ListView.LVModiValue("SL", IxVsl_part, ref Vsl_part);
            ListView.LVModiValue("SL", IxCasl, ref myTVc[Pd.IdxCasl]);
            ListView.LVModiValue("SL", IxNasl, ref myTVc[Pd.IdxNasl]);

            ListView.LVModiValue("SL", Ixfraction_sl, ref fraction_sl);
            ListView.LVModiValue("SL", Ixfraction_slCa, ref fraction_slCa);
            ListView.LVModiValue("SL", IxNasl_Min, ref Nasl_Min);
            ListView.LVModiValue("SL", IxNasl_Max, ref Nasl_Max);
            ListView.LVModiValue("SL", IxCasl_Min, ref Casl_Min);
            ListView.LVModiValue("SL", IxCasl_Max, ref Casl_Max);

            Cabuffsl.ModiValues(ref Lf, ref myTVc);
            Nabuffsl.ModiValues(ref Lf, ref myTVc);
        }
    }
}
