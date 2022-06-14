using Catalogo_Blazor.Shared.Resources;

namespace Catalogo_Blazor.Server.Util
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, Pagination pagination)
        {
            return queryable
                .Skip((pagination.Page - 1) * pagination.CountPerPage)
                .Take(pagination.CountPerPage);
        }
    }
}
