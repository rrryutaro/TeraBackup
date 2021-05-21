using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;


namespace TeraBackup
{
    class TeraBackup : Mod
    {
        internal static TeraBackup instance;
        internal static string BackupPath = Path.Combine(Main.SavePath, "Backup");
        internal static string OldConfigFilePath = Path.Combine(Main.SavePath, "Mod Configs", "TeraBackup.json");


        public TeraBackup()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }

        public override void Load()
        {
            instance = this;

            if (!Main.dedServ)
            {
                try
                {
                    // 旧設定ファイルの削除
                    var oldConfigPath = Path.Combine(Main.SavePath, "Mod Configs", "TeraBackup.json"); ;
                    if (File.Exists(oldConfigPath))
                    {
                        File.Delete(oldConfigPath);
                    }

                    if (ModContent.GetInstance<TeraBackupConfig>().isBackup)
                    {
                        BackupAll();
                    }
                }
                catch (Exception e)
                {
                    var configPath = Path.Combine(Main.SavePath, "Mod Configs", "TeraBackup_TeraBackupConfig.json");
                    if (File.Exists(configPath))
                    {
                        File.Delete(configPath);
                    }
                    throw e;
                }
            }
        }

        private string GetFormatedDateString()
        {
            var result = DateTime.Now.ToString(ModContent.GetInstance<TeraBackupConfig>().dateFormat);
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                result = result.Replace(c.ToString(), "");
            }
            return result;
        }

        private void BackupAll()
        {
            try
            {
                TeraBackupConfig config = ModContent.GetInstance<TeraBackupConfig>();

                string path = Path.Combine(BackupPath, GetFormatedDateString());

                if (config.isLogs)
                    CopyDirectory(Path.Combine(Main.SavePath, "Logs"), path);
                if (config.isModConfigs)
                    CopyDirectory(Path.Combine(Main.SavePath, "Mod Configs"), path);
                if (config.isModReader)
                    CopyDirectory(Path.Combine(Main.SavePath, "Mod Reader"), path);
                if (config.isModSources)
                    CopyDirectory(Path.Combine(Main.SavePath, "Mod Sources"), path);
                if (config.isMods)
                    CopyDirectory(Path.Combine(Main.SavePath, "Mods"), path);
                if (config.isPlayers)
                    CopyDirectory(Main.PlayerPath, path);
                if (config.isReferences)
                    CopyDirectory(Path.Combine(Main.SavePath, "references"), path);
                if (config.isWorlds)
                    CopyDirectory(Main.WorldPath, path);
                if (config.isModLoaderRootFiles)
                    CopyDirectory(Main.SavePath, path, false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void CopyDirectory(string copyFrom, string copyTo, bool isSubFolders = true)
        {
            string path = Path.Combine(copyTo, Path.GetFileName(copyFrom));
            Directory.CreateDirectory(path);

            foreach (var file in Directory.GetFiles(copyFrom))
            {
                File.Copy(file, Path.Combine(path, Path.GetFileName(file)), true);
            }

            //再帰
            if (isSubFolders)
            {
                foreach (var dir in Directory.GetDirectories(copyFrom))
                {
                    CopyDirectory(dir, path);
                }
            }
        }
    }
}
