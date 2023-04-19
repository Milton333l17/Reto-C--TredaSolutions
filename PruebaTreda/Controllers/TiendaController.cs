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

    public class TiendaController : ControllerBase
    {
        private readonly pruebaContext DBContext;

        public TiendaController(pruebaContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetTiendas")]
        public async Task<ActionResult<List<TiendaDTO>>> Get()
        {
            var List = await DBContext.Tiendas.Select(
                s => new TiendaDTO
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    FechaApertura = s.FechaApertura
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

        [HttpGet("GetTiendaById")]
        public async Task<ActionResult<TiendaDTO>> GetTiendaById(int Id)
        {
            TiendaDTO tienda = await DBContext.Tiendas.Select(
                    s => new TiendaDTO
                    {
                        Id = s.Id,
                        Nombre = s.Nombre,
                        FechaApertura = s.FechaApertura
                    })
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (tienda == null)
            {
                return NotFound();
            }
            else
            {
                return tienda;
            }
        }

        [HttpPost("InsertTienda")]
        public async Task<HttpStatusCode> InsertTienda(TiendaDTO tienda)
        {
            var entity = new Tienda()
            {
                Nombre = tienda.Nombre,
                FechaApertura = tienda.FechaApertura
            };

            DBContext.Tiendas.Add(entity);
            await DBContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateTienda")]
        public async Task<HttpStatusCode> UpdateTienda(TiendaDTO tienda)
        {
            var entity = await DBContext.Tiendas.FirstOrDefaultAsync(s => s.Id == tienda.Id);

            entity.Nombre = tienda.Nombre;
            entity.FechaApertura = tienda.FechaApertura;
               

            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteTienda/{Id}")]
        public async Task<HttpStatusCode> DeleteTienda(int Id)
        {
            var entity = new Tienda()
            {
                Id = Id
            };
            DBContext.Tiendas.Attach(entity);
            DBContext.Tiendas.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }

}
