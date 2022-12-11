namespace Sauter.InventoryManagement;

public class AuditLogger
{
    private static AuditLogger? instance;
    internal List<AuditLog> Logs { get; }
    
    private static readonly object padlock = new();

    AuditLogger()
    {
        Logs = new List<AuditLog>();
    }
    
    public static AuditLogger Instance
    {
        get
        {
            lock (padlock)
            {
                return instance ??= new AuditLogger();
            }
        }
    }
}

internal class AuditLog
{
    public string Action { get; set; }
    public DateTime DateOfOccurence { get; set; }
}

public struct AuditLogDto
{
    public string Action { get; init; }
    public DateTime DateOfOccurence { get; set; }
}