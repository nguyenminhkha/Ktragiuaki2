using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Net.Http;

namespace WebApplication1.Controllers
{
    public class PhonesController : Controller
    {
        private PhoneAPIEntities db = new PhoneAPIEntities();

        // GET: Phones
        public async Task<ActionResult> Index()
        {

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:50305/");

            var response = httpClient.GetAsync("API/Phones");

            var result = response.Result;

            var readTask = result.Content.ReadAsAsync<List<Phone>>();

            List<Phone> phone = readTask.Result;

            return View(phone);
        }

        // GET: Phones/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:50305/");

            var response = httpClient.GetAsync("API/Phones/" + id);

            var result = response.Result;

            var readTask = result.Content.ReadAsAsync<Phone>();

            Phone phone = readTask.Result;

            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);

        }

        // GET: Phones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Model,Price,GeneralNote")] Phone phone)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:50305/");

            var response = httpClient.PostAsJsonAsync("API/Phones", phone);

            var result = response.Result;
            if (ModelState.IsValid)
            {
                //db.Phones.Add(phone);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phone);
        }

        // GET: Phones/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:50305/API/");

            var editTask = httpClient.GetAsync("Phones?id=" + id.ToString());

            var result = editTask.Result;

            var readTask = result.Content.ReadAsAsync<Phone>();

            Phone phone = readTask.Result;
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);

        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Model,Price,GeneralNote")] Phone phone)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:50305/");

            var response = httpClient.PutAsJsonAsync("API/Phones/" + phone.Id, phone);

            var result = response.Result;

            return RedirectToAction("Index");


            return View(phone);
        }

        // GET: Phones/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:50305/");

            var deleteTask = httpClient.DeleteAsync("API/Phones/" + id.ToString());

            var result = deleteTask.Result;

            return RedirectToAction("Index");
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Phone phone = null;
            //db.Phones.FindAsync(id);
            //db.Phones.Remove(phone);
            //await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               // db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
