using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mi_primer_ASP.Models
{
    public class ClienteCompra
    {
        public string nombreCliente { get; set; }
        public string emailCliente { get; set; }
        public Nullable<System.DateTime> FechaCompra { get; set; }
        public Nullable<int> TotalCompra { get; set; }
        public int idUsuario { get; set; }

    }
}