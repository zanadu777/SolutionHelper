using SolutionHelper.Framework.FileData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolutionHelper.Framework.VisualStudioObjects
{
    public class AssemblyReference
    {
        public AssemblyReference()
        {

        }

        public AssemblyReference(AssemblyReferenceData data)
        {
            Name = data.Name;
            Version = data.Version;
            Culture = data.Culture;
            PublicKeyToken = data.PublicKeyToken;
            HintPath = data.HintPath;
        }

        public string Name { get; set; }
        public string Version { get; set; }
        public string Culture { get; set; }
        public string PublicKeyToken { get; set; }
        public string HintPath { get; set; }

        public override string ToString()
        {
            return $"{Name} {Version}";
        }
    }
}
