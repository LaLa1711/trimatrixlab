using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trimatrixlab.Models
{
    public class StoryModel
    {
        public int IDStory { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string HinhAnh { get; set; }
        public string Ten {  get; set; }
        public Nullable<System.DateTime> NgayDang { get; set; }
        public string Link { get; set; }
        public Nullable<bool> Hide { get; set; }
    }
}