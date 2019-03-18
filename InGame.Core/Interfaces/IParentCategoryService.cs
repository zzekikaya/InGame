using InGame.Core.Entities;
using System.Threading.Tasks;

namespace InGame.Core.Interfaces
{
    public interface IParentCategoryService : IAsyncRepository<ParentCategory>
    {
        ParentCategory GetSubCategoryId(int subCategoryId);

        Task CreateSubCategory(ParentCategory subCategory);

        Task UpdateSubCategory(ParentCategory subCategory);
    }
}
