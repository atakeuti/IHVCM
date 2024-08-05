using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cSERCA : cElement
    {
        private Pd.TIdx IxVmax_SERCA;
        private Pd.TIdx IxKmf;
        private Pd.TIdx IxKmr;
        private Pd.TIdx IxJ_SERCA;
        private Pd.TIdx IxF_SERCA;
        private Pd.TIdx IxdATPuse_SERCA;

        public double Vmax_SERCA = 1.5 * (1 / 0.7) * (5.3114e-3); //  = 0.011382 mM/msec

        //beta-AR stimulation was incorporated according to Kurata et al., Front Physiol, 2020
        public double Vmax_SERCA_;

        public double Kmf = 0.246e-3;
        public double Kmr = 1.7;
        public double J_SERCA;
        public double F_SERCA;
        public double dATPuse_SERCA;

        public bool BlnCProtSERCAOn = false;
        public double B_SERCA = 1.0;

        public double A, A0, Ainf, A02;

        public cSERCA()  //Constructor
        {
        }

        override public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
            int i = 0;

            SetIx(ref IxSF, ref i, Pd.IntPar, "SF");
            SetIx(ref IxJ_SERCA, ref i, Pd.IntVar, "J_SERCA");
            SetIx(ref IxF_SERCA, ref i, Pd.IntPar, "F_SERCA");
            SetIx(ref IxVmax_SERCA, ref i, Pd.IntPar, "Vmax_SERCA");
            SetIx(ref IxKmf, ref i, Pd.IntPar, "Kmf");
            SetIx(ref IxKmr, ref i, Pd.IntPar, "Kmr");

            SetIx(ref IxdATPuse_SERCA, ref i, Pd.IntVar, "dATPuse_SERCA");


            NumOfIx = i;

            Lf.DGViews_Initialize(Lf.tpSERCA_ListView, NumOfIx);

            J_SERCA = myTVc[Pd.InxJ_SERCA];
            F_SERCA = myTVc[Pd.InxF_SERCA];

            dATPuse_SERCA = myTVc[Pd.InxdATPuse_SERCA];

            A0 = 1.0;
            A = 1.0;

            double alpha = 1.0; // 1.0, satndard bAR stimulation
            Ainf = 1 + alpha * 0.3;
            A02 = Ainf;
        }

        override public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
            double fraction = myCell.SR.fraction_freeSERCA;

            double CaiOverKmf = Math.Pow(tvY[Pd.IdxCai] / Kmf, 1.787);
            double CasrOverKmr = Math.Pow(tvY[Pd.IdxCasr] / Kmr, 1.787);

            if (MainForm.BlnSProtocolOn == false)  //bAR stim on
            {
                if (MainForm.betaARState == 0)
                {
                    Vmax_SERCA_ = A0 * Vmax_SERCA;                    
                }
                else if (MainForm.betaARState == 1)
                {
                    Vmax_SERCA_ = Ainf * Vmax_SERCA;
                }
            }
            else  //AR protocol
            {
                if (myCell.IntbARstimProtcol == 0)
                {
                    Vmax_SERCA_ = A0 * Vmax_SERCA;                    
                }
                else if (myCell.IntbARstimProtcol == 1)
                {
                    A = A0 + (Ainf - A0) * (1 - Math.Exp(-(MainForm.TimeElp - MainForm.T_StimChangeOn) / MainForm.tau));
                    A02 = A;
                    Vmax_SERCA_ = A * Vmax_SERCA;
                }
                else if (myCell.IntbARstimProtcol == 2)
                {
                    A = A0 + (A02 - A0) * (Math.Exp(-(MainForm.TimeElp - MainForm.T_StimChangeOff) / MainForm.tau));
                    Vmax_SERCA_ = A * Vmax_SERCA;
                }
            }


            // beta-AR stimulation was incorporated according to Kurata et al., Front Physiol, 2020
            J_SERCA = SF * B_SERCA * fraction * Vmax_SERCA_ * (CaiOverKmf - CasrOverKmr) / (1 + CaiOverKmf + CasrOverKmr);

            F_SERCA = J_SERCA * myCell.SR.Vsr;

            dATPuse_SERCA = F_SERCA / (2 * myCell.Vi_diff); // 2Ca2+ ; 2H+ : 1ATP

            myCell.TVc[Pd.InxJ_SERCA] = J_SERCA;
            myCell.TVc[Pd.InxF_SERCA] = F_SERCA;
            myCell.TVc[Pd.InxdATPuse_SERCA] = dATPuse_SERCA;
        }

        override public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpSERCA_ListView;
            ListView.LVDispValue("SERCA", IxSF, ref SF);
            ListView.LVDispValue("SERCA", IxJ_SERCA, ref J_SERCA);
            ListView.LVDispValue("SERCA", IxF_SERCA, ref F_SERCA);
            ListView.LVDispValue("SERCA", IxVmax_SERCA, ref Vmax_SERCA);
            ListView.LVDispValue("SERCA", IxKmf, ref Kmf);
            ListView.LVDispValue("SERCA", IxKmr, ref Kmr);

            ListView.LVDispValue("SERCA", IxdATPuse_SERCA, ref dATPuse_SERCA);
        }

        override public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
            ucListView ListView = Lf.tpSERCA_ListView;
            ListView.LVModiValue("SERCA", IxSF, ref SF);
            ListView.LVModiValue("SERCA", IxJ_SERCA, ref J_SERCA);
            ListView.LVModiValue("SERCA", IxF_SERCA, ref F_SERCA);
            ListView.LVModiValue("SERCA", IxVmax_SERCA, ref Vmax_SERCA);
            ListView.LVModiValue("SERCA", IxKmf, ref Kmf);
            ListView.LVModiValue("SERCA", IxKmr, ref Kmr);

            ListView.LVModiValue("SERCA", IxdATPuse_SERCA, ref dATPuse_SERCA);
        }
    }
}
