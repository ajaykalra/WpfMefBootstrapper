using System;
using System.ComponentModel.Composition;
using System.Reactive.Subjects;

namespace WpfMefBootstrapper
{
    [Export]
    internal class StreamsProvider
    {
        [ImportingConstructor]
        internal StreamsProvider()
        {
            Title = new BehaviorSubject<string>(string.Empty);
        }

        [Export(StreamNames.Title)]
        [Export(StreamNames.Title, typeof(IObservable<string>))]
        public ISubject<string> Title { get; }
    }
}
