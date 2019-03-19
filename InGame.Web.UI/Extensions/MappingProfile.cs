using AutoMapper;
using InGame.Core.Entities;
using InGame.Web.UI.Models.ParentCategoryViewModels;
using InGame.Web.UI.Models.ProductViewModels;

namespace InGame.Web.UI.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();

            CreateMap<Category, CategoryViewModels>();
            CreateMap<CategoryViewModels, Category>();
        }
    }
}
