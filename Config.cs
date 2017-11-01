using Terraria;
using Terraria.IO;

namespace TeraBackup
{
    public static class Config
    {
        private static string ConfigPath = $@"{Main.SavePath}\Mod Configs\TeraBackup.json";
        private static Preferences config;
        private static int version = 1;
        public static void LoadConfig()
        {
            config = new Preferences(ConfigPath);

            if (config.Load())
            {
                config.Get("version", ref version);
                config.Get("isBackup", ref isBackup);
                config.Get("dateFormat", ref dateFormat);
            }
            else
            {
                SaveValues();
            }
        }

        internal static void SaveValues()
        {
            config.Put("version", version);
            config.Put("isBackup", isBackup);
            config.Put("dateFormat", dateFormat);
            config.Save();
        }

        public static bool isBackup = true;
		public static string defaultDateFormat = "yyyyMMdd_HHmm";
		public static string dateFormat = defaultDateFormat;
    }
}