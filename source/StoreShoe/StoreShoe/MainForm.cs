using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shoe_store_manager
{
    public partial class MainForm : Form
    {
        // Singleton instance
        public static MainForm Instance { get; private set; }
        private int minH;
        private int maxH;   
        public MainForm()
        {
            InitializeComponent();
            // Set the singleton instance
            Instance = this;

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            minH = 35;
            maxH = minH * 4;
            container(new san_pham());
            phanQuyen();
        }

        private void phanQuyen()
        {
            string query_pq = "SELECT PhanQuyen FROM TaiKhoan WHERE MaTK = @MaTK";
            object[] parameter_pq = new object[] { GlobalVariables.MaTK };
            string pq = DataProvider.Instance.ExcuteQuery(query_pq, parameter_pq).Rows[0]["PhanQuyen"].ToString();

            string query_pname = "SELECT TenNV FROM NhanVien WHERE MaNV = @MaNV";
            object[] parameter_name = new object[] { GlobalVariables.MaNV };
            string name = DataProvider.Instance.ExcuteQuery(query_pname, parameter_name).Rows[0]["TenNV"].ToString();

            if (pq != "Admin")
            {
                btn_nhanvien.Visible = false;
                btn_nhacc.Visible = false;
                btn_sanpham.Visible = false;
                btn_nhapHang.Visible = false;
                maxH = minH * 3;
            }

            lb_name.Text = name;
            lb_PhanQuyen.Text = pq;
        }

        private void container(object _form)
        {
            if (gunaPanel_container.Controls.Count > 0) gunaPanel_container.Controls.Clear();

            Form fm = _form as Form;
            fm.TopLevel = false;
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.Dock = DockStyle.Fill;
            gunaPanel_container.Controls.Add(fm);
            gunaPanel_container.Tag = fm;
            fm.Show();
        }

        private void default_color()
        {
            btn_donhang.FillColor = Color.Transparent;
            btn_sanpham.FillColor = Color.Transparent;
            btn_nhanvien.FillColor = Color.Transparent;
            btn_nhacc.FillColor = Color.Transparent;
            btn_cauHinh.FillColor = Color.Transparent;

            btn_taiKhoan.ForeColor = Color.DarkGray;
            btn_dangXuat.ForeColor = Color.DarkGray;
            btn_allDonHang.ForeColor = Color.DarkGray;
            btn_nhapHang.ForeColor = Color.DarkGray;
            btn_banHang.ForeColor = Color.DarkGray;

            
        }



        private void btn_donhang_Click(object sender, EventArgs e)
        {
            if (menuContainer_donHang.Height >= 140)
            {
                donHangTransition.Start();
            }
            else
            {
                label_val.Text = "Đơn hàng / Tất cả đơn hàng";
                img_top.Image = Properties.Resources.order;
                default_color();
                btn_donhang.FillColor = Color.FromArgb(128, 128, 255);
                btn_allDonHang.ForeColor = Color.White;
                donHangTransition.Start();
                container(new all_don_hang());
            }
            
        }

        private void btn_nhacc_Click(object sender, EventArgs e)
        {
            label_val.Text = "Nhà cung cấp";
            img_top.Image = Properties.Resources.truck;
            default_color();
            btn_nhacc.FillColor = Color.FromArgb(128, 128, 255);
            container(new nha_cung_cap());
        }

        private void btn_sanpham_Click(object sender, EventArgs e)
        {
            label_val.Text = "Sản phẩm";
            img_top.Image = Properties.Resources.product;
            default_color();
            btn_sanpham.FillColor = Color.FromArgb(128, 128, 255);
            container(new san_pham());
        }

        private void btn_nhanvien_Click(object sender, EventArgs e)
        {
            label_val.Text = "Nhân viên";
            img_top.Image = Properties.Resources.employ;
            default_color();
            btn_nhanvien.FillColor = Color.FromArgb(128, 128, 255);
            container(new nhan_vien());
        }

        private void btn_baocao_Click(object sender, EventArgs e)
        {
            label_val.Text = "Thống kê";
            img_top.Image = Properties.Resources.report;
            default_color();
            //container(new bao_cao());
        }

        bool menuExpand_donHang = true;

        private void menuTransition_Tick(object sender, EventArgs e)
        {
            if(menuExpand_donHang)
            {
                menuContainer_donHang.Height += 10;
                if(menuContainer_donHang.Height >= maxH)
                {
                    donHangTransition.Stop();
                    menuExpand_donHang = false;
                }
            }
            else
            {
                menuContainer_donHang.Height -= 10;
                if (menuContainer_donHang.Height <= minH)
                {
                    donHangTransition.Stop();
                    menuExpand_donHang = true;
                }
            }
        }

        private void btn_allDonHang_Click(object sender, EventArgs e)
        {
            label_val.Text = "Đơn hàng / Tất cả đơn hàng";
            img_top.Image = Properties.Resources.order;
            default_color();
            btn_donhang.FillColor = Color.FromArgb(128, 128, 255);
            btn_allDonHang.ForeColor = Color.White;
            container(new all_don_hang());
        }

        private void btn_nhapHang_Click(object sender, EventArgs e)
        {
            label_val.Text = "Đơn hàng / Nhập hàng";
            img_top.Image = Properties.Resources.order;
            default_color();
            btn_donhang.FillColor = Color.FromArgb(128, 128, 255);
            btn_nhapHang.ForeColor = Color.White;
            container(new nhap_hang());
        }

        private void btn_banHang_Click(object sender, EventArgs e)
        {
            label_val.Text = "Đơn hàng / Bán hàng";
            img_top.Image = Properties.Resources.order;
            default_color();
            btn_donhang.FillColor = Color.FromArgb(128, 128, 255);
            btn_banHang.ForeColor = Color.White;
            container(new ban_hang());
        }
        private void btn_closeMain_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        bool menuExpand_cauHinh = true;
        private void cauHinhTranstion_Tick(object sender, EventArgs e)
        {
            if (menuExpand_cauHinh)
            {
                menuContainer_cauHinh.Height += 10;
                if (menuContainer_cauHinh.Height >= 105)
                {
                    cauHinhTransition.Stop();
                    menuExpand_cauHinh = false;
                }
            }
            else
            {
                menuContainer_cauHinh.Height -= 10;
                if (menuContainer_cauHinh.Height <= 35)
                {
                    cauHinhTransition.Stop();
                    menuExpand_cauHinh = true;
                }
            }
        }

        private void btn_cauHinh_Click(object sender, EventArgs e)
        {
            

            if (menuContainer_cauHinh.Height >= 105)
            {
                cauHinhTransition.Start();
            }
            else
            {
                label_val.Text = "Cấu hình / Tài khoản";
                img_top.Image = Properties.Resources.setting;
                default_color();
                btn_cauHinh.FillColor = Color.FromArgb(128, 128, 255);
                btn_taiKhoan.ForeColor = Color.White;
                cauHinhTransition.Start();
                container(new tai_khoan());
            }
        }

        private void btn_taiKhoan_Click(object sender, EventArgs e)
        {
            label_val.Text = "Cấu hình / Tài khoản";
            img_top.Image = Properties.Resources.setting;
            default_color();
            btn_cauHinh.FillColor = Color.FromArgb(128, 128, 255);
            btn_taiKhoan.ForeColor = Color.White;
            cauHinhTransition.Start();
            container(new tai_khoan());
        }


        public void btn_dangXuat_Click(object sender, EventArgs e)
        {
            //Logout(this, new EventArgs());
            this.Hide();
            LoginForm.instance.Show();
        }

    }
}
