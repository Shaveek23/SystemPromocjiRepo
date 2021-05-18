using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Services.Hosted_Service.EmailSenderHostedService;

namespace WebApi.Services.Hosted_Service
{
    public class SendingMonitorService : ISendingMonitorService
    {
        public IBackgroundTaskQueueService<(string[], string, string)> TaskQueue { get; }
        public SendingMonitorService(IBackgroundTaskQueueService<(string[], string, string)> taskQueue)
        {
            TaskQueue = taskQueue;
        }

        public void Send(string[] receivers, string postTitle, string category)
        {
            TaskQueue.QueueBackgroundWorkItem((receivers, postTitle, category));
        }
    }
    
}
