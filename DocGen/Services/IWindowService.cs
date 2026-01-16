using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGen.Services
{
    public interface IWindowService
    {
        XamlRoot GetXamlRoot();
        IntPtr GetWindowHandle();
    }
}
