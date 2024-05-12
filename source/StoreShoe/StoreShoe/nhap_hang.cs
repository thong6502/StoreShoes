using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI.WinForms;
using Guna.UI2.WinForms;
using System.Drawing.Imaging;
using Microsoft.VisualBasic.Logging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace shoe_store_manager
{
    public partial class nhap_hang : Form
    {
        private Dictionary<Guna2TextBox, Guna2Button> textBoxWarningPairs;
        private int TotalPrice = 0;
        private DataTable DataTb_SPG;
        private DataTable DataTb_Previous_Size;
        private DataTable DataTb_Curent_Size;
        private DataTable DataTb_CTHDM;


        private DataTable DataTb_UIuser = new DataTable();
        private string MaHDM = "";

        private bool isEdit = false;
        public nhap_hang()
        {
            InitializeComponent();
        }

        private void nhap_hang_Load(object sender, EventArgs e)
        {
            reset();
            tbWarningPairs();

            warning2.Visible = true;
            warning3.Visible = true;

            
        }

        private void CreateId()
        {
            string queryCountId = "SELECT COUNT(*) FROM HoaDonMua WHERE MaHDM = @MaHDM";
            MaHDM = DataProvider.Instance.GenerateId(queryCountId, "HDM");
        }


        private void addValuesName()
        {
            string query = "SELECT TenGiay FROM SanPhamGiay";
            tb_name.Values = DataProvider.Instance.GetDataColumn(query);
        }
        private void addData_cbx()
        {
            string query_LG ="SELECT * FROM LoaiGiay";
            cbx_LoaiGiay.DataSource = DataProvider.Instance.ExcuteQuery(query_LG);
            cbx_LoaiGiay.DisplayMember = "Ten";
            

            string query_NCC = "SELECT * FROM NhaCungCap";
            cbx_NCC.DataSource = DataProvider.Instance.ExcuteQuery(query_NCC);
            cbx_NCC.DisplayMember = "TenNCC";

            cbx_LoaiGiay.StartIndex = -1;
            cbx_NCC.StartIndex = -1;

        }
        private void addDataTable()
        {

            string query_SPG = "SELECT * FROM SanPhamGiay";
            DataTb_SPG = DataProvider.Instance.ExcuteQuery(query_SPG);

            string query_Size = "SELECT * FROM SizeGiay";
            DataTb_Previous_Size = DataProvider.Instance.ExcuteQuery(query_Size);
            DataTb_Curent_Size = DataProvider.Instance.ExcuteQuery(query_Size);
            DataTb_Curent_Size.Clear();

            string query_CTHDM = "SELECT * FROM ChiTietHoaDonMua";
            DataTb_CTHDM = DataProvider.Instance.ExcuteQuery(query_CTHDM);

            DataTb_UIuser.Clear();
            
            if (DataTb_UIuser.Columns.Count == 0)
            {
                DataTb_UIuser.Columns.Add("MaSPG", typeof(string));
                DataTb_UIuser.Columns.Add("Img", typeof(Image));
                DataTb_UIuser.Columns.Add("TenGiay", typeof(string));
                DataTb_UIuser.Columns.Add("SoLuong", typeof(int));
                DataTb_UIuser.Columns.Add("GiaMua", typeof(string));
            }
            


        }

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

        private void clear_content_tb()
        {
            foreach (Control c in this.edit_box.Controls)
            {
                if (c is Guna2TextBox)
                {
                    c.Text = "";
                }
            }
            nud_s38.Value = 0;
            nud_s39.Value = 0;
            nud_s40.Value = 0;
            nud_s41.Value = 0;
            nud_s42.Value = 0;
            nud_s43.Value = 0;
            cbx_LoaiGiay.SelectedIndex = -1;
            cbx_NCC.SelectedIndex = -1;
        }

        private void add_Click(object sender, EventArgs e)
        {
            isEdit = false;
            eventEnabled = true;
            addValuesName();
            openEditBox();
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
            clear_content_tb();
            undisplay_warning();
            nonPic();
            enabeled();
        }


        private void tb_giaMua_TextChanged(object sender, EventArgs e)
        {
            main.UpdateTextBox(tb_giaMua, tb_giaMua.Text);
        }


        private void tb_giaBan_TextChanged(object sender, EventArgs e)
        {
            main.UpdateTextBox(tb_giaBan, tb_giaBan.Text);
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


        private void huy_Click(object sender, EventArgs e)
        {
            colseEditBox();
        }

        private void edit_Click(object sender, EventArgs e)
        {
            isEdit = true;
            eventEnabled = false;
            tb_name.Values = null;
            DataGridViewRow row = data.SelectedRows[0];
            string id = row.Cells["MaSPG"].Value.ToString();
            DataRow[] foundRows_SPG = DataTb_SPG.Select("MaSPG = '" + id + "'");
            DataRow[] foundRows_Size = DataTb_Curent_Size.Select("MaSPG = '" + id + "'");

            tb_name.Text = row.Cells["TenGiay"].Value.ToString();
            string path = foundRows_SPG[0]["Img"].ToString();
            addPic(path);
            fileName = path;

            string Id_LG = foundRows_SPG[0]["MaLG"].ToString();
            string Id_NCC = foundRows_SPG[0]["MaNCC"].ToString();
            string query_LG = "SELECT Ten FROM LoaiGiay WHERE MaLG = @MaLG";
            string query_NCC = "SELECT TenNCC FROM NhaCungCap WHERE MaNCC = @MaNCC";
            object[] parameter_LG = new object[] { Id_LG };
            object[] parameter_NCC = new object[] { Id_NCC };

            string LoaiGiay = DataProvider.Instance.ExcuteQuery(query_LG, parameter_LG).Rows[0]["Ten"].ToString();
            string NCC = DataProvider.Instance.ExcuteQuery(query_NCC, parameter_NCC).Rows[0]["TenNCC"].ToString();
            cbx_LoaiGiay.Text = LoaiGiay;
            cbx_NCC.Text = NCC;

            int s38 = Convert.ToInt32(foundRows_Size[0]["SoLuong"]);
            int s39 = Convert.ToInt32(foundRows_Size[1]["SoLuong"]);
            int s40 = Convert.ToInt32(foundRows_Size[2]["SoLuong"]);
            int s41 = Convert.ToInt32(foundRows_Size[3]["SoLuong"]);
            int s42 = Convert.ToInt32(foundRows_Size[4]["SoLuong"]);
            int s43 = Convert.ToInt32(foundRows_Size[5]["SoLuong"]);
            int SoLuong = s38 + s39 + s40 + s41 + s42 + s43;

            nud_s38.Value = s38;
            nud_s39.Value = s39;
            nud_s40.Value = s40;
            nud_s41.Value = s41;
            nud_s42.Value = s42;
            nud_s43.Value = s43;


            tb_giaMua.Text = foundRows_SPG[0]["GiaMua"].ToString();
            tb_giaBan.Text = foundRows_SPG[0]["GiaBan"].ToString();

            TotalPrice -= main.ConvertPriceStringToInt(tb_giaMua.Text) * SoLuong;

            openEditBox();
        }

        private void delete_Click(object sender, EventArgs e)
        {

            DataGridViewRow row = data.SelectedRows[0];
            string id = row.Cells["MaSPG"].Value.ToString();
            DataRow[] foundRows_SPG = DataTb_SPG.Select("MaSPG = '" + id + "'");
            DataRow[] foundRows_Size = DataTb_Curent_Size.Select("MaSPG = '" + id + "'");

            int s38 = Convert.ToInt32(foundRows_Size[0]["SoLuong"]);
            int s39 = Convert.ToInt32(foundRows_Size[1]["SoLuong"]);
            int s40 = Convert.ToInt32(foundRows_Size[2]["SoLuong"]);
            int s41 = Convert.ToInt32(foundRows_Size[3]["SoLuong"]);
            int s42 = Convert.ToInt32(foundRows_Size[4]["SoLuong"]);
            int s43 = Convert.ToInt32(foundRows_Size[5]["SoLuong"]);
            int SoLuong = s38 + s39 + s40 + s41 + s42 + s43;

            nud_s38.Value = s38;
            nud_s39.Value = s39;
            nud_s40.Value = s40;
            nud_s41.Value = s41;
            nud_s42.Value = s42;
            nud_s43.Value = s43;


            tb_giaMua.Text = foundRows_SPG[0]["GiaMua"].ToString();

            TotalPrice -= main.ConvertPriceStringToInt(tb_giaMua.Text) * SoLuong;
            UpdateTotalPrice(TotalPrice);

            DataSet.Instance.DeleteRowFromDataTable(DataTb_Curent_Size, "MaSPG", id);
            DataSet.Instance.DeleteRowFromDataTable(DataTb_CTHDM, "MaSPG", id);
            DataSet.Instance.DeleteRowFromDataTable(DataTb_UIuser, "MaSPG", id);

            string query = "SELECT MaSPG FROM SanPhamGiay WHERE MaSPG = @MaSPG";
            object[] parameter = new object[] { id };
            if(DataProvider.Instance.ExcuteQuery(query,parameter).Rows.Count == 0)
            {
                DataSet.Instance.DeleteRowFromDataTable(DataTb_SPG, "MaSPG", id);
            }

            addDataSource();
            

        }

        private bool eventEnabled = true;
        private void tb_name_TextChanged(object sender, EventArgs e)
        {
            warning1.Visible = tb_name.Text == "";
            if (eventEnabled == true)
            {
                int index = Array.IndexOf(tb_name.Values, tb_name.Text);

                if (index != -1)
                {
                    isEdit = false;
                    string str_search = tb_name.Text;
                    string query = "SELECT SanPhamGiay.Img, LoaiGiay.Ten, SanPhamGiay.TenGiay, NhaCungCap.TenNCC, SanPhamGiay.GiaMua, SanPhamGiay.GiaBan FROM SanPhamGiay JOIN LoaiGiay ON SanPhamGiay.MaLG = LoaiGiay.MaLG JOIN NhaCungCap ON SanPhamGiay.MaNCC = NhaCungCap.MaNCC JOIN ChiTietHoaDonMua ON SanPhamGiay.MaSPG = ChiTietHoaDonMua.MaSPG WHERE SanPhamGiay.TenGiay = @TenGiay ";
                    object[] parameter = new object[] { str_search };

                    DataTable dt = DataProvider.Instance.ExcuteQuery(query, parameter);

                    string fileNameDB = dt.Rows[0]["Img"].ToString();
                    string LoaiGiay = dt.Rows[0]["Ten"].ToString();
                    string NCC = dt.Rows[0]["TenNCC"].ToString();
                    string GiaMua = dt.Rows[0]["GiaMua"].ToString();
                    string GiaBan = dt.Rows[0]["GiaBan"].ToString();

                    addPic(fileNameDB);
                    fileName = fileNameDB;

                    cbx_LoaiGiay.Text = LoaiGiay;
                    cbx_NCC.Text = NCC;

                    tb_giaMua.Text = GiaMua;
                    tb_giaBan.Text = GiaBan;

                }
                else
                {
                    nonPic();
                    cbx_LoaiGiay.SelectedIndex = -1;
                    cbx_NCC.SelectedIndex = -1;
                    tb_giaMua.Text = "";
                    tb_giaBan.Text = "";
                }
            }

        }


        private void xac_nhan_Click(object sender, EventArgs e)
        {

            // Kiểm tra xem tất cả các nút cảnh báo có đang hiển thị hay không
            bool allWarningsHidden = true;
            foreach (KeyValuePair<Guna2TextBox, Guna2Button> pair in textBoxWarningPairs)
            {
                if (pair.Key.Text == "" || cbx_LoaiGiay.Text == "" || cbx_NCC.Text == "")
                {
                    allWarningsHidden = false;
                    break;
                }
            }
            if (allWarningsHidden && cbx_LoaiGiay.Text != "" && cbx_LoaiGiay.Text != "")
            {
                if (isEdit)
                {
                    sua_rowData();
                }
                else
                {
                    them_RowData();
                }
                colseEditBox();
                addDataSource();
                UpdateTotalPrice(TotalPrice);
            }
            foreach (KeyValuePair<Guna2TextBox, Guna2Button> pair in textBoxWarningPairs)
            {
                pair.Value.Visible = pair.Key.Text == "";
            }
            warning2.Visible = cbx_LoaiGiay.Text == "";
            warning3.Visible = cbx_NCC.Text == "";
        }
        

        private void them_RowData()
        {
            string MaSPG = findIdInDataTB(tb_name.Text);
            string TenSP = tb_name.Text;
            Image img = null;
            if (fileName != "")
            {
                img = Image.FromFile(fileName);
            }

            string query_LG = "SELECT MaLG FROM LoaiGiay WHERE Ten = @ten";
            string query_NCC = "SELECT MaNCC FROM NhaCungCap WHERE TenNCC = @tenNCC";
            string LoaiGiay = cbx_LoaiGiay.Text;
            string NCC = cbx_NCC.Text;
            object[] parameter_LG = new object[] { LoaiGiay };
            object[] parameter_NCC = new object[] { NCC };
            string Id_LoaiGiay = DataProvider.Instance.GetIdByName(query_LG, parameter_LG);
            string Id_NCC = DataProvider.Instance.GetIdByName(query_NCC, parameter_NCC);


            int s38 = Convert.ToInt32(nud_s38.Value);
            int s39 = Convert.ToInt32(nud_s39.Value);
            int s40 = Convert.ToInt32(nud_s40.Value);
            int s41 = Convert.ToInt32(nud_s41.Value);
            int s42 = Convert.ToInt32(nud_s42.Value);
            int s43 = Convert.ToInt32(nud_s43.Value);
            int SoLuong = s38 + s39 + s40 + s41 + s42 + s43;
            string giaMua = tb_giaMua.Text;
            string giaBan = tb_giaBan.Text;

            int price = main.ConvertPriceStringToInt(giaMua) * SoLuong;
            TotalPrice += price;

            

            
            if (MaSPG == "")
            {
                MaSPG = DataSet.Instance.GenerateIdDataTabble(DataTb_SPG, "SPG", "MaSPG");

                object[] values_SPG = new object[] { MaSPG, Id_LoaiGiay, Id_NCC, TenSP, fileName, SoLuong, giaMua, giaBan };
                object[] values_Size38 = new object[] { MaSPG, 38, s38 };
                object[] values_Size39 = new object[] { MaSPG, 39, s39 };
                object[] values_Size40 = new object[] { MaSPG, 40, s40 };
                object[] values_Size41 = new object[] { MaSPG, 41, s41 };
                object[] values_Size42 = new object[] { MaSPG, 42, s42 };
                object[] values_Size43 = new object[] { MaSPG, 43, s43 };
                object[] values_CTHDM = new object[] { MaHDM, MaSPG, SoLuong };

                DataSet.Instance.AddRowToDataTable(DataTb_SPG, values_SPG);
                DataSet.Instance.AddRowToDataTable(DataTb_Curent_Size, values_Size38);
                DataSet.Instance.AddRowToDataTable(DataTb_Curent_Size, values_Size39);
                DataSet.Instance.AddRowToDataTable(DataTb_Curent_Size, values_Size40);
                DataSet.Instance.AddRowToDataTable(DataTb_Curent_Size, values_Size41);
                DataSet.Instance.AddRowToDataTable(DataTb_Curent_Size, values_Size42);
                DataSet.Instance.AddRowToDataTable(DataTb_Curent_Size, values_Size43);
                DataSet.Instance.AddRowToDataTable(DataTb_CTHDM, values_CTHDM);

            }else
            {
                object[] values_SPG = new object[] { MaSPG, Id_LoaiGiay, Id_NCC, TenSP, fileName, SoLuong, giaMua, giaBan };
                object[] values_Size38 = new object[] { MaSPG, 38, s38 };
                object[] values_Size39 = new object[] { MaSPG, 39, s39 };
                object[] values_Size40 = new object[] { MaSPG, 40, s40 };
                object[] values_Size41 = new object[] { MaSPG, 41, s41 };
                object[] values_Size42 = new object[] { MaSPG, 42, s42 };
                object[] values_Size43 = new object[] { MaSPG, 43, s43 };
                object[] values_CTHDM = new object[] { MaHDM, MaSPG, SoLuong };

                DataSet.Instance.UpdateRowInDataTable(DataTb_SPG,"MaSPG",MaSPG, values_SPG);
                DataSet.Instance.AddRowToDataTable(DataTb_Curent_Size, values_Size38);
                DataSet.Instance.AddRowToDataTable(DataTb_Curent_Size, values_Size39);
                DataSet.Instance.AddRowToDataTable(DataTb_Curent_Size, values_Size40);
                DataSet.Instance.AddRowToDataTable(DataTb_Curent_Size, values_Size41);
                DataSet.Instance.AddRowToDataTable(DataTb_Curent_Size, values_Size42);
                DataSet.Instance.AddRowToDataTable(DataTb_Curent_Size, values_Size43);
                DataSet.Instance.AddRowToDataTable(DataTb_CTHDM, values_CTHDM);

            }
            object[] values_UIuser = new object[] { MaSPG, img, TenSP, SoLuong, giaMua };
            DataSet.Instance.AddRowToDataTable(DataTb_UIuser, values_UIuser);

        }

        private void sua_rowData()
        {
            DataGridViewRow row = data.SelectedRows[0];
            string MaSPG = row.Cells["MaSPG"].Value.ToString();
            DataRow[] foundRows_Size = DataTb_Curent_Size.Select("MaSPG = '" + MaSPG + "'");

            string TenSP = tb_name.Text;
            Image img = null;
            if (fileName != "")
            {
                img = Image.FromFile(fileName);
            }


            string query_LG = "SELECT MaLG FROM LoaiGiay WHERE Ten = @ten";
            string query_NCC = "SELECT MaNCC FROM NhaCungCap WHERE TenNCC = @tenNCC";
            string LoaiGiay = cbx_LoaiGiay.Text;
            string NCC = cbx_NCC.Text;
            object[] parameter_LG = new object[] { LoaiGiay };
            object[] parameter_NCC = new object[] { NCC };
            string Id_LoaiGiay = DataProvider.Instance.GetIdByName(query_LG, parameter_LG);
            string Id_NCC = DataProvider.Instance.GetIdByName(query_NCC, parameter_NCC);

            int s38 = Convert.ToInt32(nud_s38.Value);
            int s39 = Convert.ToInt32(nud_s39.Value);
            int s40 = Convert.ToInt32(nud_s40.Value);
            int s41 = Convert.ToInt32(nud_s41.Value);
            int s42 = Convert.ToInt32(nud_s42.Value);
            int s43 = Convert.ToInt32(nud_s43.Value);

            int SoLuong = s38 + s39 + s40 + s41 + s42 + s43;
            string giaMua = tb_giaMua.Text;
            string giaBan = tb_giaBan.Text;

            int price = main.ConvertPriceStringToInt(giaMua) * SoLuong;
            TotalPrice += price;

            object[] values_SPG = new object[] { MaSPG, Id_LoaiGiay, Id_NCC, TenSP, fileName, SoLuong, giaMua, giaBan };

            object[] values_CTHDM = new object[] { MaHDM, MaSPG, SoLuong };

            DataSet.Instance.UpdateRowInDataTable(DataTb_SPG, "MaSPG", MaSPG, values_SPG);
            foundRows_Size[0]["SoLuong"] = s38;
            foundRows_Size[1]["SoLuong"] = s39;
            foundRows_Size[2]["SoLuong"] = s40;
            foundRows_Size[3]["SoLuong"] = s41;
            foundRows_Size[4]["SoLuong"] = s42;
            foundRows_Size[5]["SoLuong"] = s43;
            DataSet.Instance.UpdateRowInDataTable(DataTb_CTHDM, "MaSPG", MaSPG, values_CTHDM);



            object[] values_UIuser = new object[] { MaSPG, img, TenSP, SoLuong, giaMua };
            DataSet.Instance.UpdateRowInDataTable(DataTb_UIuser, "MaSPG", MaSPG, values_UIuser);
        }


        private void addDataSource()
        {
            data.DataSource = DataTb_UIuser;
        }

        private void UpdateTotalPrice(int price)
        {
            lb_totalPrice.Text = price.ToString("#,##0") + " đ";
        }

        private string findIdInDataTB(string TenSPG)
        {
            string Id = "";
            Id = DataSet.Instance.findIdInDataTB(DataTb_SPG, "TenGiay", TenSPG, "MaSPG");

            return Id;
        }

        private void cbx_LoaiGiay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_LoaiGiay.Text != "") warning2.Visible = false;
        }

        private void ThanhToan_Click(object sender, EventArgs e)
        {

            string query = "INSERT INTO HoaDonMua (MaHDM, MaNV, TongTien) VALUES (@MaHDM, @MaNV, @TongTien)";
            string str_TotalPrice = main.ConvertIntPriceToString(TotalPrice);
            object[] parameter = new object[] { MaHDM, GlobalVariables.MaNV, str_TotalPrice };
            DataProvider.Instance.ExcuteNonQuery(query, parameter);

            

            string SelectQuery_SPG = "SELECT * FROM SanPhamGiay";
            string SelectQuery_CTHDM = "SELECT * FROM ChiTietHoaDonMua";
            string SelectQuery_Size = "SELECT * FROM SizeGiay";




            DataSet.Instance.UpdateAndMergeDataTables(DataTb_Previous_Size, DataTb_Curent_Size, "+");

            foreach(DataRow row_SPG in DataTb_SPG.Rows)
            {
                int TonKho = 0;
                foreach (DataRow row_result in DataTb_Previous_Size.Rows)
                {
                    if (row_result["MaSPG"].ToString() == row_SPG["MaSPG"].ToString())
                    {
                        TonKho += (int)row_result["SoLuong"];
                    }
                }
                row_SPG["TonKho"] = TonKho;
            }

            DataProvider.Instance.UpdateTableInDatabase(DataTb_SPG, SelectQuery_SPG);
            DataProvider.Instance.UpdateTableInDatabase(DataTb_Previous_Size, SelectQuery_Size);
            DataProvider.Instance.UpdateTableInDatabase(DataTb_CTHDM, SelectQuery_CTHDM);



            reset();
        }

        private void cbx_NCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_NCC.Text != "") warning3.Visible = false;
        }

        private void huy_data_Click(object sender, EventArgs e)
        {
            reset();
        }
        private void reset()
        {
            TotalPrice = 0;
            UpdateTotalPrice(TotalPrice);
            CreateId();
            addData_cbx();
            addDataTable();
            addDataSource();
        }
    }
}
