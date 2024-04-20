﻿using Azure;
using Microsoft.AspNetCore.Authentication;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MedicXR_API.Libraries
{
	public class HttpLibrary
	{
		private HttpClient _httpClient;

		public HttpLibrary()
		{
			_httpClient = new HttpClient();
		}

		private void setHttpHeaders(Dictionary<string, string> headers)
		{
			if (headers.ContainsKey(HttpConstants.Bearer))
			{
				setHttpAuthenticationHeader(headers.FirstOrDefault(h => h.Key == HttpConstants.Bearer));

				headers.Remove(HttpConstants.Bearer);
			}

			if (headers.ContainsKey(HttpConstants.Basic))
			{
				setHttpAuthenticationHeader(headers.FirstOrDefault(h => h.Key == HttpConstants.Basic));

				headers.Remove(HttpConstants.Basic);
			}

			foreach (KeyValuePair<string, string> header in headers)
				_httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
		}

		private void setHttpAuthenticationHeader(KeyValuePair<string, string> header)
		{
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(header.Key, header.Value);
		}

		private void setHttpContent(string content, HttpContentTypes contentType = HttpContentTypes.JSON)
		{
			
		}

		private FormUrlEncodedContent setHttpContent(Dictionary<string, string> content, HttpContentTypes contentType = HttpContentTypes.FormEncoded)
		{
			return new FormUrlEncodedContent(content);
		}

		private void assertResponseException(HttpResponseMessage response)
		{
			if (!response.IsSuccessStatusCode)
				throw new Exception(response.Content.ReadAsStringAsync().Result);
		}

		public async Task<T> GetAsync<T>(string url, Dictionary<string, string>? headers = null) where T : class
		{
			try
			{
				if (headers?.Any() == true)
					setHttpHeaders(headers);

				HttpResponseMessage response = await _httpClient.GetAsync(url);

				assertResponseException(response);

				return JsonSerializer.Deserialize<T>(response.Content.ReadAsStringAsync().Result);
			}
			catch
			{
				throw;
			}
		}

		//public async Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string>? headers = null)
		//{ }

		public async Task<T> PostAsync<T>(string url, Dictionary<string, string> headers, Dictionary<string, string> content)
		{
			try
			{
				setHttpHeaders(headers);

				HttpResponseMessage response = await _httpClient.PostAsync(url, setHttpContent(content));

				assertResponseException(response);

				return JsonSerializer.Deserialize<T>(response.Content.ReadAsStringAsync().Result);
			}
			catch
			{
				throw;
			}
		}

		//public async Task<HttpResponseMessage> PostAsync(string url, Dictionary<string, string> headers)
		//{ }
	}
}