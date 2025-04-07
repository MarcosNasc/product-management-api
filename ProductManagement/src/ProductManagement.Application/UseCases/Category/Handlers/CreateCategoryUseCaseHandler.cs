using ProductManagement.Application.Interfaces.Repositories;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.UseCases.Category.Command.Response;
using ProductManagement.Application.Common.Results;
using ProductManagement.Application.UseCases.Category.Command;

namespace ProductManagement.Application.UseCases.Category.Handlers
{
    public class CreateCategoryUseCaseHandler : IUseCase<CreateCategoryCommand, CreateCategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryUseCaseHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<CreateCategoryResponse>> HandleAsync(CreateCategoryCommand request)
        {
            var category = new Domain.Entities.Category(request.Name, request.Description);
            await _categoryRepository.AddAsync(category);

            var response = new CreateCategoryResponse(category.Id);

            return Result<CreateCategoryResponse>.Success(response);

        }
    }
}
