using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnounceBuilder.API;

public interface ILocalAnnounceLoader
{
	public bool TryLoad(string path, [NotNullWhen(true)] out ILocalAnnounce? announce);
}
