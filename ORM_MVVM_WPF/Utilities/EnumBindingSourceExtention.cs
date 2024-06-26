﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ORM_MVVM_WPF.Utilities
{
    public class EnumBindingSourceExtention : MarkupExtension

    {
        public Type EnumType { get; set; }
        public EnumBindingSourceExtention(Type enumType)
        {
            EnumType = enumType;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(EnumType);
                //.Cast<object>()
                //.Select(e => new { Value = (int)e, DisplayName = e.ToString() });
        }
    }
}
