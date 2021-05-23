using Microsoft.Extensions.Hosting;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Services.Hosted_Service.EmailSenderHostedService;

namespace WebApi.Services.Hosted_Service
{

    public class EmailSender: BackgroundService
    {
        public IBackgroundTaskQueueService<(List<ReceiverDTO>, string, string)> TaskQueue { get; }

        private readonly string websiteURL = "SystemPromocjiGrupaI.com";
        private readonly string serviceEmail = "https://systempromocji.azurewebsites.net/";
        private readonly string emailPassword = "1715grupaI";
        private readonly string serviceName = "System Promocji Grupa I";

        private readonly string sendGridApiKey = "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";

        public EmailSender([NotNull] IBackgroundTaskQueueService<(List<ReceiverDTO>, string, string)> taskQueue)
        {
            TaskQueue = taskQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // in work item we want to have recipients data and information about a new post and its category
                (List<ReceiverDTO> recipients, string postTitle, string category) workItem =
                await TaskQueue.DequeueAsync(stoppingToken);

                SendEmailNotifications(workItem);
            }

        }

        private void SendEmailNotifications((List<ReceiverDTO> recipients, string postTitle, string category) workItem)
        {
            foreach (var recipient in workItem.recipients)
            {
                var client = new SendGridClient(sendGridApiKey);
                EmailAddress from = new EmailAddress(serviceEmail, serviceName);
                EmailAddress to = new EmailAddress(recipient.Email); 
                var subject = "Zobacz nową promocję!";
                var plaintextContent = BuildNotificationContent(recipient, workItem.postTitle, workItem.category);

                var htmlContent = $"{plaintextContent}";

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plaintextContent, htmlContent);
                client.SendEmailAsync(msg);
            }
        }

        private string BuildNotificationContent(ReceiverDTO recipient, string postTitle, string category)
        {

            StringBuilder builder = new StringBuilder();
            builder.Append($"Hi {recipient.Name}!,");
            builder.Append("<br>");
            builder.Append($"Check out a new post: {postTitle} in {category} category!");
            builder.Append("<br>");
            builder.Append($"Don't forget to visit our website: { websiteURL }");
            builder.Append("<br>");

            return builder.ToString();

        }
    }
}
