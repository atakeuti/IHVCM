using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace HumanVentricularCell
{
    public class cMitochondria : cElement
    {
        private Pd.TIdx IxHcyt;
        private Pd.TIdx IxpHcyt;
        private Pd.TIdx IxATPiTotal;
        private Pd.TIdx IxATPiFree;
        private Pd.TIdx IxKd_ATPiMg;
        private Pd.TIdx IxATPiMg;
        private Pd.TIdx IxADPiTotal;
        private Pd.TIdx IxADPiFree;
        private Pd.TIdx IxKd_ADPiMg;
        private Pd.TIdx IxADPiMg;
        private Pd.TIdx IxAMPi;
        private Pd.TIdx IxAdenineTotali;
        private Pd.TIdx IxCreatineTotali;
        private Pd.TIdx IxCr;
        private Pd.TIdx IxPCr;
        private Pd.TIdx IxPIi;
        private Pd.TIdx IxC_Buffi;
        private Pd.TIdx IxR_Buffi;
        private Pd.TIdx IxMalatei;
        private Pd.TIdx IxGlutamatei;
        private Pd.TIdx IxAspartatei;
        private Pd.TIdx IxPyruvatei;
        private Pd.TIdx IxOxoglutaratei;
        private Pd.TIdx IxCitratei;

        private Pd.TIdx IxVmit_part;
        private Pd.TIdx IxVmit;
        private Pd.TIdx IxRmc;
        private Pd.TIdx IxHmit;
        private Pd.TIdx IxpHmit;
        private Pd.TIdx IxKmit;

        private Pd.TIdx IxCamitTot;

        private Pd.TIdx IxCamit;
        private Pd.TIdx IxCamit_Min;
        private Pd.TIdx IxCamit_Max;
        private Pd.TIdx IxNamit;
        private Pd.TIdx IxMgFreemit;
        private Pd.TIdx IxdP;
        private Pd.TIdx IxdPsi;
        private Pd.TIdx IxdpH;
        private Pd.TIdx IxCmit;
        private Pd.TIdx IxAdeninemitTotal;
        private Pd.TIdx IxKd_ATPmitMg;
        private Pd.TIdx IxATPmitTotal;
        private Pd.TIdx IxATPmitMg;
        private Pd.TIdx IxATPmitFree;
        private Pd.TIdx IxKd_ADPmitMg;
        private Pd.TIdx IxADPmitMg;
        private Pd.TIdx IxADPmitFree;
        private Pd.TIdx IxADPmitTotal;
        private Pd.TIdx IxGuanosinemitTotal;
        private Pd.TIdx IxGDPmit;
        private Pd.TIdx IxGTPmit;
        private Pd.TIdx IxPImit;
        private Pd.TIdx IxEm_NAD0;
        private Pd.TIdx IxEm_NAD;
        private Pd.TIdx IxNAD_Total;
        private Pd.TIdx IxNADH;
        private Pd.TIdx IxNAD;
        private Pd.TIdx IxR_NAD_to_NADH;
        private Pd.TIdx IxCytc_Total;
        private Pd.TIdx IxCytc_r;
        private Pd.TIdx IxCytc_o;
        private Pd.TIdx IxCyta_Total;
        private Pd.TIdx IxCyta_r;
        private Pd.TIdx IxCyta_o;
        private Pd.TIdx IxR_cytaO_to_R;
        private Pd.TIdx IxEm_UQ0;
        private Pd.TIdx IxEm_UQ;
        private Pd.TIdx IxUbiquinone_Total;
        private Pd.TIdx IxUQH2;
        private Pd.TIdx IxUQ;
        private Pd.TIdx IxEm_cytc0;
        private Pd.TIdx IxEm_cytc;
        private Pd.TIdx IxEma0;
        private Pd.TIdx IxEma;
        private Pd.TIdx IxCoAMit_Total;
        private Pd.TIdx IxCoAMit;
        private Pd.TIdx IxAcetylCoAMit;
        private Pd.TIdx IxCO2mit;
        private Pd.TIdx IxHCO3mit;
        private Pd.TIdx IxO2;
        private Pd.TIdx IxSuccinylCoAMit;
        private Pd.TIdx IxC_Buff;
        private Pd.TIdx IxR_Buff;
        private Pd.TIdx IxGlutamateMit;
        private Pd.TIdx IxAspartateMit;
        private Pd.TIdx IxMalateMit;
        private Pd.TIdx IxOxoglutarateMit;
        private Pd.TIdx IxPyruvateMit;
        private Pd.TIdx IxCitrateMit;
        private Pd.TIdx IxOxaloacetateMit;
        private Pd.TIdx IxIsocitrateMit;
        private Pd.TIdx IxIsocitrateMgMit;
        private Pd.TIdx IxIsocitrateFreeMit;
        private Pd.TIdx IxKdIsocitrateMit;
        private Pd.TIdx IxSuccinateMit;
        private Pd.TIdx IxFumarateMit;
        private Pd.TIdx IxAlanineMit;

        public double Hcyt;
        public double pHcyt;
        public double ATPiTotal;
        public double ATPiFree;
        public double Kd_ATPiMg = 0.024; // mM
        public double ATPiMg;
        public double ADPiTotal;
        public double ADPiFree;
        public double Kd_ADPiMg = 0.347; // mM
        public double ADPiMg;
        public double AMPi;
        public double AdenineTotali = 6.7;
        public double CreatineTotali = 25.0;
        public double Cr;
        public double PCr;
        public double PIi;
        public double C_Buffi = 0.025;
        public double R_Buffi = 172275.87705550331;
        public double Malatei;
        public double Glutamatei;
        public double Aspartatei;
        public double Pyruvatei;
        public double Oxoglutaratei;
        public double Citratei;

        public double Vmit_part = 0.23; // part of cell volume occupied with mitochondria
        public double Vmit;
        public double Rmc = 0.343; // Vmit/(Vi_diff=(Vcyt + Vjs + Vsl))

        public double Hmit;
        public double pHmit;
        public double Kmit;
        public double CamitTot;
        public double Camit;
        public double Camit_Min;
        public double Camit_Max;
        public double Namit;
        public double MgFreemit;
        public double dP;
        public double dPsi;
        private double dpH;
        public double meanCamit;
        private double Cmit = 1; // mM/mV

        private double AdeninemitTotal = 16.26; // mM
        private double Kd_ATPmitMg = 0.017; // mM
        public double ATPmitTotal;
        public double ATPmitMg;
        public double ATPmitFree;
        private double Kd_ADPmitMg = 0.282; // mM
        public double ADPmitTotal;
        public double ADPmitMg;
        public double ADPmitFree;
        public double GuanosinemitTotal = 1.0; // mM
        public double GDPmit;
        public double GTPmit;
        public double PImit;
        private double Em_NAD0 = -320.0; // mV
        public double Em_NAD; // mV
        private double NAD_Total = 2.97; // mM
        public double NADH;
        public double NAD;
        public double R_NAD_to_NADH;
        private double Cytc_Total = 0.27; // mM
        public double Cytc_r;
        public double Cytc_o;
        private double Cyta_Total = 0.135; // mM
        public double Cyta_r;
        public double Cyta_o;
        public double R_cytaO_to_R;
        private double Em_UQ0 = 40.0;
        public double Em_UQ;
        private double Ubiquinone_Total = 1.35; // mM
        public double UQH2;
        public double UQ;
        private double Em_cytc0 = 250.0; // mV
        public double Em_cytc; // mV
        private double Ema0 = 540.0; // mV
        private double Ema; // mV
        public double CoAMit_Total = 0.3; //mM
        public double CoAMit;
        public double AcetylCoAMit;
        public double CO2mit;
        public double HCO3mit;
        public double O2;
        public double SuccinylCoAMit;
        public double C_Buff = 0.22;

        public double R_Buff = 1992196.894548205;
        public double GlutamateMit;
        public double AspartateMit;
        public double MalateMit;
        public double OxoglutarateMit;
        public double PyruvateMit;
        public double CitrateMit;
        public double OxaloacetateMit;
        public double IsocitrateMit;
        public double IsocitrateMgMit;
        public double IsocitrateFreeMit;
        public double KdIsocitrateMit = 1.92; // mM
        public double SuccinateMit;
        public double FumarateMit;

        public double AlanineMit = 1.0; // mM

        public double AF = 2.0;
        public double AF_mem = 2.0;
        public double AF_mem_sub = 2.0;

        // Complex I
        private Pd.TIdx IxJ_C1;

        // Complex III
        private Pd.TIdx IxJ_C3;

        // Complex IV
        private Pd.TIdx IxJ_C4;
        private Pd.TIdx IxVO2;

        // ATPsynthase
        private Pd.TIdx IxJ_ATPsyn;

        // ANT
        private Pd.TIdx IxJ_ANT;

        // PiC
        private Pd.TIdx IxJ_PiC;

        // HLeak
        private Pd.TIdx IxJ_HLeak;

        // Citric acid cycle
        // CS
        private Pd.TIdx IxJ_CS;

        // ACO
        private Pd.TIdx IxJ_ACO;

        // ICDH
        private Pd.TIdx IxJ_ICDH;

        // OGDH
        private Pd.TIdx IxJ_OGDH;

        // SCS
        private Pd.TIdx IxJ_SCS;

        // SDH
        private Pd.TIdx IxJ_SDH;

        // FH
        private Pd.TIdx IxJ_FH;

        // MDH
        private Pd.TIdx IxJ_MDH;

        // NDK
        private Pd.TIdx IxJ_NDK;

        // AST
        private Pd.TIdx IxJ_AST;

        // Pyruvate pathway
        // PDHC
        private Pd.TIdx IxJ_PDHC;

        // PC
        private Pd.TIdx IxJ_PC;

        // ALT
        private Pd.TIdx IxJ_ALT;

        // Metabolite transporters
        // MCT
        private Pd.TIdx IxJ_MCT;

        // DCT
        private Pd.TIdx IxJ_DCT;

        // TCT
        private Pd.TIdx IxJ_TCT;

        // OGC
        private Pd.TIdx IxJ_OGC;

        // AGC
        private Pd.TIdx IxJ_AGC;

        // Cation transporting system
        // KUni
        private Pd.TIdx IxJ_KUni;

        // KHE
        private Pd.TIdx IxJ_KHE;

        // CaUni_js, CaUni_cyt
        private Pd.TIdx IxJ_CaUni_js;
        private Pd.TIdx IxF_CaUni_js;
        private Pd.TIdx IxJ_CaUni_cyt;
        private Pd.TIdx IxF_CaUni_cyt;

        // NCXmit, NmSC
        private Pd.TIdx IxJ_NCXmit;
        private Pd.TIdx IxF_NCXmit;
        private Pd.TIdx IxJ_NmSC;
        private Pd.TIdx IxF_NmSC;
        private Pd.TIdx IxJ_NmSC_block;
        private Pd.TIdx IxF_NmSC_block;
        private Pd.TIdx IxdATPuse_NmSC;

        // NHE
        private Pd.TIdx IxJ_NHE;

        // CHE, newly added on 24Feb08
        private Pd.TIdx IxJ_HCXmit;
        //////private Pd.TIdx IxF_HCXmit;

        // Cabuffmit

        // Extramitochondrial processes
        // AK
        private Pd.TIdx IxJ_AK;

        // CK
        private Pd.TIdx IxJ_CK;

        // Complex I
        private double k_C1 = 1.9 * 0.000059421725; // mM/mV/msec
        private double dGC1;
        private double alpha_C1;
        private double KmUQ = 0.04;
        private double KmNADH = 0.014;
        public double J_C1; // mM/msec

        // Complex III 
        private double k_C3 = 1.15 * 1.582392 * 1.9 * 0.00000180921328; // mM/mV/msec
        private double dGC3;
        private double beta_C3;
        private double KmUQH2 = 0.008; // mM
        private double KmCytco = 0.0015; // mM
        public double J_C3; // mM/msec

        // Complex IV
        private double k_C4 = 1.9 * 1.79046; // mM//msec
        private double Km_C4_O2 = 0.15; // mM
        public double J_C4; // mM/msec
        public double VO2;

        // ATPsynthase
        private double nCa_ATPsyn = 5; // changed on 24Feb21, original 2.7
        private double KaCa_ATPsyn = 0.3 * 0.0002402; // mM, changed on 24Feb21, original 0.0002402 mM
        private double L_ATPsyn = 4; // changed on 24Feb21, original 3.73
        private double dGp0 = 31.9; // kJ/mol
        private double nA_ATPsyn = 3.0;
        private double kATPsyn = 0.87 * 0.0762 * 0.008533685; // mM/msec, changed on 24Feb 21, original 0.0762 * 0.008533685 mM/msec
        public double J_ATPsyn; // mM/msec

        // ANT
        private double kANT = 0.31419682 * 0.013570912; // mM/msec
        private double KmADP = 0.0035;     // mM
        private double fANT = 0.65;
        public double J_ANT; // mM/msec

        // PiC
        private double kPiC = 3.342351023 * 17.26225;
        private double pKa = 6.8;  // acid dissociation constant
        public double J_PiC; // mM/msec

        // HLeak
        private double kLK1 = 0.0;  // mM/msec 
        private double kLK2 = 0.038;        // 1/mV
        private double FCCP = 0.0;
        public double J_HLeak; // mM/msec

        // Citric acid cycle
        // CS
        private double Etotal_CS = 0.0276; // mM
        private double kcat_CS = 0.259; // /ms
        private double KmA_CS = 0.00645; // mM
        private double KmB_CS = 0.00497; // mM
        private double KH_CS = 0.000045; // mM
        private double KiATP_CS = 0.518; // mM
        private double KiADP_CS = 1.42; // mM
        private double KiCoA_CS = 0.067; // mM
        private double KiScCoA_CS = 0.13; // mM
        private double KiCIT_CS = 1.6; // mM
        public double J_CS; // mM/msec

        // ACO
        private double Etotal_ACO = 0.5; // mM
        private double kcatf_ACO = 0.0723; // /ms
        private double kcatr_ACO = 0.0706; // /ms
        private double KmA_ACO = 0.776; // mM
        private double KmP_ACO = 0.0621; // mM
        private double KII_ACO = 1.45; // mM
        private double KIS_ACO = 0.547; // mM
        public double J_ACO; // mM/msec

        // ICDH
        private double Etotal_ICDH = 0.109; // mM
        private double kcat_ICDH = 0.148; // /ms
        private double KH_ICDH = 0.00288; // mM
        private double KmA_ICDH = 0.131; // mM
        private double KmB_ICDH = 0.251; // mM
        private double KiQ_ICDH = 0.00351; // mM
        private double KADP_ICDH = 0.614; // mM
        private double aADP_ICDH = 0.024;
        private double bADP_ICDH = 0.86;
        private double KATP_ICDH = 1.78; // mM
        private double aATP_ICDH = 0.25;
        private double bATP_ICDH = 0.86;
        private double KCa1_ICDH = 0.00388; // mM
        private double aCa1_ICDH = 0.0011;
        private double bCa1_ICDH = 0.97;
        private double KCa2_ICDH = 0.0263; // mM
        private double aCa2_ICDH = 0.027;
        private double bCa2_ICDH = 1.02;
        private double KiMg1_ICDH = 0.698; // mM
        private double KiMg2_ICDH = 1.25; // mM
        public double J_ICDH; // mM/msec

        // OGDH
        private double Etotal_OGDH = 0.0098; // mM
        private double kcat0_OGDH = 0.177; // /ms
        private double KmA0_OGDH = 1.59; // mM
        private double KiA0_OGDH = 0.72; // mM
        private double KmB_OGDH = 0.0171; // mM
        private double KiB_OGDH = 0.74; // mM
        private double KmC_OGDH = 0.0324; // mM
        private double KiC_OGDH = 0.1; // mM^2
        private double KmP_OGDH = 0.3; // mM
        private double KiP_OGDH = 0.0011; // mM
        private double KmR_OGDH = 0.6; // mM
        private double KiQ_OGDH = 0.0874; // mM
        private double KiR_OGDH = 0.00808; // mM
        private double KMg_OGDH = 0.0262; // mM
        private double alphaMg_OGDH = 1.1;
        private double betaMg_OGDH = 3.17;
        private double KCa_OGDH = 0.0003; // mM
        private double alphaCa_OGDH = 0.236;
        private double betaCa_OGDH = 1.0;
        private double nCa_OGDH = 1.63;
        private double KADP_OGDH = 0.115;
        private double alphaADP_OGDH = 0.663;
        private double betaADP_OGDH = 2.1;
        private double KPi1_OGDH = 1.2; // mM
        private double alphaPi1_OGDH = 1.0;
        private double betaPi1_OGDH = 1.58;
        private double nPi1_OGDH = 3.6;
        private double KPi2_OGDH = 10.8; // mM
        private double alphaPi2_OGDH = 1.0;
        private double betaPi2_OGDH = 1.27;
        private double nPi2_OGDH = 9.3;
        public double J_OGDH; // mM/msec

        // SCS
        private double Etotal_SCS = 0.09; // mM
        private double Kc1_SCS = 0.163; // /ms
        private double Kc2_SCS = 0.00199; // /ms
        private double KmA_SCS = 0.0556; // mM
        private double KmB_SCS = 0.0245; // mM
        private double KmC_SCS = 30.9; // mM
        private double KmC2_SCS = 0.151; // mM
        private double KmP2_SCS = 0.914; // mM
        private double KmQ_SCS = 0.0212; // mM
        private double KiA_SCS = 0.162; // mM
        private double KiC_SCS = 0.704; // mM
        private double KiP_SCS = 40.1; // mM
        private double KiQ_SCS = 0.0185; // mM
        private double KiR_SCS = 0.0249; // mM
        private double Keq_SCS = 7.605; // mM
        public double J_SCS; // mM/msec

        // SDH
        private double Etotal_SDH = 0.0664; // mM
        private double kcatf_SDH = 0.0783333; // /ms
        private double kcatr_SDH = 0.0019583; // /ms
        private double KmA_SDH = 0.13; // mM
        private double KmB_SDH = 0.0003; // mM
        private double KmP_SDH = 0.025; // mM
        private double KmQ_SDH = 0.0015; // mM
        private double KiA_SDH = 0.03; // mM
        private double KiP_SDH = 0.15; // mM
        private double KiOAA1_SDH = 0.02; // mM
        private double KiOAA2_SDH = 0.2; // mM
        public double J_SDH; // mM/msec

        // FH
        private double Etotal_FH = 0.1; // mM
        private double kcatFUM = 1.81667; // /ms
        private double kcatMAL = 1.31667; // /ms
        private double KmFUM = 0.00356; // mM
        private double KmMAL = 0.0113; // mM
        private double KiATP_FH = 0.0115; // mM
        private double KHA = 0.000264; // mM
        private double KHF = 0.06607; // mM
        private double KHM = 0.01862; // mM
        private double KaE = 0.000631; // mM
        private double KbE = 0.0001584; // mM
        private double KaEF = 0.005012; // mM
        private double KbEF = 0.00005012; // mM
        private double KaEM = 0.0002512; // mM
        private double KbEM = 0.000003981; // mM
        public double J_FH; // mM/msec

        // MDH
        private double Etotal_MDH = 0.322; // mM
        private double kcatf_MDH = 0.1177; // /ms
        private double KmA_MDH = 0.0758; // mM
        private double KmB_MDH = 0.498; // mM
        private double KmP_MDH = 0.0391; // mM
        private double KmQ_MDH = 0.111; // mM
        private double KiA_MDH = 0.205; // mM
        private double KiB_MDH = 0.86; // mM
        private double KiP_MDH = 0.00402; // mM
        private double KiQ_MDH = 0.0143; // mM
        private double Keq_MDH = 0.000136;
        private double KCIT_MDH = 105.6; // mM
        private double aCIT_MDH = 0.428;
        private double bCIT_MDH = 3.12;
        private double KiCIT_MDH = 15.3; // mM
        private double KMAL_MDH = 3.0; // mM
        private double aMAL_MDH = 9.97;
        private double bMAL_MDH = 1.78;
        private double aKiOAA_MDH = 5.51; // mM
        private double bKiOAA_MDH = 6.26; // mM
        private double KiATP_MDH = 0.1832; // mM
        private double KiADP_MDH = 0.3944; // mM
        public double J_MDH; // mM/msec

        // NDK
        private double Etotal_NDK = 0.1; // mM
        private double kcat_NDK = 10.03; // /ms
        private double KmA_NDK = 0.31; // mM
        private double KmB_NDK = 0.043; // mM
        private double KmP_NDK = 0.05; // mM
        private double KiA_NDK = 0.21; // mM
        private double KiB_NDK = 0.04; // mM
        private double KiP_NDK = 0.057; // mM
        private double KiQ_NDK = 0.35; // mM
        private double Keq_NDK = 1.28;
        public double J_NDK; // mM/msec

        // AST
        private double Etotal_AST = 0.3; // mM
        private double KcF_AST = 0.3; // mM
        private double KcR_AST = 0.58; // mM
        private double KmA_AST = 1.58; // mM
        private double KmB_AST = 0.149; // mM
        private double KmP_AST = 0.0399; // mM
        private double KmQ_AST = 2.5; // mM
        private double KiA_AST = 2.0; // mM
        private double KiQ_AST = 1.83; // mM
        private double Keq_AST = 0.113;
        public double J_AST; // mM/msec

        // Pyruvate pathway
        // PDHC
        private double Etotal_PDHC = 0.142; // mM
        private double kcatf_PDHC = 0.308; // /ms
        private double KmA_PDHC = 0.025; // mM
        private double KmB_PDHC = 0.013; // mM
        private double KmC_PDHC = 0.05; // mM
        private double KmP_PDHC = 0.00059; // mM
        private double KmR_PDHC = 0.00069; // mM
        private double KiA_PDHC = 0.55; // mM
        private double KiB_PDHC = 0.3; // mM
        private double KiC_PDHC = 0.18; // mM
        private double KiP_PDHC = 0.06; // mM
        private double KiQ_PDHC = 0.035; // mM
        private double KiR_PDHC = 0.036; // mM
        private double u1 = 13.84;
        private double u2 = 0.03389;
        private double KCa_PDHC = 0.0002556; // mM
        private double n_PDHC = 0.9497;
        public double J_PDHC; // mM/msec

        // PC
        private double Etotal_PC = 0.000001; // mM 
        private double KcF_PC = 0.07985; // /ms 
        private double KcR_PC = 0.06861; // /ms
        private double KmA_PC = 0.207; // mM 
        private double KmB_PC = 1.77; // mM
        private double KmC_PC = 0.693; // mM
        private double KmP_PC = 0.618; // mM
        private double KmQ_PC = 14.7; // mM
        private double KmR_PC = 0.037; // mM
        private double KiA_PC = 0.138; // mM
        private double KiB_PC = 7.79; // mM
        private double KiC_PC = 0.0496; // mM
        private double KiP_PC = 0.19; // mM
        private double KiQ_PC = 15.1; // mM
        private double KiR_PC = 0.167; // mM
        private double Keq_PC = 9;
        public double J_PC; // mM/msec

        // ALT
        private double Etotal_ALT = 0.000001; // mM
        private double kcatf_ALT = 0.1088; // /ms
        private double kcatr_ALT = 0.1088; // /ms
        private double KmA_ALT = 9.22; // mM
        private double KmB_ALT = 0.115; // mM
        private double KmP_ALT = 0.231; // mM
        private double KmQ_ALT = 7.4; // mM
        private double Kip_ALT = 0.192; // mM
        private double Kiq_ALT = 2.47; // mM
        private double KIA_ALT = 555.0; // mM
        private double KIQ_ALT = 97.4; // mM
        private double Keq_ALT = 2.3;
        public double J_ALT; // mM/msec

        // Metabolite transporters
        // MCT
        private double Etotal_MCT = 2 * 0.1; // mM
        private double kcatf_MCT = 0.00381; // /ms
        private double kcatr_MCT = 0.00239; // /ms
        private double KmA_MCT = 0.00000889; // mM
        private double KmB_MCT = 0.711; // mM
        private double KmP_MCT = 0.659; // mM
        private double KmQ_MCT = 0.00000136; // mM
        private double alpha_MCT = 0.14;
        public double J_MCT; // mM/msec

        // DCT
        private double Etotal_DCT = 0.2; // mM
        private double kcatf_DCT = 0.00257; // /ms
        private double kcatr_DCT = 0.0029; // /ms
        private double KmA_DCT = 0.519; // mM
        private double KmB_DCT = 0.67; // mM
        private double KmP_DCT = 0.83; // mM
        private double KmQ_DCT = 0.78; // mM
        private double alpha_DCT = 0.84;
        public double J_DCT; // mM/msec

        // TCT
        private double Etotal_TCT = 0.25; // mM
        private double kcatf_TCT = 0.00525; // /ms
        private double kcatr_TCT = 0.00575; // /ms
        private double KmA_TCT = 0.039; // mM
        private double KmB_TCT = 0.055; // mM
        private double KmP_TCT = 0.35; // mM
        private double KmQ_TCT = 0.042; // mM
        public double J_TCT; // mM/msec

        // OGC
        private double Etotal_OGC = 0.136; // mM
        private double kcatf_OGC = 0.00436; // /ms
        private double kcatr_OGC = 0.0169; // /ms
        private double KmA_OGC = 0.035; // mM
        private double KmB_OGC = 2.37; // mM
        private double KmP_OGC = 0.21; // mM
        private double KmQ_OGC = 1.0; // mM
        private double alpha_OGC = 0.65;
        public double J_OGC; // mM/msec

        // AGC
        private double Etotal_AGC = 1.0; // mM
        private double kcat_AGC = 0.0252 * 0.001088; // /ms
        private double KmA_AGC = 0.0298; // mM
        private double KmQ_AGC = 2.8; // mM
        private double KB_AGC = 1.78; // mM
        private double KP_AGC = 0.228; // mM
        private double KHG_AGC = 0.000316; // mM
        private double alpha_dPsi_AGC = 0.3713;
        private double LCa_AGC = 31.262;

        private double KaCa_AGC = 0.9 * 0.0003802;

        private double nCa_AGC = 4.6;
        private double alpha_AGC = 1.49;
        public double J_AGC; // mM/msec

        // Cation transporting system
        // KUni
        private double k_KUni = 0.0000000725; // /ms
        public double J_KUni; // mM/msec

        // KHE
        private double k_KHE = 0.075081; // /ms
        public double J_KHE; // mM/msec

        // CaUni_js, CaUni_cyt
        private double PCaUni = (1 / 0.6111) * 0.4 * 0.05624;

        private double alphamit = 0.2;
        private double alphacyt = 0.341;

        private double KiCamit = 0.01; // PTP opens arround Camit=10 uM
        public double fraction_CaUni_js = 0.5;
        public double J_CaUni_js; // mM/msec
        public double J_CaUni_cyt; // mM/msec
        public double F_CaUni_js; // mM
        public double F_CaUni_cyt; // mM
        private double Hillinh_CaUni = 1.0;
        private double Kinh_CaUni = 0.001 * 0.05;
        private double Hillrec_CaUni = 2;
        private double Krec_CaUni = 0.001 * 0.8;
        public double SF_NCXmit = 1.0; // /mM/ms

        private double PNCXmit = 0.125 * 0.4 * 0.010616;
        private double KdNamit_NCXmit = 18.7137; // /mM,
        private double KdCamit_NCXmit = 0.0025; // /mM, 

        private double KdNai_NCXmit = 32.0; // /mM
        private double KdCai_NCXmit = 0.0125; // /mM
        private double partition_NCXmit = 0.2;
        public double n_NCXmit = 3.0;
        public double fraction_NmSC = 0.7;
        public double J_NCXmit;
        public double F_NCXmit;
        public double J_NmSC;
        public double F_NmSC;
        public double J_NmSC_block;
        public double F_NmSC_block;
        public double dATPuse_NmSC;

        // NHE
        private double Vmax_NHE = 0.4 * 12.103875;
        private double KNacell_NHE = 20.0; // mM
        private double KNamit_NHE = 6.0; // mM
        private double KH_NHE = 0.0000001; // mM
        private double KH_reg_NHE = 0.0000757; // mM
        public double J_NHE; // mM/msec

        // HCXmit
        private double Vmax_HCXmit = 0.0;
        private double KCa_HCXmit = 4.8E-3;
        public double J_HCXmit; // mM/msec
        public double F_HCXmit;

        // Cabuffmit
        private double KdCabuffmit = 0.001;
        private double Cabuffmit_tot = 2.0;


        // Extramitochondrial processes
        // AK
        private double kAKf = 49.283; // /mM/ms
        private double kAKb = 1.300346667; // /mM/ms
        public double J_AK; // mM/msec

        // CK
        private double kCKf = 0.011009333; // /mM/ms
        private double kCKb = 0.00005004240167; // /mM/ms
        public double J_CK; // mM/msec

        public cMitochondria()  //Constructor
        {
        }

        void ComplexI_III_IV(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double NADH = tvY[Pd.IdxNADH];
            double UQH2 = tvY[Pd.IdxUQH2];
            double Cytc_r = tvY[Pd.IdxCytc_r];
            double dPsi = tvY[Pd.IdxdPsi];
            double O2 = tvY[Pd.IdxO2];

            // Complex I
            dGC1 = Em_UQ - Em_NAD - 2 * dP;
            alpha_C1 = NADH * UQ / (NADH * UQ + NADH * KmUQ + UQ * KmNADH);

            J_C1 = AF_mem * alpha_C1 * k_C1 * dGC1;
            if (J_C1 < 0.0)
            {
                J_C1 = 0.000000000000001;
            };

            // Complex III
            dGC3 = Em_cytc - Em_UQ - 2 * dP - dPsi;
            beta_C3 = UQH2 * Cytc_o / (UQH2 * Cytc_o + Cytc_o * KmUQH2 + UQH2 * KmCytco);

            J_C3 = AF_mem * beta_C3 * k_C3 * dGC3;
            if (J_C3 < 0.0)
            {
                J_C3 = 0.000000000000001;
            };

            // Complex IV
            J_C4 = AF_mem * k_C4 * Cyta_r * Cytc_r * O2 / (O2 + Km_C4_O2);
            if (J_C4 < 0.0)
            {
                J_C4 = 0.000000000000001;
            };

            VO2 = -0.5 * J_C4;

            myCell.TVc[Pd.InxJ_C1] = J_C1;
            myCell.TVc[Pd.InxJ_C3] = J_C3;
            myCell.TVc[Pd.InxJ_C4] = J_C4;
            myCell.TVc[Pd.InxVO2] = VO2;
        }

        void ATPsynthase(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double R_ = Pd.R;
            double T_ = Pd.T;
            double F_ = Pd.F;

            double ATPtm = tvY[Pd.IdxATPmitTotal];
            double ADPtm = ADPmitTotal;
            double PIm = tvY[Pd.IdxPImit];

            double Ca_;
            Ca_ = Math.Pow(myCell.TVc[Pd.InxCamit], nCa_ATPsyn);

            double KCa_ = Math.Pow(KaCa_ATPsyn, nCa_ATPsyn);
            double Cafactor;
            Cafactor = 1 + L_ATPsyn * Ca_ / (Ca_ + KCa_);

            double dGp = dGp0 * 1000 / (R_ * T_) + Math.Log(1000 * ATPtm / (ADPtm * PIm));
            double dGSN = nA_ATPsyn * dP * F_ / (R_ * T_) - dGp;
            double gamma = Math.Exp(dGSN);

            J_ATPsyn = AF_mem * Cafactor * kATPsyn * (gamma - 1) / (gamma + 1);

            if (J_ATPsyn < 0 && ATPtm < 0.001)
            {
                J_ATPsyn = 0.000000000000001;
            };
            if (J_ATPsyn > 0 && PIm < 0.001)
            {
                J_ATPsyn = 0.000000000000001;
            };
            myCell.TVc[Pd.InxJ_ATPsyn] = J_ATPsyn;
        }

        void ANT(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double dPsi_ = tvY[Pd.IdxdPsi];
            double dPsii = fANT * dPsi_;
            double dPsie = -(1 - fANT) * dPsi_;
            double ATPef = ATPiFree;
            double ADPef = ADPiFree;
            double ATPif = ATPmitFree;
            double ADPif = ADPmitFree;

            double FRT = 1 / Pd.RTF;

            J_ANT = AF_mem * kANT / (1 + KmADP / ADPef) * (ADPef / (ADPef + ATPef * Math.Exp(-dPsie * FRT))
                 - ADPif / (ADPif + ATPif * Math.Exp(-dPsii * FRT)));

            if (J_ANT > 0 && ATPif < 0.001)
            {
                J_ANT = 0.000000000000001;
            };
            if (J_ANT < 0 && ATPef < 0.001)
            {
                J_ANT = 0.000000000000001;
            };
            myCell.TVc[Pd.InxJ_ANT] = J_ANT;
        }

        void PiC(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Piej = tvY[Pd.IdxPIi] / (1 + Math.Pow(10, (pHcyt - pKa)));
            double Piij = tvY[Pd.IdxPImit] / (1 + Math.Pow(10, (pHmit - pKa)));
            double He = tvY[Pd.IdxHcyt];
            double Hi = tvY[Pd.IdxHmit];

            J_PiC = AF_mem * kPiC * (Piej * He - Piij * Hi);

            myCell.TVc[Pd.InxJ_PiC] = J_PiC;
        }

        void HLeak(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double F_FCCP = (1 + 5000.0 * FCCP / (FCCP + 0.0001));

            J_HLeak = AF_mem * F_FCCP * kLK1 * (Math.Exp(kLK2 * dP) - 1.0);
            myCell.TVc[Pd.InxJ_HLeak] = J_HLeak;
        }

        // Citric acid cycle
        void CS(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double A_ = tvY[Pd.IdxOxaloacetateMit];
            double B_ = tvY[Pd.IdxAcetylCoAMit];
            double CoA_ = CoAMit;
            double CIT_ = tvY[Pd.IdxCitrateMit];
            double ScCoA_ = tvY[Pd.IdxSuccinylCoAMit];
            double ATP_ = ATPmitFree;
            double ADP_ = ADPmitFree;
            double H_ = tvY[Pd.IdxHmit];
            double alpha1 = 1 / (1 + H_ / KH_CS);
            double alpha2 = 1 + CIT_ / KiCIT_CS;
            double alpha3 = 1 + CoA_ / KiCoA_CS + ScCoA_ / KiScCoA_CS + ATP_ / KiATP_CS + ADP_ / KiADP_CS;
            double Vmax = kcat_CS * Etotal_CS * alpha1;
            double KmA_ = KmA_CS * alpha2;
            double KmB_ = KmB_CS * alpha3;

            J_CS = AF * Vmax * A_ * B_ / (A_ * B_ + KmA_ * B_ + KmB_ * A_ + KmA_ * KmB_);
            myCell.TVc[Pd.InxJ_CS] = J_CS;
        }

        void ACO(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double A_ = tvY[Pd.IdxCitrateMit];
            double P_ = tvY[Pd.IdxIsocitrateMit];
            double OAA_ = tvY[Pd.IdxOxaloacetateMit];
            double OAA_IS = 1.0 + OAA_ * OAA_ / KIS_ACO;
            double OAA_II = 1.0 + OAA_ * OAA_ / KII_ACO;

            double forword = kcatf_ACO * A_ * KmP_ACO * Etotal_ACO;
            double backword = kcatr_ACO * P_ * KmA_ACO * Etotal_ACO;

            J_ACO = AF * (forword - backword) / (KmA_ACO * KmP_ACO * OAA_IS + (A_ * KmP_ACO + P_ * KmA_ACO) * OAA_II);
            myCell.TVc[Pd.InxJ_ACO] = J_ACO;
        }

        void ICDH(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double A_ = IsocitrateMgMit;
            double B_; // = NAD
            double Q_; // = NADH
            double ADP_ = ADPmitTotal;
            double ATP_ = tvY[Pd.IdxATPmitTotal];
            double Ca_;

            if (myCell.BlnCamitFix == true)
            {
                Ca_ = meanCamit;
            }
            else
            {
                Ca_ = myCell.TVc[Pd.InxCamit];
            }
            B_ = NAD;
            Q_ = tvY[Pd.IdxNADH];

            double Mg_ = myCell.TVc[Pd.InxMgFreemit];
            double H_ = tvY[Pd.IdxHmit];

            double Vmax = kcat_ICDH * Etotal_ICDH;
            double Hfactor = 1 + H_ / KH_ICDH;
            double Mgfactor1 = 1 + Mg_ / KiMg1_ICDH;
            double Mgfactor2 = 1 + Mg_ / KiMg2_ICDH * Mg_ / KiMg2_ICDH;

            double KCa1_ = KCa1_ICDH * Mgfactor2;
            double KCa2_ = KCa2_ICDH * Mgfactor2;
            double Ca1_act1;
            double Ca1_act2;
            double Ca1_act3;
            Ca1_act1 = 1 + aADP_ICDH * bCa1_ICDH * Ca_ / (aCa1_ICDH * bADP_ICDH * KCa1_);
            Ca1_act2 = 1 + aADP_ICDH * Ca_ / (aCa1_ICDH * KCa1_);
            Ca1_act3 = 1 + Ca_ / KCa1_;


            double ADP_act1 = bADP_ICDH * KmA_ICDH * ADP_ / (aADP_ICDH * KADP_ICDH * A_) * Ca1_act1;
            double ADP_act2 = KmA_ICDH * ADP_ / (aADP_ICDH * KADP_ICDH * A_) * Ca1_act2;
            double ADP_act3 = KmA_ICDH * ADP_ / (KADP_ICDH * A_) * Ca1_act3;
            double Ca2_act1;
            double Ca2_act2;
            double Ca2_act3;
            Ca2_act1 = 1 + aATP_ICDH * bCa2_ICDH * Ca_ / (aCa2_ICDH * bATP_ICDH * KCa2_);
            Ca2_act2 = 1 + aATP_ICDH * Ca_ / (aCa2_ICDH * KCa2_);
            Ca2_act3 = 1 + Ca_ / KCa2_;

            double ATP_act1 = bATP_ICDH * ATP_ / (aATP_ICDH * KATP_ICDH) * Ca2_act1;
            double ATP_act2 = ATP_ / (aATP_ICDH * KATP_ICDH) * Ca2_act2;
            double ATP_act3 = ATP_ / KATP_ICDH * Ca2_act3;
            double alpha1 = (1 + ADP_act1 + ATP_act1) / (1 + ADP_act2 + ATP_act2) / Hfactor;
            double alpha2 = (1 + ADP_act3 + ATP_act3) / (1 + ADP_act2 + ATP_act2) * Mgfactor1;
            double alpha3 = 1 + Q_ / KiQ_ICDH;

            J_ICDH = AF * Vmax * A_ * A_ * A_ * B_ * alpha1 /
                (A_ * A_ * A_ * B_ + KmA_ICDH * KmA_ICDH * KmA_ICDH * B_ * alpha2 + KmB_ICDH * A_ * A_ * A_ * alpha3 + KmA_ICDH * KmA_ICDH * KmA_ICDH * KmB_ICDH * alpha2 * alpha3);
            myCell.TVc[Pd.InxJ_ICDH] = J_ICDH;
        }

        void OGDH(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double A_ = tvY[Pd.IdxOxoglutarateMit];
            double B_ = CoAMit;
            double C_;// = NAD
            double Q_ = tvY[Pd.IdxSuccinylCoAMit];
            double R_;// = NADH
            double Ca_;

            if (myCell.BlnCamitFix == true)
            {
                Ca_ = meanCamit;
            }
            else
            {
                Ca_ = myCell.TVc[Pd.InxCamit];
            }
            C_ = NAD;
            R_ = tvY[Pd.IdxNADH];


            double Mg_ = myCell.TVc[Pd.InxMgFreemit];
            double ADP_ = ADPmitTotal;
            double Pi_;

            Pi_ = tvY[Pd.IdxPImit];


            //'Ca++ dependency
            double Ca_1;
            double Ca_2;
            double Ca_3;
            Ca_1 = 1 + Math.Pow(Ca_ / KCa_OGDH, nCa_OGDH);
            Ca_2 = 1 + Math.Pow(Ca_ / (alphaCa_OGDH * KCa_OGDH), nCa_OGDH);
            Ca_3 = 1 + betaCa_OGDH * Math.Pow(Ca_ / (alphaCa_OGDH * KCa_OGDH), nCa_OGDH);

            //'Mg++ dependency
            double Mg_1 = 1 + Mg_ / KMg_OGDH;
            double Mg_2 = 1 + Mg_ / (alphaMg_OGDH * KMg_OGDH);
            double Mg_3 = 1 + betaMg_OGDH * Mg_ / (alphaMg_OGDH * KMg_OGDH);

            //'ADP(dependency)
            double ADP_1 = 1 + ADP_ / KADP_OGDH;
            double ADP_2 = 1 + ADP_ / (alphaADP_OGDH * KADP_OGDH);
            double ADP_3 = 1 + betaADP_OGDH * ADP_ / (alphaADP_OGDH * KADP_OGDH);

            //'Phosphate(dependency)
            double Pi1_1 = 1 + Math.Pow(Pi_ / KPi1_OGDH, nPi1_OGDH);
            double Pi1_2 = 1 + Math.Pow(Pi_ / (alphaPi1_OGDH * KPi1_OGDH), nPi1_OGDH);
            double Pi1_3 = 1 + betaPi1_OGDH * Math.Pow(Pi_ / (alphaPi1_OGDH * KPi1_OGDH), nPi1_OGDH);
            double Pi2_1 = 1 + Math.Pow(Pi_ / KPi2_OGDH, nPi2_OGDH);
            double Pi2_2 = 1 + Math.Pow(Pi_ / (alphaPi2_OGDH * KPi2_OGDH), nPi2_OGDH);
            double Pi2_3 = 1 + betaPi2_OGDH * Math.Pow(Pi_ / (alphaPi2_OGDH * KPi2_OGDH), nPi2_OGDH);

            double alpha = Ca_1 * Mg_1 * ADP_1 * Pi1_1 * Pi2_1 / (Ca_2 * Mg_2 * ADP_2 * Pi1_2 * Pi2_2);
            double beta = Ca_3 * Mg_3 * ADP_3 * Pi1_3 * Pi2_3 / (Ca_2 * Mg_2 * ADP_2 * Pi1_2 * Pi2_2);

            double kcat = kcat0_OGDH * beta;
            double KmA = KmA0_OGDH * alpha;
            double KiA = KiA0_OGDH * alpha;

            double Term1 = A_ * B_ * C_;
            double Term2 = KmA * B_ * C_;
            double Term3 = KmB_OGDH * C_ * A_;
            double Term4 = KmC_OGDH * A_ * B_;
            double Term5 = KmA * KmP_OGDH * KiB_OGDH * KiC_OGDH * Q_ * R_ / (KmR_OGDH * KiP_OGDH * KiQ_OGDH);
            double Term6 = KmC_OGDH * A_ * B_ * R_ / KiR_OGDH;
            double Term7 = KmB_OGDH * C_ * A_ * Q_ / KiQ_OGDH;
            double Term8 = KmA * KmP_OGDH * KiB_OGDH * KiC_OGDH * A_ * Q_ * R_ / (KmR_OGDH * KiP_OGDH * KiA * KiQ_OGDH);

            double denominator = Term1 + Term2 + Term3 + Term4 + Term5 + Term6 + Term7 + Term8;
            double numerator = kcat * Etotal_OGDH * A_ * B_ * C_;

            J_OGDH = AF * numerator / denominator;
            myCell.TVc[Pd.InxJ_OGDH] = J_OGDH;
        }

        void SCS(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double A_ = GDPmit;
            double B_ = tvY[Pd.IdxSuccinylCoAMit];
            double C_; // = PImit
            C_ = tvY[Pd.IdxPImit];

            double P_ = tvY[Pd.IdxSuccinateMit];
            double Q_ = tvY[Pd.IdxGTPmit];
            double R_ = CoAMit;

            double Term1 = KiA_SCS * KmB_SCS * C_;
            double Term2 = KmC_SCS * A_ * B_;
            double Term3 = KmA_SCS * B_ * C_;
            double Term4 = KmB_SCS * A_ * C_;
            double Term5 = A_ * B_ * C_;
            double Term6 = Term5 * C_ / KmC2_SCS;
            double Term7 = KiA_SCS * KmB_SCS * KmC_SCS * P_ / KiP_SCS;
            double Term8 = KiA_SCS * KmB_SCS * KmC_SCS * P_ * Q_ / (KiP_SCS * KiQ_SCS);
            double Term9 = KiA_SCS * KmB_SCS * KmC_SCS * P_ * R_ / (KiP_SCS * KiR_SCS);
            double Term10 = KiA_SCS * KmB_SCS * KiC_SCS * Q_ * R_ / (KmQ_SCS * KiR_SCS);
            double Term11 = KiA_SCS * KmB_SCS * KmC_SCS * P_ * Q_ * R_ / (KiP_SCS * KmQ_SCS * KiR_SCS);
            double Term12 = Term11 * P_ / KmP2_SCS;
            double Term13 = KiA_SCS * KmB_SCS * C_ * Q_ / KiQ_SCS;
            double Term14 = KiA_SCS * KmB_SCS * C_ * R_ / KiR_SCS;
            double Term15 = Term14 * Q_ / KmQ_SCS;
            double Term16 = Term15 * P_ / KmP2_SCS;
            double Term17 = KmB_SCS * KmC_SCS * A_ * P_ / KiP_SCS;
            double Term18 = KmA_SCS * KmC_SCS * B_ * P_ / KiP_SCS;
            double Term19 = KmC_SCS * A_ * B_ * P_ / KiP_SCS;
            double Term20 = Term19 * C_ / KmC2_SCS;
            double Term21 = KmA_SCS * B_ * C_ * Q_ / KiQ_SCS;
            double Term22 = KmB_SCS * A_ * C_ * R_ / KiR_SCS;
            double Term23 = KmA_SCS * KmC_SCS * B_ * P_ * Q_ / (KiP_SCS * KiQ_SCS);
            double Term24 = KmB_SCS * KmC_SCS * A_ * P_ * R_ / (KiP_SCS * KiR_SCS);

            double denominator = Term1 + Term2 + Term3 + Term4 + Term5 + Term6 + Term7 + Term8 + Term9 + Term10
                + Term11 + Term12 + Term13 + Term14 + Term15 + Term16 + Term17 + Term18 + Term19 + Term20 + Term21 + Term22 + Term23 + Term24;
            double numerator = (Kc1_SCS + Kc2_SCS * (KmC_SCS * P_ / (KmC2_SCS * KiP_SCS) + C_ / KmC2_SCS)) * (A_ * B_ * C_ - P_ * Q_ * R_ / Keq_SCS) * Etotal_SCS;

            J_SCS = AF * numerator / denominator;
            myCell.TVc[Pd.InxJ_SCS] = J_SCS;
        }

        void SDH(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double A_ = tvY[Pd.IdxSuccinateMit];
            double B_ = UQ;
            double P_ = tvY[Pd.IdxFumarateMit];
            double Q_ = tvY[Pd.IdxUQH2];
            double OAA_ = tvY[Pd.IdxOxaloacetateMit];

            double Vmaxf = kcatf_SDH * Etotal_SDH;
            double Vmaxr = kcatr_SDH * Etotal_SDH;
            double OAAFactor1 = 1 + OAA_ / KiOAA1_SDH;
            double OAAFactor2 = 1 + OAA_ / KiOAA2_SDH;
            double forward = Vmaxf * A_ * B_ / (A_ * B_ + KmA_SDH * B_ * (1 + P_ / KiP_SDH) * OAAFactor1 + KmB_SDH * A_);
            double backward = Vmaxr * P_ * Q_ / (P_ * Q_ + KmP_SDH * Q_ * (1 + A_ / KiA_SDH) * OAAFactor2 + KmQ_SDH * P_);

            J_SDH = AF * (forward - backward);
            myCell.TVc[Pd.InxJ_SDH] = J_SDH;
        }

        void FH(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double A_ = tvY[Pd.IdxFumarateMit];
            double P_ = tvY[Pd.IdxMalateMit];
            double H_ = tvY[Pd.IdxHmit];
            double ATP_ = ATPmitFree;

            double HE = 1 + H_ / KaE + KbE / H_;
            double HEF = 1 + H_ / KaEF + KbEF / H_;
            double HF = 1 + H_ / KHF;
            double HEM = 1 + H_ / KaEM + KbEM / H_;
            double HM = 1 + H_ / KHM;
            double ATPfactor = 1 + ATP_ / (KiATP_FH * (1 + KHA / H_));

            double kcatf = kcatFUM / HEF;
            double kcatr = kcatMAL / HEM;
            double KmA = KmFUM * ATPfactor * HE * HF / HEF;
            double KmP = KmMAL * ATPfactor * HE * HM / HEM;

            double forward = kcatf * KmP * A_ * Etotal_FH;
            double backward = kcatr * KmA * P_ * Etotal_FH;

            J_FH = AF * (forward - backward) / (KmA * KmP + KmA * P_ + KmP * A_);
            myCell.TVc[Pd.InxJ_FH] = J_FH;
        }

        void MDH(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double A_; // = NAD
            double B_ = tvY[Pd.IdxMalateMit];
            double P_ = tvY[Pd.IdxOxaloacetateMit];
            double Q_; // = NADH

            A_ = NAD;
            Q_ = tvY[Pd.IdxNADH];
            double CIT_ = tvY[Pd.IdxCitrateMit];
            double ATPf = ATPmitFree;
            double ADPf = ADPmitFree;
            double aKCIT = aCIT_MDH * KCIT_MDH;
            double aKMAL = aMAL_MDH * KMAL_MDH;

            double alpha1 = 1 + ATPf / KiATP_MDH + ADPf / KiADP_MDH;
            double alpha2 = (1 + bCIT_MDH * CIT_ / aKCIT + bMAL_MDH * B_ / aKMAL) / (1 + CIT_ / aKCIT + B_ / aKMAL);
            double alpha3 = (1 + CIT_ / KCIT_MDH + B_ / KMAL_MDH) / (1 + CIT_ / aKCIT + B_ / aKMAL);
            double alpha4 = (1 + CIT_ / KiCIT_MDH) * (1 + P_ / aKiOAA_MDH);
            double alpha5 = 1 + P_ / bKiOAA_MDH;

            double kcatf_ = kcatf_MDH * alpha2;
            double KmB_ = KmB_MDH * alpha3;
            double KiB_ = KiB_MDH * alpha3;
            double KmQ_ = KmQ_MDH * alpha4;
            double KiQ_ = KiQ_MDH * alpha4;
            double Keq_ = Keq_MDH * alpha2 * alpha4;

            double Term1 = KiA_MDH * KmB_ * alpha1;
            double Term2 = KmB_ * A_;
            double Term3 = KmA_MDH * B_ * alpha1;
            double Term4 = KiA_MDH * KmB_ * KmQ_ * P_ / (KiQ_ * KmP_MDH) * alpha1;
            double Term5 = KiA_MDH * KmB_ * Q_ / KiQ_;
            double Term6 = A_ * B_;
            double Term7 = KmB_ * KmQ_ * A_ * P_ / (KiQ_ * KmP_MDH);
            double Term8 = KiA_MDH * KmB_ * P_ * Q_ / (KiQ_ * KmP_MDH) * alpha5;
            double Term9 = KmA_MDH * B_ * Q_ / KiQ_;
            double Term10 = A_ * B_ * P_ / KiP_MDH;
            double Term11 = KiA_MDH * KmB_ * B_ * P_ * Q_ / (KiB_ * KiQ_ * KmP_MDH);

            double denominator = Term1 + Term2 + Term3 + Term4 + Term5 + Term6 + Term7 + Term8 + Term9 + Term10 + Term11;
            double numerator = (kcatf_ * A_ * B_ - kcatf_MDH * P_ * Q_ / Keq_) * Etotal_MDH;

            J_MDH = AF * numerator / denominator;
            myCell.TVc[Pd.InxJ_MDH] = J_MDH;
        }

        void NDK(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double A_ = ATPmitMg;
            double B_ = GDPmit;
            double P_ = ADPmitMg;
            double Q_ = tvY[Pd.IdxGTPmit];

            double Term1 = KmB_NDK * A_;
            double Term2 = KmA_NDK * B_;
            double Term3 = A_ * B_;
            double Term4 = KiA_NDK * KmB_NDK * P_ / KiP_NDK;
            double Term5 = KmA_NDK * KiB_NDK * Q_ / KiQ_NDK;
            double Term6 = KmA_NDK * KiB_NDK * P_ * Q_ / KmP_NDK / KiQ_NDK;
            double Term7 = KmB_NDK * A_ * P_ / KiP_NDK;
            double Term8 = KmA_NDK * B_ * Q_ / KiQ_NDK;

            double denominator = Term1 + Term2 + Term3 + Term4 + Term5 + Term6 + Term7 + Term8;
            double numerator = kcat_NDK * Etotal_NDK * (A_ * B_ - P_ * Q_ / Keq_NDK);

            J_NDK = AF * numerator / denominator;
            myCell.TVc[Pd.InxJ_NDK] = J_NDK;
        }

        void AST(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double A_ = tvY[Pd.IdxAspartateMit];
            double B_ = tvY[Pd.IdxOxoglutarateMit];
            double P_ = tvY[Pd.IdxOxaloacetateMit];
            double Q_ = tvY[Pd.IdxGlutamateMit];

            double Term1 = KcR_AST * KmB_AST * A_;
            double Term2 = KcR_AST * KmA_AST * B_;
            double Term3 = KcF_AST * KmQ_AST * P_ / Keq_AST;
            double Term4 = KcF_AST * KmP_AST * Q_ / Keq_AST;
            double Term5 = KcR_AST * A_ * B_;
            double Term6 = KcF_AST * KmQ_AST * A_ * P_ / KiA_AST / Keq_AST;
            double Term7 = KcF_AST * P_ * Q_ / Keq_AST;
            double Term8 = KcR_AST * KmA_AST * B_ * Q_ / KiQ_AST;

            double denominator = Term1 + Term2 + Term3 + Term4 + Term5 + Term6 + Term7 + Term8;
            double numerator = KcF_AST * KcR_AST * Etotal_AST * (A_ * B_ - P_ * Q_ / Keq_AST);
            J_AST = AF * numerator / denominator;
            myCell.TVc[Pd.InxJ_AST] = J_AST;
        }

        // Pyruvate pathway
        void PDHC(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Ca_;

            if (myCell.BlnCamitFix == true)
            {
                Ca_ = meanCamit;
            }
            else
            {
                Ca_ = myCell.TVc[Pd.InxCamit];
            }

            double fpdha;
            fpdha = 1 / (1 + u2 * (1 + u1 / (1 + Math.Pow(Ca_ / KCa_PDHC, n_PDHC))));

            double kcat = kcatf_PDHC * fpdha;

            double A_ = tvY[Pd.IdxPyruvateMit];
            double B_ = CoAMit;
            double C_; // = NAD
            double Q_ = tvY[Pd.IdxAcetylCoAMit];
            double R_; // = NADH
            C_ = NAD;
            R_ = tvY[Pd.IdxNADH];

            double Term1 = A_ * B_ * C_;
            double Term2 = KmA_PDHC * B_ * C_;
            double Term3 = KmB_PDHC * C_ * A_;
            double Term4 = KmC_PDHC * A_ * B_;
            double Term5 = KmA_PDHC * KmP_PDHC * KiB_PDHC * KiC_PDHC * Q_ * R_ / (KmR_PDHC * KiP_PDHC * KiQ_PDHC);
            double Term6 = KmC_PDHC * A_ * B_ * R_ / KiR_PDHC;
            double Term7 = KmB_PDHC * C_ * A_ * Q_ / KiQ_PDHC;
            double Term8 = KmA_PDHC * KmP_PDHC * KiB_PDHC * KiC_PDHC * A_ * Q_ * R_ / (KmR_PDHC * KiP_PDHC * KiA_PDHC * KiQ_PDHC);

            double denominator = Term1 + Term2 + Term3 + Term4 + Term5 + Term6 + Term7 + Term8;
            double numerator = kcat * Etotal_PDHC * A_ * B_ * C_;

            J_PDHC = AF * numerator / denominator;
            myCell.TVc[Pd.InxJ_PDHC] = J_PDHC;
        }

        void PC(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double A_ = ATPmitMg;
            double B_ = myCell.TVc[Pd.InxHCO3mit];
            double C_ = tvY[Pd.IdxPyruvateMit];
            double P_ = ADPmitMg;
            double Q_ = tvY[Pd.IdxPImit];

            Q_ = tvY[Pd.IdxPImit];

            double R_ = tvY[Pd.IdxOxaloacetateMit];

            double Term1 = KcR_PC * KiA_PC * KmB_PC * C_;
            double Term2 = KcR_PC * KmC_PC * A_ * B_;
            double Term3 = KcR_PC * KmA_PC * B_ * C_;
            double Term4 = KcR_PC * KmB_PC * A_ * C_;
            double Term5 = KcR_PC * A_ * B_ * C_;
            double Term6 = KcF_PC * KiP_PC * KmQ_PC * R_ / Keq_PC;
            double Term7 = KcF_PC * KmQ_PC * P_ * R_ / Keq_PC;
            double Term8 = KcF_PC * KmP_PC * Q_ * R_ / Keq_PC;
            double Term9 = KcF_PC * KmR_PC * P_ * Q_ / Keq_PC;
            double Term10 = KcF_PC * P_ * Q_ * R_ / Keq_PC;
            double Term11 = KcR_PC * KiA_PC * KmB_PC * C_ * P_ / KiP_PC;
            double Term12 = KcR_PC * KiA_PC * KmB_PC * C_ * Q_ / KiQ_PC;
            double Term13 = KcF_PC * KmP_PC * KiQ_PC * B_ * R_ / (KiB_PC * Keq_PC);
            double Term14 = KcF_PC * KmP_PC * KiQ_PC * A_ * R_ / (KiA_PC * Keq_PC);
            double Term15 = KcR_PC * KmC_PC * A_ * B_ * R_ / KiR_PC;
            double Term16 = KcF_PC * KmR_PC * C_ * P_ * Q_ / (KiC_PC * Keq_PC);
            double Term17 = KcR_PC * KmA_PC * B_ * C_ * Q_ / KiQ_PC;
            double Term18 = KcR_PC * KmA_PC * B_ * C_ * P_ / KiP_PC;
            double Term19 = KcF_PC * KmP_PC * B_ * Q_ * R_ / (KiB_PC * Keq_PC);
            double Term20 = KcF_PC * KmQ_PC * B_ * P_ * R_ / (KiB_PC * Keq_PC);

            double denominator = Term1 + Term2 + Term3 + Term4 + Term5 + Term6 + Term7 + Term8 + Term9 + Term10
                + Term11 + Term12 + Term13 + Term14 + Term15 + Term16 + Term17 + Term18 + Term19 + Term20;
            double numerator = KcF_PC * KcR_PC * Etotal_PC * (A_ * B_ * C_ - P_ * Q_ * R_ / Keq_PC);

            J_PC = AF * numerator / denominator;
            myCell.TVc[Pd.InxJ_PC] = J_PC;
        }

        void ALT(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double A_ = AlanineMit;
            double B_ = tvY[Pd.IdxOxoglutarateMit];
            double P_ = tvY[Pd.IdxPyruvateMit];
            double Q_ = tvY[Pd.IdxGlutamateMit];

            double Term1 = kcatr_ALT * KmA_ALT * B_;
            double Term2 = kcatr_ALT * KmB_ALT * A_;
            double Term3 = kcatr_ALT * A_ * B_;
            double Term4 = kcatf_ALT * KmP_ALT * Q_ / Keq_ALT;
            double Term5 = kcatf_ALT * KmQ_ALT * P_ / Keq_ALT;
            double Term6 = kcatf_ALT * P_ * Q_ / Keq_ALT;
            double Term7 = kcatr_ALT * KmA_ALT * B_ * Q_ / Kiq_ALT;
            double Term8 = kcatr_ALT * KmB_ALT * A_ * P_ / Kip_ALT;
            double Term9 = kcatr_ALT * KmB_ALT * A_ * A_ / KIA_ALT;
            double Term10 = kcatf_ALT * KmP_ALT * Q_ * Q_ / Keq_ALT / KIQ_ALT;

            double denominator = Term1 + Term2 + Term3 + Term4 + Term5 + Term6 + Term7 + Term8 + Term9 + Term10;
            double Vmax = kcatf_ALT * kcatr_ALT * Etotal_ALT * (A_ * B_ - P_ * Q_ / Keq_ALT);

            J_ALT = AF * Vmax / denominator;
            myCell.TVc[Pd.InxJ_ALT] = J_ALT;
        }

        void MCT(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double A_ = tvY[Pd.IdxHcyt];
            double B_ = myCell.TVc[Pd.InxPyruvatei];
            double P_ = tvY[Pd.IdxPyruvateMit];
            double Q_ = tvY[Pd.IdxHmit];

            double Term1 = A_ / KmA_MCT;
            double Term2 = B_ / KmB_MCT;
            double Term3 = P_ / KmP_MCT;
            double Term4 = Q_ / KmQ_MCT;
            double Term5 = Term1 * Term2 / alpha_MCT;
            double Term6 = Term3 * Term4;
            double Term7 = Term2 * Term4;
            double Term8 = Term1 * Term3;

            double denominator = 1 + Term1 + Term2 + Term3 + Term4 + Term5 + Term6 + Term7 + Term8;
            double forward = Term5 * kcatf_MCT * Etotal_MCT;
            double backward = Term6 * kcatr_MCT * Etotal_MCT;

            J_MCT = AF_mem_sub * (forward - backward) / denominator;
            myCell.TVc[Pd.InxJ_MCT] = J_MCT;
        }

        void DCT(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double A_ = myCell.TVc[Pd.InxMalatei];
            double B_ = tvY[Pd.IdxPImit];
            double P_ = tvY[Pd.IdxPIi];
            double Q_ = tvY[Pd.IdxMalateMit];

            double Term1 = A_ / KmA_DCT;
            double Term2 = B_ / KmB_DCT;
            double Term3 = P_ / KmP_DCT;
            double Term4 = Q_ / KmQ_DCT;
            double Term5 = Term1 * Term2 / alpha_DCT;
            double Term6 = Term3 * Term4;
            double Term7 = Term2 * Term4;
            double Term8 = Term1 * Term3;

            double denominator = 1 + Term1 + Term2 + Term3 + Term4 + Term5 + Term6 + Term7 + Term8;
            double forward = Term5 * kcatf_DCT * Etotal_DCT;
            double backward = Term6 * kcatr_DCT * Etotal_DCT;

            J_DCT = AF_mem_sub * (forward - backward) / denominator;
            myCell.TVc[Pd.InxJ_DCT] = J_DCT;
        }

        void TCT(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double A_ = myCell.TVc[Pd.InxCitratei];
            double B_ = tvY[Pd.IdxMalateMit];
            double P_ = myCell.TVc[Pd.InxMalatei];
            double Q_ = tvY[Pd.IdxCitrateMit];

            double Term1 = A_ / KmA_TCT;
            double Term2 = B_ / KmB_TCT;
            double Term3 = P_ / KmP_TCT;
            double Term4 = Q_ / KmQ_TCT;
            double Term5 = Term1 * Term2;
            double Term6 = Term3 * Term4;
            double Term7 = Term2 * Term4;
            double Term8 = Term1 * Term3;

            double denominator = 1 + Term1 + Term2 + Term3 + Term4 + Term5 + Term6 + Term7 + Term8;
            double forward = Term5 * kcatf_TCT * Etotal_TCT;
            double backward = Term6 * kcatr_TCT * Etotal_TCT;

            J_TCT = AF_mem_sub * (forward - backward) / denominator;
            myCell.TVc[Pd.InxJ_TCT] = J_TCT;
        }

        void OGC(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double A_ = myCell.TVc[Pd.InxOxoglutaratei];
            double B_ = tvY[Pd.IdxMalateMit];
            double P_ = myCell.TVc[Pd.InxMalatei];
            double Q_ = tvY[Pd.IdxOxoglutarateMit];

            double Term1 = A_ / KmA_OGC;
            double Term2 = B_ / KmB_OGC;
            double Term3 = P_ / KmP_OGC;
            double Term4 = Q_ / KmQ_OGC;
            double Term5 = Term1 * Term2 / alpha_OGC;
            double Term6 = Term3 * Term4;
            double Term7 = Term2 * Term4;
            double Term8 = Term1 * Term3;

            double denominator = 1 + Term1 + Term2 + Term3 + Term4 + Term5 + Term6 + Term7 + Term8;
            double forward = Term5 * kcatf_OGC * Etotal_OGC;
            double backward = Term6 * kcatr_OGC * Etotal_OGC;

            J_OGC = AF_mem_sub * (forward - backward) / denominator;
            myCell.TVc[Pd.InxJ_OGC] = J_OGC;
        }

        void AGC(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            //'Ca++ dependenty
            double Ca_;
            Ca_ = Math.Pow(tvY[Pd.IdxCai], nCa_AGC);

            double KCa_ = Math.Pow(KaCa_AGC, nCa_AGC);
            double CaD = 1 + LCa_AGC * Ca_ / (Ca_ + KCa_);

            //'voltage dependenty
            double dPsi_ = tvY[Pd.IdxdPsi];
            double FRT = 1 / Pd.RTF;
            double VDf = Math.Exp((1 - alpha_dPsi_AGC) * dPsi_ * FRT);
            double VDr = Math.Exp(-alpha_dPsi_AGC * dPsi_ * FRT);

            double kcatf = kcat_AGC * CaD * VDf;
            double kcatr = kcat_AGC * CaD * VDr;

            double A_ = myCell.TVc[Pd.InxAspartatei];
            double B_ = tvY[Pd.IdxGlutamateMit];
            double P_ = myCell.TVc[Pd.InxGlutamatei];
            double Q_ = tvY[Pd.IdxAspartateMit];

            double KmB = KB_AGC * KHG_AGC / tvY[Pd.IdxHmit];
            double KmP = KP_AGC * KHG_AGC / tvY[Pd.IdxHcyt];

            double Term1 = A_ / KmA_AGC;
            double Term2 = B_ / KmB;
            double Term3 = P_ / KmP;
            double Term4 = Q_ / KmQ_AGC;
            double Term5 = Term1 * Term2 / alpha_AGC;
            double Term6 = Term3 * Term4;
            double Term7 = Term2 * Term4;
            double Term8 = Term1 * Term3;

            double denominator = 1 + Term1 + Term2 + Term3 + Term4 + Term5 + Term6 + Term7 + Term8;
            double forward = Term5 * kcatf * Etotal_AGC;
            double backward = Term6 * kcatr * Etotal_AGC;

            J_AGC = AF_mem_sub * (forward - backward) / denominator;
            myCell.TVc[Pd.InxJ_AGC] = J_AGC;
        }

        // Cation transporting system
        void KUni(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Ko = tvY[Pd.IdxKi];
            double Km = tvY[Pd.IdxKmit];
            double dPsi_ = tvY[Pd.IdxdPsi];
            double FRT = 1 / Pd.RTF;

            J_KUni = AF_mem * k_KUni * (Ko * Math.Exp(-dPsi_ * 0.5 * FRT) - Km * Math.Exp(dPsi_ * 0.5 * FRT));
            myCell.TVc[Pd.InxJ_KUni] = J_KUni;
        }
        void CaUni_cyt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Cam = myCell.TVc[Pd.InxCamit];

            double zFVoRT = 2 * tvY[Pd.IdxdPsi] / Pd.RTF;
            double ezFVoRT = Math.Exp(-zFVoRT);
            double denomi = (ezFVoRT - 1.0);
            double nume = alphamit * Cam - alphacyt * tvY[Pd.IdxCai] * ezFVoRT;
            double KiCamitn = KiCamit * KiCamit;
            double Camitn = Cam * Cam;
            double CamitInhibition = KiCamitn / (Camitn + KiCamitn); // Fraction of inhibition by Camit;

            double TermInh = Math.Pow(Cam, Hillinh_CaUni) / (Math.Pow(Cam, Hillinh_CaUni) + Math.Pow(Kinh_CaUni, Hillinh_CaUni));
            double TermRec = Math.Pow(Krec_CaUni, Hillrec_CaUni) / (Math.Pow(Cam, Hillrec_CaUni) + Math.Pow(Krec_CaUni, Hillrec_CaUni));

            double CamitReg = 0.9 - (TermInh - 0.3) * (TermRec - 0.3);

            if (denomi == 0.0)
            {
                J_CaUni_cyt = SF * (1 - fraction_CaUni_js) * PCaUni * CamitInhibition * CamitReg;
                F_CaUni_cyt = J_CaUni_cyt * Vmit;
            }
            else
            {
                J_CaUni_cyt = SF * (1 - fraction_CaUni_js) * PCaUni * zFVoRT * (nume / denomi) * CamitInhibition * CamitReg;
                F_CaUni_cyt = J_CaUni_cyt * Vmit;
            }

            myCell.TVc[Pd.InxJ_CaUni_cyt] = J_CaUni_cyt;
            myCell.TVc[Pd.InxF_CaUni_cyt] = F_CaUni_cyt;
        }
        void CaUni_js(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Cam = myCell.TVc[Pd.InxCamit];

            double zFVoRT = 2 * tvY[Pd.IdxdPsi] / Pd.RTF;
            double ezFVoRT = Math.Exp(-zFVoRT);
            double denomi = (ezFVoRT - 1.0);
            double nume = alphamit * Cam - alphacyt * tvY[Pd.IdxCajs] * ezFVoRT;
            double KiCamitn = KiCamit * KiCamit;
            double Camitn = Cam * Cam;
            double CamitInhibition = KiCamitn / (Camitn + KiCamitn); // Fraction of inhibition by Camit;

            double TermInh = Math.Pow(Cam, Hillinh_CaUni) / (Math.Pow(Cam, Hillinh_CaUni) + Math.Pow(Kinh_CaUni, Hillinh_CaUni));
            double TermRec = Math.Pow(Krec_CaUni, Hillrec_CaUni) / (Math.Pow(Cam, Hillrec_CaUni) + Math.Pow(Krec_CaUni, Hillrec_CaUni));

            double CamitReg = 0.9 - (TermInh - 0.3) * (TermRec - 0.3);

            if (denomi == 0.0)
            {
                J_CaUni_js = SF * fraction_CaUni_js * PCaUni * CamitInhibition * CamitReg;
                F_CaUni_js = J_CaUni_js * Vmit;
            }
            else
            {
                J_CaUni_js = SF * fraction_CaUni_js * PCaUni * zFVoRT * (nume / denomi) * CamitInhibition * CamitReg;
                F_CaUni_js = J_CaUni_js * Vmit;
            }

            myCell.TVc[Pd.InxJ_CaUni_js] = J_CaUni_js;
            myCell.TVc[Pd.InxF_CaUni_js] = F_CaUni_js;
        }

        void NCXmit(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double dPsi = tvY[Pd.IdxdPsi];
            double Cai = tvY[Pd.IdxCai];
            double Cam = myCell.TVc[Pd.InxCamit];

            double Nai = tvY[Pd.IdxNai];
            double Nam = tvY[Pd.IdxNamit];

            double FRT = 1 / Pd.RTF;
            double Nam_n = Nam * Nam * Nam;
            double Nai_n = Nai * Nai * Nai;

            double NamitOverKdNamit = Nam / KdNamit_NCXmit;
            double NaiOverKdNai = Nai / KdNai_NCXmit;

            double PNamit = Nam_n / (Nam_n + KdNamit_NCXmit * KdNamit_NCXmit * KdNamit_NCXmit * (1 + Cam / KdCamit_NCXmit));
            double PCamit = Cam / (Cam + KdCamit_NCXmit * (1 + NamitOverKdNamit * NamitOverKdNamit * NamitOverKdNamit));
            double PNai = Nai_n / (Nai_n + KdNai_NCXmit * KdNai_NCXmit * KdNai_NCXmit * (1 + Cai / KdCai_NCXmit));
            double PCai = Cai / (Cai + KdCai_NCXmit * (1 + NaiOverKdNai * NaiOverKdNai * NaiOverKdNai));

            double k1 = 1.0 * Math.Exp(partition_NCXmit * dPsi * FRT) * PNamit;			//    /msec
            double k2 = 1.0 * Math.Exp((partition_NCXmit - 1.0) * dPsi * FRT) * PNai;		//    /msec
            double k3 = 1.0 * PCamit;												//    /msec
            double k4 = 1.0 * PCai;													//    /msec

            double alpha = k2 + k4;
            double beta = k1 + k3;

            double tE1_ = alpha / (alpha + beta);

            if (tE1_ < 0)
            {
                tE1_ = 0;
            }
            else
            {
                tE1_ = alpha / (alpha + beta);
            }
            double tE2_ = 1.0 - tE1_;

            J_NCXmit = -SF_NCXmit * (1 - fraction_NmSC) * PNCXmit * 2 * (tE2_ * k2 - tE1_ * k1);
            F_NCXmit = J_NCXmit * Vmit;

            myCell.TVc[Pd.InxJ_NCXmit] = J_NCXmit;
            myCell.TVc[Pd.InxF_NCXmit] = F_NCXmit;
        }

        void NmSC(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double dPsi = tvY[Pd.IdxdPsi];
            double Cai = tvY[Pd.IdxCai];
            double Cam = myCell.TVc[Pd.InxCamit];

            double Nai = tvY[Pd.IdxNai];
            double Nam = tvY[Pd.IdxNamit];

            double FRT = 1 / Pd.RTF;
            double Nam_n = Nam * Nam * Nam;
            double Nai_n = Nai * Nai * Nai;

            double NamitOverKdNamit = Nam / KdNamit_NCXmit;
            double NaiOverKdNai = Nai / KdNai_NCXmit;

            double PNamit = Nam_n / (Nam_n + KdNamit_NCXmit * KdNamit_NCXmit * KdNamit_NCXmit * (1 + Cam / KdCamit_NCXmit));
            double PCamit = Cam / (Cam + KdCamit_NCXmit * (1 + NamitOverKdNamit * NamitOverKdNamit * NamitOverKdNamit));
            double PNai = Nai_n / (Nai_n + KdNai_NCXmit * KdNai_NCXmit * KdNai_NCXmit * (1 + Cai / KdCai_NCXmit));
            double PCai = Cai / (Cai + KdCai_NCXmit * (1 + NaiOverKdNai * NaiOverKdNai * NaiOverKdNai));

            double k1 = 1.0 * Math.Exp(partition_NCXmit * dPsi * FRT) * PNamit;			//    /msec
            double k2 = 1.0 * Math.Exp((partition_NCXmit - 1.0) * dPsi * FRT) * PNai;		//    /msec
            double k3 = 1.0 * PCamit;												//    /msec
            double k4 = 1.0 * PCai;													//    /msec

            double alpha = k2 + k4;
            double beta = k1 + k3;

            double tE1_ = alpha / (alpha + beta);

            if (tE1_ < 0)
            {
                tE1_ = 0;
            }
            else
            {
                tE1_ = alpha / (alpha + beta);
            }
            double tE2_ = 1.0 - tE1_;

            J_NmSC = -SF_NCXmit * myCell.SR.SERCA.B_SERCA * fraction_NmSC * PNCXmit * 2 * (tE2_ * k2 - tE1_ * k1);
            F_NmSC = J_NmSC * Vmit;
            J_NmSC_block = -SF_NCXmit * (1 - myCell.SR.SERCA.B_SERCA) * fraction_NmSC * PNCXmit * 2 * (tE2_ * k2 - tE1_ * k1);
            F_NmSC_block = J_NmSC_block * Vmit;

            myCell.TVc[Pd.InxJ_NmSC] = J_NmSC;
            myCell.TVc[Pd.InxF_NmSC] = F_NmSC;
            myCell.TVc[Pd.InxJ_NmSC_block] = J_NmSC_block;
            myCell.TVc[Pd.InxF_NmSC_block] = F_NmSC_block;

            dATPuse_NmSC = -F_NmSC / (2 * myCell.Vi_diff); // 2Ca2+ ; 2H+ : 1ATP
            myCell.TVc[Pd.InxdATPuse_NmSC] = dATPuse_NmSC;
        }

        void KHE(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Ho = tvY[Pd.IdxHcyt];
            double Hm = tvY[Pd.IdxHmit];
            double Ko = tvY[Pd.IdxKi];
            double Km = tvY[Pd.IdxKmit];

            J_KHE = AF_mem * k_KHE * (Km * Ho - Ko * Hm);
            myCell.TVc[Pd.InxJ_KHE] = J_KHE;
        }

        void NHE(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Nao = tvY[Pd.IdxNai];
            double Nam = tvY[Pd.IdxNamit];
            double Ho = tvY[Pd.IdxHcyt];
            double Hm = tvY[Pd.IdxHmit];
            double nume = Vmax_NHE * Hm / (Hm + KH_reg_NHE) * ((Ho * Nam) / (KH_NHE * KNamit_NHE) - (Hm * Nao) / (KH_NHE * KNacell_NHE));
            double denom = 1.0 + Ho / KH_NHE + Nam / KNamit_NHE + (Ho * Nam) / (KH_NHE * KNamit_NHE) + Hm / KH_NHE + Nao / KNacell_NHE + (Hm * Nao) / (KH_NHE * KNacell_NHE);

            J_NHE = nume / denom;
            myCell.TVc[Pd.InxJ_NHE] = J_NHE;
        }
        void HCXmit(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Cao = tvY[Pd.IdxCai];
            double Cam = myCell.TVc[Pd.InxCamit];
            double Ho = tvY[Pd.IdxHcyt];
            double Hm = tvY[Pd.IdxHmit];
            double nume = Vmax_HCXmit * (Ho * Ho * Cam - Hm * Hm * Cao);
            double denom = KCa_HCXmit * (Ho * Ho + Hm * Hm) + Ho * Ho * Cam + Hm * Hm * Cao;

            J_HCXmit = -nume / denom;
            F_HCXmit = J_HCXmit * Vmit;

            myCell.TVc[Pd.InxJ_HCXmit] = J_HCXmit;
            myCell.TVc[Pd.InxF_HCXmit] = F_HCXmit;
        }

        void Cabuffmit(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double Bvalue = KdCabuffmit + Cabuffmit_tot - tvY[Pd.IdxCamitTot];
            double Cvalue = -KdCabuffmit * tvY[Pd.IdxCamitTot];
            myCell.TVc[Pd.InxCamit] = (-Bvalue + Math.Sqrt(Bvalue * Bvalue - 4 * Cvalue)) * 0.5;
        }

        // Extramitochondrial processes
        // AK
        void AK(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double ADPef = myCell.TVc[Pd.InxADPiFree];
            double ADPem = myCell.TVc[Pd.InxADPiMg];
            double ATPem = myCell.TVc[Pd.InxATPiMg];
            double AMPe = myCell.TVc[Pd.InxAMPi];

            J_AK = kAKf * ADPef * ADPem - kAKb * ATPem * AMPe;

            myCell.TVc[Pd.InxJ_AK] = J_AK;
        }

        void CK(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double ADPe = tvY[Pd.IdxADPiTotal];
            double ATPe = tvY[Pd.IdxATPiTotal];
            double PCr = tvY[Pd.IdxPCr];
            double Cr = myCell.TVc[Pd.InxCr];

            J_CK = kCKf * ADPe * PCr - kCKb * ATPe * Cr;

            myCell.TVc[Pd.InxJ_CK] = J_CK;
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxHcyt, ref i, Pd.IntVar, "Hcyt");
            SetIx(ref IxpHcyt, ref i, Pd.IntVar, "pHcyt");
            SetIx(ref IxKd_ATPiMg, ref i, Pd.IntPar, "Kd_ATPiMg");
            SetIx(ref IxATPiTotal, ref i, Pd.IntVar, "ATPiTotal");
            SetIx(ref IxATPiFree, ref i, Pd.IntVar, "ATPiFree");
            SetIx(ref IxATPiMg, ref i, Pd.IntVar, "ATPiMg");
            SetIx(ref IxKd_ADPiMg, ref i, Pd.IntPar, "Kd_ADPiMg");
            SetIx(ref IxADPiTotal, ref i, Pd.IntVar, "ADPiTotal");
            SetIx(ref IxADPiFree, ref i, Pd.IntVar, "ADPiFree");
            SetIx(ref IxADPiMg, ref i, Pd.IntVar, "ADPiMg");
            SetIx(ref IxAMPi, ref i, Pd.IntVar, "AMPi");
            SetIx(ref IxAdenineTotali, ref i, Pd.IntPar, "AdenineTotali");
            SetIx(ref IxCreatineTotali, ref i, Pd.IntPar, "CreatineTotali");
            SetIx(ref IxPCr, ref i, Pd.IntVar, "PCr");
            SetIx(ref IxCr, ref i, Pd.IntVar, "Cr");
            SetIx(ref IxPIi, ref i, Pd.IntVar, "PIi");
            SetIx(ref IxC_Buffi, ref i, Pd.IntVar, "C_Buffi");
            SetIx(ref IxR_Buffi, ref i, Pd.IntVar, "R_Buffi");
            SetIx(ref IxMalatei, ref i, Pd.IntPar, "Malatei");
            SetIx(ref IxGlutamatei, ref i, Pd.IntPar, "Glutamatei");
            SetIx(ref IxAspartatei, ref i, Pd.IntPar, "Aspartatei");
            SetIx(ref IxPyruvatei, ref i, Pd.IntPar, "Pyruvatei");
            SetIx(ref IxOxoglutaratei, ref i, Pd.IntPar, "Oxoglutaratei");
            SetIx(ref IxCitratei, ref i, Pd.IntPar, "Citratei");

            SetIx(ref IxVmit_part, ref i, Pd.IntPar, "Vmit_part");
            SetIx(ref IxVmit, ref i, Pd.IntPar, "Vmit");
            SetIx(ref IxRmc, ref i, Pd.IntPar, "Rmc");
            SetIx(ref IxNamit, ref i, Pd.IntVar, "Namit");
            SetIx(ref IxKmit, ref i, Pd.IntVar, "Kmit");

            SetIx(ref IxCamitTot, ref i, Pd.IntVar, "CamitTot");
            SetIx(ref IxCamit, ref i, Pd.IntVar, "Camit");
            SetIx(ref IxCamit_Min, ref i, Pd.IntVar, "Camit_Min");
            SetIx(ref IxCamit_Max, ref i, Pd.IntVar, "Camit_Max");
            SetIx(ref IxMgFreemit, ref i, Pd.IntPar, "MgFreemit");
            SetIx(ref IxHmit, ref i, Pd.IntVar, "Hmit");
            SetIx(ref IxpHmit, ref i, Pd.IntVar, "pHmit");
            SetIx(ref IxdP, ref i, Pd.IntVar, "dP");
            SetIx(ref IxdPsi, ref i, Pd.IntVar, "dPsi");
            SetIx(ref IxdpH, ref i, Pd.IntVar, "dpH");
            SetIx(ref IxCmit, ref i, Pd.IntPar, "Cmit");
            SetIx(ref IxKd_ATPmitMg, ref i, Pd.IntPar, "Kd_ATPmitMg");
            SetIx(ref IxATPmitTotal, ref i, Pd.IntVar, "ATPmitTotal");
            SetIx(ref IxATPmitFree, ref i, Pd.IntVar, "ATPmitFree");
            SetIx(ref IxATPmitMg, ref i, Pd.IntVar, "ATPmitMg");
            SetIx(ref IxKd_ADPmitMg, ref i, Pd.IntPar, "Kd_ADPmitMg");
            SetIx(ref IxADPmitTotal, ref i, Pd.IntVar, "ADPmitTotal");
            SetIx(ref IxADPmitFree, ref i, Pd.IntVar, "ADPmitFree");
            SetIx(ref IxADPmitMg, ref i, Pd.IntVar, "ADPmitMg");
            SetIx(ref IxAdeninemitTotal, ref i, Pd.IntPar, "AdeninemitTotal");
            SetIx(ref IxGuanosinemitTotal, ref i, Pd.IntPar, "GuanosinemitTotal");
            SetIx(ref IxGTPmit, ref i, Pd.IntVar, "GTPmit");
            SetIx(ref IxGDPmit, ref i, Pd.IntVar, "GDPmit");
            SetIx(ref IxPImit, ref i, Pd.IntVar, "PImit");
            SetIx(ref IxEm_NAD0, ref i, Pd.IntPar, "Em_NAD0");
            SetIx(ref IxEm_NAD, ref i, Pd.IntVar, "Em_NAD");
            SetIx(ref IxNAD_Total, ref i, Pd.IntPar, "NAD_Total");
            SetIx(ref IxNADH, ref i, Pd.IntVar, "NADH");
            SetIx(ref IxNAD, ref i, Pd.IntVar, "NAD");
            SetIx(ref IxR_NAD_to_NADH, ref i, Pd.IntVar, "R_NAD_to_NADH");
            SetIx(ref IxEm_cytc0, ref i, Pd.IntPar, "Em_cytc0");
            SetIx(ref IxEm_cytc, ref i, Pd.IntVar, "Em_cytc");
            SetIx(ref IxCytc_Total, ref i, Pd.IntPar, "Cytc_Total");
            SetIx(ref IxCytc_r, ref i, Pd.IntVar, "Cytc_r");
            SetIx(ref IxCytc_o, ref i, Pd.IntVar, "Cytc_o");
            SetIx(ref IxEma0, ref i, Pd.IntPar, "Ema0");
            SetIx(ref IxEma, ref i, Pd.IntVar, "Ema");
            SetIx(ref IxCyta_Total, ref i, Pd.IntPar, "Cyta_Total");
            SetIx(ref IxCyta_r, ref i, Pd.IntVar, "Cyta_r");
            SetIx(ref IxCyta_o, ref i, Pd.IntVar, "Cyta_o");
            SetIx(ref IxR_cytaO_to_R, ref i, Pd.IntVar, "R_cytaO_to_R");
            SetIx(ref IxEm_UQ0, ref i, Pd.IntPar, "Em_UQ0");
            SetIx(ref IxEm_UQ, ref i, Pd.IntVar, "Em_UQ");
            SetIx(ref IxUbiquinone_Total, ref i, Pd.IntPar, "Ubiquinone_Total");
            SetIx(ref IxUQH2, ref i, Pd.IntVar, "UQH2");
            SetIx(ref IxUQ, ref i, Pd.IntVar, "UQ");
            SetIx(ref IxCoAMit_Total, ref i, Pd.IntPar, "CoAMit_Total");
            SetIx(ref IxCoAMit, ref i, Pd.IntVar, "CoAMit");
            SetIx(ref IxAcetylCoAMit, ref i, Pd.IntVar, "AcetylCoAMit");
            SetIx(ref IxSuccinylCoAMit, ref i, Pd.IntVar, "SuccinylCoAMit");
            SetIx(ref IxCO2mit, ref i, Pd.IntPar, "CO2mit");
            SetIx(ref IxHCO3mit, ref i, Pd.IntPar, "HCO3mit");
            SetIx(ref IxO2, ref i, Pd.IntPar, "O2");
            SetIx(ref IxC_Buff, ref i, Pd.IntVar, "C_Buff");
            SetIx(ref IxR_Buff, ref i, Pd.IntVar, "R_Buff");
            SetIx(ref IxMalateMit, ref i, Pd.IntVar, "MalateMit");
            SetIx(ref IxGlutamateMit, ref i, Pd.IntVar, "GlutamateMit");
            SetIx(ref IxAspartateMit, ref i, Pd.IntVar, "AspartateMit");
            SetIx(ref IxPyruvateMit, ref i, Pd.IntVar, "PyruvateMit");
            SetIx(ref IxOxoglutarateMit, ref i, Pd.IntVar, "OxoglutarateMit");
            SetIx(ref IxCitrateMit, ref i, Pd.IntVar, "CitrateMit");
            SetIx(ref IxOxaloacetateMit, ref i, Pd.IntVar, "OxaloacetateMit");
            SetIx(ref IxKdIsocitrateMit, ref i, Pd.IntPar, "KdIsocitrateMit");
            SetIx(ref IxIsocitrateMit, ref i, Pd.IntVar, "IsocitrateMit");
            SetIx(ref IxIsocitrateFreeMit, ref i, Pd.IntVar, "IsocitrateFreeMit");
            SetIx(ref IxIsocitrateMgMit, ref i, Pd.IntVar, "IsocitrateMgMit");
            SetIx(ref IxSuccinateMit, ref i, Pd.IntVar, "SuccinateMit");
            SetIx(ref IxFumarateMit, ref i, Pd.IntVar, "FumarateMit");
            SetIx(ref IxAlanineMit, ref i, Pd.IntPar, "AlanineMit");

            SetIx(ref IxJ_C1, ref i, Pd.IntVar, "J_C1");
            SetIx(ref IxJ_C3, ref i, Pd.IntVar, "J_C3");
            SetIx(ref IxJ_C4, ref i, Pd.IntVar, "J_C4");
            SetIx(ref IxVO2, ref i, Pd.IntVar, "VO2");
            SetIx(ref IxJ_ATPsyn, ref i, Pd.IntVar, "J_ATPsyn");
            SetIx(ref IxJ_ANT, ref i, Pd.IntVar, "J_ANT");
            SetIx(ref IxJ_PiC, ref i, Pd.IntVar, "J_PiC");
            SetIx(ref IxJ_HLeak, ref i, Pd.IntVar, "J_HLeak");
            SetIx(ref IxJ_CS, ref i, Pd.IntVar, "J_CS");
            SetIx(ref IxJ_ACO, ref i, Pd.IntVar, "J_ACO");
            SetIx(ref IxJ_ICDH, ref i, Pd.IntVar, "J_ICDH");
            SetIx(ref IxJ_OGDH, ref i, Pd.IntVar, "J_OGDH");
            SetIx(ref IxJ_SCS, ref i, Pd.IntVar, "J_SCS");
            SetIx(ref IxJ_SDH, ref i, Pd.IntVar, "J_SDH");
            SetIx(ref IxJ_FH, ref i, Pd.IntVar, "J_FH");
            SetIx(ref IxJ_MDH, ref i, Pd.IntVar, "J_MDH");
            SetIx(ref IxJ_NDK, ref i, Pd.IntVar, "J_NDK");
            SetIx(ref IxJ_AST, ref i, Pd.IntVar, "J_AST");
            SetIx(ref IxJ_PDHC, ref i, Pd.IntVar, "J_PDHC");
            SetIx(ref IxJ_PC, ref i, Pd.IntVar, "J_PC");
            SetIx(ref IxJ_ALT, ref i, Pd.IntVar, "J_ALT");
            SetIx(ref IxJ_MCT, ref i, Pd.IntVar, "J_MCT");
            SetIx(ref IxJ_DCT, ref i, Pd.IntVar, "J_DCT");
            SetIx(ref IxJ_TCT, ref i, Pd.IntVar, "J_TCT");
            SetIx(ref IxJ_OGC, ref i, Pd.IntVar, "J_OGC");
            SetIx(ref IxJ_AGC, ref i, Pd.IntVar, "J_AGC");
            SetIx(ref IxJ_KUni, ref i, Pd.IntVar, "J_KUni");
            SetIx(ref IxJ_KHE, ref i, Pd.IntVar, "J_KHE");
            SetIx(ref IxJ_CaUni_cyt, ref i, Pd.IntVar, "J_CaUni_cyt");
            SetIx(ref IxF_CaUni_cyt, ref i, Pd.IntVar, "F_CaUni_cyt");
            SetIx(ref IxJ_CaUni_js, ref i, Pd.IntVar, "J_CaUni_js");
            SetIx(ref IxF_CaUni_js, ref i, Pd.IntVar, "F_CaUni_js");
            SetIx(ref IxJ_NCXmit, ref i, Pd.IntVar, "J_NCXmit");
            SetIx(ref IxF_NCXmit, ref i, Pd.IntVar, "F_NCXmit");
            SetIx(ref IxJ_NmSC, ref i, Pd.IntVar, "J_NmSC");
            SetIx(ref IxF_NmSC, ref i, Pd.IntVar, "F_NmSC");
            SetIx(ref IxJ_NmSC_block, ref i, Pd.IntVar, "J_NmSC_cyt");
            SetIx(ref IxF_NmSC_block, ref i, Pd.IntVar, "F_NmSC_cyt");
            SetIx(ref IxdATPuse_NmSC, ref i, Pd.IntVar, "dATPuse_NmSC");
            SetIx(ref IxJ_NHE, ref i, Pd.IntVar, "J_NHE");
            SetIx(ref IxJ_HCXmit, ref i, Pd.IntVar, "J_HCXmit"); // 24Feb08
            SetIx(ref IxJ_AK, ref i, Pd.IntVar, "J_AK");
            SetIx(ref IxJ_CK, ref i, Pd.IntVar, "J_CK");

            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpMitochondria_ListView, NumOfIx);

            Hcyt = myTVc[Pd.IdxHcyt];
            pHcyt = myTVc[Pd.InxpHcyt];
            ATPiTotal = myTVc[Pd.IdxATPiTotal];
            ATPiFree = myTVc[Pd.InxATPiFree];
            ATPiMg = myTVc[Pd.InxATPiMg];
            ADPiTotal = myTVc[Pd.IdxADPiTotal];
            ADPiFree = myTVc[Pd.InxADPiFree];
            ADPiMg = myTVc[Pd.InxADPiMg];
            AMPi = myTVc[Pd.InxAMPi];
            PCr = myTVc[Pd.IdxPCr];
            Cr = myTVc[Pd.InxCr];
            PIi = myTVc[Pd.IdxPIi];
            Malatei = myTVc[Pd.InxMalatei];
            Glutamatei = myTVc[Pd.InxGlutamatei];
            Aspartatei = myTVc[Pd.InxAspartatei];
            Pyruvatei = myTVc[Pd.InxPyruvatei];
            Oxoglutaratei = myTVc[Pd.InxOxoglutaratei];
            Citratei = myTVc[Pd.InxCitratei];
            O2 = myTVc[Pd.IdxO2];
            FCCP = 0.0;

            Vmit = Vmit_part * myCell.Vcell;
            Namit = myTVc[Pd.IdxNamit];
            Kmit = myTVc[Pd.IdxKmit];
            CamitTot = myTVc[Pd.IdxCamitTot];
            Camit = myTVc[Pd.InxCamit];
            Hmit = myTVc[Pd.IdxHmit];
            pHmit = myTVc[Pd.InxpHmit];
            MgFreemit = myTVc[Pd.InxMgFreemit];
            ATPmitTotal = myTVc[Pd.IdxATPmitTotal];
            ATPmitFree = myTVc[Pd.InxATPmitFree];
            ATPmitMg = myTVc[Pd.InxATPmitMg];
            ADPmitTotal = myTVc[Pd.InxADPmitTotal];
            ADPmitFree = myTVc[Pd.InxADPmitFree];
            ADPmitMg = myTVc[Pd.InxADPmitMg];
            PImit = myTVc[Pd.IdxPImit];
            NADH = myTVc[Pd.IdxNADH];
            NAD = myTVc[Pd.InxNAD];
            UQH2 = myTVc[Pd.IdxUQH2];
            UQ = myTVc[Pd.InxUQ];
            Cytc_r = myTVc[Pd.IdxCytc_r];
            Cytc_o = myTVc[Pd.InxCytc_o];
            Cyta_r = myTVc[Pd.InxCyta_r];
            Cyta_o = myTVc[Pd.InxCyta_o];
            dPsi = myTVc[Pd.IdxdPsi];
            dpH = myTVc[Pd.InxdpH];
            dP = myTVc[Pd.InxdP];
            AspartateMit = myTVc[Pd.IdxAspartateMit];
            GlutamateMit = myTVc[Pd.IdxGlutamateMit];
            OxoglutarateMit = myTVc[Pd.IdxOxoglutarateMit];
            MalateMit = myTVc[Pd.IdxMalateMit];
            PyruvateMit = myTVc[Pd.IdxPyruvateMit];
            CitrateMit = myTVc[Pd.IdxCitrateMit];
            IsocitrateMit = myTVc[Pd.IdxIsocitrateMit];
            IsocitrateFreeMit = myTVc[Pd.InxIsocitrateFreeMit];
            IsocitrateMgMit = myTVc[Pd.InxIsocitrateMgMit];
            SuccinylCoAMit = myTVc[Pd.IdxSuccinylCoAMit];
            FumarateMit = myTVc[Pd.IdxFumarateMit];
            SuccinateMit = myTVc[Pd.IdxSuccinateMit];
            OxaloacetateMit = myTVc[Pd.IdxOxaloacetateMit];
            AcetylCoAMit = myTVc[Pd.IdxAcetylCoAMit];
            CoAMit = myTVc[Pd.InxCoAMit];
            GTPmit = myTVc[Pd.IdxGTPmit];
            GDPmit = myTVc[Pd.InxGDPmit];
            CO2mit = myTVc[Pd.InxCO2mit];
            HCO3mit = myTVc[Pd.InxHCO3mit];

            J_C1 = myTVc[Pd.InxJ_C1];
            J_C3 = myTVc[Pd.InxJ_C3];
            J_C4 = myTVc[Pd.InxJ_C4];
            VO2 = myTVc[Pd.InxVO2];
            J_ATPsyn = myTVc[Pd.InxJ_ATPsyn];
            J_ANT = myTVc[Pd.InxJ_ANT];
            J_PiC = myTVc[Pd.InxJ_PiC];
            J_HLeak = myTVc[Pd.InxJ_HLeak];
            J_CS = myTVc[Pd.InxJ_CS];
            J_ACO = myTVc[Pd.InxJ_ACO];
            J_ICDH = myTVc[Pd.InxJ_ICDH];
            J_OGDH = myTVc[Pd.InxJ_OGDH];
            J_SCS = myTVc[Pd.InxJ_SCS];
            J_SDH = myTVc[Pd.InxJ_SDH];
            J_FH = myTVc[Pd.InxJ_FH];
            J_MDH = myTVc[Pd.InxJ_MDH];
            J_NDK = myTVc[Pd.InxJ_NDK];
            J_AST = myTVc[Pd.InxJ_AST];
            J_PDHC = myTVc[Pd.InxJ_PDHC];
            J_PC = myTVc[Pd.InxJ_PC];
            J_ALT = myTVc[Pd.InxJ_ALT];
            J_MCT = myTVc[Pd.InxJ_MCT];
            J_DCT = myTVc[Pd.InxJ_DCT];
            J_TCT = myTVc[Pd.InxJ_TCT];
            J_OGC = myTVc[Pd.InxJ_OGC];
            J_AGC = myTVc[Pd.InxJ_AGC];
            J_KUni = myTVc[Pd.InxJ_KUni];
            J_KHE = myTVc[Pd.InxJ_KHE];
            J_CaUni_cyt = myTVc[Pd.InxJ_CaUni_cyt];
            F_CaUni_cyt = myTVc[Pd.InxF_CaUni_cyt];
            J_CaUni_js = myTVc[Pd.InxJ_CaUni_js];
            F_CaUni_js = myTVc[Pd.InxF_CaUni_js];
            J_NCXmit = myTVc[Pd.InxJ_NCXmit];
            F_NCXmit = myTVc[Pd.InxF_NCXmit];
            J_NmSC = myTVc[Pd.InxJ_NmSC];
            F_NmSC = myTVc[Pd.InxF_NmSC];
            J_NmSC_block = myTVc[Pd.InxJ_NmSC_block];
            F_NmSC_block = myTVc[Pd.InxF_NmSC_block];
            dATPuse_NmSC = myTVc[Pd.InxdATPuse_NmSC];
            J_NHE = myTVc[Pd.InxJ_NHE];
            J_HCXmit = myTVc[Pd.InxJ_HCXmit]; // 24Feb08
            J_AK = myTVc[Pd.InxJ_AK];
            J_CK = myTVc[Pd.InxJ_CK];
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            // cytoplasm
            AK(dt, ref tvDYdt, ref tvY, myCell);
            CK(dt, ref tvDYdt, ref tvY, myCell);

            // mitochondria
            Cabuffmit(dt, ref tvDYdt, ref tvY, myCell);
            ComplexI_III_IV(dt, ref tvDYdt, ref tvY, myCell);
            ATPsynthase(dt, ref tvDYdt, ref tvY, myCell);
            ANT(dt, ref tvDYdt, ref tvY, myCell);
            PiC(dt, ref tvDYdt, ref tvY, myCell);
            HLeak(dt, ref tvDYdt, ref tvY, myCell);
            CS(dt, ref tvDYdt, ref tvY, myCell);
            ACO(dt, ref tvDYdt, ref tvY, myCell);
            ICDH(dt, ref tvDYdt, ref tvY, myCell);
            OGDH(dt, ref tvDYdt, ref tvY, myCell);
            SCS(dt, ref tvDYdt, ref tvY, myCell);
            SDH(dt, ref tvDYdt, ref tvY, myCell);
            FH(dt, ref tvDYdt, ref tvY, myCell);
            MDH(dt, ref tvDYdt, ref tvY, myCell);
            NDK(dt, ref tvDYdt, ref tvY, myCell);
            AST(dt, ref tvDYdt, ref tvY, myCell);
            PDHC(dt, ref tvDYdt, ref tvY, myCell);
            PC(dt, ref tvDYdt, ref tvY, myCell);
            ALT(dt, ref tvDYdt, ref tvY, myCell);
            MCT(dt, ref tvDYdt, ref tvY, myCell);
            DCT(dt, ref tvDYdt, ref tvY, myCell);
            TCT(dt, ref tvDYdt, ref tvY, myCell);
            OGC(dt, ref tvDYdt, ref tvY, myCell);
            AGC(dt, ref tvDYdt, ref tvY, myCell);
            KUni(dt, ref tvDYdt, ref tvY, myCell);
            KHE(dt, ref tvDYdt, ref tvY, myCell);
            CaUni_cyt(dt, ref tvDYdt, ref tvY, myCell);
            CaUni_js(dt, ref tvDYdt, ref tvY, myCell);
            NCXmit(dt, ref tvDYdt, ref tvY, myCell);
            NmSC(dt, ref tvDYdt, ref tvY, myCell);
            NHE(dt, ref tvDYdt, ref tvY, myCell);
            HCXmit(dt, ref tvDYdt, ref tvY, myCell);

            // cytoplasm
            tvDYdt[Pd.IdxHcyt] = 0.0;
            tvDYdt[Pd.IdxPIi] = (-J_PiC + J_DCT) * Rmc + myCell.TVc[Pd.InxJ_cons];
            tvDYdt[Pd.IdxATPiTotal] = J_ANT * Rmc + J_AK + J_CK - myCell.TVc[Pd.InxJ_cons];
            tvDYdt[Pd.IdxADPiTotal] = -J_ANT * Rmc - 2 * J_AK - J_CK + myCell.TVc[Pd.InxJ_cons];
            tvDYdt[Pd.IdxPCr] = -J_CK;

            if (tvY[Pd.IdxATPiTotal] <= 0.000000000000001)
            {
                tvY[Pd.IdxATPiTotal] = 0.000000000000001;
            }
            else if (tvY[Pd.IdxATPiTotal] >= AdenineTotali)
            {
                tvY[Pd.IdxATPiTotal] = AdenineTotali - 0.000000000000001;
            }
            if (tvY[Pd.IdxADPiTotal] <= 0.000000000000001)
            {
                tvY[Pd.IdxADPiTotal] = 0.000000000000001;
            }
            if (tvY[Pd.IdxPIi] <= 0.000000000000001)
            {
                tvY[Pd.IdxPIi] = 0.000000000000001;
            }

            ATPiFree = tvY[Pd.IdxATPiTotal] / (1 + myCell.Cyt.Mgi / Kd_ATPiMg);
            myCell.TVc[Pd.InxATPiFree] = ATPiFree;

            ATPiMg = tvY[Pd.IdxATPiTotal] - ATPiFree;
            myCell.TVc[Pd.InxATPiMg] = ATPiMg;

            ADPiFree = tvY[Pd.IdxADPiTotal] / (1 + myCell.Cyt.Mgi / Kd_ADPiMg);
            myCell.TVc[Pd.InxADPiFree] = ADPiFree;

            ADPiMg = tvY[Pd.IdxADPiTotal] - ADPiFree;
            myCell.TVc[Pd.InxADPiMg] = ADPiMg;

            AMPi = AdenineTotali - (tvY[Pd.IdxATPiTotal] + tvY[Pd.IdxADPiTotal]);
            if (AMPi <= 0.000000000000001)
            {
                AMPi = 0.000000000000001;
            }
            myCell.TVc[Pd.InxAMPi] = AMPi;

            Cr = CreatineTotali - tvY[Pd.IdxPCr];
            if (Cr <= 0.000000000000001)
            {
                Cr = 0.000000000000001;
                tvY[Pd.IdxPCr] = (CreatineTotali - Cr);
            }
            else if (Cr > CreatineTotali)
            {
                Cr = CreatineTotali - 0.000000000000001;
                tvY[Pd.IdxPCr] = 0.000000000000001;
            }
            myCell.TVc[Pd.InxCr] = Cr;

            // mitochondria     
            double C_Buffmit_0 = (Math.Pow(10, -pHmit) - Math.Pow(10, -pHmit - 0.001)) / 0.001;
            R_Buff = C_Buff / C_Buffmit_0;

            tvDYdt[Pd.IdxNamit] = (-n_NCXmit * (J_NmSC + J_NmSC_block + J_NCXmit) - J_NHE);
            tvDYdt[Pd.IdxKmit] = (J_KUni - J_KHE);
            tvDYdt[Pd.IdxCamitTot] = J_CaUni_js + J_CaUni_cyt + J_NmSC + J_NmSC_block + J_NCXmit + J_HCXmit; // 24Feb09
            tvDYdt[Pd.IdxHmit] = (-4.0 * J_C1 - 2.0 * J_C3 - 4.0 * J_C4 - J_C1 +
                                   (nA_ATPsyn - 1) * J_ATPsyn + 2.0 * J_PiC + J_HLeak
                                   + J_KHE + J_NHE - 2 * J_HCXmit - J_AGC + J_MCT + J_TCT + J_CS + J_MDH) / R_Buff; // 24Feb09
            tvDYdt[Pd.IdxATPmitTotal] = (J_ATPsyn - J_ANT - J_PC - J_NDK);
            tvDYdt[Pd.IdxPImit] = (J_PiC - J_ATPsyn - J_DCT + J_PC - J_SCS);
            tvDYdt[Pd.IdxNADH] = (-J_C1 + J_PDHC + J_ICDH + J_OGDH + J_MDH);
            tvDYdt[Pd.IdxUQH2] = (J_C1 - J_C3 + J_SDH);
            tvDYdt[Pd.IdxCytc_r] = 2.0 * (J_C3 - J_C4);
            tvDYdt[Pd.IdxdPsi] = (-4.0 * J_C1 - 2.0 * J_C3 - 4.0 * J_C4 + nA_ATPsyn * J_ATPsyn
                       + J_ANT + J_HLeak - (n_NCXmit - 2) * (J_NmSC + J_NmSC_block + J_NCXmit)
                       + 2.0 * (J_CaUni_js + J_CaUni_cyt) + J_KUni - J_AGC) / Cmit;
            tvDYdt[Pd.IdxAspartateMit] = (J_AGC - J_AST);
            tvDYdt[Pd.IdxGlutamateMit] = (-J_AGC + J_ALT + J_AST);
            tvDYdt[Pd.IdxOxoglutarateMit] = (J_OGC - J_ALT + J_ICDH - J_OGDH - J_AST);
            tvDYdt[Pd.IdxMalateMit] = (-J_OGC + J_DCT - J_TCT + J_FH - J_MDH);
            tvDYdt[Pd.IdxPyruvateMit] = (J_MCT - J_PDHC + J_ALT - J_PC);
            tvDYdt[Pd.IdxCitrateMit] = (J_TCT + J_CS - J_ACO);
            tvDYdt[Pd.IdxIsocitrateMit] = (J_ACO - J_ICDH);
            tvDYdt[Pd.IdxSuccinylCoAMit] = (J_OGDH - J_SCS);
            tvDYdt[Pd.IdxFumarateMit] = (J_SDH - J_FH);
            tvDYdt[Pd.IdxSuccinateMit] = (J_SCS - J_SDH);
            tvDYdt[Pd.IdxOxaloacetateMit] = (J_PC - J_CS + J_MDH + J_AST);
            tvDYdt[Pd.IdxAcetylCoAMit] = (J_PDHC - J_CS);
            tvDYdt[Pd.IdxGTPmit] = (J_SCS + J_NDK);

            if (tvY[Pd.IdxKmit] <= 0.0)
            {
                tvY[Pd.IdxKmit] = 0.000000000001;
            }
            if (tvY[Pd.IdxHmit] <= 0.0)
            {
                tvY[Pd.IdxHmit] = 0.000000000001;
            }

            pHmit = -Math.Log10(tvY[Pd.IdxHmit] / 1000.0);
            myCell.TVc[Pd.InxpHmit] = pHmit;

            dpH = Math.Log(10) * Pd.RTF * (pHmit - pHcyt);
            myCell.TVc[Pd.InxdpH] = dpH;

            dP = dpH - tvY[Pd.IdxdPsi];
            myCell.TVc[Pd.InxdP] = dP;

            if (tvY[Pd.IdxATPmitTotal] <= 0.0)
            {
                tvY[Pd.IdxATPmitTotal] = 0.000000000000001;
            }
            ADPmitTotal = AdeninemitTotal - tvY[Pd.IdxATPmitTotal];
            if (ADPmitTotal <= 0.0)
            {
                ADPmitTotal = 0.000000000000001;
                tvY[Pd.IdxATPmitTotal] = AdeninemitTotal - ADPmitTotal;
            }
            myCell.TVc[Pd.InxADPmitTotal] = ADPmitTotal;

            ATPmitFree = tvY[Pd.IdxATPmitTotal] / (1 + MgFreemit / Kd_ATPmitMg);
            myCell.TVc[Pd.InxATPmitFree] = ATPmitFree;

            ATPmitMg = tvY[Pd.IdxATPmitTotal] - ATPmitFree;
            myCell.TVc[Pd.InxATPmitMg] = ATPmitMg;

            ADPmitFree = ADPmitTotal / (1 + MgFreemit / Kd_ADPmitMg);
            myCell.TVc[Pd.InxADPmitFree] = ADPmitFree;

            ADPmitMg = ADPmitTotal - ADPmitFree;
            myCell.TVc[Pd.InxADPmitMg] = ADPmitMg;

            if (tvY[Pd.IdxPImit] < 0)
            {
                tvY[Pd.IdxPImit] = 0.000000000000001;
            }

            R_NAD_to_NADH = NAD / tvY[Pd.IdxNADH];
            Em_NAD = Em_NAD0 + Pd.RTF2 * Math.Log(R_NAD_to_NADH);
            Em_UQ = Em_UQ0 + Pd.RTF2 * Math.Log(UQ / tvY[Pd.IdxUQH2]);
            Em_cytc = Em_cytc0 + Pd.RTF * Math.Log(Cytc_o / tvY[Pd.IdxCytc_r]);
            Ema = Em_cytc + dP - tvY[Pd.IdxdPsi];
            R_cytaO_to_R = Math.Exp(Pd.F / Pd.R / Pd.T * (Ema - Ema0));

            if (tvY[Pd.IdxNADH] <= 0.0)
            {
                tvY[Pd.IdxNADH] = 0.000000000000001;
            }
            NAD = NAD_Total - tvY[Pd.IdxNADH];
            if (NAD <= 0.0)
            {
                NAD = 0.000000000000001;
                tvY[Pd.IdxNADH] = NAD_Total - NAD;
            }
            myCell.TVc[Pd.InxNAD] = NAD;

            if (tvY[Pd.IdxUQH2] <= 0.0)
            {
                tvY[Pd.IdxUQH2] = 0.000000000000001;
            }
            UQ = Ubiquinone_Total - tvY[Pd.IdxUQH2];
            if (UQ <= 0.0)
            {
                UQ = 0.000000000000001;
                tvY[Pd.IdxUQH2] = Ubiquinone_Total - UQ;
            }
            myCell.TVc[Pd.InxUQ] = UQ;

            if (tvY[Pd.IdxCytc_r] <= 0.0)
            {
                tvY[Pd.IdxCytc_r] = 0.000000000000001;
            }
            Cytc_o = Cytc_Total - tvY[Pd.IdxCytc_r];
            if (Cytc_o <= 0.0)
            {
                Cytc_o = 0.000000000000001;
                tvY[Pd.IdxCytc_r] = Cytc_Total - Cytc_o;
            }
            myCell.TVc[Pd.InxCytc_o] = Cytc_o;

            Cyta_r = Cyta_Total / (1.0 + R_cytaO_to_R);
            if (Cyta_r <= 0.0)
            {
                Cyta_r = 1.0E-50;
            }
            myCell.TVc[Pd.InxCyta_r] = Cyta_r;

            Cyta_o = Cyta_Total - Cyta_r;
            if (Cyta_o <= 0.0)
            {
                Cyta_o = 1.0E-50;
            }
            myCell.TVc[Pd.InxCyta_o] = Cyta_o;

            if (tvY[Pd.IdxOxaloacetateMit] < 0)
            {
                tvY[Pd.IdxOxaloacetateMit] = 0.000000000000001;
            }
            if (tvY[Pd.IdxPyruvateMit] < 0)
            {
                tvY[Pd.IdxPyruvateMit] = 0.000000000000001;
            }
            if (tvY[Pd.IdxAspartateMit] < 0)
            {
                tvY[Pd.IdxAspartateMit] = 0.000000000000001;
            }
            if (tvY[Pd.IdxGlutamateMit] < 0)
            {
                tvY[Pd.IdxGlutamateMit] = 0.000000000000001;
            }
            if (tvY[Pd.IdxOxoglutarateMit] < 0)
            {
                tvY[Pd.IdxOxoglutarateMit] = 0.000000000000001;
            }
            if (tvY[Pd.IdxMalateMit] < 0)
            {
                tvY[Pd.IdxMalateMit] = 0.000000000000001;
            }
            if (tvY[Pd.IdxCitrateMit] < 0)
            {
                tvY[Pd.IdxCitrateMit] = 0.000000000000001;
            }
            if (tvY[Pd.IdxIsocitrateMit] < 0)
            {
                tvY[Pd.IdxIsocitrateMit] = 0.000000000000001;
            }
            if (tvY[Pd.IdxSuccinylCoAMit] < 0)
            {
                tvY[Pd.IdxSuccinylCoAMit] = 0.000000000000001;
            }
            if (tvY[Pd.IdxFumarateMit] < 0)
            {
                tvY[Pd.IdxFumarateMit] = 0.000000000000001;
            }
            if (tvY[Pd.IdxSuccinateMit] < 0)
            {
                tvY[Pd.IdxSuccinateMit] = 0.000000000000001;
            }
            if (tvY[Pd.IdxAcetylCoAMit] < 0)
            {
                tvY[Pd.IdxAcetylCoAMit] = 0.000000000000001;
            }
            IsocitrateFreeMit = tvY[Pd.IdxIsocitrateMit] / (1 + MgFreemit / KdIsocitrateMit);
            myCell.TVc[Pd.InxIsocitrateFreeMit] = IsocitrateFreeMit;

            IsocitrateMgMit = tvY[Pd.IdxIsocitrateMit] - IsocitrateFreeMit;
            myCell.TVc[Pd.InxIsocitrateMgMit] = IsocitrateMgMit;

            CoAMit = CoAMit_Total - tvY[Pd.IdxAcetylCoAMit] - tvY[Pd.IdxSuccinylCoAMit];
            if (CoAMit < 0)
            {
                CoAMit = 0;
            }
            myCell.TVc[Pd.InxCoAMit] = CoAMit;

            if (tvY[Pd.IdxGTPmit] <= 0.0)
            {
                tvY[Pd.IdxGTPmit] = 0.000000000000001;
            }

            GDPmit = GuanosinemitTotal - tvY[Pd.IdxGTPmit];
            if (GDPmit <= 0.0)
            {
                GDPmit = 0.000000000000001;
                tvY[Pd.IdxGTPmit] = GuanosinemitTotal - GDPmit;
            }
            myCell.TVc[Pd.InxGDPmit] = GDPmit;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpMitochondria_ListView;

            ListView.LVDispValue("Mit", IxHcyt, ref myTVc[Pd.IdxHcyt]);
            ListView.LVDispValue("Mit", IxpHcyt, ref pHcyt);
            ListView.LVDispValue("Mit", IxKd_ATPiMg, ref Kd_ATPiMg);
            ListView.LVDispValue("Mit", IxATPiTotal, ref myTVc[Pd.IdxATPiTotal]);
            ListView.LVDispValue("Mit", IxATPiFree, ref ATPiFree);
            ListView.LVDispValue("Mit", IxATPiMg, ref ATPiMg);
            ListView.LVDispValue("Mit", IxKd_ADPiMg, ref Kd_ADPiMg);
            ListView.LVDispValue("Mit", IxADPiTotal, ref myTVc[Pd.IdxADPiTotal]);
            ListView.LVDispValue("Mit", IxADPiFree, ref ADPiFree);
            ListView.LVDispValue("Mit", IxADPiMg, ref ADPiMg);
            ListView.LVDispValue("Mit", IxAMPi, ref AMPi);
            ListView.LVDispValue("Mit", IxAdenineTotali, ref AdenineTotali);
            ListView.LVDispValue("Mit", IxCreatineTotali, ref CreatineTotali);
            ListView.LVDispValue("Mit", IxPCr, ref myTVc[Pd.IdxPCr]);
            ListView.LVDispValue("Mit", IxCr, ref Cr);
            ListView.LVDispValue("Mit", IxPIi, ref myTVc[Pd.IdxPIi]);
            ListView.LVDispValue("Mit", IxC_Buffi, ref C_Buffi);
            ListView.LVDispValue("Mit", IxR_Buffi, ref R_Buffi);
            ListView.LVDispValue("Mit", IxMalatei, ref Malatei);
            ListView.LVDispValue("Mit", IxGlutamatei, ref Glutamatei);
            ListView.LVDispValue("Mit", IxAspartatei, ref Aspartatei);
            ListView.LVDispValue("Mit", IxPyruvatei, ref Pyruvatei);
            ListView.LVDispValue("Mit", IxOxoglutaratei, ref Oxoglutaratei);
            ListView.LVDispValue("Mit", IxCitratei, ref Citratei);

            ListView.LVDispValue("Mit", IxVmit_part, ref Vmit_part);
            ListView.LVDispValue("Mit", IxVmit, ref Vmit);
            ListView.LVDispValue("Mit", IxRmc, ref Rmc);
            ListView.LVDispValue("Mit", IxNamit, ref myTVc[Pd.IdxNamit]);
            ListView.LVDispValue("Mit", IxKmit, ref myTVc[Pd.IdxKmit]);
            ListView.LVDispValue("Mit", IxCamitTot, ref myTVc[Pd.IdxCamitTot]);
            ListView.LVDispValue("Mit", IxCamit, ref myTVc[Pd.InxCamit]);
            ListView.LVDispValue("Mit", IxCamit_Min, ref Camit_Min);
            ListView.LVDispValue("Mit", IxCamit_Max, ref Camit_Max);
            ListView.LVDispValue("Mit", IxMgFreemit, ref MgFreemit);
            ListView.LVDispValue("Mit", IxHmit, ref myTVc[Pd.IdxHmit]);
            ListView.LVDispValue("Mit", IxpHmit, ref pHmit);
            ListView.LVDispValue("Mit", IxdP, ref dP);
            ListView.LVDispValue("Mit", IxdPsi, ref myTVc[Pd.IdxdPsi]);
            ListView.LVDispValue("Mit", IxdpH, ref dpH);
            ListView.LVDispValue("Mit", IxCmit, ref Cmit);
            ListView.LVDispValue("Mit", IxKd_ATPmitMg, ref Kd_ATPmitMg);
            ListView.LVDispValue("Mit", IxATPmitTotal, ref myTVc[Pd.IdxATPmitTotal]);
            ListView.LVDispValue("Mit", IxATPmitFree, ref ATPmitFree);
            ListView.LVDispValue("Mit", IxATPmitMg, ref ATPmitMg);
            ListView.LVDispValue("Mit", IxKd_ADPmitMg, ref Kd_ADPmitMg);
            ListView.LVDispValue("Mit", IxADPmitTotal, ref ADPmitTotal);
            ListView.LVDispValue("Mit", IxADPmitFree, ref ADPmitFree);
            ListView.LVDispValue("Mit", IxADPmitMg, ref ADPmitMg);
            ListView.LVDispValue("Mit", IxAdeninemitTotal, ref AdeninemitTotal);
            ListView.LVDispValue("Mit", IxGuanosinemitTotal, ref GuanosinemitTotal);
            ListView.LVDispValue("Mit", IxGTPmit, ref myTVc[Pd.IdxGTPmit]);
            ListView.LVDispValue("Mit", IxGDPmit, ref GDPmit);
            ListView.LVDispValue("Mit", IxPImit, ref myTVc[Pd.IdxPImit]);
            ListView.LVDispValue("Mit", IxEm_NAD0, ref Em_NAD0);
            ListView.LVDispValue("Mit", IxEm_NAD, ref Em_NAD);
            ListView.LVDispValue("Mit", IxNAD_Total, ref NAD_Total);
            ListView.LVDispValue("Mit", IxNADH, ref myTVc[Pd.IdxNADH]);
            ListView.LVDispValue("Mit", IxNAD, ref NAD);
            ListView.LVDispValue("Mit", IxR_NAD_to_NADH, ref R_NAD_to_NADH);
            ListView.LVDispValue("Mit", IxEm_cytc0, ref Em_cytc0);
            ListView.LVDispValue("Mit", IxEm_cytc, ref Em_cytc);
            ListView.LVDispValue("Mit", IxCytc_Total, ref Cytc_Total);
            ListView.LVDispValue("Mit", IxCytc_r, ref myTVc[Pd.IdxCytc_r]);
            ListView.LVDispValue("Mit", IxCytc_o, ref Cytc_o);
            ListView.LVDispValue("Mit", IxEma0, ref Ema0);
            ListView.LVDispValue("Mit", IxEma, ref Ema);
            ListView.LVDispValue("Mit", IxCyta_Total, ref Cyta_Total);
            ListView.LVDispValue("Mit", IxCyta_r, ref Cyta_r);
            ListView.LVDispValue("Mit", IxCyta_o, ref Cyta_o);
            ListView.LVDispValue("Mit", IxR_cytaO_to_R, ref R_cytaO_to_R);
            ListView.LVDispValue("Mit", IxEm_UQ0, ref Em_UQ0);
            ListView.LVDispValue("Mit", IxEm_UQ, ref Em_UQ);
            ListView.LVDispValue("Mit", IxUbiquinone_Total, ref Ubiquinone_Total);
            ListView.LVDispValue("Mit", IxUQH2, ref myTVc[Pd.IdxUQH2]);
            ListView.LVDispValue("Mit", IxUQ, ref UQ);
            ListView.LVDispValue("Mit", IxCoAMit_Total, ref CoAMit_Total);
            ListView.LVDispValue("Mit", IxCoAMit, ref CoAMit);
            ListView.LVDispValue("Mit", IxAcetylCoAMit, ref myTVc[Pd.IdxAcetylCoAMit]);
            ListView.LVDispValue("Mit", IxSuccinylCoAMit, ref myTVc[Pd.IdxSuccinylCoAMit]);
            ListView.LVDispValue("Mit", IxCO2mit, ref CO2mit);
            ListView.LVDispValue("Mit", IxHCO3mit, ref HCO3mit);
            ListView.LVDispValue("Mit", IxO2, ref O2);
            ListView.LVDispValue("Mit", IxC_Buff, ref C_Buff);
            ListView.LVDispValue("Mit", IxR_Buff, ref R_Buff);
            ListView.LVDispValue("Mit", IxMalateMit, ref myTVc[Pd.IdxMalateMit]);
            ListView.LVDispValue("Mit", IxGlutamateMit, ref myTVc[Pd.IdxGlutamateMit]);
            ListView.LVDispValue("Mit", IxAspartateMit, ref myTVc[Pd.IdxAspartateMit]);
            ListView.LVDispValue("Mit", IxPyruvateMit, ref myTVc[Pd.IdxPyruvateMit]);
            ListView.LVDispValue("Mit", IxOxoglutarateMit, ref myTVc[Pd.IdxOxoglutarateMit]);
            ListView.LVDispValue("Mit", IxCitrateMit, ref myTVc[Pd.IdxCitrateMit]);
            ListView.LVDispValue("Mit", IxOxaloacetateMit, ref myTVc[Pd.IdxOxaloacetateMit]);
            ListView.LVDispValue("Mit", IxKdIsocitrateMit, ref KdIsocitrateMit);
            ListView.LVDispValue("Mit", IxIsocitrateMit, ref myTVc[Pd.IdxIsocitrateMit]);
            ListView.LVDispValue("Mit", IxIsocitrateFreeMit, ref IsocitrateFreeMit);
            ListView.LVDispValue("Mit", IxIsocitrateMgMit, ref IsocitrateMgMit);
            ListView.LVDispValue("Mit", IxSuccinateMit, ref myTVc[Pd.IdxSuccinateMit]);
            ListView.LVDispValue("Mit", IxFumarateMit, ref myTVc[Pd.IdxFumarateMit]);
            ListView.LVDispValue("Mit", IxAlanineMit, ref AlanineMit);

            ListView.LVDispValue("Mit", IxJ_C1, ref J_C1);
            ListView.LVDispValue("Mit", IxJ_C3, ref J_C3);
            ListView.LVDispValue("Mit", IxJ_C4, ref J_C4);
            ListView.LVDispValue("Mit", IxVO2, ref VO2);
            ListView.LVDispValue("Mit", IxJ_ATPsyn, ref J_ATPsyn);
            ListView.LVDispValue("Mit", IxJ_ANT, ref J_ANT);
            ListView.LVDispValue("Mit", IxJ_PiC, ref J_PiC);
            ListView.LVDispValue("Mit", IxJ_HLeak, ref J_HLeak);
            ListView.LVDispValue("Mit", IxJ_CS, ref J_CS);
            ListView.LVDispValue("Mit", IxJ_ACO, ref J_ACO);
            ListView.LVDispValue("Mit", IxJ_ICDH, ref J_ICDH);
            ListView.LVDispValue("Mit", IxJ_OGDH, ref J_OGDH);
            ListView.LVDispValue("Mit", IxJ_SCS, ref J_SCS);
            ListView.LVDispValue("Mit", IxJ_SDH, ref J_SDH);
            ListView.LVDispValue("Mit", IxJ_FH, ref J_FH);
            ListView.LVDispValue("Mit", IxJ_MDH, ref J_MDH);
            ListView.LVDispValue("Mit", IxJ_NDK, ref J_NDK);
            ListView.LVDispValue("Mit", IxJ_AST, ref J_AST);
            ListView.LVDispValue("Mit", IxJ_PDHC, ref J_PDHC);
            ListView.LVDispValue("Mit", IxJ_PC, ref J_PC);
            ListView.LVDispValue("Mit", IxJ_ALT, ref J_ALT);
            ListView.LVDispValue("Mit", IxJ_MCT, ref J_MCT);
            ListView.LVDispValue("Mit", IxJ_DCT, ref J_DCT);
            ListView.LVDispValue("Mit", IxJ_TCT, ref J_TCT);
            ListView.LVDispValue("Mit", IxJ_OGC, ref J_OGC);
            ListView.LVDispValue("Mit", IxJ_AGC, ref J_AGC);
            ListView.LVDispValue("Mit", IxJ_KUni, ref J_KUni);
            ListView.LVDispValue("Mit", IxJ_KHE, ref J_KHE);
            ListView.LVDispValue("Mit", IxJ_CaUni_cyt, ref J_CaUni_cyt);
            ListView.LVDispValue("Mit", IxF_CaUni_cyt, ref F_CaUni_cyt);
            ListView.LVDispValue("Mit", IxJ_CaUni_js, ref J_CaUni_js);
            ListView.LVDispValue("Mit", IxF_CaUni_js, ref F_CaUni_js);
            ListView.LVDispValue("Mit", IxJ_NCXmit, ref J_NCXmit);
            ListView.LVDispValue("Mit", IxF_NCXmit, ref F_NCXmit);
            ListView.LVDispValue("Mit", IxJ_NmSC, ref J_NmSC);
            ListView.LVDispValue("Mit", IxF_NmSC, ref F_NmSC);
            ListView.LVDispValue("Mit", IxJ_NmSC_block, ref J_NmSC_block);
            ListView.LVDispValue("Mit", IxF_NmSC_block, ref F_NmSC_block);
            ListView.LVDispValue("Mit", IxdATPuse_NmSC, ref dATPuse_NmSC);
            ListView.LVDispValue("Mit", IxJ_NHE, ref J_NHE);
            ListView.LVDispValue("Mit", IxJ_HCXmit, ref J_HCXmit); // 24Feb08
            ListView.LVDispValue("Mit", IxJ_AK, ref J_AK);
            ListView.LVDispValue("Mit", IxJ_CK, ref J_CK);
        }
        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpMitochondria_ListView;
            ListView.LVModiValue("Mit", IxHcyt, ref myTVc[Pd.IdxHcyt]);
            ListView.LVModiValue("Mit", IxpHcyt, ref pHcyt);
            ListView.LVModiValue("Mit", IxKd_ATPiMg, ref Kd_ATPiMg);
            ListView.LVModiValue("Mit", IxATPiTotal, ref myTVc[Pd.IdxATPiTotal]);
            ListView.LVModiValue("Mit", IxATPiFree, ref ATPiFree);
            ListView.LVModiValue("Mit", IxATPiMg, ref ATPiMg);
            ListView.LVModiValue("Mit", IxKd_ADPiMg, ref Kd_ADPiMg);
            ListView.LVModiValue("Mit", IxADPiTotal, ref myTVc[Pd.IdxADPiTotal]);
            ListView.LVModiValue("Mit", IxADPiFree, ref ADPiFree);
            ListView.LVModiValue("Mit", IxADPiMg, ref ADPiMg);
            ListView.LVModiValue("Mit", IxAMPi, ref AMPi);
            ListView.LVModiValue("Mit", IxAdenineTotali, ref AdenineTotali);
            ListView.LVModiValue("Mit", IxCreatineTotali, ref CreatineTotali);
            ListView.LVModiValue("Mit", IxPCr, ref myTVc[Pd.IdxPCr]);
            ListView.LVModiValue("Mit", IxCr, ref Cr);
            ListView.LVModiValue("Mit", IxPIi, ref myTVc[Pd.IdxPIi]);
            ListView.LVModiValue("Mit", IxC_Buffi, ref C_Buffi);
            ListView.LVModiValue("Mit", IxR_Buffi, ref R_Buffi);
            ListView.LVModiValue("Mit", IxMalatei, ref Malatei);
            ListView.LVModiValue("Mit", IxGlutamatei, ref Glutamatei);
            ListView.LVModiValue("Mit", IxAspartatei, ref Aspartatei);
            ListView.LVModiValue("Mit", IxPyruvatei, ref Pyruvatei);
            ListView.LVModiValue("Mit", IxOxoglutaratei, ref Oxoglutaratei);
            ListView.LVModiValue("Mit", IxCitratei, ref Citratei);

            ListView.LVModiValue("Mit", IxVmit_part, ref Vmit_part);
            ListView.LVModiValue("Mit", IxVmit, ref Vmit);
            ListView.LVModiValue("Mit", IxRmc, ref Rmc);
            ListView.LVModiValue("Mit", IxNamit, ref myTVc[Pd.IdxNamit]);
            ListView.LVModiValue("Mit", IxKmit, ref myTVc[Pd.IdxKmit]);
            ListView.LVModiValue("Mit", IxCamitTot, ref myTVc[Pd.IdxCamitTot]);
            ListView.LVModiValue("Mit", IxCamit, ref myTVc[Pd.InxCamit]);
            ListView.LVModiValue("Mit", IxCamit_Min, ref Camit_Min);
            ListView.LVModiValue("Mit", IxCamit_Max, ref Camit_Max);
            ListView.LVModiValue("Mit", IxMgFreemit, ref MgFreemit);
            ListView.LVModiValue("Mit", IxHmit, ref myTVc[Pd.IdxHmit]);
            ListView.LVModiValue("Mit", IxpHmit, ref pHmit);
            ListView.LVModiValue("Mit", IxdP, ref dP);
            ListView.LVModiValue("Mit", IxdPsi, ref myTVc[Pd.IdxdPsi]);
            ListView.LVModiValue("Mit", IxdpH, ref dpH);
            ListView.LVModiValue("Mit", IxCmit, ref Cmit);
            ListView.LVModiValue("Mit", IxKd_ATPmitMg, ref Kd_ATPmitMg);
            ListView.LVModiValue("Mit", IxATPmitTotal, ref myTVc[Pd.IdxATPmitTotal]);
            ListView.LVModiValue("Mit", IxATPmitFree, ref ATPmitFree);
            ListView.LVModiValue("Mit", IxATPmitMg, ref ATPmitMg);
            ListView.LVModiValue("Mit", IxKd_ADPmitMg, ref Kd_ADPmitMg);
            ListView.LVModiValue("Mit", IxADPmitTotal, ref ADPmitTotal);
            ListView.LVModiValue("Mit", IxADPmitFree, ref ADPmitFree);
            ListView.LVModiValue("Mit", IxADPmitMg, ref ADPmitMg);
            ListView.LVModiValue("Mit", IxAdeninemitTotal, ref AdeninemitTotal);
            ListView.LVModiValue("Mit", IxGuanosinemitTotal, ref GuanosinemitTotal);
            ListView.LVModiValue("Mit", IxGTPmit, ref myTVc[Pd.IdxGTPmit]);
            ListView.LVModiValue("Mit", IxGDPmit, ref GDPmit);
            ListView.LVModiValue("Mit", IxPImit, ref myTVc[Pd.IdxPImit]);
            ListView.LVModiValue("Mit", IxEm_NAD0, ref Em_NAD0);
            ListView.LVModiValue("Mit", IxEm_NAD, ref Em_NAD);
            ListView.LVModiValue("Mit", IxNAD_Total, ref NAD_Total);
            ListView.LVModiValue("Mit", IxNADH, ref myTVc[Pd.IdxNADH]);
            ListView.LVModiValue("Mit", IxNAD, ref NAD);
            ListView.LVModiValue("Mit", IxR_NAD_to_NADH, ref R_NAD_to_NADH);
            ListView.LVModiValue("Mit", IxEm_cytc0, ref Em_cytc0);
            ListView.LVModiValue("Mit", IxEm_cytc, ref Em_cytc);
            ListView.LVModiValue("Mit", IxCytc_Total, ref Cytc_Total);
            ListView.LVModiValue("Mit", IxCytc_r, ref myTVc[Pd.IdxCytc_r]);
            ListView.LVModiValue("Mit", IxCytc_o, ref Cytc_o);
            ListView.LVModiValue("Mit", IxEma0, ref Ema0);
            ListView.LVModiValue("Mit", IxEma, ref Ema);
            ListView.LVModiValue("Mit", IxCyta_Total, ref Cyta_Total);
            ListView.LVModiValue("Mit", IxCyta_r, ref Cyta_r);
            ListView.LVModiValue("Mit", IxCyta_o, ref Cyta_o);
            ListView.LVModiValue("Mit", IxR_cytaO_to_R, ref R_cytaO_to_R);
            ListView.LVModiValue("Mit", IxEm_UQ0, ref Em_UQ0);
            ListView.LVModiValue("Mit", IxEm_UQ, ref Em_UQ);
            ListView.LVModiValue("Mit", IxUbiquinone_Total, ref Ubiquinone_Total);
            ListView.LVModiValue("Mit", IxUQH2, ref myTVc[Pd.IdxUQH2]);
            ListView.LVModiValue("Mit", IxUQ, ref UQ);
            ListView.LVModiValue("Mit", IxCoAMit_Total, ref CoAMit_Total);
            ListView.LVModiValue("Mit", IxCoAMit, ref CoAMit);
            ListView.LVModiValue("Mit", IxAcetylCoAMit, ref myTVc[Pd.IdxAcetylCoAMit]);
            ListView.LVModiValue("Mit", IxSuccinylCoAMit, ref myTVc[Pd.IdxSuccinylCoAMit]);
            ListView.LVModiValue("Mit", IxCO2mit, ref CO2mit);
            ListView.LVModiValue("Mit", IxHCO3mit, ref HCO3mit);
            ListView.LVModiValue("Mit", IxO2, ref O2);
            ListView.LVModiValue("Mit", IxC_Buff, ref C_Buff);
            ListView.LVModiValue("Mit", IxR_Buff, ref R_Buff);
            ListView.LVModiValue("Mit", IxMalateMit, ref myTVc[Pd.IdxMalateMit]);
            ListView.LVModiValue("Mit", IxGlutamateMit, ref myTVc[Pd.IdxGlutamateMit]);
            ListView.LVModiValue("Mit", IxAspartateMit, ref myTVc[Pd.IdxAspartateMit]);
            ListView.LVModiValue("Mit", IxPyruvateMit, ref myTVc[Pd.IdxPyruvateMit]);
            ListView.LVModiValue("Mit", IxOxoglutarateMit, ref myTVc[Pd.IdxOxoglutarateMit]);
            ListView.LVModiValue("Mit", IxCitrateMit, ref myTVc[Pd.IdxCitrateMit]);
            ListView.LVModiValue("Mit", IxOxaloacetateMit, ref myTVc[Pd.IdxOxaloacetateMit]);
            ListView.LVModiValue("Mit", IxKdIsocitrateMit, ref KdIsocitrateMit);
            ListView.LVModiValue("Mit", IxIsocitrateMit, ref myTVc[Pd.IdxIsocitrateMit]);
            ListView.LVModiValue("Mit", IxIsocitrateFreeMit, ref IsocitrateFreeMit);
            ListView.LVModiValue("Mit", IxIsocitrateMgMit, ref IsocitrateMgMit);
            ListView.LVModiValue("Mit", IxSuccinateMit, ref myTVc[Pd.IdxSuccinateMit]);
            ListView.LVModiValue("Mit", IxFumarateMit, ref myTVc[Pd.IdxFumarateMit]);
            ListView.LVModiValue("Mit", IxAlanineMit, ref AlanineMit);

            ListView.LVModiValue("Mit", IxJ_C1, ref J_C1);
            ListView.LVModiValue("Mit", IxJ_C3, ref J_C3);
            ListView.LVModiValue("Mit", IxJ_C4, ref J_C4);
            ListView.LVModiValue("Mit", IxVO2, ref VO2);
            ListView.LVModiValue("Mit", IxJ_ATPsyn, ref J_ATPsyn);
            ListView.LVModiValue("Mit", IxJ_ANT, ref J_ANT);
            ListView.LVModiValue("Mit", IxJ_PiC, ref J_PiC);
            ListView.LVModiValue("Mit", IxJ_HLeak, ref J_HLeak);
            ListView.LVModiValue("Mit", IxJ_CS, ref J_CS);
            ListView.LVModiValue("Mit", IxJ_ACO, ref J_ACO);
            ListView.LVModiValue("Mit", IxJ_ICDH, ref J_ICDH);
            ListView.LVModiValue("Mit", IxJ_OGDH, ref J_OGDH);
            ListView.LVModiValue("Mit", IxJ_SCS, ref J_SCS);
            ListView.LVModiValue("Mit", IxJ_SDH, ref J_SDH);
            ListView.LVModiValue("Mit", IxJ_FH, ref J_FH);
            ListView.LVModiValue("Mit", IxJ_MDH, ref J_MDH);
            ListView.LVModiValue("Mit", IxJ_NDK, ref J_NDK);
            ListView.LVModiValue("Mit", IxJ_AST, ref J_AST);
            ListView.LVModiValue("Mit", IxJ_PDHC, ref J_PDHC);
            ListView.LVModiValue("Mit", IxJ_PC, ref J_PC);
            ListView.LVModiValue("Mit", IxJ_ALT, ref J_ALT);
            ListView.LVModiValue("Mit", IxJ_MCT, ref J_MCT);
            ListView.LVModiValue("Mit", IxJ_DCT, ref J_DCT);
            ListView.LVModiValue("Mit", IxJ_TCT, ref J_TCT);
            ListView.LVModiValue("Mit", IxJ_OGC, ref J_OGC);
            ListView.LVModiValue("Mit", IxJ_AGC, ref J_AGC);
            ListView.LVModiValue("Mit", IxJ_KUni, ref J_KUni);
            ListView.LVModiValue("Mit", IxJ_KHE, ref J_KHE);
            ListView.LVModiValue("Mit", IxJ_CaUni_cyt, ref J_CaUni_cyt);
            ListView.LVModiValue("Mit", IxF_CaUni_cyt, ref F_CaUni_cyt);
            ListView.LVModiValue("Mit", IxJ_CaUni_js, ref J_CaUni_js);
            ListView.LVModiValue("Mit", IxF_CaUni_js, ref F_CaUni_js);
            ListView.LVModiValue("Mit", IxJ_NCXmit, ref J_NCXmit);
            ListView.LVModiValue("Mit", IxF_NCXmit, ref F_NCXmit);
            ListView.LVModiValue("Mit", IxJ_NmSC, ref J_NmSC);
            ListView.LVModiValue("Mit", IxF_NmSC, ref F_NmSC);
            ListView.LVModiValue("Mit", IxJ_NmSC_block, ref J_NmSC_block);
            ListView.LVModiValue("Mit", IxF_NmSC_block, ref F_NmSC_block);
            ListView.LVModiValue("Mit", IxdATPuse_NmSC, ref dATPuse_NmSC);
            ListView.LVModiValue("Mit", IxJ_NHE, ref J_NHE);
            ListView.LVModiValue("Mit", IxJ_HCXmit, ref J_HCXmit); // 24Feb08
            ListView.LVModiValue("Mit", IxJ_AK, ref J_AK);
            ListView.LVModiValue("Mit", IxJ_CK, ref J_CK);
        }
    }
}
