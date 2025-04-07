using Microsoft.AspNetCore.Http;

namespace ProductManagement.Application.UseCases.Product.Command
{
    public record EditProductCommand(
        int Id,
        string Name,
        string Description,
        int CategoryId,
        decimal Price,
        IFormFile? Image,
        string ImageUrl,
        bool IsActive
    );
}
