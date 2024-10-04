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

namespace QL_KhachSan.FormPhong
{
    public partial class FrmPhong : Form
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
        public FrmPhong()
        {
            InitializeComponent();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtGiaPhong_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmPhong_Load(object sender, EventArgs e)
        {
            ketnoi();
            //Câu lệnh lấy dữ liệu từ database.
            string sql = "select id_phong, so_phong, trang_thai, so_nguoi ,LoaiPhong.tenloaiphong, gia_phong from LoaiPhong, Phong WHERE LoaiPhong.id_loaiphong=Phong.id_loaiphong";
            SqlCommand cmd = new SqlCommand(sql, con);
            //Tạo liên kết với bảng
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            //Hiển thị dữ liệu.
            dataPhong.DataSource = ds.Tables[0];
        }

        private void dataPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy ra DataGridViewRow tương ứng với hàng đã chọn
                DataGridViewRow row = dataPhong.Rows[e.RowIndex];

                // Gán giá trị của các ô trong hàng đã chọn vào các ô textbox tương ứng
                txtNhapID.Text = row.Cells["id_phong"].Value.ToString();
                txtSoPhong.Text = row.Cells["so_phong"].Value.ToString();
                txtSonguoi.Text = row.Cells["so_nguoi"].Value.ToString();
                //cbbLoaiPhong.Text = row.Cells["id_loaiphong"].Value.ToString();
                cbbTinhTrang.Text = row.Cells["trang_thai"].Value.ToString();
                txtGiaPhong.Text = row.Cells["gia_phong"].Value.ToString();
            }
        }

        private void btnThemPhong_Click(object sender, EventArgs e)
        {
            try
            {
                ketnoi();
                string sql = "INSERT INTO Phong (id_phong,so_phong,so_nguoi,trang_thai,gia_phong, id_loaiphong)" +
                    "VALUES(N'" + txtNhapID.Text + "',N'" + txtSoPhong.Text + "',N'" + txtSonguoi.Text + "',N'" + cbbTinhTrang.Text + "',N'" + txtGiaPhong.Text + "', N'" + cbbLoaiPhong.Text + "')";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm thành công!", "Thêm phòng!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FrmPhong_Load(sender, e);
            }
            catch
            {
                MessageBox.Show("Mã phòng bị trùng, Không thể thêm!", "Thêm phòng!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtNhapIDcantim.Text = "";
            FrmPhong_Load(sender, e);
        }

        private void btnSuaPhong_Click(object sender, EventArgs e)
        {
            string maPhong = txtNhapID.Text;
            string soPhong = txtSoPhong.Text;
            string soNguoi = txtSonguoi.Text;
            // string loaiPhong = cbbLoaiPhong.Text;
            //string queQuan=cbbQueQuan.Text;
            string tinhTrang = cbbTinhTrang.Text;
            string giaPhong = txtGiaPhong.Text;


            ketnoi();
            // Chuẩn bị câu lệnh SQL UPDATE
            string sql = "UPDATE Phong SET so_phong = N'" + soPhong + "', so_nguoi = N'" + soNguoi + "', trang_thai = N'" + tinhTrang + "', gia_phong=N'" + giaPhong + "' WHERE id_phong = N'" + maPhong + "'";

            // Thực thi câu lệnh SQL
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            FrmPhong_Load(sender, e);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                ketnoi();
                string sql;
                //Tìm kiếm theo số phòng
                sql = "select * from Phong where so_phong like N'" + txtNhapIDcantim.Text + "%'";
                SqlDataAdapter sqlda = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                sqlda.Fill(ds);
                dataPhong.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbbLoaiPhong_DropDown(object sender, EventArgs e)
        {
            try
            {
                ketnoi();
                string sqlMaKhachHang = @"SELECT id_loaiphong FROM LoaiPhong";

                SqlCommand cmdMaKhachHang = new SqlCommand(sqlMaKhachHang, con);
                SqlDataReader readerKhachHang = cmdMaKhachHang.ExecuteReader();

                // Xóa các item cũ trước khi thêm mới
                cbbLoaiPhong.Items.Clear();

                while (readerKhachHang.Read())
                {
                    cbbLoaiPhong.Items.Add(readerKhachHang["id_loaiphong"].ToString());
                }
                readerKhachHang.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
