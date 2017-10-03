using System.ComponentModel.Composition;
using NLog;

namespace WpfMefBootstrapper
{
    [Export]
    internal class SomeViewModel
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();

        [ImportingConstructor]
        SomeViewModel()
        {
            Log.Debug("Created SomeViewModel....");
        }

        public string SomeText => "Hello World";
    }
}
