using FluentResults;

namespace VDesk.Core.Errors;

internal abstract class VdeskError(string message) : Error(message)
{
    public abstract int ErrorCode { get; }
}

internal class InitializationError(Type type) : VdeskError($"Failed to create an instance of {type}")
{
    public override int ErrorCode => 2;
}

internal class NotFoundError(string entityName, Guid id) : VdeskError($"'{entityName}' with id '{id}' not found.")
{
    public string EntityName { get; } = entityName;

    public Guid Id { get; } = id;
    public override int ErrorCode => 3;
}