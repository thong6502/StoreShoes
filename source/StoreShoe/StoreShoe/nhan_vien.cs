using Guna.UI.WinForms;
using Guna.UI2.WinForms;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace shoe_store_manager
{
    public partial class nhan_vien : Form
    {
        private Dictionary<Guna2TextBox, Guna2Button> textBoxWarningPairs;
        Email email = new Email();
        public nhan_vien()
        {
            InitializeComponent();
        }

        private void nhan_vien_Load(object sender, EventArgs e)
        {
            tbWarningPairs();
            addDataSource();
            delete.Enabled = false;
        }
       

        // Tạo một từ điển để lưu trữ các cặp Guna2TextBox và Guna2Button cảnh báo
        private void tbWarningPairs()
        {
            textBoxWarningPairs = new Dictionary<Guna2TextBox, Guna2Button>()
            {
                { tb_name, warning1 },
                { tb_address, warning2 },
                { tb_phone, warning3 },
                { tb_chucVu, warning4 },
                { tb_Email, warning5 }
            };
        }

        private void disabeled()
        {
            foreach (Control c in this.Controls)
            {
                // Kiểm tra xem control có phải là edit_box hay không
                if (c.Name != "container_edit_box")
                {
                    c.Enabled = false;
                }
            }

        }

        private void enabeled()
        {
            foreach (Control c in this.Controls)
            {
                c.Enabled = true;
            }
        }

        private void undisplay_warning()
        {

            foreach(Control c in this.edit_box.Controls)
            {
                if(c is Guna2Button && c.Name != "xac_nhan" && c.Name != "huy")
                {
                    c.Visible = false;
                    c.Text = "Nội dung trống";
                }
            }
        }

        private void clear_content_tb()
        {
            foreach (Control c in this.edit_box.Controls)
            {
                if(c is Guna2TextBox)
                {
                    c.Text = "";
                }
            }
        }

        private void unVisible_boxFilter()
        {
            box_filter.Visible = false;
        }

        private void visible_boxFilter()
        {
            box_filter.Visible = true;
        }

        private void addDataSource()
        {
            String query = "SELECT * FROM NhanVien";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query);
            data.DataSource = dt;
        }


        private void add_Click(object sender, EventArgs e)
        {
            isEdit = false;
            container_edit_box.Visible = true;
            undisplay_warning();
            unVisible_boxFilter();
            disabeled();

        }
        private void huy_Click(object sender, EventArgs e)
        {
            container_edit_box.Visible = false;
            clear_content_tb();
            undisplay_warning();
            enabeled();
        }

        private bool isEdit;
        private void xac_nhan_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem tất cả các nút cảnh báo có đang hiển thị hay không
            bool allWarningsHidden = true;
            foreach (KeyValuePair<Guna2TextBox, Guna2Button> pair in textBoxWarningPairs)
            {
                if (pair.Key.Text == "")
                {
                    allWarningsHidden = false;
                    break;
                }
            }

            if (allWarningsHidden)
            {
                if(isEdit)
                {
                    sua_rowData();
                }
                else
                {
                    them_RowData();
                }
                Search_input();
                container_edit_box.Visible = false;
                clear_content_tb();
                undisplay_warning();
                enabeled();

            }

            foreach (KeyValuePair<Guna2TextBox, Guna2Button> pair in textBoxWarningPairs)
            {
                pair.Value.Visible = pair.Key.Text == "";
            }

        }

        private void them_RowData()
        {
            string queryCountId = "SELECT COUNT(*) FROM NhanVien WHERE MaNV = @MaNV";
            string maNV = DataProvider.Instance.GenerateId(queryCountId, "NV");
            string tenNV = tb_name.Text;
            string diaChi = tb_address.Text;
            string soDienThoai = tb_phone.Text;
            string email = tb_Email.Text;
            string ChucVu = tb_chucVu.Text;
            string luong = tb_Luong.Text;

            string query = "INSERT INTO NhanVien (MaNV, TenNV, DiaChi, SDT, Email, Luong, ChucVu) VALUES (@MaNV, @TenNV, @DiaChi, @SDT, @Email, @luong, @ChucVu)";
            object[] parameter = new object[] { maNV, tenNV, diaChi, soDienThoai, email, luong, ChucVu };
            DataProvider.Instance.ExcuteNonQuery(query, parameter);

            // Cập nhật DataGridView
        }
        

        private void sua_rowData()
        {
            DataGridViewRow row = data.SelectedRows[0];
            string maNV = row.Cells["Column2"].Value.ToString();
            string tenNV = tb_name.Text;
            string diaChi = tb_address.Text;
            string soDienThoai = tb_phone.Text;
            string email = tb_Email.Text;
            string ChucVu = tb_chucVu.Text;
            string luong = tb_Luong.Text;
            object[] parameter = new object[] {tenNV, diaChi, soDienThoai, email, ChucVu, luong, maNV };

            string query = "UPDATE NhanVien SET TenNV = @TenNV, DiaChi = @DiaChi, SDT = @SDT, Email = @Email,ChucVu = @ChucVu, Luong = @luong WHERE MaNV = @MaNV";
            DataProvider.Instance.ExcuteNonQuery(query, parameter);
            // Cập nhật DataGridView
        }


        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            Guna2TextBox currentTextBox = sender as Guna2TextBox;

            // Kiểm tra xem Guna2TextBox có trong từ điển không
            if (textBoxWarningPairs.ContainsKey(currentTextBox))
            {
                // Đặt thuộc tính Visible của Guna2Button cảnh báo dựa trên giá trị của Guna2TextBox
                textBoxWarningPairs[currentTextBox].Visible = currentTextBox.Text == "";
            }
        }


        private void btn_filter_Click(object sender, EventArgs e)
        {
            visible_boxFilter();
        }

        private void btn_HFilter_Click(object sender, EventArgs e)
        {
            unVisible_boxFilter();
        }

        private void btn_XNFilter_Click(object sender, EventArgs e)
        {
            unVisible_boxFilter();
            search.PlaceholderText = cbx_filter.Text;
        }

        private void delete_Click(object sender, EventArgs e)
        {
            string MaTK = data.SelectedRows[0].Cells["Column3"].Value.ToString();
            string MaNV = data.SelectedRows[0].Cells["Column2"].Value.ToString();
            object[] parameter_NV = new object[] { MaNV };
            string query_NV = "DELETE FROM NhanVien WHERE MaNv = @MaNV";
            DataProvider.Instance.ExcuteNonQuery(query_NV, parameter_NV);
            if (MaTK != null)
            {
                object[] parameter_TK = new object[] { MaTK };
                string query_TK = "DELETE FROM TaiKhoan WHERE MaTK = @MaTK";
                DataProvider.Instance.ExcuteNonQuery(query_TK, parameter_TK);
            }
            // Cập nhật DataGridView
            Search_input();
        }

        private void tb_Luong_TextChanged(object sender, EventArgs e)
        {
            main.UpdateTextBox(tb_Luong, tb_Luong.Text);
        }

        private void edit_Click(object sender, EventArgs e)
        {
            isEdit = true;
            container_edit_box.Visible = true;
            undisplay_warning();
            unVisible_boxFilter();
            disabeled();

            DataGridViewRow row = data.SelectedRows[0];
            tb_name.Text = row.Cells["Column4"].Value.ToString();
            tb_address.Text = row.Cells["Column5"].Value.ToString();
            tb_phone.Text = row.Cells["Column6"].Value.ToString();
            tb_chucVu.Text = row.Cells["Column7"].Value.ToString();
            tb_Email.Text = row.Cells["Column8"].Value.ToString();
            tb_Luong.Text = row.Cells["Column9"].Value.ToString();

        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            Search_input();
        }
        private void Search_input()
        {
            string str_search = "%" + search.Text + "%";
            string query = "";
            object[] parameter = new object[] { str_search };

            if (cbx_filter.Text == "Tên nhân viên")
                query = "SELECT * FROM NhanVien WHERE TenNV LIKE @str_search";
            else if (cbx_filter.Text == "Địa chỉ")
                query = "SELECT * FROM NhanVien WHERE DiaChi LIKE @str_search";
            else if (cbx_filter.Text == "Chức vụ")
                query = "SELECT * FROM NhanVien WHERE ChucVu LIKE @str_search";
            else
                return;  // Nếu cbx_filter.Text không phải là một trong các giá trị mong đợi, thoát khỏi hàm

            data.DataSource = DataProvider.Instance.ExcuteQuery(query, parameter);

        }

        private void CapTaiKhoan_Click(object sender, EventArgs e)
        {
            string queryCountId = "SELECT COUNT(*) FROM TaiKhoan WHERE MaTK = @MaTK";
            string MaTK = DataProvider.Instance.GenerateId(queryCountId,"TK");
            DataGridViewRow row = data.SelectedRows[0];
            string Email = row.Cells["Column8"].Value.ToString();
            string maNV = row.Cells["Column2"].Value.ToString();
            string TenNV = row.Cells["Column4"].Value.ToString();
            string TenTK = ChuyenChuoi(TenNV) + maNV;
            string MatKhau = RandomString(3);
            string PhanQuyen = "Nhân viên";

            email.Send(Email, "Cửa hàng giày STORESHOE xin chào", "Xin chúc mừng \""+ TenNV + "\" đã chính thức trở thành nhân viên của cửa hàng. Anh/Chị hãy truy cập vào phần mềm của cửa hàng chúng tôi với :" + "\n\nTài khoản: " + TenTK + "\nMật khẩu: " + MatKhau);

            string query_TK = "INSERT INTO TaiKhoan (MaTK, TenTK, MatKhau, PhanQuyen) VALUES (@MaTK, @TenTK, @MatKhau, @PhanQuyen)";
            object[] parameter_TK = new object[] { MaTK, TenTK, MatKhau, PhanQuyen };
            DataProvider.Instance.ExcuteNonQuery(query_TK, parameter_TK);

            object[] parameter_NV = new object[] { MaTK, maNV };
            string query_NV = "UPDATE NhanVien SET MaTK = @MaTK WHERE MaNV = @MaNV";
            DataProvider.Instance.ExcuteNonQuery(query_NV, parameter_NV);

            

            // Cập nhật DataGridView
            Search_input();
        }

        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }


        private string ChuyenChuoi(string input)
        {
            string[] nguyenAmCoDau = { "á", "à", "ả", "ã", "ạ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "đ", "é", "è", "ẻ", "ẽ", "ẹ", "ê", "ế", "ề", "ể", "ễ", "ệ", "í", "ì", "ỉ", "ĩ", "ị", "ó", "ò", "ỏ", "õ", "ọ", "ô", "ố", "ồ", "ổ", "ỗ", "ộ", "ơ", "ớ", "ờ", "ở", "ỡ", "ợ", "ú", "ù", "ủ", "ũ", "ụ", "ư", "ứ", "ừ", "ử", "ữ", "ự", "ý", "ỳ", "ỷ", "ỹ", "ỵ" };
            string[] nguyenAmKhongDau = { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "d", "e", "e", "e", "e", "e", "e", "e", "e", "e", "e", "e", "i", "i", "i", "i", "i", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "u", "u", "u", "u", "u", "u", "u", "u", "u", "u", "u", "y", "y", "y", "y", "y" };

            if (nguyenAmCoDau.Length != nguyenAmKhongDau.Length)
            {
                throw new ArgumentException("nguyenAmCoDau and nguyenAmKhongDau must be the same length");
            }

            StringBuilder sb = new StringBuilder(input);
            for (int i = 0; i < nguyenAmCoDau.Length; i++)
            {
                sb.Replace(nguyenAmCoDau[i], nguyenAmKhongDau[i]);
                sb.Replace(nguyenAmCoDau[i].ToUpper(), nguyenAmKhongDau[i].ToUpper());
            }

            input = sb.ToString();
            string[] words = input.Split(' ');
            sb.Clear();

            for (int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(words[i]))
                {
                    if (i == words.Length - 1)
                    {
                        sb.Append(words[i].ToLower());
                    }
                    else
                    {
                        sb.Append(words[i].Substring(0, 1).ToLower());
                    }
                }
            }

            return sb.ToString();
        }

        private void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String MaNV = data.SelectedRows[0].Cells[1].Value.ToString();
            delete.Enabled = true;
            if (MaNV == "NV1")
            {
                delete.Enabled = false;
            }
        }
    }
}
