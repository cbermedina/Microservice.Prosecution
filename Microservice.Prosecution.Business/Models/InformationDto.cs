namespace Microservice.Prosecution.Business.Models
{
    using Microsoft.AspNetCore.Http;

    public class InformationDto
    {
        /// <summary>
        /// Information file
        /// </summary>
        public IFormFile FileInfo { get; set; }
    }
}
