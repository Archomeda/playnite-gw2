using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace PlayniteGw2.Proxy
{
    [SuppressMessage("Major Code Smell", "S3881:\"IDisposable\" should be implemented correctly", Justification = "Proxy")]
    internal class ProcessMonitorProxy : IDisposable
    {
        private const string TYPE_NAME = "Playnite.ProcessMonitor, Playnite";
        private static readonly Type typeObject = Type.GetType(TYPE_NAME);
        private static readonly MethodInfo watchProcessTreeProxy = typeObject.GetMethod(nameof(WatchProcessTree));
        private static readonly MethodInfo watchDirectoryProcessesProxy = typeObject.GetMethod(nameof(WatchDirectoryProcesses));
        private static readonly MethodInfo stopWatchingProxy = typeObject.GetMethod(nameof(StopWatching));

        private readonly IDisposable processMonitor;

        public ProcessMonitorProxy()
        {
            this.processMonitor = (IDisposable)Activator.CreateInstance(typeObject);
        }

        public void WatchProcessTree(Process process) =>
            watchProcessTreeProxy.Invoke(this.processMonitor, new object[] { process });

        public void WatchDirectoryProcesses(string directory, bool alreadyRunning, bool byProcessNames = false) =>
            watchDirectoryProcessesProxy.Invoke(this.processMonitor, new object[] { directory, alreadyRunning, byProcessNames });

        public void StopWatching() =>
            stopWatchingProxy.Invoke(this.processMonitor, new object[] { });

        public void Dispose() =>
            this.processMonitor.Dispose();
    }
}
