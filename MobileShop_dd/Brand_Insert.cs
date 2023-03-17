using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobileShop_dd
{
    public partial class Brand_Insert : Form
    {
        public Brand_Insert()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-OTDT5VK;Initial Catalog=MobileShopDB;Trusted_connection=True;");
            SqlCommand cmd = new SqlCommand("INSERT INTO BrandName VALUES ('" + txtBrandName.Text + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("New Brand Name Added!!!");
            loadGrid();
            con.Close();
        }

        private void viewProductToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Brand_Insert_Load(object sender, EventArgs e)
        {
            // LOAD FORM
            loadGrid();

        }

        private void loadGrid()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-OTDT5VK;Initial Catalog=MobileShopDB;Trusted_connection=True;");

            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM BrandName", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
    }
}
