using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobileShop_dd
{
    public partial class StartPage : Form
    {
        public StartPage()
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
            //Form1 ca = new Form1();
            //ca.Show();
        }

        private void insertProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            product_insert pi = new product_insert();
            pi.Show();
            
        }

        private void StartPage_Load(object sender, EventArgs e)
        {

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEmployee ed = new FrmEmployee();
            ed.Show();
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMakePayment mp = new frmMakePayment();
            mp.Show();
        }

        private void viewProductToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmBill b = new frmBill();
            b.Show();
        }

        private void productReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptProduct pr = new rptProduct();
            pr.Show();
        }

        private void viewProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProductEdit pe = new frmProductEdit();
            pe.Show();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmployeeEdit el = new frmEmployeeEdit();
            el.Show();
        }

        private void billReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBillReport br = new frmBillReport();
            br.Show();
        }
    }
}
