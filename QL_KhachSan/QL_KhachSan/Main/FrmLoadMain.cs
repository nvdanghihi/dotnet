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
    public partial class FrmLoadMain : Form
    {
        public FrmLoadMain()
        {
            InitializeComponent();
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            FrmMain main = new FrmMain();
            this.Hide();
            main.Show();
            //this.WindowState = FormWindowState.Maximized;
            //FrmTrangChu trangChu = new FrmTrangChu();
            //trangChu.TopLevel = false;
            //trangChu.FormBorderStyle = FormBorderStyle.None;
            //trangChu.Dock = DockStyle.Fill;
            //paneNoidung.Controls.Add(trangChu);
            //trangChu.BringToFront();
            //trangChu.Show();
        }
    }
}
