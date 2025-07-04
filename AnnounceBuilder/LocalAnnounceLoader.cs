using AnnounceBuilder.API;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;

namespace AnnounceBuilder;

public sealed class LocalAnnounceLoader : ILocalAnnounceLoader
{
	public bool TryLoad(string path, [NotNullWhen(true)] out ILocalAnnounce? announce)
	{
		string text = File.ReadAllText(path);
		announce = JsonSerializer.Deserialize<LocalAnnounce>(text);
		return announce is not null;
	}
}
