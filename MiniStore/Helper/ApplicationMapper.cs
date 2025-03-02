using AutoMapper;
using MiniStore.Dto;
using MiniStore.Models;

namespace MiniStore.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<CategoryDto, Category>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<ProductDto, Product>().ReverseMap();

        }

        public Product MapToProduct(ProductDto productDto)
        {
            Product product = new Product();
            product.ProductDescription = productDto.ProductDescription;
            product.Productmanufacturing = productDto.Productmanufacturing;
            product.ProductName = productDto.ProductName;
            product.category = product.category;



            return null;
        }
    }
}
