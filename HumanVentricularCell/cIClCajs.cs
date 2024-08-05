using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cIClCajs : cElement
    {
        private Pd.TIdx IxgClCa;
        private Pd.TIdx IxKdClCa;

        public double gClCa = 0.0548125; // nS/pF
        public double KdClCa = 0.1; // mM

        public cIClCajs()  //Constructor
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

            Lf.DGViews_Initialize(Lf.tpIClCajs_ListView, NumOfIx);

            Itc = myTVc[Pd.InxIClCajs_Itc];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            Itc = SF * myCell.JS.fraction_js * gClCa * (tvY[Pd.IdxVm] - myCell.Rp.Cl) / (1 + KdClCa / tvY[Pd.IdxCajs]);
            myCell.TVc[Pd.InxIClCajs_Itc] = Itc;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIClCajs_ListView;
            ListView.LVDispValue("IClCajs", IxSF, ref SF);
            ListView.LVDispValue("IClCajs", IxItc, ref Itc);
            ListView.LVDispValue("IClCajs", IxgClCa, ref gClCa);
            ListView.LVDispValue("IClCajs", IxKdClCa, ref KdClCa);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpIClCajs_ListView;
            ListView.LVModiValue("IClCajs", IxSF, ref SF);
            ListView.LVModiValue("IClCajs", IxItc, ref Itc);
            ListView.LVModiValue("IClCajs", IxgClCa, ref gClCa);
            ListView.LVModiValue("IClCajs", IxKdClCa, ref KdClCa);
        }
    }
}
