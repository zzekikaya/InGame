using InGame.Core.Entities;
using System.Threading.Tasks;

namespace InGame.Core.Interfaces
{
    public interface ISubCategoryService : IAsyncRepository<SubCategory>
    {
        SubCategory GetSubCategoryId(int subCategoryId);

        Task CreateSubCategory(SubCategory subCategory);

        Task UpdateSubCategory(SubCategory subCategory);
    }
}
