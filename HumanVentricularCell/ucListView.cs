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
    public partial class ucListView : UserControl
    {
        public int n_Item;
        public bool ISDataGridViewInitialized;
        
        public ucListView()
        {
            InitializeComponent();
            DataGridView1.AutoGenerateColumns = false;
            n_Item = 0;
        }

        public void LVDispValue(String strComponent, Pd.TIdx Idx, ref double ItVal)
        {

            if (Idx.TType == Pd.IntPar)
            {
                DataGridView1[1, Idx.n].Value = "P";
            }
            else if (Idx.TType == Pd.IntVar)
            {
                DataGridView1[1, Idx.n].Value = "V";
            };

            DataGridView1[2, Idx.n].Value = Idx.Name;
            DataGridView1[3, Idx.n].Value = ItVal.ToString("0.00E0");


            if (Pd.BlnFirstLoad == true)
            {
                DataGridView1[4, Idx.n].Value = ItVal.ToString("0.00E0");   //' store initial value to colum 4
            };

            DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        public void LVModiValue(String strComponent, Pd.TIdx Idx, ref double ItVal)
        {
            int currendtIndx = DataGridView1.CurrentCellAddress.Y;

            if (DataGridView1[0, Idx.n].Value == null)
            {
            }
            else if ((bool)DataGridView1[0, Idx.n].Value == true)
            {
                ItVal = Convert.ToDouble(DataGridView1[3, Idx.n].Value);
                DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
        }

        private void usListView_Load(object sender, EventArgs e)
        {
            Column0.Width = 30;
            Column1.Width = 30;
            Column2.Width = 90;// 70;
            Column3.Width = Width - (Column0.Width + Column1.Width + Column2.Width);

            Column1.ReadOnly = true;
            Column2.ReadOnly = true;
            Column3.ReadOnly = false;

            DataGridView1.Columns[0].Resizable = DataGridViewTriState.False;
            DataGridView1.Columns[1].Resizable = DataGridViewTriState.False;

            this.DataGridView1.DefaultCellStyle.Font = new Font("Calibli", 9);

        }
    }
}
