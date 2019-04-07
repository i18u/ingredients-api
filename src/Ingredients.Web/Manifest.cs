using System;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Ingredients.Web
{
    [DataContract]
    public class Manifest
    {
        public static Manifest Default = new Manifest 
        {
            Name = "Ingredients",
            Description = "Ingredients",
            Version = new Version(0, 0)
        };

        protected static Lazy<Manifest> _instance = new Lazy<Manifest>(Load);

        /// <summary>
        /// The <see cref="Manifest"/> value.
        /// </summary>
        /// <value><see cref="Manifest"/> instance.</value>
        public static Manifest Instance
        {
            get
            {
                return _instance.Value;
            }
        }


        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [IgnoreDataMember]
        public Version Version { get; set; }

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

                if (System.Version.TryParse(value, out var val))
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
            Manifest apiManifest = null;
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
                apiManifest = Manifest.Default;
            }

            if (apiManifest == null)
            {
                // Handle this -- it's not the end of the world if this is inaccurate.
                return Manifest.Default;
            }

            return apiManifest;
        }
    }
}
