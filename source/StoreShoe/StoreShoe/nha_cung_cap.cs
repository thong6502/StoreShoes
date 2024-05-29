using Guna.UI2.WinForms;
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
    
    public partial class nha_cung_cap : Form
    {
        private Dictionary<Guna2TextBox, Guna2Button> textBoxWarningPairs;
        public nha_cung_cap()
        {
            InitializeComponent();
        }

        private void nha_cung_cap_Load(object sender, EventArgs e)
        {
            tbWarningPairs();
            addDataSource();
        }


        // Tạo một từ điển để lưu trữ các cặp Guna2TextBox và Guna2Button cảnh báo
        private void tbWarningPairs()
        {
            textBoxWarningPairs = new Dictionary<Guna2TextBox, Guna2Button>()
            {
                { tb_name, warning1 },
                { tb_address, warning2 },
                { tb_phone, warning3 },

            };
        }

        private void disabeled()
        {
            foreach (Control c in this.Controls)
            {
                // Kiểm tra xem control có phải là edit_box hay không
                if (c.Name != "edit_box")
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

            foreach (Control c in this.edit_box.Controls)
            {
                if (c is Guna2Button && c.Name != "xac_nhan" && c.Name != "huy")
                {
                    c.Visible = false;
                }
            }
        }

        private void clear_content_tb()
        {
            foreach (Control c in this.edit_box.Controls)
            {
                if (c is Guna2TextBox)
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
            string query = "SELECT * FROM NhaCungCap";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query);
            data.DataSource = dt;
        }


        private void add_Click(object sender, EventArgs e)
        {
            isEdit = false;
            edit_box.Visible = true;
            undisplay_warning();
            unVisible_boxFilter();
            disabeled();
        }
        private void huy_Click(object sender, EventArgs e)
        {
            edit_box.Visible = false;
            clear_content_tb();
            undisplay_warning();
            enabeled();
        }

        bool isEdit;
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
                if (isEdit)
                {
                    sua_rowData();
                }
                else
                {
                    them_RowData();
                }
                Search_input();
                edit_box.Visible = false;
                clear_content_tb();
                undisplay_warning();
                enabeled();

            }

            foreach (KeyValuePair<Guna2TextBox, Guna2Button> pair in textBoxWarningPairs)
            {
                pair.Value.Visible = pair.Key.Text == "";
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            // Lấy Guna2TextBox hiện tại
            Guna2TextBox currentTextBox = sender as Guna2TextBox;

            // Kiểm tra xem Guna2TextBox có trong từ điển không
            if (textBoxWarningPairs.ContainsKey(currentTextBox))
            {
                // Đặt thuộc tính Visible của Guna2Button cảnh báo dựa trên giá trị của Guna2TextBox
                textBoxWarningPairs[currentTextBox].Visible = currentTextBox.Text == "";
            }
        }

        private void them_RowData()
        {
            string queryCountId = "SELECT COUNT(*) FROM NhaCungCap WHERE MaNCC = @MaNCC";
            string maNCC = DataProvider.Instance.GenerateId(queryCountId, "NCC");
            string tenNCC = tb_name.Text;
            string diaChi = tb_address.Text;
            string soDienThoai = tb_phone.Text;


            object[] parameter = new object[] { maNCC, tenNCC, diaChi, soDienThoai };
            string query = "INSERT INTO NhaCungCap (MaNCC, TenNCC, DiaChi, SDT) VALUES (@MaNCC, @TenNCC, @DiaChi, @SDT)";
            DataProvider.Instance.ExcuteNonQuery(query, parameter);
            // Cập nhật DataGridView
            addDataSource();
        }

        private void sua_rowData()
        {
            DataGridViewRow row = data.SelectedRows[0];
            string maNCC = row.Cells["Column2"].Value.ToString();
            string tenNCC = tb_name.Text;
            string diaChi = tb_address.Text;
            string soDienThoai = tb_phone.Text;

            object[] parameter = new object[] { tenNCC, diaChi, soDienThoai, maNCC };
            string query = "UPDATE NhaCungCap SET TenNCC = @TenNCC, DiaChi = @DiaChi, SDT = @SDT WHERE MaNCC = @MaNCC";
            DataProvider.Instance.ExcuteNonQuery(query, parameter);
        }
        private void Search_input()
        {
            string str_search = "%" + search.Text + "%";
            string query = "";

            if (cbx_filter.Text == "Tên nhà cung cấp")
                query = "SELECT * FROM NhaCungCap WHERE TenNCC LIKE @str_search";
            else if (cbx_filter.Text == "Địa chỉ")
                query = "SELECT * FROM NhaCungCap WHERE DiaChi LIKE @str_search";
            else
                return; 

            object[] parameter = new object[] { str_search };
            data.DataSource = DataProvider.Instance.ExcuteQuery(query, parameter);
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

        private void search_TextChanged(object sender, EventArgs e)
        {
            Search_input();
        }

        private void edit_Click(object sender, EventArgs e)
        {
            isEdit = true;
            edit_box.Visible = true;
            undisplay_warning();
            unVisible_boxFilter();
            disabeled();

            DataGridViewRow row = data.SelectedRows[0];
            tb_name.Text = row.Cells["Column3"].Value.ToString();
            tb_address.Text = row.Cells["Column4"].Value.ToString();
            tb_phone.Text = row.Cells["Column5"].Value.ToString();
            
        }

        private void delete_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (data.SelectedRows.Count > 0)
            {
                string Id = data.SelectedRows[0].Cells["Column2"].Value.ToString();
                string query = "DELETE FROM NhaCungCap WHERE MaNCC = @MaNCC";
                object[] parameter = new object[] { Id };
                DataProvider.Instance.ExcuteQuery(query, parameter);
                // Cập nhật DataGridView
                Search_input();
            }
        }
    }
}
