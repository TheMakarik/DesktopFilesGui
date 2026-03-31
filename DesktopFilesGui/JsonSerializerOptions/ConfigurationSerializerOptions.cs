using System.Text.Json.Serialization;
using DesktopFilesGui.Models;

namespace DesktopFilesGui.JsonSerializerOptions;

[JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy =  JsonKnownNamingPolicy.KebabCaseLower)]
[JsonSerializable(typeof(TemplatesInfo))]
public sealed partial class ConfigurationSerializerOptions : JsonSerializerContext;