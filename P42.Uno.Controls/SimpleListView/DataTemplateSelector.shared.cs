using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;

namespace P42.Uno.Controls
{
    public class DataTemplateSelector : Windows.UI.Xaml.Controls.DataTemplateSelector
    {
        public virtual IEnumerable<DataTemplate> Templates
        {
            get
            {
                System.Console.WriteLine(GetType() + " Templates NOT IMPLEMENTED!!!!");
                throw new NotImplementedException();
            }
        }
    }
}
