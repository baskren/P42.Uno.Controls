using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo;
static class ItemsExtesions
{
    public static void Add(this ObservableCollection<Item> list, string title, string value)
    {
        list.Add(new Item(title, value));
    }
}
