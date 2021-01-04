using System;
using System.Collections.Generic;
using System.Text;
using VoiceOfKarabakh.Application.ViewModels.Donation_Shopping;

namespace VoiceOfKarabakh.Application.Interfaces.PaymentCart
{
    public interface IPaymentCartService
    {
        PaymentCartViewModel GetPaymentCartViewModel();
        void AddCartItem(CartItemViewModel cartItemViewModel);
        void CreateNewCart();
        void UpdateCart(PaymentCartViewModel paymentCartViewModel);
        void DeleteCartItem(int donationId);
        bool Exists();
    }
}
