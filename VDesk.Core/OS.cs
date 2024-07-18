using Microsoft.Win32;

namespace VDesk.Core
{
    public static class Os
    {
        /// <summary>
        /// Return the OS Build number such as: 22621.2215
        /// </summary>
        /// <returns></returns>
        public static Version Build
        {
            get
            {
                Version v = Environment.OSVersion.Version;
                Version actual = new(v.Major, v.Minor, v.Build,
                    int.Parse(Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion")
                        .GetValue("UBR").ToString()));
                return actual;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly string VersionPrefix = "10.0.";
    }
}