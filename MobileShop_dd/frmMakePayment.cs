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
    public partial class frmMakePayment : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-OTDT5VK;Initial Catalog=MobileShopDB;Trusted_connection=True;");
        public frmMakePayment()
        {
            InitializeComponent();
        }

        private void frmMakePayment_Load(object sender, EventArgs e)
        {
            LoadCombo();
            LoadGridData();
            LoadBillGridData();
        }
        private void LoadBillGridData()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT m.model_Name,b.Customer_Name,b.Sold_Date,b.Quantity,e.Employee_Name,b.Product_Price,b.Quantity*b.Product_Price As Total FROM Bill b INNER JOIN Mobile m ON m.brand_ID=b.Bill_Id INNER JOIN Employee e ON b.Bill_Id=e.Employee_ID", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
        }
        private void LoadCombo()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from Employee", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            cmbEmployee.DataSource = dt;
            cmbEmployee.DisplayMember = "Employee_Name";
            cmbEmployee.ValueMember = "Employee_ID";
            con.Close();
        }

        private void LoadGridData()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT m.mobile_ID as Id,b.Brand, m.model_Name as Model,m.mobile_Price as Price FROM Mobile m INNER JOIN BrandName B ON b.Id=m.brand_ID", con);
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
                SqlCommand cmd = new SqlCommand("SELECT m.mobile_ID as Id,b.Brand, m.model_Name as Model,m.mobile_Price as Price FROM Mobile m INNER JOIN BrandName B ON b.Id=m.brand_ID where mobile_ID=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtProduct.Text = dr.GetInt32(0).ToString();
                    txtPrice.Text = dr.GetDecimal(3).ToString("0.00");
                }
                con.Close();
            }
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"INSERT INTO Bill VALUES(@pname,@cname,@contact,@price,@date,@quantity,@soldBy)", con);
            cmd.Parameters.AddWithValue("@pname", txtProduct.Text); /*--Problem*/
            cmd.Parameters.AddWithValue("@cname", txtC_Name.Text);
            cmd.Parameters.AddWithValue("@contact", txtC_Contact.Text);
            cmd.Parameters.AddWithValue("@price", txtPrice.Text);
            cmd.Parameters.AddWithValue("@date", dateTimePicker1.Value.Date);
            cmd.Parameters.AddWithValue("@quantity", txtQuantity.Text);
            cmd.Parameters.AddWithValue("@soldBy", cmbEmployee.SelectedValue);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data inserted successfully!!!");
            con.Close();
            LoadBillGridData();
        }
    }
}
