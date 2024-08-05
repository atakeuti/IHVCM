using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cNabuffsl : cElement
    {
        private Pd.TIdx IxbNabuffsl;
        private Pd.TIdx Ixkonsl;
        private Pd.TIdx Ixkoffsl;
        private Pd.TIdx IxNabuffsl_tot;
        private Pd.TIdx IxJ_Nabuffsl;

        public double bNabuffsl;  // bound Na buffer in SL space
        public double konsl = 0.1e-3; // /mM*msec
        public double koffsl = 1e-3; // /msec
        public double Nabuffsl_tot = 1.65; // mM
        public double J_Nabuffsl;

        public cNabuffsl()  //Constructor 
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxbNabuffsl, ref i, Pd.IntVar, "bNabuffsl");
            SetIx(ref Ixkonsl, ref i, Pd.IntPar, "konsl");
            SetIx(ref Ixkoffsl, ref i, Pd.IntPar, "koffsl");
            SetIx(ref IxNabuffsl_tot, ref i, Pd.IntPar, "Nabuffsl_tot");
            SetIx(ref IxJ_Nabuffsl, ref i, Pd.IntVar, "J_Nabuffsl");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpNabuffsl_ListView, NumOfIx);

            bNabuffsl = myTVc[Pd.IdxbNabuffsl];
            J_Nabuffsl = myTVc[Pd.InxJ_Nabuffsl];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            tvDYdt[Pd.IdxbNabuffsl] = konsl * tvY[Pd.IdxNasl] * (Nabuffsl_tot - tvY[Pd.IdxbNabuffsl]) - koffsl * tvY[Pd.IdxbNabuffsl];
            J_Nabuffsl = tvDYdt[Pd.IdxbNabuffsl];

            myCell.TVc[Pd.InxJ_Nabuffsl] = J_Nabuffsl;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpNabuffsl_ListView;
            ListView.LVDispValue("Nabuffsl", IxbNabuffsl, ref myTVc[Pd.IdxbNabuffsl]);
            ListView.LVDispValue("Nabuffsl", Ixkonsl, ref konsl);
            ListView.LVDispValue("Nabuffsl", Ixkoffsl, ref koffsl);
            ListView.LVDispValue("Nabuffsl", IxNabuffsl_tot, ref Nabuffsl_tot);

            ListView.LVDispValue("Nabuffsl", IxJ_Nabuffsl, ref J_Nabuffsl);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpNabuffsl_ListView;
            ListView.LVModiValue("Nabuffsl", IxbNabuffsl, ref myTVc[Pd.IdxbNabuffsl]);
            ListView.LVModiValue("Nabuffsl", Ixkonsl, ref konsl);
            ListView.LVModiValue("Nabuffsl", Ixkoffsl, ref koffsl);
            ListView.LVModiValue("Nabuffsl", IxNabuffsl_tot, ref Nabuffsl_tot);

            ListView.LVModiValue("Nabuffsl", IxJ_Nabuffsl, ref J_Nabuffsl);
        }
    }
}
