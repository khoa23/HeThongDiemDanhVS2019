using HeThongDiemDanh.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXing;

namespace HeThongDiemDanh.Controllers
{
    public class GiangvienController : Controller
    {
        DatabaseDDSVEntities db = new DatabaseDDSVEntities();
        [Authorize(Roles ="GIANGVIEN")]
        // GET: Giangvien
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThongTinGV()
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
        public ActionResult DiemDanh()
        {
            int id = Convert.ToInt32(Session["IDNGUOIDUNG"]);
            var list = from m in db.MONHOCs
                       join l in db.LOPMONHOCs
                       on m.IDMONHOC equals l.IDMONHOC
                       where id == l.IDGIANGVIEN
                       select m;
            return View(list);
            
        }

        

        public ActionResult LopMonHoc(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();

            }
            int? idlopmonhoc = id;
            var list = from s in db.LOPMONHOCs where s.IDMONHOC == id select s;
            return View(list);
        }       

        public ActionResult DanhSachLop(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();

            }
            var list = from a in db.NGUOIDUNGs
                       join d in db.DANHSACHLOPs
                       on a.IDNGUOIDUNG equals d.IDNGUOIDUNG

                       where id == d.IDLOPMH
                       select a;
            return View(list);
        }

        public ActionResult QR()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Generate(QRCodeModel qrcode)
        {
            try
            {
                qrcode.QRCodeImagePath = GenerateQRCode(qrcode.QRCodeText);
                ViewBag.Message = "QR Code Created successfully";
            }
            catch (Exception ex)
            {
                //catch exception if there is any
            }
            return View("QR", qrcode);
        }

        private string GenerateQRCode(string qrcodeText)
        {
            string folderPath = "~/Images/";
            string imagePath = "~/Images/QrCode.jpg";
            // If the directory doesn't exist then create it.
            if (!Directory.Exists(Server.MapPath(folderPath)))
            {
                Directory.CreateDirectory(Server.MapPath(folderPath));
            }

            var barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            var result = barcodeWriter.Write(qrcodeText);

            string barcodePath = Server.MapPath(imagePath);
            var barcodeBitmap = new Bitmap(result);
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(barcodePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            return imagePath;
        }
    }
}