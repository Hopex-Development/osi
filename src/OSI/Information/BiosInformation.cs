using System.Linq;
using System.Management;

namespace Hopex.OSI.Information
{
    /// <summary>
    /// Provides a structure for storing information about the installed BIOS.
    /// </summary>
    public class BiosInformation
    {
        /// <summary>
        /// Provides a structure for storing information about the installed BIOS.
        /// </summary>
        public BiosInformation()
        {
            new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS")
                .Get()
                .OfType<ManagementObject>()
                .ToList()
                .ForEach(bios =>
                {
                    BiosCharacteristics = (ushort[])bios["BiosCharacteristics"];
                    BIOSVersion = (string[])bios["BIOSVersion"];
                    BuildNumber = (string)bios["BuildNumber"];
                    Caption = (string)bios["Caption"];
                    CodeSet = (string)bios["CodeSet"];
                    CurrentLanguage = (string)bios["CodeSet"];
                    Description = (string)bios["CodeSet"];
                    EmbeddedControllerMajorVersion = (byte)bios["EmbeddedControllerMajorVersion"];
                    EmbeddedControllerMinorVersion = (byte)bios["EmbeddedControllerMinorVersion"];
                    IdentificationCode = (string)bios["IdentificationCode"];
                    InstallDate = bios["InstallDate"];
                    LanguageEdition = (string)bios["LanguageEdition"];
                    ListOfLanguages = (string[])bios["ListOfLanguages"];
                    Manufacturer = (string)bios["Manufacturer"];
                    Name = (string)bios["Name"];
                    OtherTargetOS = (string)bios["OtherTargetOS"];
                    PrimaryBIOS = (bool)bios["PrimaryBIOS"];
                    ReleaseDate = bios["ReleaseDate"];
                    SerialNumber = (string)bios["SerialNumber"];
                    SMBIOSBIOSVersion = (string)bios["SMBIOSBIOSVersion"];
                    SMBIOSMajorVersion = (ushort)bios["SMBIOSMajorVersion"];
                    SMBIOSMinorVersion = (ushort)bios["SMBIOSMinorVersion"];
                    SMBIOSPresent = (bool)bios["SMBIOSPresent"];
                    SoftwareElementID = (string)bios["SoftwareElementID"];
                    SoftwareElementState = (ushort)bios["SoftwareElementState"];
                    Status = (string)bios["Status"];
                    SystemBiosMajorVersion = (byte)bios["SystemBiosMajorVersion"];
                    SystemBiosMinorVersion = (byte)bios["SystemBiosMinorVersion"];
                    TargetOperatingSystem = (ushort)bios["TargetOperatingSystem"];
                    Version = (string)bios["Version"];
                });
        }


        /// <summary>
        /// Array of BIOS characteristics supported by the system as defined by the System
        /// Management BIOS Reference Specification.
        /// This value comes from the BIOS Characteristics member of the BIOS Information
        /// structure in the SMBIOS information.
        /// </summary>
        /// <remarks>
        /// <see href="https://learn.microsoft.com/en-us/windows/win32/cimwin32prov/win32-bios#properties">
        /// Read more
        /// </see>
        /// </remarks>
        public ushort[] BiosCharacteristics { get; private set; }

        /// <summary>
        /// Array of the complete system BIOS information.
        /// In many computers there can be several version strings that are stored in the
        /// registry and represent the system BIOS information
        /// </summary>
        public string[] BIOSVersion { get; private set; }

        /// <summary>
        /// Internal identifier for this compilation of this software element.
        /// </summary>
        public string BuildNumber { get; private set; }

        /// <summary>
        /// Short description of the object a one-line string.
        /// </summary>
        public string Caption { get; private set; }

        /// <summary>
        /// Code set used by this software element.
        /// </summary>
        public string CodeSet { get; private set; }

        /// <summary>
        /// Name of the current BIOS language.
        /// </summary>
        public string CurrentLanguage { get; private set; }

