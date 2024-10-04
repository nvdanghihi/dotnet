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

namespace QL_KhachSan.Phong
{
    public partial class FrmQLPhong : Form
    {

        //Khai báo biến.
        SqlConnection con;
        DataSet ds;
        SqlDataAdapter sqda;
        //Thủ tục kết nối.
        public void ketnoi()
        {
            string ketnoi = "Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi";
            con = new SqlConnection(ketnoi);
            con.Open();
        }
        public FrmQLPhong()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void FrmQLPhong_Load(object sender, EventArgs e)
        {
            ketnoi();
            string query =
                @"
            SELECT p.id_phong AS'Mã phòng',
                   P.so_phong AS 'Số phòng', 
                   KH.ten_kh AS 'Tên khách hàng', 
                   DP.ngay_nhan AS 'Ngày nhận', 
                   DP.ngay_tra AS 'Ngày trả', 
                   (P.gia_phong * DATEDIFF(DAY, DP.ngay_nhan, DP.ngay_tra)) AS 'Giá tiền', 
                   DP.tien_coc AS 'Tiền cọc',
                   P.id_loaiphong AS 'Mã loại phòng'
            FROM DatPhong DP
            INNER JOIN Phong P ON DP.id_phong = P.id_phong
            INNER JOIN KhachHang KH ON DP.id_khachhang = KH.id_khachhang;";

            SqlCommand command = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            //hiển thị dữ liệu
            dataqlphong.DataSource = ds.Tables[0];
            dataqlphong.CellClick += dataqlphong_CellClick;
        }

        private void dataqlphong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataqlphong.Rows[e.RowIndex];
                txtTienCoc.Text= dataqlphong.CurrentRow.Cells[6].Value.ToString();
                txtTienPhong.Text= dataqlphong.CurrentRow.Cells[5].Value.ToString();
                
                string idPhong = row.Cells["Mã phòng"].Value.ToString();
                string idLoaiPhong = row.Cells["Mã loại phòng"].Value.ToString();
                LoadPhieuDVData(idPhong);
                LoaiPhong(idLoaiPhong);
            }
        }
        private void LoadPhieuDVData(string idPhong)
        {
            try
            {
                    ketnoi();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT PhieuDV.id_phieudv AS'Mã dịch vụ', PhieuDV.so_luong AS'Số lượng', PhieuDV.thanh_tien AS'Thành tiền', dv.tendv AS'Tên dịch vụ'" +
                        " FROM PhieuDV INNER JOIN DichVu dv ON PhieuDV.id_dichvu = dv.id_dichvu" +
                        " WHERE id_phong = @id_phong", con);
                    adapter.SelectCommand.Parameters.AddWithValue("@id_phong", idPhong);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    datadichvu.DataSource = dt;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void LoaiPhong(string idLoaiPhong)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi"))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT LoaiPhong.id_loaiphong AS'Mã loại phòng',LoaiPhong.tenloaiphong AS'Tên loại phòng'  FROM LoaiPhong WHERE id_loaiphong = @id_loaiphong", conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@id_loaiphong", idLoaiPhong);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataloaiphong.DataSource = dt;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void datadichvu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTienDichVu.Text = datadichvu.CurrentRow.Cells[2].Value.ToString();
            float a = float.Parse(txtTienCoc.Text);
            float b = float.Parse(txtTienPhong.Text);
            float c = float.Parse(txtTienDichVu.Text);
            float tong =  b- a + c;
            txtTongTien.Text=tong.ToString();
        }
    }
}
