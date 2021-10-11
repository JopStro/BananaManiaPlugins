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
			if (MainGame.mainGameStage == null || MainGame.mainGameStage.m_IsAssistConfirmed) return;
			MainGame.mainGameStage.m_IsAssistConfirmed = true;
		}
	}
}
