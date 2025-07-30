using CBS.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBS.Service.Utilities
{
    public static class Pagination
    {
        public static PaginatedResult<T> GetPagedData<T>(IQueryable<T> data, int currentPage, int pageSize)
        {
            var paginatedResult = new PaginatedResult<T>
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalItems = data.Count(),
                Items = data.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
            };

            return paginatedResult;
        }
    }
}