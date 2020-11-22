using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCibertec.Areas.Order.ViewModels
{
    public class RegistroOrderViewModel
    {
        public RegistroOrderViewModel()
        {
            LstOrder = new List<Models.OrdenDetalle>();
        }
        public string StrNroDoc { get; set; }
        public DateTime DtFecha { get; set; }
        public List<Models.OrdenDetalle> LstOrder { get; set; }
        public int IdProducto { get; set; }

    }
}