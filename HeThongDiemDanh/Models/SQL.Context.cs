﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HeThongDiemDanh.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DatabaseDDSVEntities : DbContext
    {
        public DatabaseDDSVEntities()
            : base("name=DatabaseDDSVEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CHITIETQUYENNGUOIDUNG> CHITIETQUYENNGUOIDUNGs { get; set; }
        public virtual DbSet<DANHSACHLOP> DANHSACHLOPs { get; set; }
        public virtual DbSet<DIEMDANHSV> DIEMDANHSVs { get; set; }
        public virtual DbSet<HOCKY> HOCKies { get; set; }
        public virtual DbSet<LICHHOC> LICHHOCs { get; set; }
        public virtual DbSet<LOPMONHOC> LOPMONHOCs { get; set; }
        public virtual DbSet<LOPSINHVIEN> LOPSINHVIENs { get; set; }
        public virtual DbSet<MONHOC> MONHOCs { get; set; }
        public virtual DbSet<NAMHOC> NAMHOCs { get; set; }
        public virtual DbSet<NGUOIDUNG> NGUOIDUNGs { get; set; }
        public virtual DbSet<PHANQUYEN> PHANQUYENs { get; set; }
        public virtual DbSet<QUYENNGUOIDUNG> QUYENNGUOIDUNGs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
