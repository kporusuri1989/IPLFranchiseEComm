using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync(int page, int pageSize);
        Task<List<Product>> GetAllProductsAsync();
        Task<IEnumerable<ProductDetailsDto>> GetProductDetailsAsync();
        Task<IEnumerable<ProductDetailsDto>> SearchProductsAsync(ProductSearchDto searchDto);

        Task<List<Product>> GetFilteredProductsAsync(string searchTerm = null,
        decimal? minPrice = null,
        decimal? maxPrice = null);
    }
}
