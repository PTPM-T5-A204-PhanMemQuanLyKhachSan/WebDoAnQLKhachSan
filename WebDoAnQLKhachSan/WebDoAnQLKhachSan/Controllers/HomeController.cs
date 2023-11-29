using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDoAnQLKhachSan.Models;

namespace WebDoAnQLKhachSan.Controllers
{
    public class HomeController : Controller
    {
        QLKhachSanDataContext db = new QLKhachSanDataContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PHONG()
        {
            ViewBag.LoaiPhongs = db.LoaiPhongs;
            return View(db.Phongs);
        }
        public ActionResult LienHe()
        {
            return View();
        }

        public ActionResult DaGuiYeuCau()
        {
            return View();
        }

        public ActionResult getPhongById(int id)
        {
            Phong Phong = db.Phongs.FirstOrDefault(t => t.MaPhong == id);
            PhongDTO p = new PhongDTO();
            p.MaPhong = Phong.MaPhong;
            p.TenPhong = Phong.TenPhong;
            p.MaLoai = Phong.MaLoai;
            p.GiaPhong = Phong.GiaPhong;
            p.TrangThai = Phong.TrangThai;
            return Json(new { data = p }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DatPhong(KhachHang kh, DatPhong dp)
        {
            try
            {
                KhachHang khachHang = db.KhachHangs.FirstOrDefault(t => t.DienThoai == kh.DienThoai);
                if (khachHang == null)
                {
                    db.KhachHangs.InsertOnSubmit(kh);
                    db.SubmitChanges();
                    dp.MaKH = kh.MaKH;
                }
                else
                {
                    khachHang.HoTenKH = kh.HoTenKH;
                    khachHang.CCCD = kh.CCCD;
                    khachHang.DienThoai = kh.DienThoai;
                    khachHang.DiaChi = kh.DiaChi;
                    db.SubmitChanges();
                    dp.MaKH = khachHang.MaKH;
                }

                Phong p = db.Phongs.FirstOrDefault(t => t.MaPhong == dp.MaPhong);
                if (p == null)
                {
                    return Json(new { success = false, message = "Lỗi không tìm thấy phòng!" });
                }

                if (p.TrangThai != "Trống")
                {
                    return Json(new { success = false, message = "Phòng này hiện đang có người!" });
                }

                db.DatPhongs.InsertOnSubmit(dp);
                db.SubmitChanges();

                p.TrangThai = "Đã đặt";
                db.SubmitChanges();

                return Json(new { success = true, message = "Đặt phòng thành công!" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e });
            }
        }
    }
}