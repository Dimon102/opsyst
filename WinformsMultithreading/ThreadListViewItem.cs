using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformsMultithreading
{
    internal class ThreadListViewItem : ListViewItem
    {
        public readonly ThreadClass ThreadClass;

        public ThreadListViewItem(String name, ThreadClass threadClass) : base(name)
        {
            ThreadClass = threadClass;
        }
    }
}
