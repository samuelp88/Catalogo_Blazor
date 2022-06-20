using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo_Blazor.Shared.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição obrigatória")]
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;
       
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Imagem obrigatória")]
        public string ImageUrl { get; set; } = string.Empty;

        //Relationship
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

    }
}
