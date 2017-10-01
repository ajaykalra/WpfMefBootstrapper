using System.ComponentModel.Composition;

namespace WpfMefBootstrapper
{
    [Export]
    class SomeViewModel
    {
        public string SomeText => "Hello World";
    }
}
