using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Master_Project.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Imię { get; set; }
        public string Nazwisko { get; set; }
        public string Miasto { get; set; }
        public string Zakład { get; set; }
        public virtual List<Profile> Profiles { get; set; }
    }
}