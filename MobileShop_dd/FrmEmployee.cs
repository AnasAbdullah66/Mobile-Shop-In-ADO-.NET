using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobileShop_dd
{
    public partial class FrmEmployee : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-OTDT5VK;Initial Catalog=MobileShopDB;Trusted_connection=True;");
        public FrmEmployee()
        {
            InitializeComponent();
        }

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            // Form Employee Load
            LoadCombo();
            LoadGridData();
        }

        private void LoadCombo()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Post", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            cmbPost.DataSource = dt;
            cmbPost.DisplayMember = "Post_Name";
            cmbPost.ValueMember = "Post_Id";
            con.Close();
        }

        private void LoadGridData()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT e.Employee_Name, e.Join_Date,p.Post_Name,e.Basic_Salary,e.Employee_Picture FROM Employee e INNER JOIN Post p ON p.Post_Id=e.Employee_ID", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }

        private void btnPhoto_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                this.pictureBox1.Image = img;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"INSERT INTO Employee VALUES(@n,@j,@d,@s,@p)", con);
            cmd.Parameters.AddWithValue("@n", txtName.Text);
            cmd.Parameters.AddWithValue("@j", dateTimePicker1.Value.Date);
            cmd.Parameters.AddWithValue("@d", cmbPost.SelectedValue);
            cmd.Parameters.AddWithValue("@s", txtSalary.Text);
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            cmd.Parameters.AddWithValue("@p", ms.ToArray());
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data inserted successfully!!!");
            LoadGridData();
            con.Close();
        }
    }
}
