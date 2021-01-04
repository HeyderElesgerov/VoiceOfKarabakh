using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace VoiceOfKarabakh.Application.Interfaces.Email
{
    public interface IEmailSenderService
    {
        //we send
        void Send(string to, string topic, string message);

        //we receive
        void Receive(string from, string topic, string message, IEnumerable<IFormFile> files = null);
    }
}
