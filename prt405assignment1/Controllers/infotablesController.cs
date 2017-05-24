using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using prt405assignment1;

namespace prt405assignment1.Controllers
{
    //for controller
    public class infotablesController : Controller
    {
        private PersonalDetailsEntities db = new PersonalDetailsEntities();

        // GET: infotables
        // function for identifying user
        public ActionResult Index()
        {
            if (Checkuserauth() == true)
            {
                if (checkadmnin() == true)
                {
                    var infotables = db.infotables.Include(i => i.AspNetUser);
                    return View(infotables);
                }
                else
                {
                    var currentuser = User.Identity.GetUserId();
                    var infodetails = db.infotables.Where(s => s.Uid == currentuser);
                    return View(infodetails.ToList());
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        // GET: infotables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            infotable infotable = db.infotables.Find(id);
            if (infotable == null)
            {
                return HttpNotFound();
            }
            return View(infotable);
        }

        // GET: infotables/Create
        public ActionResult Create()
        {
            ViewBag.Uid = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: infotables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Uid,FirstName,LastName,Email,Address,City,PhoneNumber")] infotable infotable)
        {
            infotable.Uid = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {

                db.infotables.Add(infotable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Uid = new SelectList(db.AspNetUsers, "Id", "Email", infotable.Uid);
            return View(infotable);
        }

        // GET: infotables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            infotable infotable = db.infotables.Find(id);
            if (infotable == null)
            {
                return HttpNotFound();
            }
            ViewBag.Uid = new SelectList(db.AspNetUsers, "Id", "Email", infotable.Uid);
            return View(infotable);
        }

        // POST: infotables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Uid,FirstName,LastName,Email,Address,City,PhoneNumber")] infotable infotable)
        {
            infotable.Uid = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(infotable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Uid = new SelectList(db.AspNetUsers, "Id", "Email", infotable.Uid);
            return View(infotable);
        }

        // GET: infotables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            infotable infotable = db.infotables.Find(id);
            if (infotable == null)
            {
                return HttpNotFound();
            }
            return View(infotable);
        }

        // POST: infotables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            infotable infotable = db.infotables.Find(id);
            db.infotables.Remove(infotable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public Boolean Checkuserauth()
        {
            var userid = User.Identity.GetUserId();
            var Username = User.Identity.GetUserName();
            if (userid != null && Username != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean checkadmnin()
        {
            if(User.Identity.GetUserName() == "admin@gmail.com")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
