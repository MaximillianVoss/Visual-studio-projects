using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xmemo
{
    class Memo_groups
    {
        public string Name { set; get; }
        public string Day { set; get; }
        public string Day_of_week { set; get; }
        public string Month { set; get; }
        public string Name_of_month { set; get; }
        public string Year { set; get; }
        
        public ObservableCollection<Memo> Memos { get; set; }

        public Memo_groups ()
        {
            Memos = new ObservableCollection<Memo>();
        }
        
    }
}
