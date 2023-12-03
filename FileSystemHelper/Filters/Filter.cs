using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemHelper.Filters
{
    public abstract class Filter<T>
    {
        public abstract bool IsIncluded(T item);

        public abstract bool IsExcluded(T item);

        public abstract string DisplayText { get; }

        public bool IsEnabled { get; set; } = true;

        public ExclusionType ExclusionType { get; set; }

        public List<T> FilterItems(IEnumerable<T> items)
        {
            if (!IsEnabled)
                return items.ToList();

            var filtered = new List<T>();
            if (ExclusionType == ExclusionType.Inclusive)
            {
                filtered.AddRange(items.Where(item => IsIncluded(item)));
            }
            else if (ExclusionType == ExclusionType.Exclusive)

                filtered.AddRange(items.Where(item => !IsExcluded(item)));

            return filtered;
        }

        public async Task<List<T>> FilterItemsAsync(IEnumerable<T> items)
        {
            return FilterItems(items);
            //if (!IsEnabled)
            //  return items.ToList();

            //var filtered = new List<T>();
            //if (ExclusionType == ExclusionType.Inclusive)
            //{
            //  filtered.AddRange(items.Where(item => IsIncluded(item)));
            //}
            //else if (ExclusionType == ExclusionType.Exclusive)

            //  filtered.AddRange(items.Where(item => !IsExcluded(item)));

            //return filtered;
        }
    }
}
