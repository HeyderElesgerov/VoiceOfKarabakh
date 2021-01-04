using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mail;
using VoiceOfKarabakh.Application.Interfaces.Email;
using VoiceOfKarabakh.Application.ViewModels.Email;

namespace VoiceOfKarabakh.UI.Mvc.ApiControllers.Email
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IEmailSenderService _emailSenderService;

        public ContactsController(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }
        [HttpPost, Route("[action]")]
        public IActionResult Send(ContactViewModel receiveEmailViewModel)
        {
            string from = receiveEmailViewModel.From;
            string topic = receiveEmailViewModel.Topic;

            string message = $"Author: {receiveEmailViewModel.FullName}";
            message += Environment.NewLine + receiveEmailViewModel.Message;

            try
            {
                _emailSenderService.Receive(from, topic, message);
            }
            catch (SmtpException ex)
            {
                Send(receiveEmailViewModel);
            }

            return Ok();
        }
    }
}
