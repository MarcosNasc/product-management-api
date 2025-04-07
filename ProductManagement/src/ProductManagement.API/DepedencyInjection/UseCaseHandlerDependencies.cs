using ProductManagement.Application.Interfaces;
using ProductManagement.Application.UseCases.Category.Command;
using ProductManagement.Application.UseCases.Category.Command.Response;
using ProductManagement.Application.UseCases.Category.Handlers;
using ProductManagement.Application.UseCases.Category.Query;
using ProductManagement.Application.UseCases.Product.Command;
using ProductManagement.Application.UseCases.Product.Command.Response;
using ProductManagement.Application.UseCases.Product.Handlers;
using ProductManagement.Application.UseCases.Product.Query;
using System.Collections.Generic;

namespace ProductManagement.API.DepedencyInjection
{
    public static class UseCaseHandlerDependencies
    {
        public static IServiceCollection AddUseCasesHandlers(this IServiceCollection services)
        {
            #region Command
            services.AddScoped<IUseCase<CreateProductCommand, CreateProductResponse>, CreateProductUseCaseHandler>();
            services.AddScoped<IUseCase<CreateCategoryCommand, CreateCategoryResponse>, CreateCategoryUseCaseHandler>();
            services.AddScoped<IUseCase<EditProductCommand, EditProductResponse>, EditProductUseCaseHandler>();
            services.AddScoped<IUseCase<DeleteProductCommand>, DeleteProductUseCaseHandler>();
            #endregion

            #region Query
            services.AddScoped<IUseCase<GetAllCategoriesQuery, IEnumerable<CategoryResponse>>, GetAllCategoriesUseCaseHandler>();
            services.AddScoped<IUseCase<GetProductsWithFiltersQuery, IEnumerable<ProductResponse>>, GetProductsWithFiltersUseCaseHandler>();
            #endregion

            return services;
        }
    }
}
