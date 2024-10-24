using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veeb_MT.Data;
using Veeb_MT.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Veeb_MT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TootedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TootedController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET /tooted
        public async Task<List<Toode>> Get()
        {
            return await _context.Tooted.ToListAsync();
        }

        // GET /tooted/kustuta/1
        [HttpDelete("kustuta/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var toode = await _context.Tooted.FindAsync(id);
            if (toode == null)
            {
                return NotFound("Toodet ei leitud.");
            }

            _context.Tooted.Remove(toode);
            await _context.SaveChangesAsync();

            return Ok(await _context.Tooted.ToListAsync());
        }

        // PUT /tooted/uuenda/6/Pepsi/4/true
        // добавление нового или обновление существующего товара
        [HttpPut("uuenda/{id}/{nimi}/{hind}/{aktiivne}")]
        public async Task<ActionResult<List<Toode>>> Update(int id, string nimi, double hind, bool aktiivne)
        {
            var existingToode = await _context.Tooted.FindAsync(id);

            if (existingToode != null)
            {
                existingToode.Nimi = nimi;
                existingToode.Price = hind;
                existingToode.IsActive = aktiivne;
            }
            else
            {
                Toode toode = new Toode(id, nimi, hind, aktiivne);
                _context.Tooted.Add(toode);
            }

            await _context.SaveChangesAsync();
            return Ok(await _context.Tooted.ToListAsync());
        }

        // GET /tooted/lisa?id=1&nimi=Koola&hind=1.5&aktiivne=true
        [HttpGet("lisa")]
        public async Task<ActionResult<List<Toode>>> Add2([FromQuery] int id, [FromQuery] string nimi, [FromQuery] double hind, [FromQuery] bool aktiivne)
        {
            Toode toode = new Toode(id, nimi, hind, aktiivne);
            _context.Tooted.Add(toode);
            await _context.SaveChangesAsync();
            return Ok(await _context.Tooted.ToListAsync());
        }

        // GET /tooted/hind-dollaritesse/1.5
        [HttpGet("hind-dollaritesse/{kurss}")]
        public async Task<ActionResult<List<Toode>>> Dollaritesse(double kurss)
        {
            var tooted = await _context.Tooted.ToListAsync();
            foreach (var toode in tooted)
            {
                toode.Price = toode.Price * kurss;
            }

            await _context.SaveChangesAsync();
            return Ok(tooted);
        }

        // GET /tooted/kustuta-koik
        [HttpDelete("kustuta-koik")]
        public async Task<ActionResult> DeleteAll()
        {
            var allTooted = await _context.Tooted.ToListAsync();
            _context.Tooted.RemoveRange(allTooted);
            await _context.SaveChangesAsync();
            return Ok("Kõik tooted kustutatud!");
        }

        // GET /tooted/muuda-aktiivsus-valeks
        [HttpPut("muuda-aktiivsus-valeks")]
        public async Task<ActionResult<List<Toode>>> DeactivateAll()
        {
            var tooted = await _context.Tooted.ToListAsync();
            foreach (var toode in tooted)
            {
                toode.IsActive = false;
            }

            await _context.SaveChangesAsync();
            return Ok(tooted);
        }

        // GET /tooted/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Toode>> GetToodeById(int id)
        {
            var toode = await _context.Tooted.FindAsync(id);
            if (toode == null)
            {
                return NotFound("Toodet ei leitud.");
            }
            return Ok(toode);
        }

        // GET /tooted/korgeim-hind
        [HttpGet("korgeim-hind")]
        public async Task<ActionResult<Toode>> GetMostExpensiveToode()
        {
            var kallimToode = await _context.Tooted.OrderByDescending(t => t.Price).FirstOrDefaultAsync();
            if (kallimToode == null)
            {
                return NotFound("Tooteid pole saadaval.");
            }
            return Ok(kallimToode);
        }

        // POST /tooted/ostu-soorita
        [HttpPost("ostu-soorita")]
        public async Task<IActionResult> Purchase([FromBody] List<OstukorvToode> cartItems, [FromQuery] int kasutajaId)
        {
            var user = await _context.Kasutajad.FirstOrDefaultAsync(u => u.Id == kasutajaId);

            if (user == null)
            {
                return Unauthorized("Kasutajat ei leitud.");
            }

            var newOrder = new Ostukorv
            {
                KasutajaId = kasutajaId,
                OrderDate = DateTime.Now,
                Items = new List<OstukorvToode>()
            };

            foreach (var item in cartItems)
            {
                var existingToode = await _context.Tooted.FindAsync(item.ToodeId);
                if (existingToode != null)
                {
                    var existingOrderItem = newOrder.Items.FirstOrDefault(oi => oi.ToodeId == item.ToodeId);
                    if (existingOrderItem != null)
                    {
                        existingOrderItem.Quantity += item.Quantity;
                        existingOrderItem.TotalPrice = existingOrderItem.Quantity * existingToode.Price;
                    }
                    else
                    {
                        newOrder.Items.Add(new OstukorvToode
                        {
                            ToodeId = item.ToodeId,
                            Quantity = item.Quantity,
                            TotalPrice = item.Quantity * existingToode.Price
                        });
                    }
                }
            }

            _context.Ostukorvid.Add(newOrder);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Ost sooritatud!" });
        }
    }
}
