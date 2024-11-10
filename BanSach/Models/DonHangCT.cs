﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BanSach.Models
{
    using System;
    using System.Collections.Generic;

    public partial class DonHangCT
    {
        public int IDdh { get; set; }
        public Nullable<int> IDSanPham { get; set; }
        public int IDDonHang { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<double> Gia { get; set; }
        public string DanhGia { get; set; }

        public virtual DonHang DonHang { get; set; }
        public virtual SanPham SanPham { get; set; }

        public double TongTien
        {
            get
            {
                // Kiểm tra nếu SoLuong hoặc Gia null thì trả về 0
                return (SoLuong ?? 0) * (Gia ?? 0);
            }
        }
    }
}