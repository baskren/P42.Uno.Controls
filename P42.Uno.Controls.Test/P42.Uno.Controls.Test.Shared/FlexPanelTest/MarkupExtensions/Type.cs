using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Markup;

namespace FlexPanelTest.MarkupExtensions
{
    [MarkupExtensionReturnType(ReturnType = typeof(Type))]
    public sealed class TypeExtension : MarkupExtension
    {
        public Type Type { get; set; }

        /// <inheritdoc/>
        protected override object ProvideValue() => Type;
    }
}
