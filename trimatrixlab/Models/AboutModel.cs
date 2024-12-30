using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trimatrixlab.Models
{
    public class AboutModel
    {
        public int IDAbout { get; set; }
        public string Name { get; set; }
        public string Contents { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Descriptions { get; set; }
        public string ZipCode { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public string MoTaInterest { get; set; }
        public string Poster { get; set; }
    }
}