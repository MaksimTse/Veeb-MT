using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Veeb_MT.Models
{
    public class OstukorvToode
    {
        public int Id { get; set; }
        public int OstukorvId { get; set; }
        public Ostukorv Ostukorv { get; set; }

        public int ToodeId { get; set; }
        public Toode Toode { get; set; }

        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
}
