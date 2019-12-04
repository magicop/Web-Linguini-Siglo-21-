using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebLinguini.Models.ViewModel
{
    public class DetalleCartaViewModel
    {
        [JsonProperty("LstCartas")]
        public SelectList LstCartas { get; set; }

        [JsonProperty("nombreCarta")]
        public string nombreCarta { get; set; }

        [JsonProperty("idDetalleCarta")]
        public int idDetalleCarta { get; set; }

        #region Constructor para inicializar el combobox
        public DetalleCartaViewModel()
        {
            var _rest = new ApiRestful();
            var lstInfo = _rest.listarDetalleCarta();
            LstCartas = new SelectList(lstInfo, "idDetalleCarta", "nombreCarta");

        }
        #endregion
    }
}