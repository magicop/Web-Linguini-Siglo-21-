﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLinguini.Models;
using WebLinguini.Models.DTO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;

namespace WebLinguini.Controllers
{
    public class PedidoProveedorController : Controller
    {
        private ApiRestful pedprovApiController = new ApiRestful();

        #region Listar
        // GET: PedidoProveedor
        public ActionResult Listar()
        {
            List<PedidoProveedor> model = pedprovApiController.listarPedidosProveedores();

            ViewBag.data = model;

            return View();

        }
        #endregion

        #region Formularios
        public ActionResult FormBuscar()
        {
            return View(new PedidoProveedor());
        }
        #endregion

        #region Buscar
        [HttpPost]
        public ActionResult Buscar(PedidoProveedor c)
        {
            try
            {
                //ViewBag.Result1 = categoriaApiClienat.categorias();

                PedidoProveedor model = pedprovApiController.buscarPedProvPorOrdenCompra(c);

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

        #region Exportar pdf
        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(PedidoProveedor c)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(c.Grid);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Listado-PedidoProvs.pdf");
            }
        }
        #endregion

    }
}