using System;
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
using WebLinguini.Models.ViewModel;

namespace WebLinguini.Controllers
{
    public class DetalleCartaController : Controller
    {
        private ApiRestful detalleCartaApiClient = new ApiRestful();

        #region Listar
        // GET: DetalleCarta
        public ActionResult Listar()
        {
            List<DetalleCarta> model = detalleCartaApiClient.listarDetalleCarta();

            ViewBag.data = model;
            return View();
        }
        #endregion

        #region Formularios
        public ActionResult FormBuscar()
        {
            return View(new DetalleCartaViewModel());
        }
        #endregion

        #region Exportar pdf
        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(DetalleCarta c)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(c.Grid);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Listado-DetalleCarta.pdf");
            }
        }
        #endregion
    }
}