        /// <summary>
        /// Description of the object.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The major release of the embedded controller firmware.
        /// </summary>
        /// <remarks>
        /// This value comes from the Embedded Controller Firmware Major Release member
        /// of the BIOS Information structure in the SMBIOS information.
        /// Windows Server 2012 R2, Windows 8.1, Windows Server 2012, Windows 8,
        /// Windows Server 2008 R2, Windows 7, Windows Server 2008 and Windows
        /// Vista: This property is not supported before Windows 10 and Windows Server 2016.
        /// </remarks>
        public byte EmbeddedControllerMajorVersion { get; private set; }

        /// <summary>
        /// The minor release of the embedded controller firmware.
        /// </summary>
        /// <remarks>
        /// This value comes from the Embedded Controller Firmware Minor Release member
        /// of the BIOS Information structure in the SMBIOS information.
        /// Windows Server 2012 R2, Windows 8.1, Windows Server 2012, Windows 8,
        /// Windows Server 2008 R2, Windows 7, Windows Server 2008 and Windows
        /// Vista: This property is not supported before Windows 10 and Windows Server 2016.
        /// </remarks>
        public byte EmbeddedControllerMinorVersion { get; private set; }

        /// <summary>
        /// Manufacturer's identifier for this software element. Often this will be a stock
        /// keeping unit (SKU) or a part number.
        /// </summary>
        public string IdentificationCode { get; private set; }

        /// <summary>
        /// Number of languages available for installation on this system. Language may determine
        /// properties such as the need for Unicode and bidirectional text.
        /// </summary>
        public ushort InstallableLanguages { get; private set; }

        /// <summary>
        /// Date and time the object was installed. This property does not need a value to
        /// indicate that the object is installed.
        /// </summary>
        public object InstallDate { get; private set; }

        /// <summary>
        /// Language edition of this software element. The language codes defined in
        /// ISO 639 should be used. Where the software element represents a multilingual
        /// or international version of a product, the string "multilingual" should be used.
        /// </summary>
        public string LanguageEdition { get; private set; }

        /// <summary>
        /// Array of names of available BIOS-installable languages.
        /// </summary>
        public string[] ListOfLanguages { get; private set; }

        /// <summary>
        /// Manufacturer of this software element.
        /// </summary>
        /// <remarks>
        /// This value comes from the Vendor member of the BIOS Information structure
        /// in the SMBIOS information.
        /// </remarks>
        public string Manufacturer { get; private set; }

        /// <summary>
        /// Name used to identify this software element.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Records the manufacturer and operating system type for a software element
        /// when the TargetOperatingSystem property has a value of 1 (Other).
        /// When TargetOperatingSystem has a value of 1, OtherTargetOS must have a nonnull value.
        /// For all other values of TargetOperatingSystem, OtherTargetOS is <see langword="null"/>.
        /// </summary>
        public string OtherTargetOS { get; private set; }

        /// <summary>
        /// If <see langword="true"/>, this is the primary BIOS of the computer system.
        /// </summary>
        public bool PrimaryBIOS { get; private set; }

        /// <summary>
        /// Release date of the Windows BIOS in the Coordinated Universal Time (UTC) format
        /// of YYYYMMDDHHMMSS.MMMMMM(+-)OOO.
        /// </summary>
        /// <remarks>
        /// This value comes from the BIOS Release Date member of the BIOS Information structure
        /// in the SMBIOS information.
        /// </remarks>
        public object ReleaseDate { get; private set; }

        /// <summary>
        /// Assigned serial number of the software element.
        /// </summary>
        public string SerialNumber { get; private set; }

        /// <summary>
        /// BIOS version as reported by SMBIOS.
        /// </summary>
        /// <remarks>
        /// This value comes from the BIOS Version member of the BIOS Information structure
        /// in the SMBIOS information.
        /// </remarks>
        public string SMBIOSBIOSVersion { get; private set; }

        /// <summary>
        /// Major SMBIOS version number. This property is NULL if SMBIOS is not found.
        /// </summary>
        public ushort SMBIOSMajorVersion { get; private set; }

        /// <summary>
        /// Minor SMBIOS version number. This property is NULL if SMBIOS is not found.
        /// </summary>
        public ushort SMBIOSMinorVersion { get; private set; }

        /// <summary>
        /// If <see langword="true"/>, the SMBIOS is available on this computer system.
        /// </summary>
        public bool SMBIOSPresent { get; private set; }

