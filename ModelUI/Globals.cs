using System.Collections.Generic; // Currently unused - remove if unnecessary
using System.IO;
using Salaros.Configuration;

namespace ModelUI;

/// <summary>
/// Static class containing global application configuration and metadata.
/// </summary>
public static class Globals
{
    /// <summary>
    /// The global configuration parser for reading from Config.inf.
    /// </summary>
    public static ConfigParser appConfig;

    /// <summary>
    /// Main title of the application. Defaults to "MainTitle".
    /// </summary>
    public static string MainTitle = "MainTitle";

    /// <summary>
    /// Subtitle of the application. Defaults to "SubTitle".
    /// </summary>
    public static string SubTitle = "SubTitle";

    /// <summary>
    /// Initializes the global configuration by loading or creating Config.inf.
    /// 
    /// If the file does not exist, a default section is written.
    /// Then it attempts to read the application titles from the config file.
    /// Any missing values are set to defaults and saved back to the config file.
    /// </summary>
    public static void InitConfigs()
    {
        // Create default config file if it does not exist
        if (!File.Exists("Config.inf"))
        {
            File.AppendAllText("Config.inf", "[Config]");
        }

        // Load configuration
        appConfig = new ConfigParser("Config.inf");

        // Read or set default values for MainTitle and SubTitle
        MainTitle = appConfig.GetValue("Config", "MainTitle", MainTitle);
        SubTitle = appConfig.GetValue("Config", "SubTitle", SubTitle);

        // Save any changes (e.g., if defaults were added)
        appConfig.Save();
    }
}