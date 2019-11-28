using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongDiemDanh.Models;

namespace HeThongDiemDanh.Controllers
{
    public class SinhvienController : Controller
    {
        DatabaseDDSVEntities db = new DatabaseDDSVEntities();

        [Authorize(Roles ="SINHVIEN")]
        // GET: Sinhvien
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Thongtinsv()
        {
            int id = Convert.ToInt32(Session["IDNGUOIDUNG"]);
            var v = from t in db.NGUOIDUNGs
                    where t.IDNGUOIDUNG == id
                    select t;

            var user = db.NGUOIDUNGs.FirstOrDefault(c => c.IDNGUOIDUNG == id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult Test()
        {
            return View();
        }

    }
}