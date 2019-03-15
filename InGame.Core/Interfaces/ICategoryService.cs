using System.Threading.Tasks;
using InGame.Core.Entities;

namespace InGame.Core.Interfaces
{
    public interface ICategoryService: IAsyncRepository<Category>
    {
        Category GetCagetoryById(int cagetoryId);

        Task CreateCagetory(Category cagetory);

        Task UpdateCagetory(Category cagetory);

    }
}
