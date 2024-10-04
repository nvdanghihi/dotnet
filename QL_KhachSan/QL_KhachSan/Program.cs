using QL_KhachSan.DichVu;
using QL_KhachSan.FormPhong;
using QL_KhachSan.FrmMain;
using QL_KhachSan.HoaDon;
using QL_KhachSan.Phong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_KhachSan
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmDangNhap());
        }
    }
}
