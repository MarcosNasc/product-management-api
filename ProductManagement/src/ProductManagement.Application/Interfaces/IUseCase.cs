using ProductManagement.Application.Common.Results;

namespace ProductManagement.Application.Interfaces
{
    public interface IUseCase<TRequest , TResponse>
    {
        Task<Result<TResponse>> HandleAsync(TRequest request);
    }

    public interface IUseCase<in TRequest>
    {
        Task<Result> HandleAsync(TRequest request);
    }
}
