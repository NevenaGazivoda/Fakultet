using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakultetAPI.Models
{
    public class Predmet
    {
        public int PKPredmetId { get; set; }
        public string Naziv { get; set; }
        public int Godina { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
    }
}