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
    public class CompanyJobController : Controller
    {
        CompanyJobLogic _logic = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>(false));
        private CareerCloudContext db = new CareerCloudContext();

        // GET: CompanyJob
        //[Route("CompanyJob/Index")]
        //public ActionResult Index()
        //{
        //    var companyJob = db.CompanyJob.Include(c => c.CompanyProfile);
        //    return View(companyJob.ToList());
        //}

        [Route("CompanyJob/Index/companyId")]        
        public ActionResult Index(Guid? companyId)
        {
            var companyJob = db.CompanyJob.Include(c => c.CompanyProfile);
            if (companyId!= null)
                 companyJob = companyJob.Where(cj=>cj.Company==companyId);
            return View(companyJob.ToList());
        }


        // GET: CompanyJob/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyJobPoco companyJobPoco = db.CompanyJob.Find(id); // _logic.Get(id);
            if (companyJobPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyJobPoco);
        }

        // GET: CompanyJob/Create
        public ActionResult Create()
        {
            ViewBag.Company = new SelectList(db.CompanyProfile, "Id", "CompanyWebsite");
            return View();
        }

        // POST: CompanyJob/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProfileCreated,IsInactive,IsCompanyHidden,TimeStamp,Company")] CompanyJobPoco companyJobPoco)
        {
            if (ModelState.IsValid)
            {
                companyJobPoco.Id = Guid.NewGuid();
                //db.CompanyJob.Add(companyJobPoco);
                //db.SaveChanges();
                _logic.Add(new CompanyJobPoco[] { companyJobPoco });
                return RedirectToAction("Index");
            }

            ViewBag.Company = new SelectList(db.CompanyProfile, "Id", "CompanyWebsite", companyJobPoco.Company);
            return View(companyJobPoco);
        }

        // GET: CompanyJob/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyJobPoco companyJobPoco = _logic.Get(id);  // db.CompanyJob.Find(id);
            if (companyJobPoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Company = new SelectList(db.CompanyProfile, "Id", "CompanyWebsite", companyJobPoco.Company);
            return View(companyJobPoco);
        }

        // POST: CompanyJob/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProfileCreated,IsInactive,IsCompanyHidden,TimeStamp,Company")] CompanyJobPoco companyJobPoco)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(companyJobPoco).State = EntityState.Modified;
                //db.SaveChanges();
                _logic.Update(new CompanyJobPoco[] { companyJobPoco });
                return RedirectToAction("Index");
            }
            ViewBag.Company = new SelectList(db.CompanyProfile, "Id", "CompanyWebsite", companyJobPoco.Company);
            return View(companyJobPoco);
        }

        // GET: CompanyJob/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyJobPoco companyJobPoco = _logic.Get(id);  // db.CompanyJob.Find(id);
            if (companyJobPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyJobPoco);
        }

        // POST: CompanyJob/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CompanyJobPoco companyJobPoco = _logic.Get(id); //  db.CompanyJob.Find(id);
            //db.CompanyJob.Remove(companyJobPoco);
            //db.SaveChanges();
            _logic.Delete(new CompanyJobPoco[] { companyJobPoco });
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
