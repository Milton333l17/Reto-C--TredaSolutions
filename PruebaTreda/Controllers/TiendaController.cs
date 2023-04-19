using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTreda.DTO;
using PruebaTreda.Entities;
using Serilog;
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
            Log.Information("Start - EntPoint GetTiendas");

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
                Log.Information("Finish - EntPoint GetTiendas");
                return List;
            }
        }

        [HttpGet("GetTiendaById")]
        public async Task<ActionResult<TiendaDTO>> GetTiendaById(int Id)
        {
            Log.Information("Start - EntPoint GetTiendaById");
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
                Log.Information("Finish - EntPoint GetTiendaById");
                return tienda;
            }
        }

        [HttpPost("InsertTienda")]
        public async Task<HttpStatusCode> InsertTienda(TiendaDTO tienda)
        {
            Log.Information("Start - EntPoint InsertTienda");
            var entity = new Tienda()
            {
                Nombre = tienda.Nombre,
                FechaApertura = tienda.FechaApertura
            };

            DBContext.Tiendas.Add(entity);
            await DBContext.SaveChangesAsync();

            Log.Information("Finish - EntPoint InsertTienda");
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateTienda")]
        public async Task<HttpStatusCode> UpdateTienda(TiendaDTO tienda)
        {
            Log.Information("Start - EntPoint UpdateTienda");
            var entity = await DBContext.Tiendas.FirstOrDefaultAsync(s => s.Id == tienda.Id);

            entity.Nombre = tienda.Nombre;
            entity.FechaApertura = tienda.FechaApertura;
               

            await DBContext.SaveChangesAsync();
            Log.Information("Finish - EntPoint UpdateTienda");
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteTienda/{Id}")]
        public async Task<HttpStatusCode> DeleteTienda(int Id)
        {
            Log.Information("Start - EntPoint DeleteTienda");
            var entity = new Tienda()
            {
                Id = Id
            };
            DBContext.Tiendas.Attach(entity);
            DBContext.Tiendas.Remove(entity);
            await DBContext.SaveChangesAsync();
            Log.Information("Finish - EntPoint DeleteTienda");
            return HttpStatusCode.OK;
        }
    }

}
