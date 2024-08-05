using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HumanVentricularCell
{
    public partial class ListForm : Form
    {
        MainForm Mf;
        public ListForm(MainForm f)
        {
            Mf = f;
            InitializeComponent();
        }

        public void DGViews_Initialize(ucListView tpList, int RowNum)
        {
            if (tpList.ISDataGridViewInitialized == false)
            {
                if (RowNum != 1)
                {
                    tpList.DataGridView1.Rows.AddCopies(0, RowNum - 1);
                    tpList.ISDataGridViewInitialized = true;
                }
                else
                {
                    tpList.ISDataGridViewInitialized = true;
                }
            }
        }

        private void ListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
        }               
    }
}
