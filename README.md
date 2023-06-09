# Operation system information by Schizo

Provides the ability to quickly obtain detailed information about the Windows operating system, processor, installed SP, .NET Frameworks. It also allows you to get information about running system processes and manage them.

# Adding to the project

#### .NET CLI
```CLI
> dotnet add package Hopex.OSI --version 23.0.1
```

#### Package Manager
```CLI
PM> NuGet\Install-Package Hopex.OSI -Version 23.0.1
```

#### PackageReference
```XML
<PackageReference Include="Hopex.OSI" Version="23.0.1" />
```

#### Paket CLI
```CLI
> paket add Hopex.OSI --version 23.0.1
```

#### Script & Interactive
```CLI
> #r "nuget: Hopex.OSI, 23.0.1"
```

#### Cake
```
// Install Hopex.OSI as a Cake Addin
#addin nuget:?package=Hopex.OSI&version=23.0.1

// Install Hopex.OSI as a Cake Tool
#tool nuget:?package=Hopex.OSI&version=23.0.1
```

# Opportunities

### Hidden command line

| Option | Status |
| --- | ----------- |
| Executing any commands | :white_check_mark: |

### Process manager

| Option | Status |
| --- | ----------- |
| Search for a running process by its name | :white_check_mark: |
| Asynchronous verification of the existence of a process(s) by its name | :white_check_mark: |
| Getting a list of all processes | :white_check_mark: |
| Getting а process ID by its name | :white_check_mark: |
| Getting а process ID by its name | :white_check_mark: |
| Getting а process executable path by its name | :white_check_mark: |
| Getting а process executable path by its ID | :white_check_mark: |
| Closing a process by its name | :white_check_mark: |

### Opertion sysytem

| Option | Status |
| --- | ----------- |
| Getting user name | :white_check_mark: |
| Getting computer name | :white_check_mark: |
| Getting computer RAM size | :white_check_mark: |
| Getting user name | :white_check_mark: |
| Getting a number of processor cores | :white_check_mark: |
| Getting edition of the OS | :white_check_mark: |
| Getting name of the OS | :white_check_mark: |
| Getting edition of the OS | :white_check_mark: |
| Getting version OS | :white_check_mark: |
| Getting major version of the OS | :white_check_mark: |
| Getting minor version of the OS | :white_check_mark: |
| Getting build version of the OS | :white_check_mark: |
| Getting revision version of the OS | :white_check_mark: |
| Getting installed .NET Framework versions | :white_check_mark: |
| Getting service pack information if exists | :white_check_mark: |
| Getting the bitness of the OS (system, software, processor) | :white_check_mark: |
| Getting the name of the OS release | :white_check_mark: |
| Getting user displays information (name, is it the main one, resolution) | :white_check_mark: |
| Getting connected drives information | :white_check_mark: |

# How to use

### Hidden command line

```C#
public void NotepadLaunch()
{
    // Create a hidden command line instances and process managers instances
    HiddenCommandLine commandLine = new HiddenCommandLine();

    // Run the notepad application
    commandLine.Exec("notepad");
}
```

### Process manager

```C#
public void LainchEndCloseNotepad()
{
    /**
     * Let's launch notepad, check if it is running,
     * and close it using the hidden command line and process manager.
     */
     
    // Create a hidden command line instances and process managers instances
    HiddenCommandLine commandLine = new HiddenCommandLine();
    ProcessManager processManager = new ProcessManager();

    // The second parameter is set to false (by default true), so as not to wait for the process to complete.
    commandLine.Exec("notepad", false);
    
    // Let's wait one second until notepad is displayed (locking main ui thread is a bad practice).
    System.Threading.Thread.Sleep(1000);
    
    // Now, if notepad is running, we will close all its instances
    bool notepadIsRunning = await processManager.AsyncProcessExistsByName("notepad");
    if (notepadIsRunning)
        processManager.KillProcessByName("notepad");
    Console.WriteLine(notepadIsRunning ? "Exists" : "Not exists");
}
```

### Detailed information about the system

```C#
public void SystemInformation()
{
    OperationSystem OS = new OperationSystem();
    Console.WriteLine(string.Join(
        "\n",
        $"Computer name: {OS.ComputerName}",
        $"User name: {OS.UserName}",
        $"OS name: {OS.Name}",
        $"Core counts: {OS.CoreCounts}",
        $"OS version: {OS.VersionString}",
        $"Major version: {OS.MajorVersion}",
        $"Minor version: {OS.MinorVersion}",
        $"Build version: {OS.BuildVersion}",
        $"Revision version: {OS.RevisionVersion}",
        $"RAM size: {OS.RandomAccessMemorySize}",
        $"WindowsVersion: {OS.WindowsVersion}",
        $"WindowsBit: {OS.WindowsBit}",
        $"Bist: [OS: {OS.Bits.OperationSystemBits}], [Sofware: {OS.Bits.ProgramBits}], [Processor: {OS.Bits.ProcessorBits}]",
        $".NET Framework versions: {string.Join(", ", OS.DotNetFrameworkVersions.ToArray())}",
        $"Screens: {OS.Screens.Select(screen => $"{screen.Name}{(screen.IsPrimary? " (main)" : "") } {screen.Size.Width}x{screen.Size.Height}").First()}",
        $"Drives: {string.Join(", ", OS.Drives.Select((title, size) => $"{title}: {size}").ToArray())}"
    ));
    
    /**
     * Output for this:
     *
     * Computer name: HOPEX
     * User name: schizo
     * OS name: Windows 10
     * Core counts: 4
     * OS version: 10.0.22000.0
     * Major version: 10
     * Minor version: 0
     * Build version: 22000
     * Revision version: 0
     * RAM size: 8
     * WindowsVersion: Windows 10
     * WindowsBit: 64
     * Bist:[OS: Bit64], [Sofware: Bit32], [Processor: Bit64]
     * .NET Framework versions: 2.0.50727.4927 Service Pack 2, 3.0.30729.4926 Service Pack 2, 3.5.30729.4926 Service Pack 1, 4.0.0.0, 4.8.04161
     * Screens: DISPLAY1 (main) 1920x1200
     * Drives: [C, 181 / 240]: 0, [G, 69 / 1000]: 1, [J, 69 / 1000]: 2
     */
```

## License

MIT License
