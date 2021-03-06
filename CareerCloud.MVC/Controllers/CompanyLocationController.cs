﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;

namespace CareerCloud.MVC.Controllers
{
    public class CompanyLocationController : Controller
    {
        CompanyLocationLogic _logic = new CompanyLocationLogic(new EFGenericRepository<CompanyLocationPoco>(false));
        private CareerCloudContext db = new CareerCloudContext();

        // GET: CompanyLocation
        //[Route("CompanyLocation/Index")]
        //public ActionResult Index()
        //{
        //    var companyLocation = db.CompanyLocation.Include(c => c.CompanyProfile);
        //    return View(companyLocation.ToList());
        //}

        [Route("CompanyLocation/Index/companyId")]
        public ActionResult Index(Guid? companyId)
        {
            var companyLocation = db.CompanyLocation.Include(c => c.CompanyProfile);
            if (companyId != null)
                companyLocation = companyLocation.Where(cl=>cl.Company==companyId);
            return View(companyLocation.ToList());
        }

        // GET: CompanyLocation/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyLocationPoco companyLocationPoco =  db.CompanyLocation.Find(id);//  _logic.Get(id);  
            if (companyLocationPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyLocationPoco);
        }

        // GET: CompanyLocation/Create
        public ActionResult Create()
        {
            ViewBag.Company = new SelectList(db.CompanyProfile, "Id", "CompanyWebsite");
            return View();
        }

        // POST: CompanyLocation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CountryCode,Province,Street,City,PostalCode,TimeStamp,Company")] CompanyLocationPoco companyLocationPoco)
        {
            if (ModelState.IsValid)
            {
                companyLocationPoco.Id = Guid.NewGuid();
                //db.CompanyLocation.Add(companyLocationPoco);
                //db.SaveChanges();
                _logic.Add(new CompanyLocationPoco[] { companyLocationPoco });
                return RedirectToAction("Index");
            }

            ViewBag.Company = new SelectList(db.CompanyProfile, "Id", "CompanyWebsite", companyLocationPoco.Company);
            return View(companyLocationPoco);
        }

        // GET: CompanyLocation/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyLocationPoco companyLocationPoco = _logic.Get(id);  // db.CompanyLocation.Find(id);
            if (companyLocationPoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Company = new SelectList(db.CompanyProfile, "Id", "CompanyWebsite", companyLocationPoco.Company);
            return View(companyLocationPoco);
        }

        // POST: CompanyLocation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CountryCode,Province,Street,City,PostalCode,TimeStamp,Company")] CompanyLocationPoco companyLocationPoco)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(companyLocationPoco).State = EntityState.Modified;
                //db.SaveChanges();
                _logic.Update(new CompanyLocationPoco[] { companyLocationPoco });
                return RedirectToAction("Index");
            }
            ViewBag.Company = new SelectList(db.CompanyProfile, "Id", "CompanyWebsite", companyLocationPoco.Company);
            return View(companyLocationPoco);
        }

        // GET: CompanyLocation/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyLocationPoco companyLocationPoco = _logic.Get(id);  // db.CompanyLocation.Find(id);
            if (companyLocationPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyLocationPoco);
        }

        // POST: CompanyLocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CompanyLocationPoco companyLocationPoco = _logic.Get(id);  // db.CompanyLocation.Find(id);
            //db.CompanyLocation.Remove(companyLocationPoco);
            //db.SaveChanges();
            _logic.Delete(new CompanyLocationPoco[] { companyLocationPoco });
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
