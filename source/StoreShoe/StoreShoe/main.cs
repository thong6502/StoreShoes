using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoe_store_manager
{
    public class main
    {
        public static bool isUpdating = false;

        public static void UpdateTextBox(Guna2TextBox tb, string text)
        {
            // Kiểm tra xem sự kiện có được kích hoạt bởi mã của chúng ta không
            if (isUpdating) return;

            // Bắt đầu cập nhật
            isUpdating = true;

            // Xóa ký tự không phải số và chữ "đ"
            string digitsOnly = new String(text.Where(c => Char.IsDigit(c)).ToArray());

            // Kiểm tra xem người dùng có nhập số không
            if (Decimal.TryParse(digitsOnly, out decimal value))
            {
                // Định dạng giá trị thành chuỗi với dấu phẩy phân cách và ký hiệu đồng
                string formattedValue = String.Format("{0:N0} đ", value);

                // Cập nhật giá trị hiển thị trên TextBox
                tb.Text = formattedValue;

                // Di chuyển con trỏ về cuối TextBox, trừ đi số ký tự của chuỗi " đ"
                tb.SelectionStart = tb.Text.Length - " đ".Length;
            }
            else
            {
                // Nếu không phải số, chỉ hiển thị chữ "đ"
                tb.Text = "0 đ";

                // Di chuyển con trỏ về cuối TextBox
                tb.SelectionStart = tb.Text.Length - " đ".Length;
            }

            // Kết thúc cập nhật
            isUpdating = false;
        }



        public static int ConvertPriceStringToInt(string priceStr)
        {
            string numberStr = new string(priceStr.Where(char.IsDigit).ToArray());
            int price = int.Parse(numberStr);
            return price;
        }

        public static string ConvertIntPriceToString(int price)
        {
            return string.Format("{0:n0} đ", price);
        }

    }
}
