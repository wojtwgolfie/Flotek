using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Master_Project.Models
{
    [Table("Timetables")]
    public class Timetables
    {
        [Key]
        public int Id { get; set; }
        public string Kategoria { get; set; }
        public string Numer { get; set; }
        public string Nazwa { get; set; }
        public string StacjaPoczątkowa { get; set; }
        public string StacjaKońcowa { get; set; }
        public string Odjazd { get; set; }
        public string Przyjazd { get; set; }
        public string Obsługa { get; set; }
        public string Uwagi { get; set; }
        public virtual List<Timetables> Timetable { get; set; }
    }
}