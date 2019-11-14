using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HeThongDiemDanh
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Lop mon hoc",
                "Giangvien/DiemDanh/LopMonHoc-{id}",
                new { controller = "Giangvien", action = "LopMonHoc" }, new { id = @"\d+" }
            );

            routes.MapRoute(
                "Danh sach sv trong lop",
                "Giangvien/DiemDanh/LopMonHoc/DanhSachLop/{id}",
                new { controller = "Giangvien", action = "DanhSachLop" }, new { id = @"\d+" }
            );

            routes.MapRoute(
                "QR",
                "Giangvien/DiemDanh/LopMonHoc/QR/{id}",
                new { controller = "Giangvien", action = "QR" }, new { id = @"\d+" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
