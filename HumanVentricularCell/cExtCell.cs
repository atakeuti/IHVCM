using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cExtCell : cElement
    {
        private Pd.TIdx IxNao;      // extracellular Na concentration (mM)
        private Pd.TIdx IxKo;       // extracellular K concentration (mM)
        private Pd.TIdx IxCao;      // extracellular Ca concentration (mM)
        private Pd.TIdx IxClo;      // extracellular Cl concentration (mM)

        public double Nao;      // extracellular Na concentration (mM)
        public double Ko;       // extracellular K concentration (mM)
        public double Cao;      // extracellular Ca concentration (mM)
        public double Clo;      // extracellular Cl concentration (mM)

        public cExtCell()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxNao, ref i, Pd.IntPar, "Nao");
            SetIx(ref IxKo, ref i, Pd.IntPar, "Ko");
            SetIx(ref IxCao, ref i, Pd.IntPar, "Cao");
            SetIx(ref IxClo, ref i, Pd.IntPar, "Clo");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpExtCell_ListView, NumOfIx);

            Nao = 140.0;
            Ko = 5.4;
            Cao = 1.8;
            Clo = 150;
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpExtCell_ListView;
            ListView.LVDispValue("ExtCell", IxNao, ref Nao);
            ListView.LVDispValue("ExtCell", IxKo, ref Ko);
            ListView.LVDispValue("ExtCell", IxCao, ref Cao);
            ListView.LVDispValue("ExtCell", IxClo, ref Clo);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpExtCell_ListView;
            ListView.LVModiValue("ExtCell", IxNao, ref Nao);
            ListView.LVModiValue("ExtCell", IxKo, ref Ko);
            ListView.LVModiValue("ExtCell", IxCao, ref Cao);
            ListView.LVModiValue("ExtCell", IxClo, ref Clo);
        }
    }
}
