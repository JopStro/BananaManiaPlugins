using System;
using BepInEx;
using BepInEx.IL2CPP;
using UnhollowerRuntimeLib;
using HarmonyLib;
using UnityEngine;
using Flash2;
using BepInEx.Configuration;
using Object = UnityEngine.Object;

namespace SkipBootLogos
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("smbbm.exe")]
    public class Plugin : BasePlugin
    {
	public static ConfigEntry<bool> configSkipTitle;
	public static BepInEx.Logging.ManualLogSource log;
	public Plugin() {
		log = Log;
	}
	
        public override void Load()
        {
		configSkipTitle = Config.Bind("General", "SkipTitle", false, "Skip the title screen as well");
		Harmony.CreateAndPatchAll(typeof(Patch));
        }
    }

    [HarmonyPatch(typeof(SelDisplayLogoSequence), "onStart")]
    class Patch {
	[HarmonyPrefix]
	static bool SkipLogos() {
		var newScene = Plugin.configSkipTitle.Value ? AppScene.eID.MainMenu : AppScene.eID.Title;
		AppScene.Change(newScene);
		return false;
	}
    }
}
