using HarmonyLib;
using UnityEngine;

namespace AnnounceBuilder.Patch;


[HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
public static class MainMenuManagerStartPatch
{
	public static PassiveButton? Button { get; private set; }
	public static void Prefix(MainMenuManager __instance)
	{
		if (Button != null)
		{
			return;
		}
		var button = Object.Instantiate(__instance.playButton);
		button.OnClick.RemoveAllListeners();
		button.gameObject.SetActive(false);
		Button = button;
	}
}
