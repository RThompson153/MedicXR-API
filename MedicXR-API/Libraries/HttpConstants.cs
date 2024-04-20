namespace MedicXR_API.Libraries
{
	internal static class HttpConstants
	{
		internal const string JSON = "application/json";
		internal const string FormEncoded = "application/x-www-urlformencoded";
		internal const string Basic = "Basic";
		internal const string Bearer = "Bearer";
	}

	internal enum HttpContentTypes
	{
		JSON, FormEncoded
	}

	internal enum HttpAuthenticationTypes
	{
		Basic, Bearer
	}

	
}
