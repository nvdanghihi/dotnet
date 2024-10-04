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
    public partial class FrmKhach : Form
    {

        //Khai báo biến.
        SqlConnection con;
        //DataSet ds;
        //SqlDataAdapter sqda;
        //Thủ tục kết nối.
        public void ketnoi()
        {
            string ketnoi = "Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi";
            con = new SqlConnection(ketnoi);
            con.Open();
        }
        public FrmKhach()
        {
            InitializeComponent();
        }

        private void FrmKhach_Load(object sender, EventArgs e)
        {
            try
            {
                ketnoi();
                //Bước 3: Tạo câu lệnh truy vấn
                string sql;
                sql = "select * from KhachHang";
                //Bước 4: Tạo cầu nối dữ liệu
                SqlCommand sqlcom = new SqlCommand(sql, con);
                SqlDataAdapter sqlda = new SqlDataAdapter(sqlcom);
                //Bước 5: Tạo Dataset
                DataSet ds = new DataSet();
                //Bước 6: Điền dữ liệu
                sqlda.Fill(ds);
                //Bước 7: Hiển thị dữ liệu
                dataKhachhang.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataKhachhang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                String gt;
                // Lấy ra DataGridViewRow tương ứng với hàng đã chọn
                DataGridViewRow row = dataKhachhang.Rows[e.RowIndex];
                // Gán giá trị của các ô trong hàng đã chọn vào các ô textbox tương ứng
                txtIDKH.Text = row.Cells["id_khachhang"].Value.ToString();
                txtHoTenKH.Text = row.Cells["ten_kh"].Value.ToString();
                dateNgaySinh.Text = row.Cells["ngay_sinh"].Value.ToString();
                gt = row.Cells["gioi_tinh"].Value.ToString();
                if (gt == "Nam")
                {
                    rdoNam.Checked = true;
                }
                if (gt == "Nữ")
                {
                    rdoNu.Checked = true;
                }
                txtCCCD.Text = row.Cells["CCCD"].Value.ToString();
                //cbbQueQuan.Text = row.Cells["QueQuan"].Value.ToString();
                txtQuocTichKH.Text = row.Cells["quoc_tich"].Value.ToString();
                txtSDT.Text = row.Cells["sdt"].Value.ToString();

            }
        }

        private void btnThemKH_Click(object sender, EventArgs e)
        {

            ketnoi();
            String gt = "";
            if (rdoNam.Checked == true)
            {
                gt = "Nam";
            }
            if (rdoNu.Checked == true)
            {
                gt = "Nữ";
            }
            //câu lệnh thêm dữ liệu vào sql
            string sql = "INSERT INTO KhachHang (id_khachhang,ten_kh,ngay_sinh,gioi_tinh,CCCD,quoc_tich,sdt)" +
                "VALUES(N'" + txtIDKH.Text + "',N'" + txtHoTenKH.Text + "',N'" + dateNgaySinh.Value.ToString("yyyy-MM-dd") + "',N'" + gt + "',N'" + txtCCCD.Text + "',N'" + txtQuocTichKH.Text + "',N'" + txtSDT.Text + "' )";
            SqlCommand cmd = new SqlCommand(sql, con);
            int kq = cmd.ExecuteNonQuery();
            if (kq > 0)
            {
                MessageBox.Show("Bạn đã thêm thành công khách hàng", "Thêm khách hàng!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FrmKhach_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Không thể thêm.", "Thêm khách hàng!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }    
        }

        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            try
            {
                String gt;
                string ketnoi = "Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi";
                using (SqlConnection conn = new SqlConnection(ketnoi))
                {
                    conn.Open();
                    string sql = "UPDATE KhachHang SET ten_kh = @ten_kh, ngay_sinh = @ngay_sinh, gioi_tinh = @gioi_tinh, CCCD = @CCCD, quoc_tich = @quoc_tich, sdt = @sdt WHERE id_khachhang = @id_khachhang";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ten_kh", txtHoTenKH.Text);
                        cmd.Parameters.AddWithValue("@ngay_sinh", dateNgaySinh.Value);
                        gt=rdoNam.Checked ? "Nam" : "Nữ";
                        cmd.Parameters.AddWithValue("@gioi_tinh", gt);
                        cmd.Parameters.AddWithValue("@CCCD", txtCCCD.Text);
                        cmd.Parameters.AddWithValue("@quoc_tich", txtQuocTichKH.Text);
                        cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);
                        cmd.Parameters.AddWithValue("@id_khachhang", txtIDKH.Text);

                        int kq = cmd.ExecuteNonQuery();
                        if (kq > 0)
                        {
                            MessageBox.Show("Sửa thông tin thành công", "Sửa khách hàng!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FrmKhach_Load(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("Không thể sửa.", "Sửa khách hàng!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult tl;
                tl = MessageBox.Show("Bạn có muốn xóa không?", "Xóa khách hàng!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (tl == DialogResult.Yes) 
             { 
                ketnoi();
                string sql = "DELETE FROM KhachHang WHERE id_khachhang = @id_khachhang";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id_khachhang", txtIDKH.Text);

                    int kq = cmd.ExecuteNonQuery();
                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa khách hàng thành công.", "Xóa khách hàng!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmKhach_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa.", "Xóa khách hàng!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
                else
                {
                    this.Close();
                }    
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
