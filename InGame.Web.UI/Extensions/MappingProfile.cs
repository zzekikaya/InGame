using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InGame.Core.Entities;
using InGame.Web.UI.Models.CategoryViewModels;
using InGame.Web.UI.Models.ProductViewModels;

namespace InGame.Web.UI.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();

            CreateMap<SubCategory, SubCategoryViewModel>();
            CreateMap<SubCategoryViewModel, SubCategory>();

            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel, Category>();
        }
    }
}
