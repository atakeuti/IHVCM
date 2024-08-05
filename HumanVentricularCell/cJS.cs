using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cJS : cElement
    {
        public cCabuffjs Cabuffjs = new cCabuffjs();
        public cNabuffjs Nabuffjs = new cNabuffjs();

        private Pd.TIdx IxVjs;
        private Pd.TIdx IxVjs_part;
        private Pd.TIdx IxCajs;
        private Pd.TIdx IxNajs;
        private Pd.TIdx Ixfraction_js;
        private Pd.TIdx Ixfraction_jsCa;
        private Pd.TIdx IxNajs_Min;
        private Pd.TIdx IxNajs_Max;
        private Pd.TIdx IxCajs_Min;
        private Pd.TIdx IxCajs_Max;

        public double Vjs;
        public double Vjs_part = 5.39e-4; // part of cell volume occupied with junctional space
        public double Cajs;  // Ca concentration in junctional space (mM)
        public double Najs;  // Na concentration in junctional space (mM)
        public double fraction_js = 0.11;
        public double fraction_jsCa = 0.9;
        public double Najs_Min;
        public double Najs_Max;
        public double Cajs_Min;
        public double Cajs_Max;

        public cJS()  //Constructor 
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            Cajs = myTVc[Pd.IdxCajs];
            Najs = myTVc[Pd.IdxNajs];
            Vjs = Vjs_part * myCell.Vcell;

            Cabuffjs.Initialize(ref myTVc, ref myCell, ref Lf);
            Nabuffjs.Initialize(ref myTVc, ref myCell, ref Lf);

            int i = 0;

            SetIx(ref IxVjs, ref i, Pd.IntVar, "Vjs");
            SetIx(ref IxVjs_part, ref i, Pd.IntVar, "Vjs_part");
            SetIx(ref IxCajs, ref i, Pd.IntVar, "Cajs");
            SetIx(ref IxNajs, ref i, Pd.IntVar, "Najs");
            SetIx(ref Ixfraction_js, ref i, Pd.IntPar, "fraction_js");
            SetIx(ref Ixfraction_jsCa, ref i, Pd.IntPar, "fraction_jsCa");
            SetIx(ref IxNajs_Min, ref i, Pd.IntVar, "Najs_Min");
            SetIx(ref IxNajs_Max, ref i, Pd.IntVar, "Najs_Max");
            SetIx(ref IxCajs_Min, ref i, Pd.IntVar, "Cajs_Min");
            SetIx(ref IxCajs_Max, ref i, Pd.IntVar, "Cajs_Max");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpJS_ListView, NumOfIx);
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            Cabuffjs.dydt(dt, ref tvDYdt, ref tvY, myCell);
            Nabuffjs.dydt(dt, ref tvDYdt, ref tvY, myCell);

            tvDYdt[Pd.IdxCajs] = -(myCell.ICaLjs.ICaL_Ca + myCell.IpCajs.Itc + myCell.ICabjs.Itc - 2 * myCell.INaCajs.Itc) * (myCell.Cm / (Vjs * 2 * Pd.F))
                       + myCell.Cadiff_jssl.J_Cadif_jssl / Vjs
                       - Cabuffjs.J_Cabuff_js_tot
                       + (myCell.SR.RyR.F_RyR + myCell.SR.Leak.F_Leak - myCell.Mitochondria.F_CaUni_js) / Vjs;

            tvDYdt[Pd.IdxNajs] = -(myCell.INajs.Itc + myCell.INabjs.Itc + 3 * myCell.INaKjs.Itc + 3 * myCell.INaCajs.Itc + myCell.ICaLjs.ICaL_Na) * (myCell.Cm / (Vjs * Pd.F))
                                + myCell.Nadiff_jssl.J_Nadif_jssl / Vjs
                                - Nabuffjs.J_Nabuffjs;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpJS_ListView;
            ListView.LVDispValue("JS", IxVjs, ref Vjs);
            ListView.LVDispValue("JS", IxVjs_part, ref Vjs_part);
            ListView.LVDispValue("JS", IxCajs, ref myTVc[Pd.IdxCajs]);
            ListView.LVDispValue("JS", IxNajs, ref myTVc[Pd.IdxNajs]);

            ListView.LVDispValue("JS", Ixfraction_js, ref fraction_js);
            ListView.LVDispValue("JS", Ixfraction_jsCa, ref fraction_jsCa);
            ListView.LVDispValue("JS", IxNajs_Min, ref Najs_Min);
            ListView.LVDispValue("JS", IxNajs_Max, ref Najs_Max);
            ListView.LVDispValue("JS", IxCajs_Min, ref Cajs_Min);
            ListView.LVDispValue("JS", IxCajs_Max, ref Cajs_Max);

            Cabuffjs.DispValues(ref Lf, ref myTVc);
            Nabuffjs.DispValues(ref Lf, ref myTVc);
        }
        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpJS_ListView;
            ListView.LVModiValue("JS", IxVjs, ref Vjs);
            ListView.LVModiValue("JS", IxVjs_part, ref Vjs_part);
            ListView.LVModiValue("JS", IxCajs, ref myTVc[Pd.IdxCajs]);
            ListView.LVModiValue("JS", IxNajs, ref myTVc[Pd.IdxNajs]);

            ListView.LVModiValue("JS", Ixfraction_js, ref fraction_js);
            ListView.LVModiValue("JS", Ixfraction_jsCa, ref fraction_jsCa);
            ListView.LVModiValue("JS", IxNajs_Min, ref Najs_Min);
            ListView.LVModiValue("JS", IxNajs_Max, ref Najs_Max);
            ListView.LVModiValue("JS", IxCajs_Min, ref Cajs_Min);
            ListView.LVModiValue("JS", IxCajs_Max, ref Cajs_Max);

            Cabuffjs.ModiValues(ref Lf, ref myTVc);
            Nabuffjs.ModiValues(ref Lf, ref myTVc);
        }
    }
}