        /// <summary>
        /// Identifier for this software element; designed to be used in conjunction
        /// with other keys to create a unique representation of this instance.
        /// </summary>
        public string SoftwareElementID { get; private set; }

        /// <summary>
        /// State of a software element.
        /// </summary>
        public ushort SoftwareElementState { get; private set; }

        /// <summary>
        /// Current status of the object. Various operational and nonoperational statuses
        /// can be defined. Operational statuses include: "OK", "Degraded", and "Pred Fail"
        /// (an element, such as a SMART-enabled hard disk drive, may be functioning
        /// properly but predicting a failure in the near future). Nonoperational statuses
        /// include: "Error", "Starting", "Stopping", and "Service". The latter, "Service",
        /// could apply during mirror-resilvering of a disk, reload of a user permissions
        /// list, or other administrative work. Not all such work is online, yet the
        /// managed element is neither "OK" nor in one of the other states.
        /// </summary>
        public string Status { get; private set; }

        /// <summary>
        /// The major release of the System BIOS.
        /// </summary>
        /// <remarks>
        /// This value comes from the System BIOS Major Release member of the BIOS Information structure
        /// in the SMBIOS information.
        /// Windows Server 2012 R2, Windows 8.1, Windows Server 2012, Windows 8, Windows Server 2008 R2,
        /// Windows 7, Windows Server 2008 and Windows Vista: This property is not supported before
        /// Windows 10 and Windows Server 2016.
        /// </remarks>
        public byte SystemBiosMajorVersion { get; private set; }

        /// <summary>
        /// The minor release of the System BIOS.
        /// </summary>
        /// <remarks>
        /// This value comes from the System BIOS Minor Release member of the BIOS Information structure
        /// in the SMBIOS information.
        /// Windows Server 2012 R2, Windows 8.1, Windows Server 2012, Windows 8, Windows Server 2008 R2,
        /// Windows 7, Windows Server 2008 and Windows Vista: This property is not supported before
        /// Windows 10 and Windows Server 2016.
        /// </remarks>
        public byte SystemBiosMinorVersion { get; private set; }

