using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebLinguini.Models;
using WebLinguini.Models.DTO;

namespace WebLinguini.Controllers
{
    public class ReservaController : Controller
    {

        private ApiRestful reservaApiClient = new ApiRestful();
        static HttpClient client = new HttpClient();

        // GET: Reserva
        public ActionResult Listar()
        {

            //ViewBag.Result1 = categoriaApiClient.categorias();

            List<Reservas> model = reservaApiClient.listarReservas();

            ViewBag.data = model;

            return View();
        }




    }
}