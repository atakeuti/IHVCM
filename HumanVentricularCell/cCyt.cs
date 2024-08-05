using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cCyt : cElement
    {
        public cCabuffcyt Cabuffcyt = new cCabuffcyt();

        private Pd.TIdx IxNai;
        private Pd.TIdx IxKi;
        private Pd.TIdx IxCai;
        private Pd.TIdx IxCli;
        private Pd.TIdx IxMgi;

        private Pd.TIdx IxVcyt_part;
        private Pd.TIdx IxVcyt;

        private Pd.TIdx IxNai_Min;
        private Pd.TIdx IxNai_Max;
        private Pd.TIdx IxCai_Min;
        private Pd.TIdx IxCai_Max;

        public double Nai;  // intracellular Na concentration (mM)
        public double Ki;   // intracellular K concentration (mM)
        public double Cai;  // intracellular Ca concentration (mM)
        public double Cli;  // intracellular Cl concentration (mM)
        public double Mgi;  // intracellular Mg concentration (mM)

        public double Vcyt_part = 0.65; // part of cell volume occupied with cytoplasm
        public double Vcyt; // cytoplasm volume

        public double Nai_Min;
        public double Nai_Max;
        public double Cai_Min;
        public double Cai_Max;

        public cCyt()  //Constructor 
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            Nai = myTVc[Pd.IdxNai];
            Ki = myTVc[Pd.IdxKi];
            Cai = myTVc[Pd.IdxCai];
            Cli = myTVc[Pd.IdxCli];
            Mgi = 1.0; // mM

            Vcyt = Vcyt_part * myCell.Vcell;

            Cabuffcyt.Initialize(ref myTVc, ref myCell, ref Lf);

            int i = 0;

            SetIx(ref IxNai, ref i, Pd.IntVar, "Nai");
            SetIx(ref IxKi, ref i, Pd.IntVar, "Ki");
            SetIx(ref IxCai, ref i, Pd.IntVar, "Cai");
            SetIx(ref IxCli, ref i, Pd.IntVar, "Cli");
            SetIx(ref IxMgi, ref i, Pd.IntPar, "Mgi");
            SetIx(ref IxVcyt_part, ref i, Pd.IntPar, "Vcyt_part");
            SetIx(ref IxVcyt, ref i, Pd.IntVar, "Vcyt");
            SetIx(ref IxNai_Min, ref i, Pd.IntVar, "NaiMin");
            SetIx(ref IxNai_Max, ref i, Pd.IntVar, "NaiMax");
            SetIx(ref IxCai_Min, ref i, Pd.IntVar, "CaiMin");
            SetIx(ref IxCai_Max, ref i, Pd.IntVar, "CaiMax");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpCyt_ListView, NumOfIx);
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            Cabuffcyt.dydt(dt, ref tvDYdt, ref tvY, myCell);

            tvDYdt[Pd.IdxCai] = -(myCell.SR.SERCA.F_SERCA
                + myCell.Mitochondria.F_CaUni_cyt + myCell.Mitochondria.F_NmSC_block + myCell.Mitochondria.F_NCXmit + myCell.Mitochondria.F_HCXmit) / Vcyt
                - Cabuffcyt.J_Cabuff_cyt_tot
                + myCell.Contraction.J_Ca_contraction 
                + myCell.Cadiff_cytsl.J_Cadif_cytsl / Vcyt; 

            tvDYdt[Pd.IdxNai] = myCell.Nadiff_cytsl.J_Nadif_cytsl / Vcyt
                + (myCell.Mitochondria.n_NCXmit * (myCell.Mitochondria.F_NmSC + myCell.Mitochondria.F_NmSC_block + myCell.Mitochondria.F_NCXmit)
                + myCell.Mitochondria.J_NHE * myCell.Mitochondria.Vmit ) / Vcyt; 

            tvDYdt[Pd.IdxKi] = 0.0;
            tvDYdt[Pd.IdxCli] = 0.0;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpCyt_ListView;
            ListView.LVDispValue("Cyt", IxNai, ref myTVc[Pd.IdxNai]);
            ListView.LVDispValue("Cyt", IxKi, ref myTVc[Pd.IdxKi]);
            ListView.LVDispValue("Cyt", IxCai, ref myTVc[Pd.IdxCai]);
            ListView.LVDispValue("Cyt", IxCli, ref myTVc[Pd.IdxCli]);
            ListView.LVDispValue("Cyt", IxMgi, ref Mgi);

            ListView.LVDispValue("Cyt", IxVcyt_part, ref Vcyt_part);
            ListView.LVDispValue("Cyt", IxVcyt, ref Vcyt);
            ListView.LVDispValue("Cyt", IxNai_Min, ref Nai_Min);
            ListView.LVDispValue("Cyt", IxNai_Max, ref Nai_Max);
            ListView.LVDispValue("Cyt", IxCai_Min, ref Cai_Min);
            ListView.LVDispValue("Cyt", IxCai_Max, ref Cai_Max);

            Cabuffcyt.DispValues(ref Lf, ref myTVc);
        }
        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpCyt_ListView;
            ListView.LVModiValue("Cyt", IxNai, ref myTVc[Pd.IdxNai]);
            ListView.LVModiValue("Cyt", IxKi, ref myTVc[Pd.IdxKi]);
            ListView.LVModiValue("Cyt", IxCai, ref myTVc[Pd.IdxCai]);
            ListView.LVModiValue("Cyt", IxCli, ref myTVc[Pd.IdxCli]);
            ListView.LVModiValue("Cyt", IxMgi, ref Mgi);

            ListView.LVModiValue("Cyt", IxVcyt_part, ref Vcyt_part);
            ListView.LVModiValue("Cyt", IxVcyt, ref Vcyt);
            ListView.LVModiValue("Cyt", IxNai_Min, ref Nai_Min);
            ListView.LVModiValue("Cyt", IxNai_Max, ref Nai_Max);
            ListView.LVModiValue("Cyt", IxCai_Min, ref Cai_Min);
            ListView.LVModiValue("Cyt", IxCai_Max, ref Cai_Max);

            Cabuffcyt.ModiValues(ref Lf, ref myTVc);
        }
    }
}
