using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace WpfMefBootstrapper.ClientServices
{
    
    class BackgroundClientService
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();

        [Export(ExportNames.ApplicationBackgroundTaskAtStartup)]
        public void DoSomethingInBackground()
        {
            Log.Debug($"Backgound Task Started; Is Background: {Thread.CurrentThread.IsBackground}: ThreadID: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
