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
            // Plugin startup logic
            Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

	    try {
		    ClassInjector.RegisterTypeInIl2Cpp<BootStrapper>();
		    ClassInjector.RegisterTypeInIl2Cpp<PromptDisabler>();
	    }
	    catch
            {
                log.LogError("FAILED to Register Il2Cpp Type!");
            }
	    try {
		    var harmony = new Harmony("jostro.noassist.il2cpp");

		    //Update
		    var originalUpdate = AccessTools.Method(typeof(Sound), "Update");
		    log.LogMessage("Harmony - Original Method: " + originalUpdate.DeclaringType.Name + "." + originalUpdate.Name);
		    var postUpdate = AccessTools.Method(typeof(BootStrapper), "Update");
		    log.LogMessage("Harmony - Postfix Method: " + postUpdate.DeclaringType.Name + "." + postUpdate.Name);
		    harmony.Patch(originalUpdate, postfix: new HarmonyMethod(postUpdate));
		    log.LogMessage("Harmony - Runtime Patch's Applied");
	    }
	    catch {
		    log.LogError("Harmony - FAILED to Apply Patch's!");
	    }
	    BootStrapper.Create("BootStrapperGO");
        }
    }
}
