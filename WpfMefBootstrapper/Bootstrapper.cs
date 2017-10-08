using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace WpfMefBootstrapper
{
    public class Bootstrapper
    {
        private string[] _args;

        private static Logger Log = LogManager.GetCurrentClassLogger();

        public Bootstrapper(string[] args)
        {
            Log.Info($"Starting Application. Thread: {Thread.CurrentThread.ManagedThreadId}; Isbackground: {Thread.CurrentThread.IsBackground}");
            _args = args;
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

            // Push commandline arguments to the container.
            MefContainer.ComposeExportedValue(new CommandLineArguments { Arguments = _args });

          
          
            PerformApplicatoinStartupTasks();
        }

        
        public static CompositionContainer MefContainer { get; private set; }


        private void PerformApplicatoinStartupTasks()
        {
            // Create Application Startup objects
            // Following works as well, creating all objects 
            //var appServices = MefContainer.GetExportedValues<object>(ExportNames.ApplicationCreateAtStartup);

            Log.Info("Starting ApplicationCreateAtStartup");
            var appServices = MefContainer.GetExports<object>(ExportNames.ApplicationCreateAtStartup);

            appServices.Select(s => s.Value)
                        .Do(s => Log.Debug($"Started {s.GetType()}"))
                        .LastOrDefault();

            // Now perform tasks if any, using List to just be able to call Count
            List<Action> backgroundTasks = MefContainer.GetExportedValues<Action>(ExportNames.ApplicationBackgroundTaskAtStartup).ToList();

            Log.Debug(CultureInfo.InvariantCulture, $"Starting up {backgroundTasks.Count} tasks on task pool.");
            backgroundTasks.ForEach(task => Task.Factory.StartNew(task));
        }
    }
}
