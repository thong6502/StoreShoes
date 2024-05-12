using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Threading;
using System.Media;
using System.Data.SqlTypes;

namespace shoe_store_manager
{
    public partial class ban_hang : Form
    {
        private int TotalPrice = 0;
        private DataTable DataTb_SPG;
        private DataTable DataTb_Previous_Size;
        private DataTable DataTb_Curent_Size;
        private DataTable DataTb_CTHDB;


        private DataTable DataTb_UIuser = new DataTable();
        private string MaHDB = "";

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

        public ban_hang()
        {
            InitializeComponent();
        }
        
        private void ban_hang_Load(object sender, EventArgs e)
        {
            InitializeCamera();
            addDataTable();
            CreateId();
        }
        private void addDataTable()
        {
            string query_SPG = "SELECT * FROM SanPhamGiay";
            DataTb_SPG = DataProvider.Instance.ExcuteQuery(query_SPG);

            string query_Size = "SELECT * FROM SizeGiay";
            DataTb_Previous_Size = DataProvider.Instance.ExcuteQuery(query_Size);
            DataTb_Curent_Size = DataProvider.Instance.ExcuteQuery(query_Size);
            DataTb_Curent_Size.Clear();

            string query_CTHDB = "SELECT * FROM ChiTietHoaDonBan";
            DataTb_CTHDB = DataProvider.Instance.ExcuteQuery(query_CTHDB);

            DataTb_UIuser.Clear();

            if (DataTb_UIuser.Columns.Count == 0)
            {
                DataTb_UIuser.Columns.Add("MaSPG", typeof(string));
                DataTb_UIuser.Columns.Add("Img", typeof(Image));
                DataTb_UIuser.Columns.Add("TenGiay", typeof(string));
                DataTb_UIuser.Columns.Add("SizeGiay", typeof(int));
                DataTb_UIuser.Columns.Add("SoLuong", typeof(int));
                DataTb_UIuser.Columns.Add("GiaBan", typeof(string));
                DataTb_UIuser.Columns.Add("ThanhTien", typeof(string));
            }

        }
        private void addDataSource()
        {
            data.DataSource = DataTb_UIuser;
        }
        private void CreateId()
        {
            string queryCountId = "SELECT COUNT(*) FROM HoaDonBan WHERE MaHDB = @MaHDB";
            MaHDB = DataProvider.Instance.GenerateId(queryCountId, "HDB");
        }
        private void InitializeCamera()
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in filterInfoCollection)
                cboCamera.Items.Add(Device.Name);
            if (cboCamera.Items.Count > 0)
            {
                cboCamera.SelectedIndex = 0;
                videoCaptureDevice = new VideoCaptureDevice();
            }
        }
        CancellationTokenSource cancellationToken;

        private void btn_scan_Click(object sender, EventArgs e)
        {
            if (btn_scan.Text == "Bắt đầu")
            {
                videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboCamera.SelectedIndex].MonikerString);
                videoCaptureDevice.NewFrame += FinalFrame_NewFrame;
                videoCaptureDevice.Start();
                cancellationToken = new CancellationTokenSource();
                var sourcetoken = cancellationToken.Token;
                onStartScan(sourcetoken);
                btn_scan.Text = "Dừng lại!";
            }
            else
            {
                btn_scan.Text = "Bắt đầu";
                cancellationToken.Cancel();
                if (videoCaptureDevice != null)
                    if (videoCaptureDevice.IsRunning == true)
                        videoCaptureDevice.Stop();
            }

        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        public void onStartScan(CancellationToken sourcetoken)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                while (true)
                {
                    if (sourcetoken.IsCancellationRequested)
                    {
                        return;
                    }
                    Thread.Sleep(50);
                    BarcodeReader Reader = new BarcodeReader();
                    pictureBox1.BeginInvoke(new Action(() =>
                    {
                        if (pictureBox1.Image != null)
                        {
                            try
                            {
                                var results = Reader.DecodeMultiple((Bitmap)pictureBox1.Image);
                                if (results != null)
                                {
                                    foreach (Result result in results)
                                    {
                                        lbl_result.Text = result.ToString();

                                        SystemSounds.Beep.Play();
                                        if (result.ResultPoints.Length > 0)
                                        {
                                            var offsetX = pictureBox1.SizeMode == PictureBoxSizeMode.Zoom
                                               ? (pictureBox1.Width - pictureBox1.Image.Width) / 2 :
                                               0;
                                            var offsetY = pictureBox1.SizeMode == PictureBoxSizeMode.Zoom
                                               ? (pictureBox1.Height - pictureBox1.Image.Height) / 2 :
                                               0;
                                            var rect = new Rectangle((int)result.ResultPoints[0].X + offsetX, (int)result.ResultPoints[0].Y + offsetY, 1, 1);
                                            foreach (var point in result.ResultPoints)
                                            {
                                                if (point.X + offsetX < rect.Left)
                                                    rect = new Rectangle((int)point.X + offsetX, rect.Y, rect.Width + rect.X - (int)point.X - offsetX, rect.Height);
                                                if (point.X + offsetX > rect.Right)
                                                    rect = new Rectangle(rect.X, rect.Y, rect.Width + (int)point.X - (rect.X - offsetX), rect.Height);
                                                if (point.Y + offsetY < rect.Top)
                                                    rect = new Rectangle(rect.X, (int)point.Y + offsetY, rect.Width, rect.Height + rect.Y - (int)point.Y - offsetY);
                                                if (point.Y + offsetY > rect.Bottom)
                                                    rect = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height + (int)point.Y - (rect.Y - offsetY));
                                            }
                                            using (var g = pictureBox1.CreateGraphics())
                                            {
                                                using (Pen pen = new Pen(Color.Green, 5))
                                                {
                                                    g.DrawRectangle(pen, rect);

                                                    pen.Color = Color.Yellow;
                                                    pen.DashPattern = new float[] { 5, 5 };
                                                    g.DrawRectangle(pen, rect);
                                                }
                                                g.DrawString(result.ToString(), new Font("Tahoma", 16f), Brushes.Blue, new Point(rect.X - 60, rect.Y - 50));
                                            }
                                        }


                                    }

                                }
                            }
                            catch (Exception)
                            {
                            }
                        }

                    }));

                }
            }), sourcetoken);
        }


        private void ban_hang_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (videoCaptureDevice != null)
                if (videoCaptureDevice.IsRunning == true)
                    videoCaptureDevice.Stop();
        }

        private void UpdateTotalPrice(int price)
        {
            lb_totalPrice.Text = price.ToString("#,##0") + " đ";
        }

        private void lbl_result_TextChanged(object sender, EventArgs e)
        {
            if (!lbl_result.Text.Contains("XXX") ) return;
            
                string[] PhanTichChuoi = lbl_result.Text.Split(new string[] { "XXX" }, StringSplitOptions.None);
                string MaSPG = PhanTichChuoi[0];
                int SizeGiay = Convert.ToInt32(PhanTichChuoi[1]);

                string query = "SELECT * FROM SanPhamGiay WHERE MaSPG = @MaSPG";
                object[] parameter = new object[] { MaSPG };
                DataTable dt = DataProvider.Instance.ExcuteQuery(query, parameter);

                Image img = Image.FromFile(dt.Rows[0]["Img"].ToString());

                string TenGiay = dt.Rows[0]["TenGiay"].ToString();
                string GiaBan = dt.Rows[0]["GiaBan"].ToString();
                string ThanhTien = GiaBan;

                int s38 = SizeGiay == 38 ? 1 : 0;
                int s39 = SizeGiay == 39 ? 1 : 0;
                int s40 = SizeGiay == 40 ? 1 : 0;
                int s41 = SizeGiay == 41 ? 1 : 0;
                int s42 = SizeGiay == 42 ? 1 : 0;
                int s43 = SizeGiay == 43 ? 1 : 0;
                int SoLuong = s38 + s39 + s40 + s41 + s42 + s43;

                int price = main.ConvertPriceStringToInt(GiaBan) * SoLuong;
                TotalPrice += price;

                object[] values_Size38 = new object[] { MaSPG, 38, s38 };
                object[] values_Size39 = new object[] { MaSPG, 39, s39 };
                object[] values_Size40 = new object[] { MaSPG, 40, s40 };
                object[] values_Size41 = new object[] { MaSPG, 41, s41 };
                object[] values_Size42 = new object[] { MaSPG, 42, s42 };
                object[] values_Size43 = new object[] { MaSPG, 43, s43 };
                object[] values_CTHDB = new object[] { MaHDB, MaSPG, SoLuong };

                DataSet.Instance.AddRowToDataTable(DataTb_Curent_Size, values_Size38);
                DataSet.Instance.AddRowToDataTable(DataTb_Curent_Size, values_Size39);
                DataSet.Instance.AddRowToDataTable(DataTb_Curent_Size, values_Size40);
                DataSet.Instance.AddRowToDataTable(DataTb_Curent_Size, values_Size41);
                DataSet.Instance.AddRowToDataTable(DataTb_Curent_Size, values_Size42);
                DataSet.Instance.AddRowToDataTable(DataTb_Curent_Size, values_Size43);
                DataSet.Instance.AddRowToDataTable(DataTb_CTHDB, values_CTHDB);

                object[] values_UiUser = new object[] { MaSPG, img, TenGiay, SizeGiay, SoLuong, GiaBan, ThanhTien };
                DataSet.Instance.AddRowToDataTable(DataTb_UIuser, values_UiUser);

                addDataSource();
                UpdateTotalPrice(TotalPrice);


        }

        private void data_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewRow row = data.SelectedRows[0];
            int SoLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);


            string GiaBan = row.Cells["GiaBan"].Value.ToString();
            int price = main.ConvertPriceStringToInt(GiaBan) * SoLuong;
            TotalPrice -= price;
        }

        private void data_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = data.SelectedRows[0];
            string MaSPG = row.Cells["MaSPG"].Value.ToString();
            DataRow[] foundRows_Size = DataTb_Curent_Size.Select("MaSPG = '" + MaSPG + "'");
            int SizeGiay = Convert.ToInt32(row.Cells["SizeGiay"].Value);

            int s38 = SizeGiay == 38 ? Convert.ToInt32(row.Cells["SoLuong"].Value) : 0;
            int s39 = SizeGiay == 39 ? Convert.ToInt32(row.Cells["SoLuong"].Value) : 0;
            int s40 = SizeGiay == 40 ? Convert.ToInt32(row.Cells["SoLuong"].Value) : 0;
            int s41 = SizeGiay == 41 ? Convert.ToInt32(row.Cells["SoLuong"].Value) : 0;
            int s42 = SizeGiay == 42 ? Convert.ToInt32(row.Cells["SoLuong"].Value) : 0;
            int s43 = SizeGiay == 43 ? Convert.ToInt32(row.Cells["SoLuong"].Value) : 0;

            int SoLuong = s38 + s39 + s40 + s41 + s42 + s43;
            string GiaBan = row.Cells["GiaBan"].Value.ToString();
            int price = main.ConvertPriceStringToInt(GiaBan) * SoLuong;
            TotalPrice += price;

            object[] values_CTHDB = new object[] { MaHDB, MaSPG, SoLuong };

            foundRows_Size[0]["SoLuong"] = s38;
            foundRows_Size[1]["SoLuong"] = s39;
            foundRows_Size[2]["SoLuong"] = s40;
            foundRows_Size[3]["SoLuong"] = s41;
            foundRows_Size[4]["SoLuong"] = s42;
            foundRows_Size[5]["SoLuong"] = s43;

            // Tìm hàng cần cập nhật 
            DataRow[] rows_CTHDB = DataTb_CTHDB.Select("MaHDB = '" + MaHDB + "' AND MaSPG = '" + MaSPG + "'");
            DataRow[] rows_UIuser = DataTb_UIuser.Select("MaSPG  = '" + MaSPG + "'");
            rows_CTHDB[0]["SoLuong"] = SoLuong;
            rows_UIuser[0]["SoLuong"] = SoLuong;
            rows_UIuser[0]["ThanhTien"] = price.ToString("#,##0") + " đ";

            addDataSource();
            UpdateTotalPrice(TotalPrice);
        }

        

        private void delete_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = data.SelectedRows[0];
            string id = row.Cells["MaSPG"].Value.ToString();
            DataRow[] foundRows_Size = DataTb_Curent_Size.Select("MaSPG = '" + id + "'");

            int s38 = Convert.ToInt32(foundRows_Size[0]["SoLuong"]);
            int s39 = Convert.ToInt32(foundRows_Size[1]["SoLuong"]);
            int s40 = Convert.ToInt32(foundRows_Size[2]["SoLuong"]);
            int s41 = Convert.ToInt32(foundRows_Size[3]["SoLuong"]);
            int s42 = Convert.ToInt32(foundRows_Size[4]["SoLuong"]);
            int s43 = Convert.ToInt32(foundRows_Size[5]["SoLuong"]);
            int SoLuong = s38 + s39 + s40 + s41 + s42 + s43;
             
            string GiaBan = row.Cells["GiaBan"].Value.ToString();
            int price = main.ConvertPriceStringToInt(GiaBan) * SoLuong;
            TotalPrice -= price;

            DataSet.Instance.DeleteRowFromDataTable(DataTb_Curent_Size, "MaSPG", id);
            DataSet.Instance.DeleteRowFromDataTable(DataTb_CTHDB, "MaSPG", id);
            DataSet.Instance.DeleteRowFromDataTable(DataTb_UIuser, "MaSPG", id);


            addDataSource();
            UpdateTotalPrice(TotalPrice);
        }

        private void ThanhToan_Click(object sender, EventArgs e)
        {
            



            string SelectQuery_SPG = "SELECT * FROM SanPhamGiay";
            string SelectQuery_CTHDM = "SELECT * FROM ChiTietHoaDonBan";
            string SelectQuery_Size = "SELECT * FROM SizeGiay";


            DataSet.Instance.UpdateAndMergeDataTables(DataTb_Previous_Size, DataTb_Curent_Size, "-");

            foreach (DataRow row_SPG in DataTb_SPG.Rows)
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
            DataProvider.Instance.UpdateTableInDatabase(DataTb_CTHDB, SelectQuery_CTHDM);

            string query = "INSERT INTO HoaDonBan (MaHDB, MaNV, TongTien) VALUES (@MaHDM, @MaNV, @TongTien)";
            string str_TotalPrice = main.ConvertIntPriceToString(TotalPrice);
            object[] parameter = new object[] { MaHDB, GlobalVariables.MaNV, str_TotalPrice };
            DataProvider.Instance.ExcuteNonQuery(query, parameter);

            reset();
        }
        private void Huy_Click(object sender, EventArgs e)
        {
            reset();
        }
        private void reset()
        {
            lbl_result.Text = "";
            TotalPrice = 0;
            UpdateTotalPrice(TotalPrice);
            CreateId();
            addDataTable();
            addDataSource();
        }

    }
}
