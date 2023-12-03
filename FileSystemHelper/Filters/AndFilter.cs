using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemHelper.Filters
{
    public class AndFilter<T> : Filter<T>
    {
        public List<Filter<T>> Filters = new();
        public override bool IsIncluded(T item)
        {

            foreach (var filter in Filters)
                if (filter.IsEnabled)
                    if (!filter.IsIncluded(item))
                        return false;

            return true;
        }

        public override bool IsExcluded(T item)
        {
            foreach (var filter in Filters)
                if (filter.IsEnabled)
                    if (filter.IsIncluded(item))
                        return false;

            return true;
        }

        public override string DisplayText { get; }
    }
}
