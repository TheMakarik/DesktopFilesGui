using System.Diagnostics;
using DesktopFilesGui.Services.Interfaces;

namespace DesktopFilesGui.Services;

public class GithubSourceOpener : IGithubSourceOpener
{
    public void Open()
    {
        var startInfo = new ProcessStartInfo
        {
            UseShellExecute = true,
            FileName = Configuration.GITHUB_LINK
        };
        
        Process.Start(startInfo);
    }
}