using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceOfKarabakh.Application.ViewModels.Donation_Shopping
{
    public class CartItemViewModel
    {
        public int Id { get; set; }
        public int DonationId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
