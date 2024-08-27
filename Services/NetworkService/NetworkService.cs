using Polly;

namespace SupportDesk.App.Services.NetworkService;

public class NetworkService : INetworkService
{
    public async Task<T> Retry<T>(Func<Task<T>> action)
    {
        return await RetryInner(action);
    }

    public async Task<T> Retry<T>(
        Func<Task<T>> action,
        int retryCount)
    {
        return await RetryInner(action, retryCount);
    }

    public async Task<T> Retry<T>(
        Func<Task<T>> action,
        int retryCount,
        Func<Exception, int, Task> onRetry)
    {
        return await RetryInner(action, retryCount, onRetry);
    }

    public async Task<T> WaitAndRetry<T>(
        Func<Task<T>> action,
        Func<int, TimeSpan> sleepDurationProvider)
    {
        return await WaitAndRetryInner(action, sleepDurationProvider);
    }

    public async Task<T> WaitAndRetry<T>(
        Func<Task<T>> action,
        Func<int, TimeSpan> sleepDurationProvider,
        int retryCount)
    {
        return await WaitAndRetryInner(
            action,
            sleepDurationProvider,
            retryCount);
    }

    public async Task<T> WaitAndRetry<T>(
        Func<Task<T>> action,
        Func<int, TimeSpan> sleepDurationProvider,
        int retryCount,
        Func<Exception, TimeSpan, Task> onRetryAsync)
    {
        return await WaitAndRetryInner(
            action,
            sleepDurationProvider,
            retryCount,
            onRetryAsync);
    }

    #region Inner Methods

    internal async Task<T> RetryInner<T>(
        Func<Task<T>> action,
        int retryCount = 1,
        Func<Exception, int, Task> onRetry = null)
    {
        var onRetryInner = new Func<Exception, int, Task>((e, i) =>
        {
            return Task.Factory.StartNew(() =>
            {
#if DEBUG
                System
                .Diagnostics
                .Debug
                .WriteLine(
                    $"Retry #{i} due to exception '{(e.InnerException ?? e).Message}'");
#endif
            });
        });

        return await Policy
            .Handle<Exception>()
            .RetryAsync(retryCount, onRetry ?? onRetryInner)
            .ExecuteAsync(action);
    }

    internal async Task<T> WaitAndRetryInner<T>(
        Func<Task<T>> action,
        Func<int, TimeSpan> sleepDurationProvider,
        int retryCount = 1,
        Func<Exception, TimeSpan, Task> onRetryAsync = null)
    {
        var onRetryInner = new Func<Exception, TimeSpan, Task>((e, t) =>
        {
            return Task.Factory.StartNew(() =>
            {
#if DEBUG
                System
                .Diagnostics
                .Debug
                .WriteLine(
                    $"Retrying in {t.ToString("g")} due to exception '{(e.InnerException ?? e).Message}'");
#endif
            });
        });

        return await Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount,
                sleepDurationProvider,
                onRetryAsync ?? onRetryInner)
            .ExecuteAsync(action);
    }
    #endregion
}
