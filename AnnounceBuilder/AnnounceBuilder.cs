using AnnounceBuilder.API;
using Assets.InnerNet;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UnityEngine;


namespace AnnounceBuilder;

public sealed class AnnounceBuilder : IAnnounceBuilder
{
	private readonly ILocalAnnounceLoader _loader;

	public AnnounceBuilder(ILocalAnnounceLoader loader)
	{
		this._loader = loader;
		if (TryCreateFolder(out string path))
		{
			BuildSampleAnnounce(path);
		}
	}

	public IEnumerable<Announcement> Load()
	{
		string? ausFolder = Path.GetDirectoryName(Application.dataPath);
		if (string.IsNullOrEmpty(ausFolder))
		{
			yield break;
		}

		string path = Path.Combine(ausFolder, "AnnounceBuilder");

		foreach (string file in Directory.EnumerateFiles(path))
		{
			if (!(File.Exists(file) && file.EndsWith(".json")))
			{
				continue;
			}
			if (!_loader.TryLoad(file, out var announce))
			{
				continue;
			}
			yield return announce.Convert();
		}
	}

	private static void BuildSampleAnnounce(string folderPath)
	{
		string path = Path.Combine(folderPath, "sample.json");
		var sample = new LocalAnnounce();
		sample.Text = "BodyText";
		sample.Title = "Sample Announce";
		sample.SubTitle = "Sub Title";
		sample.ShortTitle = "Sample";
		string jsonStr = JsonSerializer.Serialize(sample, new JsonSerializerOptions()
		{
			WriteIndented = true,
		});
		File.WriteAllText(path, jsonStr);
	}

	private static bool TryCreateFolder(out string path)
	{
		string? ausFolder = Path.GetDirectoryName(Application.dataPath);
		if (string.IsNullOrEmpty(ausFolder))
		{
			path = string.Empty;
			return false;
		}

		path = Path.Combine(ausFolder, "AnnounceBuilder");
		if (Directory.Exists(path))
		{
			return false;
		}
		Directory.CreateDirectory(path);
		return true;
	}
}
