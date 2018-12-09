using System;
using System.Diagnostics;
using System.Reflection;

namespace PlayniteGw2.Proxy
{
    internal class ProcessMonitorProxy : IDisposable
    {
        private const string TypeName = "Playnite.ProcessMonitor, Playnite";
        private static readonly Type TypeObject;
        private static readonly MethodInfo WatchProcessTreeProxy;
        private static readonly MethodInfo WatchDirectoryProcessesProxy;
        private static readonly MethodInfo StopWatchingProxy;

        private readonly IDisposable processMonitor;

        static ProcessMonitorProxy()
        {
            TypeObject = Type.GetType(TypeName);
            if (TypeObject == null)
                throw new InvalidOperationException($"Type {TypeName} could not be found, cannot proceed.");
            WatchProcessTreeProxy = TypeObject.GetMethod(nameof(WatchProcessTree));
            WatchDirectoryProcessesProxy = TypeObject.GetMethod(nameof(WatchDirectoryProcesses));
            StopWatchingProxy = TypeObject.GetMethod(nameof(StopWatching));
        }

        public ProcessMonitorProxy()
        {
            this.processMonitor = (IDisposable)Activator.CreateInstance(TypeObject);
        }

        public void Dispose() =>
            this.processMonitor.Dispose();

        public void WatchProcessTree(Process process) =>
            WatchProcessTreeProxy.Invoke(this.processMonitor, new object[] { process });

        public void WatchDirectoryProcesses(string directory, bool alreadyRunning, bool byProcessNames = false) =>
            WatchDirectoryProcessesProxy.Invoke(this.processMonitor, new object[] { directory, alreadyRunning, byProcessNames });

        public void StopWatching() =>
            StopWatchingProxy.Invoke(this.processMonitor, new object[] { });
    }
}
