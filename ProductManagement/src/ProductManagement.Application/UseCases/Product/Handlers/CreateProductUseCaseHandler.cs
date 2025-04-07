using ProductManagement.Application.Interfaces.Repositories;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.UseCases.Product.Command.Response;
using ProductManagement.Application.Common.Results;
using System.Globalization;
using ProductManagement.Application.UseCases.Product.Command;

namespace ProductManagement.Application.UseCases.Product.Handlers
{
    public class CreateProductUseCaseHandler : IUseCase<CreateProductCommand, CreateProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CreateProductUseCaseHandler(
            IProductRepository productRepository
            , ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<CreateProductResponse>> HandleAsync(CreateProductCommand request)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
                return Result<CreateProductResponse>.Failure("Categoria não encontrada.");

            var product = new Domain.Entities.Product(
                name: request.Name,
                description: request.Description,
                categoryId: request.CategoryId,
                price: request.Price,
                imageUrl: request.ImageUrl
            );

            await _productRepository.AddAsync(product);

            var response = new CreateProductResponse(product.Id);

            return Result<CreateProductResponse>.Success(response);
        }
    }
}
