using System;
using System.Collections.Generic;
using System.Text;

namespace Tiendita.Models
{
    class Venta
    {


        public uint Id { get; set; }
        public decimal Total { get; set; }
        public DateTime fecha { get; set; }
        public string Cliente { get; set; }
    }
}
