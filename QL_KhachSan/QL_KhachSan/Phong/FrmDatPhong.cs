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
    public partial class FrmDatPhong : Form
    {
        public FrmDatPhong()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            txtmaphieudv.Clear();
            txtTienCoc.Clear();
            txtmaphieudv.Focus();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiemKhachHang.Clear();
            FrmDatPhong_Load(sender, e);
            txtTimKiemKhachHang.Focus();
        }

        private void FrmDatPhong_Load(object sender, EventArgs e)
        {
            string ketnoi = "Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi";
            SqlConnection conn = new SqlConnection(ketnoi);
            //mở    
            conn.Open();
            // select tới bảng tb_khachhang
            string sql = "select * from DatPhong";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            //hiển thị dữ liệu
            dataDatphong.DataSource = ds.Tables[0];
            // lấy dữ lieeij phòng
            string sqlMaPhong = "SELECT id_phong FROM Phong WHERE trang_thai= N'Available'";
            SqlCommand cmdMaPhong = new SqlCommand(sqlMaPhong, conn);
            SqlDataReader reader = cmdMaPhong.ExecuteReader();
            while (reader.Read())
            {
                cbomaphong.Items.Add(reader["id_phong"].ToString());
            }
            loadcbb();
            reader.Close();
        }
        private void loadcbb()
        {
            string ketnoi = "Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi";
            SqlConnection conn = new SqlConnection(ketnoi);
            //mở    
            conn.Open();
            // select tới bảng tb_khachhang
            string sql = "select id_loaiphong, tenloaiphong from LoaiPhong";
            SqlCommand cmd3 = new SqlCommand(sql, conn);
            SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            cbbLoaiPhong.DisplayMember = "id_loaiphong";
            cbbLoaiPhong.DataSource = dt3;
        }

        private void cbomakh_DropDown(object sender, EventArgs e)
        {
            string ketnoi = "Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi";
            using (SqlConnection conn = new SqlConnection(ketnoi))
            {
                conn.Open();
                // Lấy danh sách mã khách hàng chưa đăng ký phòng
                string sqlMaKhachHang = @"SELECT kh.id_khachhang FROM KhachHang kh LEFT JOIN DatPhong dp ON kh.id_khachhang = dp.id_khachhang  WHERE dp.id_khachhang IS NULL";

                SqlCommand cmdMaKhachHang = new SqlCommand(sqlMaKhachHang, conn);
                SqlDataReader readerKhachHang = cmdMaKhachHang.ExecuteReader();

                // Xóa các item cũ trước khi thêm mới
                cbomakh.Items.Clear();

                while (readerKhachHang.Read())
                {
                    cbomakh.Items.Add(readerKhachHang["id_khachhang"].ToString());
                }
                readerKhachHang.Close();
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string ketnoi = "Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi";
            SqlConnection conn = new SqlConnection(ketnoi);
            // mở
            conn.Open();
            //câu lệnh thêm dữ liệu vào sql
            string sql = "INSERT INTO DatPhong (id_phieudatphong , id_phong,ngay_nhan, ngay_tra, tien_coc, id_loaiphong,id_khachhang)" +
                "VALUES(N'" + txtmaphieudv.Text + "',N'" + cbomaphong.Text + "',N'" + dateTimeNgayThue.Value.ToString("yyyy-MM-dd") + "',N'" + dateTimeNgayTra.Value.ToString("yyyy-MM-dd") + "',N'" + txtTienCoc.Text + "',N'" + cbbLoaiPhong.Text + "',N'" + cbomakh.Text + "')" +
                "UPDATE Phong SET trang_thai = 'Occupied' WHERE  id_phong =N'" + cbomaphong.Text + "' ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            int kq = cmd.ExecuteNonQuery();
            if (kq > 0)
            {
                MessageBox.Show("Đăng ký phòng thành công!");
                FrmDatPhong_Load(sender, e);
            }
        }
        //Xóa đặt phòng
        private void XoaDP()
        {
            try
            {
                string connectionString = "Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    try
                    {
                        DialogResult tl;
                        tl = MessageBox.Show("Bạn có muốn xóa không?", "Xóa đặt phòng!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (tl == DialogResult.Yes) { 
                        // Xóa đặt phòng
                        string deleteDatPhongSql = "DELETE FROM DatPhong WHERE id_phieudatphong = N'" + txtmaphieudv.Text + "'" +
                        "UPDATE Phong SET trang_thai = 'Available' WHERE  id_phong =N'" + cbomaphong.Text + "' " +
                        "DELETE FROM PhieuDV WHERE id_phong = N'" + cbomaphong.Text + "'";
                        SqlCommand deleteCmd = new SqlCommand(deleteDatPhongSql, conn);
                        deleteCmd.ExecuteNonQuery();
                        MessageBox.Show("Xóa đặt phòng thành công và cập nhật trạng thái phòng về trống.");
                        }
                        else
                        {
                            this.Close();
                        }    
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
                MessageBox.Show("Đã xảy ra lỗi : " + ex.Message);
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            XoaDP();
            FrmDatPhong_Load(sender,e);
        }

        private void dataDatphong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmaphieudv.Text = dataDatphong.CurrentRow.Cells[0].Value.ToString();
            txtTienCoc.Text = dataDatphong.CurrentRow.Cells[3].Value.ToString();
            cbomaphong.Text = dataDatphong.CurrentRow.Cells[4].Value.ToString();
            cbbLoaiPhong.Text = dataDatphong.CurrentRow.Cells[5].Value.ToString();
            cbomakh.Text = dataDatphong.CurrentRow.Cells[6].Value.ToString();
            txtTimKiemKhachHang.Text = dataDatphong.CurrentRow.Cells[6].Value.ToString();
        }

        private void btnTimKhachHang_Click(object sender, EventArgs e)
        {
            try
            {
                string ketnoi = "Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi";
                SqlConnection conn = new SqlConnection(ketnoi);
                // mở
                conn.Open();
                String sql = "SELECT dp.id_phieudatphong, dp.ngay_nhan, dp.ngay_tra, dp.tien_coc, dp.id_phong, p.id_loaiphong, kh.id_khachhang FROM KhachHang kh JOIN DatPhong dp ON kh.id_khachhang = dp.id_khachhang JOIN Phong p ON dp.id_phong = p.id_phong JOIN LoaiPhong lp ON p.id_loaiphong = lp.id_loaiphong WHERE kh.id_khachhang = N'" + txtTimKiemKhachHang.Text+"'";
                SqlDataAdapter sqlda = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                sqlda.Fill(ds);
                dataDatphong.DataSource = ds.Tables[0];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
