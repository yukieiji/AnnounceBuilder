using AnnounceBuilder.API;
using Assets.InnerNet;

namespace AnnounceBuilder;

public sealed class LocalAnnounce : ILocalAnnounce
{
	public string Title { get; set; } = string.Empty;

	public string Text { get; set; } = string.Empty;

	public string SubTitle { get; set; } = string.Empty;

	public string ShortTitle { get; set; } = string.Empty;

	public Announcement Convert()
	{
		var a = new Announcement();
		a.Title = Title;
		a.Text = Text;
		a.SubTitle = SubTitle;
		a.ShortTitle = ShortTitle;
		return a;
	}
}
