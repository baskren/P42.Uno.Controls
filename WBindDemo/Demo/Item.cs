using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo;
internal class Item : P42.NotifiableObject.SelfBackedNotifiablePropertyObject
{

    #region Title Property
    public string Title
    {
        get => GetValue(string.Empty);
        set => SetValue(value);
    }
    #endregion Title Property


    #region Value Property
    public string Value
    {
        get => GetValue(string.Empty);
        set => SetValue(value);
    }
    #endregion Value Property

    public Item() { }

    public Item(string title, string value) 
    {
        Title = title;
        Value = value;
    }
}
