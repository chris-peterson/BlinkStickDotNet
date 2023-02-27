using System.Diagnostics;

public record ProcessChangeEventArgs(string ProcessName);

public class ProcessDetector
{
    string[] _processNames;

    public ProcessDetector()
    {
        _processNames = GetProcessNames();
        new Thread(MonitorProcessChanges).Start();
    }
    
    public bool IsRunning(string partialMatch)
    {
        return _processNames.Any(p => p.Contains(partialMatch.ToLower()));
    }

    public event EventHandler<ProcessChangeEventArgs> OnProcessStarted;

    public event EventHandler<ProcessChangeEventArgs> OnProcessEnded;

    void MonitorProcessChanges()
    {
        while (true)
        {
            Thread.Sleep(1000);
            var newProcesses = GetProcessNames();
            foreach (var p in _processNames)
            {
                if (!newProcesses.Contains(p))
                {
                    OnProcessEnded?.Invoke(this, new ProcessChangeEventArgs(p));
                }
            }
            foreach (var p in newProcesses)
            {
                if (!_processNames.Contains(p))
                {
                    OnProcessStarted?.Invoke(this, new ProcessChangeEventArgs(p));
                }
            }
            _processNames = newProcesses;
        }
    }

    string[] GetProcessNames()
    {
        return Process.GetProcesses()
            .Select(p => p.ProcessName.ToLower())
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .Distinct()
            .Order()
            .ToArray();
    }
}