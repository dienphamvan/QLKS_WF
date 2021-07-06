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

namespace BTL
{
    public partial class fdmhdphong : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd;
        DataTable dt = new DataTable();
        string sql, connstr;
        int i;
        public fdmhdphong()
        {
            InitializeComponent();
        }

        private void fdmhdphong_Load(object sender, EventArgs e)
        {
            connstr = Bientoancuc.TCconnstr;
            conn.ConnectionString = connstr;
            conn.Open();
            sql = "SELECT idhdp,hoten,cmnd, idphong, ngaycheckin,ngaycheckout,tongtien FROM dbo.hoadonphong, dbo.khachhang WHERE hoadonphong.idkh = khachhang.idkh";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            grdhoadon.DataSource = dt;
            NapCT();
        }
        /*
        private void btndau_Click(object sender, EventArgs e)
        {
            grdhoadon.CurrentCell = grdhoadon[0, 0];
        }

        private void btnxuong_Click(object sender, EventArgs e)
        {
            i = grdhoadon.CurrentRow.Index;
            if (i > 0)
                grdhoadon.CurrentCell = grdhoadon[0, i - 1];
        }

        private void btnlen_Click(object sender, EventArgs e)
        {
            i = grdhoadon.CurrentRow.Index;
            if (i < grdhoadon.RowCount - 1)
                grdhoadon.CurrentCell = grdhoadon[0, i+1];
        }

        private void btncuoi_Click(object sender, EventArgs e)
        {
            grdhoadon.CurrentCell = grdhoadon[0, grdhoadon.RowCount - 2];
        }
        */

        private void btnloc_Click(object sender, EventArgs e)
        {
            sql = "SELECT idhdp,hoten,cmnd, idphong, ngaycheckin,ngaycheckout,tongtien FROM dbo.hoadonphong, dbo.khachhang WHERE hoadonphong.idkh = khachhang.idkh " +
                  " and "+cmbtentruong.Text+" LIKE N'%"+txtgiatriloc.Text+"%'";
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
            grdhoadon.DataSource = dt;
            grdhoadon.Refresh();
        }

        private void btnxemchitiet_Click(object sender, EventArgs e)
        {
            fdmhddv f = new fdmhddv(txtidhdp.Text);
            f.ShowDialog();
        }

        private void btnchon_Click(object sender, EventArgs e)
        {
            NapCT();
        }

        public void NapCT()
        {
            i = grdhoadon.CurrentRow.Index;
            txtidhdp.Text = grdhoadon[0, i].Value.ToString();
            txthoten.Text = grdhoadon[1, i].Value.ToString();
            txtcmnd.Text = grdhoadon[2, i].Value.ToString();
            txtsophong.Text = grdhoadon[3, i].Value.ToString();
        }

        private void grdhoadon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            flocdatachitietphong f = new flocdatachitietphong();
            f.ShowDialog();
        }

        private void btnketthuc_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
