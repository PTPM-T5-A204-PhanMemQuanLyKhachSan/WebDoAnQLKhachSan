using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDoAnQLKhachSan.Models
{
    public class PhongDTO
    {
        public int MaPhong { get; set; }
        public int? MaLoai { get; set; }
        public string TenPhong { get; set; }
        public double? GiaPhong { get; set; }
        public string TrangThai { get; set; }
    }
}