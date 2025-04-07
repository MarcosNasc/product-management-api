namespace ProductManagement.Application.UseCases.Product.Command.Response
{
    public record ProductResponse
    (
        int ProductId,
        int CategoryId,
        string Name,
        string Description,
        decimal Price,
        string ImageUrl,
        bool IsActive
    );
}
