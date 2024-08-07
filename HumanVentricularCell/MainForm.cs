using HumanVentricularCell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HumanVentricularCell
{
    public partial class MainForm : Form
    {
        public System.Windows.Forms.MdiClient mc;
        public ListForm ListForm;
        public DispForm1 DispForm1;
        public DispForm2 DispForm2;
        public DispForm3 DispForm3;
        public DispForm4 DispForm4;
        public DispForm5 DispForm5;
        public DispForm6 DispForm6;
        public DispForm7 DispForm7;
        public DispForm8 DispForm8;

        public AboutBox1 AboutBox1;

        public cCell Cell;

        public double dt = 0.00001;
        public static double TimeElp;			//elapsed time in a frame
        public double TimeElp_Sht;		//elapsed time for short display
        public double TimeElp_Lng;		//elapsed time for long display
        public double ElapsedTime_cyc;

        public static double ExternalLoad;
        public static double ExternalLoad2;  //during high stim
        public static int ContState;

        public static int betaARState;
        public static double tau = 40000; // 40 sec, for exercise protocol

        public bool BlnWriteSimData;
        public bool BlnStopFlag;
        public bool BlnPause;
        public bool BlnParListForm;

        private string InitFileName;
        private string SimFileName;
        private string SimFileName_beat;
        private string LCFileName;
        private string LCFNameForProtocol = "testLCFnameP";

        private int PeakFlag = 0;
        public double dVdTVal;
        public double VmOld;
        public Pd.TdVdTPeak[] dVdTPeak;

        public static bool BlnSProtocolOn = false;
        public static double T_StimChangeOn;
        public static double T_StimChangeOff;
        private double T_Duration;

        private bool BlnRunProtocolOn = false;

        private double meanCamittemp = 0.0;
        private double stimIntV, stimIntV0, stimIntVinf, stimIntV02;
 
        public MainForm()
        {
            InitializeComponent();

            Cell = new cCell();

            ListForm = new ListForm(this);
            ListForm.MdiParent = this;
            ListForm.Text = "Parameter List";

            DispForm1 = new DispForm1(this);
            DispForm1.MdiParent = this;
            DispForm2 = new DispForm2(this);
            DispForm2.MdiParent = this;
            DispForm3 = new DispForm3(this);
            DispForm3.MdiParent = this;
            DispForm4 = new DispForm4(this);
            DispForm4.MdiParent = this;
            DispForm5 = new DispForm5(this);
            DispForm5.MdiParent = this;
            DispForm6 = new DispForm6(this);
            DispForm6.MdiParent = this;
            DispForm7 = new DispForm7(this);
            DispForm7.MdiParent = this;
            DispForm8 = new DispForm8(this);
            DispForm8.MdiParent = this;

            AboutBox1 = new AboutBox1(this);

            dVdTPeak = new Pd.TdVdTPeak[2];
            for (int i = 0; i <= 1; i++)
            {
                dVdTPeak[i] = new Pd.TdVdTPeak();
            };
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DispForm1.Show();
            DispForm2.Show();
            DispForm3.Show();
            DispForm4.Show();
            DispForm5.Show();
            DispForm6.Show();
            DispForm7.Show();
            DispForm8.Show();
            ListForm.Show();

            DispForm1.Top = 0;
            DispForm2.Top = 0;
            DispForm3.Top = 0;
            DispForm4.Top = 0;
            DispForm5.Top = DispForm1.Height;
            DispForm6.Top = DispForm1.Height;
            DispForm7.Top = DispForm1.Height;
            DispForm8.Top = DispForm1.Height;
            ListForm.Top = 0;

            DispForm1.Left = 0;
            DispForm2.Left = DispForm1.Width;
            DispForm3.Left = DispForm1.Width + DispForm2.Width;
            DispForm4.Left = DispForm1.Width + DispForm2.Width + DispForm3.Width;
            DispForm5.Left = 0;
            DispForm6.Left = DispForm5.Width;
            DispForm7.Left = DispForm5.Width + DispForm6.Width;
            DispForm8.Left = DispForm5.Width + DispForm6.Width + DispForm7.Width;

            ListForm.Left = DispForm1.Width + DispForm2.Width + DispForm3.Width + DispForm4.Width;

            ResetToolStripMenuItem.Enabled = false;
            StartToolStripMenuItem.Enabled = false;
            StopToolStripMenuItem.Enabled = false;
            SaveToolStripMenuItem.Enabled = false;

            GetNewSettings();

            DispForm1.SendImageBmpToPbox();
            DispForm2.SendImageBmpToPbox();
            DispForm3.SendImageBmpToPbox();
            DispForm4.SendImageBmpToPbox();
            DispForm5.SendImageBmpToPbox();
            DispForm6.SendImageBmpToPbox();
            DispForm7.SendImageBmpToPbox();
            DispForm8.SendImageBmpToPbox();
        }

        private void SetSimDataFile(ref string strFileName)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            string FName = "test";

            sfd.Filter = "csv file|*.csv|All files|*.*";

            sfd.FilterIndex = 1;

             sfd.Title = "Select one file that simulation results are saved";

            sfd.RestoreDirectory = true;

            sfd.CreatePrompt = true;

            sfd.OverwritePrompt = true;

            sfd.CheckPathExists = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FName = sfd.FileName;
                if (System.IO.File.Exists(FName))
                {
                    System.IO.File.Delete(FName);
                };
            }
            else
            {
                FName = "test";
            }
            strFileName = FName;
        }

        private void GetNewSettings()
        {
            if (rbtn_CellShortening.Checked) //cellshortenig
            {
                ContState = 0;
            }
            else  //isometric contraction
            {
                ContState = 1;
            }

            if (cbxbAR.Checked) // beta AR on
            {
                betaARState = 1;
            }
            else  // control; beta AR off
            {
                betaARState = 0;
            }

            Cell.MSet.StimCycle = Double.Parse(Txt_OneCycle.Text);
            Cell.MSet.SDispTimeMax = Cell.MSet.StimCycle;       
            Cell.MSet.StimAmp = Double.Parse(Txt_StimAmp.Text);
            Cell.MSet.PulseOnset = 50;
            Cell.MSet.CC_Onset = Cell.MSet.PulseOnset;
            Cell.MSet.PulseOffset = 51;
            Cell.MSet.CC_Offset = Cell.MSet.PulseOffset;

            ExternalLoad = Double.Parse(txt_ExternalLoad.Text);

            if (ComboBox2.SelectedIndex == 0)
            {
                Cell.MSet.LDispTimeMax = Double.Parse(Txt_LngDispTimeMax.Text); // msec	
            }
            else if (ComboBox2.SelectedIndex == 1)
            {
                Cell.MSet.LDispTimeMax = Double.Parse(Txt_LngDispTimeMax.Text) * 1000.0;  // sec . msec
            }
            else if (ComboBox2.SelectedIndex == 2)
            {
                Cell.MSet.LDispTimeMax = Double.Parse(Txt_LngDispTimeMax.Text) * 1000.0 * 60.0;  // min . msec	
            }
            else if (ComboBox2.SelectedIndex == 3)
            {
                Cell.MSet.LDispTimeMax = Double.Parse(Txt_LngDispTimeMax.Text) * 1000.0 * 60.0 * 60.0; // hr . msec	
            }

            if (ComboBox1.SelectedIndex == 0)
            {
                Cell.MSet.SimTimeMax = Double.Parse(Txt_SimeTimeMax.Text);
            }
            else if (ComboBox1.SelectedIndex == 1)
            {
                Cell.MSet.SimTimeMax = Double.Parse(Txt_SimeTimeMax.Text) * 1000.0;  //' sec	
            }
            else if (ComboBox1.SelectedIndex == 2)
            {
                Cell.MSet.SimTimeMax = Double.Parse(Txt_SimeTimeMax.Text) * 1000.0 * 60.0; // min	
            }
            else if (ComboBox1.SelectedIndex == 3)
            {
                Cell.MSet.SimTimeMax = Double.Parse(Txt_SimeTimeMax.Text) * 1000.0 * 60.0 * 60.0; // hr
            }

            DispForm1.DispXmax(Cell.MSet.LDispTimeMax);
            DispForm2.DispXmax(Cell.MSet.LDispTimeMax);
            DispForm3.DispXmax(Cell.MSet.LDispTimeMax);
            DispForm4.DispXmax(Cell.MSet.LDispTimeMax);
            DispForm5.DispXmax(Cell.MSet.LDispTimeMax);
            DispForm6.DispXmax(Cell.MSet.LDispTimeMax);
            DispForm7.DispXmax(Cell.MSet.LDispTimeMax);
            DispForm8.DispXmax(Cell.MSet.LDispTimeMax);

            DispForm1.GetNewSettings();
            DispForm2.GetNewSettings();
            DispForm3.GetNewSettings();
            DispForm4.GetNewSettings();
            DispForm5.GetNewSettings();
            DispForm6.GetNewSettings();
            DispForm7.GetNewSettings();
            DispForm8.GetNewSettings();
        }

        private void GetNewSettingsStimProt()
        {
            if (rbtn_CellShortening.Checked) //cellshortenig
            {
                ContState = 0;
            }
            else  //isometric contraction
            {
                ContState = 1;
            }

            if (cbxbAR.Checked) // beta AR on
            {
                betaARState = 1;
            }
            else  // control; beta AR off
            {
                betaARState = 0;
            }

            Cell.MSet.StimCycle = Double.Parse(Txt_OriginalCycle.Text);
            Cell.MSet.SDispTimeMax = Cell.MSet.StimCycle;      
            Cell.MSet.StimAmp = Double.Parse(Txt_StimAmp.Text);
            Cell.MSet.PulseOnset = 50;
            Cell.MSet.CC_Onset = Cell.MSet.PulseOnset;
            Cell.MSet.PulseOffset = 51;
            Cell.MSet.CC_Offset = Cell.MSet.PulseOffset;

            ExternalLoad = Double.Parse(txt_ExternalLoad.Text);

            Cell.MSet.LDispTimeMax = Double.Parse(Txt_TotalDuration.Text) * 1000.0; // sec . msec
            Cell.MSet.SimTimeMax = Double.Parse(Txt_TotalDuration.Text) * 1000.0; // sec . msec

            DispForm1.DispXmax(Cell.MSet.LDispTimeMax);
            DispForm2.DispXmax(Cell.MSet.LDispTimeMax);
            DispForm3.DispXmax(Cell.MSet.LDispTimeMax);
            DispForm4.DispXmax(Cell.MSet.LDispTimeMax);
            DispForm5.DispXmax(Cell.MSet.LDispTimeMax);
            DispForm6.DispXmax(Cell.MSet.LDispTimeMax);
            DispForm7.DispXmax(Cell.MSet.LDispTimeMax);
            DispForm8.DispXmax(Cell.MSet.LDispTimeMax);

            DispForm1.GetNewSettings();
            DispForm2.GetNewSettings();
            DispForm3.GetNewSettings();
            DispForm4.GetNewSettings();
            DispForm5.GetNewSettings();
            DispForm6.GetNewSettings();
            DispForm7.GetNewSettings();
            DispForm8.GetNewSettings();
        }

        private void ModiValueCells(cCell myCell)
        {
            myCell.ModiValues(ref ListForm, ref Cell.TVc);
        }

        void SimCells()
        {
            double TimeElp_Plot = 0.0;
            double PlotInt_Sht = 0.0;
            double TimeElp_DispSht = 0.0;
            double TimeElp_SimSave = 0.0;
            double SimSaveIntV = 0.0;
            double RereshIntV = 0.0;
            double TimeRereshIntV = 0.0;

            int SampCount = 0;
            int SampCount_beat = 0;
            int NumOfLDisp = 1;

            string Hstr;
            string Mstr;
            string Sstr;
            double Hdbl;
            double Mdbl;
            int Sint;
            double TimeSec;
            double TimeSimStart;
            bool BlnLastCycle;

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            if (BlnRunProtocolOn == true)
            {
            }
            else
            {
                SimFileName = "SimData";
            }

            LCFileName = "LastCycleData";
            BlnPause = false;

            GetNewSettings();

            DispForm1.EraseGraphs();
            DispForm2.EraseGraphs();
            DispForm3.EraseGraphs();
            DispForm4.EraseGraphs();
            DispForm5.EraseGraphs();
            DispForm6.EraseGraphs();
            DispForm7.EraseGraphs();
            DispForm8.EraseGraphs();

            ModiValueCells(Cell);

            SetInitMinMax(ref Cell);

            BlnStopFlag = false;
            BlnPause = false;
            PlotInt_Sht = double.Parse(Txt_PlotIntv.Text);
            RereshIntV = double.Parse(Txt_RefreshIntv.Text);
            SimSaveIntV = double.Parse(Txt_SimSaveIntV.Text);
            Pd.BlnFirstPlot_SDisp = true;
            Pd.BlnFirstPlot_LDisp = true;
            NumOfLDisp = 1;
            TimeSimStart = TimeElp;
            BlnLastCycle = false;

            if (Rbn_SaveSimData.Checked == true)
            {
                SetSimDataFile(ref SimFileName);
                SimFileName_beat = System.IO.Path.GetDirectoryName(SimFileName) + '\\'
                                 + System.IO.Path.GetFileNameWithoutExtension(SimFileName) + "_beat.csv";
            }
            else if (Rbn_SaveLC.Checked && BlnRunProtocolOn == false)

            {
                SetSimDataFile(ref LCFileName);
            }

            while (TimeElp <= Cell.MSet.SimTimeMax + TimeSimStart)
            {
                TimeElp_Sht = 0;
                Pd.BlnFirstPlot_SDisp = true;

                if (TimeElp_Lng > Cell.MSet.LDispTimeMax)
                {
                    if (Cbx_OverWriteDisps.Checked == false)
                    {
                        DispForm1.EraseGraphs();
                        DispForm2.EraseGraphs();
                        DispForm3.EraseGraphs();
                        DispForm4.EraseGraphs();
                        DispForm5.EraseGraphs();
                        DispForm6.EraseGraphs();
                        DispForm7.EraseGraphs();
                        DispForm8.EraseGraphs();
                    }

                    TimeElp_Lng = 0;
                    Pd.BlnFirstPlot_LDisp = true;
                    NumOfLDisp = NumOfLDisp + 1;
                };

                if (TimeElp >= Cell.MSet.SimTimeMax + TimeSimStart - Cell.MSet.StimCycle) BlnLastCycle = true;

                sw.Start();

                while (TimeElp_Sht < Cell.MSet.SDispTimeMax)
                {
                    Cell.Integrate(ref dt, ref Cell.TVc, Cell);

                    if (Double.IsNaN(Cell.TVc[Pd.IdxCai]))
                    {
                        BlnStopFlag = true;
                        sw.Stop();
                        ElapsedTime_cyc = 0;
                        MessageBox.Show("Cai is NaN", "Error!");
                        break;
                    }

                    if ((Cell.MSet.CC_Onset <= ElapsedTime_cyc) && (ElapsedTime_cyc < Cell.MSet.CC_Offset))
                    {
                        Cell.Istim = Cell.MSet.StimAmp;
                    }
                    else
                    {
                        Cell.Istim = 0.0;
                    };

                    if (TimeElp_Plot > PlotInt_Sht)
                    {
                        if (Cbx_ShowDispForms.Checked == true)
                        {
                            DispForm1.PlotSimDisp(TimeElp_Lng, Cell);
                            DispForm2.PlotSimDisp(TimeElp_Lng, Cell);
                            DispForm3.PlotSimDisp(TimeElp_Lng, Cell);
                            DispForm4.PlotSimDisp(TimeElp_Lng, Cell);
                            DispForm5.PlotSimDisp(TimeElp_Lng, Cell);

                            DispForm6.PlotSimDisp(TimeElp_Lng, Cell);
                            DispForm7.PlotSimDisp(TimeElp_Lng, Cell);
                            DispForm8.PlotSimDisp(TimeElp_Lng, Cell);
                        }

                        if (Pd.BlnFirstPlot_SDisp == true)
                        {
                            Pd.BlnFirstPlot_SDisp = false;
                        };
                        if (Pd.BlnFirstPlot_LDisp == true)
                        {
                            Pd.BlnFirstPlot_LDisp = false;
                        };
                        TimeElp_Plot = 0;
                    };

                    if (TimeRereshIntV > RereshIntV)
                    {
                        if (Cbx_ShowDispForms.Checked == true)
                        {
                            DispForm1.SendImageBmpToPbox();
                            DispForm2.SendImageBmpToPbox();
                            DispForm3.SendImageBmpToPbox();
                            DispForm4.SendImageBmpToPbox();
                            DispForm5.SendImageBmpToPbox();
                            DispForm6.SendImageBmpToPbox();
                            DispForm7.SendImageBmpToPbox();
                            DispForm8.SendImageBmpToPbox();
                        }

                        TimeRereshIntV = 0.0;
                    }

                    if (Rbn_SaveSimData.Checked)
                    {
                        if ((TimeElp_SimSave >= SimSaveIntV))
                        {
                            SaveSimData(SimFileName, TimeElp, SampCount);
                            ++SampCount;
                            TimeElp_SimSave = 0.0;
                        };
                    };

                    if (ElapsedTime_cyc >= Cell.MSet.StimCycle)
                    {
                        if (Rbn_SaveSimData.Checked)
                        {
                            SaveSimDataByBeat(SimFileName_beat, TimeElp, SampCount_beat);
                            ++SampCount_beat;
                        };

                        DispMinMax(Cell);
                        if (Cbx_ShowDispForms.Checked == true)
                        {
                            DispForm1.DispLabels(Cell);
                            DispForm2.DispLabels(Cell);
                            DispForm3.DispLabels(Cell);
                            DispForm4.DispLabels(Cell);
                            DispForm5.DispLabels(Cell);
                            DispForm6.DispLabels(Cell);
                            DispForm7.DispLabels(Cell);
                            DispForm8.DispLabels(Cell);
                        }

                        SetInitMinMax(ref Cell);

                        ElapsedTime_cyc = 0;
                    }
                    else
                    {
                        FindMinMax(ref Cell);
                    };

                    //increment elapsed time
                    TimeElp = TimeElp + dt;
                    TimeElp_Sht = TimeElp_Sht + dt;
                    TimeElp_Lng = TimeElp_Lng + dt;
                    ElapsedTime_cyc = ElapsedTime_cyc + dt;
                    TimeElp_Plot = TimeElp_Plot + dt;
                    TimeElp_DispSht = TimeElp_DispSht + dt;
                    TimeElp_SimSave = TimeElp_SimSave + dt;
                    TimeRereshIntV = TimeRereshIntV + dt;


                } //End of 2nd while

                sw.Stop();
                lbl_SimRealRatio.Text = String.Format("{0,6:#.#0}", TimeElp_Sht / sw.ElapsedMilliseconds);
                sw.Reset();

                Cell.DispValues(ref ListForm, ref Cell.TVc);

                TimeSec = TimeElp / 1000;
                lbl_Time.Text = TimeSec.ToString("F1");   //Format(TimeSec, "#.#")
                Hdbl = Math.Truncate(TimeSec / 60.0 / 60.0);
                Mdbl = Math.Truncate((TimeSec - Hdbl * 60 * 60) / 60);
                Sint = ((int)(TimeSec - Hdbl * 60 * 60)) % 60;

                Hstr = String.Format("{0,3:00}", Hdbl);
                Mstr = String.Format("{0,3:00}", Mdbl);
                Sstr = String.Format("{0,3:00}", Sint);

                lblTime_min.Text = Hstr + ":" + Mstr + ":" + Sstr;
                lbl_Time.Refresh();
                lblTime_min.Refresh();

                Application.DoEvents();
                if (BlnStopFlag == true)
                {
                    sw.Stop();
                    ElapsedTime_cyc = 0;
                    break;
                };
            }   //End of 1st while

            TimeSimStart = TimeElp;
            TimeElp_Sht = 0.0;
            ElapsedTime_cyc = 0.0;

            if (Rbn_SaveSimData.Checked)
            {
                if (SampCount > 0)
                {
                    WriteSimData(SimFileName, TimeElp, SampCount);
                    WriteSimDataBybeat(SimFileName_beat, TimeElp, SampCount_beat);
                };
            }
            else if (Rbn_SaveLC.Checked && BlnLastCycle == true)
            {
                if (SampCount > 0)
                {
                    //if (BlnProtocolOn == false) //24Apr07
                    if (BlnRunProtocolOn == false)
                    {
                        WriteSimData(LCFileName, TimeElp, SampCount);
                    }
                    else
                    {
                        WriteSimData(LCFNameForProtocol, TimeElp, SampCount);
                    }
                };
            };
        }

        void SimCells_StimProt()   //SimCells with a protocol
        {
            Cell.BlnCamitFix = false;

            double TimeElp_Plot = 0.0;
            double PlotInt_Sht = 0.0;
            double TimeElp_DispSht = 0.0;
            double TimeElp_SimSave = 0.0;
            double SimSaveIntV = 0.0;
            double RereshIntV = 0.0;
            double TimeRereshIntV = 0.0;

            int SampCount = 0;
            int SampCount_beat = 0;
            int NumOfLDisp = 1;

            string Hstr;
            string Mstr;
            string Sstr;
            double Hdbl;
            double Mdbl;
            int Sint;
            double TimeSec;
            double TimeSimStart;
            bool BlnLastCycle;

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            LCFileName = "LastCycleData";
            BlnPause = false;

            GetNewSettingsStimProt();

            DispForm1.EraseGraphs();
            DispForm2.EraseGraphs();
            DispForm3.EraseGraphs();
            DispForm4.EraseGraphs();
            DispForm5.EraseGraphs();
            DispForm6.EraseGraphs();
            DispForm7.EraseGraphs();
            DispForm8.EraseGraphs();

            ModiValueCells(Cell);

            Rbn_SaveSimData.Checked = true;

            SetInitMinMax(ref Cell);

            BlnStopFlag = false;
            BlnPause = false;
            PlotInt_Sht = double.Parse(Txt_PlotIntv.Text);
            RereshIntV = double.Parse(Txt_RefreshIntv.Text);
            SimSaveIntV = double.Parse(Txt_SimSaveIntV.Text);
            Pd.BlnFirstPlot_SDisp = true;
            Pd.BlnFirstPlot_LDisp = true;
            NumOfLDisp = 1;
            TimeSimStart = TimeElp;
            BlnLastCycle = false;

            if (Rbn_SaveSimData.Checked == true && BlnRunProtocolOn == false)
            {
                SetSimDataFile(ref SimFileName);
                SimFileName_beat = System.IO.Path.GetDirectoryName(SimFileName) + '\\'
                                 + System.IO.Path.GetFileNameWithoutExtension(SimFileName) + "_beat.csv";
            }

            while (TimeElp <= Cell.MSet.SimTimeMax + TimeSimStart)
            {
                TimeElp_Sht = 0;
                Pd.BlnFirstPlot_SDisp = true;

                if (TimeElp_Lng > Cell.MSet.LDispTimeMax)
                {
                    if (Cbx_OverWriteDisps.Checked == false)
                    {
                        DispForm1.EraseGraphs();
                        DispForm2.EraseGraphs();
                        DispForm3.EraseGraphs();
                        DispForm4.EraseGraphs();
                        DispForm5.EraseGraphs();
                        DispForm6.EraseGraphs();
                        DispForm7.EraseGraphs();
                        DispForm8.EraseGraphs();
                    }

                    TimeElp_Lng = 0;
                    Pd.BlnFirstPlot_LDisp = true;
                    NumOfLDisp = NumOfLDisp + 1;
                };

                if (TimeElp >= Cell.MSet.SimTimeMax + TimeSimStart - Cell.MSet.StimCycle) BlnLastCycle = true;

                sw.Start();

                T_StimChangeOn = Double.Parse(Txt_StimChangeOn.Text) * 1000;
                T_StimChangeOff = Double.Parse(Txt_StimChangeOff.Text) * 1000;

                while (TimeElp_Sht < Cell.MSet.SDispTimeMax)
                {
                    Cell.Integrate(ref dt, ref Cell.TVc, Cell);
                 
                    if (Double.IsNaN(Cell.TVc[Pd.IdxCai]))
                    {
                        BlnStopFlag = true;
                        sw.Stop();
                        ElapsedTime_cyc = 0;
                        MessageBox.Show("Cai is NaN", "Error!");
                        break;
                    }

                    if (TimeElp <= T_StimChangeOn)
                    {
                        Cell.MSet.StimCycle = Double.Parse(Txt_OriginalCycle.Text);

                        if ((Cell.MSet.CC_Onset <= ElapsedTime_cyc) && (ElapsedTime_cyc < Cell.MSet.CC_Offset))
                        {
                            Cell.Istim = Cell.MSet.StimAmp;
                        }
                        else
                        {
                            Cell.Istim = 0.0;
                        };

                        if (chkBox_ELChangeProtocol.Checked)
                        {
                            Cell.IntELChangeProtcol = 0;
                        }
                        if (chkBox_bARProtocol.Checked)
                        {
                            Cell.IntbARstimProtcol = 0;
                        }
                        Cell.IntFreqChangeProtcol = 0;

                    }

                    else if ((TimeElp > T_StimChangeOn) && (TimeElp <= T_StimChangeOff))
                    {
                        if ((chkbox_CamitFix.Checked) && (Cell.BlnCamitFix == false))
                        {
                            Cell.BlnCamitFix = true;
                        }

                        if ((chkBox_ELChangeProtocol.Checked) && (Cell.IntELChangeProtcol == 0))
                        {
                            Cell.IntELChangeProtcol = 1;
                        }
 
                        if ((chkBox_bARProtocol.Checked) && (Cell.IntbARstimProtcol == 0))
                        {
                            Cell.IntbARstimProtcol = 1;
                        }

                        if (chkBox_ChangeFreqwithtau.Checked)
                        {
                            stimIntV = stimIntV0 + (stimIntVinf - stimIntV0) * (1 - Math.Exp(-(MainForm.TimeElp - MainForm.T_StimChangeOn) / MainForm.tau));
                            stimIntV02 = stimIntV;
                            Cell.MSet.StimCycle = stimIntV;
                        }
                        else
                        {
                            Cell.MSet.StimCycle = Double.Parse(Txt_TestCycle.Text);
                        }


                        if ((Cell.MSet.CC_Onset <= ElapsedTime_cyc) && (ElapsedTime_cyc < Cell.MSet.CC_Offset))
                        {
                            Cell.Istim = Cell.MSet.StimAmp;
                        }
                        else
                        {
                            Cell.Istim = 0.0;
                        };

                    }
                    else if ((TimeElp > T_StimChangeOff) && (TimeElp <= Cell.MSet.SimTimeMax + TimeSimStart))
                    {
                        if ((chkbox_CamitFix.Checked) && (Cell.BlnCamitFix == false))
                        {
                            Cell.BlnCamitFix = true;
                        }

                        if ((chkBox_ELChangeProtocol.Checked) && (Cell.IntELChangeProtcol == 1))
                        {
                            Cell.IntELChangeProtcol = 2;
                        }
 
                        if ((chkBox_bARProtocol.Checked) && (Cell.IntbARstimProtcol == 1))
                        {
                            Cell.IntbARstimProtcol = 2;
                        }

                        if (chkBox_ChangeFreqwithtau.Checked)
                        {
                            stimIntV = stimIntV0 + (stimIntV02 - stimIntV0) * (Math.Exp(-(MainForm.TimeElp - MainForm.T_StimChangeOff) / MainForm.tau));
                            Cell.MSet.StimCycle = stimIntV;
                        }
                        else
                        {
                            Cell.MSet.StimCycle = Double.Parse(Txt_OriginalCycle.Text);
                        }


                        if ((Cell.MSet.CC_Onset <= ElapsedTime_cyc) && (ElapsedTime_cyc < Cell.MSet.CC_Offset))
                        {
                            Cell.Istim = Cell.MSet.StimAmp;
                        }
                        else
                        {
                            Cell.Istim = 0.0;
                        };
                    }

                    if (TimeElp_Plot > PlotInt_Sht)
                    {
                        if (Cbx_ShowDispForms.Checked == true)
                        {
                            DispForm1.PlotSimDisp(TimeElp_Lng, Cell);
                            DispForm2.PlotSimDisp(TimeElp_Lng, Cell);
                            DispForm3.PlotSimDisp(TimeElp_Lng, Cell);
                            DispForm4.PlotSimDisp(TimeElp_Lng, Cell);
                            DispForm5.PlotSimDisp(TimeElp_Lng, Cell);
                            DispForm6.PlotSimDisp(TimeElp_Lng, Cell);
                            DispForm7.PlotSimDisp(TimeElp_Lng, Cell);
                            DispForm8.PlotSimDisp(TimeElp_Lng, Cell);
                        }

                        if (Pd.BlnFirstPlot_SDisp == true)
                        {
                            Pd.BlnFirstPlot_SDisp = false;
                        };
                        if (Pd.BlnFirstPlot_LDisp == true)
                        {
                            Pd.BlnFirstPlot_LDisp = false;
                        };
                        TimeElp_Plot = 0;
                    };

                    if (TimeRereshIntV > RereshIntV)
                    {
                        if (Cbx_ShowDispForms.Checked == true)
                        {
                            DispForm1.SendImageBmpToPbox();
                            DispForm2.SendImageBmpToPbox();
                            DispForm3.SendImageBmpToPbox();
                            DispForm4.SendImageBmpToPbox();
                            DispForm5.SendImageBmpToPbox();
                            DispForm6.SendImageBmpToPbox();
                            DispForm7.SendImageBmpToPbox();
                            DispForm8.SendImageBmpToPbox();
                        }

                        TimeRereshIntV = 0.0;
                    }

                    if (Rbn_SaveSimData.Checked)
                    {
                        if ((TimeElp_SimSave >= SimSaveIntV))
                        {
                            SaveSimData(SimFileName, TimeElp, SampCount);
                            ++SampCount;
                            TimeElp_SimSave = 0.0;
                        };
                    };

                    if (ElapsedTime_cyc >= Cell.MSet.StimCycle)
                    {
                        if (Rbn_SaveSimData.Checked)
                        {
                            SaveSimDataByBeat(SimFileName_beat, TimeElp, SampCount_beat);
                            ++SampCount_beat;
                        };

                        DispMinMax(Cell);
                        if (Cbx_ShowDispForms.Checked == true)
                        {
                            DispForm1.DispLabels(Cell);
                            DispForm2.DispLabels(Cell);
                            DispForm3.DispLabels(Cell);
                            DispForm4.DispLabels(Cell);
                            DispForm5.DispLabels(Cell);
                            DispForm6.DispLabels(Cell);
                            DispForm7.DispLabels(Cell);
                            DispForm8.DispLabels(Cell);
                        }

                        SetInitMinMax(ref Cell);

                        ElapsedTime_cyc = 0;
                    }
                    else
                    {
                        FindMinMax(ref Cell);
                    };

                    //increment elapsed time
                    TimeElp = TimeElp + dt;
                    TimeElp_Sht = TimeElp_Sht + dt;
                    TimeElp_Lng = TimeElp_Lng + dt;
                    ElapsedTime_cyc = ElapsedTime_cyc + dt;
                    TimeElp_Plot = TimeElp_Plot + dt;
                    TimeElp_DispSht = TimeElp_DispSht + dt;
                    TimeElp_SimSave = TimeElp_SimSave + dt;
                    TimeRereshIntV = TimeRereshIntV + dt;


                } //End of 2nd while

                sw.Stop();
                lbl_SimRealRatio.Text = String.Format("{0,6:#.#0}", TimeElp_Sht / sw.ElapsedMilliseconds);
                sw.Reset();

                Cell.DispValues(ref ListForm, ref Cell.TVc);

                TimeSec = TimeElp / 1000;
                lbl_Time.Text = TimeSec.ToString("F1");   //Format(TimeSec, "#.#")
                Hdbl = Math.Truncate(TimeSec / 60.0 / 60.0);
                Mdbl = Math.Truncate((TimeSec - Hdbl * 60 * 60) / 60);
                Sint = ((int)(TimeSec - Hdbl * 60 * 60)) % 60;

                Hstr = String.Format("{0,3:00}", Hdbl);
                Mstr = String.Format("{0,3:00}", Mdbl);
                Sstr = String.Format("{0,3:00}", Sint);

                lblTime_min.Text = Hstr + ":" + Mstr + ":" + Sstr;
                lbl_Time.Refresh();
                lblTime_min.Refresh();

                Application.DoEvents();
                if (BlnStopFlag == true)
                {
                    sw.Stop();
                    ElapsedTime_cyc = 0;
                    break;
                };
            }   //End of 1st while

            TimeSimStart = TimeElp;
            TimeElp_Sht = 0.0;
            ElapsedTime_cyc = 0.0;

            if (Rbn_SaveSimData.Checked)
            {
                if (SampCount > 0)
                {
                    WriteSimData(SimFileName, TimeElp, SampCount);
                    WriteSimDataBybeat(SimFileName_beat, TimeElp, SampCount_beat);
                };
            }
            else if (Rbn_SaveLC.Checked && BlnLastCycle == true)
            {
                if (SampCount > 0)
                {

                    if (BlnRunProtocolOn == false)
                    {
                        WriteSimData(LCFileName, TimeElp, SampCount);
                    }
                    else
                    {
                        WriteSimData(LCFNameForProtocol, TimeElp, SampCount);
                    }
                };
            };
        }


        private void SetInitMinMax(ref cCell myCell)
        {
            myCell.Vm_Min = myCell.TVc[Pd.IdxVm];
            myCell.Vm_Max = myCell.TVc[Pd.IdxVm];

            myCell.Cyt.Nai_Min = myCell.TVc[Pd.IdxNai];
            myCell.Cyt.Nai_Max = myCell.TVc[Pd.IdxNai];

            myCell.JS.Najs_Min = myCell.TVc[Pd.IdxNajs];
            myCell.JS.Najs_Max = myCell.TVc[Pd.IdxNajs];

            myCell.SL.Nasl_Min = myCell.TVc[Pd.IdxNasl];
            myCell.SL.Nasl_Max = myCell.TVc[Pd.IdxNasl];

            myCell.Cyt.Cai_Min = myCell.TVc[Pd.IdxCai];
            myCell.Cyt.Cai_Max = myCell.TVc[Pd.IdxCai];

            myCell.JS.Cajs_Min = myCell.TVc[Pd.IdxCajs];
            myCell.JS.Cajs_Max = myCell.TVc[Pd.IdxCajs];

            myCell.SL.Casl_Min = myCell.TVc[Pd.IdxCasl];
            myCell.SL.Casl_Max = myCell.TVc[Pd.IdxCasl];

            myCell.SR.Casr_Min = myCell.TVc[Pd.IdxCasr];
            myCell.SR.Casr_Max = myCell.TVc[Pd.IdxCasr];

            myCell.Mitochondria.Camit_Min = myCell.TVc[Pd.InxCamit];
            myCell.Mitochondria.Camit_Max = myCell.TVc[Pd.InxCamit];

            myCell.Contraction.Fb_Min = myCell.TVc[Pd.InxFb];
            myCell.Contraction.Fb_Max = myCell.TVc[Pd.InxFb];

            myCell.Contraction.hsmL_Min = myCell.TVc[Pd.IdxhsmL];
            myCell.Contraction.hsmL_Max = myCell.TVc[Pd.IdxhsmL];

            myCell.sumATPuse_contraction = 0.0;
            myCell.sumATPuse_INaKjs = 0.0;
            myCell.sumATPuse_INaKsl = 0.0;
            myCell.sumATPuse_IpCajs = 0.0;
            myCell.sumATPuse_IpCasl = 0.0;
            myCell.sumATPuse_SERCA = 0.0;
            myCell.sumATPuse_NmSC = 0.0;
            myCell.sumVO2 = 0.0;

            meanCamittemp = 0.0;
            myCell.sumJ_C1 = 0.0;
            myCell.sumJ_PDHC = 0.0;
            myCell.sumJ_ICDH = 0.0;
            myCell.sumJ_OGDH = 0.0;
            myCell.sumJ_MDH = 0.0;
            myCell.sumJ_DHall = 0.0;
        }

        private void FindMinMax(ref cCell myCell)
        {
            if (myCell.Vm_Min >= myCell.TVc[Pd.IdxVm])
            {
                myCell.Vm_Min = myCell.TVc[Pd.IdxVm];
            };
            if (myCell.Vm_Max <= myCell.TVc[Pd.IdxVm])
            {
                myCell.Vm_Max = myCell.TVc[Pd.IdxVm];
            };

            if (myCell.Cyt.Nai_Min >= myCell.TVc[Pd.IdxNai])
            {
                myCell.Cyt.Nai_Min = myCell.TVc[Pd.IdxNai];
            };
            if (myCell.Cyt.Nai_Max <= myCell.TVc[Pd.IdxNai])
            {
                myCell.Cyt.Nai_Max = myCell.TVc[Pd.IdxNai];
            };

            if (myCell.JS.Najs_Min >= myCell.TVc[Pd.IdxNajs])
            {
                myCell.JS.Najs_Min = myCell.TVc[Pd.IdxNajs];
            };
            if (myCell.JS.Najs_Max <= myCell.TVc[Pd.IdxNajs])
            {
                myCell.JS.Najs_Max = myCell.TVc[Pd.IdxNajs];
            };

            if (myCell.SL.Nasl_Min >= myCell.TVc[Pd.IdxNasl])
            {
                myCell.SL.Nasl_Min = myCell.TVc[Pd.IdxNasl];
            };
            if (myCell.SL.Nasl_Max <= myCell.TVc[Pd.IdxNasl])
            {
                myCell.SL.Nasl_Max = myCell.TVc[Pd.IdxNasl];
            };

            if (myCell.Cyt.Cai_Min >= myCell.TVc[Pd.IdxCai])
            {
                myCell.Cyt.Cai_Min = myCell.TVc[Pd.IdxCai];
            };
            if (myCell.Cyt.Cai_Max <= myCell.TVc[Pd.IdxCai])
            {
                myCell.Cyt.Cai_Max = myCell.TVc[Pd.IdxCai];
            };

            if (myCell.JS.Cajs_Min >= myCell.TVc[Pd.IdxCajs])
            {
                myCell.JS.Cajs_Min = myCell.TVc[Pd.IdxCajs];
            };
            if (myCell.JS.Cajs_Max <= myCell.TVc[Pd.IdxCajs])
            {
                myCell.JS.Cajs_Max = myCell.TVc[Pd.IdxCajs];
            };

            if (myCell.SL.Casl_Min >= myCell.TVc[Pd.IdxCasl])
            {
                myCell.SL.Casl_Min = myCell.TVc[Pd.IdxCasl];
            };
            if (myCell.SL.Casl_Max <= myCell.TVc[Pd.IdxCasl])
            {
                myCell.SL.Casl_Max = myCell.TVc[Pd.IdxCasl];
            };

            if (myCell.SR.Casr_Min >= myCell.TVc[Pd.IdxCasr])
            {
                myCell.SR.Casr_Min = myCell.TVc[Pd.IdxCasr];
            };
            if (myCell.SR.Casr_Max <= myCell.TVc[Pd.IdxCasr])
            {
                myCell.SR.Casr_Max = myCell.TVc[Pd.IdxCasr];
            };

            if (myCell.Mitochondria.Camit_Min >= myCell.TVc[Pd.InxCamit])
            {
                myCell.Mitochondria.Camit_Min = myCell.TVc[Pd.InxCamit];
            };
            if (myCell.Mitochondria.Camit_Max <= myCell.TVc[Pd.InxCamit])
            {
                myCell.Mitochondria.Camit_Max = myCell.TVc[Pd.InxCamit];
            };

            if (myCell.Contraction.Fb_Min >= myCell.TVc[Pd.InxFb])
            {
                myCell.Contraction.Fb_Min = myCell.TVc[Pd.InxFb];
            };
            if (myCell.Contraction.Fb_Max <= myCell.TVc[Pd.InxFb])
            {
                myCell.Contraction.Fb_Max = myCell.TVc[Pd.InxFb];
            };

            if (myCell.Contraction.hsmL_Min >= myCell.TVc[Pd.IdxhsmL])
            {
                myCell.Contraction.hsmL_Min = myCell.TVc[Pd.IdxhsmL];
            };
            if (myCell.Contraction.hsmL_Max <= myCell.TVc[Pd.IdxhsmL])
            {
                myCell.Contraction.hsmL_Max = myCell.TVc[Pd.IdxhsmL];
            };

            myCell.sumATPuse_contraction = myCell.sumATPuse_contraction + dt * myCell.TVc[Pd.InxdATPuse_contraction];
            myCell.sumATPuse_INaKjs = myCell.sumATPuse_INaKjs + dt * myCell.TVc[Pd.InxdATPuse_INaKjs];
            myCell.sumATPuse_INaKsl = myCell.sumATPuse_INaKsl + dt * myCell.TVc[Pd.InxdATPuse_INaKsl];
            myCell.sumATPuse_IpCajs = myCell.sumATPuse_IpCajs + dt * myCell.TVc[Pd.InxdATPuse_IpCajs];
            myCell.sumATPuse_IpCasl = myCell.sumATPuse_IpCasl + dt * myCell.TVc[Pd.InxdATPuse_IpCasl];
            myCell.sumATPuse_SERCA = myCell.sumATPuse_SERCA + dt * myCell.TVc[Pd.InxdATPuse_SERCA];
            myCell.sumATPuse_NmSC = myCell.sumATPuse_NmSC + dt * myCell.TVc[Pd.InxdATPuse_NmSC];
            myCell.sumVO2 = myCell.sumVO2 + dt * myCell.TVc[Pd.InxVO2];

            meanCamittemp = meanCamittemp + myCell.TVc[Pd.InxCamit] * dt; 

            myCell.sumJ_C1 = myCell.sumJ_C1 + dt * myCell.TVc[Pd.InxJ_C1];
            myCell.sumJ_PDHC = myCell.sumJ_PDHC + dt * myCell.TVc[Pd.InxJ_PDHC];
            myCell.sumJ_ICDH = myCell.sumJ_ICDH + dt * myCell.TVc[Pd.InxJ_ICDH];
            myCell.sumJ_OGDH = myCell.sumJ_OGDH + dt * myCell.TVc[Pd.InxJ_OGDH];
            myCell.sumJ_MDH = myCell.sumJ_MDH + dt * myCell.TVc[Pd.InxJ_MDH];
            myCell.sumJ_DHall = myCell.sumJ_DHall + dt * myCell.TVc[Pd.InxJ_PDHC] + dt * myCell.TVc[Pd.InxJ_ICDH] + dt * myCell.TVc[Pd.InxJ_OGDH] + dt * myCell.TVc[Pd.InxJ_MDH];
        }

        private void DispMinMax(cCell myCell)
        {
            double Nai;
            double Najs;

            // 24Apr11
            double Fb;
            
            double Cai1000;
            double Cajs1000;
            double Casl1000;
            double Casr;
            double Camit1000;

            double sumATPuse_contraction1000;
            double sumATPuse_INaK1000;
            double sumATPuse_IpCa1000;
            double sumATPuse_SERCA1000;
            double sumVO21000;

            double sumATPuse_contraction_percent;
            double sumATPuse_INaK_percent;
            double sumATPuse_IpCa_percent;
            double sumATPuse_SERCA_percent;
            double total_sumATPuse;

            lbl_VmMin.Text = String.Format("{0,3:0.0}", myCell.Vm_Min);
            lbl_VmMax.Text = String.Format("{0,3:0.0}", myCell.Vm_Max);

            Nai = myCell.Cyt.Nai_Min;
            lbl_NaiMin.Text = String.Format("{0,3:0.00}", Nai);
            Nai = myCell.Cyt.Nai_Max;
            lbl_NaiMax.Text = String.Format("{0,3:0.00}", Nai);
            Najs = myCell.JS.Najs_Min;
            lbl_NajsMin.Text = String.Format("{0,3:0.00}", Najs);
            Najs = myCell.JS.Najs_Max;
            lbl_NajsMax.Text = String.Format("{0,3:0.00}", Najs);

            // 24Apr11
            Fb = myCell.Contraction.Fb_Min;
            lbl_FbMin.Text = String.Format("{0,3:0.00}", Fb);
            Fb = myCell.Contraction.Fb_Max;
            lbl_FbMax.Text = String.Format("{0,3:0.00}", Fb);

            Cai1000 = myCell.Cyt.Cai_Min * 1000;
            lbl_CaiMin.Text = String.Format("{0,3:0.00}", Cai1000);
            Cai1000 = myCell.Cyt.Cai_Max * 1000;
            lbl_CaiMax.Text = String.Format("{0,3:0.00}", Cai1000);
            Cajs1000 = myCell.JS.Cajs_Min * 1000;
            lbl_CajsMin.Text = String.Format("{0,3:0.00}", Cajs1000);
            Cajs1000 = myCell.JS.Cajs_Max * 1000;
            lbl_CajsMax.Text = String.Format("{0,3:0.00}", Cajs1000);
            Casl1000 = myCell.SL.Casl_Min * 1000;
            lbl_CaslMin.Text = String.Format("{0,3:0.00}", Casl1000);
            Casl1000 = myCell.SL.Casl_Max * 1000;
            lbl_CaslMax.Text = String.Format("{0,3:0.00}", Casl1000);

            Casr = myCell.SR.Casr_Min;
            lbl_CasrMin.Text = String.Format("{0,3:0.00}", Casr);
            Casr = myCell.SR.Casr_Max;
            lbl_CasrMax.Text = String.Format("{0,3:0.00}", Casr);

            Camit1000 = myCell.Mitochondria.Camit_Min * 1000;
            lbl_CamitMin.Text = String.Format("{0,3:0.00}", Camit1000);
            Camit1000 = myCell.Mitochondria.Camit_Max * 1000;
            lbl_CamitMax.Text = String.Format("{0,3:0.00}", Camit1000);

            sumATPuse_contraction1000 = myCell.sumATPuse_contraction * 1000;
            lbl_sumATPuse_contraction.Text = String.Format("{0,3:0.0}", sumATPuse_contraction1000);
            sumATPuse_INaK1000 = (myCell.sumATPuse_INaKjs + myCell.sumATPuse_INaKsl) * 1000;
            lbl_sumATPuse_INaK.Text = String.Format("{0,3:0.0}", sumATPuse_INaK1000);
            sumATPuse_IpCa1000 = (myCell.sumATPuse_IpCajs + myCell.sumATPuse_IpCasl) * 1000;
            lbl_sumATPuse_IpCa.Text = String.Format("{0,3:0.0}", sumATPuse_IpCa1000);
            sumATPuse_SERCA1000 = (myCell.sumATPuse_SERCA + myCell.sumATPuse_NmSC) * 1000;
            lbl_sumATPuse_SERCA.Text = String.Format("{0,3:0.0}", sumATPuse_SERCA1000);

            total_sumATPuse = myCell.sumATPuse_contraction + myCell.sumATPuse_INaKjs + myCell.sumATPuse_INaKsl + myCell.sumATPuse_IpCajs + myCell.sumATPuse_IpCasl
                     + myCell.sumATPuse_SERCA + myCell.sumATPuse_NmSC;
            sumATPuse_contraction_percent = 100 * myCell.sumATPuse_contraction / total_sumATPuse;
            lbl_sumATPuse_contraction_percent.Text = String.Format("{0,3:0.0}", sumATPuse_contraction_percent);
            sumATPuse_INaK_percent = 100 * (myCell.sumATPuse_INaKjs + myCell.sumATPuse_INaKsl) / total_sumATPuse;
            lbl_sumATPuse_INaK_percent.Text = String.Format("{0,3:0.0}", sumATPuse_INaK_percent);
            sumATPuse_IpCa_percent = 100 * (myCell.sumATPuse_IpCajs + myCell.sumATPuse_IpCasl) / total_sumATPuse;
            lbl_sumATPuse_IpCa_percent.Text = String.Format("{0,3:0.0}", sumATPuse_IpCa_percent);
            sumATPuse_SERCA_percent = 100 * (myCell.sumATPuse_SERCA + myCell.sumATPuse_NmSC) / total_sumATPuse;
            lbl_sumATPuse_SERCA_percent.Text = String.Format("{0,3:0.0}", sumATPuse_SERCA_percent);

            sumVO21000 = -(myCell.sumVO2) * 1000;
            lbl_VO2.Text = String.Format("{0,3:0.0}", sumVO21000);

            if (Cell.BlnCamitFix == false)
            {
                myCell.Mitochondria.meanCamit = meanCamittemp / Cell.MSet.StimCycle;
                lbl_meanCamit.Text = String.Format("{0,3:0.00}", myCell.Mitochondria.meanCamit * 1000);
                myCell.TVc[Pd.InxmeanCamit] = myCell.Mitochondria.meanCamit;
            }
            else
            {
            }

            myCell.TVc[Pd.InxVm_Min] = myCell.Vm_Min;
            myCell.TVc[Pd.InxVm_Max] = myCell.Vm_Max;
            myCell.TVc[Pd.InxNai_Min] = myCell.Cyt.Nai_Min;
            myCell.TVc[Pd.InxNai_Max] = myCell.Cyt.Nai_Max;
            myCell.TVc[Pd.InxNajs_Min] = myCell.JS.Najs_Min;
            myCell.TVc[Pd.InxNajs_Max] = myCell.JS.Najs_Max;
            myCell.TVc[Pd.InxNasl_Min] = myCell.SL.Nasl_Min;
            myCell.TVc[Pd.InxNasl_Max] = myCell.SL.Nasl_Max;
            myCell.TVc[Pd.InxCai_Min] = myCell.Cyt.Cai_Min;
            myCell.TVc[Pd.InxCai_Max] = myCell.Cyt.Cai_Max;
            myCell.TVc[Pd.InxCajs_Min] = myCell.JS.Cajs_Min;
            myCell.TVc[Pd.InxCajs_Max] = myCell.JS.Cajs_Max;
            myCell.TVc[Pd.InxCasl_Min] = myCell.SL.Casl_Min;
            myCell.TVc[Pd.InxCasl_Max] = myCell.SL.Casl_Max;
            myCell.TVc[Pd.InxCasr_Min] = myCell.SR.Casr_Min;
            myCell.TVc[Pd.InxCasr_Max] = myCell.SR.Casr_Max;
            myCell.TVc[Pd.InxCamit_Min] = myCell.Mitochondria.Camit_Min;
            myCell.TVc[Pd.InxCamit_Max] = myCell.Mitochondria.Camit_Max;
        }

        private void SaveSimData(string strFileName, double ElapsedTime, int SamplCount)
        {
            int i = -1;
            if (SamplCount < Pd.mTVMax)
            {
                InputmTV(++i, SamplCount, ElapsedTime / 1000, "Time(sec)");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxVm], "Vm");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxNajs], "Najs");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxNasl], "Nasl");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxCai], "Cai");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxCajs], "Cajs");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxCasl], "Casl");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxCasr], "Casr");

                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxINajs_Itc], "INajs_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxINasl_Itc], "INasl_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxINabjs_Itc], "INabjs_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxINabsl_Itc], "INabsl_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxINaKjs_Itc], "INaKjs_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxINaKsl_Itc], "INaKsl_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxIKrjs_Itc], "IKrjs_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxIKrsl_Itc], "IKrsl_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxIKsjs_Itc], "IKsjs_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxIKssl_Itc], "IKssl_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxIKpjs_Itc], "IKpjs_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxIKpsl_Itc], "IKpsl_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxIClCajs_Itc], "IClCajs_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxIClCasl_Itc], "IClCasl_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxICaLjs_Itc], "ICaLjs_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxICaL_Cajs], "ICaL_Cajs");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxICaL_Najs], "ICaL_Najs");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxICaL_Kjs], "ICaL_Kjs");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxICaLsl_Itc], "ICaLsl_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxICaL_Casl], "ICaL_Casl");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxICaL_Nasl], "ICaL_Nasl");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxICaL_Ksl], "ICaL_Ksl");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxINaCajs_Itc], "INaCajs_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxINaCasl_Itc], "INaCasl_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxIpCajs_Itc], "IpCajs_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxIpCasl_Itc], "IpCasl_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxICabjs_Itc], "ICabjs_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxICabsl_Itc], "ICabsl_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxItosjs_Itc], "Itosjs_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxItossl_Itc], "Itossl_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxItofjs_Itc], "Itofjs_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxItofsl_Itc], "Itofsl_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxIK1js_Itc], "IK1js_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxIK1sl_Itc], "IK1sl_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxIClbjs_Itc], "IClbjs_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxIClbsl_Itc], "IClbsl_Itc");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_Cadiff_cytsl], "JCadiff_cytsl");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_Cadiff_jssl], "JCadiff_jssl");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_SERCA], "JSERCA");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_RyR], "JRyR");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_Leak], "JLeak");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_CSQN], "JCSQN");

                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxFb], "Fb");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxTwitch], "Twitch");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxhsmL], "hsmL");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxhsmX], "hsmX");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxTrnCa], "TrnCa");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxTrnCa_cb], "TrnCa_cb");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxTrn_cb], "Trn_cb");

                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_Ca_contraction], "J_Ca_contraction");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxdATPuse_contraction], "dATPuse_contraction");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxdATPuse_INaKjs], "dATPuse_INaKjs");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxdATPuse_INaKsl], "dATPuse_INaKsl");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxdATPuse_IpCajs], "dATPuse_IpCajs");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxdATPuse_IpCasl], "dATPuse_IpCasl");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxdATPuse_SERCA], "dATPuse_SERCA");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxdATPuse_NmSC], "dATPuse_NmSC");

                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxATPiTotal], "ATPiTotal");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxATPiFree], "ATPiFree");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxATPiMg], "ATPiMg");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxADPiTotal], "ADPiTotal");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxADPiFree], "ADPiFree");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxADPiMg], "ADPiMg");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxAMPi], "AMPi");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxPCr], "PCr");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxCr], "Cr");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxPIi], "PIi");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxNamit], "Namit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxKmit], "Kmit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxCamit], "Camit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxHmit], "Hmit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxpHmit], "pHmit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxdP], "dP");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxdPsi], "dPsi");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxdpH], "dpH");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxATPmitTotal], "ATPmitTotal");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxATPmitFree], "ATPmitFree");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxATPmitMg], "ATPmitMg");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxADPmitTotal], "ADPmitTotal");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxADPmitFree], "ADPmitFree");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxADPmitMg], "ADPmitMg");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxGTPmit], "GTPmit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxGDPmit], "GDPmit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxPImit], "PImit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxNADH], "NADH");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxNAD], "NAD");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxCytc_r], "Cytc_r");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxCytc_o], "Cytc_o");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxCyta_r], "Cyta_r");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxCyta_o], "Cyta_o");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxUQH2], "UQH2");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxUQ], "UQ");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxCoAMit], "CoAMit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxAcetylCoAMit], "AcetylCoAMit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxSuccinylCoAMit], "SuccinylCoAMit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxMalateMit], "MalateMit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxGlutamateMit], "GlutamateMit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxAspartateMit], "AspartateMit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxPyruvateMit], "PyruvateMit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxOxoglutarateMit], "OxoglutarateMit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxCitrateMit], "CitrateMit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxOxaloacetateMit], "OxaloacetateMit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxIsocitrateMit], "IsocitrateMit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxIsocitrateFreeMit], "IsocitrateFreeMit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxIsocitrateMgMit], "IsocitrateMgMit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxSuccinateMit], "SuccinateMit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.IdxFumarateMit], "FumarateMit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_C1], "J_C1");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_C3], "J_C3");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_C4], "J_C4");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_ATPsyn], "J_ATPsyn");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_ANT], "J_ANT");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_PiC], "J_PiC");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_CS], "J_CS");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_ACO], "J_ACO");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_ICDH], "J_ICDH");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_OGDH], "J_OGDH");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_SCS], "J_SCS");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_SDH], "J_SDH");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_FH], "J_FH");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_MDH], "J_MDH");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_NDK], "J_NDK");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_AST], "J_AST");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_PDHC], "J_PDHC");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_PC], "J_PC");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_ALT], "J_ALT");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_MCT], "J_MCT");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_DCT], "J_DCT");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_TCT], "J_TCT");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_OGC], "J_OGC");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_AGC], "J_AGC");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_KUni], "J_KUni");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_KHE], "J_KHE");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_CaUni_cyt], "J_CaUni_cyt");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_CaUni_js], "J_CaUni_js");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_NCXmit], "J_NCXmit");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_NmSC], "J_NmSC");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_NmSC_block], "J_NmSC_block");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_NHE], "J_NHE");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_HCXmit], "J_HCXmit"); // 24Feb08
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_AK], "J_AK");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_CK], "J_CK");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxJ_cons], "J_cons");
                InputmTV(++i, SamplCount, Cell.TVc[Pd.InxVO2], "VO2");

                Pd.mTVIdxLast = i;
            };
        }

        private void SaveSimDataByBeat(string strFileName, double ElapsedTime, int SamplCount)
        {
            int i = -1;
            if (SamplCount < Pd.mTVMax_beat)
            {
                InputmTVbeat(++i, SamplCount, ElapsedTime / 1000, "Time(sec)");
                InputmTVbeat(++i, SamplCount, Cell.Vm_Min, "Vm_Min");
                InputmTVbeat(++i, SamplCount, Cell.Vm_Max, "Vm_Max");
                InputmTVbeat(++i, SamplCount, Cell.Cyt.Nai_Min, "Nai_Min");
                InputmTVbeat(++i, SamplCount, Cell.Cyt.Nai_Max, "Nai_Max");
                InputmTVbeat(++i, SamplCount, Cell.JS.Najs_Min, "Najs_Min");
                InputmTVbeat(++i, SamplCount, Cell.JS.Najs_Max, "Najs_Max");
                InputmTVbeat(++i, SamplCount, Cell.SL.Nasl_Min, "Nasl_Min");
                InputmTVbeat(++i, SamplCount, Cell.SL.Nasl_Max, "Nasl_Max");
                InputmTVbeat(++i, SamplCount, Cell.Cyt.Cai_Min, "Cai_Min");
                InputmTVbeat(++i, SamplCount, Cell.Cyt.Cai_Max, "Cai_Max");
                InputmTVbeat(++i, SamplCount, Cell.JS.Cajs_Min, "Cajs_Min");
                InputmTVbeat(++i, SamplCount, Cell.JS.Cajs_Max, "Cajs_Max");
                InputmTVbeat(++i, SamplCount, Cell.SL.Casl_Min, "Casl_Min");
                InputmTVbeat(++i, SamplCount, Cell.SL.Casl_Max, "Casl_Max");
                InputmTVbeat(++i, SamplCount, Cell.SR.Casr_Min, "Casr_Min");
                InputmTVbeat(++i, SamplCount, Cell.SR.Casr_Max, "Casr_Max");
                InputmTVbeat(++i, SamplCount, Cell.Mitochondria.Camit_Min, "Camit_Min");
                InputmTVbeat(++i, SamplCount, Cell.Mitochondria.Camit_Max, "Camit_Max");

                InputmTVbeat(++i, SamplCount, Cell.sumATPuse_contraction, "sumATPuse_contraction");
                InputmTVbeat(++i, SamplCount, Cell.sumATPuse_INaKjs, "sumATPuse_INaKjs");
                InputmTVbeat(++i, SamplCount, Cell.sumATPuse_INaKsl, "sumATPuse_INaKsl");
                InputmTVbeat(++i, SamplCount, Cell.sumATPuse_IpCajs, "sumATPuse_IpCajs");
                InputmTVbeat(++i, SamplCount, Cell.sumATPuse_IpCasl, "sumATPuse_IpCasl");
                InputmTVbeat(++i, SamplCount, Cell.sumATPuse_SERCA, "sumATPuse_SERCA");
                InputmTVbeat(++i, SamplCount, Cell.sumATPuse_NmSC, "sumATPuse_NmSC");
                InputmTVbeat(++i, SamplCount, Cell.sumVO2, "sumVO2");

                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxATPiTotal], "ATPiTotal");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxATPiFree], "ATPiFree");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxATPiMg], "ATPiMg");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxADPiTotal], "ADPiTotal");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxADPiFree], "ADPiFree");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxADPiMg], "ADPiMg");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxAMPi], "AMPi");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxPCr], "PCr");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxCr], "Cr");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxPIi], "PIi");

                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxNamit], "Namit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxKmit], "Kmit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxHmit], "Hmit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxpHmit], "pHmit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxdP], "dP");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxdPsi], "dPsi");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxdpH], "dpH");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxATPmitTotal], "ATPmitTotal");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxATPmitFree], "ATPmitFree");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxATPmitMg], "ATPmitMg");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxADPmitTotal], "ADPmitTotal");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxADPmitFree], "ADPmitFree");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxADPmitMg], "ADPmitMg");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxGTPmit], "GTPmit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxGDPmit], "GDPmit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxPImit], "PImit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxNADH], "NADH");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxNAD], "NAD");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxCytc_r], "Cytc_r");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxCytc_o], "Cytc_o");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxCyta_r], "Cyta_r");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxCyta_o], "Cyta_o");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxUQH2], "UQH2");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxUQ], "UQ");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxCoAMit], "CoAMit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxAcetylCoAMit], "AcetylCoAMit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxSuccinylCoAMit], "SuccinylCoAMit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxMalateMit], "MalateMit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxGlutamateMit], "GlutamateMit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxAspartateMit], "AspartateMit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxPyruvateMit], "PyruvateMit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxOxoglutarateMit], "OxoglutarateMit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxCitrateMit], "CitrateMit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxOxaloacetateMit], "OxaloacetateMit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxIsocitrateMit], "IsocitrateMit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxIsocitrateFreeMit], "IsocitrateFreeMit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxIsocitrateMgMit], "IsocitrateMgMit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxSuccinateMit], "SuccinateMit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.IdxFumarateMit], "FumarateMit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_C1], "J_C1");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_C3], "J_C3");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_C4], "J_C4");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_ATPsyn], "J_ATPsyn");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_ANT], "J_ANT");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_PiC], "J_PiC");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_CS], "J_CS");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_ACO], "J_ACO");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_ICDH], "J_ICDH");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_OGDH], "J_OGDH");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_SCS], "J_SCS");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_SDH], "J_SDH");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_FH], "J_FH");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_MDH], "J_MDH");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_NDK], "J_NDK");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_AST], "J_AST");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_PDHC], "J_PDHC");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_PC], "J_PC");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_ALT], "J_ALT");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_MCT], "J_MCT");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_DCT], "J_DCT");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_TCT], "J_TCT");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_OGC], "J_OGC");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_AGC], "J_AGC");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_KUni], "J_KUni");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_KHE], "J_KHE");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_CaUni_cyt], "J_CaUni_cyt");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_CaUni_js], "J_CaUni_js");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_NCXmit], "J_NCXmit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_NmSC], "J_NmSC");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_NmSC_block], "J_NmSC_block");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_NHE], "J_NHE");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_HCXmit], "J_HCXmit");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_AK], "J_AK");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_CK], "J_CK");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxJ_cons], "J_cons");
                InputmTVbeat(++i, SamplCount, Cell.TVc[Pd.InxVO2], "VO2");
                InputmTVbeat(++i, SamplCount, Cell.Mitochondria.meanCamit, "meanCamit");
                InputmTVbeat(++i, SamplCount, Cell.meanCai, "meanCai");

                InputmTVbeat(++i, SamplCount, Cell.sumJ_C1, "sumJ_C1");
                InputmTVbeat(++i, SamplCount, Cell.sumJ_PDHC, "sumJ_PDHC");
                InputmTVbeat(++i, SamplCount, Cell.sumJ_ICDH, "sumJ_ICDH");
                InputmTVbeat(++i, SamplCount, Cell.sumJ_OGDH, "sumJ_OGDH");
                InputmTVbeat(++i, SamplCount, Cell.sumJ_MDH, "sumJ_MDH");
                InputmTVbeat(++i, SamplCount, Cell.sumJ_DHall, "sumJ_DHall");

                InputmTVbeat(++i, SamplCount, Cell.MSet.StimCycle, "StimCycle");
                InputmTVbeat(++i, SamplCount, Cell.Contraction.EL, "EL");
                InputmTVbeat(++i, SamplCount, Cell.Contraction.Fb_Min, "Fb_Min");
                InputmTVbeat(++i, SamplCount, Cell.Contraction.Fb_Max, "Fb_Max");

                InputmTVbeat(++i, SamplCount, Cell.Contraction.hsmL_Min, "hsmL_Min");
                InputmTVbeat(++i, SamplCount, Cell.Contraction.hsmL_Max, "hsmL_Max");

                Pd.mTVIdxLast_beat = i;  // i - 1;      //  Must Be equal to or smaller than mTVIdxMax
            };
        }

        private void WriteSimData(string strFileName, double ElapsedTime, int SamplCount)
        {
            string Strtemp = "";
            System.IO.StreamWriter sw1 = new System.IO.StreamWriter(strFileName, false, Encoding.GetEncoding("Shift_JIS"));

            int j = -1;
            for (j = 0; j < Pd.mTVIdxLast; j++)
            {
                Strtemp = Strtemp + Cell.mTV[j].Str + ",";
            }
            j = Pd.mTVIdxLast;
            Strtemp = Strtemp + Cell.mTV[j].Str;
            sw1.WriteLine(Strtemp);

            Strtemp = "";
            int i;
            for (i = 0; i < (SamplCount - 1); i++)
            {
                Strtemp = "";
                for (j = 0; j < Pd.mTVIdxLast; j++)
                {
                    Strtemp = Strtemp + Cell.mTV[j].Val[i].ToString() + ",";
                    Cell.mTV[j].Val[i] = 0.0;       

                }
                j = Pd.mTVIdxLast;
                Strtemp = Strtemp + Cell.mTV[j].Val[i].ToString();
                Cell.mTV[j].Val[i] = 0.0;       
                sw1.WriteLine(Strtemp);
            }
            sw1.Close();
        }

        private void WriteSimDataBybeat(string strFileName, double ElapsedTime, int SamplCount)
        {
            string Strtemp = "";
            System.IO.StreamWriter sw1 = new System.IO.StreamWriter(strFileName, false, Encoding.GetEncoding("Shift_JIS"));

            int j = -1;
            for (j = 0; j < Pd.mTVIdxLast_beat; j++)
            {
                Strtemp = Strtemp + Cell.mTVbeat[j].Str + ",";
            }
            j = Pd.mTVIdxLast_beat;
            Strtemp = Strtemp + Cell.mTVbeat[j].Str;
            sw1.WriteLine(Strtemp);

            Strtemp = "";
            int i;
            for (i = 0; i < (SamplCount - 1); i++)
            {
                Strtemp = "";
                for (j = 0; j < Pd.mTVIdxLast_beat; j++)
                {
                    Strtemp = Strtemp + Cell.mTVbeat[j].Val[i].ToString() + ",";
                    Cell.mTVbeat[j].Val[i] = 0.0;       

                }
                j = Pd.mTVIdxLast_beat;
                Strtemp = Strtemp + Cell.mTVbeat[j].Val[i].ToString();
                Cell.mTVbeat[j].Val[i] = 0.0;       
                sw1.WriteLine(Strtemp);
            }
            sw1.Close();
        }

        private void InputmTV(int i, int SampCount, double Value, string ValStr)
        {
            if (SampCount == 0)
            {
                Cell.mTV[i].Str = ValStr;
                Cell.mTV[i].Val[SampCount] = Value;
            }
            else
            {
                Cell.mTV[i].Str = ValStr;
                Cell.mTV[i].Val[SampCount] = Value;
            }
        }

        private void InputmTVbeat(int i, int SampCount, double Value, string ValStr)
        {
            if (SampCount == 0)
            {
                Cell.mTVbeat[i].Str = ValStr;
                Cell.mTVbeat[i].Val[SampCount] = Value;
            }
            else
            {
                Cell.mTVbeat[i].Str = ValStr;
                Cell.mTVbeat[i].Val[SampCount] = Value;
            }
        }

        private void ReadInitVals()
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(InitFileName, Encoding.GetEncoding("Shift_JIS"));
            int j = -1;
            while (sr.EndOfStream == false)
            {
                j = j + 1;
                if (sr.EndOfStream == true) break;
                string line = sr.ReadLine();
                string[] fields = line.Split(',');

                for (int i = 0; i < fields.Length; i++)
                {
                    if ((i == 1) && (j < Cell.TVc.Length))
                    {
                        Cell.TVc[j] = Convert.ToDouble(fields[i]);
                    }
                }
            }
            sr.Close();
        }

        private void SaveInitVals(String FName)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter(FName, false, Encoding.GetEncoding("Shift_JIS")); //20211218

            int j = -1;
            for (j = 0; j < Pd.IdxLast; j++)
            {
                writer.WriteLine(Cell.TVcStr[j] + "," + Cell.TVc[j]);
            }
            writer.Close();
        }

        private void ResetVariables()
        {
            ReadInitVals();

            TimeElp = 0.0;
            TimeElp_Sht = 0.0;
            TimeElp_Lng = 0.0;
            ElapsedTime_cyc = 0.0;

            lbl_Time.Text = "0.0";
            lblTime_min.Text = "0.0";

            lbl_Time.Refresh();
            lblTime_min.Refresh();

            Pd.BlnFirstLoad = true;
            Cell.InitializeCell(ref Cell, ref ListForm);
            Pd.BlnFirstLoad = false;

            DispForm1.EraseGraphs();
            DispForm2.EraseGraphs();
            DispForm3.EraseGraphs();
            DispForm4.EraseGraphs();
            DispForm5.EraseGraphs();
            DispForm6.EraseGraphs();
            DispForm7.EraseGraphs();
            DispForm8.EraseGraphs();

            ResetToolStripMenuItem.Enabled = true;
            StartToolStripMenuItem.Enabled = true;
            StopToolStripMenuItem.Enabled = true;
            SaveToolStripMenuItem.Enabled = true;
        }

        private void ResetNumericvaluesInTabs()
        {
            Txt_OneCycle.Text = "1000";
            Txt_SimeTimeMax.Text = "60";
            ComboBox1.Text = "s";
            Txt_LngDispTimeMax.Text = "2000";
            ComboBox2.Text = "ms";
            Txt_PlotIntv.Text = "1.0";
            Txt_SimSaveIntV.Text = "1.0";
        }

        private int Roundoff(double dValue)
        {
            return (int)Math.Round(dValue, MidpointRounding.AwayFromZero);
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string FNametemp;
            OpenFileDialog sfd = new OpenFileDialog();
            string strCurrentDir = System.Environment.CurrentDirectory;

            // Init file exists in \IHVCM\HumanVentricularCell\InitFiles\HumanVentricularCell.asc

            DirectoryInfo di = new DirectoryInfo(strCurrentDir); //parrent
            DirectoryInfo diParent = di.Parent;
            strCurrentDir = diParent.FullName;
            DirectoryInfo di2 = new DirectoryInfo(strCurrentDir); //parrent2
            DirectoryInfo di2Parent = di2.Parent;
            strCurrentDir = di2Parent.FullName;
            DirectoryInfo di3 = new DirectoryInfo(strCurrentDir); //parrent3
            DirectoryInfo di3Parent = di3.Parent;
            strCurrentDir = di3Parent.FullName;

            strCurrentDir = di2Parent.FullName;
            DirectoryInfo di3 = new DirectoryInfo(strCurrentDir); //parrent3  now project folder
            DirectoryInfo di3Parent = di3.Parent;
            strCurrentDir = di3Parent.FullName;


            strCurrentDir = di2Parent.FullName;
            DirectoryInfo di3 = new DirectoryInfo(strCurrentDir); //parrent3  now project folder
            DirectoryInfo di3Parent = di3.Parent;
            strCurrentDir = di3Parent.FullName;


            Pd.NameTV(Cell.TVcStr);

            if (Rbtn_DefaultInitFile.Checked == true)
            {
                InitFileName = strCurrentDir + '\\' + "InitFiles" + '\\' + "HumanVentricularCell.asc";
                ReadInitVals();
            }
            else if (Rbtn_OtherInitFile.Checked)
            {
                sfd.FileName = "HumanVentricularCell.asc";

                sfd.Filter = "Init file (*.asc)|*.asc|All files(*.*)|*.*";

                sfd.FilterIndex = 1;

                sfd.Title = "Select one Init file";

                sfd.RestoreDirectory = true;

                sfd.CheckPathExists = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    FNametemp = System.IO.Path.GetFileName(sfd.FileName);   
                    InitFileName = sfd.FileName;                            
                    ReadInitVals();                                        
                }
            };

            TimeElp = 0.0;
            TimeElp_Sht = 0.0;
            TimeElp_Lng = 0.0;
            ElapsedTime_cyc = 0.0;

            lbl_Time.Text = "0.0";
            lblTime_min.Text = "0.0";

            lbl_Time.Refresh();
            lblTime_min.Refresh();

            Pd.BlnFirstLoad = true;
            Cell.InitializeCell(ref Cell, ref ListForm);
            Cell.DispValues(ref ListForm, ref Cell.TVc);
            Pd.BlnFirstLoad = false;

            DispForm1.EraseGraphs();
            DispForm2.EraseGraphs();
            DispForm3.EraseGraphs();
            DispForm4.EraseGraphs();
            DispForm5.EraseGraphs();
            DispForm6.EraseGraphs();
            DispForm7.EraseGraphs();
            DispForm8.EraseGraphs();

            ResetToolStripMenuItem.Enabled = true;
            StartToolStripMenuItem.Enabled = true;
            StopToolStripMenuItem.Enabled = true;
            SaveToolStripMenuItem.Enabled = true;
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.FileName = "HumanVentricularCell.asc";

            sfd.Filter = "asc file(*.asc)|*.asc|All files(*.*)|*.*";

            sfd.FilterIndex = 1;

            sfd.Title = "Select one file to be saved";

            sfd.RestoreDirectory = true;

            sfd.OverwritePrompt = true;

            sfd.CheckPathExists = true;

            string NewInitFileName;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                NewInitFileName = sfd.FileName;                           
                SaveInitVals(NewInitFileName);
            }

        }

        private void QuitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListForm.Hide();
            Close();
        }

        private void AllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetVariables();
            ResetNumericvaluesInTabs();
        }

        private void OnlyVariablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetVariables();
        }

        private void TabSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetNumericvaluesInTabs();
        }

        private void StartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetToolStripMenuItem.Enabled = false;
            StartToolStripMenuItem.Enabled = false;
            StopToolStripMenuItem.Enabled = true;
            FileToolStripMenuItem.Enabled = false;

            BlnStopFlag = false;

            SimCells();

            ResetToolStripMenuItem.Enabled = true;
            StartToolStripMenuItem.Enabled = true;
            StopToolStripMenuItem.Enabled = false;
            FileToolStripMenuItem.Enabled = true;

            BlnStopFlag = false;
        }

        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BlnStopFlag = true;
            ResetToolStripMenuItem.Enabled = true;
            StartToolStripMenuItem.Enabled = true;
            StopToolStripMenuItem.Enabled = false;
            FileToolStripMenuItem.Enabled = true;
        }

        private void EraseSubDispsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DispForm1.EraseGraphs();
            DispForm2.EraseGraphs();
            DispForm3.EraseGraphs();
            DispForm4.EraseGraphs();
            DispForm5.EraseGraphs();
            DispForm6.EraseGraphs();
            DispForm7.EraseGraphs();
            DispForm8.EraseGraphs();
        }

        private void WiderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double K = 1.1;

            DispForm1.Width = Roundoff(DispForm1.Width * K);
            DispForm2.Width = Roundoff(DispForm2.Width * K);
            DispForm3.Width = Roundoff(DispForm3.Width * K);
            DispForm4.Width = Roundoff(DispForm4.Width * K);
            DispForm5.Width = Roundoff(DispForm5.Width * K);
            DispForm6.Width = Roundoff(DispForm6.Width * K);
            DispForm7.Width = Roundoff(DispForm7.Width * K);
            DispForm8.Width = Roundoff(DispForm8.Width * K);

            AlignToolStripMenuItem_Click(this, e);
        }

        private void NarrowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double K = 0.9;
            DispForm1.Width = Roundoff(DispForm1.Width * K);
            DispForm2.Width = Roundoff(DispForm2.Width * K);
            DispForm3.Width = Roundoff(DispForm3.Width * K);
            DispForm4.Width = Roundoff(DispForm4.Width * K);
            DispForm5.Width = Roundoff(DispForm5.Width * K);
            DispForm6.Width = Roundoff(DispForm6.Width * K);
            DispForm7.Width = Roundoff(DispForm7.Width * K);
            DispForm8.Width = Roundoff(DispForm8.Width * K);

            AlignToolStripMenuItem_Click(this, e);
        }

        private void HigherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double K = 1.1;
            DispForm1.Height = Roundoff(DispForm1.Height * K);
            DispForm2.Height = Roundoff(DispForm2.Height * K);
            DispForm3.Height = Roundoff(DispForm3.Height * K);
            DispForm4.Height = Roundoff(DispForm4.Height * K);
            DispForm5.Height = Roundoff(DispForm5.Height * K);
            DispForm6.Height = Roundoff(DispForm6.Height * K);
            DispForm7.Height = Roundoff(DispForm7.Height * K);
            DispForm8.Height = Roundoff(DispForm8.Height * K);

            AlignToolStripMenuItem_Click(this, e);
        }

        private void LowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double K = 0.9;
            DispForm1.Height = Roundoff(DispForm1.Height * K);
            DispForm2.Height = Roundoff(DispForm2.Height * K);
            DispForm3.Height = Roundoff(DispForm3.Height * K);
            DispForm4.Height = Roundoff(DispForm4.Height * K);
            DispForm5.Height = Roundoff(DispForm5.Height * K);
            DispForm6.Height = Roundoff(DispForm6.Height * K);
            DispForm7.Height = Roundoff(DispForm7.Height * K);
            DispForm8.Height = Roundoff(DispForm8.Height * K);

            AlignToolStripMenuItem_Click(this, e);
        }

        private void AlignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DispForm1.Top = 0;
            DispForm2.Top = 0;
            DispForm3.Top = 0;
            DispForm4.Top = 0;
            DispForm5.Top = 0;
            DispForm6.Top = 0;
            DispForm7.Top = 0;
            DispForm8.Top = 0;

            ListForm.Top = 0;

            DispForm1.Left = 0;
            DispForm2.Left = DispForm1.Width;
            DispForm3.Left = DispForm1.Width + DispForm2.Width;
            DispForm4.Left = DispForm1.Width + DispForm2.Width + DispForm3.Width;
            DispForm5.Left = DispForm1.Width + DispForm2.Width + DispForm3.Width + DispForm4.Width;
            DispForm6.Left = 0;
            DispForm7.Left = DispForm6.Width;
            DispForm8.Left = DispForm6.Width + DispForm7.Width;

            ListForm.Left = DispForm1.Width + DispForm2.Width + DispForm3.Width + DispForm4.Width + DispForm5.Width;
        }

        public static System.Windows.Forms.MdiClient
        GetMdiClient(System.Windows.Forms.Form f)
        {
            foreach (System.Windows.Forms.Control c in f.Controls)
                if (c is System.Windows.Forms.MdiClient)
                    return (System.Windows.Forms.MdiClient)c;
            return null;
        }

        private void Btn_RunStimChange_Click_1(object sender, EventArgs e)
        {
            bool BlnFirstRun;

            if (BlnRunProtocolOn == true)
            {
            }
            else
            {
                SimFileName = "SimData";
            }


            T_Duration = Double.Parse(Txt_TotalDuration.Text) * 1000;
            double tempTimeSec = T_Duration / 1000; //in sec
            Txt_SimeTimeMax.Text = tempTimeSec.ToString();
            Txt_LngDispTimeMax.Text = T_Duration.ToString();

            BlnFirstRun = true;

            ExternalLoad = Double.Parse(txt_ExternalLoad.Text);
            ExternalLoad2 = Double.Parse(txtBox_EL_TestStim.Text);

            Cell.Contraction.EL0 = ExternalLoad;
            Cell.Contraction.EL = ExternalLoad;
            Cell.Contraction.ELinf = ExternalLoad2;
            Cell.Contraction.EL02 = ExternalLoad2;

            stimIntV0 = Double.Parse(Txt_OriginalCycle.Text);
            stimIntV = stimIntV0;
            stimIntVinf = Double.Parse(Txt_TestCycle.Text);
            stimIntV02 = stimIntVinf;

            BlnSProtocolOn = true;// Stimulation Change Protocol

            ResetToolStripMenuItem.Enabled = false;
            StartToolStripMenuItem.Enabled = false;
            StopToolStripMenuItem.Enabled = true;
            FileToolStripMenuItem.Enabled = false;

            //****************************************************
            SimCells_StimProt();
            //****************************************************

            BlnFirstRun = false;

            BlnSProtocolOn = false;// Stimulation Change Protocol

            ResetToolStripMenuItem.Enabled = true;
            StartToolStripMenuItem.Enabled = true;
            StopToolStripMenuItem.Enabled = false;
            FileToolStripMenuItem.Enabled = true;
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1.ShowDialog();
        }

    }
}

