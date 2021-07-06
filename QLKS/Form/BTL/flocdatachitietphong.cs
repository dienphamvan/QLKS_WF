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
    public partial class flocdatachitietphong : Form
    {
        public flocdatachitietphong()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dtrpt = new DataTable();
        DataTable dtrpt1 = new DataTable();
        DataTable dt = new DataTable();
        string sql, connstr;

        private void btnloc_Click(object sender, EventArgs e)
        {
            string ngaybatdausql = dtpdau.Value.ToString("yyyy-MM-dd");
            string ngaybatdau = dtpdau.Value.ToString("dd-MM-yyyy");
            string ngayketthucsql = dtpketthuc.Value.ToString("yyyy-MM-dd");
            string ngayketthuc = dtpketthuc.Value.ToString("dd-MM-yyyy");
            if (DateTime.Compare(dtpketthuc.Value, dtpdau.Value) != -1) 
            {
                sql = "SELECT idhdp, hoten, cmnd, hoadonphong.idphong, FORMAT(ngaycheckin, 'dd/MM/yyyy') AS ngayden, FORMAT(ngaycheckout, 'dd/MM/yyyy') AS ngaydi,  " +
                    "tongtien, loaiphong, sogiuong FROM dbo.hoadonphong, dbo.khachhang, dbo.phong " +
                    "WHERE hoadonphong.idkh = khachhang.idkh AND hoadonphong.idphong = phong.idphong " +
                    "AND ngaycheckin >='"+ngaybatdausql+"' AND ngaycheckout <= '"+ngayketthucsql+"' " +
                    "ORDER BY ngaydi ";
                da = new SqlDataAdapter(sql, conn);
                dtrpt.Clear();
                da.Fill(dtrpt);
                int l = dtrpt.Rows.Count;
                for (int i = 0; i <= l - 1; i++)
                {
                    if (dtrpt.Rows[i].ItemArray[4].ToString() == "0")
                    {
                        dtrpt.Rows[i].SetField(4, "1");
                    }
                }


                rptchitietphong rpt = new rptchitietphong();
                rpt.SetDataSource(dtrpt);
                rpt.DataDefinition.FormulaFields["ngaybatdau"].Text = "'" + ngaybatdau + "'";
                rpt.DataDefinition.FormulaFields["ngayketthuc"].Text = "'" + ngayketthuc + "'";
                rpt.DataDefinition.FormulaFields["Tenks"].Text = "'" + Bientoancuc.tenks + "'";
                rpt.DataDefinition.FormulaFields["Diachi"].Text = "'" + Bientoancuc.diachi + "'";
                rpt.DataDefinition.FormulaFields["sdt"].Text = "'" + Bientoancuc.sdt + "'";
                rpt.DataDefinition.FormulaFields["logofile"].Text = "'" + Bientoancuc.logofile + "'";

                rptchitietphongPrv f = new rptchitietphongPrv(rpt);
                f.Show();
            }
            else
                MessageBox.Show("Cần chọn ngày bắt đầu trước ngày kết thúc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void flocdatachitietphong_Load(object sender, EventArgs e)
        {
            connstr = Bientoancuc.TCconnstr;
            conn.ConnectionString = connstr;
            conn.Open();
            dtpdau.MinDate = Bientoancuc.ngaythanhlap;
            dtpketthuc.MaxDate = DateTime.Now;
        }
    }
}
