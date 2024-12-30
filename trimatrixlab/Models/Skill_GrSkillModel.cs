using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trimatrixlab.Models
{
    public class Skill_GrSkillModel
    {
        public GrSkillModel Group { get; set; }
        public List<SkillModel> Skills { get; set; }
    }
}