using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Veeb_MT.Models
{
    public class Kasutaja
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Parool { get; set; }
        public string Eesnimi { get; set; }
        public string Perenimi { get; set; }

        public List<Ostukorv> Orders { get; set; }

        public Kasutaja(int id, string nimi, string parool, string eesnimi, string perenimi)
        {
            Id = id;
            Nimi = nimi;
            Parool = parool;
            Eesnimi = eesnimi;
            Perenimi = perenimi;
            Orders = new List<Ostukorv>();
        }
    }
}

