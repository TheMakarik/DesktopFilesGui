using System.Diagnostics;
using DesktopFilesGui.Services.Interfaces;
using Serilog;

namespace DesktopFilesGui.Services;

public class GithubSourceOpener(ILogger logger) : IGithubSourceOpener
{
    public void Open()
    {
        logger.Information("Opening Github project's source {source}", Configuration.GITHUB_LINK);
        var startInfo = new ProcessStartInfo
        {
            UseShellExecute = true,
            FileName = Configuration.GITHUB_LINK
        };
        
        Process.Start(startInfo);
    }
}