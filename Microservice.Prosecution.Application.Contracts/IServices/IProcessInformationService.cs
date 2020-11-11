namespace Microservice.Prosecution.Application.Contracts.IServices
{
    using Microservice.Prosecution.Business.Models;
    using System.Threading.Tasks;
    public interface IProcessInformationService
    {
        Task<bool> DoProcess(InformationDto informationDto);
    }
}
