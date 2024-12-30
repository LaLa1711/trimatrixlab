using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trimatrixlab.Models
{
    public class InterestModel
    {
        public int IDInterest { get; set; }
        public string Icon { get; set; }
        public string TenIcon { get; set; }
        public string Background { get; set; }
        public Nullable<bool> Hide { get; set; }
    }
}