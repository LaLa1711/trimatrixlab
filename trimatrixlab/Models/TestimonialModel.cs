using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trimatrixlab.Models
{
    public class TestimonialModel
    {
        public int IDTestimonial { get; set; }
        public string TieuDe { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public string NoiDung { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<bool> Hide { get; set; }
    }
}