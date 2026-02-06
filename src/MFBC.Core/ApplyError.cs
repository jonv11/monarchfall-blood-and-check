namespace MFBC.Core;

/// <summary>
/// Represents an error produced during ApplyAction.
/// </summary>
public sealed record ApplyError(string Code, string Message);
