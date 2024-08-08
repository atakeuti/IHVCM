using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVentricularCell
{
    public class cElement
    {
        //protected Pd.TIdx IxDensity;
        protected Pd.TIdx IxItc;
        protected Pd.TIdx IxSF;
        //protected Pd.TIdx IxFlux;

        //public double Density;
        public double SF;	//'Scaling factor
        public double Itc;      // total current
        //public double Flux;
        protected int NumOfIx;
        public Pd.TIons Ic;
        public Pd.TIons Fx;
        //protected Pd.TIons P; ////relative permeability
        protected ucListView ListTemp;

        public cElement()  //Constructor 
        {
            SF = 1.0;
        }

        virtual public void Initialize(ref double[] myTVc, ref cCell myCell, ref ListForm Lf)
        {
        }

        virtual public void dydt(double dt, ref double[] tvDYdt, ref double[] tvY, cCell myCell)
        {
        }

        virtual public void DispValues(ref ListForm Lf, ref double[] myTVc)
        {
        }

        virtual public void ModiValues(ref ListForm Lf, ref double[] myTVc)
        {
        }

        virtual public void SetIx(ref Pd.TIdx Ix, ref int i, int IntType, string StrName)
        {
            Ix.n = i;
            Ix.TType = IntType;
            Ix.Name = StrName;
            i = i + 1;
        } 
    }
}
