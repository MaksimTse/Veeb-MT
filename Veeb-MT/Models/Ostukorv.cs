using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Veeb_MT.Models
{
    public class Ostukorv
    {
        public int Id { get; set; }
        public int KasutajaId { get; set; }
        public Kasutaja Kasutaja { get; set; }
        public DateTime OrderDate { get; set; }

        // Связь с продуктами
        public List<OstukorvToode> Items { get; set; }

        public Ostukorv()
        {
            Items = new List<OstukorvToode>();
        }
    }
}

