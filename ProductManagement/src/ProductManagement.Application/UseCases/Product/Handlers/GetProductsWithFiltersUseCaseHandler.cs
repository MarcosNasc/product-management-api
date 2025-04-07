using ProductManagement.Application.Common.Results;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Interfaces.Repositories;
using ProductManagement.Application.UseCases.Product.Command.Response;
using ProductManagement.Application.UseCases.Product.Query;
using System.Linq.Expressions;

namespace ProductManagement.Application.UseCases.Product.Handlers
{
    public class GetProductsWithFiltersUseCaseHandler : IUseCase<GetProductsWithFiltersQuery, IEnumerable<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsWithFiltersUseCaseHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<IEnumerable<ProductResponse>>> HandleAsync(GetProductsWithFiltersQuery request)
        {
            Expression<Func<Domain.Entities.Product, bool>> filter = p =>
                (request.CategoryId == null || p.CategoryId == request.CategoryId) &&
                (request.MinPrice == null || p.Price >= request.MinPrice) &&
                (request.MaxPrice == null || p.Price <= request.MaxPrice) &&
                (request.IsActive == null || p.IsActive == request.IsActive);

            var products = await _productRepository.FindAsync(filter);

            var response = products.Select(p => new ProductResponse(
                p.Id,
                p.CategoryId,
                p.Name,
                p.Description,
                p.Price,
                p.ImageUrl,
                p.IsActive
            ));

            return Result<IEnumerable<ProductResponse>>.Success(response);
        }
    }
}
