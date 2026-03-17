using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using DesktopFilesGui.Models;
using DesktopFilesGui.Models.Enums;

namespace DesktopFilesGui.Converters;

public class EnableIfDesktopFileTypeIsNotParameterType : MarkupExtension, IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not DesktopFileType desktopFileType)
            return false;
        
        if (Enum.TryGetValue<DesktopFileType>(parameter as string, out var expectedDesktopFileType))
            return false;
        
        return desktopFileType != expectedDesktopFileType;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}