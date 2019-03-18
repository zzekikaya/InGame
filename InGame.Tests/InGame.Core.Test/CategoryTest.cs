using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InGame.Core.Entities;
using InGame.Core.Interfaces;
using InGame.Core.Services;
using Moq;
using Xunit;

namespace InGame.Tests.InGame.Core.Test
{
   public class CategoryTest
    {

        private Mock<IAsyncRepository<Category>> _mockCategoryRepo;

        public CategoryTest()
        {
            _mockCategoryRepo = new Mock<IAsyncRepository<Category>>();
        }

        [Fact]
        public async Task CategoryGetById_Test()
        {
            var category = new Category(); 
            _mockCategoryRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(category);
            var categoryService = new CategoryService(_mockCategoryRepo.Object, null);
            await categoryService.GetByIdAsync(category.Id);
            _mockCategoryRepo.Verify(x => x.GetByIdAsync(category.Id), Times.Once);
        }

        [Fact]
        public async Task Insert_Category_Test()
        {
            var category = new Category();
            
            var categoryService = new CategoryService(_mockCategoryRepo.Object, null);

            await categoryService.AddAsync(category);

            _mockCategoryRepo.Verify(x => x.AddAsync(category));
        }

        [Fact]
        public async Task Update_Category_Test()
        {
            var category = new Category();
            _mockCategoryRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(category);
            var categoryService = new CategoryService(_mockCategoryRepo.Object, null);

            await categoryService.UpdateCagetory(category);

            _mockCategoryRepo.Verify(x => x.UpdateAsync(It.IsAny<Category>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Category_Test()
        {
            var category = new Category();
            _mockCategoryRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(category);
            var categoryService = new CategoryService(_mockCategoryRepo.Object, null);

            await categoryService.DeleteAsync(category);

            _mockCategoryRepo.Verify(x => x.DeleteAsync(It.IsAny<Category>()), Times.Once);
        }
    }
}
