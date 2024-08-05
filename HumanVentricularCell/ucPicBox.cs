using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HumanVentricularCell
{
    public partial class ucPicBox : UserControl
    {
        public int NumOfMyPen = 20;
        private Graphics myG;
        public Pen myPen;
        private Graphics myG_bmp;
        private Bitmap bmpBuf;
        public Pd.TmyPenS[] myPoint;
        public Pd.TPBoxProperty PBox1P;
        private Pd.TPBoxProperty bmpBufP;
        public double TimeMax;

        Pen PenPlot = new Pen(Brushes.Black);

        public ucPicBox()
        {
            InitializeComponent();

            myPoint = new Pd.TmyPenS[NumOfMyPen];

            PBox1P = new Pd.TPBoxProperty();
            bmpBufP = new Pd.TPBoxProperty();
            bmpBuf = new Bitmap(PBox.Width, PBox.Height);
            PBox.Image = bmpBuf;
            myPen = new Pen(Brushes.Black);
            //myG_bmp = PBox.CreateGraphics();
            myG_bmp = Graphics.FromImage(bmpBuf);
            myG = PBox.CreateGraphics();

            for (int i = 0; i <= (NumOfMyPen - 1); i++)
            {
                myPoint[i] = new Pd.TmyPenS();
            }

            myPoint[0].MyColor = Color.Black;
            myPoint[1].MyColor = Color.Red;
            myPoint[2].MyColor = Color.Blue;
            myPoint[3].MyColor = Color.Green;
            myPoint[4].MyColor = Color.HotPink;
            myPoint[5].MyColor = Color.Orange;
            myPoint[6].MyColor = Color.Cyan;
            myPoint[7].MyColor = Color.GreenYellow;
            myPoint[8].MyColor = Color.DarkSalmon;
            myPoint[9].MyColor = Color.Orchid;
            myPoint[10].MyColor = Color.DarkGray;
            myPoint[11].MyColor = Color.OrangeRed;
            myPoint[12].MyColor = Color.Navy;
            myPoint[13].MyColor = Color.Goldenrod;
            myPoint[14].MyColor = Color.Gold;
            myPoint[15].MyColor = Color.OliveDrab;
            myPoint[16].MyColor = Color.Indigo;
            myPoint[17].MyColor = Color.DarkSlateBlue;
            myPoint[18].MyColor = Color.LightBlue;
            myPoint[19].MyColor = Color.Brown;
        }

        public int Roundoff(double dValue)
        {
            return (int)Math.Round(dValue, MidpointRounding.AwayFromZero);
        }

        public void EraseGraph()
        {
            double dx, dy;
            Point P0 = new Point();
            Point P1 = new Point();

            dy = (PBox1P.YMax - PBox1P.YMin) / 10.0;
            dx = (PBox1P.XMax - PBox1P.XMin) / 10.0;

            myG.Clear(PBox.BackColor);

            P0.X = 0;
            P0.Y = 0;
            P1.X = 0;
            P1.Y = PBox.Height;

            myG.DrawLine(Pens.LightGray, P0.X, P0.Y, P1.X, P1.Y);

            P0.X = 0;
            P0.Y = PBox.Height;
            P1.X = PBox.Width;
            P1.Y = PBox.Height;

            myG.DrawLine(Pens.LightGray, P0.X, P0.Y, P1.X, P1.Y);

            for (int i = 0; i < 10; i++)
            {
                P0.X = Roundoff(i * PBox1P.XCal * dx);
                P0.Y = 0;
                P1.X = Roundoff(i * PBox1P.XCal * dx);
                P1.Y = PBox.Height;

                myG.DrawLine(Pens.LightGray, P0.X, P0.Y, P1.X, P1.Y);
                P0.X = 0;
                P0.Y = Roundoff(i * PBox1P.YCal * dy);
                P1.X = PBox.Width;
                P1.Y = Roundoff(i * PBox1P.YCal * dy);

                myG.DrawLine(Pens.LightGray, P0.X, P0.Y, P1.X, P1.Y);
            }

            //Drawline Y=0
            P0.X = 0;
            P0.Y = Roundoff(PBox.Height + (PBox1P.YMin - 0.0) * PBox1P.YCal);
            P1.X = PBox.Width;
            P1.Y = Roundoff(PBox.Height + (PBox1P.YMin - 0.0) * PBox1P.YCal);
            myG.DrawLine(Pens.DarkGray, P0.X, P0.Y, P1.X, P1.Y);


            dy = (bmpBufP.YMax - bmpBufP.YMin) / 10.0;
            dx = (bmpBufP.XMax - bmpBufP.XMin) / 10.0;

            myG_bmp.Clear(PBox.BackColor);

            P0.X = 0;
            P0.Y = 0;
            P1.X = 0;
            P1.Y = PBox.Height;

            myG_bmp.DrawLine(Pens.LightGray, P0.X, P0.Y, P1.X, P1.Y);

            P0.X = 0;
            P0.Y = PBox.Height;
            P1.X = PBox.Width;
            P1.Y = PBox.Height;

            myG_bmp.DrawLine(Pens.LightGray, P0.X, P0.Y, P1.X, P1.Y);

            for (int i = 0; i < 10; i++)
            {
                P0.X = Roundoff(i * bmpBufP.XCal * dx);
                P0.Y = 0;
                P1.X = Roundoff(i * bmpBufP.XCal * dx);
                P1.Y = PBox.Height;

                myG_bmp.DrawLine(Pens.LightGray, P0.X, P0.Y, P1.X, P1.Y);
                P0.X = 0;
                P0.Y = Roundoff(i * bmpBufP.YCal * dy);
                P1.X = PBox.Width;
                P1.Y = Roundoff(i * bmpBufP.YCal * dy);

                myG_bmp.DrawLine(Pens.LightGray, P0.X, P0.Y, P1.X, P1.Y);
            }

            //Drawline Y=0
            P0.X = 0;
            P0.Y = Roundoff(PBox.Height + (PBox1P.YMin - 0.0) * PBox1P.YCal);
            P1.X = PBox.Width;
            P1.Y = Roundoff(PBox.Height + (PBox1P.YMin - 0.0) * PBox1P.YCal);
            myG_bmp.DrawLine(Pens.DarkGray, P0.X, P0.Y, P1.X, P1.Y);

            PBox.Image = bmpBuf;

        }

        public void GetNewSetting()
        {

            bmpBuf = new Bitmap(PBox.Width, PBox.Height);
            myG_bmp = Graphics.FromImage(bmpBuf);

            PBox1P.XMin = 0.0;

            PBox1P.XMax = double.Parse((lbl_Xmax.Text));    //100.0;
            PBox1P.X0 = 0.0;
            PBox1P.XCal = PBox.Width / (PBox1P.XMax - PBox1P.XMin);
            PBox1P.YMin = double.Parse((txb_Ymin.Text)); //0.0;
            PBox1P.YMax = double.Parse((txb_Ymax.Text)); //100.0;
            PBox1P.Y0 = 0.0;
            PBox1P.YCal = PBox.Height / (PBox1P.YMax - PBox1P.YMin);

            bmpBufP.XMin = double.Parse((lbl_Xmin.Text)); //0.0;
            bmpBufP.XMax = double.Parse((lbl_Xmax.Text));    //100.0;
            bmpBufP.X0 = 0.0;
            bmpBufP.XCal = bmpBuf.Width / (bmpBufP.XMax - bmpBufP.XMin);
            bmpBufP.YMin = double.Parse((txb_Ymin.Text)); //0.0;
            bmpBufP.YMax = double.Parse((txb_Ymax.Text)); //100.0;
            bmpBufP.Y0 = 0.0;
            bmpBufP.YCal = bmpBuf.Height / (bmpBufP.YMax - bmpBufP.YMin);
        }


        public void PlotSimData(double SimTime, double var1, ref Pd.TmyPenS myP, bool myFirst)
        {
            PenPlot.Color = myP.MyColor;

            if (myFirst == true)
            {
                myP.P0.X = Roundoff(SimTime * PBox1P.XCal);
                myP.P0.Y = Roundoff(PBox.Height + (PBox1P.YMin - var1) * PBox1P.YCal);
            }
            else
            {
                myP.P1.X = Roundoff(SimTime * PBox1P.XCal);
                myP.P1.Y = Roundoff(PBox.Height + (PBox1P.YMin - var1) * PBox1P.YCal);

                int Ylimit = 2147483647; //intの最大値　//  2 * bmpBuf.Height; //20211224
                if ((myP.P1.Y <= Ylimit) && (myP.P1.Y >= 0))
                {
                    myG_bmp.DrawLine(PenPlot, myP.P0.X, myP.P0.Y, myP.P1.X, myP.P1.Y);
                    myP.P0.X = myP.P1.X;
                    myP.P0.Y = myP.P1.Y;

                }

            };
        }

        public void DrawLine(double x1, ref Pd.TmyPenS myP)
        {
            PenPlot.Color = Color.Blue; //myP.MyColor;

            myP.P1.X = Roundoff(x1 * PBox1P.XCal);
            myP.P1.Y = Roundoff(0);

            myG_bmp.DrawLine(PenPlot, myP.P1.X, bmpBuf.Height, myP.P1.X, 0);

        }

        public void LoadBmpToPbox()
        {
            PBox.Image = bmpBuf;
            PBox.Refresh();
        }

        private void usPicBox_Layout(object sender, LayoutEventArgs e)
        {
            if ((PBox.Width != 0) && (PBox.Height != 0))
            {
                GetNewSetting();
                EraseGraph();
            }
        }

        private void ucPicBox_Load(object sender, EventArgs e)
        {
            lbl_Var1.ForeColor = myPoint[0].MyColor;
            lbl_Var2.ForeColor = myPoint[1].MyColor;
            lbl_Var3.ForeColor = myPoint[2].MyColor;
            lbl_Var4.ForeColor = myPoint[3].MyColor;
            lbl_Var1Str.ForeColor = myPoint[0].MyColor;
            lbl_Var2Str.ForeColor = myPoint[1].MyColor;
            lbl_Var3Str.ForeColor = myPoint[2].MyColor;
            lbl_Var4Str.ForeColor = myPoint[3].MyColor;

            lbl_Var1Str.Text = "";
            lbl_Var2Str.Text = "";
            lbl_Var3Str.Text = "";
            lbl_Var4Str.Text = "";
            lbl_Var1.Text = "";
            lbl_Var2.Text = "";
            lbl_Var3.Text = "";
            lbl_Var4.Text = "";
        }

    }
}
