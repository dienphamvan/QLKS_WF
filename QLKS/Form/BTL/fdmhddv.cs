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
    public partial class fdmhddv : Form
    {
        public fdmhddv()
        {
            InitializeComponent();
        }



        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd;
        DataTable dt = new DataTable();
        DataTable dtrpt = new DataTable();
        string sql, connstr;
        int i;

        public fdmhddv(string idhdpstr) : this()
        {
            //nhanstr = idhdpstr;
            txtidhdp.Text = idhdpstr;
        }

        private void fdmhddv_Load(object sender, EventArgs e)
        {
            connstr = Bientoancuc.TCconnstr;
            conn.ConnectionString = connstr;
            if (txtidhdp.Text != "")
                sql = "SELECT hoadondv.idhdp, hoten, idphong, dbo.dichvu.tendv, soluong, giadv, (soluong * giadv) AS thanhtien, ngaygoi FROM dbo.hoadonphong,dbo.hoadondv,dbo.khachhang, dbo.dichvu   " +
                      " where hoadondv.idhdp = '" + txtidhdp.Text+ "' and hoadondv.idhdp = hoadonphong.idhdp " +
                      "AND hoadonphong.idkh = khachhang.idkh AND dichvu.tendv = hoadondv.tendv ";
            else
                sql = "SELECT hoadondv.idhdp, hoten, idphong, dbo.dichvu.tendv, soluong, giadv, (soluong * giadv) AS thanhtien, ngaygoi FROM dbo.hoadonphong,dbo.hoadondv,dbo.khachhang, dbo.dichvu   " +
                    "WHERE hoadondv.idhdp = hoadonphong.idhdp  AND hoadonphong.idkh = khachhang.idkh AND dichvu.tendv = hoadondv.tendv ";
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
            grdhoadon.DataSource = dt;
            if (dt.Rows.Count >= 1)
                NapCT();
        }

        private void btnloc_Click(object sender, EventArgs e)
        {
            sql = "SELECT hoadondv.idhdp, hoten, idphong, tendv, soluong, ngaygoi FROM dbo.hoadonphong,dbo.hoadondv,dbo.khachhang " +
                  "WHERE hoadondv.idhdp = hoadonphong.idhdp  AND hoadonphong.idkh = khachhang.idkh  " +
                  "AND "+cmbtentruong.Text+" LIKE N'%"+txtgiatriloc.Text+"%' ";
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
            grdhoadon.DataSource = dt;
            grdhoadon.Refresh();
        }

        private void grdhoadon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
        }

        private void btnketthuc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn chắc chắn muốn xóa hóa đơn này ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error,MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                sql = "DELETE FROM dbo.hoadondv WHERE idhddv = '" + txtidhddv.Text + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                i = grdhoadon.CurrentRow.Index;
                grdhoadon.Rows.RemoveAt(i);
            }
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            flocdatatonghopdv f = new flocdatatonghopdv();
            f.ShowDialog();
        }

        private void fdmhddv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

      

        /*private void btnInDichvu_Click(object sender, EventArgs e)
        {
            if (cmbtentruong.Text == "tendv")
            {
                sql= "SELECT hoten, cmnd, idphong, soluong, (soluong * tiendichvu) AS tongien FROM dbo.khachhang, dbo.hoadondv, dbo.hoadonphong " 
       + " WHERE hoadonphong.idkh = khachhang.idkh AND hoadondv.idhdp = hoadonphong.idhdp AND tendv = N'" + txtgiatriloc.Text + "' ";
               
                da = new SqlDataAdapter(sql, conn);
                dtrpt.Clear();
                da.Fill(dtrpt);

                rptdichvu rpt = new rptdichvu();
                rpt.SetDataSource(dtrpt);
                rpt.DataDefinition.FormulaFields["tendv"].Text = "'" + txtgiatriloc.Text + "'";
                rptdichvuPrv f = new rptdichvuPrv(rpt);
                f.Show();

            }
            else
                MessageBox.Show("Ban can thuc hien loc hang hoa theo nhom truoc khi bao cao!", "Thong bao");
        }
        */

        public void NapCT()
        {
            i = grdhoadon.CurrentRow.Index;
            txtidhddv.Text = grdhoadon[0, i].Value.ToString();
            txtidhdp.Text = grdhoadon[1, i].Value.ToString();
            txttendv.Text = grdhoadon[2, i].Value.ToString();
            txtsl.Text = grdhoadon[4, i].Value.ToString();
        }
    }
}
