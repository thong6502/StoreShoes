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

namespace shoe_store_manager
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void gunaLabel1_Click(object sender, EventArgs e)
        {

        }

        
        bool check_eye = false;
        private void tbx_matKhau_IconRightClick(object sender, EventArgs e)
        {
            if(!check_eye)
            {
                tbx_matKhau.IconRight = Properties.Resources.eye;
                tbx_matKhau.UseSystemPasswordChar = false;
                tbx_matKhau.PasswordChar = (char)0;
                check_eye = true;
            }
            else
            {
                tbx_matKhau.IconRight = Properties.Resources.eye_slash;
                tbx_matKhau.UseSystemPasswordChar = true;
                check_eye = false;
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string query = "SELECT MaTK FROM TaiKhoan WHERE TenTK = @TenTK AND MatKhau = @MatKhau";
            object[] parameter = new object[] { tbx_taiKhoan.Text, tbx_matKhau.Text };
            DataTable result = DataProvider.Instance.ExcuteQuery(query, parameter);
            if (result.Rows.Count > 0) {
                string MaTK = result.Rows[0]["MaTK"].ToString();
                string query_MaNV = "SELECT MaNV FROM NhanVien WHERE MaTK = @MaTK";
                object[] parameter_Ma_NV = new object[] { MaTK };
                string MaNV =  DataProvider.Instance.ExcuteQuery(query_MaNV, parameter_Ma_NV).Rows[0]["MaNV"].ToString();
                GlobalVariables.MaNV = MaNV;
                GlobalVariables.MaTK = MaTK;
                MainForm f = new MainForm();
                f.Show();
                Hide();
                f.Logout += F_Logout;
            }else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu");
            }
            
        }

        private void F_Logout(object sender, EventArgs e)
        {
            (sender as MainForm).Close();
            this.Show();
        }
    }
}
