using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Camada.Aplicacao.Controllers
{
    public class ManualController : Controller
    {
        // GET: Manual
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cleaners()
        {
            return View();
        }

        public ActionResult FechamentoCaixa()
        {
            return View();
        }
    }
}