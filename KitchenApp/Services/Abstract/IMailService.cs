using Microsoft.AspNetCore.Mvc;

namespace KitchenApp.Services.Abstract
{
    public interface IMailService
    {
        void SetMailView(PartialViewResult partialViewResult);
        void SendEmail(string toMail, string subject, string content, bool isHtml = false);
    }
}
