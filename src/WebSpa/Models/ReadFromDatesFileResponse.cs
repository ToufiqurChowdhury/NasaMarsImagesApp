using System.Collections.Generic;

namespace WebSpa.Models
{
    public record ReadFromDatesFileResponse : BaseServiceResponse
    {
        /// <summary>
        /// Datetime list for log entries
        /// </summary>
        public List<string> imageDates { get; init; }

    }
}