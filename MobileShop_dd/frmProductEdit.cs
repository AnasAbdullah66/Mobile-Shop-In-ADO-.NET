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
    public partial class frmProductEdit : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-OTDT5VK;Initial Catalog=MobileShopDB;Trusted_connection=True;");
        public frmProductEdit()
        {
            InitializeComponent();
        }

        private void frmProductEdit_Load(object sender, EventArgs e)
        {
            LoadGridData();
            LoadCombo();
        }
        private void LoadCombo()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM BrandName", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            cmbBrand.DisplayMember = "Brand";
            cmbBrand.ValueMember = "Id";
            cmbBrand.DataSource = dt;
            con.Close();
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                this.PictureBox.Image = img;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //delete
            con.Open();
            SqlCommand cmd = new SqlCommand(@"DELETE FROM Mobile WHERE mobile_ID=@i", con);
            cmd.Parameters.AddWithValue("@i", Convert.ToInt32(txtId.Text));
            cmd.ExecuteNonQuery();
            MessageBox.Show("Product Deleted!!!");
            con.Close();
            LoadGridData();
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                int id = (int)this.dataGridView1.SelectedRows[0].Cells[0].Value;
                SqlCommand cmd = new SqlCommand("SELECT m.mobile_ID as Id,b.Id, m.model_Name as Model,m.mobile_Price as Price,m.mobile_Picture as Picture,m.camera as Camera,m.ram as Ram, m.rom as Rom FROM Mobile m INNER JOIN BrandName B ON b.Id=m.brand_ID where mobile_ID=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtId.Text = id.ToString();
                    cmbBrand.SelectedValue = dr.GetInt32(1);
                    txtModel.Text = dr.GetString(2);
                    txtPrice.Text = dr.GetDecimal(3).ToString("0.00");
                    MemoryStream ms = new MemoryStream((byte[])dr[4]);
                    Image img = Image.FromStream(ms);
                    PictureBox.Image = img;
                    txtCamera.Text = dr.GetInt32(5).ToString();
                    txtRam.Text = dr.GetInt32(6).ToString();
                    txtRom.Text = dr.GetInt32(7).ToString();
                }
                con.Close();
            }
        }

        private void btnUpload_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                this.PictureBox.Image = img;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //update
            con.Open();
            SqlCommand cmd = new SqlCommand(@"UPDATE Mobile SET brand_ID=@bi,model_Name=@mn,mobile_Price=@mp,mobile_Picture=@p,camera=@c,ram=@ra,rom=@ro WHERE mobile_ID=@i", con);
            cmd.Parameters.AddWithValue("@i", Convert.ToInt32(txtId.Text));
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
    }
}
