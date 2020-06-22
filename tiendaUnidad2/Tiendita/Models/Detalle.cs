using System;
using System.Collections.Generic;
using System.Text;

namespace Tiendita.Models
{
    class Detalle
    {
        public uint Id { get; set; }
        public uint ProductoId { get; set; }
        public Producto Producto { get; set; }
        public uint VentaId { get; set; }
        public Venta Venta { get; set; }
        public decimal Subtotal { get; set; }
        public virtual ICollection<Detalle> Detalles {get; set;}

        public override string ToString()
        {
            return $"{Id}) Id de Producto: {ProductoId} Id de Venta: {VentaId} Subtotal: {Subtotal}MXN" ;
        }
    }
}
