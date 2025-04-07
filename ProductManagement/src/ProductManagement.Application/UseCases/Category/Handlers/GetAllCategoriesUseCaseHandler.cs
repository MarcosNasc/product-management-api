using ProductManagement.Application.Common.Results;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Interfaces.Repositories;
using ProductManagement.Application.UseCases.Category.Command.Response;
using ProductManagement.Application.UseCases.Category.Query;

namespace ProductManagement.Application.UseCases.Category.Handlers
{
    public class GetAllCategoriesUseCaseHandler : IUseCase<GetAllCategoriesQuery, IEnumerable<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesUseCaseHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<IEnumerable<CategoryResponse>>> HandleAsync(GetAllCategoriesQuery request)
        {
            var categories = await _categoryRepository.GetAllAsync();

            var response = categories.Select(c => new CategoryResponse(c.Id, c.Name));

            return Result<IEnumerable<CategoryResponse>>.Success(response);
        }
    }
}
