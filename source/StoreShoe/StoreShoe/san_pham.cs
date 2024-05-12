using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shoe_store_manager
{
    public partial class san_pham : Form
    {
        private Dictionary<Guna2TextBox, Guna2Button> textBoxWarningPairs;
        private DataTable DataTb_UIuser = new DataTable();
        public san_pham()
        {
            InitializeComponent();
        }

        private void san_pham_Load(object sender, EventArgs e)
        {
            addData_cbx();
            addColumnsTb();
            string query = "SELECT * FROM SanPhamGiay";
            addDataTableUI(query);
            addDataSource();
            tbWarningPairs();
        }

        private void addColumnsTb()
        {

            DataTb_UIuser.Columns.Add("MaSPG", typeof(string));
            DataTb_UIuser.Columns.Add("Img", typeof(Image));
            DataTb_UIuser.Columns.Add("TenGiay", typeof(string));
            DataTb_UIuser.Columns.Add("TonKho", typeof(string));
            DataTb_UIuser.Columns.Add("GiaMua", typeof(string));
            DataTb_UIuser.Columns.Add("GiaBan", typeof(string));

            
        }
        private void addDataTableUI(string query, Object[] parameter = null)
        {
            DataTable dt = DataProvider.Instance.ExcuteQuery(query,parameter);
            DataTb_UIuser.Clear();

            foreach (DataRow row in dt.Rows)
            {
                DataRow newRow = DataTb_UIuser.NewRow();

                newRow["MaSPG"] = row["MaSPG"];
                Image img = Image.FromFile(row["img"].ToString());
                newRow["Img"] = new Bitmap(img);
                img.Dispose();
                newRow["TenGiay"] = row["TenGiay"];
                newRow["TonKho"] = row["TonKho"];
                newRow["GiaMua"] = row["GiaMua"];
                newRow["GiaBan"] = row["GiaBan"];

                DataTb_UIuser.Rows.Add(newRow);
            }
            
        }


        // Tạo một từ điển để lưu trữ các cặp Guna2TextBox và Guna2Button cảnh báo
        private void tbWarningPairs()
        {
            textBoxWarningPairs = new Dictionary<Guna2TextBox, Guna2Button>()
            {
                { tb_name, warning1 }

            };
        }

        private void disabeled()
        {
            foreach (Control c in this.Controls)
            {
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

            warning1.Visible = false;
            warning2.Visible = false;
            warning3.Visible = false;
        }




        private void addDataSource()
        {
            data.DataSource = DataTb_UIuser;
        }
        private void xac_nhan_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem tất cả các nút cảnh báo có đang hiển thị hay không
            bool allWarningsHidden = true;
            foreach (KeyValuePair<Guna2TextBox, Guna2Button> pair in textBoxWarningPairs)
            {
                if (pair.Value.Visible)
                {
                    allWarningsHidden = false;
                    break;
                }
            }
            if (allWarningsHidden && cbx_LoaiGiay.Text != "" && cbx_LoaiGiay.Text != "")
            {
                sua_rowData();
                colseEditBox();
                Search_input();
                addDataSource();
            }
            foreach (KeyValuePair<Guna2TextBox, Guna2Button> pair in textBoxWarningPairs)
            {
                pair.Value.Visible = pair.Key.Text == "";
            }

        }
        private void sua_rowData()
        {

            DataGridViewRow row = data.SelectedRows[0];
            string MaSPG = row.Cells["MaSPG"].Value.ToString();
            string TenSP = tb_name.Text;
            string Giaban = tb_giaBan.Text;

            string query_Update = "UPDATE SanPhamGiay SET TenGiay = @TenGiay, Img = @Img, GiaBan = @GiaBan WHERE MaSPG = @MaSPG";
            object[] parameter = new object[] { TenSP,fileName,Giaban, MaSPG };
            DataProvider.Instance.ExcuteNonQuery(query_Update, parameter);

        }

        private void huy_Click(object sender, EventArgs e)
        {
            colseEditBox();
        }


        private void openEditBox()
        {
            container_edit_box.Visible = true;
            undisplay_warning();
            disabeled();

        }
        private void colseEditBox()
        {
            container_edit_box.Visible = false;
            undisplay_warning();
            nonPic();
            enabeled();
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


        private void addData_cbx()
        {
            string query_LG = "SELECT * FROM LoaiGiay";
            cbx_LoaiGiay.DataSource = DataProvider.Instance.ExcuteQuery(query_LG);
            cbx_LoaiGiay.DisplayMember = "Ten";


            string query_NCC = "SELECT * FROM NhaCungCap";
            cbx_NCC.DataSource = DataProvider.Instance.ExcuteQuery(query_NCC);
            cbx_NCC.DisplayMember = "TenNCC";

            cbx_LoaiGiay.StartIndex = -1;
            cbx_NCC.StartIndex = -1;

        }


        string fileName = "";
        void LoadImage(ref string filename)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filename = ofd.FileName;
            }
        }
        private void pic_img_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LoadImage(ref fileName);
            addPic(fileName);
        }

        private void addPic(string fileName)
        {

            if (File.Exists(fileName))
            {
                try
                {
                    // Kiểm tra xem tệp có thể được tải như một hình ảnh hay không
                    using (Image img = Image.FromFile(fileName))
                    {
                        pic_img.Image = new Bitmap(img);
                    }
                }
                catch (OutOfMemoryException)
                {
                    // Nếu tệp không phải là hình ảnh hợp lệ, Image.FromFile sẽ ném ra một ngoại lệ OutOfMemoryException
                    MessageBox.Show("Tệp không phải là hình ảnh hợp lệ: " + fileName);
                    fileName = "";
                }
            }
            else
            {
                MessageBox.Show("Tệp tin không tồn tại: " + fileName);
                fileName = "";
            }
        }
        private void nonPic()
        {
            pic_img.Image = null;
        }



        private void tb_giaBan_TextChanged(object sender, EventArgs e)
        {
            main.UpdateTextBox(tb_giaBan, tb_giaBan.Text);
        }

        private void edit_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = data.SelectedRows[0];
            string id = row.Cells["MaSPG"].Value.ToString();

            string query_SPG = "SELECT * FROM SanPhamGiay WHERE MaSPG = @MaSPG";
            string query_Size = "SELECT * FROM SizeGiay WHERE MaSPG = @MaSPG";
            object[] parameter_SPG = new object[] { id };
            object[] parameter_Size = new object[] { id };
            DataTable dt_SPG = DataProvider.Instance.ExcuteQuery(query_SPG, parameter_SPG);
            DataTable dt_Size = DataProvider.Instance.ExcuteQuery(query_Size, parameter_Size);

            tb_name.Text = row.Cells["TenGiay"].Value.ToString();
            string path = dt_SPG.Rows[0]["Img"].ToString();
            addPic(path);
            fileName = path;

            string Id_LG = dt_SPG.Rows[0]["MaLG"].ToString();
            string Id_NCC = dt_SPG.Rows[0]["MaNCC"].ToString();
            string query_LG = "SELECT Ten FROM LoaiGiay WHERE MaLG = @MaLG";
            string query_NCC = "SELECT TenNCC FROM NhaCungCap WHERE MaNCC = @MaNCC";
            object[] parameter_LG = new object[] { Id_LG };
            object[] parameter_NCC = new object[] { Id_NCC };

            string LoaiGiay = DataProvider.Instance.ExcuteQuery(query_LG, parameter_LG).Rows[0]["Ten"].ToString();
            string NCC = DataProvider.Instance.ExcuteQuery(query_NCC, parameter_NCC).Rows[0]["TenNCC"].ToString();
            cbx_LoaiGiay.Text = LoaiGiay;
            cbx_NCC.Text = NCC;

            int s38 = Convert.ToInt32(dt_Size.Rows[0]["SoLuong"]);
            int s39 = Convert.ToInt32(dt_Size.Rows[1]["SoLuong"]);
            int s40 = Convert.ToInt32(dt_Size.Rows[2]["SoLuong"]);
            int s41 = Convert.ToInt32(dt_Size.Rows[3]["SoLuong"]);
            int s42 = Convert.ToInt32(dt_Size.Rows[4]["SoLuong"]);
            int s43 = Convert.ToInt32(dt_Size.Rows[5]["SoLuong"]);

            nud_s38.Value = s38;
            nud_s39.Value = s39;
            nud_s40.Value = s40;
            nud_s41.Value = s41;
            nud_s42.Value = s42;
            nud_s43.Value = s43;


            tb_giaMua.Text = dt_SPG.Rows[0]["GiaMua"].ToString();
            tb_giaBan.Text = dt_SPG.Rows[0]["GiaBan"].ToString();



            openEditBox();
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            Search_input();
        }
        private void Search_input()
        {
            string str_search = "%" + search.Text + "%";
            string query = "SELECT * FROM SanPhamGiay WHERE TenGiay LIKE @str_search";
            object[] parameter = new object[] { str_search };
            addDataTableUI(query, parameter);
            addDataSource();
        }
    }
}
