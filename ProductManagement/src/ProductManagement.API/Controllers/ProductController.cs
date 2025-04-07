using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.DTOs.Product;
using ProductManagement.API.Interfaces;
using ProductManagement.API.Results;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Interfaces.Services;
using ProductManagement.Application.UseCases.Product.Command;
using ProductManagement.Application.UseCases.Product.Command.Response;
using ProductManagement.Application.UseCases.Product.Query;

namespace ProductManagement.API.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {

        private readonly IUseCase<CreateProductCommand, CreateProductResponse> _createProductUseCase;
        private readonly IUseCase<EditProductCommand, EditProductResponse> _editProductUseCase;
        private readonly IUseCase<DeleteProductCommand> _deleteProductUsecase;
        private readonly IUseCase<GetProductsWithFiltersQuery, IEnumerable<ProductResponse>> _getProductsWithFiltersUseCase;
        private readonly IStorageService _storageService;

        public ProductController(
          IUseCase<CreateProductCommand, CreateProductResponse> createProductUseCase
        , IUseCase<EditProductCommand, EditProductResponse> editProductUseCase
        , IUseCase<DeleteProductCommand> deleteProductUsecase
        , IUseCase<GetProductsWithFiltersQuery, IEnumerable<ProductResponse>> getProductsWithFiltersUseCase
        , IStorageService storageService)
        {
            _storageService = storageService;
            _createProductUseCase = createProductUseCase;
            _editProductUseCase = editProductUseCase;
            _deleteProductUsecase = deleteProductUsecase;
            _getProductsWithFiltersUseCase = getProductsWithFiltersUseCase;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<CreateProductResponse>>> CreateProduct([FromForm]CreateProductWithImageRequest request)
        {
            var errors = request.Validate();
            if (errors.Any())
                return BadRequest(ApiResult<CreateProductResponse>.Fail(errors));

            string imageUrlFromUpload = await UploadProductImage(request);

            var command = request.ToCommand(imageUrlFromUpload);

            var result = await _createProductUseCase.HandleAsync(command);

            if (result.IsFailure)
                return BadRequest(ApiResult<CreateProductResponse>.Fail(result.Error));

            var apiResult = ApiResult<CreateProductResponse>.Ok(result.Value, "Produto criado com sucesso");

            return StatusCode(201, apiResult);
        }


        [HttpPut]
        public async Task<ActionResult<ApiResult<EditProductResponse>>> EditProduct([FromForm] EditProductWithImageRequest request)
        {
            var errors = request.Validate();
            if (errors.Any())
                return BadRequest(ApiResult<EditProductResponse>.Fail(errors));

            string imageUrlFromUpload = await UploadProductImage(request);

            var command = request.ToCommand(imageUrlFromUpload);

            var result = await _editProductUseCase.HandleAsync(command);

            if (result.IsFailure) 
                return BadRequest(ApiResult<EditProductResponse>.Fail(result.Error));

            var apiResult = ApiResult<EditProductResponse>.Ok(result.Value, "Produto editado com sucesso");

            return Ok(apiResult);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
            var request = new DeleteProductCommand(id);

            var result = await _deleteProductUsecase.HandleAsync(request);

            if (result.IsFailure)
                return BadRequest(ApiResult.Fail(result.Error));

            return Ok(ApiResult.Ok("Produto excluído com sucesso"));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<IEnumerable<ProductResponse>>>> GetFilteredProducts([FromQuery] GetProductsWithFiltersRequest request)
        {
            var errors = request.Validate();

            if (errors.Any())
                return BadRequest(ApiResult<IEnumerable<ProductResponse>>.Fail(errors));

            var query = request.ToQuery();

            var result = await _getProductsWithFiltersUseCase.HandleAsync(query);

            if (result.IsFailure)
                return BadRequest(ApiResult.Fail(result.Error));

            var apiResult = ApiResult<IEnumerable<ProductResponse>>.Ok(result.Value, "Produtos filtrados com sucesso");

            return Ok(apiResult);
        }

        private async Task<string> UploadProductImage(IProductImageRequest request)
        {
            string uploadUrl = string.Empty;

            if (request.Image != null && request.Image.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.Image.FileName)}";
                using var stream = request.Image.OpenReadStream();
                uploadUrl = await _storageService.UploadFileAsync(stream, fileName, request.Image.ContentType);
            }

            return uploadUrl;
        }
    }
}
