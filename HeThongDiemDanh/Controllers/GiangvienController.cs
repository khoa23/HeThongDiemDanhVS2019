using HeThongDiemDanh.Models;
using QRCoder;
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


        public ActionResult QR(string qrcode)
        {

            using (MemoryStream ms = new MemoryStream())
            {
                int idmonhoc = Convert.ToInt32(Session["IDMONHOC"]);
                int Numrd;
                string Numrd_str;
                Random rd = new Random();
                Numrd = rd.Next(10000000, 1000000000);//biến Numrd sẽ nhận có giá trị ngẫu nhiên trong khoảng 1 đến 100
                Numrd_str = rd.Next(10000000, 1000000000).ToString();//Chuyển giá trị ramdon về kiểu string
                DateTime dt = DateTime.Now;
                string strDate = dt.ToString("dd/MM/yy");

                //string dayqr = idmonhoc+'-'+Numrd_str+'-'+strDate;
                string dayqr = "123"+'-'+Numrd_str+'-'+strDate;



                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(dayqr, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    bitMap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }

            return View();
        }
    }
}