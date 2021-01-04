using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceOfKarabakh.Application.Options
{
    public class SmtpOption
    {
        public string Email { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string Password { get; set; }
    }
}
