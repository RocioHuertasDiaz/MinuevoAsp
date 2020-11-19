using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mi_primer_ASP.Models;


namespace Mi_primer_ASP.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.producto.ToList());
            }
          
        }
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create(producto newProduct)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities1())
                {
                    db.producto.Add(newProduct);
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
        public ActionResult edit(producto updateProduct)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    producto objProduct = db.producto.Find(updateProduct.id);
                    objProduct.nombre = updateProduct.nombre;
                    objProduct.percio_unitario = updateProduct.percio_unitario;
                    objProduct.descripcion = updateProduct.descripcion;
                    objProduct.cantidad = updateProduct.cantidad;
                    objProduct.id_proveedor = updateProduct.id_proveedor;
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
        public static string NombreProveedor(int? id_proveedor)
        {
            using (var db = new inventarioEntities1())
            {
                return db.proveedor.Find(id_proveedor).nombre;
            }

        }
        public ActionResult ListProvider()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.proveedor.ToList());
            }
        }
        public ActionResult Details(int id)
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
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    var findproduct = db.producto.Find(id);
                    db.producto.Remove(findproduct);
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