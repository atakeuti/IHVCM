using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cIClCasl : cElement
    {
        private Pd.TIdx IxgClCa;
        private Pd.TIdx IxKdClCa;

        public double gClCa = 0.0548125; // nS/pF
        public double KdClCa = 0.1; // mM

        public cIClCasl()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxItc, ref i, Pd.IntVar, "Itc");
            SetIx(ref IxgClCa, ref i, Pd.IntPar, "gClCa");
            SetIx(ref IxKdClCa, ref i, Pd.IntPar, "KdClCa");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpIClCasl_ListView, NumOfIx);

            Itc = myTVc[Pd.InxIClCasl_Itc];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            Itc = SF * myCell.SL.fraction_sl * gClCa * (tvY[Pd.IdxVm] - myCell.Rp.Cl) / (1 + KdClCa / tvY[Pd.IdxCasl]);
            myCell.TVc[Pd.InxIClCasl_Itc] = Itc;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIClCasl_ListView;
            ListView.LVDispValue("IClCasl", IxSF, ref SF);
            ListView.LVDispValue("IClCasl", IxItc, ref Itc);
            ListView.LVDispValue("IClCasl", IxgClCa, ref gClCa);
            ListView.LVDispValue("IClCasl", IxKdClCa, ref KdClCa);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIClCasl_ListView;
            ListView.LVModiValue("IClCasl", IxSF, ref SF);
            ListView.LVModiValue("IClCasl", IxItc, ref Itc);
            ListView.LVModiValue("IClCasl", IxgClCa, ref gClCa);
            ListView.LVModiValue("IClCasl", IxKdClCa, ref KdClCa);
        }
    }
}
