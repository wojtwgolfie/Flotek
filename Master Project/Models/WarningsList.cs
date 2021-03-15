using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Master_Project.Models
{
    [Table("Warningslist")]
    public class WarningsList
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Od { get; set; }
        public string Do { get; set; }
        public string Odcinek { get; set; }
        public string Szczegóły { get; set; }
        public string AdnotacjeDyspozytora { get; set; }
    }
}