using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGen.Models
{
    public class AppSettings
    {
        public string Language { get; set; } = null;
        public List<string> SelectedExtensions { get; set; } = new() { ".cs", ".xaml", ".csproj" };

        public bool IncludeXaml { get; set; } = true;
        public bool IncludeCs { get; set; } = true;
        public bool IncludeCsproj { get; set; } = true;
        public bool IncludeJson { get; set; } = false;
        public bool IncludeManifest { get; set; } = false;
    }
}
