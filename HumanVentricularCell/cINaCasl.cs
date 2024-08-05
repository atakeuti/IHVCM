using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cINaCasl : cElement
    {
        private Pd.TIdx IxKmCai;
        private Pd.TIdx IxKmCao;
        private Pd.TIdx IxKmNai;
        private Pd.TIdx IxKmNao;
        private Pd.TIdx IxKdact;
        private Pd.TIdx Ixksat;
        private Pd.TIdx Ixnu;
        private Pd.TIdx IxANCX;

        public double KmCai = 0.00359; // mM
        public double KmCao = 1.3; // mM
        public double KmNai = 12.29; // mM
        public double KmNao = 87.5; // mM
        public double Kdact = 1.5e-4; // mM
        public double ksat = 0.32; // 
        public double nu = 0.27; // mM
        public double ANCX =  4.5; // pA/pF


        public cINaCasl()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");
            SetIx(ref IxANCX, ref i, Pd.IntPar, "ANCX");

            SetIx(ref IxKmCai, ref i, Pd.IntPar, "KmCai");
            SetIx(ref IxKmCao, ref i, Pd.IntPar, "KmCao");
            SetIx(ref IxKmNai, ref i, Pd.IntPar, "KmNai");
            SetIx(ref IxKmNao, ref i, Pd.IntPar, "KmNao");
            SetIx(ref IxKdact, ref i, Pd.IntPar, "Kdact");
            SetIx(ref Ixksat, ref i, Pd.IntPar, "ksat");
            SetIx(ref Ixnu, ref i, Pd.IntPar, "nu");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpINaCasl_ListView, NumOfIx);

            Itc = myTVc[Pd.InxINaCasl_Itc];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Cao = myCell.ExtCell.Cao;
            double Nao = myCell.ExtCell.Nao;
            double Cai = tvY[Pd.IdxCasl];
            double Nai = tvY[Pd.IdxNasl];
            double VFRT = tvY[Pd.IdxVm] / Pd.RTF;
            double KdactOvCai = Kdact / Cai;
            double Ka = 1 / (1 + KdactOvCai * KdactOvCai);
            double Nai3 = Nai * Nai * Nai;
            double Nao3 = Nao * Nao * Nao;
            double s1 = Nai3 * Cao * Math.Exp(nu * VFRT);
            double Exp_nu_1_VFRT = Math.Exp((nu - 1) * VFRT);
            double s2 = Nao3 * Cai * Exp_nu_1_VFRT;
            double NaiOvKmNai = Nai / KmNai;
            double s3 = KmCai * Nao3 * (1 + NaiOvKmNai * NaiOvKmNai * NaiOvKmNai) + KmNao * KmNao * KmNao * Cai * (1 + Cai / KmCai) + KmCao * Nai3 + Nai3 * Cao + Nao3 * Cai;

            Itc = SF * myCell.SL.fraction_sl * ANCX * Ka * (s1 - s2) / (s3 * (1 + ksat * Exp_nu_1_VFRT));
            myCell.TVc[Pd.InxINaCasl_Itc] = Itc;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpINaCasl_ListView;
            ListView.LVDispValue("INaCajs", IxSF, ref SF);
            ListView.LVDispValue("INaCajs", IxItc, ref Itc);
            ListView.LVDispValue("INaCajs", IxANCX, ref ANCX);

            ListView.LVDispValue("INaCajs", IxKmCai, ref KmCai);
            ListView.LVDispValue("INaCajs", IxKmCao, ref KmCao);
            ListView.LVDispValue("INaCajs", IxKmNai, ref KmNai);
            ListView.LVDispValue("INaCajs", IxKmNao, ref KmNao);
            ListView.LVDispValue("INaCajs", IxKdact, ref Kdact);
            ListView.LVDispValue("INaCajs", Ixksat, ref ksat);
            ListView.LVDispValue("INaCajs", Ixnu, ref nu);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpINaCasl_ListView;
            ListView.LVModiValue("INaCajs", IxSF, ref SF);
            ListView.LVModiValue("INaCajs", IxItc, ref Itc);
            ListView.LVModiValue("INaCajs", IxANCX, ref ANCX);

            ListView.LVModiValue("INaCajs", IxKmCai, ref KmCai);
            ListView.LVModiValue("INaCajs", IxKmCao, ref KmCao);
            ListView.LVModiValue("INaCajs", IxKmNai, ref KmNai);
            ListView.LVModiValue("INaCajs", IxKmNao, ref KmNao);
            ListView.LVModiValue("INaCajs", IxKdact, ref Kdact);
            ListView.LVModiValue("INaCajs", Ixksat, ref ksat);
            ListView.LVModiValue("INaCajs", Ixnu, ref nu);
        }
    }
}
