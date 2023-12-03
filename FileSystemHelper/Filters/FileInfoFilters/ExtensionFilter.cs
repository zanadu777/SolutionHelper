using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FileSystemHelper.Filters;

namespace FileSystemHelper.Filters.FileInfoFilters
{
    public class ExtensionFilter : Filter<FileInfo>
    {
        public List<string> Extensions { get; set; } = new();
        public override bool IsIncluded(FileInfo item)
        {
            var extension = item.Extension;
            bool isInListedExtensions = Extensions.Contains(extension);
            return isInListedExtensions;
        }

        public override bool IsExcluded(FileInfo item)
        {
            var extension = item.Extension;
            bool isInListedExtensions = Extensions.Contains(extension);
            return !isInListedExtensions;
        }

        public override string DisplayText { get; }
    }
}
