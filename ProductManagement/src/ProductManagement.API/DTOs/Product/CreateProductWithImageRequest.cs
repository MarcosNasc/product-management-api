using ProductManagement.API.Interfaces;
using ProductManagement.Application.UseCases.Product.Command;

namespace ProductManagement.API.DTOs.Product
{
    public record class CreateProductWithImageRequest : IProductImageRequest, IRequestValidator

    {
        public string Name { get; init; }
        public string Description { get; init; }
        public int CategoryId { get; init; }
        public decimal Price { get; init; }
        public IFormFile? Image { get; init; }


        private const string DefaultPlaceholder = "string";


        public CreateProductCommand ToCommand(string? imageUrlFromUpload)
        {
            return new CreateProductCommand(
                Name,
                Description,
                CategoryId,
                Price,
                null,
                imageUrlFromUpload
            );
        }

        public List<string> Validate()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Name) || Name == DefaultPlaceholder)
            {
                errors.Add("O nome do produto é obrigatório e não pode conter o valor padrão.");
            }

            if (string.IsNullOrWhiteSpace(Description) || Description == DefaultPlaceholder)
            {
                errors.Add("A descrição do produto é obrigatória e não pode conter o valor padrão.");
            }

            if (CategoryId <= 0)
            {
                errors.Add("A categoria do produto deve ser válida.");
            }

            if (Price <= 0)
            {
                errors.Add("O preço do produto deve ser maior que zero.");
            }

            return errors;
        }
    }
}
