using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trimatrixlab.Models
{
    public class Port_GrPortModel
    {
        public int IDPortfolio { get; set; }
        public int IDGrPortfolios { get; set; }
        public Nullable<int> IDGrPortfolio { get; set; }
        public string TieuDe { get; set; }
        public string Portfolio { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public Nullable<bool> Hide { get; set; }
    }
}