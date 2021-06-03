using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakultetAPI.Models
{
    public class Profesor
    {
        public int PKProfesorId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int GodRodjenja { get; set; }
        public string Titula { get; set; }

    }
}