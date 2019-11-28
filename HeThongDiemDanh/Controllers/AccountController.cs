using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HeThongDiemDanh.Models;

namespace HeThongDiemDanh.Controllers
{
    public class AccountController : Controller
    {

        // LOGIN//
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(NGUOIDUNG model)
        {
            using (DatabaseDDSVEntities db = new DatabaseDDSVEntities())
            {
                var user = db.NGUOIDUNGs.FirstOrDefault(x => x.TENDANGNHAP == model.TENDANGNHAP && x.MATKHAU == model.MATKHAU);
                if (user != null)
                {
                    Session["IDNGUOIDUNG"] = user.IDNGUOIDUNG;
                    Session["TENDANGNHAP"] = user.TENDANGNHAP;
                    Session["IDPHANQUYEN"] = user.IDPHANQUYEN;
                    FormsAuthentication.SetAuthCookie(user.TENDANGNHAP, false);
                    if (user.IDPHANQUYEN == 1)
                    {
                        return RedirectToAction("../Admin/Index");
                    }
                    else if (user.IDPHANQUYEN == 2)
                    {
                        return RedirectToAction("../Giangvien/Index");
                    }
                    else if (user.IDPHANQUYEN == 3)
                    {
                        return RedirectToAction("../Sinhvien/Index");
                    }
                }
                else
                {
                    model.LoginErroMsg = "Incorrect Username Or Password";
                    return View("Login", model);
                }
            }
            return View("Login", model);
        }
        //END-LOGIN//

        //LOGOUT//
        public ActionResult LogOut()
        {
            int userId = (int)Session["IDNGUOIDUNG"];
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
        //END-LOGOUT//

        //IsEmailExist//
        //[NonAction]
        //public bool IsEmailExist(string emailID)
        //{
        //    using (DatabaseDDSVEntities dc = new DatabaseDDSVEntities())
        //    {
        //        var v = dc.NGUOIDUNGs.Where(a => a.EMAIL == emailID).FirstOrDefault();
        //        return v != null;
        //    }
        //}
        //End IsEmailExist//

        //SendVerificationLinkEmail//
        //[NonAction]
        //public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        //{
        //    var verifyUrl = "/User/" + emailFor + "/" + activationCode;
        //    var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

        //    var fromEmail = new MailAddress("dotnetawesome@gmail.com", "Dotnet Awesome");
        //    var toEmail = new MailAddress(emailID);
        //    var fromEmailPassword = "********"; // Replace with actual password

        //    string subject = "";
        //    string body = "";
        //    if (emailFor == "VerifyAccount")
        //    {
        //        subject = "Your account is successfully created!";
        //        body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
        //            " successfully created. Please click on the below link to verify your account" +
        //            " <br/><br/><a href='" + link + "'>" + link + "</a> ";
        //    }
        //    else if (emailFor == "ResetPassword")
        //    {
        //        subject = "Reset Password";
        //        body = "Hi,<br/>br/>We got request for reset your account password. Please click on the below link to reset your password" +
        //            "<br/><br/><a href=" + link + ">Reset Password link</a>";
        //    }

        //    var smtp = new SmtpClient
        //    {
        //        Host = "smtp.gmail.com",
        //        Port = 587,
        //        EnableSsl = true,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
        //    };

        //    using (var message = new MailMessage(fromEmail, toEmail)
        //    {
        //        Subject = subject,
        //        Body = body,
        //        IsBodyHtml = true
        //    })
        //        smtp.Send(message);
        //}
        //End SendVerificationLinkEmail//

        //Verify Account//
        //[HttpGet]
        //public ActionResult VerifyAccount(string id)
        //{
        //    bool Status = false;
        //    using (DatabaseDDSVEntities dc = new DatabaseDDSVEntities())
        //    {
        //        dc.Configuration.ValidateOnSaveEnabled = false; // This line I have added here to avoid 
        //                                                        // Confirm password does not match issue on save changes
        //        var v = dc.NGUOIDUNGs.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
        //        if (v != null)
        //        {
        //            v.IsEmailVerified = true;
        //            dc.SaveChanges();
        //            Status = true;
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Invalid Request";
        //        }
        //    }
        //    ViewBag.Status = Status;
        //    return View();
        //}
        //END Verify Account//

        //FORGOT PASSWORD//
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string EmailID, string UserID)
        {
            DatabaseDDSVEntities db = new DatabaseDDSVEntities();
            var check = db.NGUOIDUNGs.Where(x => x.TENDANGNHAP == UserID && x.EMAIL == EmailID).FirstOrDefault();
            if (check != null)
            {
                string senderID = "dangthao0102@gmail.com";
                string senderPassword = "2302@0102";
                string result = "Email Sent Successfully";

                string body = " " + UserID + " has sent an email from " + EmailID;
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(senderID);
                    mail.From = new MailAddress(senderID);
                    mail.Subject = "My Test Email!";
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                    smtp.Credentials = new System.Net.NetworkCredential(senderID, senderPassword);
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    result = "problem occurred";
                    Response.Write("Exception in sendEmail:" + ex.Message);
                }
                return Json(result);
            }
            return View();
        }
        //END FORGOT PASSWORD//

        //RESET PASSWORD//
        public ActionResult ResetPassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            using (DatabaseDDSVEntities dc = new DatabaseDDSVEntities())
            {
                var user = dc.NGUOIDUNGs.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (DatabaseDDSVEntities dc = new DatabaseDDSVEntities())
                {
                    var user = dc.NGUOIDUNGs.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.MATKHAU = Crypto.Hash(model.NewPassword);
                        user.ResetPasswordCode = "";
                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.SaveChanges();
                        message = "New password updated successfully";
                    }
                }
            }
            else
            {
                message = "Something invalid";
            }
            ViewBag.Message = message;
            return View(model);
        }
        //END RESET PASSWORD//


    }
}