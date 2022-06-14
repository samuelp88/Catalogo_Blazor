using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo_Blazor.Shared.Resources
{
    public class Pagination
    {
        public int Page { get; set; } = 1;
        public int CountPerPage { get; set; } = 5;
    }
}
