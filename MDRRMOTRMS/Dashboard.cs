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
    public partial class Dashboard : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\SystemsDB\trms_db.accdb");
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand blssfa = new OleDbCommand("SELECT COUNT(*) from tbl_trms WHERE TrainingAcquired = 'Basic Life Support and Standard First-Aid' ", con);
            OleDbCommand cbdrrm = new OleDbCommand("SELECT count(*) from tbl_trms WHERE TrainingAcquired = 'Community-Based Disaster Risk Reduction and Management (CBDRRM)'", con);
            int totalBLSSFA = (int)blssfa.ExecuteScalar();
            int totalCBDRRM = (int)cbdrrm.ExecuteScalar();
            con.Close();

            dataBLS.Text = totalBLSSFA.ToString();
            dataCBDRRM.Text = totalCBDRRM.ToString();
        }
    }
}
