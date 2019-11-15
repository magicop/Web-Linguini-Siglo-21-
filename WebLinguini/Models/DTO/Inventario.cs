using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLinguini.Models.DTO
{
    public class Inventario
    {
        [JsonProperty("idInventario")]
        public int idInventario { get; set; }

        [JsonProperty("stock")]
        public int stock { get; set; }

        [JsonProperty("idBodega")]
        public int idBodega { get; set; }
    }
}