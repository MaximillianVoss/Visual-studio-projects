using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xmemo.Pages
{
    class Color_groups
    {
        public string group_name { set; get; }
        public ObservableCollection<Memo_color> Colors { set; get; }
        public Color_groups ()
        {
            Colors = new ObservableCollection<Memo_color>();
        }
    }
}
