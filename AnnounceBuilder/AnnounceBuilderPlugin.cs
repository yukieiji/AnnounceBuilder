using System;
using AnnounceBuilder.API;
using BepInEx;
using BepInEx.Unity.IL2CPP;

using HarmonyLib;
using Microsoft.Extensions.DependencyInjection;

namespace AnnounceBuilder;

[BepInAutoPlugin("me.yukieiji.announcebuilder", "Announce Builder")]
[BepInProcess("Among Us.exe")]
public partial class AnnounceBuilderPlugin : BasePlugin
{
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。'required' 修飾子を追加するか、Null 許容として宣言することを検討してください。
	public static AnnounceBuilderPlugin Instance { get; private set; }
#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。'required' 修飾子を追加するか、Null 許容として宣言することを検討してください。
	public Harmony Harmony { get; } = new Harmony(Id);
	public IServiceProvider Provider { get; }

	public AnnounceBuilderPlugin()
	{
		Instance = this;
		var collection = new ServiceCollection();
		collection
			.AddTransient<IAnnounceBuilder, AnnounceBuilder>()
			.AddTransient<ILocalAnnounceLoader, LocalAnnounceLoader>();
		Provider = collection.BuildServiceProvider();
	}

	public override void Load()
	{
		this.Harmony.PatchAll();
	}
}
