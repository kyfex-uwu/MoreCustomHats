using System;
using System.Collections;
using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace MoreCustomHats
{
	[BepInPlugin("kyfexuwu.deadsimplehats", "Add your own custom hats", "1.0.0")]
	public class Plugin : BaseUnityPlugin
	{
		public static AssetBundle assetBundle;
		public static List<HatEntry> hats;

		private class Patcher
		{
			public static bool CreateHatOption(Customization customization, string name, Texture2D icon)
			{
				if (Array.Exists(customization.hats, hat => hat.name == name))
				{
					Debug.LogError($"[MonAmiral] [Kyfex-DSH] Trying to add {name} a second time.");
					return false;
				}

				CustomizationOption hatOption = ScriptableObject.CreateInstance<CustomizationOption>();
				hatOption.color = Color.white;
				hatOption.name = name;
				hatOption.texture = icon;
				hatOption.type = Customization.Type.Hat;
				hatOption.requiredAchievement = ACHIEVEMENTTYPE.NONE;
				customization.hats = customization.hats.AddToArray(hatOption);

				Debug.Log($"[MonAmiral] [Kyfex-DSH] {name} added.");

				return true;
			}

			[HarmonyPatch(typeof(PassportManager), "Awake")]
			[HarmonyPostfix]
			public static void PassportManagerAwakePostfix(PassportManager __instance)
			{
				Customization customization = __instance.GetComponent<Customization>();

				Debug.Log($"[MonAmiral] [Kyfex-DSH] Adding hat CustomizationOptions.");
				for (int i = 0; i < hats.Count; i++)
				{
					HatEntry hat = hats[i];
					CreateHatOption(customization, hat.Name, hat.Icon);
				}

				Debug.Log($"[MonAmiral] [Kyfex-DSH] Done.");
			}

			[HarmonyPatch(typeof(CharacterCustomization), "Awake")]
			[HarmonyPostfix]
			public static void CharacterCustomizationAwakePostfix(CharacterCustomization __instance)
			{
				Transform hatsContainer = __instance.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(1);

				Debug.Log($"[MonAmiral] [Kyfex-DSH] Instanciating hats as children of {hatsContainer}.");
				for (int i = 0; i < hats.Count; i++)
				{
					HatEntry hat = hats[i];

					GameObject hatInstance = GameObject.Instantiate(hat.Prefab, hatsContainer.position, hatsContainer.rotation, hatsContainer);

					Renderer renderer = hatInstance.GetComponentInChildren<Renderer>();
					renderer.gameObject.SetActive(false);

					renderer.material.shader = Shader.Find("W/Character");
					__instance.refs.playerHats = __instance.refs.playerHats.AddToArray(renderer);
				}
			}
		}

		public void Awake()
		{
			new Harmony("kyfexuwu.deadsimplehats").PatchAll(typeof(Patcher));
			this.StartCoroutine(LoadHatsFromDisk());
		}

		private static IEnumerator LoadHatsFromDisk()
		{
			Debug.Log($"[MonAmiral] [Kyfex-DSH] Loading hats from disk.");

			string directoryName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			string path = System.IO.Path.Combine(directoryName, "kyfexcustomhats");

			Debug.Log($"[MonAmiral] [Kyfex-DSH] Path to AssetBundle: " + path);

			AssetBundleCreateRequest createRequest = AssetBundle.LoadFromMemoryAsync(System.IO.File.ReadAllBytes(path));
			yield return createRequest;
			assetBundle = createRequest.assetBundle;

			Debug.Log($"[MonAmiral] [Kyfex-DSH] AssetBundle loaded.");

			hats = new List<HatEntry>();
			var hatNames = assetBundle.GetAllAssetNames().ToList();
			//filter and transform
   			foreach(var hatName in hatNames){
				hats.Add(LoadHat(hatName));
			}

			Debug.Log($"[MonAmiral] [Kyfex-DSH] Done!");
		}

		private static HatEntry LoadHat(string hatName)
		{
			Debug.Log($"[MonAmiral] [Kyfex-DSH] Loading hat '{hatName}'.");

			GameObject prefab = assetBundle.LoadAsset<GameObject>($"Assets/{hatName}.prefab");
			Texture2D icon = assetBundle.LoadAsset<Texture2D>($"Assets/{hatName}.png");

			Debug.Log($"[MonAmiral] [Kyfex-DSH] Loaded prefab {prefab} and texture {icon}");

			return new HatEntry(hatName, prefab, icon);
		}

		public struct HatEntry
		{
			public string Name;
			public GameObject Prefab;
			public Texture2D Icon;

			public HatEntry(string name, GameObject prefab, Texture2D icon)
			{
				this.Name = name;
				this.Prefab = prefab;
				this.Icon = icon;
			}
		}
	}
}
