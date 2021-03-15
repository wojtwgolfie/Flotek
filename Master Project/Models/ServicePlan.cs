using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Master_Project.Models;

namespace Master_Project.Models
{
    [Table("ServicePlan")]
    public class ServicePlan
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Nazwa { get; set; }
        public string PoczątekSłużby { get; set; }
        public string ZakończenieSłużby { get; set; }
        public string StartSłużby { get; set; }
        public string KoniecSłużby { get; set; }
        public string Obsługa { get; set; }
        public string UwagiDyspozytora { get; set; }
        public virtual List<Profile> Profiles { get; set; }
        public virtual List<ServicePlan> ServicePlans { get; set; }

    }
}