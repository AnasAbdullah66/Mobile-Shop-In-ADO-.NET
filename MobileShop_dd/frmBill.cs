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
    public partial class frmBill : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-OTDT5VK;Initial Catalog=MobileShopDB;Trusted_connection=True;");
        public frmBill()
        {
            InitializeComponent();
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

        private void btnBuy_Click(object sender, EventArgs e)
        {
            frmMakePayment mp = new frmMakePayment();
            mp.Show();
        }

        private void frmBill_Load(object sender, EventArgs e)
        {
            LoadGridData();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                int id = (int)this.dataGridView1.SelectedRows[0].Cells[0].Value;
                SqlCommand cmd = new SqlCommand("SELECT m.mobile_ID as Id,b.Brand, m.model_Name as Model,m.mobile_Price as Price,m.mobile_Picture as Picture,m.camera as Camera,m.ram as Ram, m.rom as Rom FROM Mobile m INNER JOIN BrandName B ON b.Id=m.brand_ID where mobile_ID=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lblBrand.Text = dr.GetString(1);
                    lblModel.Text = dr.GetString(2);
                    lblPrice.Text = dr.GetDecimal(3).ToString("0.00");
                    MemoryStream ms = new MemoryStream((byte[])dr[4]);
                    Image img = Image.FromStream(ms);
                    PictureBox.Image = img;
                    lblCamera.Text = dr.GetInt32(5).ToString();
                    lblRam.Text = dr.GetInt32(6).ToString();
                    lblRom.Text = dr.GetInt32(7).ToString();
                }
                con.Close();
            }
        }
    }
}
