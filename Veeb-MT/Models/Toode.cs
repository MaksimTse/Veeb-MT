﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Veeb_MT.Models
{
    public class Toode
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        public double Price { get; set; }
        public bool Aktiivne { get; set; }

        public Toode(int id, string nimi, double price, bool aktiivne)
        {
            Id = id;
            Nimi = nimi;
            Price = price;
            Aktiivne = aktiivne;
        }
    }

}
