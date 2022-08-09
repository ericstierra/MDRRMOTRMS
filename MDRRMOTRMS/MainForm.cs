namespace MDRRMOTRMS
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void secretLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddRecord frm2 = new AddRecord();
            frm2.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            txtUsername.Text = String.Empty;
            txtPass.Text = String.Empty;
            MessageBox.Show("Login Details are incorrect. Please try again!");
            ClearTextBox();
        }

        private void ClearTextBox()
        {
            txtPass.Text = String.Empty;
            txtUsername.Text = String.Empty;
                    }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void secretLogin_Click_1(object sender, EventArgs e)
        {
            
        }

        private void secreLoginx_Click(object sender, EventArgs e)
        {
            //this.Hide();
            AddRecord frm2 = new AddRecord();
            frm2.Show();
        }
    }
}