using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DesktopFilesGui.Attributes;
using DesktopFilesGui.Extensions;
using DesktopFilesGui.Models;
using DesktopFilesGui.Services.Interfaces;
using Serilog;

namespace DesktopFilesGui.Services;

public class DesktopFileSerializer(ILogger logger) : IDesktopFileSerializer
{
    public string Serialize(DesktopFile desktopFile)
    {
        var fileContentBuilder = new StringBuilder();
        
        logger.Debug($"Generating desktop file from: {desktopFile}...");

        var properties = GetDesktopProperties(desktopFile);
        
        var result = fileContentBuilder.ToString();
        logger.Information("Generated .desktop file \n {code}", result);
        return result;
    }

    private IEnumerable<KeyValuePair<string, string>> GetDesktopProperties(DesktopFile desktopFile)
    {
        var properties = desktopFile.GetType()
            .GetProperties()
            .Select(prop => new {
                ClrProperty = prop,
                DesktopFileAttribute = prop.GetCustomAttribute<DesktopFilePropertyAttribute>()
            })
            .Where(prop => prop.DesktopFileAttribute is  not null)
            .Where(prop => 
                prop.DesktopFileAttribute!.TypeWhenAdd is null 
                || prop.DesktopFileAttribute!.TypeWhenAdd == desktopFile.Type)
            .Select(prop =>
            {
                var value = prop.ClrProperty.PropertyType == typeof(IEnumerable<string>)
                    ? (prop.ClrProperty
                        .GetValue(desktopFile) as IEnumerable<string> ?? [])
                        .ToDesktopFileArray()
                    : (prop.ClrProperty.ToString());
                
                var key = prop.DesktopFileAttribute!.Key;
                
                if(string.IsNullOrWhiteSpace(value))
                    logger.Error("This desktop file has invalidated property values at key {key} and may be corrupted", key);
                
                return KeyValuePair.Create(key, value ?? string.Empty);

            });
        
        return properties;

    }
}