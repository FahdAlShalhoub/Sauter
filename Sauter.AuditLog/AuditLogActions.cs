using System.Reflection;

namespace Sauter.AuditLog;

public static class AuditLogActions
{
    public static void Log(MethodBase method, IEnumerable<object> arguments)
    {
        var aggregateArguments = arguments
            .Select(arg => arg.ToString());

        var argsString = string.Join(" - ", aggregateArguments);

        AuditLog auditLog = new AuditLog
        {
            Action = $"Action {method.Name} Was Committed With Input {argsString}",
            DateOfOccurence = DateTime.Now
        };
        
        AuditLogger.Instance.Logs.Add(auditLog);
    }
    
    public static List<AuditLogDto> GetAuditLogs()
    {
        return AuditLogger.Instance.Logs.Select(log => log.ToDto).ToList();
    }

    public static void Reset()
    {
        AuditLogger.Instance.Logs.Clear();
    }
}