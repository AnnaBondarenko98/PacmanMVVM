using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Localizator
{
    public static class DesignHelpers
    {
        /// <summary>
        /// Checks whether the application is in design mode
        /// </summary>
        public static bool IsInDesignMode => (bool)(DesignerProperties.IsInDesignModeProperty
            .GetMetadata(typeof(DependencyObject))
            .DefaultValue);
    }
}
