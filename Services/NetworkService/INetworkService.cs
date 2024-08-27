namespace SupportDesk.App.Services.NetworkService;

public interface INetworkService
{
    Task<T> Retry<T>(Func<Task<T>> action);
    Task<T> Retry<T>(Func<Task<T>> action, int retryCount);
    Task<T> Retry<T>(Func<Task<T>> action, int retryCount, Func<Exception, int, Task> onRetry);
    Task<T> WaitAndRetry<T>(Func<Task<T>> action, Func<int, TimeSpan> sleepDurationProvider);
    Task<T> WaitAndRetry<T>(Func<Task<T>> action, Func<int, TimeSpan> sleepDurationProvider, int retryCount);
    Task<T> WaitAndRetry<T>(Func<Task<T>> action, Func<int, TimeSpan> sleepDurationProvider, int retryCount, Func<Exception, TimeSpan, Task> onRetryAsync);
}
