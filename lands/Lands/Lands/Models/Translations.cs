namespace Lands.Models
{
    using Newtonsoft.Json;

    public class Translations
    {
        [JsonProperty(PropertyName = "de")]
        public string Aleman { get; set; }

        [JsonProperty(PropertyName = "es")]
        public string Español { get; set; }

        [JsonProperty(PropertyName = "fr")]
        public string Frances { get; set; }

        [JsonProperty(PropertyName = "ja")]
        public string Japones { get; set; }

        [JsonProperty(PropertyName = "it")]
        public string Italiano { get; set; }

        [JsonProperty(PropertyName = "br")]
        public string Brasileño { get; set; }

        [JsonProperty(PropertyName = "pt")]
        public string Portugues { get; set; }

        [JsonProperty(PropertyName = "nl")]
        public string Olandes { get; set; }

        [JsonProperty(PropertyName = "hr")]
        public string Croata { get; set; }

        [JsonProperty(PropertyName = "fa")]
        public string Danes { get; set; }
    }
}
