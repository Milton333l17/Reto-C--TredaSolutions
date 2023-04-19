using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTreda.DTO
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string SKU { get; set; }
        public string Descripcion { get; set; }
        public float Valor { get; set; }
        public int TiendaId { get; set; }
        public TiendaDTO Tienda { get; set; }
        public string Imagen { get; set; }
    }
}
