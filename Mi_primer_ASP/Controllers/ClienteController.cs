using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.IO;
using System.Web.Mvc;
using Mi_primer_ASP.Models;
using Rotativa;


namespace Mi_primer_ASP.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.cliente.ToList());
            }
        }
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create(cliente newClient)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities1())
                {
                    db.cliente.Add(newClient);
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
                    cliente findClient = db.cliente.Where(a => a.id == id).FirstOrDefault();
                    return View(findClient);
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
        public ActionResult edit(cliente updateClient)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    cliente objclient = db.cliente.Find(updateClient.id);
                    objclient.nombre = updateClient.nombre;
                    objclient.documento = updateClient.documento;
                    objclient.email = updateClient.email;
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
                    cliente findClient = db.cliente.Where(a => a.id == id).FirstOrDefault();
                    return View(findClient);
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
                    cliente findClient = db.cliente.Find(id);
                    db.cliente.Remove(findClient);
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

        public ActionResult ReporteClienteCompra()
        {
            var db = new inventarioEntities1();
            {
                var query = from cliente in db.cliente
                            join compra in db.compra on cliente.id equals compra.id
                            select new ClienteCompra
                            {
                                nombreCliente = cliente.nombre,
                                emailCliente = cliente.email,
                                FechaCompra = compra.fecha,
                                TotalCompra = compra.total,
                                idUsuario = compra.id
                            };
                return View(query);
            }

        }
        
        public ActionResult uploadCSV()
        {
            return View();
        }
        [HttpPost]

        public ActionResult uploadCSV(HttpPostedFileBase fileForm)
        {
            string filePath = string.Empty;

            if (fileForm != null)
            {
                string path = Server.MapPath("~/uploads/");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(fileForm.FileName);
                string extension = Path.GetExtension(fileForm.FileName);
                fileForm.SaveAs(filePath);

                String csvData = System.IO.File.ReadAllText(filePath);
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        var newCliente = new cliente
                        {
                            id = Convert.ToInt32(row.Split(';')[0]),
                            nombre = row.Split(';')[1],
                            documento = row.Split(';')[2],
                            email = row.Split(';')[3],
                           

                        };
                        using (var db = new inventarioEntities1())
                        {
                            db.cliente.Add(newCliente);
                            db.SaveChanges();

                        }
                    }
                }
            }
            return View();
        }
        public ActionResult DescargaCliente()
        {
            using(var db = new inventarioEntities1())
            {
                return View(db.cliente.ToList());

            }
        }
        public ActionResult ImprimirReporteCliente()
        {
            return new ActionAsPdf("DescargaCliente") { FileName = "ReporteCliente.pdf" };
        }
    }
}