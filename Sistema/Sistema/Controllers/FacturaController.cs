using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Sistema.Models;
using System.Data.Entity;
using System.Data;

namespace Sistema.Controllers
{
    public class FacturaController : Controller
    {
        private Prog3FinalEntities db = new Prog3FinalEntities();
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
    }
}