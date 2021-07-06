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
    
    public partial class flocdata : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dtrpt = new DataTable();
        DataTable dt = new DataTable();
        string sql, connstr;
        SqlCommand cmd;
     

        public flocdata()
        {
            InitializeComponent();
        }
       
        private void flocdata_Load(object sender, EventArgs e)
        {
            connstr = Bientoancuc.TCconnstr;
            conn.ConnectionString = connstr;
            conn.Open();
            dtpketthuc.MaxDate = DateTime.Now;
            dtpdau.MinDate = Bientoancuc.ngaythanhlap;
        }

        private void btnloc_Click(object sender, EventArgs e)
        {

            if (DateTime.Compare(dtpketthuc.Value, dtpdau.Value) != -1)
            {
                string ngaybatdausql = dtpdau.Value.ToString("yyyy-MM-dd");
                string ngaybatdau = dtpdau.Value.ToString("dd-MM-yyyy");
                string ngayketthucsql = dtpketthuc.Value.ToString("yyyy-MM-dd");
                string ngayketthuc = dtpketthuc.Value.ToString("dd-MM-yyyy");
                /*
                DateTime date = dtpdau.Value;
                string ngaydau = Convert.ToString(date.Year) + "-" + Convert.ToString(date.Month) + "-" + Convert.ToString(date.Day);
                date = dtpketthuc.Value;
                string ngaycuoi = Convert.ToString(date.Year) + "-" + Convert.ToString(date.Month) + "-" + Convert.ToString(date.Day);
                */
                sql = "EXEC Laydata '" + ngaybatdausql + "','" + ngayketthucsql + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                sql = "Select * from dbo.data ";
                da = new SqlDataAdapter(sql, conn);
                dtrpt.Clear();
                da.Fill(dtrpt);
              
                rptHieuSuatPhong rpt = new rptHieuSuatPhong();
                rpt.SetDataSource(dtrpt);
                rpt.DataDefinition.FormulaFields["ngaybatdau"].Text = "'" + ngaybatdau + "'";
                rpt.DataDefinition.FormulaFields["ngaykethuc"].Text = "'" + ngayketthuc + "'";
                rpt.DataDefinition.FormulaFields["Tenks"].Text = "'" + Bientoancuc.tenks + "'";
                rpt.DataDefinition.FormulaFields["Diachi"].Text = "'" + Bientoancuc.diachi + "'";
                rpt.DataDefinition.FormulaFields["sdt"].Text = "'" + Bientoancuc.sdt + "'";
                rpt.DataDefinition.FormulaFields["logofile"].Text = "'" + Bientoancuc.logofile + "'";
                rptHieuSuatPhongprv f = new rptHieuSuatPhongprv(rpt);
                f.Show();
            }
            else
                MessageBox.Show("Cần chọn ngày bắt đầu trước ngày kết thúc", "Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error) ;


        }
    }
}
