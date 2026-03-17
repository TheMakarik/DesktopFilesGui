using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopFilesGui.Models;
using DesktopFilesGui.Models.Enums;
using DesktopFilesGui.Services.Interfaces;

namespace DesktopFilesGui.ViewModels;

public partial class MainWindowViewModel(IGithubSourceOpener githubSourceOpener) : ViewModelBase
{
    [ObservableProperty] private bool _isCodePopupVisible = false;
    [ObservableProperty] private string? _fileName;
    [ObservableProperty] private string? _applicationName;
    [ObservableProperty] private bool _enableLocalization;
    [ObservableProperty] private DesktopFileType _selectedFileType = Configuration.DEFAULT_DESKTOP_FILE_TYPE;
    [ObservableProperty] private string? _pathToFile;
    [ObservableProperty] private string? _pathToIcon;
    [ObservableProperty] private bool _showTerminal;           
    [ObservableProperty] private bool _requireSudo;            
    [ObservableProperty] private bool _displayInMenu = true;   
    [ObservableProperty] private bool _isHidden;             
    [ObservableProperty] private bool _runFromDBus;           
    [ObservableProperty] private bool _startupNotifySupport;   
    [ObservableProperty] private bool _customExecCommand;      
    [ObservableProperty] private ObservableCollection<string> _supportedMimeTypes = new();

    private bool _mustUpdateDesktopFile = true;
    private DesktopFile? _desktopFile;

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        _mustUpdateDesktopFile = true;
        base.OnPropertyChanged(e);
    }

    [RelayCommand]
    private void ChangeCodePopupVisibility()
    {
        if (!IsCodePopupVisible && _mustUpdateDesktopFile)
        {
            UpdateDesktopFile();
        }
        IsCodePopupVisible = !IsCodePopupVisible;
    }

    [RelayCommand]
    private void AddMimeType()
    {
        SupportedMimeTypes.Add("application/json");
    }

    [RelayCommand]
    private void OpenGithubSource()
    {
        Task.Run(githubSourceOpener.Open);
    }
    
    
    [RelayCommand]
    private void ClearMimeTypes()
    {
        Console.WriteLine(string.Join(",", SupportedMimeTypes));
        SupportedMimeTypes.Clear();
    }
    
    private void UpdateDesktopFile()
    {
        _mustUpdateDesktopFile = false;
        _desktopFile = new DesktopFile
        {
            Type = SelectedFileType,
            ShowTerminal = ShowTerminal,
            RequireSudo = RequireSudo,
            DisplayInMenu = DisplayInMenu,
            EnableLocalization = EnableLocalization,
            IsHidden = IsHidden,
            RunFromDBus = RunFromDBus,
            StartupNotifySupport = StartupNotifySupport,
            CustomExecCommand = CustomExecCommand
        };
    }
}