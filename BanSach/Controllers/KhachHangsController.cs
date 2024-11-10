﻿using BanSach.Models;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;

namespace BanSach.Controllers
{
    public class KhachHangsController : Controller
    {
        private dbSach db = new dbSach();
        public ActionResult LoginCus()
        {
            return View();
        }

        // GET: KhachHangs
        public ActionResult Index(int? page)
        {
            int pageSize = 10; // Số bản ghi trên mỗi trang
            int pageNumber = (page ?? 1); // Trang hiện tại, mặc định là trang 1

            var khachHangList = db.KhachHang.OrderBy(kh => kh.IDkh).ToPagedList(pageNumber, pageSize);
            return View(khachHangList);
        }

        // GET: KhachHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHang.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }
        public ActionResult DetailsAD(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHang.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: KhachHangs/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDkh,TenKH,SoDT,Email,TKhoan,MKhau")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem email đã tồn tại chưa
                var existingEmail = db.KhachHang.FirstOrDefault(kh => kh.Email == khachHang.Email);
                if (existingEmail != null)
                {
                    // Nếu email đã tồn tại, thêm lỗi vào ModelState
                    ModelState.AddModelError("Email", "Email này đã tồn tại. Vui lòng sử dụng email khác.");
                    return View(khachHang);
                }

                // Kiểm tra xem số điện thoại đã tồn tại chưa
                var existingPhoneNumber = db.KhachHang.FirstOrDefault(kh => kh.SoDT == khachHang.SoDT);
                if (existingPhoneNumber != null)
                {
                    // Nếu số điện thoại đã tồn tại, thêm lỗi vào ModelState
                    ModelState.AddModelError("SoDT", "Số điện thoại này đã tồn tại. Vui lòng sử dụng số điện thoại khác.");
                    return View(khachHang);
                }

                // Nếu cả email và số điện thoại không tồn tại, thêm đối tượng mới vào cơ sở dữ liệu
                db.KhachHang.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khachHang);
        }




        // GET: KhachHangs/Edit/5
        [HttpGet]
        // GET: KhachHangAD/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang kh = db.KhachHang.Find(id);
            if (kh == null)
            {
                return HttpNotFound();
            }
            return View(kh);
        }

        // POST: KhachHangAD/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDkh,TenKH,SoDT,Email,TKhoan,MKhau,ConfirmPass")] KhachHang kh)
        {

            if (ModelState.IsValid)
            {
                db.Entry(kh).State = EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                return RedirectToAction("UpdateSuccesss", "KhachHangs");
            }
            return View();
        }
        // GET: KhachHangAD/Edit/5
        public ActionResult EditAD(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang kh = db.KhachHang.Find(id);
            if (kh == null)
            {
                return HttpNotFound();
            }
            return View(kh);
        }

        // POST: KhachHangAD/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAD([Bind(Include = "IDkh,TenKH,SoDT,Email,TKhoan,MKhau,ConfirmPass")] KhachHang kh)
        {

            if (ModelState.IsValid)
            {
                db.Entry(kh).State = EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                return RedirectToAction("Index", "KhachHangs");
            }
            return View();
        }


        [HttpGet]
        public ActionResult UpdateSuccesss()
        {
            return View();
        }
        // GET: KhachHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHang.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachHang khachHang = db.KhachHang.Find(id);
            db.KhachHang.Remove(khachHang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult LichSuDonHang()
        {
            int? currentCustomerId = Session["IDkh"] as int?;

            if (currentCustomerId == null)
            {
                // Nếu khách hàng chưa đăng nhập, chuyển hướng họ tới trang đăng nhập
                return RedirectToAction("LoginAccountCus", "LoginUser");
            }

            // Lấy danh sách đơn hàng của khách hàng hiện tại
            var orders = db.DonHang
                .Where(dh => dh.IDkh == currentCustomerId)
                .Include(dh => dh.DonHangCT)  // Bao gồm chi tiết đơn hàng
                .ToList();

            return View(orders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}