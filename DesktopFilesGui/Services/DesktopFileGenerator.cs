using System.Text;
using DesktopFilesGui.Models;
using DesktopFilesGui.Services.Interfaces;

namespace DesktopFilesGui.Services;

public class DesktopFileGenerator : IDesktopFileGenerator
{
    public string Generate(DesktopFile desktopFile)
    {
        var fileContentBuilder = new StringBuilder();
        
        fileContentBuilder
            .AppendLine(Configuration.DESKTOP_FILE_STARTING)
            .AppendLine($"{Configuration.TYPE_KEY}={desktopFile.Type}")
            .AppendLine($"{Configuration.TERMINAL_KEY}={desktopFile.ShowTerminal
                .ToString()
                .ToLower()}")
            .AppendLine($"{Configuration.NO_DISPLAY_KEY}={(!desktopFile.DisplayInMenu)
                .ToString()
                .ToLower()}")
            .AppendLine($"{Configuration.HIDDEN_KEY}={desktopFile.IsHidden
                .ToString()
                .ToLower()}")
            .AppendLine($"{Configuration.DBUS_ACTIVATABLE_KEY}={desktopFile.RunFromDBus
                .ToString()
                .ToLower()}")
            .AppendLine($"{Configuration.STARTUP_NOTIFY_KEY}={desktopFile.StartupNotifySupport
                .ToString()
                .ToLower()}");
        
        return fileContentBuilder.ToString();
    }
}