using Application.Models;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<ProductDetailsDto,ProductDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.ProductPrice));

            CreateMap<CustomerCart, CustomerCartDto>();

            //CreateMap<SearchRequestBody, ProductSearchDto>()
            //.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            //.ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.ProductCode))
            //.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.CategoryName))
            //.ForMember(dest => dest.FranchiseName, opt => opt.MapFrom(src => src.FranchiseName))
            //.ForMember(dest => dest.maxPrice, opt => opt.MapFrom(src => src.MaxPrice))
            //.ForMember(dest => dest.minPrice, opt => opt.MapFrom(src => src.MinPrice));

            CreateMap<SearchRequestBody, ProductSearchDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.ProductCode))
            .ForMember(dest => dest.FranchiseName, opt => opt.MapFrom(src => src.FranchiseName))
            .ForMember(dest => dest.maxPrice, opt => opt.MapFrom(src => src.MaxPrice))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.CategoryName))
            .ForMember(dest => dest.minPrice, opt => opt.MapFrom(src => src.MinPrice));

            CreateMap<CreateCartRequestBody, CreateCartDto>();

            //CreateMap<Product, ProductDetailsDto>()
            //.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductID))
            //.ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Price))
            //.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            //.ForMember(dest => dest.IPLFranchiseName, opt => opt.MapFrom(src => src.IplfranchiseId))
            //.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
            //.ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductID));
        }
    }
}
