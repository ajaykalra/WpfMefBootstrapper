using System;
using System.ComponentModel.Composition;

namespace WpfMefBootstrapper
{
    [Export]
    public class SecondViewModel : BindableBase, IDisposable
    {
        private IDisposable _disposable;

        [ImportingConstructor]
        SecondViewModel([Import(StreamNames.Title)] IObservable<string> title)
        {
            _disposable = title.Subscribe(t => {
                                                    SomeText = t;
                                                    OnPropertyChanged(SomeText);
                                                });
        }

        public string SomeText { get; set; }

        public void Dispose()
        {  
            _disposable.Dispose();          
        }
    }
}
