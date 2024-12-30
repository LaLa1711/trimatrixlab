using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trimatrixlab.Models
{
    public class SocialModel
    {
        public int IDSocial { get; set; }
        public string Image { get; set; }
        public string LinkSocial { get; set; }
        public string Button { get; set; }
        public Nullable<bool> Hide { get; set; }
    }
}