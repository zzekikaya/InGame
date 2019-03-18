using InGame.Core.Entities;
using InGame.Core.Interfaces;
using InGame.Core.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace InGame.Tests.InGame.Core.Test
{
    public class SubCategoryTest
    {
        private Mock<IAsyncRepository<SubCategory>> _mockSubCategoryRepo;

        public SubCategoryTest()
        {
            _mockSubCategoryRepo = new Mock<IAsyncRepository<SubCategory>>();
        }

        [Fact]
        public async Task CategoryGetById_Test()
        {
            var subCategory = new SubCategory();
            _mockSubCategoryRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(subCategory);
            var subCategoryService = new SubCategoryService(_mockSubCategoryRepo.Object, null);
            await subCategoryService.GetByIdAsync(subCategory.Id);
            _mockSubCategoryRepo.Verify(x => x.GetByIdAsync(subCategory.Id), Times.Once);
        }

        [Fact]
        public async Task Insert_Category_Test()
        {
            var subCategory = new SubCategory();

            var categoryService = new SubCategoryService(_mockSubCategoryRepo.Object, null);

            await categoryService.AddAsync(subCategory);

            _mockSubCategoryRepo.Verify(x => x.AddAsync(subCategory));
        }

        [Fact]
        public async Task Update_Category_Test()
        {
            var subCategory = new SubCategory();
            _mockSubCategoryRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(subCategory);
            var categoryService = new SubCategoryService(_mockSubCategoryRepo.Object, null);

            await categoryService.UpdateAsync(subCategory);

            _mockSubCategoryRepo.Verify(x => x.UpdateAsync(It.IsAny<SubCategory>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Category_Test()
        {
            var subCategory = new SubCategory();
            _mockSubCategoryRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(subCategory);
            var categoryService = new SubCategoryService(_mockSubCategoryRepo.Object, null);

            await categoryService.DeleteAsync(subCategory);

            _mockSubCategoryRepo.Verify(x => x.DeleteAsync(It.IsAny<SubCategory>()), Times.Once);
        }
    }
}
