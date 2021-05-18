using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Services.Hosted_Service.EmailSenderHostedService;

namespace WebApi.Services.Hosted_Service
{
    public class BackgroundTaskQueueService<T> : IBackgroundTaskQueueService<T>
    {
        private ConcurrentQueue<T> workItems =
            new ConcurrentQueue<T>();
        private SemaphoreSlim signal = new SemaphoreSlim(0);

        public void QueueBackgroundWorkItem(T workItem)
        {
            workItems.Enqueue(workItem);
            signal.Release();
        }

        public async Task<T> DequeueAsync(
            CancellationToken cancellationToken)
        {
            await signal.WaitAsync(cancellationToken);
            workItems.TryDequeue(out var workItem);

            return workItem;
        }
    }
}
