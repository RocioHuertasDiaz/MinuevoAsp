using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Mi_primer_ASP.Models;

namespace Mi_primer_ASP.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.usuario.ToList());
            }

        }

        [Authorize]
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult create(usuario newUser)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities1())
                {
                    db.usuario.Add(newUser);
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
        [Authorize]
        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    usuario findUser = db.usuario.Where(a => a.id == id).FirstOrDefault();
                    return View(findUser);
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
        public ActionResult edit(usuario updateUser)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    usuario objuser = db.usuario.Find(updateUser.id);
                    objuser.nombre = updateUser.nombre;
                    objuser.apellido = updateUser.apellido;
                    objuser.fecha_nacimiento = updateUser.fecha_nacimiento;
                    objuser.email = updateUser.email;
                    objuser.password = updateUser.password;
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
                    usuario findUser = db.usuario.Where(a => a.id == id).FirstOrDefault();
                    return View(findUser);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
                throw;
            }
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                using(var db = new inventarioEntities1())
                {
                    usuario findUser = db.usuario.Find(id);
                    db.usuario.Remove(findUser);
                    db.SaveChanges();
                    return RedirectToAction("index");

                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
                throw;
            }
        }

        public ActionResult Login(string mesagge ="")
        {
            ViewBag.message = mesagge;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string user, string password)
        {
            using (var db = new inventarioEntities1())
            {         
                var userlogin = db.usuario.FirstOrDefault(e => e.email == user && e.password == password);
                if (userlogin != null)
                {
                    FormsAuthentication.SetAuthCookie(userlogin.email, true);
                    return RedirectToAction("Index", "Producto");
                }
                else
                {
                    return Login("Verifique sus datos");
                }
            }
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}

