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

namespace QL_KhachSan.DichVu
{
    public partial class FrmQLDichVu : Form
    {
        public FrmQLDichVu()
        {
            InitializeComponent();
        }

        private void FrmQLDichVu_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi");
            conn.Open();
            string sql = "SELECT PhieuDV.id_phieudv AS'Mã phiếu DV', KhachHang.ten_kh AS'Tên khách hàng', Phong.so_phong AS'Số phòng', DichVu.tendv AS'Tên DV', PhieuDV.so_luong AS'Số lượng',PhieuDV.thanh_tien AS'Thành tiền'" +
            "FROM  PhieuDV " +
            "JOIN  KhachHang ON PhieuDV.id_khachhang = KhachHang.id_khachhang " +
            "JOIN  Phong ON PhieuDV.id_phong = Phong.id_phong " +
            "JOIN  DichVu ON PhieuDV.id_dichvu = DichVu.id_dichvu;";
            SqlCommand command = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            //hiển thị dữ liệu
            dataQLDV.DataSource = ds.Tables[0];
            showDV();
        }
        private void showDV()
        {
            SqlConnection conn = new SqlConnection("Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi");
            conn.Open();
            string sql = "SELECT dv.id_dichvu AS'Mã dịch vụ', dv.tendv AS'Tên dịch vụ',dv.giadv AS'Giá dịch vụ' FROM DichVu dv";
            SqlCommand command = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            //hiển thị dữ liệu
            dataDV.DataSource = ds.Tables[0];
        }

        private void dataDV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIDDV.Text = dataDV.CurrentRow.Cells[0].Value.ToString();
            txtTenDV.Text = dataDV.CurrentRow.Cells[1].Value.ToString();
            txtGiaTien.Text = dataDV.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnthemdv_Click(object sender, EventArgs e)
        {
            string madv, tendv, tien;
            madv = txtIDDV.Text;
            tendv = txtTenDV.Text;
            tien = txtGiaTien.Text;
            SqlConnection conn = new SqlConnection("Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi");
            conn.Open();
            string sql = "INSERT INTO DichVu values(N'" + madv + "',N'" + tendv + "',N'" + tien + "')";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            showDV();
        }

        private void btnsuadv_Click(object sender, EventArgs e)
        {
            string madv, tendv, tien;
            madv = txtIDDV.Text;
            tendv = txtTenDV.Text;
            tien = txtGiaTien.Text;
            SqlConnection conn = new SqlConnection("Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi");
            conn.Open();
            string sql = "UPDATE DichVu SET tendv=N'" + tendv + "', giadv=N'" + tien + "' Where id_dichvu=N'" + madv + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            showDV();
        }

        private void btnXoaDV_Click(object sender, EventArgs e)
        {
            string madv;
            madv = txtIDDV.Text;
            SqlConnection conn = new SqlConnection("Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi");
            conn.Open();
            string sql = "DELETE from DichVu WHERE id_dichvu=N'" + madv + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            showDV();
        }

        private void cbbmaphong_DropDown(object sender, EventArgs e)
        {
            string ketnoi = "Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi";
            using (SqlConnection conn = new SqlConnection(ketnoi))
            {
                conn.Open();
                // Lấy danh sách mã khách hàng chưa đăng ký phòng
                string sqlphong = @"SELECT id_phong FROM Phong ";

                SqlCommand cmdphong = new SqlCommand(sqlphong, conn);
                SqlDataReader readerPhong = cmdphong.ExecuteReader();

                // Xóa các item cũ trước khi thêm mới
                cbbmaphong.Items.Clear();

                while (readerPhong.Read())
                {
                    cbbmaphong.Items.Add(readerPhong["id_phong"].ToString());
                }
                readerPhong.Close();
            }
        }

        private void btnThemDVPhong_Click(object sender, EventArgs e)
        {
            string maphieudv, soluong, tien, madv, maphong, makh;
            maphieudv = txtidPhieu.Text;
            soluong = txtSL.Text;
            tien = txtTien.Text;
            madv = txtmdv.Text;
            maphong = cbbmaphong.Text;
            makh = cbbidkh.Text;
            SqlConnection conn = new SqlConnection("Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi");
            conn.Open();
            string sql = "INSERT INTO PhieuDV values(N'" + maphieudv + "', N'" + soluong + "', N'" + tien + "', N'" + madv + "', N'" + maphong + "', N'" + makh + "')";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            FrmQLDichVu_Load(sender, e);
        }

        private void cbbidkh_DropDown(object sender, EventArgs e)
        {
            string ketnoi = "Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi";
            using (SqlConnection conn = new SqlConnection(ketnoi))
            {
                conn.Open();
                // Lấy danh sách mã khách hàng chưa đăng ký phòng
                string sqlMaKhachHang = @"SELECT id_khachhang FROM KhachHang";

                SqlCommand cmdMaKhachHang = new SqlCommand(sqlMaKhachHang, conn);
                SqlDataReader readerKhachHang = cmdMaKhachHang.ExecuteReader();

                // Xóa các item cũ trước khi thêm mới
                cbbmaphong.Items.Clear();

                while (readerKhachHang.Read())
                {
                    cbbidkh.Items.Add(readerKhachHang["id_khachhang"].ToString());
                }
                readerKhachHang.Close();
            }
        }

        private void btnXoaDVPhong_Click(object sender, EventArgs e)
        {
            string mapdv;
            mapdv = txtidPhieu.Text;
            SqlConnection conn = new SqlConnection("Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi");
            conn.Open();
            string sql = "DELETE from PhieuDV WHERE id_phieudv=N'" + mapdv + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            FrmQLDichVu_Load(sender, e);
        }

        private void dataQLDV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtidPhieu.Text = dataQLDV.CurrentRow.Cells[0].Value.ToString();
            txtSL.Text = dataQLDV.CurrentRow.Cells[4].Value.ToString();
            txtTien.Text= dataQLDV.CurrentRow.Cells[5].Value.ToString();
        }

        private void txtTien_MouseClick(object sender, MouseEventArgs e)
        {
            float a= float.Parse(txtGiaTien.Text);
            float b= float.Parse(txtSL.Text);
            float tong = a * b;
            txtTien.Text=tong.ToString();
        }
    }
}
