using datingo.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace datingo.Controllers
{

    [Authorize]
    public class ProfileController : Controller
    {
        // GET: Profile


        datingDbEntities db = new datingDbEntities();


        [Authorize]
        [Route("Profile/Index/{id}")]
        public ActionResult Index(string id)
        {

            if (id.Trim() != null)
            {
                userAcc user = db.userAcc.Find(id.Trim());
                

                if (user.GetType() == null) // type ı null ise httpnotfound ver diyorum ama bana nesne ayarlanamadı diyor.
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(user);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }


        [HttpGet]
        public ActionResult ProfilGuncelle(string id)
        {
            id = User.Identity.Name;
            if (id.Trim() != null)
            {
                userAcc user = db.userAcc.Find(id.Trim());
                if (user.GetType() == null) // type ı null ise httpnotfound ver diyorum ama bana nesne ayarlanamadı diyor.
                {
                    return HttpNotFound();
                }
                else if (user.userName != User.Identity.Name)
                {
                    return HttpNotFound();

                }
                else
                {
                    return View(user);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ProfilGuncelle(userAcc user)
        {
            var guncellenecekUser = db.userAcc.Find(user.userName);
            if (guncellenecekUser == null)
                return HttpNotFound();
            else
            {

                guncellenecekUser.userAd = user.userAd;
                guncellenecekUser.userSoyad = user.userSoyad;
                guncellenecekUser.userGender = user.userGender;
                guncellenecekUser.userBoy = user.userBoy;
                guncellenecekUser.userKilo = user.userKilo;
                guncellenecekUser.userHair = user.userHair;
                guncellenecekUser.userEye = user.userEye;
                guncellenecekUser.userCountry = user.userCountry;
                guncellenecekUser.userFavTeam = user.userFavTeam;
                guncellenecekUser.userFavMusic = user.userFavMusic;
                guncellenecekUser.userFavFilm = user.userFavFilm;
                guncellenecekUser.userMeslek = user.userMeslek;
                guncellenecekUser.userEgitim = user.userEgitim;
                guncellenecekUser.userBirthday = user.userBirthday;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

  
    }
}

   