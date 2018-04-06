using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localizator.Interfaces
{
    public interface ILocalizationProvider
    {
        object Localize(string key);

        IEnumerable<CultureInfo> Cultures { get; }
    }
}
