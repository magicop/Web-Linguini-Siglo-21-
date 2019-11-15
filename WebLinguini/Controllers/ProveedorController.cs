using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLinguini.Models;
using WebLinguini.Models.DTO;

namespace WebLinguini.Controllers
{
    public class ProveedorController : Controller
    {
        private ApiRestful provApiController = new ApiRestful();

        // GET: Proveedor
        public ActionResult Listar()
        {
            List<Proveedor> model = provApiController.listarProveedores();

            ViewBag.data = model;

            return View();

        }


        #region Formularios

        public ActionResult FormBuscar()
        {
            return View(new Proveedor());
        }
        #endregion

        #region Buscar
        [HttpPost]
        public ActionResult Buscar(Proveedor c)
        {
            try
            {
                //ViewBag.Result1 = categoriaApiClienat.categorias();

                Proveedor model = provApiController.buscarProveedor(c);

                if (model == null)
                {
                    ViewBag.error = "si";
                    ViewBag.error2 = "No se ha encontrado el pedido de proveedor.";
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