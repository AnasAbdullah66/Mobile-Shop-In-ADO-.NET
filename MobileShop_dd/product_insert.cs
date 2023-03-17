using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace MobileShop_dd
{
    public partial class product_insert : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-OTDT5VK;Initial Catalog=MobileShopDB;Trusted_connection=True;");
        public product_insert()
        {
            InitializeComponent();
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartPage sp = new StartPage();
            sp.Show();
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Brand_Insert bi = new Brand_Insert();
            bi.Show();
        }

        private void insertProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            product_insert pi = new product_insert();
            pi.Show();
            //pi.MdiParent = this;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"INSERT INTO Mobile VALUES (@bi,@mn,@mp,@p,@c,@ra,@ro)",con);
            cmd.Parameters.AddWithValue("@bi", cmbBrand.SelectedValue);
            cmd.Parameters.AddWithValue("@mn", txtModel.Text);
            cmd.Parameters.AddWithValue("@mp", txtPrice.Text);
            MemoryStream ms = new MemoryStream();
            PictureBox.Image.Save(ms, PictureBox.Image.RawFormat);
            cmd.Parameters.AddWithValue("@p", ms.ToArray());
            cmd.Parameters.AddWithValue("@c", txtCamera.Text);
            cmd.Parameters.AddWithValue("@ra", txtRam.Text);
            cmd.Parameters.AddWithValue("@ro", txtRom.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("New Product Information Added!!!");
            con.Close();
            LoadGridData();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                this.PictureBox.Image = img;
            }
        }

        private void product_insert_Load(object sender, EventArgs e)
        {
            // FORM LOAD
            LoadGridData();
            LoadCombo();
            
        }

        private void LoadCombo()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM BrandName", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            cmbBrand.DataSource = dt;
            cmbBrand.DisplayMember = "Brand";
            cmbBrand.ValueMember = "Id";
            con.Close();
        }

        private void LoadGridData()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT m.mobile_ID as Id,b.Brand, m.model_Name as Model,m.mobile_Price as Price,m.mobile_Picture as Picture,m.camera as Camera,m.ram as Ram, m.rom as Rom FROM Mobile m INNER JOIN BrandName B ON b.Id=m.brand_ID", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEmployee ed = new FrmEmployee();
            ed.Show();
        }
    }
}
