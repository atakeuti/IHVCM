using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cCabuffcyt : cElement
    {
        // TnClow part in Grandi model was removed because TnClow is used in cContraction

        private Pd.TIdx IxbTnChCa;
        private Pd.TIdx IxkonTnChCa;
        private Pd.TIdx IxkoffTnChCa;
        private Pd.TIdx IxbTnChMg;
        private Pd.TIdx IxkonTnChMg;
        private Pd.TIdx IxkoffTnChMg;
        private Pd.TIdx IxTnChightot;

        private Pd.TIdx IxbCaM;
        private Pd.TIdx IxkonCaM;
        private Pd.TIdx IxkoffCaM;
        private Pd.TIdx IxCaMtot;

        private Pd.TIdx IxbMyocinCa;
        private Pd.TIdx IxkonMyocinCa;
        private Pd.TIdx IxkoffMyocinCa;
        private Pd.TIdx IxbMyocinMg;
        private Pd.TIdx IxkonMyocinMg;
        private Pd.TIdx IxkoffMyocinMg;
        private Pd.TIdx IxMyocintot;

        private Pd.TIdx IxbSRB;
        private Pd.TIdx IxkonSRB;
        private Pd.TIdx IxkoffSRB;
        private Pd.TIdx IxSRBtot;

        private Pd.TIdx IxJ_Cabuff_cyt_tot;

        public double bTnChCa;  // Ca bound troponinC high affinity site in cytoplasm
        public double konTnChCa = 2.37; // /mM*msec
        public double koffTnChCa = 3.2E-5; // /msec
        public double bTnChMg;  // Mg bound troponinC high affinity site in cytoplasm
        public double konTnChMg = 0.003; // /mM*msec
        public double koffTnChMg = 0.00333; // /msec
        public double TnChightot = 0.14; // mM
        public double bCaM;  // bound CaM in cytoplasm
        public double konCaM = 34.0; // /mM*msec
        public double koffCaM = 0.238; // /msec
        public double CaMtot = 0.024; // mM

        public double bMyocinCa;  // Ca bound Myocin in cytoplasm
        public double konMyocinCa = 13.8; // /mM*msec
        public double koffMyocinCa = 4.6E-4; // /msec
        public double bMyocinMg;  // Mg bound Myocin in cytoplasm
        public double konMyocinMg = 0.0157; // /mM*msec
        public double koffMyocinMg = 5.7E-5; // /msec
        public double Myocintot = 0.14; // mM

        public double bSRB;  // bound SRB in cytoplasm
        public double konSRB = 100.0; // /mM*msec
        public double koffSRB = 0.06; // /msec
        public double SRBtot = 0.0171; // mM

        public double J_Cabuff_cyt_tot;

        public cCabuffcyt()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxbTnChCa, ref i, Pd.IntVar, "bTnChCa");
            SetIx(ref IxkonTnChCa, ref i, Pd.IntPar, "konTnChCa");
            SetIx(ref IxkoffTnChCa, ref i, Pd.IntPar, "koffTnChCa");
            SetIx(ref IxbTnChMg, ref i, Pd.IntVar, "bTnChMg");
            SetIx(ref IxkonTnChMg, ref i, Pd.IntPar, "konTnChMg");
            SetIx(ref IxkoffTnChMg, ref i, Pd.IntPar, "koffTnChMg");
            SetIx(ref IxTnChightot, ref i, Pd.IntPar, "TnChightot");

            SetIx(ref IxbCaM, ref i, Pd.IntVar, "bCaM");
            SetIx(ref IxkonCaM, ref i, Pd.IntPar, "konCaM");
            SetIx(ref IxkoffCaM, ref i, Pd.IntPar, "koffCaM");
            SetIx(ref IxCaMtot, ref i, Pd.IntPar, "CaMtot");

            SetIx(ref IxbMyocinCa, ref i, Pd.IntVar, "bMyocinCa");
            SetIx(ref IxkonMyocinCa, ref i, Pd.IntPar, "konMyocinCa");
            SetIx(ref IxkoffMyocinCa, ref i, Pd.IntPar, "koffMyocinCa");
            SetIx(ref IxbMyocinMg, ref i, Pd.IntVar, "bMyocinMg");
            SetIx(ref IxkonMyocinMg, ref i, Pd.IntPar, "konMyocinMg");
            SetIx(ref IxkoffMyocinMg, ref i, Pd.IntPar, "koffMyocinMg");
            SetIx(ref IxMyocintot, ref i, Pd.IntPar, "Myocintot");

            SetIx(ref IxbSRB, ref i, Pd.IntVar, "bSRB");
            SetIx(ref IxkonSRB, ref i, Pd.IntPar, "konSRB");
            SetIx(ref IxkoffSRB, ref i, Pd.IntPar, "koffSRB");
            SetIx(ref IxSRBtot, ref i, Pd.IntPar, "SRBtot");

            SetIx(ref IxJ_Cabuff_cyt_tot, ref i, Pd.IntVar, "J_Cabuff_cyt_tot");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpCabuffcyt_ListView, NumOfIx);

            bTnChCa = myTVc[Pd.IdxbTnChCa];
            bTnChMg = myTVc[Pd.IdxbTnChMg];
            bCaM = myTVc[Pd.IdxbCaM];
            bMyocinCa = myTVc[Pd.IdxbMyocinCa];
            bMyocinMg = myTVc[Pd.IdxbMyocinMg];
            bSRB = myTVc[Pd.IdxbSRB];

            J_Cabuff_cyt_tot = myTVc[Pd.InxJ_Cabuff_cyt_tot];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Cai = tvY[Pd.IdxCai];
            double Mgi = myCell.Cyt.Mgi;

            bTnChCa = tvY[Pd.IdxbTnChCa];
            bTnChMg = tvY[Pd.IdxbTnChMg];
            bCaM = tvY[Pd.IdxbCaM];
            bMyocinCa = tvY[Pd.IdxbMyocinCa];
            bMyocinMg = tvY[Pd.IdxbMyocinMg];
            bSRB = tvY[Pd.IdxbSRB];

            tvDYdt[Pd.IdxbTnChCa] = konTnChCa * Cai * (TnChightot - bTnChCa - bTnChMg) - koffTnChCa * bTnChCa;
            tvDYdt[Pd.IdxbTnChMg] = konTnChMg * Mgi * (TnChightot - bTnChCa - bTnChMg) - koffTnChMg * bTnChMg;
            tvDYdt[Pd.IdxbCaM] = konCaM * Cai * (CaMtot - bCaM) - koffCaM * bCaM;
            tvDYdt[Pd.IdxbMyocinCa] = konMyocinCa * Cai * (Myocintot - bMyocinCa - bMyocinMg) - koffMyocinCa * bMyocinCa;
            tvDYdt[Pd.IdxbMyocinMg] = konMyocinMg * Mgi * (Myocintot - bMyocinCa - bMyocinMg) - koffMyocinMg * bMyocinMg;
            tvDYdt[Pd.IdxbSRB] = konSRB * Cai * (SRBtot - bSRB) - koffSRB * bSRB;

            J_Cabuff_cyt_tot = tvDYdt[Pd.IdxbTnChCa] + tvDYdt[Pd.IdxbTnChMg] + tvDYdt[Pd.IdxbCaM] + tvDYdt[Pd.IdxbMyocinCa] + tvDYdt[Pd.IdxbMyocinMg] + tvDYdt[Pd.IdxbSRB];
            myCell.TVc[Pd.InxJ_Cabuff_cyt_tot] = J_Cabuff_cyt_tot;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpCabuffcyt_ListView;

            ListView.LVDispValue("Cabuffcyt", IxbTnChCa, ref myTVc[Pd.IdxbTnChCa]);
            ListView.LVDispValue("Cabuffcyt", IxkonTnChCa, ref konTnChCa);
            ListView.LVDispValue("Cabuffcyt", IxkoffTnChCa, ref koffTnChCa);
            ListView.LVDispValue("Cabuffcyt", IxbTnChMg, ref myTVc[Pd.IdxbTnChMg]);
            ListView.LVDispValue("Cabuffcyt", IxkonTnChMg, ref konTnChMg);
            ListView.LVDispValue("Cabuffcyt", IxkoffTnChMg, ref koffTnChMg);
            ListView.LVDispValue("Cabuffcyt", IxTnChightot, ref TnChightot);

            ListView.LVDispValue("Cabuffcyt", IxbCaM, ref myTVc[Pd.IdxbCaM]);
            ListView.LVDispValue("Cabuffcyt", IxkonCaM, ref konCaM);
            ListView.LVDispValue("Cabuffcyt", IxkoffCaM, ref koffCaM);
            ListView.LVDispValue("Cabuffcyt", IxCaMtot, ref CaMtot);

            ListView.LVDispValue("Cabuffcyt", IxbMyocinCa, ref myTVc[Pd.IdxbMyocinCa]);
            ListView.LVDispValue("Cabuffcyt", IxkonMyocinCa, ref konMyocinCa);
            ListView.LVDispValue("Cabuffcyt", IxkoffMyocinCa, ref koffMyocinCa);
            ListView.LVDispValue("Cabuffcyt", IxbMyocinMg, ref myTVc[Pd.IdxbMyocinMg]);
            ListView.LVDispValue("Cabuffcyt", IxkonMyocinMg, ref konMyocinMg);
            ListView.LVDispValue("Cabuffcyt", IxkoffMyocinMg, ref koffMyocinMg);
            ListView.LVDispValue("Cabuffcyt", IxMyocintot, ref Myocintot);

            ListView.LVDispValue("Cabuffcyt", IxbSRB, ref myTVc[Pd.IdxbSRB]);
            ListView.LVDispValue("Cabuffcyt", IxkonSRB, ref konSRB);
            ListView.LVDispValue("Cabuffcyt", IxkoffSRB, ref koffSRB);
            ListView.LVDispValue("Cabuffcyt", IxSRBtot, ref SRBtot);

            ListView.LVDispValue("Cabuffcyt", IxJ_Cabuff_cyt_tot, ref J_Cabuff_cyt_tot);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpCabuffcyt_ListView;

            ListView.LVModiValue("Cabuffcyt", IxbTnChCa, ref myTVc[Pd.IdxbTnChCa]);
            ListView.LVModiValue("Cabuffcyt", IxkonTnChCa, ref konTnChCa);
            ListView.LVModiValue("Cabuffcyt", IxkoffTnChCa, ref koffTnChCa);
            ListView.LVModiValue("Cabuffcyt", IxbTnChMg, ref myTVc[Pd.IdxbTnChMg]);
            ListView.LVModiValue("Cabuffcyt", IxkonTnChMg, ref konTnChMg);
            ListView.LVModiValue("Cabuffcyt", IxkoffTnChMg, ref koffTnChMg);
            ListView.LVModiValue("Cabuffcyt", IxTnChightot, ref TnChightot);

            ListView.LVModiValue("Cabuffcyt", IxbCaM, ref myTVc[Pd.IdxbCaM]);
            ListView.LVModiValue("Cabuffcyt", IxkonCaM, ref konCaM);
            ListView.LVModiValue("Cabuffcyt", IxkoffCaM, ref koffCaM);
            ListView.LVModiValue("Cabuffcyt", IxCaMtot, ref CaMtot);

            ListView.LVModiValue("Cabuffcyt", IxbMyocinCa, ref myTVc[Pd.IdxbMyocinCa]);
            ListView.LVModiValue("Cabuffcyt", IxkonMyocinCa, ref konMyocinCa);
            ListView.LVModiValue("Cabuffcyt", IxkoffMyocinCa, ref koffMyocinCa);
            ListView.LVModiValue("Cabuffcyt", IxbMyocinMg, ref myTVc[Pd.IdxbMyocinMg]);
            ListView.LVModiValue("Cabuffcyt", IxkonMyocinMg, ref konMyocinMg);
            ListView.LVModiValue("Cabuffcyt", IxkoffMyocinMg, ref koffMyocinMg);
            ListView.LVModiValue("Cabuffcyt", IxMyocintot, ref Myocintot);

            ListView.LVModiValue("Cabuffcyt", IxbSRB, ref myTVc[Pd.IdxbSRB]);
            ListView.LVModiValue("Cabuffcyt", IxkonSRB, ref konSRB);
            ListView.LVModiValue("Cabuffcyt", IxkoffSRB, ref koffSRB);
            ListView.LVModiValue("Cabuffcyt", IxSRBtot, ref SRBtot);

            ListView.LVModiValue("Cabuffcyt", IxJ_Cabuff_cyt_tot, ref J_Cabuff_cyt_tot);
        }
    }
}
