using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cIpCajs : cElement
    {
        private Pd.TIdx IxApCa;
        private Pd.TIdx IxKmpCa;
        private Pd.TIdx IxdATPuse_IpCajs;

        public double ApCa = 0.0673; // pA/pF
        public double KmpCa = 5.0e-4; // mM
        public double dATPuse_IpCajs;

        public cIpCajs()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");
            SetIx(ref IxApCa, ref i, Pd.IntPar, "ApCa");
            SetIx(ref IxKmpCa, ref i, Pd.IntPar, "KmpCa");

            SetIx(ref IxdATPuse_IpCajs, ref i, Pd.IntVar, "dATPuse_IpCajs");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpIpCajs_ListView, NumOfIx);

            Itc = myTVc[Pd.InxIpCajs_Itc];

            dATPuse_IpCajs = myTVc[Pd.InxdATPuse_IpCajs];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Ca_ = Math.Pow(tvY[Pd.IdxCajs], 1.6);
            Itc = SF * myCell.JS.fraction_js * ApCa * Ca_ / (Math.Pow(KmpCa, 1.6) + Ca_);
            myCell.TVc[Pd.InxIpCajs_Itc] = Itc;

            dATPuse_IpCajs = Itc * myCell.Cm / (myCell.Vi_diff * Pd.F); // 1Ca2+ : 1H+ : 1ATP
            myCell.TVc[Pd.InxdATPuse_IpCajs] = dATPuse_IpCajs;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIpCajs_ListView;
            ListView.LVDispValue("IpCajs", IxSF, ref SF);
            ListView.LVDispValue("IpCajs", IxItc, ref Itc);
            ListView.LVDispValue("IpCajs", IxApCa, ref ApCa);
            ListView.LVDispValue("IpCajs", IxKmpCa, ref KmpCa);

            ListView.LVDispValue("IpCajs", IxdATPuse_IpCajs, ref dATPuse_IpCajs);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIpCajs_ListView;
            ListView.LVModiValue("IpCajs", IxSF, ref SF);
            ListView.LVModiValue("IpCajs", IxItc, ref Itc);
            ListView.LVModiValue("IpCajs", IxApCa, ref ApCa);
            ListView.LVModiValue("IpCajs", IxKmpCa, ref KmpCa);

            ListView.LVModiValue("IpCajs", IxdATPuse_IpCajs, ref dATPuse_IpCajs);
        }
    }
}
