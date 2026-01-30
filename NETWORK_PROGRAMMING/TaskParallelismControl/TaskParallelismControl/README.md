# TaskParallelismControl
 
.NET library providing extension methods for executing asynchronous operations in parallel with a controlled degree of parallelism (throttling). Useful to avoid saturating network/CPU/resources when processing large collections.
 
## Features
 
- Controlled concurrency via a fixed number of worker tasks
- Two overloads:
  - `ExecuteInParallel<T>(Func<T, Task>, ...)`
  - `ExecuteInParallel<T, TResult>(Func<T, Task<TResult>>, ...)`
- Thread-safe implementation based on `ConcurrentQueue<T>` and `ConcurrentBag<TResult>`
 
## Installation
 
If published on NuGet:
 
```bash
dotnet add package TaskParallelismControl
```
 
Local feed scenario (see `LocalNuget` folder in the solution):
 
```bash
dotnet nuget add source <PATH_TO_LocalNuget> -n LocalNuget
dotnet add package TaskParallelismControl --source LocalNuget
```
 
## Quick start
 
### Example: with results
 
```csharp
using TaskParallelismControl;
 
var urls = new[]
{
    "https://learn.microsoft.com/",
    "https://learn.microsoft.com/dotnet/",
};
 
const int maxParallelism = 5;
 
var results = await urls.ExecuteInParallel(
    async url =>
    {
        using var client = new HttpClient();
        var bytes = await client.GetByteArrayAsync(url);
        return bytes.Length;
    },
    maxParallelism);
 
Console.WriteLine($"Total bytes: {results.Sum()}");
```
 
### Example: without results
 
```csharp
using TaskParallelismControl;
 
var items = Enumerable.Range(1, 100);
 
await items.ExecuteInParallel(
    async i =>
    {
        await Task.Delay(50);
        Console.WriteLine(i);
    },
    degreeOfParallelism: 8);
```
 
## Notes
 
- Target framework: `net9.0`.
- Pack with `dotnet pack`.
 
 
# TaskParallelismControl
 
.NET library providing extension methods for executing asynchronous operations in parallel with a controlled degree of parallelism (throttling). Useful to avoid saturating network/CPU/resources when processing large collections.
 
## Features
 
- Controlled concurrency via a fixed number of worker tasks
- Two overloads:
  - `ExecuteInParallel<T>(Func<T, Task>, ...)`
  - `ExecuteInParallel<T, TResult>(Func<T, Task<TResult>>, ...)`
- Thread-safe implementation based on `ConcurrentQueue<T>` and `ConcurrentBag<TResult>`
 
## Installation
 
If published on NuGet:
 
```bash
dotnet add package TaskParallelismControl
```
 
Local feed scenario (see `LocalNuget` folder in the solution):
 
```bash
dotnet nuget add source <PATH_TO_LocalNuget> -n LocalNuget
dotnet add package TaskParallelismControl --source LocalNuget
```
 
## Quick start
 
### Example: with results
 
```csharp
using TaskParallelismControl;
 
var urls = new[]
{
    "https://learn.microsoft.com/",
    "https://learn.microsoft.com/dotnet/",
};
 
const int maxParallelism = 5;
 
var results = await urls.ExecuteInParallel(
    async url =>
    {
        using var client = new HttpClient();
        var bytes = await client.GetByteArrayAsync(url);
        return bytes.Length;
    },
    maxParallelism);
 
Console.WriteLine($"Total bytes: {results.Sum()}");
```
 
### Example: without results
 
```csharp
using TaskParallelismControl;
 
var items = Enumerable.Range(1, 100);
 
await items.ExecuteInParallel(
    async i =>
    {
        await Task.Delay(50);
        Console.WriteLine(i);
    },
    degreeOfParallelism: 8);
```
 
## Notes
 
- Target framework: `net9.0`.
- Pack with `dotnet pack`.