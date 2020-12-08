using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Orders.DAL;
using Orders.Models;

namespace Orders.Controllers
{
    public class OrdersController : Controller
    {
        private OrdersContext db = new OrdersContext();

        // GET: Orders
        public ActionResult Index(string number, string date, string itemName, string unit, string provider)
        {
            var orders = db.Orders.Include(o => o.Provider);

            ViewBag.NumberFilter = new SelectList((from c in db.Orders select c.Number).Distinct());
            ViewBag.DateFilter = new SelectList((from c in db.Orders select c.Date).Distinct());
            ViewBag.ItemNameFilter = new SelectList((from c in db.OrderItems select c.Name).Distinct());
            ViewBag.UnitFilter = new SelectList((from c in db.OrderItems select c.Unit).Distinct());
            ViewBag.ProviderFilter = new SelectList((from c in db.Providers select c.Name).Distinct());

            if (!string.IsNullOrEmpty(number))
            {
                orders = orders.Where(s => s.Number.Equals(number));
            }
            if (!string.IsNullOrEmpty(date))
            {
                DateTime fdate = DateTime.Parse(date);
                orders = orders.Where(s => s.Date.Equals(fdate));
            }
            if (!string.IsNullOrEmpty(provider))
            {
                orders = orders.Where(s => s.Provider.Name.Equals(provider));
            }
            if (!string.IsNullOrEmpty(itemName))
            {
                orders = orders.Where(s => s.OrderItems.Any(p => p.Name.Equals(itemName)));
            }
            if (!string.IsNullOrEmpty(unit))
            {
                orders = orders.Where(s => s.OrderItems.Any(p => p.Unit.Equals(unit)));
            }
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.ProviderId = new SelectList(db.Providers, "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Number,Date,ProviderId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProviderId = new SelectList(db.Providers, "Id", "Name", order.ProviderId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProviderId = new SelectList(db.Providers, "Id", "Name", order.ProviderId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Number,Date,ProviderId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProviderId = new SelectList(db.Providers, "Id", "Name", order.ProviderId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult InsertOrderItem(OrderItem newItem)
        {
            db.OrderItems.Add(newItem);
            db.SaveChanges();
            return Json(newItem);
        }

        [HttpPost]
        public ActionResult UpdateOrderItem(OrderItem newItem)
        {
            OrderItem oldItem = (from c in db.OrderItems where c.Id == newItem.Id select c).FirstOrDefault();
            oldItem.Name = newItem.Name;
            oldItem.Quantity = newItem.Quantity;
            oldItem.Unit = newItem.Unit;
            db.SaveChanges();
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult DeleteOrderItem(int itemId)
        {
            OrderItem itemToRemove = (from c in db.OrderItems where c.Id == itemId select c).FirstOrDefault();
            db.OrderItems.Remove(itemToRemove);
            db.SaveChanges();
            return new EmptyResult();
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
