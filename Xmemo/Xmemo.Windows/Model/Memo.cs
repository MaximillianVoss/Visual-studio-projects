using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xmemo
{
    class Memo
    {
        public string name { set; get; }
        public string theme { set; get; }
        public DateTimeOffset date { get; set; }
        public string Hour { get; set; }
        public string Minutes { get; set; }
        public string Day { get; set; }
        public string Day_of_week { get; set; }
        public string Month { get; set; }
        public string Name_of_month { set; get; }
        public string text { get; set; }
        public string memo_color { get; set; }
        public string text_color { get; set; }
    }
}
