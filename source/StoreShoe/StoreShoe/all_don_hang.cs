using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;

namespace shoe_store_manager
{
    public partial class all_don_hang : Form
    {
        public all_don_hang()
        {
            InitializeComponent();
        }

        private void don_hang_Load(object sender, EventArgs e)
        {

            string query_HDB = "SELECT * FROM HoaDonBan";
            data_HDB.DataSource = DataProvider.Instance.ExcuteQuery(query_HDB);

            string query_HDM = "SELECT * FROM HoaDonMua";
            data_HDM.DataSource = DataProvider.Instance.ExcuteQuery(query_HDM);
        }

        private void cbx_DonHang_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cbx_DonHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            search.Text = "";
            if (cbx_DonHang.Text == "Tất cả đơn hàng bán")
            {
                data_HDB.Visible = true;
                data_HDM.Visible = false;
            }
            else
            {
                data_HDB.Visible = false;
                data_HDM.Visible = true;
            }
            SearchInput();
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            SearchInput();
        }
        private void SearchInput()
        {
            string str_search = "%" + search.Text + "%";
            string query = "";
            object[] parameter = new object[] { str_search };

            if (cbx_DonHang.Text == "Tất cả đơn hàng nhập")
            {
                query = "SELECT * FROM HoaDonMua WHERE MaHDM LIKE @str_search";
                data_HDM.DataSource = DataProvider.Instance.ExcuteQuery(query, parameter);
            }
            else if (cbx_DonHang.Text == "Tất cả đơn hàng bán")
            {
                query = "SELECT * FROM HoaDonBan WHERE MaHDB LIKE @str_search";
                data_HDB.DataSource = DataProvider.Instance.ExcuteQuery(query, parameter);
            }
            else
                return;  // Nếu cbx_filter.Text không phải là một trong các giá trị mong đợi, thoát khỏi hàm

        }
    }
}
