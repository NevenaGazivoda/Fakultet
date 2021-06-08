using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakultetAPI.Models
{
    public class Student
    {
        public int PKStudentId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int BrIndexa { get; set; }
    }
}