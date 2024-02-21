using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace P42.Uno.Controls;

public partial class EnhancedSegmentedControl
{
    public EnhancedSegmentedControl() : base()
    {
        Build();

        IsOverflowedChanged += OnOverflowChanged;
    }

    private void OnOverflowChanged(object sender, bool e)
    {
        
    }
}
