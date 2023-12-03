using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemHelper.Filters
{
    public class OrFilter<T> : Filter<T>
    {
        public List<Filter<T>> Filters = new();
        public override bool IsIncluded(T item)
        {
            foreach (var filter in Filters)
                if (filter.IsEnabled)
                    if (filter.IsIncluded(item))
                        return true;

            return true;
        }

        public override bool IsExcluded(T item)
        {
            throw new NotImplementedException();
        }

        public override string DisplayText { get; }
    }
}
