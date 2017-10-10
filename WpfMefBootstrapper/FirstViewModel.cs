using System;
using System.ComponentModel.Composition;
using System.Reactive.Subjects;
using NLog;

namespace WpfMefBootstrapper
{
    [Export]
    internal class FirstViewModel 
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();

        [ImportingConstructor]
        FirstViewModel()
        {
            Log.Debug("Created FirstViewModel....");
        }

        public string SomeText
        {
            get
            {
                var text = "Text from FirstViewModel";
                Title.OnNext(text);
                return text;
            }
        }

        [Import(StreamNames.Title)]
        public ISubject<string> Title { get; set; }
    }
}
