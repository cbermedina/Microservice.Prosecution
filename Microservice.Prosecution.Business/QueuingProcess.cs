namespace Microservice.Prosecution.Business
{
    using Hangfire;
    using Hangfire.Server;
    using Microservice.Prosecution.Common.Constants;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    public class QueuingProcess
    {

        #region Queue transactions
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TypeFilter(typeof(AutomaticRetries))]
        [DisplayName("{0}"), Queue(Queues.QueueTransactions, Order = QueueOrder.One), AutomaticRetries()]
        public async Task ProcessPriorityOne(List<int> lstInformationFile, PerformContext context, int retryNumber)
        {
            try
            {
                ProcessTransaction(lstInformationFile, context.GetJobParameter<int>("RetryCount"), retryNumber);
            }
            catch (Exception Error)
            {
                throw Error;
            }
        }

        private void ProcessTransaction(List<int> lstInformationFile, int retryCount, int retryNumber)
        {
            if (retryNumber > retryCount)
            {
                throw new Exception("Hola");
                int resultSum = lstInformationFile.Sum();
                Thread.Sleep(100);
            }
        }
        #endregion
    }
}
