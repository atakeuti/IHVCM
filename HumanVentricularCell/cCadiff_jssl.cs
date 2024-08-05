using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cCadiff_jssl : cElement
    {
        private Pd.TIdx IxJ_Cadif_jssl;
        private Pd.TIdx IxpermeabilityCa_jssl;

        public double J_Cadif_jssl;
        public double permeabilityCa_jssl = 1.1 * 824.13; //  = 906.543 um^3/ms; permeability for Ca diffusion JS to SL (8.2413e-13 L/ms x 10^15)

        public cCadiff_jssl()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxpermeabilityCa_jssl, ref i, Pd.IntPar, "permeabilityCa_jssl");
            SetIx(ref IxJ_Cadif_jssl, ref i, Pd.IntVar, "J_Cadif_jssl");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpCadiff_jssl_ListView, NumOfIx);
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            J_Cadif_jssl = permeabilityCa_jssl * (tvY[Pd.IdxCasl] - tvY[Pd.IdxCajs]);
            myCell.TVc[Pd.InxJ_Cadiff_jssl] = J_Cadif_jssl;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpCadiff_jssl_ListView;
            ListView.LVDispValue("Cadiff_jssl", IxpermeabilityCa_jssl, ref permeabilityCa_jssl);
            ListView.LVDispValue("Cadiff_jssl", IxJ_Cadif_jssl, ref J_Cadif_jssl);
        }
        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpCadiff_jssl_ListView;
            ListView.LVModiValue("Cadiff_jssl", IxpermeabilityCa_jssl, ref permeabilityCa_jssl);
            ListView.LVModiValue("Cadiff_jssl", IxJ_Cadif_jssl, ref J_Cadif_jssl);
        }
    }
}
