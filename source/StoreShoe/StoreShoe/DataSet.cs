using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoe_store_manager
{
    public class DataSet
    {
        private static DataSet instance;
        public static DataSet Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataSet();
                }
                return instance;
            }
            // set {instance = value}
        }
        private DataSet() { }


        public void AddRowToDataTable(DataTable dt, object[] values = null)
        {
            // Tạo một hàng mới
            DataRow row = dt.NewRow();

            // Đặt giá trị cho các cột
            for (int i = 0; i < values.Length; i++)
            {
                row[i] = values[i];
            }

            // Thêm hàng mới vào DataTable
            dt.Rows.Add(row);
        }
        public void DeleteRowFromDataTable(DataTable dt, string columnName, string value)
        {
            // Tìm hàng cần xóa
            DataRow[] rows = dt.Select(columnName + " = '" + value + "'");

            // Xóa tất cả các hàng tìm thấy
            foreach (DataRow row in rows)
            {
                dt.Rows.Remove(row);
            }
        }
        public void UpdateRowInDataTable(DataTable dt, string columnName, string searchValue, object[] values = null)
        {
            // Tìm hàng cần cập nhật
            DataRow[] rows = dt.Select(columnName + " = '" + searchValue + "'");

            // Cập nhật tất cả các hàng tìm thấy
            foreach (DataRow row in rows)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    row[i] = values[i];
                }
            }
        }

        public string findIdInDataTB(DataTable dt, string columnName, string searchValue, string colResultId)
        {
            string id = "";
            // Tìm hàng cần cập nhật
            DataRow[] rows = dt.Select(columnName + " = '" + searchValue + "'");
            if (rows.Length > 0)
            {
                id = rows[0][colResultId].ToString();
            }
            return id;
        }
        public string GenerateIdDataTabble(DataTable dt, string firstIdStr, string colId)
        {
            int count = 1;
            string id = "";
            bool exists = true;

            while (exists)
            {
                id = firstIdStr + count.ToString();

                // Kiểm tra xem ID đã tồn tại trong DataTable chưa
                DataRow[] foundRows = dt.Select(colId + " = '" + id + "'");
                exists = (foundRows.Length > 0);

                count++;
            }

            return id;
        }

        // 
        public void UpdateAndMergeDataTables(DataTable PreviousDT, DataTable CurentDT, string operation)
        {
            // Kết hợp hai bảng và đổ dữ liệu vào PreviousDT

            foreach (DataRow rowPr in PreviousDT.Rows)
            {
                foreach (DataRow rowCr in CurentDT.Rows)
                {
                    if (rowPr["MaSPG"].ToString() == rowCr["MaSPG"].ToString() && (int)rowPr["KichThuoc"] == (int)rowCr["KichThuoc"])
                    {
                        if (operation == "+")
                        {
                            rowPr["SoLuong"] = (int)rowPr["SoLuong"] + (int)rowCr["SoLuong"];
                        }
                        else if (operation == "-")
                        {
                            rowPr["SoLuong"] = (int)rowPr["SoLuong"] - (int)rowCr["SoLuong"];
                        }
                    }
                }
            }

            foreach (DataRow rowCr in CurentDT.Rows)
            {
                bool found = false;
                foreach (DataRow rowPr in PreviousDT.Rows)
                {
                    if (rowPr["MaSPG"].ToString() == rowCr["MaSPG"].ToString() && (int)rowPr["KichThuoc"] == (int)rowCr["KichThuoc"])
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    PreviousDT.ImportRow(rowCr);
                }
            }
        }


    }
}
