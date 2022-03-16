namespace Application;

public class OperationResult<T>
    where T : class
{
    public Status Status { get; init; }
    public T? Result { get; init; }

    public OperationResult(Status status, T? result)
    {
        Status = status;
        Result = result;
    }
}
