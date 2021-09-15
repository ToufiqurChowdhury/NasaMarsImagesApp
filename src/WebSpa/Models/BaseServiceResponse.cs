using Grpc.Core;

namespace WebSpa.Models
{
    public record BaseServiceResponse
    {
        /// <summary>
        /// Status Code for signals 
        /// </summary>
        public StatusCode statusCode { get; init; }

        /// <summary>
        /// Error Message optional
        /// </summary>
        public string? message { get; init; }
    }
}
