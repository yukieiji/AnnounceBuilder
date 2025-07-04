using Assets.InnerNet;

namespace AnnounceBuilder.API;

public interface ILocalAnnounce
{
	public string Title { get; }
	public string Text { get; }
	public string SubTitle { get; }
	public string ShortTitle { get; }

	public Announcement Convert();
}
