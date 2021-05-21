using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace TeraBackup
{
    [Label("Config")]
    public class TeraBackupConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Take a backup")]
        [DefaultValue(false)]
        public bool isBackup;

        [Label("Backup: Logs folder")]
        [DefaultValue(true)]
        public bool isLogs;

        [Label("Backup: Mod Configs folder")]
        [DefaultValue(true)]
        public bool isModConfigs;

        [Label("Backup: Mod Reader folder")]
        [DefaultValue(false)]
        public bool isModReader;

        [Label("Backup: Mod Sources folder")]
        [DefaultValue(false)]
        public bool isModSources;

        [Label("Backup: Mods folder")]
        [DefaultValue(false)]
        public bool isMods;

        [Label("Backup: Players folder")]
        [DefaultValue(true)]
        public bool isPlayers;

        [Label("Backup: references folder")]
        [DefaultValue(false)]
        public bool isReferences;

        [Label("Backup: Worlds folder")]
        [DefaultValue(true)]
        public bool isWorlds;

        [Label("Backup: ModLoader root files")]
        [DefaultValue(false)]
        public bool isModLoaderRootFiles;

        [Label("Backup folder date time format")]
        [DefaultValue("yyyyMMdd_HHmm")]
        public string dateFormat = "yyyyMMdd_HHmm";
    }
}
