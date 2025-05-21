using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Media;
using System;
using System.Windows.Input;
using ActiproSoftware;

namespace ModelUI
{

    /// <summary>
    /// Provides the application view-model.
    /// </summary>
    public partial class ApplicationViewModel : ObservableObjectBase
    {

        // private bool _arePrivateItemsAvailable = false;
        // private bool _isDrawerOpen = true;
        // private readonly NavigationService _navigationService = new();
        // private ProductData? _productData;
        // private bool _showPrivateItems = false;
        // private string? _statusMessage;
        // private Visual? _viewElement;
        // private bool _viewHasCustomStatusBar;
        // private bool _viewHasNavigationButtons;
        private IImage? _viewImageSource;
        // private ProductItemInfo? _viewItemInfo;
        // private string? _viewSubTitle;
        // private string? _viewTitle;
        private bool _viewTransitionForward = true;
        //
        // private DelegateCommand<object?>? _navigateViewToHomeCommand;
        private DelegateCommand<object>? _navigateViewToItemInfoCommand;
        // private DelegateCommand<object?>? _navigateViewToNextItemInfoCommand;
        // private DelegateCommand<object?>? _navigateViewToPreviousItemInfoCommand;
        // private DelegateCommand<object>? _openUrlCommand;
        // private DelegateCommand<UserInterfaceDensity?>? _setUserInterfaceDensityCommand;
        private DelegateCommand<object?>? _toggleDrawerOpenCommand;
        // private DelegateCommand<object?>? _toggleShowPrivateItemsCommand;
        //
        // public const string ProductDataResourceKey = "AppProductData";
        //
        // private const string DefaultSampleUri = null;

        private ApplicationViewModel()
        {
            // Internal initialization
            var methodInfo = this.GetType().GetMethod("InitializePrivate", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            methodInfo?.Invoke(this, Array.Empty<object>());


        }

        public static ApplicationViewModel Instance { get; } = new ApplicationViewModel();

        public IMessageService? MessageService { get; set; }

        public void NavigateViewBackward()
        {

        }

        public void NavigateViewForward()
        {

        }

        public ICommand NavigateViewToItemInfoCommand
        {
            get
            {
                return _navigateViewToItemInfoCommand ??= new DelegateCommand<object>((param) =>
                {

                });
            }
        }

        public IPlatformHelper? PlatformHelper { get; set; }

        public ICommand ToggleDrawerOpenCommand
            => _toggleDrawerOpenCommand ??= new DelegateCommand<object?>(_ =>
            {

            });
        public IImage? ViewImageSource
        {
            get => _viewImageSource;
            set => SetProperty(ref _viewImageSource, value);
        }


        public bool ViewTransitionForward
        {
            get => _viewTransitionForward;
            set => SetProperty(ref _viewTransitionForward, value);
        }

    }

}
