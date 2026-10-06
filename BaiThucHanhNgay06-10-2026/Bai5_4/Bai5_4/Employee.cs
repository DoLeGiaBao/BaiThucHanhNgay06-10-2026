using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai5_4
{
    public class Employee
    {
        // Mã nhân viên
        public string MaNV { get; set; }

        // Họ tên nhân viên
        public string HoTen { get; set; }

        // Chức vụ
        public string ChucVu { get; set; }

        // Ngày vào làm
        public DateTime NgayVaoLam { get; set; }

        // Phòng ban
        public string PhongBan { get; set; }

        // Nhóm
        public string Nhom { get; set; }

        // Constructor
        public Employee(
            string maNV,
            string hoTen,
            string chucVu,
            DateTime ngayVaoLam,
            string phongBan,
            string nhom)
        {
            MaNV = maNV;
            HoTen = hoTen;
            ChucVu = chucVu;
            NgayVaoLam = ngayVaoLam;
            PhongBan = phongBan;
            Nhom = nhom;
        }
    }
}