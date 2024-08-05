using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cRyR : cElement
    {
        private Pd.TIdx Ixks_RyR;
        private Pd.TIdx Ixec50sr;
        private Pd.TIdx IxkoCa;
        private Pd.TIdx Ixkom;
        private Pd.TIdx IxkiCa;
        private Pd.TIdx Ixkim;
        private Pd.TIdx IxMaxsr;
        private Pd.TIdx IxMinsr;
        private Pd.TIdx IxRyR_R;
        private Pd.TIdx IxRyR_O;
        private Pd.TIdx IxRyR_I;
        private Pd.TIdx IxJ_RyR;
        private Pd.TIdx IxF_RyR;

        public double ks_RyR = 1.5 * 25; // =37.5 /ms 

        public double ec50sr = 0.45;// mM
        public double koCa = 10; // mM2/ms
        public double kom = 0.06; // ms
        public double kiCa = 0.5;// /mM*ms
        public double kim = 0.005; // ms
        public double Maxsr = 15;
        public double Minsr = 1;
        public double RyR_R;
        public double RyR_O;
        public double RyR_I;
        public double J_RyR;
        public double F_RyR;

        public bool BlnCProtRyROn = false;
        public double A_RyR = 1.0;

        public cRyR()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxJ_RyR, ref i, Pd.IntVar, "J_RyR");
            SetIx(ref IxF_RyR, ref i, Pd.IntVar, "F_RyR");

            SetIx(ref Ixks_RyR, ref i, Pd.IntPar, "ks_RyR");
            SetIx(ref Ixec50sr, ref i, Pd.IntPar, "ec50sr");
            SetIx(ref IxkoCa, ref i, Pd.IntPar, "koCa");
            SetIx(ref Ixkom, ref i, Pd.IntPar, "kom");
            SetIx(ref IxkiCa, ref i, Pd.IntPar, "kiCa");
            SetIx(ref Ixkim, ref i, Pd.IntPar, "kim");
            SetIx(ref IxMaxsr, ref i, Pd.IntPar, "Maxsr");
            SetIx(ref IxMinsr, ref i, Pd.IntPar, "Minsr");
            SetIx(ref IxRyR_R, ref i, Pd.IntVar, "RyR_R");
            SetIx(ref IxRyR_O, ref i, Pd.IntVar, "RyR_O");
            SetIx(ref IxRyR_I, ref i, Pd.IntVar, "RyR_I");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpRyR_ListView, NumOfIx);

            RyR_R = myTVc[Pd.IdxRyR_R];
            RyR_O = myTVc[Pd.IdxRyR_O];
            RyR_I = myTVc[Pd.IdxRyR_I];
            J_RyR = myTVc[Pd.InxJ_RyR];
            F_RyR = myTVc[Pd.InxF_RyR];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Cai = tvY[Pd.IdxCajs];
            double Casr = tvY[Pd.IdxCasr];
            double Cai2 = Cai * Cai;

            double kCaSR; // = Maxsr - (Maxsr - Minsr) / (1 + Math.Pow(ec50sr / Casr, 2.5));
            double koSR; // = koCa_ / kCaSR;
            double kiSR; // = kiCa * kCaSR;


            double koCa2 = A_RyR * koCa;
            double kom2 = kom / A_RyR;
            kCaSR = Maxsr - (Maxsr - Minsr) / (1 + Math.Pow(ec50sr / Casr, 2.5));
            koSR = koCa2 / kCaSR;
            kiSR = kiCa * kCaSR;

            RyR_R = tvY[Pd.IdxRyR_R];
            RyR_O = tvY[Pd.IdxRyR_O];
            RyR_I = tvY[Pd.IdxRyR_I];

            double RyR_RI = 1 - RyR_R - RyR_O - RyR_I;

            tvDYdt[Pd.IdxRyR_R] = (kim * RyR_RI - kiSR * Cai * RyR_R) - (koSR * Cai2 * RyR_R - kom2 * RyR_O);
            tvDYdt[Pd.IdxRyR_O] = (koSR * Cai2 * RyR_R - kom2 * RyR_O) - (kiSR * Cai * RyR_O - kim * RyR_I);
            tvDYdt[Pd.IdxRyR_I] = (kiSR * Cai * RyR_O - kim * RyR_I) - (kom2 * RyR_I - koSR * Cai2 * RyR_RI);

            J_RyR = SF * ks_RyR * RyR_O * (Casr - Cai);
            F_RyR = J_RyR * myCell.SR.Vsr;

            myCell.TVc[Pd.InxJ_RyR] = J_RyR;
            myCell.TVc[Pd.InxF_RyR] = F_RyR;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpRyR_ListView;
            ListView.LVDispValue("RyR", IxSF, ref SF);
            ListView.LVDispValue("RyR", IxJ_RyR, ref J_RyR);
            ListView.LVDispValue("RyR", IxF_RyR, ref F_RyR);

            ListView.LVDispValue("RyR", Ixks_RyR, ref ks_RyR);
            ListView.LVDispValue("RyR", Ixec50sr, ref ec50sr);
            ListView.LVDispValue("RyR", IxkoCa, ref koCa);
            ListView.LVDispValue("RyR", Ixkom, ref kom);
            ListView.LVDispValue("RyR", IxkiCa, ref kiCa);
            ListView.LVDispValue("RyR", Ixkim, ref kim);
            ListView.LVDispValue("RyR", IxMaxsr, ref Maxsr);
            ListView.LVDispValue("RyR", IxMinsr, ref Minsr);
            ListView.LVDispValue("RyR", IxRyR_R, ref myTVc[Pd.IdxRyR_R]);
            ListView.LVDispValue("RyR", IxRyR_O, ref myTVc[Pd.IdxRyR_O]);
            ListView.LVDispValue("RyR", IxRyR_I, ref myTVc[Pd.IdxRyR_I]);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpRyR_ListView;
            ListView.LVModiValue("RyR", IxSF, ref SF);
            ListView.LVModiValue("RyR", IxJ_RyR, ref J_RyR);
            ListView.LVModiValue("RyR", IxF_RyR, ref F_RyR);

            ListView.LVModiValue("RyR", Ixks_RyR, ref ks_RyR);
            ListView.LVModiValue("RyR", Ixec50sr, ref ec50sr);
            ListView.LVModiValue("RyR", IxkoCa, ref koCa);
            ListView.LVModiValue("RyR", Ixkom, ref kom);
            ListView.LVModiValue("RyR", IxkiCa, ref kiCa);
            ListView.LVModiValue("RyR", Ixkim, ref kim);
            ListView.LVModiValue("RyR", IxMaxsr, ref Maxsr);
            ListView.LVModiValue("RyR", IxMinsr, ref Minsr);
            ListView.LVModiValue("RyR", IxRyR_R, ref myTVc[Pd.IdxRyR_R]);
            ListView.LVModiValue("RyR", IxRyR_O, ref myTVc[Pd.IdxRyR_O]);
            ListView.LVModiValue("RyR", IxRyR_I, ref myTVc[Pd.IdxRyR_I]);
        }
    }
}
