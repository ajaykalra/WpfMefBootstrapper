using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using NLog;

namespace WpfMefBootstrapper
{
    public static class ViewModelLocator
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();
        public static bool GetAutoWireViewModel(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoWireViewModelProperty, value);
        }

        public static readonly DependencyProperty AutoWireViewModelProperty =
            DependencyProperty.RegisterAttached("AutoWireViewModel",
            typeof(bool), typeof(ViewModelLocator),
            new PropertyMetadata(false, AutoWireViewModelChanged));

        private static void AutoWireViewModelChanged(DependencyObject d,
             DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d)) return;
            var viewType = d.GetType();
            var viewModelTypeName = viewType + "Model";
            var vType = Type.GetType(viewModelTypeName);
            var viewModel = Bootstrapper.MefContainer.GetExports(vType, null, null).FirstOrDefault()?.Value;
            if (viewModel != null)
            {
                ((FrameworkElement) d).DataContext = viewModel;
                Log.Info("Found {0} ViewModel");
            }
            else
                Log.Error("ViewModelLocatorError: {0} not found in MEF container", vType);
        }
    }
}
