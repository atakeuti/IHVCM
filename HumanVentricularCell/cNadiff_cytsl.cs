using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cNadiff_cytsl : cElement
    {
        private Pd.TIdx IxpermeabilityNa_cytsl;
        private Pd.TIdx IxJ_Nadif_cytsl;

        public double permeabilityNa_cytsl = 1638.63; // um^3/ms; permeability for Na diffusion cytoplasm to SL (1.63863e-12 L/ms x 10^15) 
        public double J_Nadif_cytsl;

        public cNadiff_cytsl()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxpermeabilityNa_cytsl, ref i, Pd.IntPar, "permeabilityNa_cytsl");
            SetIx(ref IxJ_Nadif_cytsl, ref i, Pd.IntVar, "J_Nadif_cytsl");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpNadiff_cytsl_ListView, NumOfIx);
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            J_Nadif_cytsl = permeabilityNa_cytsl * (tvY[Pd.IdxNasl] - tvY[Pd.IdxNai]);

            myCell.TVc[Pd.InxJ_Nadiff_cytsl] = J_Nadif_cytsl;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpNadiff_cytsl_ListView;
            ListView.LVDispValue("Nadiff_cytsl", IxpermeabilityNa_cytsl, ref permeabilityNa_cytsl);
            ListView.LVDispValue("Nadiff_cytsl", IxJ_Nadif_cytsl, ref J_Nadif_cytsl);
        }
        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpNadiff_cytsl_ListView;
            ListView.LVModiValue("Nadiff_cytsl", IxpermeabilityNa_cytsl, ref permeabilityNa_cytsl);
            ListView.LVModiValue("Nadiff_cytsl", IxJ_Nadif_cytsl, ref J_Nadif_cytsl);
        }
    }
}
