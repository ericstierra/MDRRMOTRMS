using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace MDRRMOTRMS
{
    public partial class DataTableView : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\SystemsDB\trms_db.accdb");
        public DataTableView()
        {
            InitializeComponent();
        }

        private void dataview()
        {
            con.Open();
            OleDbDataAdapter adapt = new OleDbDataAdapter
                (
                    "Select * from tbl_trms ORDER by YearAcquired desc", con
                );
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dgView.DataSource = dt;
            con.Close();
        }

        private void DataTableView_Load(object sender, EventArgs e)
        {
            dataview();
        }
    }
}
