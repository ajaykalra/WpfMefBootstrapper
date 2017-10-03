using System.ComponentModel.Composition;
using NLog;

namespace WpfMefBootstrapper.ClientServices
{
    [Export(ExportNames.ApplicationCreateAtStartup)]
    internal class SampleClientService
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();

        [ImportingConstructor]
        SampleClientService()
        {
           Log.Debug("Created SampleClientService...."); 
        }

    }
}
