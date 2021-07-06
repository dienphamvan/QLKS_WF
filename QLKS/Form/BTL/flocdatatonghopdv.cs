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
    public partial class flocdatatonghopdv : Form
    {
        public flocdatatonghopdv()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dtrpt = new DataTable();
        DataTable dt = new DataTable();
        string sql, connstr;

        private void flocdatatonghopdv_Load(object sender, EventArgs e)
        {
            connstr = Bientoancuc.TCconnstr;
            conn.ConnectionString = connstr;
            conn.Open();
            dtpdau.MinDate = Bientoancuc.ngaythanhlap;
            dtpketthuc.MaxDate = DateTime.Now;
        }

        private void btnloc_Click(object sender, EventArgs e)
        {
            string ngaybatdausql = dtpdau.Value.ToString("yyyy-MM-dd");
            string ngaybatdau = dtpdau.Value.ToString("dd-MM-yyyy");
            string ngayketthucsql = dtpketthuc.Value.ToString("yyyy-MM-dd");
            string ngayketthuc = dtpketthuc.Value.ToString("dd-MM-yyyy");

            if (DateTime.Compare(dtpketthuc.Value, dtpdau.Value) != -1)
            {
                sql = "SELECT dichvu.tendv, giadv,SUM(soluong) AS soluong, SUM(soluong * giadv) AS tongtien, " +
                    "'"+ngaybatdau+"' AS 'ngaybatdau', '"+ngayketthuc+"' AS 'ngayketthuc' " +
                    "FROM dbo.dichvu,dbo.hoadondv " +
                    "WHERE dichvu.tendv = hoadondv.tendv " +
                    "AND '"+ngaybatdausql+"' <= ngaygoi AND ngaygoi <= '"+ngayketthucsql+"'  " +
                    " GROUP BY dichvu.tendv, giadv";
                da = new SqlDataAdapter(sql, conn);
                dtrpt.Clear();
                da.Fill(dtrpt);

                rpttonghopdichvu rpt = new rpttonghopdichvu();
                rpt.SetDataSource(dtrpt);
                rpt.DataDefinition.FormulaFields["Tenks"].Text = "'" + Bientoancuc.tenks + "'";
                rpt.DataDefinition.FormulaFields["Diachi"].Text = "'" + Bientoancuc.diachi + "'";
                rpt.DataDefinition.FormulaFields["sdt"].Text = "'" + Bientoancuc.sdt + "'";
                rpt.DataDefinition.FormulaFields["logofile"].Text = "'" + Bientoancuc.logofile + "'";
                rpttonghopdichvuPrv f = new rpttonghopdichvuPrv(rpt);
                f.Show();
            }
            else
                MessageBox.Show("Cần chọn ngày bắt đầu trước ngày kết thúc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
