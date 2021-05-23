using System.Collections.Generic;

namespace WebApi.Services.Hosted_Service
{
    public interface ISendingMonitorService
    {
        public void Send(List<ReceiverDTO> receivers, string postTitle, string category);
    }
}