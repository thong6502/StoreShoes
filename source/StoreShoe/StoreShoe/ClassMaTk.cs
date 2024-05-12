using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoe_store_manager
{
    public static class GlobalVariables
    {
        public static string MaNV { get; set; }
        public static string MaTK { get; set; }
    }


    public class ClassMaNV
    {
        public ClassMaNV()
        {
        }

        public ClassMaNV(string maNV)
        {
            GlobalVariables.MaNV = maNV;
        }
    }

    public class ClassMaTK
    {
        public ClassMaTK()
        {
        }

        public ClassMaTK(string maTK)
        {
            GlobalVariables.MaTK = maTK;
        }
    }
}
