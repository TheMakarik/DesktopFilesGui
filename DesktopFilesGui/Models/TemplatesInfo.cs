using System.Collections.Generic;

namespace DesktopFilesGui.Models;

public class TemplatesInfo
{
    public required ICollection<ExecutionTemplates> ExecutionTemplatesCollection { get; set; }
}