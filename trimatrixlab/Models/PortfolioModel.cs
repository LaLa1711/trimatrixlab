using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using trimatrixlab.DBContext;

namespace trimatrixlab.Models
{
    public class PortfolioModel
    {
        public int IDPortfolio { get; set; }
        public Nullable<int> IDGrPortfolio { get; set; }
        public string TieuDe { get; set; }
        public string Portfolio { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public Nullable<bool> Hide { get; set; }
    }
}