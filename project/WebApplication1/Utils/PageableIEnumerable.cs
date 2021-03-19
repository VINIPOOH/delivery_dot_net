

using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class PageableIEnumerable
    {
        public static IEnumerable<T> findAllByUserIdAndIsDeliveryPaidTrue<T>(IEnumerable<T> targetForPagination, int currentPage, int pageSize)
        {
            return targetForPagination.Skip(currentPage * pageSize).Take(pageSize);
        }
    }
}