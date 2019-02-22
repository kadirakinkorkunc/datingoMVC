using datingo.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace datingo.Controllers
{

    public class SecurityController : Controller
    {
        datingDbEntities db = new datingDbEntities();

        // GET: Security
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(userAcc user)
        {
            var userInDb = db.userAcc.FirstOrDefault(x => x.userName == user.userName && x.userPw == user.userPw);

            if (userInDb!=null){
                FormsAuthentication.SetAuthCookie(userInDb.userName,false);
                return RedirectToAction("Index","Home");
            }
            else {
                ViewBag.Mesaj = "Geçersiz kullanıcı adı veya şifre!";
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(userAcc user)
        {
            if( db.userAcc.Any(x=>x.userName == user.userName))
            {

                ViewBag.AlreadyExists = "Bu kullanıcı adı daha önceden alınmış!";
                return View();
               

            }

            else if (user.userId == 0) //ekleme işlemi
            {
                db.userAcc.Add(user);
                db.SaveChanges();
                FormsAuthentication.SetAuthCookie(user.userName, false);
                return RedirectToAction("ProfilGuncelle", "Profile");
            }

            else
            {
                return View(); 
            }

        }
    }
}