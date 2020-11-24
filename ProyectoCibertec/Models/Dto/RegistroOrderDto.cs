using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoCibertec.Models.Dto
{
    public class RegistroOrderDto
    {
        public RegistroOrderDto()
        {
            LstOrder = new List<Detalle>();
        }
        [DisplayName("Nro. DOcumento")]
        public string StrNroDoc { get; set; }
        [DisplayName("Fecha")]
        [DataType(DataType.Date)]
        public DateTime DtFecha { get; set; }
        public List<Detalle> LstOrder { get; set; }
        [DisplayName("Id Producto")]
        public int IdProducto { get; set; }
        [DisplayName("Cantidad")]
        public int DblCantidad { get; set; }
    }
}