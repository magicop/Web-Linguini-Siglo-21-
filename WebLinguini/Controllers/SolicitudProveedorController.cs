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
    public class SolicitudProveedorController : Controller
    {

        private ApiRestful solprovApiClient = new ApiRestful();
        static HttpClient client = new HttpClient();

        // GET: SolicitudProveedor
        public ActionResult Listar()
        {

            //ViewBag.Result1 = categoriaApiClient.categorias();

            List<SolicitudProveedor> model = solprovApiClient.listarSolicitudProveedores();

            ViewBag.data = model;

            return View();
        }

        #region Formularios
        public ActionResult FormAgregar()
        {
            return View();
        }

        public ActionResult FormBuscar()
        {
            return View(new SolicitudProveedor());
        }
        #endregion

        #region Buscar
        [HttpPost]
        public ActionResult Buscar(SolicitudProveedor c)
        {
            try
            {
                //ViewBag.Result1 = categoriaApiClienat.categorias();

                SolicitudProveedor model = solprovApiClient.buscarSolProv(c);

                if (model == null)
                {
                    ViewBag.error = "si";
                    ViewBag.error2 = "No se ha encontrado la solicitud del proveedor.";
                }
                else
                {
                    ViewBag.error = "no";
                    ViewBag.data = model;
                }


                return View();
            }
            catch
            {
                return RedirectToAction("Error");
            }

        }


        #endregion


    }
}