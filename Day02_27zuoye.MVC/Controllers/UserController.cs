using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using DAL;
using Model;

namespace Day02_27zuoye.MVC.Controllers
{
    public class UserController : Controller
    {
        UserDAL dal = new UserDAL();
        public ActionResult Show()
        {
            return View(dal.Show("", 1, 3));
        }
        [HttpPost]
        public ActionResult Show(string name = "", int index = 1)
        {

            return View(dal.Show(name, index, 3));
        }
    }
}