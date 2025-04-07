namespace ProductManagement.Application.UseCases.Product.Command.Response
{
    public record EditProductResponse(
    int ProductId,
    string Name,
    string Description,
    decimal Price,
    string ImageUrl,
    bool IsActive
);
}
