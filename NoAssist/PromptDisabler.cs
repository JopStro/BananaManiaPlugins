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
	public class PromptDisabler : MonoBehaviour {
		public PromptDisabler(IntPtr ptr) : base(ptr) {
            		Plugin.log.LogMessage("[NoAssist] Entered Constructor");
        	}
		
		[HarmonyPostfix]
		public static void Update() {
			var stage = Object.FindObjectOfType<MainGameStage>();
			if (stage == null || stage.m_IsAssistConfirmed) return;
			stage.m_IsAssistConfirmed = true;
		}
	}
}
