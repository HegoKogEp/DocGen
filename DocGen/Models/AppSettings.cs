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
        public bool IncludeKt { get; set; } = false;
        public bool IncludeGradle { get; set; } = false;
        public bool IncludeKts { get; set; } = false;
        public bool IncludeXml { get; set; } = false;
        public bool IncludeProperties { get; set; } = false;
        public bool IncludeJava { get; set; } = false;
        public bool IncludeJar { get; set; } = false;
        public bool IncludeAar { get; set; } = false;
        public bool IncludePy { get; set; } = false;
        public bool IncludeToml { get; set; } = false;
    }
}
