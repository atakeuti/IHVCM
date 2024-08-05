using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cCell : cElement
    {
        private Pd.TIdx IxCm;
        private Pd.TIdx IxVcell;
        private Pd.TIdx IxVi_diff;
        private Pd.TIdx IxVm;
        private Pd.TIdx IxVm_Max;
        private Pd.TIdx IxVm_Min;

        private Pd.TIdx IxINatot;
        private Pd.TIdx IxIKtot;
        private Pd.TIdx IxICatot;
        private Pd.TIdx IxICltot;

        private Pd.TIdx IxJ_cons;

        private Pd.TIdx IxsumATPuse_contraction;
        private Pd.TIdx IxsumATPuse_INaKjs;
        private Pd.TIdx IxsumATPuse_INaKsl;
        private Pd.TIdx IxsumATPuse_IpCajs;
        private Pd.TIdx IxsumATPuse_IpCasl;
        private Pd.TIdx IxsumATPuse_SERCA;
        private Pd.TIdx IxsumATPuse_NmSC;
        private Pd.TIdx IxsumVO2;

        public double Cm = 138.1;       // Cell capacitance (pF)
        public double Vcell = 33000;  // Cell volume (um^3)
        public double Vi_diff;  // diffusible cell volume (um^3)

        public double Vm;       // Membrane potential (mV)
        public double Vm_Max;       // Membrane potential (mV)
        public double Vm_Min;       // Membrane potential (mV)

        public double INatot;
        public double IKtot;
        public double ICatot;
        public double ICltot;

        public double J_cons; // mM/msec

        public double sumATPuse_contraction;
        public double sumATPuse_INaKjs;
        public double sumATPuse_INaKsl;
        public double sumATPuse_IpCajs;
        public double sumATPuse_IpCasl;
        public double sumATPuse_SERCA;
        public double sumATPuse_NmSC;
        public double sumVO2;

        public double meanCai;
        public bool BlnCamitFix = false;

        public int IntbARstimProtcol = 0; //0:control, 1:teststim, 2;Off
        public int IntELChangeProtcol = 0; //0:control, 1:teststim, 2;Off
        public int IntFreqChangeProtcol = 0; //0:control, 1:teststim, 2;Off

        public double sumJ_C1;
        public double sumJ_PDHC;
        public double sumJ_ICDH;
        public double sumJ_OGDH;
        public double sumJ_MDH;
        public double sumJ_DHall;

        public double Istim; // External Stimulation Amplitude (pA/pF)

        public Pd.TIons Rp;

        public Pd.TModelSetting MSet = new Pd.TModelSetting();
        public cExtCell ExtCell = new cExtCell();
        public cSR SR = new cSR();
        public cMitochondria Mitochondria = new cMitochondria();
        public cJS JS = new cJS();
        public cSL SL = new cSL();
        public cCyt Cyt = new cCyt();

        public cCadiff_cytsl Cadiff_cytsl = new cCadiff_cytsl();
        public cCadiff_jssl Cadiff_jssl = new cCadiff_jssl();
        public cNadiff_cytsl Nadiff_cytsl = new cNadiff_cytsl();
        public cNadiff_jssl Nadiff_jssl = new cNadiff_jssl();

        public cINajs INajs = new cINajs();
        public cINasl INasl = new cINasl();
        public cINabjs INabjs = new cINabjs();
        public cINabsl INabsl = new cINabsl();
        public cINaKjs INaKjs = new cINaKjs();
        public cINaKsl INaKsl = new cINaKsl();
        public cIKrjs IKrjs = new cIKrjs();
        public cIKrsl IKrsl = new cIKrsl();
        public cIKsjs IKsjs = new cIKsjs();
        public cIKssl IKssl = new cIKssl();
        public cIKpjs IKpjs = new cIKpjs();
        public cIKpsl IKpsl = new cIKpsl();
        public cIClCajs IClCajs = new cIClCajs();
        public cIClCasl IClCasl = new cIClCasl();
        public cICaLjs ICaLjs = new cICaLjs();
        public cICaLsl ICaLsl = new cICaLsl();
        public cINaCajs INaCajs = new cINaCajs();
        public cINaCasl INaCasl = new cINaCasl();
        public cIpCajs IpCajs = new cIpCajs();
        public cIpCasl IpCasl = new cIpCasl();
        public cICabjs ICabjs = new cICabjs();
        public cICabsl ICabsl = new cICabsl();
        public cItosjs Itosjs = new cItosjs();
        public cItossl Itossl = new cItossl();
        public cItofjs Itofjs = new cItofjs();
        public cItofsl Itofsl = new cItofsl();
        public cIK1js IK1js = new cIK1js();
        public cIK1sl IK1sl = new cIK1sl();
        public cIClbjs IClbjs = new cIClbjs();
        public cIClbsl IClbsl = new cIClbsl();

        public cContraction Contraction = new cContraction();

        public double[] TVc = new double[Pd.IdxLast];
        public string[] TVcStr = new string[Pd.IdxLast];
        public double[] tvDYdt = new double[Pd.IdxLast]; //dummy

        public Pd.mTV_VStr[] mTV;
        public Pd.mTV_VStr2[] mTVbeat;

        public cCell()  //Constructor
        {
            mTV = new Pd.mTV_VStr[Pd.mTVIdxMax];
            mTVbeat = new Pd.mTV_VStr2[Pd.mTVMax_beat];

            for (int i = 0; i <= (Pd.mTVIdxMax - 1); i++)
            {
                mTV[i] = new Pd.mTV_VStr();
            };

            for (int i = 0; i <= (Pd.mTVMax_beat - 1); i++)
            {
                mTVbeat[i] = new Pd.mTV_VStr2();
            };
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            Vm = myTVc[Pd.IdxVm];

            int i = 0;

            SetIx(ref IxCm, ref i, Pd.IntPar, "Cm");
            SetIx(ref IxVcell, ref i, Pd.IntPar, "Vcell");
            SetIx(ref IxVi_diff, ref i, Pd.IntPar, "Vi_diff");
            SetIx(ref IxVm, ref i, Pd.IntVar, "Vm");
            SetIx(ref IxINatot, ref i, Pd.IntVar, "INatot");
            SetIx(ref IxIKtot, ref i, Pd.IntVar, "IKtot");
            SetIx(ref IxICatot, ref i, Pd.IntVar, "ICatot");
            SetIx(ref IxICltot, ref i, Pd.IntVar, "ICltot");
            SetIx(ref IxVm_Max, ref i, Pd.IntVar, "Vm_Max");
            SetIx(ref IxVm_Min, ref i, Pd.IntVar, "Vm_Min");
            SetIx(ref IxJ_cons, ref i, Pd.IntVar, "J_cons");
            SetIx(ref IxsumATPuse_contraction, ref i, Pd.IntVar, "sumATPse_contraction");
            SetIx(ref IxsumATPuse_INaKjs, ref i, Pd.IntVar, "sumATPse_INaKjs");
            SetIx(ref IxsumATPuse_INaKsl, ref i, Pd.IntVar, "sumATPse_INaKsl");
            SetIx(ref IxsumATPuse_IpCajs, ref i, Pd.IntVar, "sumATPse_IpCajs");
            SetIx(ref IxsumATPuse_IpCasl, ref i, Pd.IntVar, "sumATPse_IpCasl");
            SetIx(ref IxsumATPuse_SERCA, ref i, Pd.IntVar, "sumATPse_SERCA");
            SetIx(ref IxsumATPuse_NmSC, ref i, Pd.IntVar, "sumATPse_NmSC");
            SetIx(ref IxsumVO2, ref i, Pd.IntVar, "sumVO2");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpCell_ListView, NumOfIx);
        }

        public void InitializeCell(ref cCell myCell, ref ListForm Lf)
        {
            Initialize(ref TVc, ref myCell, ref Lf);

            ExtCell.Initialize(ref TVc, ref myCell, ref Lf);
            SR.Initialize(ref TVc, ref myCell, ref Lf);
            Mitochondria.Initialize(ref TVc, ref myCell, ref Lf);

            JS.Initialize(ref TVc, ref myCell, ref Lf);
            SL.Initialize(ref TVc, ref myCell, ref Lf);
            Cyt.Initialize(ref TVc, ref myCell, ref Lf);

            Cadiff_cytsl.Initialize(ref TVc, ref myCell, ref Lf);
            Cadiff_jssl.Initialize(ref TVc, ref myCell, ref Lf);
            Nadiff_cytsl.Initialize(ref TVc, ref myCell, ref Lf);
            Nadiff_jssl.Initialize(ref TVc, ref myCell, ref Lf);

            INajs.Initialize(ref TVc, ref myCell, ref Lf);
            INasl.Initialize(ref TVc, ref myCell, ref Lf);
            INabjs.Initialize(ref TVc, ref myCell, ref Lf);
            INabsl.Initialize(ref TVc, ref myCell, ref Lf);
            INaKjs.Initialize(ref TVc, ref myCell, ref Lf);
            INaKsl.Initialize(ref TVc, ref myCell, ref Lf);
            IKrjs.Initialize(ref TVc, ref myCell, ref Lf);
            IKrsl.Initialize(ref TVc, ref myCell, ref Lf);
            IKsjs.Initialize(ref TVc, ref myCell, ref Lf);
            IKssl.Initialize(ref TVc, ref myCell, ref Lf);
            IKpjs.Initialize(ref TVc, ref myCell, ref Lf);
            IKpsl.Initialize(ref TVc, ref myCell, ref Lf);
            IClCajs.Initialize(ref TVc, ref myCell, ref Lf);
            IClCasl.Initialize(ref TVc, ref myCell, ref Lf);
            ICaLjs.Initialize(ref TVc, ref myCell, ref Lf);
            ICaLsl.Initialize(ref TVc, ref myCell, ref Lf);
            INaCajs.Initialize(ref TVc, ref myCell, ref Lf);
            INaCasl.Initialize(ref TVc, ref myCell, ref Lf);
            IpCajs.Initialize(ref TVc, ref myCell, ref Lf);
            IpCasl.Initialize(ref TVc, ref myCell, ref Lf);
            ICabjs.Initialize(ref TVc, ref myCell, ref Lf);
            ICabsl.Initialize(ref TVc, ref myCell, ref Lf);
            Itosjs.Initialize(ref TVc, ref myCell, ref Lf);
            Itossl.Initialize(ref TVc, ref myCell, ref Lf);
            Itofjs.Initialize(ref TVc, ref myCell, ref Lf);
            Itofsl.Initialize(ref TVc, ref myCell, ref Lf);
            IK1js.Initialize(ref TVc, ref myCell, ref Lf);
            IK1sl.Initialize(ref TVc, ref myCell, ref Lf);
            IClbjs.Initialize(ref TVc, ref myCell, ref Lf);
            IClbsl.Initialize(ref TVc, ref myCell, ref Lf);

            Contraction.Initialize(ref TVc, ref myCell, ref Lf);

            Vi_diff = myCell.Cyt.Vcyt + myCell.JS.Vjs + myCell.SL.Vsl;
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            ExtCell.dydt(dt, ref tvDYdt, ref tvY, myCell);
            SR.dydt(dt, ref tvDYdt, ref tvY, myCell);
            Mitochondria.dydt(dt, ref tvDYdt, ref tvY, myCell);

            JS.dydt(dt, ref tvDYdt, ref tvY, myCell);
            SL.dydt(dt, ref tvDYdt, ref tvY, myCell);
            Cyt.dydt(dt, ref tvDYdt, ref tvY, myCell);

            Cadiff_cytsl.dydt(dt, ref tvDYdt, ref tvY, myCell);
            Cadiff_jssl.dydt(dt, ref tvDYdt, ref tvY, myCell);
            Nadiff_cytsl.dydt(dt, ref tvDYdt, ref tvY, myCell);
            Nadiff_jssl.dydt(dt, ref tvDYdt, ref tvY, myCell);

            INajs.dydt(dt, ref tvDYdt, ref tvY, myCell);
            INasl.dydt(dt, ref tvDYdt, ref tvY, myCell);
            INabjs.dydt(dt, ref tvDYdt, ref tvY, myCell);
            INabsl.dydt(dt, ref tvDYdt, ref tvY, myCell);
            INaKjs.dydt(dt, ref tvDYdt, ref tvY, myCell);
            INaKsl.dydt(dt, ref tvDYdt, ref tvY, myCell);
            IKrjs.dydt(dt, ref tvDYdt, ref tvY, myCell);
            IKrsl.dydt(dt, ref tvDYdt, ref tvY, myCell);
            IKsjs.dydt(dt, ref tvDYdt, ref tvY, myCell);
            IKssl.dydt(dt, ref tvDYdt, ref tvY, myCell);
            IKpjs.dydt(dt, ref tvDYdt, ref tvY, myCell);
            IKpsl.dydt(dt, ref tvDYdt, ref tvY, myCell);
            IClCajs.dydt(dt, ref tvDYdt, ref tvY, myCell);
            IClCasl.dydt(dt, ref tvDYdt, ref tvY, myCell);
            ICaLjs.dydt(dt, ref tvDYdt, ref tvY, myCell);
            ICaLsl.dydt(dt, ref tvDYdt, ref tvY, myCell);
            INaCajs.dydt(dt, ref tvDYdt, ref tvY, myCell);
            INaCasl.dydt(dt, ref tvDYdt, ref tvY, myCell);
            IpCajs.dydt(dt, ref tvDYdt, ref tvY, myCell);
            IpCasl.dydt(dt, ref tvDYdt, ref tvY, myCell);
            ICabjs.dydt(dt, ref tvDYdt, ref tvY, myCell);
            ICabsl.dydt(dt, ref tvDYdt, ref tvY, myCell);
            Itosjs.dydt(dt, ref tvDYdt, ref tvY, myCell);
            Itossl.dydt(dt, ref tvDYdt, ref tvY, myCell);
            Itofjs.dydt(dt, ref tvDYdt, ref tvY, myCell);
            Itofsl.dydt(dt, ref tvDYdt, ref tvY, myCell);
            IK1js.dydt(dt, ref tvDYdt, ref tvY, myCell);
            IK1sl.dydt(dt, ref tvDYdt, ref tvY, myCell);
            IClbjs.dydt(dt, ref tvDYdt, ref tvY, myCell);
            IClbsl.dydt(dt, ref tvDYdt, ref tvY, myCell);

            Contraction.dydt(dt, ref tvDYdt, ref tvY, myCell);

            INatot = myCell.INajs.Itc + myCell.INasl.Itc + myCell.INabjs.Itc + myCell.INabsl.Itc + 3 * myCell.INaKjs.Itc + 3 * myCell.INaKsl.Itc + 3 * myCell.INaCajs.Itc + 3 * myCell.INaCasl.Itc + myCell.ICaLjs.ICaL_Na + myCell.ICaLsl.ICaL_Na;
            IKtot = myCell.IKrjs.Itc + myCell.IKrsl.Itc + myCell.IKsjs.Itc + myCell.IKssl.Itc + myCell.IKpjs.Itc + myCell.IKpsl.Itc + myCell.Itosjs.Itc + myCell.Itossl.Itc + myCell.Itofjs.Itc + myCell.Itofsl.Itc + myCell.IK1js.Itc + myCell.IK1sl.Itc + myCell.ICaLjs.ICaL_K + myCell.ICaLsl.ICaL_K - 2 * myCell.INaKjs.Itc - 2 * myCell.INaKsl.Itc;
            ICatot = myCell.ICaLjs.ICaL_Ca + myCell.ICaLsl.ICaL_Ca + myCell.IpCajs.Itc + myCell.IpCasl.Itc + myCell.ICabjs.Itc + myCell.ICabsl.Itc - 2 * myCell.INaCajs.Itc - 2 * myCell.INaCasl.Itc;
            ICltot = myCell.IClCajs.Itc + myCell.IClCasl.Itc + myCell.IClbjs.Itc + myCell.IClbsl.Itc;

            tvDYdt[Pd.IdxVm] = -(INatot + IKtot + ICatot + ICltot + Istim);

            J_cons = myCell.TVc[Pd.InxdATPuse_contraction] + myCell.TVc[Pd.InxdATPuse_INaKjs] + myCell.TVc[Pd.InxdATPuse_INaKsl]
                        + myCell.TVc[Pd.InxdATPuse_IpCajs] + myCell.TVc[Pd.InxdATPuse_IpCasl]
                        + myCell.TVc[Pd.InxdATPuse_SERCA] + myCell.TVc[Pd.InxdATPuse_NmSC];

            myCell.TVc[Pd.InxJ_cons] = J_cons;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpCell_ListView;
            ListView.LVDispValue("Cell", IxCm, ref Cm);
            ListView.LVDispValue("Cell", IxVcell, ref Vcell);
            ListView.LVDispValue("Cell", IxVi_diff, ref Vi_diff);
            ListView.LVDispValue("Cell", IxVm, ref Vm);
            ListView.LVDispValue("Cell", IxINatot, ref INatot);
            ListView.LVDispValue("Cell", IxIKtot, ref IKtot);
            ListView.LVDispValue("Cell", IxICatot, ref ICatot);
            ListView.LVDispValue("Cell", IxICltot, ref ICltot);
            ListView.LVDispValue("Cell", IxVm_Max, ref Vm_Max);
            ListView.LVDispValue("Cell", IxVm_Min, ref Vm_Min);
            ListView.LVDispValue("Cell", IxJ_cons, ref J_cons);
            ListView.LVDispValue("Cell", IxsumATPuse_contraction, ref sumATPuse_contraction);
            ListView.LVDispValue("Cell", IxsumATPuse_INaKjs, ref sumATPuse_INaKjs);
            ListView.LVDispValue("Cell", IxsumATPuse_INaKsl, ref sumATPuse_INaKsl);
            ListView.LVDispValue("Cell", IxsumATPuse_IpCajs, ref sumATPuse_IpCajs);
            ListView.LVDispValue("Cell", IxsumATPuse_IpCasl, ref sumATPuse_IpCasl);
            ListView.LVDispValue("Cell", IxsumATPuse_SERCA, ref sumATPuse_SERCA);
            ListView.LVDispValue("Cell", IxsumATPuse_NmSC, ref sumATPuse_NmSC);
            ListView.LVDispValue("Cell", IxsumVO2, ref sumVO2);

            ExtCell.DispValues(ref Lf, ref myTVc);
            SR.DispValues(ref Lf, ref myTVc);
            Mitochondria.DispValues(ref Lf, ref myTVc);

            JS.DispValues(ref Lf, ref myTVc);
            SL.DispValues(ref Lf, ref myTVc);
            Cyt.DispValues(ref Lf, ref myTVc);

            Cadiff_cytsl.DispValues(ref Lf, ref myTVc);
            Cadiff_jssl.DispValues(ref Lf, ref myTVc);
            Nadiff_cytsl.DispValues(ref Lf, ref myTVc);
            Nadiff_jssl.DispValues(ref Lf, ref myTVc);

            INajs.DispValues(ref Lf, ref myTVc);
            INasl.DispValues(ref Lf, ref myTVc);
            INabjs.DispValues(ref Lf, ref myTVc);
            INabsl.DispValues(ref Lf, ref myTVc);
            INaKjs.DispValues(ref Lf, ref myTVc);
            INaKsl.DispValues(ref Lf, ref myTVc);
            IKrjs.DispValues(ref Lf, ref myTVc);
            IKrsl.DispValues(ref Lf, ref myTVc);
            IKsjs.DispValues(ref Lf, ref myTVc);
            IKssl.DispValues(ref Lf, ref myTVc);
            IKpjs.DispValues(ref Lf, ref myTVc);
            IKpsl.DispValues(ref Lf, ref myTVc);
            IClCajs.DispValues(ref Lf, ref myTVc);
            IClCasl.DispValues(ref Lf, ref myTVc);
            ICaLjs.DispValues(ref Lf, ref myTVc);
            ICaLsl.DispValues(ref Lf, ref myTVc);
            INaCajs.DispValues(ref Lf, ref myTVc);
            INaCasl.DispValues(ref Lf, ref myTVc);
            IpCajs.DispValues(ref Lf, ref myTVc);
            IpCasl.DispValues(ref Lf, ref myTVc);
            ICabjs.DispValues(ref Lf, ref myTVc);
            ICabsl.DispValues(ref Lf, ref myTVc);
            Itosjs.DispValues(ref Lf, ref myTVc);
            Itossl.DispValues(ref Lf, ref myTVc);
            Itofjs.DispValues(ref Lf, ref myTVc);
            Itofsl.DispValues(ref Lf, ref myTVc);
            IK1js.DispValues(ref Lf, ref myTVc);
            IK1sl.DispValues(ref Lf, ref myTVc);
            IClbjs.DispValues(ref Lf, ref myTVc);
            IClbsl.DispValues(ref Lf, ref myTVc);

            Contraction.DispValues(ref Lf, ref myTVc);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpCell_ListView;
            ListView.LVModiValue("Cell", IxCm, ref Cm);
            ListView.LVModiValue("Cell", IxVcell, ref Vcell);
            ListView.LVModiValue("Cell", IxVi_diff, ref Vi_diff);
            ListView.LVModiValue("Cell", IxVm, ref Vm);
            ListView.LVModiValue("Cell", IxINatot, ref INatot);
            ListView.LVModiValue("Cell", IxIKtot, ref IKtot);
            ListView.LVModiValue("Cell", IxICatot, ref ICatot);
            ListView.LVModiValue("Cell", IxICltot, ref ICltot);
            ListView.LVModiValue("Cell", IxVm_Max, ref Vm_Max);
            ListView.LVModiValue("Cell", IxVm_Min, ref Vm_Min);
            ListView.LVModiValue("Cell", IxJ_cons, ref J_cons);
            ListView.LVModiValue("Cell", IxsumATPuse_contraction, ref sumATPuse_contraction);
            ListView.LVModiValue("Cell", IxsumATPuse_INaKjs, ref sumATPuse_INaKjs);
            ListView.LVModiValue("Cell", IxsumATPuse_INaKsl, ref sumATPuse_INaKsl);
            ListView.LVModiValue("Cell", IxsumATPuse_IpCajs, ref sumATPuse_IpCajs);
            ListView.LVModiValue("Cell", IxsumATPuse_IpCasl, ref sumATPuse_IpCasl);
            ListView.LVModiValue("Cell", IxsumATPuse_SERCA, ref sumATPuse_SERCA);
            ListView.LVModiValue("Cell", IxsumATPuse_NmSC, ref sumATPuse_NmSC);
            ListView.LVModiValue("Cell", IxsumVO2, ref sumVO2);

            ExtCell.ModiValues(ref Lf, ref myTVc);
            SR.ModiValues(ref Lf, ref myTVc);
            Mitochondria.ModiValues(ref Lf, ref myTVc);

            JS.ModiValues(ref Lf, ref myTVc);
            SL.ModiValues(ref Lf, ref myTVc);
            Cyt.ModiValues(ref Lf, ref myTVc);

            Cadiff_cytsl.ModiValues(ref Lf, ref myTVc);
            Cadiff_jssl.ModiValues(ref Lf, ref myTVc);
            Nadiff_cytsl.ModiValues(ref Lf, ref myTVc);
            Nadiff_jssl.ModiValues(ref Lf, ref myTVc);

            INajs.ModiValues(ref Lf, ref myTVc);
            INasl.ModiValues(ref Lf, ref myTVc);
            INabjs.ModiValues(ref Lf, ref myTVc);
            INabsl.ModiValues(ref Lf, ref myTVc);
            INaKjs.ModiValues(ref Lf, ref myTVc);
            INaKsl.ModiValues(ref Lf, ref myTVc);
            IKrjs.ModiValues(ref Lf, ref myTVc);
            IKrsl.ModiValues(ref Lf, ref myTVc);
            IKsjs.ModiValues(ref Lf, ref myTVc);
            IKssl.ModiValues(ref Lf, ref myTVc);
            IKpjs.ModiValues(ref Lf, ref myTVc);
            IKpsl.ModiValues(ref Lf, ref myTVc);
            IClCajs.ModiValues(ref Lf, ref myTVc);
            IClCasl.ModiValues(ref Lf, ref myTVc);
            ICaLjs.ModiValues(ref Lf, ref myTVc);
            ICaLsl.ModiValues(ref Lf, ref myTVc);
            INaCajs.ModiValues(ref Lf, ref myTVc);
            INaCasl.ModiValues(ref Lf, ref myTVc);
            IpCajs.ModiValues(ref Lf, ref myTVc);
            IpCasl.ModiValues(ref Lf, ref myTVc);
            ICabjs.ModiValues(ref Lf, ref myTVc);
            ICabsl.ModiValues(ref Lf, ref myTVc);
            Itosjs.ModiValues(ref Lf, ref myTVc);
            Itossl.ModiValues(ref Lf, ref myTVc);
            Itofjs.ModiValues(ref Lf, ref myTVc);
            Itofsl.ModiValues(ref Lf, ref myTVc);
            IK1js.ModiValues(ref Lf, ref myTVc);
            IK1sl.ModiValues(ref Lf, ref myTVc);
            IClbjs.ModiValues(ref Lf, ref myTVc);
            IClbsl.ModiValues(ref Lf, ref myTVc);

            Contraction.ModiValues(ref Lf, ref myTVc);
        }

        protected void ConstantField(ref double[] tvaY)
        {
            double RTF = Pd.RTF;
            double RTF2 = Pd.RTF2;

            //--------------- reversal potential---------------------  
            Rp.Najs = RTF * Math.Log(ExtCell.Nao / tvaY[Pd.IdxNajs]);
            Rp.Nasl = RTF * Math.Log(ExtCell.Nao / tvaY[Pd.IdxNasl]);
            Rp.K = RTF * Math.Log(ExtCell.Ko / tvaY[Pd.IdxKi]);
            Rp.Cajs = RTF2 * Math.Log(ExtCell.Cao / tvaY[Pd.IdxCajs]);
            Rp.Casl = RTF2 * Math.Log(ExtCell.Cao / tvaY[Pd.IdxCasl]);
            Rp.Cl = RTF * Math.Log(tvaY[Pd.IdxCli] / ExtCell.Clo);

            tvaY[Pd.InxRpNajs] = Rp.Najs;
            tvaY[Pd.InxRpNasl] = Rp.Nasl;
            tvaY[Pd.InxRpK] = Rp.K;
            tvaY[Pd.InxRpCajs] = Rp.Cajs;
            tvaY[Pd.InxRpCasl] = Rp.Casl;
            tvaY[Pd.InxRpCl] = Rp.Cl;
        }

        public void Integrate(ref double dt, ref double[] mytvY, cCell myCell)
        {
            ConstantField(ref mytvY);
            RungeKutta.rkqc(ref dt, ref mytvY, myCell);
           
        }
    }
}
