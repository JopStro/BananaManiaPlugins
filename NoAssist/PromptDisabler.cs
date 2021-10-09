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

		public static GameObject obj = null;
	        
		internal static GameObject Create(string name)
       		{
           		obj = new GameObject(name);
            		DontDestroyOnLoad(obj);

            		var component = new PromptDisabler(obj.AddComponent(UnhollowerRuntimeLib.Il2CppType.Of<PromptDisabler>()).Pointer);

            		return obj;
        	}


		public PromptDisabler(IntPtr ptr) : base(ptr) {}
		
		public void Update() {
			var stage = Object.FindObjectOfType<MainGameStage>();
			if (stage == null || stage.m_IsAssistConfirmed) return;
			stage.m_IsAssistConfirmed = true;
		}
	}
}
