using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using datingo.Models.EntityFramework;

namespace datingo.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment



        datingDbEntities db = new datingDbEntities();

        [Route("Comment/Index/{id}")]
        [HttpGet]
        public ActionResult Index(string id)
        {

            var comment = from m in db.commentParent
                          select m;

            comment = comment.Where(x => x.parentProfile == id);
            
            return View(comment);
        }

        [HttpPost]
        [Route("Comment/Index/{id}")]
        public ActionResult Index(commentParent commentx,string id,string comment)
        {
            if (comment.Length < 2)
            {
                TempData["mesaj"] = "Lütfen Yorumunuzu Girin!";
                return RedirectToAction("Index","Comment");
            }
            else
            {

                commentx.comment = comment.Trim();
                commentx.commentOwner = User.Identity.Name;
                commentx.commentDate = DateTime.Now;
                commentx.parentProfile = id;
                db.commentParent.Add(commentx);
                db.SaveChanges();

                return RedirectToAction("Index","Comment");
            }
        }
    }
}