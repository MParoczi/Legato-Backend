namespace Legato.Models
{
    /// <summary>
    ///     Contains properties for the HTTP responses
    /// </summary>
    public class Response
    {
        /// <summary>
        ///     Default constructor for the Response class
        /// </summary>
        public Response()
        {
        }

        /// <summary>
        ///     Constructor for the Response class
        /// </summary>
        /// <param name="payload">Payload of the response object</param>
        /// <param name="message">API response message to display notifications from the API</param>
        public Response(object payload, string message)
        {
            Payload = payload;
            Message = message;
        }

        /// <summary>
        ///     Payload of the response object
        /// </summary>
        public object Payload { get; }

        /// <summary>
        ///     API response message to display notifications from the API
        /// </summary>
        public string Message { get; }
    }
}