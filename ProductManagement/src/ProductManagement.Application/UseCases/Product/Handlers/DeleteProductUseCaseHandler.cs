using ProductManagement.Application.Interfaces.Repositories;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Common.Results;
using ProductManagement.Application.UseCases.Product.Command;

namespace ProductManagement.Application.UseCases.Product.Handlers
{
    public class DeleteProductUseCaseHandler : IUseCase<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductUseCaseHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> HandleAsync(DeleteProductCommand request)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product is null)
                return Result.Failure("Produto não encontrado.");

            if (!product.IsActive)
                return Result.Failure("Produto já está inativo.");

            product.SetInactive();

            await _productRepository.UpdateAsync(product);

            return Result.Success();
        }
    }
}
