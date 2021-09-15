using System;

namespace WebSpa.Models
{
    public record ImageContentResponse
    {

        /// <summary>
        /// Image title
        /// </summary>
        public string title { get; init; } = string.Empty;

        /// <summary>
        /// Image Name
        /// </summary>
        public string imageName { get; init; } = string.Empty;

        /// <summary>
        /// Image Path
        /// </summary>
        public string imagePath { get; init; } = string.Empty;

        /// <summary>
        /// Image Date
        /// </summary>
        public DateTimeOffset date { get; init; }
    }
}