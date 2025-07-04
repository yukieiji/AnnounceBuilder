using AmongUs.Data;
using AnnounceBuilder.API;
using Assets.InnerNet;
using HarmonyLib;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace AnnounceBuilder.Patch;

[HarmonyPatch(typeof(AnnouncementPopUp), nameof(AnnouncementPopUp.OnEnable))]
public static class AnnouncementPopUpOnEnablePatch
{
	private static PassiveButton? reloadButton;
	public static void Postfix(AnnouncementPopUp __instance)
	{
		if (reloadButton != null)
		{
			reloadButton.gameObject.SetActive(true);
			return;
		}

		if (MainMenuManagerStartPatch.Button == null)
		{
			return;
		}

		reloadButton = UnityEngine.Object.Instantiate(
			MainMenuManagerStartPatch.Button, __instance.transform);
		reloadButton.buttonText.m_text = "Reload";
		reloadButton.OnClick.AddListener((UnityAction)(__instance.CreateAnnouncementList));
		reloadButton.gameObject.SetActive(true);
		reloadButton.transform.localPosition = new Vector3(-2.75f, 1.9f, 0);
		reloadButton.transform.localScale = new Vector3(0.7f, 0.7f, 1);
	}
}

[HarmonyPatch(typeof(AnnouncementPopUp), nameof(AnnouncementPopUp.CreateAnnouncementList))]
public static class AnnouncementPopUpCreateAnnouncementListPatch
{
	private static Announcement[]? _vanillaAnnounce;
	private static IAnnounceBuilder? _builder;
	public static void Prefix()
	{
		if (_builder is null)
		{
			_builder = AnnounceBuilderPlugin.Instance.Provider.GetService<IAnnounceBuilder>();
			if (_builder is null)
			{
				return;
			}
		}

		if (_vanillaAnnounce == null)
		{
			_vanillaAnnounce = DataManager.Player.Announcements.AllAnnouncements.ToArray();
		}

		if (_vanillaAnnounce.Length == 0)
		{
			return;
		}

		var cache = _vanillaAnnounce.OrderByDescending(x => x.Number).First();
		int number = cache.Number;

		uint lang = cache.Language;
		string date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
		var newAnnounce = _vanillaAnnounce.ToList();
		foreach (var a in _builder.Load())
		{
			number++;
			a.Id = "AnnounceBuilder";
			a.Number = number;
			a.Language = lang;
			a.Date = date;
			newAnnounce.Add(a);
		}
		DataManager.Player.Announcements.SetAnnouncements(newAnnounce.ToArray());
	}
}

