using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cNadiff_jssl : cElement
    {
        private Pd.TIdx IxpermeabilityNa_jssl;
        private Pd.TIdx IxJ_Nadif_jssl;

        public double permeabilityNa_jssl = 18.3128; //  um^3/ms; permeability for Na diffusion JS to SL (1.83128e-14 L/ms x 10^15)
        public double J_Nadif_jssl;

        public cNadiff_jssl()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxpermeabilityNa_jssl, ref i, Pd.IntPar, "permeabilityNa_jssl");
            SetIx(ref IxJ_Nadif_jssl, ref i, Pd.IntVar, "J_Nadif_jssl");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpNadiff_jssl_ListView, NumOfIx);
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            J_Nadif_jssl = permeabilityNa_jssl * (tvY[Pd.IdxNasl] - tvY[Pd.IdxNajs]);

            myCell.TVc[Pd.InxJ_Nadiff_jssl] = J_Nadif_jssl;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpNadiff_jssl_ListView;
            ListView.LVDispValue("Nadiff_jssl", IxpermeabilityNa_jssl, ref permeabilityNa_jssl);
            ListView.LVDispValue("Nadiff_jssl", IxJ_Nadif_jssl, ref J_Nadif_jssl);
        }
        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpNadiff_jssl_ListView;
            ListView.LVModiValue("Nadiff_jssl", IxpermeabilityNa_jssl, ref permeabilityNa_jssl);
            ListView.LVModiValue("Nadiff_jssl", IxJ_Nadif_jssl, ref J_Nadif_jssl);
        }
    }
}
