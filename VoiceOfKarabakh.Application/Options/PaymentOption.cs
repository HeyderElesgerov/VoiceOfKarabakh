using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceOfKarabakh.Application.Options
{
    public class PaymentOption
    {
        public string SecretKey { get; set; }
        public string SuccessPath { get; set; }
        public string CanclePath { get; set; }
    }
}