        /// <summary>
        /// Target operating system of the owning software element.
        /// The possible values are (1-61).
        /// <see href="https://learn.microsoft.com/en-us/windows/win32/cimwin32prov/win32-bios#properties">
        /// Read more
        /// </see>
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        ///     <item>
        ///         <term>0</term><description>Unknown</description>
        ///     </item>
        ///     <item>
        ///         <term>1</term><description>Other</description>
        ///     </item>
        ///     <item>
        ///         <term>2</term><description>MACOS</description>
        ///     </item>
        ///     <item>
        ///         <term>3</term><description>ATTUNIX</description>
        ///     </item>
        ///     <item>
        ///         <term>4</term><description>DGUX</description>
        ///     </item>
        ///     <item>
        ///         <term>5</term><description>DECNT</description>
        ///     </item>
        ///     <item>
        ///         <term>6</term><description>Digital Unix</description>
        ///     </item>
        ///     <item>
        ///         <term>7</term><description>OpenVMS</description>
        ///     </item>
        ///     <item>
        ///         <term>8</term><description>HPUX</description>
        ///     </item>
        ///     <item>
        ///         <term>9</term><description>AIX</description>
        ///     </item>
        ///     <item>
        ///         <term>10</term><description>MVS</description>
        ///     </item>
        ///     <item>
        ///         <term>11</term><description>OS400</description>
        ///     </item>
        ///     <item>
        ///         <term>12</term><description>OS/2</description>
        ///     </item>
        ///     <item>
        ///         <term>13</term><description>JavaVM</description>
        ///     </item>
        ///     <item>
        ///         <term>14</term><description>MSDOS</description>
        ///     </item>
        ///     <item>
        ///         <term>15</term><description>WIN3x</description>
        ///     </item>
        ///     <item>
        ///         <term>16</term><description>WIN95</description>
        ///     </item>
        ///     <item>
        ///         <term>17</term><description>WIN98</description>
        ///     </item>
        ///     <item>
        ///         <term>18</term><description>WINNT</description>
        ///     </item>
        ///     <item>
        ///         <term>19</term><description>WINCE</description>
        ///     </item>
        ///     <item>
        ///         <term>20</term><description>NCR3000</description>
        ///     </item>
        ///     <item>
        ///         <term>21</term><description>NetWare</description>
        ///     </item>
        ///     <item>
        ///         <term>22</term><description>OSF</description>
        ///     </item>
        ///     <item>
        ///         <term>23</term><description>DC/OS</description>
        ///     </item>
        ///     <item>
        ///         <term>24</term><description>Reliant UNIX</description>
        ///     </item>
        ///     <item>
        ///         <term>25</term><description>SCO UnixWare</description>
        ///     </item>
        ///     <item>
        ///         <term>26</term><description>SCO OpenServer</description>
        ///     </item>
        ///     <item>
        ///         <term>27</term><description>Sequent</description>
        ///     </item>
        ///     <item>
        ///         <term>28</term><description>IRIX</description>
        ///     </item>
        ///     <item>
        ///         <term>29</term><description>Solaris</description>
        ///     </item>
        ///     <item>
        ///         <term>30</term><description>SunOS</description>
        ///     </item>
        ///     <item>
        ///         <term>31</term><description>U6000</description>
        ///     </item>
        ///     <item>
        ///         <term>32</term><description>ASERIES</description>
        ///     </item>
        ///     <item>
        ///         <term>33</term><description>TandemNSK</description>
        ///     </item>
        ///     <item>
        ///         <term>34</term><description>TandemNT</description>
        ///     </item>
        ///     <item>
        ///         <term>35</term><description>BS2000</description>
        ///     </item>
        ///     <item>
        ///         <term>36</term><description>LINUX</description>
        ///     </item>
        ///     <item>
        ///         <term>37</term><description>Lynx</description>
        ///     </item>
        ///     <item>
        ///         <term>38</term><description>XENIX</description>
        ///     </item>
        ///     <item>
        ///         <term>39</term><description>VM/ESA</description>
        ///     </item>
        ///     <item>
        ///         <term>40</term><description>Interactive UNIX</description>
        ///     </item>
        ///     <item>
        ///         <term>41</term><description>BSDUNIX</description>
        ///     </item>
        ///     <item>
        ///         <term>42</term><description>FreeBSD</description>
        ///     </item>
        ///     <item>
        ///         <term>43</term><description>NetBSD</description>
        ///     </item>
        ///     <item>
        ///         <term>44</term><description>GNU Hurd</description>
        ///     </item>
        ///     <item>
        ///         <term>45</term><description>OS9</description>
        ///     </item>
        ///     <item>
        ///         <term>46</term><description>MACH Kernel</description>
        ///     </item>
        ///     <item>
        ///         <term>47</term><description>Inferno</description>
        ///     </item>
        ///     <item>
        ///         <term>48</term><description>QNX</description>
        ///     </item>
        ///     <item>
        ///         <term>49</term><description>EPOC</description>
        ///     </item>
        ///     <item>
        ///         <term>50</term><description>IxWorks</description>
        ///     </item>
        ///     <item>
        ///         <term>51</term><description>VxWorks</description>
        ///     </item>
        ///     <item>
        ///         <term>52</term><description>MiNT</description>
        ///     </item>
        ///     <item>
        ///         <term>53</term><description>BeOS</description>
        ///     </item>
        ///     <item>
        ///         <term>54</term><description>HP MPE</description>
        ///     </item>
        ///     <item>
        ///         <term>55</term><description>NextStep</description>
        ///     </item>
        ///     <item>
        ///         <term>56</term><description>PalmPilot</description>
        ///     </item>
        ///     <item>
        ///         <term>57</term><description>Rhapsody</description>
        ///     </item>
        ///     <item>
        ///         <term>58</term><description>Windows 2000</description>
        ///     </item>
        ///     <item>
        ///         <term>59</term><description>Dedicated</description>
        ///     </item>
        ///     <item>
        ///         <term>60</term><description>VSE</description>
        ///     </item>
        ///     <item>
        ///         <term>61</term><description>TPF</description>
        ///    </item>
        /// </list>
        /// </remarks>
        public ushort TargetOperatingSystem { get; private set; }

        /// <summary>
        /// Version of the BIOS. This string is created by the BIOS manufacturer.
        /// </summary>
        public string Version { get; private set; }
    }
}
