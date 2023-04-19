using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTreda.DTO;
using PruebaTreda.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
