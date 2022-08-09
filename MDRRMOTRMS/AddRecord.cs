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
            if (String.IsNullOrEmpty(txtYear.Text))
            {
                MessageBox.Show("Please complete all the details required!");
            }
            else
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand
                    (
                        "Insert into tbl_trms (Firstname,Lastname,Sex,Birthday,ContactNum,Barangay,Category,TrainingAcquired,YearAcquired,TrainingVenue)values('" + txtFirstname.Text + "','" + txtLastname.Text + "','" + cmbSex.Text + "','" + dateTimePicker1.Text + "','" + txtContactNum.Text + "','" + cmbBarangay.Text + "','" + cmbCategory.Text + "','" + cmbTrainings.Text + "','" + txtYear.Text + "','" + txtVenue.Text + "')", con
                    );

                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Successfully Recorded!");
                con.Close();

                ClearTextBox();
                ReloadDataGrid();
            }
        }

        private void AddRecord_Load(object sender, EventArgs e)
        {

            Dashboard frm1 = new Dashboard() { TopLevel = false, TopMost = true };
            this.panelDgview.Controls.Clear();
            this.panelDgview.Controls.Add(frm1);
            frm1.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("ID is required to . Please try again!");
            }
            else
            {
                //Update Data
                con.Open();
                string query = "UPDATE tbl_trms SET Firstname = @firstname, Lastname = @lastname, Sex = @sex, Birthday = @bday, ContactNum = @contactnum, Barangay = @barangay, Category = @category, TrainingAcquired = @training, YearAcquired = @year, TrainingVenue = @venue WHERE [ID] = " + txtSearch.Text + " ";
                OleDbCommand cmd = new OleDbCommand(query, con);

                cmd.Parameters.AddWithValue("@firstname", txtFirstname.Text);
                cmd.Parameters.AddWithValue("@lastname", txtLastname.Text);
                cmd.Parameters.AddWithValue("@sex", cmbSex.Text);
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

                DataTableView frm1 = new DataTableView() { TopLevel = false, TopMost = true };
                this.panelDgview.Controls.Clear();
                this.panelDgview.Controls.Add(frm1);
                frm1.Show();

                ReloadDataGrid();
                ClearTextBox();
            }
        }

        private void ReloadDataGrid()
        {
            DataTableView frm1 = new DataTableView() { TopLevel = false, TopMost = true };
            this.panelDgview.Controls.Clear();
            this.panelDgview.Controls.Add(frm1);
            frm1.Show();
        }
        private void ReloadDashboard()
        {
            Dashboard frm1 = new Dashboard() { TopLevel = false, TopMost = true };
            this.panelDgview.Controls.Clear();
            this.panelDgview.Controls.Add(frm1);
            frm1.Show();
        }
        private void ClearTextBox()
        {
            txtFirstname.Text = String.Empty;
            txtLastname.Text = String.Empty;
            cmbSex.Text = String.Empty;
            dateTimePicker1.Text = String.Empty;
            txtContactNum.Text = String.Empty;
            cmbBarangay.Text = String.Empty;
            cmbCategory.Text = String.Empty;
            cmbTrainings.Text = String.Empty;
            txtYear.Text = String.Empty;
            txtVenue.Text = String.Empty;
            txtSearch.Text = String.Empty;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtSearch.Text))
            {
                ClearTextBox();
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
                    cmbSex.Text = dr["Sex"].ToString();
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


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Search box is empty. Please try again!");
            }
            else
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

                ReloadDataGrid();
                ClearTextBox();
            }
        }

        private void dgView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearTextBox();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            //System.Windows.Forms.Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panelDgview_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ReloadDashboard();
        }

        private void btnViewRec_Click(object sender, EventArgs e)
        {
            ReloadDataGrid();
        }
    }
}
