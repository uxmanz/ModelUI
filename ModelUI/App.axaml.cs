using ActiproSoftware.UI.Avalonia.Media;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace ModelUI;

/// <summary>
/// Represents the Avalonia application.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Initializes the application.
    /// </summary>
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        RequestedThemeVariant = ThemeVariant.Light;
        ImageProvider.Default.ChromaticAdaptationMode = ImageChromaticAdaptationMode.DarkThemes;
    }

    /// <summary>
    /// Occurs after framework initialization has completed.
    /// </summary>
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Create a root window for desktop applications
            desktop.MainWindow = new WindowMain();
        }

        base.OnFrameworkInitializationCompleted();
    }
}