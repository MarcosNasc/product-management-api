namespace ProductManagement.Application.UseCases.Product.Query
{
    public record GetProductsWithFiltersQuery
    (
         int? CategoryId,
         decimal? MinPrice,
         decimal? MaxPrice,
         bool? IsActive = true
    );
}
