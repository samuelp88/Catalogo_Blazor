using Microsoft.EntityFrameworkCore;

namespace Catalogo_Blazor.Server.Util
{
    public static class HttpContextExtensions
    {
        public static async Task InserirParametroEmPageResponse<T>(this HttpContext context,
            IQueryable<T> queryable, int quantidadeRegistrosExibir)
        {
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            double quantidadeRegistrosTotal = await queryable.CountAsync();
            double totalPaginas = Math.Ceiling(quantidadeRegistrosTotal / quantidadeRegistrosExibir);

            context.Response.Headers.Add("quantidadeRegistrosTotal", quantidadeRegistrosTotal.ToString());
            context.Response.Headers.Add("totalPaginas", totalPaginas.ToString());
        }
    }
}
