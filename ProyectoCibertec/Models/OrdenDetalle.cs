//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoCibertec.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class OrdenDetalle
    {
        public int idOrdenDetalle { get; set; }
        public Nullable<int> Orden_idOrden { get; set; }
        public Nullable<int> Producto_idProducto { get; set; }
        [DisplayName("Cantidad")]
        public Nullable<decimal> dblCantidad { get; set; }
    
        public virtual Orden Orden { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
