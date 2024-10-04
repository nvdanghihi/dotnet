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
using TheArtOfDevHtmlRenderer.Adapters;

namespace QL_KhachSan.HoaDon
{
    public partial class FrmHoaDon : Form
    {

        public FrmHoaDon()
        {
            InitializeComponent();
        }
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
        private void showTTHD()
        {
            ketnoi();
            string sql = "SELECT  dp.id_phieudatphong AS'Mã đặt phòng', p.id_phong AS'Mã phòng', kh.ten_kh AS'Tên khách',dp.ngay_nhan AS'Ngày nhận',dp.ngay_tra AS 'Ngày trả', P.gia_phong * DATEDIFF(DAY, DP.ngay_nhan, DP.ngay_tra) AS 'Tiền phòng', dp.tien_coc AS'Tiền cọc'," +
                "(SELECT SUM(pd.thanh_tien) FROM PhieuDV pd WHERE pd.id_phong = p.id_phong) AS 'Tiền dịch vụ'," +
                "(P.gia_phong * DATEDIFF(DAY, DP.ngay_nhan, DP.ngay_tra) - dp.tien_coc + (SELECT SUM(pd.thanh_tien)  " +
                "FROM PhieuDV pd WHERE pd.id_phong = p.id_phong)) AS 'Tổng tiền' " +
                "FROM DatPhong dp INNER JOIN Phong p ON dp.id_phong = p.id_phong INNER JOIN KhachHang kh ON dp.id_khachhang = kh.id_khachhang";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            datathongtin.DataSource = dt;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrmHoaDon_Load(object sender, EventArgs e)
        {
            showTTHD();
        }

        private void datathongtin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string idPhong = datathongtin.CurrentRow.Cells["Mã phòng"].Value.ToString();
            LoadPhieuDVData(idPhong);

            txtIDDP.Text = datathongtin.CurrentRow.Cells[0].Value.ToString();
            txtIDPhong.Text = datathongtin.CurrentRow.Cells[1].Value.ToString();
            txtTienPhong.Text = datathongtin.CurrentRow.Cells[5].Value.ToString();
            txtTienCoc.Text = datathongtin.CurrentRow.Cells[6].Value.ToString();
            txtTienDichVu.Text = datathongtin.CurrentRow.Cells[7].Value.ToString();
            txtTongTien.Text = datathongtin.CurrentRow.Cells[8].Value.ToString();
        }
        private void ThanhToan()
        {
            try
            {
                string connectionString = "Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    try
                    {
                        // Xóa đặt phòng
                        string deleteDatPhongSql = "DELETE FROM DatPhong WHERE id_phieudatphong = N'" + txtIDDP.Text + "'" +
                        "UPDATE Phong SET trang_thai = 'Available' WHERE  id_phong =N'" + txtIDPhong.Text + "' " +
                        "DELETE FROM PhieuDV WHERE id_phong = N'" + txtIDPhong.Text + "'";
                        SqlCommand deleteCmd = new SqlCommand(deleteDatPhongSql, conn);
                        deleteCmd.ExecuteNonQuery();
                        MessageBox.Show("Thanh toán thành công! Hẹn gặp lại quý khách một ngày gần nhất");
                    }
                    catch (Exception ex)
                    {
                        // Hủy bỏ giao dịch nếu có lỗi
                        MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi kết nối: " + ex.Message);
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            ThanhToan();
            showTTHD();
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
                datadv.DataSource = dt;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnTimKiemTraPhong_Click(object sender, EventArgs e)
        {
            try
            {
                ketnoi();
                String sql = "SELECT PhieuDV.id_phieudv AS'Mã dịch vụ', PhieuDV.so_luong AS'Số lượng', PhieuDV.thanh_tien AS'Thành tiền', dv.tendv AS'Tên dịch vụ'" +
                    " FROM PhieuDV INNER JOIN DichVu dv ON PhieuDV.id_dichvu = dv.id_dichvu" +
                    " WHERE id_phong ='" + txttimkiem.Text + "'";
                SqlCommand sqlCommand = new SqlCommand(sql,con);
                sqda= new SqlDataAdapter(sqlCommand);
                ds= new DataSet();
                sqda.Fill(ds);
                datathongtin.DataSource = ds.Tables[0];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);   
            }
        }
    }
}
