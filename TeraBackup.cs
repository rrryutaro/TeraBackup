using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using FKTModSettings;

namespace TeraBackup
{
	class TeraBackup : Mod
	{
		internal static TeraBackup instance;
		internal static string BackupPath = Path.Combine(Main.SavePath, "Backup");

		internal bool LoadedFKTModSettings = false;

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
				Config.LoadConfig();
				LoadedFKTModSettings = ModLoader.GetMod("FKTModSettings") != null;
				try
				{
					if (LoadedFKTModSettings)
					{
						LoadModSettings();
					}
					if (Config.isBackup)
					{
						BackupAll();
					}
				}
				catch { }
			}
		}

		public override void PreSaveAndQuit()
		{
			Config.SaveValues();
		}

		public override void PostUpdateInput()
		{
			try
			{
				if (LoadedFKTModSettings && !Main.gameMenu)
				{
					UpdateModSettings();
				}
			}
			catch { }
		}

		private void LoadModSettings()
		{
			ModSetting setting = ModSettingsAPI.CreateModSettingConfig(this);
			setting.AddComment($"TeraBackup v{TeraBackup.instance.Version}");
			setting.AddBool("isBackup", "Take a backup", false);
			setting.AddComment($"Date format: {Config.dateFormat}{Environment.NewLine}{GetFormatedDateString(true)}");
		}

		private void UpdateModSettings()
		{
			ModSetting setting;
			if (ModSettingsAPI.TryGetModSetting(this, out setting))
			{
				setting.Get("isBackup", ref Config.isBackup);
			}
		}

		private static string GetFormatedDateString(bool isError = false)
		{
			string result;
			try
			{
				result = DateTime.Now.ToString(Config.dateFormat);
				char[] invalidChars = Path.GetInvalidPathChars();
				if (0 <= result.IndexOfAny(invalidChars))
				{
					if (isError)
						result = $"Unusable characters are used:{Environment.NewLine}{string.Join(" ", invalidChars)}";
					else
						result = DateTime.Now.ToString(Config.defaultDateFormat);
				}
			}
			catch (Exception ex)
			{
				if (isError)
					result = ex.Message;
				else
					result = DateTime.Now.ToString(Config.defaultDateFormat);
			}
			return result;
		}

		private static void BackupAll()
		{
			try
			{
				string path = Path.Combine(BackupPath, GetFormatedDateString());
				CopyDirectory(Main.PlayerPath, path);
				CopyDirectory(Main.WorldPath, path);
				CopyDirectory(Path.Combine(Main.SavePath, "Logs"), path);
			}
			catch { }
		}

		private static void CopyDirectory(string copyFrom, string copyTo)
		{
			string path = Path.Combine(copyTo, Path.GetFileName(copyFrom));
			Directory.CreateDirectory(path);

			foreach (var file in Directory.GetFiles(copyFrom))
			{
				System.IO.File.Copy(file, Path.Combine(path, Path.GetFileName(file)), true);
			}

			//再帰
			foreach (var dir in Directory.GetDirectories(copyFrom))
			{
				CopyDirectory(dir, path);
			}
		}
	}
}
