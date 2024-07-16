using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo;
internal class ViewModel : P42.NotifiableObject.SelfBackedNotifiablePropertyObject
{
    #region Name Property
    public string Name
    {
        get => GetValue<string>();
        set => SetValue(value);
    }
    #endregion Name Property
}
