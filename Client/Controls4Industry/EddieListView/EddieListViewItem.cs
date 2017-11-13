using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace C4I
{
    public class EddieListViewItem: ListViewItem
    {

        static EddieListViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieListViewItem), new FrameworkPropertyMetadata(typeof(EddieListViewItem)));
        }
    }
}
