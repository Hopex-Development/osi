# Operation system information by Schizo

Provides the ability to quickly obtain detailed information about the Windows operating system, processor, installed SP, .NET Frameworks and BIOS. Also allows you to get information about running system processes and manage them.

# Adding to the project

#### .NET CLI
```CLI
> dotnet add package Hopex.OSI --version 23.0.3
```

#### Package Manager
```CLI
PM> NuGet\Install-Package Hopex.OSI -Version 23.0.3
```

#### PackageReference
```XML
<PackageReference Include="Hopex.OSI" Version="23.0.3" />
```

#### Paket CLI
```CLI
> paket add Hopex.OSI --version 23.0.3
```

#### Script & Interactive
```CLI
> #r "nuget: Hopex.OSI, 23.0.3"
```

#### Cake
```
// Install Hopex.OSI as a Cake Addin
#addin nuget:?package=Hopex.OSI&version=23.0.3

// Install Hopex.OSI as a Cake Tool
#tool nuget:?package=Hopex.OSI&version=23.0.3
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

### BIOS
| Option | Status |
| --- | ----------- |
| Bios characteristics | :white_check_mark: |
| BIOS version | :white_check_mark: |
| Build number | :white_check_mark: |
| Caption | :white_check_mark: |
| CodeSet | :white_check_mark: |
| Current language | :white_check_mark: |
| Description | :white_check_mark: |
| Embedded controller major version | :white_check_mark: |
| Embedded controller minor version | :white_check_mark: |
| Identification code | :white_check_mark: |
| Installable languages | :white_check_mark: |
| Install date | :white_check_mark: |
| Language edition | :white_check_mark: |
| List of languages | :white_check_mark: |
| Manufacturer | :white_check_mark: |
| Name | :white_check_mark: |
| Other target OS | :white_check_mark: |
| Primary BIOS | :white_check_mark: |
| Release date | :white_check_mark: |
| Serial number | :white_check_mark: |
| SMBIOS BIOS version | :white_check_mark: |
| SMBIOS major version | :white_check_mark: |
| SMBIOS minor version | :white_check_mark: |
| SMBIOS present | :white_check_mark: |
| Software element ID | :white_check_mark: |
| Software element state | :white_check_mark: |
| Status | :white_check_mark: |
| System BIOS major version | :white_check_mark: |
| System BIOS minor version | :white_check_mark: |
| Target operating system | :white_check_mark: |
| Version | :white_check_mark: |

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
using Hopex.OSI.Information;
using Newtonsoft.Json;
using System;

public void SystemInformation()
{
    Console.WriteLine(JsonConvert.SerializeObject(new SystemInformation(), Formatting.Indented));
    Console.ReadKey();
    
    /**
     * Output for this:
     *
     * {
     *    "Screens": [
     *      {
     *          "Name": "DISPLAY1",
     *          "IsPrimary": true,
     *          "Size": "1920, 1200"
     *      },
     *      {
     *          "Name": "DISPLAY2",
     *          "IsPrimary": false,
     *          "Size": "1920, 1080"
     *      }
     *    ],
     *    "RandomAccessMemorySize": 8,
     *    "CoreCounts": "4",
     *    "UserName": "Schizo",
     *    "ComputerName": "DESKTOP-607U6SR",
     *    "Bits": {
     *          "ProgramBits": 1,
     *          "InformationBits": 2,
     *          "ProcessorBits": 2
     *    },
     *    "Edition": "Professional",
     *    "Name": "Windows 10",
     *    "ServicePack": "",
     *    "VersionString": "10.0.19045.0",
     *    "Version": {
     *          "Major": 10,
     *          "Minor": 0,
     *          "Build": 19045,
     *          "Revision": 0,
     *          "MajorRevision": 0,
     *          "MinorRevision": 0
     *    },
     *    "MajorVersion": 10,
     *    "MinorVersion": 0,
     *    "BuildVersion": 19045,
     *    "RevisionVersion": 0,
     *    "DotNetFrameworkVersions": [
     *          "2.0.50727.4927 Service Pack 2",
     *          "3.0.30729.4926 Service Pack 2",
     *          "3.5.30729.4926 Service Pack 1",
     *          "4.0.0.0",
     *          "4.8.04084"
     *    ],
     *    "WindowsBit": 64,
     *    "WindowsVersion": "Windows 10",
     *    "Drives": {
     *          "C": "218/239",
     *          "E": "232/1000",
     *          "J": "0/1000"
     *    }
     *  }
     */
```

### Detailed information about the BIOS

```C#
using Hopex.OSI.Information;
using Newtonsoft.Json;
using System;

public void BiosInformation()
{
    Console.WriteLine(JsonConvert.SerializeObject(new BiosInformation(), Formatting.Indented));
    Console.ReadKey();
    
    /**
     * Output for this:
     *
     * {
     *     "BiosCharacteristics": [
     *          7,
     *          11,
     *          12,
     *          15,
     *          16,
     *          17,
     *          19,
     *          23,
     *          24,
     *          25,
     *          26,
     *          27,
     *          28,
     *          29,
     *          32,
     *          33,
     *          40,
     *          42,
     *          43
     *     ],
     *     "BIOSVersion": [
     *          "ALASKA - 1072009",
     *          "V1.7",
     *          "American Megatrends - 4028D"
     *     ],
     *     "BuildNumber": null,
     *     "Caption": "V1.7",
     *     "CodeSet": null,
     *     "CurrentLanguage": null,
     *     "Description": null,
     *     "EmbeddedControllerMajorVersion": 255,
     *     "EmbeddedControllerMinorVersion": 255,
     *     "IdentificationCode": null,
     *     "InstallableLanguages": 0,
     *     "InstallDate": null,
     *     "LanguageEdition": null,
     *     "ListOfLanguages": [
     *          "en|US|iso8859-1"
     *     ],
     *     "Manufacturer": "American Megatrends Inc.",
     *     "Name": "V1.7",
     *     "OtherTargetOS": null,
     *     "PrimaryBIOS": true,
     *     "ReleaseDate": "20130930000000.000000+000",
     *     "SerialNumber": "To be filled by O.E.M.",
     *     "SMBIOSBIOSVersion": "V1.7",
     *     "SMBIOSMajorVersion": 2,
     *     "SMBIOSMinorVersion": 7,
     *     "SMBIOSPresent": true,
     *     "SoftwareElementID": "V1.7",
     *     "SoftwareElementState": 3,
     *     "Status": "OK",
     *     "SystemBiosMajorVersion": 4,
     *     "SystemBiosMinorVersion": 6,
     *     "TargetOperatingSystem": 0,
     *     "Version": "ALASKA - 1072009"
     * }
     */
```

## License

MIT License
