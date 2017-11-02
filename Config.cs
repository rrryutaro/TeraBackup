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
				config.Get("isLogs", ref isLogs);
				config.Get("isModConfigs", ref isModConfigs);
				config.Get("isMods", ref isMods);
				config.Get("isPlayers", ref isPlayers);
				config.Get("isWorlds", ref isWorlds);
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
			config.Put("isLogs", isLogs);
			config.Put("isModConfigs", isModConfigs);
			config.Put("isMods", isMods);
			config.Put("isPlayers", isPlayers);
			config.Put("isWorlds", isWorlds);
			config.Put("dateFormat", dateFormat);
            config.Save();
        }

        public static bool isBackup = true;
		public static bool isLogs = true;
		public static bool isModConfigs = false;
		public static bool isMods = false;
		public static bool isPlayers = true;
		public static bool isWorlds = true;
		public static string defaultDateFormat = "yyyyMMdd_HHmm";
		public static string dateFormat = defaultDateFormat;
    }
}