namespace WebApi.Services.Hosted_Service
{
    public interface ISendingMonitorService
    {
        public void Send(string[] receivers, string postTitle, string category);
    }
}