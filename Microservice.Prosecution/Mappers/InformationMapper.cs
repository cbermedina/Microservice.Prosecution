namespace Microservice.Prosecution.Mappers
{
    using Microservice.Prosecution.Business.Models;
    using Microservice.Prosecution.ViewModels;
    public static class InformationMapper
    {
        public static InformationDto Map(this InformationViewModel dto)
        {
            return new InformationDto()
            {
                FileInfo = dto.File
            };
        }

        public static InformationViewModel Map(this InformationDto entity)
        {
            return new InformationViewModel()
            {
                File = entity.FileInfo
            };
        }
    }
}
