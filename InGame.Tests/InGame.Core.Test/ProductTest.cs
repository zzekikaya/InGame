using System.Threading.Tasks;
using InGame.Core.Entities;
using InGame.Core.Interfaces;
using InGame.Core.Services;
using Moq;
using Xunit;

namespace InGame.Tests.CoreTest
{
    public class ProductTest
    {
        private Mock<IAsyncRepository<Product>> _mockProductRepo;

        public ProductTest()
        {
            _mockProductRepo = new Mock<IAsyncRepository<Product>>();
        }

        [Fact]
        public async Task ProductGetById_Test()
        {
            var product = new Product();
            _mockProductRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(product);
            var productService = new ProductService(_mockProductRepo.Object, null, null);

            await productService.GetByIdAsync(product.Id);

            _mockProductRepo.Verify(x => x.GetByIdAsync(product.Id), Times.Once);
        }

        [Fact]
        public async Task Insert_Product_Test()
        {
            var product = new Product()
            {
                Name = "silah",
                Description = "ücretsiz",
                Price = 5000,
                SubCategoryID = 1
            };
            var productService = new ProductService(_mockProductRepo.Object, null, null);

            await productService.AddAsync(product);

            _mockProductRepo.Verify(x => x.AddAsync(product));
        }

        [Fact]
        public async Task Update_Product_Test()
        {
            var product = new Product();
            _mockProductRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(product);
            var productService = new ProductService(_mockProductRepo.Object, null, null);

            await productService.UpdateAsync(product);

            _mockProductRepo.Verify(x => x.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Product_Test()
        {
            var product = new Product();
            _mockProductRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(product);
            var productService = new ProductService(_mockProductRepo.Object, null, null);

            await productService.DeleteAsync(product);

            _mockProductRepo.Verify(x => x.DeleteAsync(It.IsAny<Product>()), Times.Once);
        }
    }
}
