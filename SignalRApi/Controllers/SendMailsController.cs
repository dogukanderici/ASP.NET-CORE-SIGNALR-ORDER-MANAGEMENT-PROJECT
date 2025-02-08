using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using SignalR.Dtos.Dtos.MailDtos;
using SignalRApi.Settings;

namespace SignalRApi.Controllers
{
    [Authorize(Policy = "AdminPermissionPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailsController : BaseController
    {
        private readonly SendMailSettings _mailSettings;

        public SendMailsController(IOptions<SendMailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        [HttpPost]
        public IActionResult SendMail(CreateMailDto createMailDto)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("SignalR Rezervasyon", "");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", createMailDto.ReceiverMail);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = createMailDto.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = createMailDto.Subject;

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate(_mailSettings.SenderMailAddress, _mailSettings.MailApplicationPassword);
            client.Send(mimeMessage);

            client.Disconnect(true);

            return Ok("Mail Gönderimi Başarılı");
        }
    }
}
