using Hangfire;
using Hangfire.Server;
using Microservice.Prosecution.Application.Contracts.IServices;
using Microservice.Prosecution.Business;
using Microservice.Prosecution.Business.Models;
using Microservice.Prosecution.Common.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microservice.Prosecution.Application.Services
{
    public class ProcessInformationService : IProcessInformationService
    {
        private readonly QueuingProcess _queuingProcess;
        private readonly IAppConfigService _appConfigService;
        public ProcessInformationService(QueuingProcess queuingProcess, IAppConfigService appConfigService)
        {
            _queuingProcess = queuingProcess;
            _appConfigService = appConfigService;
        }
        /// <summary>
        /// Process information
        /// </summary>
        /// <param name="participantInformationDto"></param>
        /// <returns></returns>
        public async Task<bool> DoProcess(InformationDto informationDto)
        {
            List<int> lstInformationFile = new List<int>();
            using StreamReader reader = new StreamReader(informationDto.FileInfo.OpenReadStream());
            string fileLine = string.Empty;
            while (reader.Peek() >= 0)
            {
                fileLine = await reader.ReadLineAsync();
                if (!string.IsNullOrEmpty(fileLine) && Validations.IsNaturalNumber(fileLine))
                {
                    lstInformationFile.Add(Convert.ToInt32(fileLine));
                }
                fileLine = string.Empty;
            }
            foreach (var item in lstInformationFile)
            {
                BackgroundJob.Enqueue(() => _queuingProcess.ProcessPriorityOne(lstInformationFile,null, _appConfigService.GetProcessRetriesNumber));
            }
            return true;
        }
    }
}
