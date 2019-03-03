using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using datingo.Models.EntityFramework;


namespace datingo.Controllers
{
    [HandleError] 
    public class HomeController : Controller
    {
        // GET: Home
        
        public ActionResult Index()
        {
            return View();
        }


        [Authorize]
        public ActionResult FindMatch(string location, string searchString,string cinsiyet)
        {
            datingDbEntities db = new datingDbEntities();
            //lokasyona göre
            var locationlist = new List<string>();

            var locationQry = from d in db.userAcc
                              orderby d.userCountry
                              select d.userCountry;

            locationlist.AddRange(locationQry.Distinct());

            ViewBag.location = new SelectList(locationlist);
            //lokasyona göre

            //cinsiyete göre
            var genderlist = new List<string>();

            var genderQry = from e in db.userAcc
                            orderby e.userGender
                            select e.userGender;

            genderlist.AddRange(genderQry.Distinct());

            ViewBag.cinsiyet = new SelectList(genderlist);
            //cinsiyete göre

            //isme göre
            var users = from m in db.userAcc
                        select m;

            //isme göre

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.userAd.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(location))
            {
                users = users.Where(x => x.userCountry == location);
            }
            if (!String.IsNullOrEmpty(cinsiyet))
            {
                users = users.Where(t => t.userGender == cinsiyet);
            }





            return View(users);

        }

        public ActionResult Faq()
        {
            return View();
        }

        public ActionResult Hakkımızda()
        {
            return View();
        }



        [Authorize]
        public ActionResult Chat(string id)
        {
            datingDbEntities db =new datingDbEntities();
            id = User.Identity.Name;
            userAcc user = db.userAcc.Find(id);
            return View(user);
        }
    }
}