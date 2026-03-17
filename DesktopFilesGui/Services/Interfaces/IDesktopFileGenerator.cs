using DesktopFilesGui.Models;

namespace DesktopFilesGui.Services.Interfaces;

public interface  IDesktopFileGenerator
{
    public string Generate(DesktopFile desktopFile);
}