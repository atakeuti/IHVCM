using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class Pd
    {
        public struct TPBoxProperty
        {
            public double XMin;
            public double XMax;
            public double X0;
            public double XCal;
            public double YMin;
            public double YMax;
            public double Y0;
            public double YCal;
        };
        public struct TIdx
        {
            public int TType;
            public int n;
            public string Name;
        };

        public struct TIons
        {
            public double Na;
            public double Najs;
            public double Nasl;
            public double K;
            public double Ca;
            public double Cajs;
            public double Casl;
            public double Cl;
            public double Mg;
        };

        public struct TModelSetting
        {
            public int intClampMode;			//Clamp mode: CurrentClamp = 0;  VoltageClamp = 1;
            public double SimTimeMax;
            public double LDispTimeMax;
            public double SDispTimeMax;
            public double StimCycle;
            public double StimAmp;
            public double PulseOnset;			//Onset time (msec) of the test pulse
            public double PulseOffset;          //Offset time (msec) of the test pulse
            public double CC_Onset;
            public double CC_Offset;
        };

        public struct TmyPenS
        {
            public int i;
            public Point P0;
            public Point P1;
            public System.Drawing.Color MyColor;
        };

        static public int mTVIdxMax = 300;
        static public int mTVMax = 500000;
        static public int mTVMax_beat = 50000;
        static public int mTVIdxLast;
        static public int mTVIdxLast_beat;


        public class mTV_VStr
        {
            public int Idx;
            public string Str;
            public double[] Val = new double[mTVMax]; // max 100000 points 
        }

        public class mTV_VStr2
        {
            public int Idx;
            public string Str;
            public double[] Val = new double[mTVMax_beat]; // max 100000 points 
        }

        public class TdVdTPeak
        {
            public double SDTime;
            public double ElpTime;
            public double dVdTPeak;
            public bool blDetect;
        }

        static public double R = 8.3143;				// Gas constant
        static public double T = 310.15;				// absolute temperature
        static public double F = 96.4867;				// Faraday const. Coulomb/mmol

        static public double RTF = R * T / F;			//  valent ions
        static public double RTF2 = R * T / F / 2.0;		// for divalent ions

        static public int IntVar = 1;
        static public int IntPar = 2;

        static public TModelSetting MSet;
        static public bool BlnFirstLoad;
        static public bool BlnFirstPlot_SDisp;
        static public bool BlnFirstPlot_LDisp;

        static public bool BlnFirstPLFormLoad = true;

        //index of parameters
        //that enter Runge-Kutta
        static public int IdxVm = 0;   //    membrane potential
        static public int IdxNai = 1;   //    Free Na concentration in cytoplasm
        static public int IdxKi = 2;   //    Free K concentration in cytoplasm
        static public int IdxCai = 3;   //    Free Ca concentration in cytoplasm
        static public int IdxCli = 4;   //   Free Cl concentration in cytoplasm

        static public int IdxCajs = 5;   //    Free Ca concentration in junctional space
        static public int IdxCasl = 6;   //    Free Ca concentration in subsarcolemmal space

        static public int IdxNajs = 7;   //    Free Na concentration in junctional space
        static public int IdxNasl = 8;   //    Free Na concentration in subsarcolemmal space

        static public int IdxCasr = 9; // Free Ca concentration in SR

        static public int IdxINa_m_js = 10; // m gate of INajs
        static public int IdxINa_h_js = 11; // h gate of INajs
        static public int IdxINa_j_js = 12; // j gate of INajs
        static public int IdxINa_m_sl = 13; // m gate of INasl
        static public int IdxINa_h_sl = 14; // h gate of INasl
        static public int IdxINa_j_sl = 15; // j gate of INasl

        static public int IdxIKr_xkr_js = 16; // gate of IKrjs
        static public int IdxIKr_xkr_sl = 17; // gate of IKrsl

        static public int IdxIKs_xks_js = 18; // gate of IKsjs
        static public int IdxIKs_xks_sl = 19; // gate of IKssl

        static public int IdxICaL_d_js = 20; // d gate of ICaLjs
        static public int IdxICaL_f_js = 21; // f gate of ICaLjs
        static public int IdxICaL_fCaB_js = 22; //fCaB gate of ICaLjs
        static public int IdxICaL_d_sl = 23; // d gate of ICaLsl
        static public int IdxICaL_f_sl = 24; // f gate of ICaLsl
        static public int IdxICaL_fCaB_sl = 25; // fCaB gate of ICaLsl

        static public int IdxItos_xtos_js = 26; // x gate of Itosjs
        static public int IdxItos_ytos_js = 27; // y gate of Itosjs
        static public int IdxItos_xtos_sl = 28; // x gate of Itossl
        static public int IdxItos_ytos_sl = 29; // y gate of Itossl

        static public int IdxItof_xtof_js = 30; // x gate of Itofjs
        static public int IdxItof_ytof_js = 31; // y gate of Itofjs
        static public int IdxItof_xtof_sl = 32; // x gate of Itofsl
        static public int IdxItof_ytof_sl = 33; // y gate of Itofsl

        static public int IdxbTnChCa = 34; //  Ca bound troponinC high affinity site in cytoplasm
        static public int IdxbTnChMg = 35; // Mg bound troponinC high affinity site in cytoplasm
        static public int IdxbCaM = 36; // bound CaM in cytoplasm
        static public int IdxbMyocinCa = 37; // Ca bound Myocin in cytoplasm
        static public int IdxbMyocinMg = 38; // Mg bound Myocin in cytoplasm
        static public int IdxbSRB = 39; // bound SRB in cytoplasm

        static public int IdxbCabuffjs_low = 40; // bound Ca buffer low affinity site in junctional space
        static public int IdxbCabuffjs_high = 41; // bound Ca buffer high affinity site in junctional space
        static public int IdxbCabuffsl_low = 42; // bound Ca buffer low affinity site in subsarcolemmal space
        static public int IdxbCabuffsl_high = 43; // bound Ca buffer high affinity site in subsarcolemmal space

        static public int IdxbNabuffjs = 44; // bound Na buffer in junctional space
        static public int IdxbNabuffsl = 45; // bound Na buffer in subsarcolemmal space

        static public int IdxRyR_R = 46; //  R state of RyR channel
        static public int IdxRyR_O = 47; //  O state of RyR channel
        static public int IdxRyR_I = 48; //  I state of RyR channel

        static public int IdxbCSQN = 49; // bound calsequestrin

        static public int IdxTrnCa = 50;
        static public int IdxTrnCa_cb = 51;
        static public int IdxTrn_cb = 52;
        static public int IdxhsmX = 53;
        static public int IdxhsmL = 54;
        static public int IdxV_hsmL = 55;

        // Mitochondria model by Saito et al., J Physiol, 2016
        static public int IdxNADH = 56;
        static public int IdxUQH2 = 57;
        static public int IdxCytc_r = 58;
        static public int IdxO2 = 59;
        static public int IdxHmit = 60;
        static public int IdxKmit = 61;
        static public int IdxATPmitTotal = 62;
        static public int IdxPImit = 63;
        static public int IdxdPsi = 64;
        static public int IdxNamit = 65;

        //static public int IdxCamit = 66;
        static public int IdxCamitTot = 66;

        static public int IdxbCabuff_mit=67;
        static public int IdxAspartateMit = 68;
        static public int IdxGlutamateMit = 69;
        static public int IdxOxoglutarateMit = 70;
        static public int IdxMalateMit = 71;
        static public int IdxPyruvateMit = 72;
        static public int IdxCitrateMit = 73;
        static public int IdxIsocitrateMit = 74;
        static public int IdxSuccinylCoAMit = 75;
        static public int IdxFumarateMit = 76;
        static public int IdxSuccinateMit = 77;
        static public int IdxOxaloacetateMit = 78;
        static public int IdxAcetylCoAMit = 79;
        static public int IdxGTPmit = 80;
        static public int IdxHcyt = 81;
        static public int IdxPIi = 82;
        static public int IdxATPiTotal = 83;
        static public int IdxADPiTotal = 84;
        static public int IdxPCr = 85;

        static public int NOPRungeKutta = 85;

        //index of parameters that Not enter Runge-Kutta
        static public int FirstNonRK = 200;

        static public int InxINajs_Itc = 200;
        static public int InxINasl_Itc = 201;
        static public int InxINabjs_Itc = 202;
        static public int InxINabsl_Itc = 203;
        static public int InxINaKjs_Itc = 204;
        static public int InxINaKsl_Itc = 205;
        static public int InxIKrjs_Itc = 206;
        static public int InxIKrsl_Itc = 207;
        static public int InxIKsjs_Itc = 208;
        static public int InxIKssl_Itc = 209;
        static public int InxIKpjs_Itc = 210;
        static public int InxIKpsl_Itc = 211;
        static public int InxIClCajs_Itc = 212;
        static public int InxIClCasl_Itc = 213;
        static public int InxICaLjs_Itc = 214;
        static public int InxICaL_Cajs = 215;
        static public int InxICaL_Najs = 216;
        static public int InxICaL_Kjs = 217;
        static public int InxICaLsl_Itc = 218;
        static public int InxICaL_Casl = 219;
        static public int InxICaL_Nasl = 220;
        static public int InxICaL_Ksl = 221;
        static public int InxINaCajs_Itc = 222;
        static public int InxINaCasl_Itc = 223;
        static public int InxIpCajs_Itc = 224;
        static public int InxIpCasl_Itc = 225;
        static public int InxICabjs_Itc = 226;
        static public int InxICabsl_Itc = 227;
        static public int InxItosjs_Itc = 228;
        static public int InxItossl_Itc = 229;
        static public int InxItofjs_Itc = 230;
        static public int InxItofsl_Itc = 231;
        static public int InxIK1js_Itc = 232;
        static public int InxIK1_gate_js = 233;
        static public int InxIK1sl_Itc = 234;
        static public int InxIK1_gate_sl = 235;
        static public int InxIClbjs_Itc = 236;
        static public int InxIClbsl_Itc = 237;

        static public int InxJ_Cabuff_cyt_tot = 238;
        static public int InxJ_Cabuff_js_tot = 239;
        static public int InxJ_Cabuff_sl_tot = 240;
        static public int InxJ_Cadiff_cytsl = 241;
        static public int InxJ_Cadiff_jssl = 242;

        static public int InxJ_Nabuffjs = 243;
        static public int InxJ_Nabuffsl = 244;
        static public int InxJ_Nadiff_cytsl = 245;
        static public int InxJ_Nadiff_jssl = 246;

        static public int InxJ_SERCA = 247;
        static public int InxF_SERCA = 248;
        static public int InxJ_RyR = 249;
        static public int InxF_RyR = 250;
        static public int InxJ_Leak = 251;
        static public int InxF_Leak = 252;
        static public int InxJ_CSQN = 253;

        static public int InxJ_CaUni_cyt = 254;
        static public int InxF_CaUni_cyt = 255;
        static public int InxJ_CaUni_js = 256;
        static public int InxF_CaUni_js = 257;

        static public int InxJ_NCXmit = 258;
        static public int InxF_NCXmit = 259;
        static public int InxJ_NmSC = 260;
        static public int InxF_NmSC = 261;
        static public int InxJ_NmSC_block = 262;
        static public int InxF_NmSC_block = 263;

        static public int InxRpNajs = 264;
        static public int InxRpNasl = 265;
        static public int InxRpK = 266;
        static public int InxRpCajs = 267;
        static public int InxRpCasl = 268;
        static public int InxRpCl = 269;

        static public int InxVm_Min = 270;
        static public int InxVm_Max = 271;
        static public int InxCai_Min = 272;
        static public int InxCai_Max = 273;
        static public int InxCajs_Min = 274;
        static public int InxCajs_Max = 275;
        static public int InxCasl_Min = 276;
        static public int InxCasl_Max = 277;
        static public int InxCasr_Min = 278;
        static public int InxCasr_Max = 279;
        static public int InxCamit_Min = 280;
        static public int InxCamit_Max = 281;
        static public int InxNai_Min = 282;
        static public int InxNai_Max = 283;
        static public int InxNajs_Min = 284;
        static public int InxNajs_Max = 285;
        static public int InxNasl_Min = 286;
        static public int InxNasl_Max = 287;

        static public int InxFb = 288;
        static public int InxTwitch = 289;
        static public int InxF_ext = 290;

        static public int InxJ_Ca_contraction = 291;
        static public int InxdATPuse_contraction = 292;
        static public int InxdATPuse_INaKjs = 293;
        static public int InxdATPuse_INaKsl = 294;
        static public int InxdATPuse_IpCajs = 295;
        static public int InxdATPuse_IpCasl = 296;
        static public int InxdATPuse_SERCA = 297;
        static public int InxdATPuse_NmSC = 298;

        // Mitochondria model by Saito et al., J Physiol, 2016 (except for CaUni & NCXmit)
        static public int InxpHcyt = 299;
        static public int InxMgcyt = 300;
        static public int InxATPiFree = 301;
        static public int InxATPiMg = 302;
        static public int InxADPiFree = 303;
        static public int InxADPiMg = 304;
        static public int InxMalatei = 305;
        static public int InxGlutamatei = 306;
        static public int InxAspartatei = 307;
        static public int InxPyruvatei = 308;
        static public int InxOxoglutaratei = 309;
        static public int InxCitratei = 310;
        static public int InxJ_C1 = 311;
        static public int InxJ_C3 = 312;
        static public int InxJ_C4 = 313;
        static public int InxVO2 = 314;
        static public int InxJ_ATPsyn = 315;
        static public int InxJ_ANT = 316;
        static public int InxJ_PiC = 317;
        static public int InxJ_HLeak = 318;
        static public int InxJ_CS = 319;
        static public int InxJ_ACO = 320;
        static public int InxJ_ICDH = 321;
        static public int InxJ_OGDH = 322;
        static public int InxJ_SCS = 323;
        static public int InxJ_SDH = 324;
        static public int InxJ_FH = 325;
        static public int InxJ_MDH = 326;
        static public int InxJ_NDK = 327;
        static public int InxJ_AST = 328;
        static public int InxJ_PDHC = 329;
        static public int InxJ_PC = 330;
        static public int InxJ_ALT = 331;
        static public int InxJ_MCT = 332;
        static public int InxJ_DCT = 333;
        static public int InxJ_TCT = 334;
        static public int InxJ_OGC = 335;
        static public int InxJ_AGC = 336;
        static public int InxJ_KUni = 337;
        static public int InxJ_KHE = 338;
        static public int InxJ_NHE = 339;
        static public int InxJ_AK = 340;
        static public int InxJ_CK = 341;
        static public int InxJ_cons = 342;
        static public int InxJ_Cabuff_mit = 343;
        static public int InxpHmit = 344;
        static public int InxdpH = 345;
        static public int InxdP = 346;
        static public int InxMgFreemit = 347;
        static public int InxATPmitFree = 348;
        static public int InxATPmitMg = 349;
        static public int InxADPmitTotal = 350;
        static public int InxADPmitFree = 351;
        static public int InxADPmitMg = 352;
        static public int InxNAD = 353;
        static public int InxUQ = 354;
        static public int InxCytc_o = 355;
        static public int InxCyta_r = 356;
        static public int InxCyta_o = 357;
        static public int InxCoAMit = 358;
        static public int InxCO2mit = 359;
        static public int InxHCO3mit = 360;
        static public int InxIsocitrateFreeMit = 361;
        static public int InxIsocitrateMgMit = 362;
        static public int InxGDPmit = 363;
        static public int InxAMPi = 364;
        static public int InxCr = 365;

        static public int InxsumATPuse_contraction = 366;
        static public int InxsumATPuse_INaKjs = 367;
        static public int InxsumATPuse_INaKsl = 368;
        static public int InxsumATPuse_IpCajs = 369;
        static public int InxsumATPuse_IpCasl = 370;
        static public int InxsumATPuse_SERCA = 371;
        static public int InxsumATPuse_NmSC = 372;
        static public int InxsumVO2 = 373;

        static public int InxmeanCamit = 374;
        static public int InxmeanCai = 375;

        static public int InxCamit = 376;

        static public int InxJ_HCXmit = 377; // 24Feb08
        static public int InxF_HCXmit = 378;

        static public int IdxLast = 1000;

        static public void NameTV(string[] TVcStr)
        {
            //index of parameters
            //that enter Runge-Kutta
            TVcStr[IdxVm] = "Vm";   // membrane potential, 0
            TVcStr[IdxNai] = "Nai";   // Free Na concentration in cytoplasm, 1
            TVcStr[IdxKi] = "Ki";   // Free K concentration in cytoplasm, 2
            TVcStr[IdxCai] = "Cai";   // Free Ca concentration in cytoplasm, 3
            TVcStr[IdxCli] = "Cli";   // Free Cl concentration in cytoplasm, 4
            TVcStr[IdxCajs] = "Cajs";   // Free Ca concentration in junctional space, 5
            TVcStr[IdxCasl] = "Casl";   // Free Ca concentration in subsarcolemmal space, 6
            TVcStr[IdxNajs] = "Najs";   // Free Na concentration in junctional space, 7
            TVcStr[IdxNasl] = "Nasl";   // Free Na concentration in subsarcolemmal space, 8
            TVcStr[IdxCasr] = "Casr";   // Free Ca concentration in SR, 9
            
            TVcStr[IdxINa_m_js] = "INa_m_js";   // m gate of INajs, 10
            TVcStr[IdxINa_h_js] = "INa_h_js";   // h gate of INajs, 11
            TVcStr[IdxINa_j_js] = "INa_j_js";   // j gate of INajs, 12
            TVcStr[IdxINa_m_sl] = "INa_m_sl";   // m gate of INasl, 13
            TVcStr[IdxINa_h_sl] = "INa_h_sl";   // h gate of INasl, 14
            TVcStr[IdxINa_j_sl] = "INa_j_sl";   // j gate of INasl, 15
            TVcStr[IdxIKr_xkr_js] = "IKr_xkr_js";   // gate of IKrjs, 16
            TVcStr[IdxIKr_xkr_sl] = "IKr_xkr_sl";   //gate of IKrsl, 17
            TVcStr[IdxIKs_xks_js] = "IKs_xks_js";   //  gate of IKsjs, 18
            TVcStr[IdxIKs_xks_sl] = "IKs_xks_sl";   //  gate of IKssl, 19
            TVcStr[IdxICaL_d_js] = "ICaL_d_js";   //  d gate of ICaLjs, 20
            TVcStr[IdxICaL_f_js] = "ICaL_f_js";   //  f gate of ICaLjs, 21
            TVcStr[IdxICaL_fCaB_js] = "ICaL_fCaB_js";   // fCaB gate of ICaLjs, 22
            TVcStr[IdxICaL_d_sl] = "ICaL_d_sl";   // d gate of ICaLsl, 23
            TVcStr[IdxICaL_f_sl] = "ICaL_f_sl";   // f gate of ICaLsl, 24
            TVcStr[IdxICaL_fCaB_sl] = "ICaL_fCaB_sl";   // fCaB gate of ICaLsl, 25
            TVcStr[IdxItos_xtos_js] = "Itos_xtos_js";   // x gate of Itosjs, 26
            TVcStr[IdxItos_ytos_js] = "Itos_ytos_js";   // y gate of Itosjs, 27
            TVcStr[IdxItos_xtos_sl] = "Itos_xtos_sl";   // x gate of Itosjs, 28
            TVcStr[IdxItos_ytos_sl] = "Itos_ytos_sl";   // y gate of Itosjs, 29
            TVcStr[IdxItof_xtof_js] = "Itof_xtof_js";   // x gate of Itofjs, 30
            TVcStr[IdxItof_ytof_js] = "Itof_ytof_js";   // y gate of Itofjs, 31
            TVcStr[IdxItof_xtof_sl] = "Itof_xtof_sl";   // x gate of Itofjs, 32
            TVcStr[IdxItof_ytof_sl] = "Itof_ytof_sl";   // y gate of Itofjs, 33

            TVcStr[IdxbTnChCa] = "bTnChCa";   // Ca bound troponinC high affinity site in cytoplasm, 34
            TVcStr[IdxbTnChMg] = "bTnChMg";   // Mg bound troponinC high affinity site in cytoplasm, 35
            TVcStr[IdxbCaM] = "bCaM";   // bound CaM in cytoplasm, 36
            TVcStr[IdxbMyocinCa] = "bMyocinCa";   // Ca bound Myocin in cytoplasm, 37
            TVcStr[IdxbMyocinMg] = "bMyocinMg";   // Mg bound Myocin in cytoplasm, 38
            TVcStr[IdxbSRB] = "bSRB";   //bound SRB in cytoplasm, 39
            TVcStr[IdxbCabuffjs_low] = "bCabuffjs_low";   // bound Ca buffer low affinity site in junctional space, 40
            TVcStr[IdxbCabuffjs_high] = "bCabuffjs_high";   // bound Ca buffer high affinity site in junctional space, 41
            TVcStr[IdxbCabuffsl_low] = "bCabuffsl_low";   // bound Ca buffer low affinity site in subsarcolemmal space, 42
            TVcStr[IdxbCabuffsl_high] = "bCabuffsl_high";   // bound Ca buffer high affinity site in subsarcolemmal space, 43
            TVcStr[IdxbNabuffjs] = "bNabuffjs";   // bound Na buffer in junctional space, 44
            TVcStr[IdxbNabuffsl] = "bNabuffsl";   // bound Na buffer in subsarcolemmal space, 45
            TVcStr[IdxRyR_R] = "RyR_R";   // R state of RyR channel, 46
            TVcStr[IdxRyR_O] = "RyR_O";   // O state of RyR channel, 47
            TVcStr[IdxRyR_I] = "RyR_I";   // I state of RyR channel, 48
            TVcStr[IdxbCSQN] = "bCSQN";   // bound calsequestrin, 49

            TVcStr[IdxTrnCa] = "TrnCa"; // 50
            TVcStr[IdxTrnCa_cb] = "TrnCa_cb"; // 51
            TVcStr[IdxTrn_cb] = "Trn_cb"; // 52
            TVcStr[IdxhsmX] = "hsmX"; // 53
            TVcStr[IdxhsmL] = "hsmL"; // 54
            TVcStr[IdxV_hsmL] = "V_hsmL"; // 55

            // Mitochondria model by Saito et al., J Physiol, 2016
            TVcStr[IdxNADH] = "NADH"; // 56 
            TVcStr[IdxUQH2] = "UQH2";  // 57  
            TVcStr[IdxCytc_r] = "Cytc_r"; // 58
            TVcStr[IdxO2] = "O2";   // 59 
            TVcStr[IdxHmit] = "Hmit"; // 60
            TVcStr[IdxKmit] = "Kmit"; // 61
            TVcStr[IdxATPmitTotal] = "ATPmitTotal"; // 62
            TVcStr[IdxPImit] = "PImit"; // 63
            TVcStr[IdxdPsi] = "dPsi"; // 64
            TVcStr[IdxNamit] = "Namit"; // 65
            TVcStr[IdxCamitTot] = "CamitTot"; // 66
            TVcStr[IdxbCabuff_mit] = "bCabuff_mit"; // 67
            TVcStr[IdxAspartateMit] = "AspartateMit"; // 68
            TVcStr[IdxGlutamateMit] = "GlutamateMit"; // 69
            TVcStr[IdxOxoglutarateMit] = "OxoglutarateMit"; // 70
            TVcStr[IdxMalateMit] = "MalateMit"; // 71
            TVcStr[IdxPyruvateMit] = "PyruvateMit"; // 72
            TVcStr[IdxCitrateMit] = "CitrateMit"; // 73
            TVcStr[IdxIsocitrateMit] = "IsocitrateMit"; // 74
            TVcStr[IdxSuccinylCoAMit] = "SuccinylCoAMit"; // 75
            TVcStr[IdxFumarateMit] = "FumarateMit"; // 76
            TVcStr[IdxSuccinateMit] = "SuccinateMit"; // 77
            TVcStr[IdxOxaloacetateMit] = "OxaloacetateMit"; // 78
            TVcStr[IdxAcetylCoAMit] = "AcetylCoAMit"; // 79
            TVcStr[IdxGTPmit] = "GTPmit"; // 80
            TVcStr[IdxHcyt] = "Hcyt"; // 81
            TVcStr[IdxPIi] = "PIi"; // 82
            TVcStr[IdxATPiTotal] = "ATPiTotal"; // 83
            TVcStr[IdxADPiTotal] = "ADPiTotal"; // 84
            TVcStr[IdxPCr] = "PCr"; // 85

            //index of parameters that Not enter Runge-Kutta
            TVcStr[InxINajs_Itc] = "INajs_Itc"; //200;
            TVcStr[InxINasl_Itc] = "INasl_Itc"; //201;
            TVcStr[InxINabjs_Itc] = "INabjs_Itc"; //202;
            TVcStr[InxINabsl_Itc] = "INabsl_Itc"; //203;
            TVcStr[InxINaKjs_Itc] = "INaKjs_Itc"; //204;
            TVcStr[InxINaKsl_Itc] = "INaKsl_Itc"; //205;
            TVcStr[InxIKrjs_Itc] = "IKrjs_Itc"; //206;
            TVcStr[InxIKrsl_Itc] = "IKrsl_Itc"; //207;
            TVcStr[InxIKsjs_Itc] = "IKsjs_Itc"; //208;
            TVcStr[InxIKssl_Itc] = "IKssl_Itc"; //209;
            TVcStr[InxIKpjs_Itc] = "IKpjs_Itc"; //210;
            TVcStr[InxIKpsl_Itc] = "IKpsl_Itc"; //211;
            TVcStr[InxIClCajs_Itc] = "IClCajs_Itc"; //212;
            TVcStr[InxIClCasl_Itc] = "IClCasl_Itc"; //213;
            TVcStr[InxICaLjs_Itc] = "ICaLjs_Itc"; //214;
            TVcStr[InxICaL_Cajs] = "ICaL_Cajs"; //215;
            TVcStr[InxICaL_Najs] = "ICaL_Najs"; //216;
            TVcStr[InxICaL_Kjs] = "ICaL_Kjs"; //217;
            TVcStr[InxICaLsl_Itc] = "ICaLsl_Itc"; //218;
            TVcStr[InxICaL_Casl] = "ICaL_Casl"; //219;
            TVcStr[InxICaL_Nasl] = "ICaL_Nasl"; //220;
            TVcStr[InxICaL_Ksl] = "ICaL_Ksl"; //221;
            TVcStr[InxINaCajs_Itc] = "INaCajs_Itc"; //222;
            TVcStr[InxINaCasl_Itc] = "INaCasl_Itc"; //223;
            TVcStr[InxIpCajs_Itc] = "IpCajs_Itc"; //224;
            TVcStr[InxIpCasl_Itc] = "IpCasl_Itc"; //225;
            TVcStr[InxICabjs_Itc] = "ICabjs_Itc"; //226;
            TVcStr[InxICabsl_Itc] = "ICabsl_Itc"; //227;
            TVcStr[InxItosjs_Itc] = "Itosjs_Itc"; //228;
            TVcStr[InxItossl_Itc] = "Itossl_Itc"; //229;
            TVcStr[InxItofjs_Itc] = "Itofjs_Itc"; //230;
            TVcStr[InxItofsl_Itc] = "Itofsl_Itc"; //231;
            TVcStr[InxIK1js_Itc] = "IK1js_Itc"; //232;
            TVcStr[InxIK1_gate_js] = "IK1_gate_js"; //233;
            TVcStr[InxIK1sl_Itc] = "IK1sl_Itc"; //234;
            TVcStr[InxIK1_gate_sl] = "IK1_gate_sl"; //235;
            TVcStr[InxIClbjs_Itc] = "IClbjs_Itc"; //236;
            TVcStr[InxIClbsl_Itc] = "IClbsl_Itc"; //237;
            TVcStr[InxJ_Cabuff_cyt_tot] = "J_Cabuff_cyt_tot"; //238;
            TVcStr[InxJ_Cabuff_js_tot] = "J_Cabuff_js_tot"; //239;
            TVcStr[InxJ_Cabuff_sl_tot] = "J_Cabuff_sl_tot"; //240;
            TVcStr[InxJ_Cadiff_cytsl] = "J_Cadiff_cytsl"; //241;
            TVcStr[InxJ_Cadiff_jssl] = "J_Cadiff_jssl"; //242;
            TVcStr[InxJ_Nabuffjs] = "J_Nabuffjs"; //243;
            TVcStr[InxJ_Nabuffsl] = "J_Nabuffsl"; //244;
            TVcStr[InxJ_Nadiff_cytsl] = "J_Nadiff_cytsl"; //245;
            TVcStr[InxJ_Nadiff_jssl] = "J_Nadiff_jssl"; //246;
            TVcStr[InxJ_SERCA] = "J_SERCA"; //247;
            TVcStr[InxF_SERCA] = "F_SERCA"; //248;
            TVcStr[InxJ_RyR] = "J_RyR"; //249;
            TVcStr[InxF_RyR] = "F_RyR"; //250;
            TVcStr[InxJ_Leak] = "J_Leak"; //251;
            TVcStr[InxF_Leak] = "F_Leak"; //252;
            TVcStr[InxJ_CSQN] = "J_CSQN"; //253;
            TVcStr[InxJ_CaUni_cyt] = "J_CaUni_cyt"; //254;
            TVcStr[InxF_CaUni_cyt] = "F_CaUni_cyt"; //255;
            TVcStr[InxJ_CaUni_js] = "J_CaUni_js"; //256;
            TVcStr[InxF_CaUni_js] = "F_CaUni_js"; //257;
            TVcStr[InxJ_NCXmit] = "J_NCXmit"; //258;
            TVcStr[InxF_NCXmit] = "F_NCXmit"; //259;
            TVcStr[InxJ_NmSC] = "J_NmSC"; //260;
            TVcStr[InxF_NmSC] = "F_NmSC"; //261;
            TVcStr[InxJ_NmSC_block] = "J_NmSC_block"; //262;
            TVcStr[InxF_NmSC_block] = "F_NmSC_block"; //263;
            TVcStr[InxRpNajs] = "RpNajs"; //264;
            TVcStr[InxRpNasl] = "RpNasl"; //265;
            TVcStr[InxRpK] = "RpK"; //266;
            TVcStr[InxRpCajs] = "RpCajs"; //267;
            TVcStr[InxRpCasl] = "RpCasl"; //268;
            TVcStr[InxRpCl] = "RpCl"; //269;
            TVcStr[InxVm_Min] = "Vm_Min"; //270;
            TVcStr[InxVm_Max] = "Vm_Max"; //271;
            TVcStr[InxCai_Min] = "Cai_Min"; //272;
            TVcStr[InxCai_Max] = "Cai_Max"; //273;
            TVcStr[InxCajs_Min] = "Cajs_Min"; //274;
            TVcStr[InxCajs_Max] = "Cajs_Max"; //275;
            TVcStr[InxCasl_Min] = "Casl_Min"; //276;
            TVcStr[InxCasl_Max] = "Casl_Max"; //277;
            TVcStr[InxCasr_Min] = "Casr_Min"; //278;
            TVcStr[InxCasr_Max] = "Casr_Max"; //279;
            TVcStr[InxCamit_Min] = "Camit_Min"; //280;
            TVcStr[InxCamit_Max] = "Camit_Max"; //281;
            TVcStr[InxNai_Min] = "Nai_Min"; //282;
            TVcStr[InxNai_Max] = "Nai_Max"; //283;
            TVcStr[InxNajs_Min] = "Najs_Min"; //284;
            TVcStr[InxNajs_Max] = "Najs_Max"; //285;
            TVcStr[InxNasl_Min] = "Nasl_Min"; //286;
            TVcStr[InxNasl_Max] = "Nasl_Max"; //287;

            TVcStr[InxFb] = "Fb"; //288;
            TVcStr[InxTwitch] = "Twitch"; //289;
            TVcStr[InxF_ext] = "F_ext"; //290;

            TVcStr[InxJ_Ca_contraction] = "J_Ca_contraction"; //291;
            TVcStr[InxdATPuse_contraction] = "dATPuse_contraction"; //292;
            TVcStr[InxdATPuse_INaKjs] = "dATPuse_INaKjs"; //293;
            TVcStr[InxdATPuse_INaKsl] = "dATPuse_INaKsl"; //294;
            TVcStr[InxdATPuse_IpCajs] = "dATPuse_IpCajs"; //295;
            TVcStr[InxdATPuse_IpCasl] = "dATPuse_IpCasl"; //296;
            TVcStr[InxdATPuse_SERCA] = "dATPuse_SERCA"; //297;
            TVcStr[InxdATPuse_NmSC] = "dATPuse_NmSC"; //298;

            // Mitochondria model by Saito et al., J Physiol, 2016
            TVcStr[InxpHcyt] = "pHcyt"; // 299
            TVcStr[InxMgcyt] = "Mgcyt"; // 300
            TVcStr[InxATPiFree] = "ATPiFree"; // 301
            TVcStr[InxATPiMg] = "ATPiMg"; // 302
            TVcStr[InxADPiFree] = "ADPiFree"; // 303
            TVcStr[InxADPiMg] = "ADPiMg"; // 304
            TVcStr[InxMalatei] = "Malatei"; // 305
            TVcStr[InxGlutamatei] = "Glutamatei"; // 306
            TVcStr[InxAspartatei] = "Aspartatei"; // 307
            TVcStr[InxPyruvatei] = "Pyruvatei"; // 308
            TVcStr[InxOxoglutaratei] = "Oxoglutaratei"; // 309
            TVcStr[InxCitratei] = "Citratei"; // 310
            TVcStr[InxJ_C1] = "J_C1"; // 311
            TVcStr[InxJ_C3] = "J_C3"; // 312
            TVcStr[InxJ_C4] = "J_C4"; // 313
            TVcStr[InxVO2] = "VO2"; // 314
            TVcStr[InxJ_ATPsyn] = "J_ATPsyn"; // 315
            TVcStr[InxJ_ANT] = "J_ANT"; // 316
            TVcStr[InxJ_PiC] = "J_PiC"; // 317
            TVcStr[InxJ_HLeak] = "J_HLeak"; // 318
            TVcStr[InxJ_CS] = "J_CS"; // 319
            TVcStr[InxJ_ACO] = "J_ACO"; // 329
            TVcStr[InxJ_ICDH] = "J_ICDH"; // 321
            TVcStr[InxJ_OGDH] = "J_OGDH"; // 322
            TVcStr[InxJ_SCS] = "J_SCS"; // 323
            TVcStr[InxJ_SDH] = "J_SDH"; // 324
            TVcStr[InxJ_FH] = "J_FH"; // 325
            TVcStr[InxJ_MDH] = "J_MDH"; // 326
            TVcStr[InxJ_NDK] = "J_NDK"; // 327
            TVcStr[InxJ_AST] = "J_AST"; // 328
            TVcStr[InxJ_PDHC] = "J_PDHC"; // 329
            TVcStr[InxJ_PC] = "J_PC"; // 330
            TVcStr[InxJ_ALT] = "J_ALT"; // 331
            TVcStr[InxJ_MCT] = "J_MCT"; // 332
            TVcStr[InxJ_DCT] = "J_DCT"; // 333
            TVcStr[InxJ_TCT] = "J_TCT"; // 334
            TVcStr[InxJ_OGC] = "J_OGC"; // 335
            TVcStr[InxJ_AGC] = "J_AGC"; // 336
            TVcStr[InxJ_KUni] = "J_KUni"; // 337
            TVcStr[InxJ_KHE] = "J_KHE"; // 338
            TVcStr[InxJ_NHE] = "J_NHE"; // 339
            TVcStr[InxJ_AK] = "J_AK"; // 340
            TVcStr[InxJ_CK] = "J_CK"; // 341
            TVcStr[InxJ_cons] = "J_cons"; // 342
            TVcStr[InxJ_Cabuff_mit] = "J_Cabuff_mit"; // 343
            TVcStr[InxpHmit] = "pHmit"; // 344
            TVcStr[InxdpH] = "dpH"; // 345
            TVcStr[InxdP] = "dP"; // 346
            TVcStr[InxMgFreemit] = "MgFreemit"; // 347
            TVcStr[InxATPmitFree] = "ATPmitFree"; // 348
            TVcStr[InxATPmitMg] = "ATPmitMg"; // 349
            TVcStr[InxADPmitTotal] = "ADPmitTotal"; // 350
            TVcStr[InxADPmitFree] = "ADPmitFree"; // 351
            TVcStr[InxADPmitMg] = "ADPmitMg"; // 352
            TVcStr[InxNAD] = "NAD"; // 353
            TVcStr[InxUQ] = "UQ"; // 354
            TVcStr[InxCytc_o] = "Cytc_o"; // 355
            TVcStr[InxCyta_r] = "Cyta_r"; // 356
            TVcStr[InxCyta_o] = "Cyta_o"; // 357
            TVcStr[InxCoAMit] = "CoAMit"; // 358
            TVcStr[InxCO2mit] = "CO2mit"; // 359
            TVcStr[InxHCO3mit] = "HCO3mit"; // 360
            TVcStr[InxIsocitrateFreeMit] = "IsocitrateFree"; // 361
            TVcStr[InxIsocitrateMgMit] = "IsocitrateMg"; // 362
            TVcStr[InxGDPmit] = "GDPmit"; // 363
            TVcStr[InxAMPi] = "AMPi"; // 364
            TVcStr[InxCr] = "Cr"; // 365
            TVcStr[InxsumATPuse_contraction] = "sumATPuse_contraction"; // 366
            TVcStr[InxsumATPuse_INaKjs] = "sumATPuse_INaKjs"; // 367
            TVcStr[InxsumATPuse_INaKsl] = "sumATPuse_INaKsl"; // 368
            TVcStr[InxsumATPuse_IpCajs] = "sumATPuse_IpCajs"; // 369
            TVcStr[InxsumATPuse_IpCasl] = "sumATPuse_IpCasl"; // 370
            TVcStr[InxsumATPuse_SERCA] = "sumATPuse_SERCA"; // 371
            TVcStr[InxsumATPuse_NmSC] = "sumATPuse_NmSC"; // 372
            TVcStr[InxsumVO2] = "sumVO2"; // 373
            TVcStr[InxmeanCamit] = "meanCamit"; // 374
            TVcStr[InxmeanCai] = "meanCai"; // 375
            TVcStr[InxCamit] = "Camit"; // 376
            TVcStr[InxJ_HCXmit] = "J_HCXmit"; // 377 // 24Feb08
            TVcStr[InxF_HCXmit] = "F_HCXmit"; // 378 // 24Feb08
        }
    }
}
