using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cCadiff_cytsl : cElement
    {
        private Pd.TIdx IxpermeabilityCa_cytsl;
        private Pd.TIdx IxJ_Cadif_cytsl;

        public double permeabilityCa_cytsl = 1.1 * 3724.3; // = 4096.73 um^3/ms; permeability for Ca diffusion cytoplasm to SL (3.7243e-12 L/ms x 10^15) 

        public double J_Cadif_cytsl;

        public cCadiff_cytsl()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxpermeabilityCa_cytsl, ref i, Pd.IntPar, "permeabilityCa_cytsl");
            SetIx(ref IxJ_Cadif_cytsl, ref i, Pd.IntVar, "J_Cadif_cytsl");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpCadiff_cytsl_ListView, NumOfIx);
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            J_Cadif_cytsl = permeabilityCa_cytsl * (tvY[Pd.IdxCasl] - tvY[Pd.IdxCai]);
            myCell.TVc[Pd.InxJ_Cadiff_cytsl] = J_Cadif_cytsl;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpCadiff_cytsl_ListView;
            ListView.LVDispValue("Cadiff_cytsl", IxpermeabilityCa_cytsl, ref permeabilityCa_cytsl);
            ListView.LVDispValue("Cadiff_cytsl", IxJ_Cadif_cytsl, ref J_Cadif_cytsl);
        }
        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpCadiff_cytsl_ListView;
            ListView.LVModiValue("Cadiff_cytsl", IxpermeabilityCa_cytsl, ref permeabilityCa_cytsl);
            ListView.LVModiValue("Cadiff_cytsl", IxJ_Cadif_cytsl, ref J_Cadif_cytsl);
        }
    }
}
