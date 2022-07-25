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
            //Update Data
            con.Open();
            string query = "UPDATE tbl_trms SET Firstname = @firstname, Lastname = @lastname, Birthday = @bday, ContactNum = @contactnum, Barangay = @barangay, Category = @category, TrainingAcquired = @training, YearAcquired = @year, TrainingVenue = @venue WHERE [ID] = "+txtSearch.Text+" ";
            OleDbCommand cmd = new OleDbCommand(query, con);

            cmd.Parameters.AddWithValue("@firstname", txtFirstname.Text);
            cmd.Parameters.AddWithValue("@lastname", txtLastname.Text);
            cmd.Parameters.AddWithValue("@bday", dateTimePicker1.Value.ToString());
            cmd.Parameters.AddWithValue("@contactnum", txtContactNum.Text);
            cmd.Parameters.AddWithValue("@barangay", cmbBarangay.Text);
            cmd.Parameters.AddWithValue("@category", cmbCategory.Text);
            cmd.Parameters.AddWithValue("@training", cmbTrainings.Text);
            cmd.Parameters.AddWithValue("@year", txtYear.Text);
            cmd.Parameters.AddWithValue("@venue", txtVenue.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Data Updated Successfully!");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtSearch.Text))
            {
                txtFirstname.Text = String.Empty;
                txtLastname.Text = String.Empty;
                dateTimePicker1.Text = String.Empty;
                txtContactNum.Text = String.Empty;
                cmbBarangay.Text = String.Empty;
                cmbCategory.Text = String.Empty;
                cmbTrainings.Text = String.Empty;
                txtYear.Text = String.Empty;
                txtVenue.Text = String.Empty;
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

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtSearch2_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            DataTable dt = new DataTable();
            DataView dv = new DataView(dt);

            //Check if SearchBox is Empty or Not
            if (String.IsNullOrEmpty(txtSearch2.Text))
            {
                con.Close();
                dataview();
            }
            else
            {
                dv.RowFilter = string.Format("[ID] like '%{0}%'", txtSearch2.Text);
                dgView.DataSource = dv;
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Delete or Remove Data
            con.Open();
            OleDbCommand cmd = new OleDbCommand
                (
                    "Delete from tbl_trms where ID=" + txtSearch.Text + " ", con
                );
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Data Deleted Successfully!");
        }
    }
}
