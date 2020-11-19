using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mi_primer_ASP.Models
{
    public class ProductoProveedor
    {
        public string nombreProducto { get; set; }
        public string nombreProveedor { get; set; }
        public string telefonoProveedor { get; set; }
        public Nullable<int> precioUnitario { get; set; }
        public string descripcion { get; set; }

    }
   
}