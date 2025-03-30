using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Domain.Entities;
using Application.Interfaces;
using Application.Models;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IIplfranchiseEcommDbContext _context;

        public ProductService(IIplfranchiseEcommDbContext context)
        {
            _context = context;
        }

        // Get all product details asynchronously
        /// <summary>
        /// GetProductDetailsAsync
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductDetailsDto>> GetProductDetailsAsync()
        {
            return await _context.Products
                .Include(p => p.Iplfranchise)
                .Include(p => p.Categories)
                .Select(p => new ProductDetailsDto
                {
                    ProductName = p.ProductName,
                    IPLFranchiseName = p.Iplfranchise.IplteamName,
                    CategoryName = p.Categories.CategoryName,
                    //ProductDescription=p.Description,
                    //ProductCode=p.ProductCode,
                    ProductPrice = p.Price,
                    ProductId=p.ProductId
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductDetailsDto>> SearchProductsAsync(ProductSearchDto searchDto)
        {
            var query = _context.Products
                .Include(p => p.Iplfranchise)
                .Include(p => p.Categories)
                .AsQueryable();

            // Apply filters only if they are provided
            query = query.Where(p =>
                (string.IsNullOrEmpty(searchDto.ProductName) || p.ProductName.Contains(searchDto.ProductName)) &&
                (string.IsNullOrEmpty(searchDto.ProductCode) || p.ProductCode.Contains(searchDto.ProductCode)) &&
                (string.IsNullOrEmpty(searchDto.FranchiseName) || p.Iplfranchise.IplteamName.Contains(searchDto.FranchiseName)) &&
                (string.IsNullOrEmpty(searchDto.Category) || p.Categories.CategoryName.Contains(searchDto.Category))
            );

            if (searchDto.minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= searchDto.minPrice);
            }

            if (searchDto.maxPrice.HasValue)    
            {
                query = query.Where(p => p.Price <= searchDto.maxPrice);
            }

            //// Apply pagination
            //if (searchDto.PageSize > 0)
            //{
            //    query = query.Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
            //                .Take(searchDto.PageSize);
            //}

            var products = await query
                .Select(p => new ProductDetailsDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.Description,
                    ProductPrice = p.Price,
                    IPLFranchiseName = p.Iplfranchise.IplteamName,
                    CategoryName = p.Categories.CategoryName,
                })
                .ToListAsync();

            return products;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .ToListAsync();
        }

        // Get products with pagination
        public async Task<List<Product>> GetProductsAsync(int page = 1, int pageSize = 10)
        {
            return await _context.Products
                .Include(p => p.Categories)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // Get filtered products
        public async Task<List<Product>> GetFilteredProductsAsync(
            string searchTerm = null,
            decimal? minPrice = null,
            decimal? maxPrice = null)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => p.ProductName.Contains(searchTerm));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            return await query
                .Include(p => p.Categories)
                .ToListAsync();
        }
        
    }
}