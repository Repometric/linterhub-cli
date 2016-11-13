namespace Linterhub.Engine.Linters.htmlhint
{
    using Newtonsoft.Json;

    public class Error
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Changed code
        /// </summary>
        [JsonProperty("evidence")]
        public string Evidence { get; set; }

        /// <summary>
        /// Type of error (error, warning, ...)
        /// </summary>
        [JsonProperty("type")]
        public string Severity { get; set; }

        /// <summary>
        /// Wrong code
        /// </summary>
        [JsonProperty("raw")]
        public string Raw { get; set; }

        [JsonProperty("column")]
        public int Column { get; set; }

        [JsonProperty("line")]
        public int Line { get; set; }

        [JsonProperty("rule")]
        public LRule Rule { get; set; } 
    }
}
