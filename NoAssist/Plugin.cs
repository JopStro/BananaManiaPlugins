using System;
using BepInEx;
using BepInEx.IL2CPP;
using UnhollowerRuntimeLib;
using HarmonyLib;
using UnityEngine;
using Flash2;
using Object = UnityEngine.Object;

namespace NoAssist
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
		Harmony.CreateAndPatchAll(typeof(Patch));
        }
    }
    [HarmonyPatch(typeof(MainGameStage), "Initialize")]
    class Patch {
	    static void Postfix(ref MainGameStage __instance) {
		    __instance.m_IsAssistConfirmed = true;
	    }
    }
}
