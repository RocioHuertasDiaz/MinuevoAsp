using Mi_primer_ASP.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mi_primer_ASP.Controllers
{
    public class ProductoCompraController : Controller
    {
        // GET: ProductoCompra
        public ActionResult Index()
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.producto_compra.ToList());
            }

        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto_compra newProduct_Purchase)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities1())
                {
                    db.producto_compra.Add(newProduct_Purchase);
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
                    producto findProduct = db.producto.Where(a => a.id == id).FirstOrDefault();
                    return View(findProduct);
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
        public ActionResult edit(producto_compra updateProduct_Purchase)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    producto_compra objProductPurchase = db.producto_compra.Find(updateProduct_Purchase.id);
                    objProductPurchase.id_compra = updateProduct_Purchase.id_compra;
                    objProductPurchase.id_producto = updateProduct_Purchase.id_producto;
                    objProductPurchase.cantidad = updateProduct_Purchase.cantidad;
                    
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
        public static int Id_Compra(int? id_compra)
        {
            using (var db = new inventarioEntities1())
            {
                return db.compra.Find(id_compra).id;
            }

        }
        public static int Purchase(int? id_compra)
        {
            using (var db = new inventarioEntities1())
            {
                return db.compra.Find(id_compra).id;
            }

        }
        public ActionResult ListPurchase()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.compra.ToList());
            }
        }

        public static string Nombre_Producto(int? id_producto)
        {
            using (var db = new inventarioEntities1())
            {
                return db.producto.Find(id_producto).nombre;
            }

        }
        public static int Product(int? id_producto)
        {
            using (var db = new inventarioEntities1())
            {
                return db.compra.Find(id_producto).id;
            }

        }
        public ActionResult ListProduct()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.producto.ToList());
            }
        }
        public ActionResult Details(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    producto_compra findProductPurchase = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
                    return View(findProductPurchase);
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
                    var findproductPurchase = db.producto_compra.Find(id);
                    db.producto_compra.Remove(findproductPurchase);
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