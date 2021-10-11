using System;
using BepInEx;
using BepInEx.IL2CPP;
using UnhollowerRuntimeLib;
using HarmonyLib;
using UnityEngine;
using Flash2;
using Object = UnityEngine.Object;

namespace SkipBootLogos
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("smbbm.exe")]
    public class Plugin : BasePlugin
    {
	public static BepInEx.Logging.ManualLogSource log;
	public Plugin() {
		log = Log;
	}
	
        public override void Load()
        {
		Harmony.CreateAndPatchAll(typeof(BootMenuPatch));
        }
    }

    [HarmonyPatch(typeof(SelDisplayLogoSequence), "onStart")]
    class BootMenuPatch {
	static bool Prefix() {
		AppScene.Add(AppScene.eID.Title);
		AppScene.SetActive(AppScene.eID.Title);
		return false;
	}
    }
}
