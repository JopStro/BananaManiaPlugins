using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.IL2CPP;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib;
using HarmonyLib;
using Flash2;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NoAssist {
	public class BootStrapper : MonoBehaviour {
		
		private static GameObject promptDisabler = null;

        	internal static GameObject Create(string name)
        	{
           	 	var obj = new GameObject(name);
		 	DontDestroyOnLoad(obj);
		 	var component = new BootStrapper(obj.AddComponent(UnhollowerRuntimeLib.Il2CppType.Of<BootStrapper>()).Pointer);
		 	return obj;
        	}

		public BootStrapper(IntPtr ptr) : base(ptr) {}
		
		public void Awake() {}

		[HarmonyPostfix]
		public static void Update() {
			// Create Game Objects here (note: this will run every frame)
			if (promptDisabler == null) {
				try {
					promptDisabler = PromptDisabler.Create("PromptDisablerGO");
				}
				catch (Exception e) {
					Plugin.log.LogMessage("ERROR Bootstrapping PromptDisabler: " + e.Message);
				}
			}
		}
	}
}
