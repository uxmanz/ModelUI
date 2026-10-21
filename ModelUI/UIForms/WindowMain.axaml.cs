using Avalonia.Controls;
using Avalonia.Interactivity;
using Database;
using ModelUI.Views;
using System;
using System.Collections.Generic;
using System.Reflection;

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

    /// <summary>
    /// Sets the window title and version text from the assembly product attribute.
    /// The version number automatically increments with each build.
    /// The app name is read from the assembly's product attribute stated in csproj file.
    /// </summary>
    private void setApplicationVersioninTitle()
    {
        AssemblyProductAttribute productAttr = typeof(WindowMain).Assembly
            .GetCustomAttributes(typeof(AssemblyProductAttribute), true)[0] as AssemblyProductAttribute;

        // Title = $"{productAttr.Product}";
        versionTextBlock.Text = $"{productAttr.Product}";
    }

    /// <summary>
    /// Toggles the sidebar pane visibility when the application button is clicked.
    /// </summary>
    private void ApplicationButton_OnClick(object? sender, RoutedEventArgs e)
    {
        splitView.IsPaneOpen = !splitView.IsPaneOpen;
    }

    /// <summary>
    /// Opens the ribbon-based sample window.
    /// </summary>
    private void RibbonWindow_OnClick(object? sender, RoutedEventArgs e)
    {
        new RibbonWindow().Show(this);
    }

    /// <summary>
    /// Updates the status bar for placeholder menu items that don't do anything in this sample.
    /// </summary>
    private void PlaceholderMenu_OnClick(object? sender, RoutedEventArgs e)
    {
        statusTextBlock.Text = $"{((MenuItem)sender).Header} - placeholder item in this sample";
    }
}