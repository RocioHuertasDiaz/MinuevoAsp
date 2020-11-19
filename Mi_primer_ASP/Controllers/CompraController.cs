using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mi_primer_ASP.Models;

namespace Mi_primer_ASP.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        [Authorize]
        public ActionResult Index()
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.compra.ToList());
            }
            
        }
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create(compra newPurchase)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities1())
                {
                    db.compra.Add(newPurchase);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
                throw;
            }

        }
        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    compra findPurchase = db.compra.Where(a => a.id == id).FirstOrDefault();
                    return View(findPurchase);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit(compra updatePurchase)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    compra objPurchase = db.compra.Find(updatePurchase.id);
                    objPurchase.fecha = updatePurchase.fecha;
                    objPurchase.total = updatePurchase.total;
                    objPurchase.id_usuario = updatePurchase.id_usuario;
                    objPurchase.id_cliente = updatePurchase.id_cliente;
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
                throw;
            }
        }
        public static string NombreUsuario(int? id_usuario)
        {
            using (var db = new inventarioEntities1())
            {
                return db.usuario.Find(id_usuario).nombre;
            }

        }
        public ActionResult ListUser()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.usuario.ToList());
            }
        }
        public static string NombreCliente(int? id_cliente)
        {
            using (var db = new inventarioEntities1())
            {
                return db.cliente.Find(id_cliente).nombre;
            }

        }
        public ActionResult ListClient()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.cliente.ToList());
            }
        }
        public ActionResult Details(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    compra findpurchase = db.compra.Where(a => a.id == id).FirstOrDefault();
                    return View(findpurchase);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
                throw;
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    var findpurchase = db.compra.Find(id);
                    db.compra.Remove(findpurchase);
                    db.SaveChanges();
                    return RedirectToAction("index");

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
                throw;
            }
        }

    }
}