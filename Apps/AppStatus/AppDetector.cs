using System.Diagnostics;

public class AppDetector
{
    public static string CheckForApp(IEnumerable<string> apps)
    {
        return apps
            .FirstOrDefault(app => GetProcessNames()
                .Any(p => p.Contains(app.ToLower())))
        ?? "";
    }
    
    static IEnumerable<string> GetProcessNames()
    {
        return Process.GetProcesses()
            .Select(p => p.ProcessName.ToLower())
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .Distinct()
            .Order()
            .ToArray();
    }
}