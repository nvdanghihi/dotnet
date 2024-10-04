using QL_KhachSan;
using QL_KhachSan.DichVu;
using QL_KhachSan.FormPhong;
using QL_KhachSan.HoaDon;
using QL_KhachSan.Phong;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_KhachSan.FrmMain
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
       

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            // Tạo một thể hiện của fTrangChu
            FrmTrangChu trangchu = new FrmTrangChu();

            // Đặt Parent của fTrangChu thành panelNoiDung của fMain
            trangchu.TopLevel = false;
            trangchu.FormBorderStyle = FormBorderStyle.None;
            trangchu.Dock = DockStyle.Fill;
            paneNoidung.Controls.Add(trangchu);
            trangchu.BringToFront();
            // Hiển thị fTrangChu
            trangchu.Show();
            //panelDiChuyen.Visible = true;
            //panelDiChuyen.Top = btnTrangChu.Top + 17;
        }
        #region FrmPhong
        private void btnPhong_Click(object sender, EventArgs e)
        {
           FrmPhong phong = new FrmPhong();
            phong.TopLevel = false;
            phong.FormBorderStyle = FormBorderStyle.None;
            phong.Dock = DockStyle.Fill;
            paneNoidung.Controls.Add(phong);
            phong.BringToFront();
            //Hiển thị phòng
            phong.Show();
        }
        #endregion
        #region FrmDatPhong
        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            FrmDatPhong datphong = new FrmDatPhong();
            //hiển thị form đặt phòng lên pane nội dung
            datphong.TopLevel = false;
            datphong.FormBorderStyle= FormBorderStyle.None;
            datphong.Dock = DockStyle.Fill;
            paneNoidung.Controls.Add(datphong);
            datphong.BringToFront();
            //hiển thị frm dặt phòng
            datphong.Show();

        }
        #endregion
        #region FrmQLPhong
        private void btnQLPhong_Click(object sender, EventArgs e)
        {
            FrmQLPhong qLPhong = new FrmQLPhong();
            //Hiển thị form phòng trong pane nội dung
            qLPhong.TopLevel = false;
            qLPhong.FormBorderStyle =FormBorderStyle.None;
            qLPhong.Dock = DockStyle.Fill;
            paneNoidung.Controls.Add(qLPhong);
            qLPhong.BringToFront();
            //Hiển thị form quản lý phòng
            qLPhong.Show();
        }
        #endregion
        #region FrmHoaDon
        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            FrmHoaDon   hoaDon = new FrmHoaDon();
            //Hiển thị form hóa đơn lên pane nội dung
            hoaDon.TopLevel = false;
            hoaDon.FormBorderStyle = FormBorderStyle.None;
            hoaDon.Dock = DockStyle.Fill;
            paneNoidung.Controls.Add(hoaDon);
            hoaDon.BringToFront();
            //hiển thị form
            hoaDon.Show();

        }
        #endregion
        #region FrmKhach
        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            FrmKhach khach=new FrmKhach();
            khach.TopLevel = false;
            khach.FormBorderStyle = FormBorderStyle.None;
            khach.Dock = DockStyle.Fill;
            paneNoidung.Controls.Add(khach);
            khach.BringToFront();
            //hiển thị form
            khach.Show();
        }
        #endregion

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            FrmTrangChu trangChu = new FrmTrangChu();
            trangChu.TopLevel = false;
            trangChu.FormBorderStyle = FormBorderStyle.None;
            trangChu.Dock = DockStyle.Fill;
            paneNoidung.Controls.Add(trangChu);
            trangChu.BringToFront();
            trangChu.Show();
            
        }

        private void btnLoaiPhong_Click(object sender, EventArgs e)
        {
            FrmLoaiPhong frmloaiPhong = new FrmLoaiPhong();
            frmloaiPhong.TopLevel = false;
            frmloaiPhong.FormBorderStyle = FormBorderStyle.None;
            frmloaiPhong.Dock = DockStyle.Fill;
            paneNoidung.Controls.Add(frmloaiPhong);
            frmloaiPhong.BringToFront();
            //Hiển thị phòng
            frmloaiPhong.Show();
        }

        private void btnQLDichVu_Click(object sender, EventArgs e)
        {
            FrmQLDichVu frmQLDichVu = new FrmQLDichVu();
            frmQLDichVu.TopLevel = false;
            frmQLDichVu.FormBorderStyle = FormBorderStyle.None;
            frmQLDichVu.Dock = DockStyle.Fill;
            paneNoidung.Controls.Add(frmQLDichVu);
            frmQLDichVu.BringToFront();
            //Hiển thị phòng
            frmQLDichVu.Show();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
