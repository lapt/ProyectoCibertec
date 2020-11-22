using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCibertec.Models.Dto
{
    public class RegistroOrderDto
    {
        public RegistroOrderDto()
        {
            LstOrder = new List<Models.OrdenDetalle>();
        }
        public string StrNroDoc { get; set; }
        public DateTime DtFecha { get; set; }
        public List<Models.OrdenDetalle> LstOrder { get; set; }
        public int IdProducto { get; set; }
    }
}