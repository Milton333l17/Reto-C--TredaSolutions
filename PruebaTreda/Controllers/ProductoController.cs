using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTreda.DTO;
using PruebaTreda.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PruebaTreda.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductoController : ControllerBase
    {
        private readonly pruebaContext DBContext;

        public ProductoController(pruebaContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetProducto")]
        public async Task<ActionResult<List<ProductoDTO>>> Get()
        {
            var List = await DBContext.Productos.Select(
                s => new ProductoDTO
                {
                    Id = s.Id,
                    SKU= s.SKU,
                    Nombre = s.Nombre,
                    Descripcion=s.Descripcion,
                    Imagen=s.Imagen,
                    Valor=s.Valor,
                    TiendaId=s.TiendaId
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetProductoById")]
        public async Task<ActionResult<ProductoDTO>> GetProductoById(int Id)
        {
            ProductoDTO producto = await DBContext.Productos.Select(
                    s => new ProductoDTO
                    {
                        Id = s.Id,
                        SKU = s.SKU,
                        Nombre = s.Nombre,
                        Descripcion = s.Descripcion,
                        Imagen = s.Imagen,
                        Valor = s.Valor,
                        TiendaId = s.TiendaId
                    })
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (producto == null)
            {
                return NotFound();
            }
            else
            {
                return producto;
            }
        }

        [HttpPost("InsertProducto")]
        public async Task<HttpStatusCode> InsertProducto(ProductoDTO producto)
        {
            var entity = new Producto()
            {
                Id = producto.Id,
                SKU = producto.SKU,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Imagen = producto.Imagen,
                Valor = producto.Valor,
                TiendaId = producto.TiendaId
            };

            DBContext.Productos.Add(entity);
            await DBContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateProducto")]
        public async Task<HttpStatusCode> UpdateProducto(ProductoDTO producto)
        {
            var entity = await DBContext.Productos.FirstOrDefaultAsync(s => s.Id == producto.Id);

            entity.SKU = producto.SKU;
            entity.Nombre = producto.Nombre;
            entity.Descripcion = producto.Descripcion;
            entity.Imagen = producto.Imagen;
            entity.Valor = producto.Valor;
            entity.TiendaId = producto.TiendaId;


            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteProducto/{Id}")]
        public async Task<HttpStatusCode> DeleteProducto(int Id)
        {
            var entity = new Producto()
            {
                Id = Id
            };
            DBContext.Productos.Attach(entity);
            DBContext.Productos.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
