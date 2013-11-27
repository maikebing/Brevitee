using System;

namespace Brevitee.Net
{
	public enum PingResponseType
	{
		Ok = 0,
		CouldNotResolveHost,
		RequestTimedOut,
		ConnectionError,
		InternalError,
		Canceled
	}
}
