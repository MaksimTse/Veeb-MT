using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Veeb_MT.Models;

namespace Veeb_MT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TootedController : ControllerBase
    {
        private static List<Toode> _tooted = new List<Toode>{
        new Toode(1,"Koola", 1.5, true),
        new Toode(2,"Fanta", 1.0, false),
        new Toode(3,"Sprite", 1.7, true),
        new Toode(4,"Vichy", 2.0, true),
        new Toode(5,"Vitamin well", 2.5, true)
        };

        [HttpGet]

        // GET /tooted
        public List<Toode> Get()
        {
            return _tooted;
        }

        // GET /tooted/kustuta/1
        [HttpGet("kustuta/{index}")]
        public List<Toode> Delete(int index)
        {
            _tooted.RemoveAt(index-1);
            return _tooted;
        }

        // GET /tooted/kustuta2/1
        [HttpGet("kustuta2/{index}")]
        public string Delete2(int index)
        {
            _tooted.RemoveAt(index-1);
            return "Kustutatud!";
        }
        // GET /tooted/lisa/6/Pepsi/4/true
        [HttpGet("lisa/{id}/{nimi}/{hind}/{aktiivne}")]
        public List<Toode> Add(int id, string nimi, double hind, bool aktiivne)
        {
            Toode toode = new Toode(id, nimi, hind, aktiivne);
            _tooted.Add(toode);
            return _tooted;
        }

        [HttpGet("lisa")] // GET /tooted/lisa?id=1&nimi=Koola&hind=1.5&aktiivne=true
        public List<Toode> Add2([FromQuery] int id, [FromQuery] string nimi, [FromQuery] double hind, [FromQuery] bool aktiivne)
        {
            Toode toode = new Toode(id, nimi, hind, aktiivne);
            _tooted.Add(toode);
            return _tooted;
        }

        [HttpGet("hind-dollaritesse/{kurss}")] // GET /tooted/hind-dollaritesse/1.5
        public List<Toode> Dollaritesse(double kurss)
        {
            for (int i = 0; i < _tooted.Count; i++)
            {
                _tooted[i].Price = _tooted[i].Price * kurss;
            }
            return _tooted;
        }

        // või foreachina:

        [HttpGet("hind-dollaritesse2/{kurss}")] // GET /tooted/hind-dollaritesse2/1.5
        public List<Toode> Dollaritesse2(double kurss)
        {
            foreach (var t in _tooted)
            {
                t.Price = t.Price * kurss;
            }

            return _tooted;
        }

        [HttpDelete("kustuta-koik")]
        public List<Toode> DeleteAll()
        {
            _tooted.Clear();
            return _tooted;
        }

        [HttpPut("muuda-aktiivsus-valeks")]
        public List<Toode> DeactivateAll()
        {
            foreach (var t in _tooted)
            {
                t.IsActive = false;
            }
            return _tooted;
        }

        [HttpGet("{index}")]
        public ActionResult<Toode> GetToodeByIndex(int index)
        {
            if (index < 0 || index >= _tooted.Count)
            {
                return NotFound("Toodet ei leitud.");
            }
            return _tooted[index];
        }

        [HttpGet("korgeim-hind")]
        public ActionResult<Toode> GetMostExpensiveToode()
        {
            if (_tooted.Count == 0)
            {
                return NotFound("Tooteid pole saadaval.");
            }
            var kallimToode = _tooted.OrderByDescending(t => t.Price).FirstOrDefault();
            return kallimToode;
        }

    }
}
