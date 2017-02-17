using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Camada.Aplicacao.Controllers.ActionsFilters
{
    public class UtilsActionFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            //ViewBag.User = "";// coloque aqui a informação do usuário
            filterContext.Controller.ViewBag.User = "";
            this.OnActionExecuting(filterContext);
        }
    }
}