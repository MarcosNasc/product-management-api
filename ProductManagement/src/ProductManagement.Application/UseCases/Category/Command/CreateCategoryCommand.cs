namespace ProductManagement.Application.UseCases.Category.Command
{
    public record CreateCategoryCommand(
        string Name,
        string Description
    );
}
