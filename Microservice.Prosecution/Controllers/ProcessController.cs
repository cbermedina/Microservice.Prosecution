using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.Prosecution.Application.Contracts.IServices;
using Microservice.Prosecution.Mappers;
using Microservice.Prosecution.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Prosecution.Controllers
{
    public class ProcessController : ControllerBase
    {
        #region Properties
        private readonly IProcessInformationService _processInformationService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="processInformationService"></param>
        public ProcessController(IProcessInformationService processInformationService)
        {
            _processInformationService = processInformationService;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Process information
        /// </summary>
        /// <param name="informationViewModel"></param>
        /// <returns></returns>
        [HttpPost("DoProcess")]
        public async Task<bool> DoProcess([FromForm] InformationViewModel informationViewModel)
        {
            return await _processInformationService.DoProcess(informationViewModel.Map());
        }
        #endregion
    }
}
