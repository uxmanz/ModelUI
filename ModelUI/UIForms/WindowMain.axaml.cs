using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Avalonia.Interactivity;
using ActiproSoftware.UI.Avalonia.Themes;
using Avalonia;
using Avalonia.Controls.Notifications;
using Avalonia.Input;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Database;
using ModelUI.Views;
using Newtonsoft.Json;

namespace ModelUI;

/// <summary>
/// Main window for the application, managing navigation and layout between views.
/// </summary>
public partial class WindowMain : Window
{
    /// <summary>
    /// Main view instance displayed by default.
    /// </summary>
    public MainView MainView;

    /// <summary>
    /// Delta view instance for comparison or alternate view.
    /// </summary>
    public DeltaView DeltaView;
    public DatabaseHelper databaseHelper = new DatabaseHelper();

    /// <summary>
    /// Initializes a new instance of the <see cref="WindowMain"/> class.
    /// </summary>
    ///

    public WindowMain()
    {
        InitializeComponent();
        setApplicationVersioninTitle(); // Set application title using assembly metadata
        Globals.InitConfigs(); // Load global config values
        loadDBValue();

        titleTextBlock.Text = Globals.MainTitle;
        subtitleTextBlock.Text = Globals.SubTitle;

        MainView = new MainView();
        MainView.MainContext(this);

        DeltaView = new DeltaView();
        DeltaView.MainContext(this);

        transitionControl.Content = MainView; // Set initial view
    }

    private void loadDBValue()
    {
        List<generalDataClass> getAllDBData = databaseHelper.GetAllDataFromTable("GeneralSettings");
        generalDataClass SomeKey = databaseHelper.GetKeyValue("SomeKey", new generalDataClass
        {
            ID = 0,
            Name = "SomeKey",
            JSON = "{Some JSON}",
            AnyData = "some default value"
        });
        //Test Code
        // databaseHelper.InsertUpdateInTable(new generalDataClass
        // {
        //     ID = 0,
        //     Name = "SomeOtherKey",
        //     JSON = "{Some Other JSON}",
        //     AnyData = "some other default value"
        // }, "GeneralSettings");

        generalDataClass SomeOtherKey = databaseHelper.GetKeyValue("SomeOtherKey", new generalDataClass
        {
            ID = 0,
            Name = "SomeOtherKey",
            JSON = "{Some Other JSON test}",
            AnyData = "some other default value test"
        });

    }

    /// <summary>
    /// Handles selection change for the sidebar menu and switches views accordingly.
    /// </summary>
    private void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        // NOTE: This can be made more robust by avoiding casting UI elements directly.
        var rx = (TextBlock)(((StackPanel)(((ListBox)sender).SelectedItems[0])).Children[1]);

        if (rx.Text.Contains("Main View"))
        {
            transitionControl.Content = MainView;
        }
        else if (rx.Text.Contains("Delta View"))
        {
            transitionControl.Content = DeltaView;
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // NON-PUBLIC PROCEDURES
    /////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Sets the window title and version text from the assembly product attribute.
    /// The version number automatically increments with each build.
    /// The app name is read from the assembly's product attribute stated in csproj file.
    /// </summary>
    private void setApplicationVersioninTitle()
    {
        AssemblyProductAttribute productAttr = typeof(WindowMain).Assembly
            .GetCustomAttributes(typeof(AssemblyProductAttribute), true)[0] as AssemblyProductAttribute;

        Title = $"{productAttr.Product}";
        versionTextBlock.Text = $"{productAttr.Product}";
    }

    /// <summary>
    /// Handles property changes in the ViewModel, especially for navigation direction.
    /// </summary>
    private void OnApplicationViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ApplicationViewModel.ViewTransitionForward))
        {
            if (transitionControl.PageTransition is DirectionalPageSlide transition)
                transition.UseForwardDirection = ViewModel?.ViewTransitionForward ?? true;
        }
    }

    /// <summary>
    /// Prepares the View menu based on the current theme. (Stubbed - logic not implemented)
    /// </summary>
    private void OnViewMenuOpening(object? sender, EventArgs e)
    {
        if (ModernTheme.TryGetCurrent(out var theme))
        {
            var definition = theme.Definition;
            if (definition is not null)
            {
                // Add theme-based logic if required
            }
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // PUBLIC PROCEDURES
    /////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Called when the window is attached to the visual tree. Initializes notification manager.
    /// </summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        var notificationManager = new WindowNotificationManager(TopLevel.GetTopLevel(this));
        if ((notificationManager is not null) && (ViewModel is not null))
            ViewModel.MessageService = new NotificationMessageService(notificationManager);

        base.OnAttachedToVisualTree(e);
    }

    /// <summary>
    /// Toggles the sidebar pane visibility when the application button is clicked.
    /// </summary>
    private void ApplicationButton_OnClick(object? sender, RoutedEventArgs e)
    {
        splitView.IsPaneOpen = !splitView.IsPaneOpen;
    }

    /// <summary>
    /// Handles pointer (mouse) release to support back/forward navigation via mouse buttons.
    /// </summary>
    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);

        if (!e.Handled)
        {
            switch (e.InitialPressMouseButton)
            {
                case MouseButton.XButton1:
                    ViewModel?.NavigateViewBackward();
                    break;
                case MouseButton.XButton2:
                    ViewModel?.NavigateViewForward();
                    break;
            }
        }
    }

    /// <summary>
    /// Gets or sets the ViewModel associated with the window.
    /// </summary>
    public ApplicationViewModel? ViewModel
    {
        get => DataContext as ApplicationViewModel;
        set => DataContext = value;
    }
}
