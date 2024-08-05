using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cIpCasl : cElement
    {
        private Pd.TIdx IxApCa;
        private Pd.TIdx IxKmpCa;
        private Pd.TIdx IxdATPuse_IpCasl;

        public double ApCa = 0.0673; // pA/pF
        public double KmpCa = 5.0e-4; // mM
        public double dATPuse_IpCasl;

        public cIpCasl()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");
            SetIx(ref IxApCa, ref i, Pd.IntPar, "ApCa");
            SetIx(ref IxKmpCa, ref i, Pd.IntPar, "KmpCa");

            SetIx(ref IxdATPuse_IpCasl, ref i, Pd.IntVar, "dATPuse_IpCasl");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpIpCasl_ListView, NumOfIx);

            Itc = myTVc[Pd.InxIpCasl_Itc];

            dATPuse_IpCasl = myTVc[Pd.InxdATPuse_IpCasl];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Ca_ = Math.Pow(tvY[Pd.IdxCasl], 1.6);
            Itc = SF * myCell.SL.fraction_sl * ApCa * Ca_ / (Math.Pow(KmpCa, 1.6) + Ca_);
            myCell.TVc[Pd.InxIpCasl_Itc] = Itc;

            dATPuse_IpCasl = Itc * myCell.Cm / (myCell.Vi_diff * Pd.F); // 1Ca2+ : 1H+ : 1ATP
            myCell.TVc[Pd.InxdATPuse_IpCasl] = dATPuse_IpCasl;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIpCasl_ListView;
            ListView.LVDispValue("IpCasl", IxSF, ref SF);
            ListView.LVDispValue("IpCasl", IxItc, ref Itc);
            ListView.LVDispValue("IpCasl", IxApCa, ref ApCa);
            ListView.LVDispValue("IpCasl", IxKmpCa, ref KmpCa);

            ListView.LVDispValue("IpCasl", IxdATPuse_IpCasl, ref dATPuse_IpCasl);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIpCasl_ListView;
            ListView.LVModiValue("IpCasl", IxSF, ref SF);
            ListView.LVModiValue("IpCasl", IxItc, ref Itc);
            ListView.LVModiValue("IpCasl", IxApCa, ref ApCa);
            ListView.LVModiValue("IpCasl", IxKmpCa, ref KmpCa);

            ListView.LVModiValue("IpCasl", IxdATPuse_IpCasl, ref dATPuse_IpCasl);
        }
    }
}
