
namespace Sauter.AuditLog;

internal class AuditLogger
{
    private static AuditLogger? _instance;
    internal List<AuditLog> Logs { get; }

    private static readonly object Padlock = new();

    AuditLogger()
    {
        Logs = new List<AuditLog>();
    }

    internal static AuditLogger Instance
    {
        get
        {
            lock (Padlock)
            {
                return _instance ??= new AuditLogger();
            }
        }
    }
}

internal class AuditLog
{
    public string Action { get; set; }
    public DateTime DateOfOccurence { get; set; }

    internal AuditLogDto ToDto => new()
    {
        Action = Action,
        DateOfOccurence = DateOfOccurence
    };
}

public struct AuditLogDto
{
    public string Action { get; init; }
    public DateTime DateOfOccurence { get; set; }
}