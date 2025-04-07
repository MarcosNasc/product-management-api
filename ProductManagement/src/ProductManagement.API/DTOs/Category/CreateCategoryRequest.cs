using ProductManagement.API.Interfaces;
using ProductManagement.Application.UseCases.Category.Command;

namespace ProductManagement.API.DTOs.Category
{
    public record class CreateCategoryRequest : IRequestValidator

    {
        public string Name { get; init; }
        public string Description { get; init; }

        public CreateCategoryCommand ToCommand()
        {
            return new CreateCategoryCommand(Name, Description);
        }

        public List<string> Validate()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Name) || Name == "string")
            {
                errors.Add("O nome da categoria é obrigatório e não pode conter o valor padrão.");
            }

            if (string.IsNullOrWhiteSpace(Description) || Description == "string")
            {
                errors.Add("A descrição da categoria é obrigatória e não pode conter o valor padrão.");
            }

            return errors;
        }
    }
}
