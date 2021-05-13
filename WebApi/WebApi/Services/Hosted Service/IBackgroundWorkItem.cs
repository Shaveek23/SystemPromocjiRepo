using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Services.Hosted_Service.EmailSenderHostedService
{
    public interface IBackgroundTaskQueueService<T>
    {
        void QueueBackgroundWorkItem(T workItem);
        Task<T> DequeueAsync(
            CancellationToken cancellationToken);
    }
}
