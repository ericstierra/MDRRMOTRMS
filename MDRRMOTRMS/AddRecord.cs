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
    public partial class AddRecord : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\SystemsDB\trms_db.accdb");
        //DataTableReader dt;
        public AddRecord()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = new OleDbCommand
                (
                    "Insert into tbl_trms (Firstname,Lastname,Birthday,ContactNum,Barangay,Category,TrainingAcquired,YearAcquired,TrainingVenue)values('" + txtFirstname.Text + "','" + txtLastname.Text + "','" + dateTimePicker1.Text + "','" + txtContactNum.Text + "','" + cmbBarangay.Text + "','" + cmbCategory.Text + "','" + cmbTrainings.Text + "','" + txtYear.Text + "','" + txtVenue.Text + "')", con
                );

            cmd.ExecuteNonQuery();
            this.Controls.Clear();
            this.InitializeComponent();
            MessageBox.Show("Data Successfully Recorded!");
            con.Close();
            
        }

        private void dataview()
        {
            con.Open();
            OleDbDataAdapter adapt = new OleDbDataAdapter
                (
                    "Select * from tbl_trms ORDER by [YearAcquired] desc", con
                );
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dgView.DataSource = dt;
            con.Close();
        }


        private void AddRecord_Load(object sender, EventArgs e)
        {
            dataview();
            //dgView.Columns["ID"].Visible = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = new OleDbCommand
                (
                    "Update tbl_trms set Firstname='"+txtFirstname.Text+ "',Lastname='" + txtLastname.Text + "',Birthday='" + dateTimePicker1.Text + "',ContactNum='" + txtContactNum.Text + "',Barangay='" + cmbBarangay.Text + "',Category='" + cmbCategory.Text + "',TrainingAcquired='" + cmbTrainings.Text + "',YearAcquired='" + txtYear.Text + "',TrainingVenue='" + txtVenue.Text + "', where ID=" + txtSearch.Text + " ", con
                );
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Data Updated Successfully!");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtSearch.Text))
            {
                this.Controls.Clear();
                this.InitializeComponent();
            }

            else
            {
                DataTable dt = new DataTable();
                con.Open();
                OleDbDataReader dr = null;
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM tbl_trms WHERE [ID] = " + txtSearch.Text + " ", con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    txtFirstname.Text = dr["Firstname"].ToString();
                    txtLastname.Text = dr["Lastname"].ToString();
                    dateTimePicker1.Text = dr["Birthday"].ToString();
                    txtContactNum.Text = dr["ContactNum"].ToString();
                    cmbBarangay.Text = dr["Barangay"].ToString();
                    cmbCategory.Text = dr["Category"].ToString();
                    cmbTrainings.Text = dr["TrainingAcquired"].ToString();
                    txtYear.Text = dr["YearAcquired"].ToString();
                    txtVenue.Text = dr["TrainingVenue"].ToString();
                }
            }

            con.Close();
        }
    }
}
