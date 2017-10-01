using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using NLog;

namespace WpfMefBootstrapper
{
    public class Bootstrapper
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();

        public Bootstrapper()
        {
            Initialize();
        }

        private void Initialize()
        {
            Log.Info("Starting MEF Composition");

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var dllsCatalog = new DirectoryCatalog(path, "*.dll");
            var exeCatalog = new DirectoryCatalog(path, "*.exe");

            MefContainer = new CompositionContainer(new AggregateCatalog(dllsCatalog, exeCatalog));

            Log.Info("Finished MEF Composition. Number of Parts: {0}", MefContainer.Catalog.Parts.Count());
            Log.Info("List of Parts -- Begin");

            foreach (var composablePartDefinition in MefContainer.Catalog.Parts)
            {
                Log.Info(composablePartDefinition.ToString());
            }

            Log.Info("List of Parts -- End");

            //  var provider = MefContainer.GetExportedValue();
            // var appServices = Container.GetExportedValue(ExportNames.ApplicationService);
        }

        public static CompositionContainer MefContainer { get; private set; }
    }
}
