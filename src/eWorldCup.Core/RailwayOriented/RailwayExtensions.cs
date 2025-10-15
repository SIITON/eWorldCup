namespace eWorldCup.Core.RailwayOriented;

public static class RailwayExtensions
{
    /// <summary>
    /// If the result is successful, continue with another operation <paramref name="onSuccessFunction"/> that returns
    /// a result with a return value <typeparamref name="TOut"/>.
    /// If not, return the error.
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="input"></param>
    /// <param name="onSuccessFunction"></param>
    /// <returns></returns>
    public static Result<TOut> OnSuccess<TIn, TOut>(this Result<TIn> input, 
        Func<TIn, Result<TOut>> onSuccessFunction)
    {
        return input.IsSuccess
            ? onSuccessFunction(input.Value)
            : Result<TOut>.Failure(input.Error);
    }

    public static async Task<Result<TOut>> OnSuccessAsync<TIn, TOut>(this Task<Result<TIn>> task, 
        Func<TIn, Task<Result<TOut>>> onSuccessFunction)
    {
        var input = await task;
        return input.IsSuccess
            ? await onSuccessFunction(input.Value)
            : Result<TOut>.Failure(input.Error);
    }

    public static async Task<Result<TOut>> OnSuccessAsync<TIn, TOut>(this Task<Result<TIn>> task, 
        Func<TIn, TOut> onSuccessFunction)
    {
        var input = await task;
        return input.IsSuccess
            ? Result<TOut>.Success(onSuccessFunction(input.Value))
            : Result<TOut>.Failure(input.Error);
    }

    public static async Task<Result<TOut>> OnSuccessAsync<TIn, TOut>(this Result<TIn> input, 
        Func<TIn, Task<Result<TOut>>> onSuccessFunction)
    {
        return input.IsSuccess
            ? await onSuccessFunction(input.Value)
            : Result<TOut>.Failure(input.Error);
    }
    public static async Task<Result<TOut>> OnSuccessAsync<TIn, TOut>(this Task<Result<TIn>> task, 
        Func<TIn, Result<TOut>> onSuccessFunction)
    {
        var input = await task;
        return input.IsSuccess
            ? onSuccessFunction(input.Value)
            : Result<TOut>.Failure(input.Error);
    }

    /// <summary>
    /// If the result is successful, continue with the operation <paramref name="onSuccessFunction"/> using the value.
    /// If not, return the results error.
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="input"></param>
    /// <param name="onSuccessFunction"></param>
    /// <returns></returns>
    public static Result<TOut> OnSuccess<TIn, TOut>(this Result<TIn> input, Func<TIn, TOut> onSuccessFunction)
    {
        return input.IsSuccess
            ? Result<TOut>.Success(onSuccessFunction(input.Value))
            : Result<TOut>.Failure(input.Error);
    }

    public static async Task<Result<TOut>> OnSuccessAsync<TIn, TOut>(this Task<Result<TIn>> task,
        Func<TIn, Task<TOut>> onSuccessFunction)
    {
        var input = await task;
        return input.IsSuccess
            ? Result<TOut>.Success(await onSuccessFunction(input.Value))
            : Result<TOut>.Failure(input.Error);
    }

    public static Result<TOut> OnSuccess<TOut>(this Result<bool> input, Func<TOut> onSuccessFunction)
    {
        return input.IsSuccess ? Result<TOut>.Success(onSuccessFunction()) : Result<TOut>.Failure(input.Error);
    }

    public static async Task<Result<TOut>> OnSuccessAsync<TOut>(this Task<Result<bool>> task, 
        Func<Task<Result<TOut>>> onSuccessFunction)
    {
        var input = await task;
        return input.IsSuccess 
            ? await onSuccessFunction() 
            : Result<TOut>.Failure(input.Error);
    }
    public static async Task<Result<TOut>> OnSuccessAsync<TOut>(this Task<Result<bool>> task, Func<Result<TOut>> onSuccessFunction)
    {
        var input = await task;
        return input.IsSuccess ? onSuccessFunction() : Result<TOut>.Failure(input.Error);
    }

    public static Result<T> DeadEnd<T>(this Result<T> input, Action<T> deadEndFunction)
    {
        if (input.IsSuccess)
            deadEndFunction(input.Value);

        return input;
    }

    public static Result<T> DeadEnd<T>(this Result<T> input, Action deadEndFunction)
    {
        if (input.IsSuccess)
            deadEndFunction();

        return input;
    }

    public static async Task<Result<T>> DeadEndAsync<T>(this Task<Result<T>> task, Action<T> deadEndFunction)
    {
        var input = await task;
        if (input.IsSuccess)
            deadEndFunction(input.Value);

        return input;
    }
    public static async Task<Result<T>> DeadEndAsync<T>(this Task<Result<T>> task, Action deadEndFunction)
    {
        var input = await task;
        if (input.IsSuccess)
            deadEndFunction();

        return input;
    }

    public static Result<TOut> OnBoth<TIn, TOut>(this Result<TIn> input,
        Func<TIn, TOut> sucessSingleTrackFunction,
        Func<TIn, TOut> failureSingleTrackFunction)
    {
        if (input.IsSuccess) return Result<TOut>.Success(sucessSingleTrackFunction(input.Value));

        failureSingleTrackFunction(input.Value);
        return Result<TOut>.Failure(input.Error);
    }

    public static Result<T> OnFailure<T>(this Result<T> input,
        Func<Result<T>, Result<T>> failureTrackReturnOverrideFunction)
    {
        return input.IsFailure ? failureTrackReturnOverrideFunction(input) : input;
    }



}