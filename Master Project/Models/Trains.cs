using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Master_Project.Models
{
    // Dane dotyczące pociągów w bazie pojazdów (ID pociągu, Oznaczenie serii, numer inwertarzowy, Nazwa bazy/zajezdni pociągu)
    [Table("Trains")]
    public class Trains
    {
        [Key]
        public int Id { get; set; }
        public string SeriaPociągu { get; set; }
        public string NumerPociągu { get; set; }
        public string Stacja { get; set; }
        public string Rewizja { get; set; }
        public string Adnotacje { get; set; }
        public string Foto { get; set; }
        public virtual List<Trains> Train { get; set; }
    }
}