using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using VoiceOfKarabakh.Application.Interfaces.Email;
using VoiceOfKarabakh.Application.ViewModels.Email;

namespace VoiceOfKarabakh.UI.Mvc.ApiControllers.Email
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpsController : ControllerBase
    {
        private readonly IEmailSenderService _emailSenderService;

        public HelpsController(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        [HttpPost, Route("[action]")]
        public IActionResult Help([FromForm] HelpViewModel helpViewModel)
        {
            List<IFormFile> files = new List<IFormFile>();
            files.Add(helpViewModel.Cv);

            if (helpViewModel.Documents != null)
            {
                foreach (var document in helpViewModel.Documents)
                {
                    files.Add(document);
                }
            }

            string from = helpViewModel.Email;
            string message = helpViewModel.Message;

            try
            {
                _emailSenderService.Receive(from, "Kömək etmək istəyirəm", message, files);
            }
            catch (SmtpException ex)
            {
                Help(helpViewModel);
            }

            return Ok();
        }

        [HttpPost, Route("[action]")]
        public IActionResult GetHelp([FromForm]GetHelpViewModel getHelpViewModel)
        {
            int day = getHelpViewModel.BirthDate.Day;
            int month = getHelpViewModel.BirthDate.Month;
            int year = getHelpViewModel.BirthDate.Year;

            string message = 
                $@"
                Kurs: {getHelpViewModel.Course}
                
                                Haqqında
                Ad və Soyad: {getHelpViewModel.FullName}
                Status: {getHelpViewModel.Status}
                Doğum tarixi: İl - {year}, Ay - {month}, Gün - {day}
                Ünvan: {getHelpViewModel.Address}
                
                                 Əlaqə
                Telefon: {getHelpViewModel.PhoneNumber}
                Email: {getHelpViewModel.Email}
                                
                                 Qeyd
                Əlavə qeyd: {getHelpViewModel.Note}";

            string from = getHelpViewModel.Email;

            try
            {
                _emailSenderService.Receive(from, "Kömək almaq istəyirəm", message, getHelpViewModel.Documents);
            }
            catch (SmtpException ex)
            {
                GetHelp(getHelpViewModel);
            }

            return Ok();
        }
    }
}
