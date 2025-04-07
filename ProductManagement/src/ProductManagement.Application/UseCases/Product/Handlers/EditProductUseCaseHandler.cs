using ProductManagement.Application.Interfaces.Repositories;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.UseCases.Product.Command.Response;
using ProductManagement.Application.Common.Results;
using ProductManagement.Application.UseCases.Product.Command;

namespace ProductManagement.Application.UseCases.Product.Handlers
{
    public class EditProductUseCaseHandler : IUseCase<EditProductCommand, EditProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public EditProductUseCaseHandler(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<EditProductResponse>> HandleAsync(EditProductCommand request)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
                return Result<EditProductResponse>.Failure("produto não encontrada.");

            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

            if (category == null)
                return Result<EditProductResponse>.Failure("categoria não encontrada.");

            product.Update(
                request.Name,
                request.Description,
                request.CategoryId,
                request.Price,
                request.ImageUrl,
                request.IsActive
            );

            await _productRepository.UpdateAsync(product);

            var response =  new EditProductResponse(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.ImageUrl,
                product.IsActive
            );

            return Result<EditProductResponse>.Success(response);
        }
    }
}
