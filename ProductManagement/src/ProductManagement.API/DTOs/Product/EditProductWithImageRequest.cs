using ProductManagement.API.Interfaces;
using ProductManagement.Application.UseCases.Product.Command;

namespace ProductManagement.API.DTOs.Product
{
    public record class EditProductWithImageRequest : IProductImageRequest , IRequestValidator
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public int CategoryId { get; init; }
        public decimal Price { get; init; }
        public IFormFile? Image { get; init; }
        public bool? IsActive { get; init; } = true;


        private const string DefaultPlaceholder = "string";

        public EditProductCommand ToCommand(string? imageUrlFromUpload)
        {
            return new EditProductCommand(
                Id,
                Name,
                Description,
                CategoryId,
                Price,
                null,
                imageUrlFromUpload,
                IsActive.Value
            );
        }

        public List<string> Validate()
        {
            var errors = new List<string>();

            if (Id <= 0)
            {
                errors.Add("O ID do produto é obrigatório.");
            }

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

            if (IsActive is null)
            {
                errors.Add("O Status do produto é obrigatório");
            }

            return errors;
        }
    }
}
