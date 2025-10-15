namespace eWorldCup.Core.RailwayOriented;

public sealed class Result<T>
{
    public static Result<T> Success(T value) => new Result<T>(value);
    public static Result<T> Failure(Error error) => new Result<T>(error);
    public readonly bool IsSuccess;
    public bool IsFailure => !IsSuccess;
    
    private readonly T? _value;
    private readonly Error? _error;
    public T Value => IsSuccess && _value is not null 
        ? _value 
        : throw new InvalidOperationException("No value present");
    public Error Error => IsFailure && _error is not null 
        ? _error 
        : throw new InvalidOperationException("No error present");

    private Result(T value)
    {
        IsSuccess = true;
        _value = value;
    }

    private Result(Error error)
    {
        IsSuccess = false;
        _error = error;
    }
}


public abstract class Error(string message)
{
    
}
