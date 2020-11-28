using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using Mi_primer_ASP.Models;

namespace Mi_primer_ASP.Controllers
{
    public class proveedorController : Controller
    {
        // GET: proveedor
        public ActionResult Index()
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.proveedor.ToList());
            }
        }
        public ActionResult create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create(proveedor newProvider)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities1())
                {
                    db.proveedor.Add(newProvider);
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
                    proveedor findProvider = db.proveedor.Where(a => a.id == id).FirstOrDefault();
                    return View(findProvider);
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
        public ActionResult edit(proveedor updateProvider)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    proveedor objProvider = db.proveedor.Find(updateProvider.id);
                    objProvider.nombre = updateProvider.nombre;
                    objProvider.direccion = updateProvider.direccion;
                    objProvider.telefono = updateProvider.telefono;
                    objProvider.nombre_contacto = updateProvider.nombre_contacto;

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
        public ActionResult Details(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    proveedor findProvider = db.proveedor.Where(a => a.id == id).FirstOrDefault();
                    return View(findProvider);
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
                    proveedor findProvider = db.proveedor.Find(id);
                    db.proveedor.Remove(findProvider);
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
        public ActionResult DetalleProveedorProducto()
        {
            var db = new inventarioEntities1();
            {
                var query = from proveedor in db.proveedor
                           join producto in db.producto on proveedor.id equals producto.id_proveedor
                           select new ProductoProveedor
                           {
                               nombreProveedor = proveedor.nombre,
                               nombreProducto = producto.nombre,
                               telefonoProveedor = proveedor.telefono,
                               descripcionProducto = producto.descripcion,
                               precioUnitario = producto.percio_unitario
                           };
                return View(query);
            }
           
        }
        

    }

    
}