using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trimatrixlab.Models
{
    public class SkillModel
    {
        public int IDSkill { get; set; }
        public Nullable<int> IDGrSkill { get; set; }
        public string Ten { get; set; }
        public string NoiDung { get; set; }
        public Nullable<int> PhanTram { get; set; }
        public Nullable<bool> Hide { get; set; }
    }
}