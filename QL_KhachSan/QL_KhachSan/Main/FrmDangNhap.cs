using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_KhachSan.FrmMain
{
    public partial class FrmDangNhap : Form
    {
        public FrmDangNhap()
        {
            InitializeComponent();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //Bước 1: Kết nối csdl
                string ketnoi;
                ketnoi = "Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi";
                SqlConnection sqlcon = new SqlConnection();
                sqlcon.ConnectionString = ketnoi;
                // Bước 2: Mở kết nối
                sqlcon.Open();
                //MessageBox.Show("Kết nối thành công");

                // Bước 3: Tạo câu lệnh truy vấn sử dụng tham số thay thế
                string sql = "SELECT * FROM Userinfor WHERE Username = @username AND Password = @password";
                SqlCommand command = new SqlCommand(sql, sqlcon);
                command.Parameters.AddWithValue("@username", txtUser.Text);
                command.Parameters.AddWithValue("@password", txtPass.Text);

                // Bước 4: Thực thi câu truy vấn và xử lý kết quả
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    // Đăng nhập thành công
                    //MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Chuyển đến form chính
                    FrmLoadMain loadMain = new FrmLoadMain();
                    loadMain.Show();

                }
                else
                {
                    DialogResult tl;
                    // Đăng nhập thất bại
                    tl = MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Thông báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                    {
                        if (tl == DialogResult.Retry)
                        {
                            txtUser.Text = "";
                            txtPass.Text = "";
                            txtUser.Focus();
                        }
                        else
                        {
                            this.Close();
                        }
                    }

                }

                reader.Close(); // Đóng reader
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void fDangNhap_Load(object sender, EventArgs e)
        {
            lbError.Visible = false;
            label2.Focus();
        }
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        

        private void checkMK_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkMK.Checked == false)
            {
                //Đưa về giá trị 0 = null.
                txtPass.PasswordChar = '\0';
            }
            else
            {
                //Set kí tự cho mật khẩu.
                txtPass.PasswordChar = '●';
            }

        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar=='\r')
            {
                try
                {
                    //Bước 1: Kết nối csdl
                    string ketnoi;
                    ketnoi = "Data Source=ITACHI\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=danghihi";
                    SqlConnection sqlcon = new SqlConnection();
                    sqlcon.ConnectionString = ketnoi;
                    // Bước 2: Mở kết nối
                    sqlcon.Open();
                    //MessageBox.Show("Kết nối thành công");

                    // Bước 3: Tạo câu lệnh truy vấn sử dụng tham số thay thế
                    string sql = "SELECT * FROM Userinfor WHERE Username = @username AND Password = @password";
                    SqlCommand command = new SqlCommand(sql, sqlcon);
                    command.Parameters.AddWithValue("@username", txtUser.Text);
                    command.Parameters.AddWithValue("@password", txtPass.Text);

                    // Bước 4: Thực thi câu truy vấn và xử lý kết quả
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        // Đăng nhập thành công
                       // MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Chuyển đến form chính
                        FrmLoadMain loadMain = new FrmLoadMain();
                        loadMain.Show();

                    }
                    else
                    {
                        DialogResult tl;
                        // Đăng nhập thất bại
                        tl = MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Thông báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        {
                            if (tl == DialogResult.Retry)
                            {
                                txtUser.Text = "";
                                txtPass.Text = "";
                                txtUser.Focus();
                            }
                            else
                            {
                                this.Close();
                            }
                        }

                    }

                    reader.Close(); // Đóng reader
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }
    }
}
