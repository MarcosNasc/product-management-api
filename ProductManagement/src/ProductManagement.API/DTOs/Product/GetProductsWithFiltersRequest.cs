using ProductManagement.API.Interfaces;
using ProductManagement.Application.UseCases.Product.Query;

namespace ProductManagement.API.DTOs.Product
{
    public record class GetProductsWithFiltersRequest : IRequestValidator
    {
        public int? CategoryId { get; init; }
        public decimal? MinPrice { get; init; }
        public decimal? MaxPrice { get; init; }
        public bool? IsActive { get; init; } = true;

        public GetProductsWithFiltersQuery ToQuery()
        {
            return new GetProductsWithFiltersQuery(CategoryId, MinPrice, MaxPrice, IsActive);
        }

        public List<string> Validate()
        {
            var errors = new List<string>();

            if (CategoryId is not null && CategoryId <= 0)
            {
                errors.Add("A categoria do produto deve ser válida.");
            }

            if (MinPrice is not null && MinPrice <= 0)
            {
                errors.Add("O preço mínimo do produto deve ser maior que zero.");
            }

            if (MaxPrice is not null && MaxPrice <= 0)
            {
                errors.Add("O preço máximo do produto deve ser maior que zero.");
            }

            if (MinPrice is not null && MaxPrice is not null && MinPrice > MaxPrice)
            {
                errors.Add("O preço mínimo não pode ser maior que o preço máximo.");
            }

            return errors;
        }
    }
}
