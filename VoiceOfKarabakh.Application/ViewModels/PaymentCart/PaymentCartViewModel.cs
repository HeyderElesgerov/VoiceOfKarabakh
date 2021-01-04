using System;
using System.Collections.Generic;

namespace VoiceOfKarabakh.Application.ViewModels.Donation_Shopping
{
    //it is model of basket which contains selected items. it is not purschase cart
    public class PaymentCartViewModel
    {
        public Guid SessionId { get; set; }
        public List<CartItemViewModel> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
