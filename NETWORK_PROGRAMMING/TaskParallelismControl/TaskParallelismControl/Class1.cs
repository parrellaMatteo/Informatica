namespace TaskParallelismControl;
 
using System.Collections.Concurrent;
 
/// <summary>
/// Provides extension methods for executing asynchronous operations in parallel with controlled degree of parallelism.
/// Useful for throttling concurrent operations to avoid resource saturation (network, CPU, memory).
/// </summary>
/// <remarks>
/// Based on the pattern described at:
/// https://medium.com/@nirinchev/executing-a-collection-of-tasks-in-parallel-with-control-over-the-degree-of-parallelism-in-c-508d59ddffc6
/// </remarks>
public static class TaskConcurrencyHelper
{
    /// <summary>
    /// Executes an asynchronous operation on each element of a collection in parallel,
    /// with a controlled maximum degree of parallelism.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to process.</param>
    /// <param name="processor">The asynchronous operation to execute on each element.</param>
    /// <param name="degreeOfParallelism">The maximum number of concurrent tasks to use.</param>
    /// <returns>A task that completes when all elements have been processed.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> or <paramref name="processor"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="degreeOfParallelism"/> is less than 1.</exception>
    /// <example>
    /// <code>
    /// var urls = new[] { "https://example.com", "https://example.org" };
    /// await urls.ExecuteInParallel(
    ///     async url => await DownloadAsync(url),
    ///     degreeOfParallelism: 5);
    /// </code>
    /// </example>
    public static async Task ExecuteInParallel<T>(
        this IEnumerable<T> collection,
        Func<T, Task> processor,
        int degreeOfParallelism)
    {
        // Create a thread-safe queue from the input collection
        var queue = new ConcurrentQueue<T>(collection);
 
        // Create worker tasks equal to the degree of parallelism
        // Each worker will compete to dequeue and process items
        var tasks = Enumerable.Range(0, degreeOfParallelism).Select(async _ =>
                {
                    // Each worker continuously dequeues and processes items until the queue is empty
                    while (queue.TryDequeue(out T? item))
                    {
                        await processor(item);
                    }
                });
 
        // Wait for all worker tasks to complete
        await Task.WhenAll(tasks);
    }
 
    /// <summary>
    /// Executes an asynchronous operation on each element of a collection in parallel,
    /// with a controlled maximum degree of parallelism, and collects the results.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <typeparam name="TResult">The type of result produced by processing each element.</typeparam>
    /// <param name="collection">The collection to process.</param>
    /// <param name="processor">The asynchronous operation to execute on each element that returns a result.</param>
    /// <param name="degreeOfParallelism">The maximum number of concurrent tasks to use.</param>
    /// <returns>
    /// A task that completes when all elements have been processed,
    /// containing a <see cref="ConcurrentBag{T}"/> with all the results.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> or <paramref name="processor"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="degreeOfParallelism"/> is less than 1.</exception>
    /// <example>
    /// <code>
    /// var urls = new[] { "https://example.com", "https://example.org" };
    /// var results = await urls.ExecuteInParallel(
    ///     async url => await client.GetByteArrayAsync(url),
    ///     degreeOfParallelism: 5);
    /// Console.WriteLine($"Total bytes: {results.Sum(r => r.Length)}");
    /// </code>
    /// </example>
    public static async Task<ConcurrentBag<TResult>> ExecuteInParallel<T, TResult>(
        this IEnumerable<T> collection,
        Func<T, Task<TResult>> processor,
        int degreeOfParallelism)
    {
        // Create a thread-safe queue from the input collection
        var queue = new ConcurrentQueue<T>(collection);
 
        // Create a thread-safe bag to collect results from all workers
        var results = new ConcurrentBag<TResult>();
 
        // Create worker tasks equal to the degree of parallelism
        // Each worker will compete to dequeue and process items
        var tasks = Enumerable.Range(0, degreeOfParallelism).Select(async _ =>
        {
            // Each worker continuously dequeues, processes items, and adds results until the queue is empty
            while (queue.TryDequeue(out T? item))
            {
                results.Add(await processor(item));
            }
        });
 
        // Wait for all worker tasks to complete
        await Task.WhenAll(tasks);
        return results;
    }
}