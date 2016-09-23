namespace Metrics.Integrations.Linters.csslint
{
    using Newtonsoft.Json;

    public class Rule
    {
        /// <summary>
        ///  Rule class
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        ///  Rule name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///  Small description of problem in general
        /// </summary>
        [JsonProperty("desc")]
        public string Description { get; set; }

        /// <summary>
        ///  Url where u can find more about this
        /// </summary>
        [JsonProperty("url")]
        public string GithubUrl { get; set; }

        [JsonProperty("browsers")]
        /// <summary>
        ///  Choose whick rules to use (by browser)
        /// </summary>
        public string Browsers = null;
    }
}