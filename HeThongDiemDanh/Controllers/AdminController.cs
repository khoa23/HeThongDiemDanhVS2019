using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongDiemDanh.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles ="ADMIN")]
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}