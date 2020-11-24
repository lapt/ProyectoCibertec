using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCibertec.Models.Dto
{
    public class Detalle
    {
        public int dblCantidad { get; set; }
        public int Producto_idProducto { get; set; }
        public string NameProduct { get; set; }
        public string StrNroDoc { get; set;}
        public DateTime DtFecha { get; set; }
    }
}