using System.Windows;
using System.Windows.Controls;
using System.Collections.Specialized;
using AIClient.ViewModels;

namespace AIClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Subscribe to collection changes to auto-scroll
            if (DataContext is MainViewModel vm)
            {
                vm.Messages.CollectionChanged += Messages_CollectionChanged;
            }
        }

        private void Messages_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // Ensure scroll to bottom happens after the UI is updated
                MessagesScroller.Dispatcher.InvokeAsync(() =>
                {
                    MessagesScroller.ScrollToBottom();
                }, System.Windows.Threading.DispatcherPriority.Loaded);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            // Clean up the event handler
            if (DataContext is MainViewModel vm)
            {
                vm.Messages.CollectionChanged -= Messages_CollectionChanged;
            }
            base.OnClosed(e);
        }
    }
}