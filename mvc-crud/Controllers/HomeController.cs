using mvc_crud.db;
using mvc_crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using login = mvc_crud.Models.login;

namespace mvc_crud.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult logoff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(login logobj)
        {
            newprojectEntities obj = new newprojectEntities();
            var user = obj.logins.Where(a => a.email == logobj.email).FirstOrDefault();
            if(user==null)
            {
                TempData["in"] = "Email  Not  Found Or Invalid  User";
            }
            else
            {
                if(user.email==logobj.email && user.password==logobj.password)
                {
                    FormsAuthentication.SetAuthCookie(user.email, false);
                    Session["email"] = user.email;
                    return RedirectToAction("Indexdashboard", "Home");
                }
                else
                {
                    TempData["ro"] = "Wrong Email Or Password";
                    return View();
                }
            }

            return View();
        }
        [Authorize]
        //mytable1
        public ActionResult mytable1()
        {
            newprojectEntities obj = new newprojectEntities();
            List<Class1> clobj = new List<Class1>();
            var res = obj.newtables.ToList();
            foreach (var item in res)
            {
                clobj.Add(new Class1
                {

                    id = item.id,
                    name = item.name,
                    email = item.email,
                    city = item.city,
                    college = item.college
                });
            }

            return View(clobj);
        }
        [Authorize]
        public ActionResult Indexdashboard()
        {


            return View();
        }
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
         

            return View(   );
        }
        [Authorize]
        //my form
        [HttpGet]
        public ActionResult myform()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult myform(Class1 objnew)
        {
            newprojectEntities obj = new newprojectEntities();
            newtable obj1 = new newtable();

            obj1.id = objnew.id;
            obj1.name = objnew.name;
            obj1.email = objnew.email;
            obj1.city = objnew.city;
            obj1.college = objnew.college;

            if (objnew.id == 0)
            {
                obj.newtables.Add(obj1);
                obj.SaveChanges();
            }
            else
            {
                obj.Entry(obj1).State = System.Data.Entity.EntityState.Modified;
                obj.SaveChanges();
            }


            return RedirectToAction("mytable1");
            // return View();

        }
        /*  public ActionResult mytable()
          {
              newprojectEntities obj = new newprojectEntities();
              List<Class1> clobj = new List<Class1>();
              var res = obj.newtables.ToList();
              foreach (var item in res)
              {
                  clobj.Add(new Class1
                  {

                      id = item.id,
                      name = item.name,
                      email = item.email,
                      city = item.city,
                      college = item.college
                  });
              }

              return View(clobj);
          }*/
        [Authorize]
        public ActionResult delete(int id)
        {
            newprojectEntities obj = new newprojectEntities();
            newtable deleteitem = obj.newtables.Where(x => x.id == id).First();
            obj.newtables.Remove(deleteitem);
            obj.SaveChanges();

            return RedirectToAction("mytable1");
        }

        [Authorize]
        public ActionResult edit(int id)
        {
            Class1 obj2 = new Class1();
            newprojectEntities obj = new newprojectEntities();
            var edititem = obj.newtables.Where(x => x.id == id).First();

            obj2.id = edititem.id;
            obj2.name = edititem.name;
            obj2.email = edititem.email;
            obj2.city = edititem.city;
            obj2.college = edititem.college;

            ViewBag.id = edititem.id;

            return View("myform", obj2);
        }

    }
}