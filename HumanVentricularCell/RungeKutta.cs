using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    static public class RungeKutta
    {
        static private int NOPRungeKutta_ = Pd.NOPRungeKutta;
        static private double[] tvYtemp = new double[NOPRungeKutta_ + 1];     //temporaly value of time variables    IdxInitValOneRKC or IdxInitValRK
        static private double[] tvDydt1 = new double[NOPRungeKutta_ + 1];     //1st value of dydt                    IdxDyDt ( * IdxTimeInterval = IdxTempDy1 )
        static private double[] tvDydt2 = new double[NOPRungeKutta_ + 1];     //2nd value of dydt                    IdxDyDt ( * IdxTimeInterval = IdxTempDy2 )
        static private double[] tvDydt3 = new double[NOPRungeKutta_ + 1];     //

        private static bool rk4(ref double dt, ref double[] tvY, cCell myCell)
        {
            //' -------------------------------------------------------------------------------
            //'        Runge(-Kutta)
            //'        from() 'Numerical recipes in C'
            //'  dt            time interval
            //'  tvY           time variables
            //'-------------------------------------------------------------------------------}
            //'*************************Runge Kutta***************************************************

            int[] a = new int[5];
            int i;
            double dth;

            dth = dt / 2.0;
            //''****** Cycle No.1...of  RungeKutta...............
            myCell.dydt(dt, ref tvDydt1, ref tvY, myCell);

            for (i = 0; i <= NOPRungeKutta_; i++)
            {
                tvYtemp[i] = tvY[i] + dth * tvDydt1[i];
            };

            //****** Cycle No.2...of  RungeKutta...............
            myCell.dydt(dt, ref tvDydt2, ref tvYtemp, myCell);
            for (i = 0; i <= NOPRungeKutta_; i++)
            {
                tvYtemp[i] = tvY[i] + dth * tvDydt2[i];
            };

            //****** Cycle No.3...of  RungeKutta...............
            myCell.dydt(dt, ref tvDydt3, ref tvYtemp, myCell);
            for (i = 0; i <= NOPRungeKutta_; i++)
            {
                tvYtemp[i] = tvY[i] + dt * tvDydt3[i];
                tvDydt3[i] = tvDydt3[i] + tvDydt2[i];
            };
            //****** Cycle No.4...of  RungeKutta...............
            myCell.dydt(dt, ref tvDydt2, ref tvYtemp, myCell);

            for (i = 0; i <= NOPRungeKutta_; i++)
            {
                tvYtemp[i] = tvY[i] + (tvDydt1[i] + 2 * tvDydt3[i] + tvDydt2[i]) * dt / 6;
            };

            for (i = 0; i <= NOPRungeKutta_; i++)
            {
                tvY[i] = tvYtemp[i];   //enter result values into tvY 
            };

            // myCell.dVdTVal = tvDydt1[Pd.IdxVm] / dt;


            return true;
        }

        public static void rkqc(ref double dt, ref double[] tvY, cCell myCell)
        {
            //'-------------------------------------------------------------------------------
            //'  adaptive stepsize control
            //'    fit maximum dy(%) within the limit.
            //
            //'  dt            time interval
            //'  tvY           time variables
            //'-------------------------------------------------------------------------------}
            int NOPRungeKutta_ = Pd.NOPRungeKutta;

            double MaxTh = 0.1;				//maximum Threshold of dy
            double MinTh = 0.01 * 2.0;  	//minimum Threshold of dy
            double Maxdt = 0.005;           //maximum dt
            double Mindt = 0.000001;        //minimum dt

            int i;
            double dY = 0.0;				//(%)delta Y of each variable
            double maxdY;					//(%)maximum delta Y

            double[] tvSave = new double[NOPRungeKutta_ + 1];
            //store the original value
            for (i = 0; i <= NOPRungeKutta_; i++)
            {
                tvSave[i] = tvY[i];
            };

            do
            {
                //restore the original value
                for (i = 0; i <= NOPRungeKutta_; i++)
                {
                    tvY[i] = tvSave[i];
                };
                //calculate Runge-Kutta

                if (rk4(ref dt, ref tvY, myCell) == true)
                {
                    //when Runge-Kutta successfully finished, Check relative amplitude of change
                    maxdY = 0;
                    for (i = 0; i <= NOPRungeKutta_; i++)
                    {
                        if (0.0000000001 < tvY[i]) dY = Math.Abs((tvY[i] - tvSave[i]) / tvY[i]); //if variable is larger than 0 then calculate dydt
                        //leave maximum dydt
                        if (dY > maxdY) maxdY = dY;
                    };
                    if ((maxdY < MinTh) && (dt < Maxdt))
                    {
                        //when dy is too small and dt is slower than the upper limit, twice the dt and exit the loop.
                        dt = dt * 1.5; //  
                        break;
                    };

                    //when dy is reasonable or dt is too slow to decrease, exit the loop.
                    if ((maxdY <= MaxTh) || (dt < Mindt)) break;
                };
                //When dy is too large and dt is faster than the lower limit
                //make calculation slower
                dt = dt / 1.5; // 
            } while (true);			//make endless loop
        }
    }
}
