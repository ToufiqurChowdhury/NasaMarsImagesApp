namespace WebSpa.Models
{
    public record SaveImageContentResponse : BaseServiceResponse
    {
        /// <summary>
        /// Image Content Response for View Model
        /// </summary>
        public ImageContentResponse content { get; init; }

    }
}