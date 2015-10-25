using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountsModelsController : Controller
    {
        private AccountsDBContext db = new AccountsDBContext();

        // GET: AccountsModels
        public ActionResult Index()
        {
            return View(db.Account.ToList());
        }

        // GET: AccountsModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountsModel accountsModel = db.Account.Find(id);
            if (accountsModel == null)
            {
                return HttpNotFound();
            }
            return View(accountsModel);
        }

        // GET: AccountsModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountsModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,firstName,lastName,phoneNumber,gender,address,city,state,zipCode,birthDate,email,userName")] AccountsModel accountsModel)
        {
            if (ModelState.IsValid)
            {
                db.Account.Add(accountsModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountsModel);
        }

        // GET: AccountsModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountsModel accountsModel = db.Account.Find(id);
            if (accountsModel == null)
            {
                return HttpNotFound();
            }
            return View(accountsModel);
        }

        // POST: AccountsModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,firstName,lastName,phoneNumber,gender,address,city,state,zipCode,birthDate,email,userName")] AccountsModel accountsModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountsModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountsModel);
        }

        // GET: AccountsModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountsModel accountsModel = db.Account.Find(id);
            if (accountsModel == null)
            {
                return HttpNotFound();
            }
            return View(accountsModel);
        }

        // POST: AccountsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountsModel accountsModel = db.Account.Find(id);
            db.Account.Remove(accountsModel);
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
    }
}
