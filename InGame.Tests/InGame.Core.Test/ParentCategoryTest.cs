using InGame.Core.Entities;
using InGame.Core.Interfaces;
using InGame.Core.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace InGame.Tests.InGame.Core.Test
{
    public class ParentCategoryTest
    {
        private Mock<IAsyncRepository<ParentCategory>> _mockSubCategoryRepo;

        public ParentCategoryTest()
        {
            _mockSubCategoryRepo = new Mock<IAsyncRepository<ParentCategory>>();
        }

        [Fact]
        public async Task CategoryGetById_Test()
        {
            var subCategory = new ParentCategory();
            _mockSubCategoryRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(subCategory);
            var subCategoryService = new ParentCategoryService(_mockSubCategoryRepo.Object, null);
            await subCategoryService.GetByIdAsync(subCategory.Id);
            _mockSubCategoryRepo.Verify(x => x.GetByIdAsync(subCategory.Id), Times.Once);
        }

        [Fact]
        public async Task Insert_Category_Test()
        {
            var subCategory = new ParentCategory();

            var categoryService = new ParentCategoryService(_mockSubCategoryRepo.Object, null);

            await categoryService.AddAsync(subCategory);

            _mockSubCategoryRepo.Verify(x => x.AddAsync(subCategory));
        }

        [Fact]
        public async Task Update_Category_Test()
        {
            var subCategory = new ParentCategory();
            _mockSubCategoryRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(subCategory);
            var categoryService = new ParentCategoryService(_mockSubCategoryRepo.Object, null);

            await categoryService.UpdateAsync(subCategory);

            _mockSubCategoryRepo.Verify(x => x.UpdateAsync(It.IsAny<ParentCategory>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Category_Test()
        {
            var subCategory = new ParentCategory();
            _mockSubCategoryRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(subCategory);
            var categoryService = new ParentCategoryService(_mockSubCategoryRepo.Object, null);

            await categoryService.DeleteAsync(subCategory);

            _mockSubCategoryRepo.Verify(x => x.DeleteAsync(It.IsAny<ParentCategory>()), Times.Once);
        }
    }
}
