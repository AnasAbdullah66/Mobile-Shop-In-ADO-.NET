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
    public partial class frmEmployeeEdit : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-OTDT5VK;Initial Catalog=MobileShopDB;Trusted_connection=True;");
        public frmEmployeeEdit()
        {
            InitializeComponent();
        }

        private void frmEmployeeEdit_Load(object sender, EventArgs e)
        {
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
            SqlDataAdapter sda = new SqlDataAdapter("SELECT e.Employee_ID, e.Employee_Name, e.Join_Date,p.Post_Name,e.Basic_Salary,e.Employee_Picture FROM Employee e INNER JOIN Post p ON p.Post_Id=e.Employee_ID", con);
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                SqlCommand cmd = new SqlCommand("SELECT e.Employee_Name, e.Join_Date,e.Employee_Post,e.Basic_Salary,e.Employee_Picture FROM Employee e INNER JOIN Post p ON p.Post_Id=e.Employee_ID where Employee_ID=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtId.Text = id.ToString();
                    txtName.Text = dr.GetString(0);
                    dateTimePicker1.Value = dr.GetDateTime(1).Date;
                    cmbPost.SelectedValue = dr.GetInt32(2);
                    txtSalary.Text = dr.GetDecimal(3).ToString("0.00");
                    MemoryStream ms = new MemoryStream((byte[])dr[4]);
                    Image img = Image.FromStream(ms);
                    pictureBox1.Image = img;
                }
                con.Close();
            }
        }
    }
}
