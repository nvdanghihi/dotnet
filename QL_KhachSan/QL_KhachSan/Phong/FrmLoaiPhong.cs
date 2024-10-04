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
    public partial class FrmLoaiPhong : Form
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

        public FrmLoaiPhong()
        {
            InitializeComponent();
        }
        private void showLoaiPhong()
        {
            ketnoi();
            string sql = "Select LoaiPhong.id_loaiphong, LoaiPhong.tenloaiphong from LoaiPhong";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataLoaiPhong.DataSource = ds.Tables[0];
        }
        private void FrmLoaiPhong_Load(object sender, EventArgs e)
        {
            showLoaiPhong();
        }

        private void dataLoaiPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtma.Text = dataLoaiPhong.CurrentRow.Cells[0].Value.ToString();
            txtten.Text = dataLoaiPhong.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnThemLP_Click(object sender, EventArgs e)
        {
            string idloaiphong, tenphong;
            idloaiphong = txtma.Text;
            tenphong = txtten.Text;
            ketnoi();
            string sql = "INSERT INTO LoaiPhong values(N'" + idloaiphong + "', N'" + tenphong + "')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            showLoaiPhong();
        }

        private void btnSuaLP_Click(object sender, EventArgs e)
        {
            string idloaiphong, tenphong;
            idloaiphong = txtma.Text;
            tenphong = txtten.Text;
            ketnoi();
            string sql = "UPDATE  LoaiPhong SET tenloaiphong=N'" + tenphong + "' WHERE id_loaiphong=N'" + idloaiphong + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            showLoaiPhong();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtNhapIDcantim.Text = "";
            txtma.Text = "";
            txtten.Text = "";
            showLoaiPhong();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                ketnoi();
                string sql;
                //Tìm kiếm theo số phòng
                sql = "select * from LoaiPhong where id_loaiphong like N'" + txtNhapIDcantim.Text + "%'";
                SqlDataAdapter sqlda = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                sqlda.Fill(ds);
                dataLoaiPhong.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
