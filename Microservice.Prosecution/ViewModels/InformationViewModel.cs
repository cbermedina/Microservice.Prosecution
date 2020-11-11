namespace Microservice.Prosecution.ViewModels
{
    using Microservice.Prosecution.Extensions;
    using Microservice.Prosecution.Resources;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    /// <summary>
    /// Participant information
    /// </summary>
    public class InformationViewModel
    {
        /// <summary>
        /// Information file
        /// </summary>
        [Display(ResourceType = typeof(WebUiResources), Name = nameof(WebUiResources.InformationFile))]
        [Required(ErrorMessageResourceType = typeof(WebUiResources), ErrorMessageResourceName = nameof(WebUiResources.Validations_Required))]
        [AllowedExtensions(new string[] { ".txt" })]
        public IFormFile File { get; set; }
    }
}
