using Assets.InnerNet;

using System.Collections.Generic;

namespace AnnounceBuilder.API;

public interface IAnnounceBuilder
{
	public IEnumerable<Announcement> Load();
}
