using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trimatrixlab.Models
{
    public class EducationModel
    {
        public int IDEducation { get; set; }
        public string TieuDe { get; set; }
        public string TieuDeCon { get; set; }
        public string MoTa { get; set; }
        public string NoiDung { get; set; }
        public Nullable<System.DateTime> NgayBatDau { get; set; }
        public Nullable<System.DateTime> NgayKetThuc { get; set; }
        public string HinhAnh { get; set; }
        public string Icon { get; set; }
        public Nullable<bool> Hide { get; set; }
    }
}