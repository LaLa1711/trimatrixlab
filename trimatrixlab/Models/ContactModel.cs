using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trimatrixlab.Models
{
    public class ContactModel
    {
        public int IDContact { get; set; }
        public string Ten { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string NoiDung { get; set; }
        public Nullable<bool> Hide { get; set; }
    }
}