using Microsoft.AspNetCore.Http;

namespace ProductManagement.Application.UseCases.Product.Command
{
    public record CreateProductCommand(
        string Name,
        string Description,
        int CategoryId,
        decimal Price,
        IFormFile? Image,
        string ImageUrl
    );
}
