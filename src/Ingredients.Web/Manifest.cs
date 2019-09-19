using System;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Ingredients.Web
{
	/// <summary>
	/// Manifest for API information.
	/// </summary>
	[DataContract]
	public class Manifest
	{
		/// <summary>
		/// Default <see cref="Manifest"/> object.
		/// </summary>
		public static readonly Manifest Default = new Manifest
		{
			Name = "Ingredients",
			Description = "Ingredients",
			Version = new Version(0, 0)
		};

		/// <summary>
		/// Lazy singleton for <see cref="Manifest"/> instance.
		/// </summary>
		protected static readonly Lazy<Manifest> _instance = new Lazy<Manifest>(Load);

		/// <summary>
		/// The <see cref="Manifest"/> value.
		/// </summary>
		/// <value><see cref="Manifest"/> instance.</value>
		public static Manifest Instance => _instance.Value;

		/// <summary>
		/// Instance name.
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Instance description.
		/// </summary>
		[DataMember(Name = "description")]
		public string Description { get; set; }

		/// <summary>
		/// Instance version as <see cref="Version"/> object.
		/// </summary>
		[IgnoreDataMember]
		public Version Version { get; set; }

		/// <summary>
		/// Instance version as <see langword="string"/> object.
		/// </summary>
		[DataMember(Name = "version")]
		public string VersionString
		{
			get
			{
				return Version.ToString();
			}
			set
			{
				Version = null;

				if (Version.TryParse(value, out var val))
				{
					Version = val;
				}
			}
		}

		/// <summary>
		/// Loads the <see cref="Manifest"/> value.
		/// </summary>
		/// <returns>The loaded <see cref="Manifest"/> value.</returns>
		private static Manifest Load()
		{
			Manifest apiManifest;
			var apiManifestStr = string.Join(
				Environment.NewLine, // localized newline char
				File.ReadAllLines("./api-manifest.json") // content of api-manifest.json, as string[]
			);

			try
			{
				apiManifest = JsonConvert.DeserializeObject<Manifest>(apiManifestStr);
			}
			catch (JsonSerializationException)
			{
				apiManifest = Default;
			}

			if (apiManifest == null)
			{
				// Handle this -- it's not the end of the world if this is inaccurate.
				return Default;
			}

			return apiManifest;
		}
	}
}
