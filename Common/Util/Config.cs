using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Market.Util
{
    class Config   // revision 11
    {
        const string INI_FILE_PATH = "Common/Config/settings.ini";
        string Path;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public Config(string IniPath = INI_FILE_PATH)
        {
            Path = System.IO.Path.GetFullPath(IniPath);
            if (!File.Exists(Path)) {
                throw new Exception("設定ファイルが見つからない");
                
            }
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }
    }
}
