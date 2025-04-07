using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.DTOs.Category;
using ProductManagement.API.Results;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.UseCases.Category.Command;
using ProductManagement.Application.UseCases.Category.Command.Response;
using ProductManagement.Application.UseCases.Category.Query;

namespace ProductManagement.API.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IUseCase<CreateCategoryCommand, CreateCategoryResponse> _createCategoryUseCase;
        private readonly IUseCase<GetAllCategoriesQuery, IEnumerable<CategoryResponse>> _getAllCategoriesUseCase;

        public CategoryController(IUseCase<CreateCategoryCommand, CreateCategoryResponse> createCategoryUseCase
                                  ,IUseCase<GetAllCategoriesQuery, IEnumerable<CategoryResponse>> getAllCategoriesUseCase
            )
        {
            _createCategoryUseCase = createCategoryUseCase;
            _getAllCategoriesUseCase = getAllCategoriesUseCase;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<CreateCategoryResponse>>> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            var errors = request.Validate();
            if (errors.Any())
                return BadRequest(ApiResult<CreateCategoryResponse>.Fail(errors));

            var command = request.ToCommand();
            var result = await _createCategoryUseCase.HandleAsync(command);

            if (result.IsFailure) 
                return BadRequest(ApiResult<CreateCategoryResponse>.Fail(result.Error));

            var apiResult = ApiResult<CreateCategoryResponse>.Ok(result.Value, "Categoria criada com sucesso");

            return StatusCode(201,apiResult);
        }

        [HttpGet]
        public async  Task<ActionResult<ApiResult<IEnumerable<CategoryResponse>>>> GetAll()
        {
            var result = await _getAllCategoriesUseCase.HandleAsync(new GetAllCategoriesQuery());

            if (result.IsFailure)
                return BadRequest(ApiResult.Fail(result.Error));

            var apiResult = ApiResult<IEnumerable<CategoryResponse>>.Ok(result.Value, "Categorias listadas com sucesso");

            return Ok(apiResult);
        }



    }
}
