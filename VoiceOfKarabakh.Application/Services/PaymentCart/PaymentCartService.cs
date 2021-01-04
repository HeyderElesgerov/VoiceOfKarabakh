using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using VoiceOfKarabakh.Application.Interfaces.PaymentCart;
using VoiceOfKarabakh.Application.Options;
using VoiceOfKarabakh.Application.Utility;
using VoiceOfKarabakh.Application.ViewModels.Donation_Shopping;

namespace VoiceOfKarabakh.Application.Services.PaymentCart
{
    public class PaymentCartService : IPaymentCartService
    {
        private readonly string paymentCartCookieKey;
        private readonly ISession _session;

        public PaymentCartService(IOptions<CookieKeyOption> cookieKeyOptions, IHttpContextAccessor httpContextAccessor)
        {
            paymentCartCookieKey = cookieKeyOptions.Value.PaymentCart;
            _session = httpContextAccessor.HttpContext.Session;
        }

        public void AddCartItem(CartItemViewModel cartItemViewModel)
        {
            var paymentCart = GetPaymentCartViewModel();
            paymentCart.CartItems.Add(cartItemViewModel);
            paymentCart.TotalPrice += cartItemViewModel.Price;

            UpdateCart(paymentCart);
        }

        public void CreateNewCart()
        {
            var paymentCartVM = new PaymentCartViewModel()
            {
                SessionId = Guid.NewGuid(),
                TotalPrice = 0,
                CartItems = new List<CartItemViewModel>()
            };

            _session.SetObjectAsJson(paymentCartCookieKey, paymentCartVM);
        }

        public void DeleteCartItem(int id)
        {
            var paymentCartVM = GetPaymentCartViewModel();

            var cartItem = paymentCartVM.CartItems.Find(c => c.Id == id);

            if (cartItem != null)
            {
                paymentCartVM.TotalPrice -= cartItem.Price;
                paymentCartVM.CartItems.Remove(cartItem);

                UpdateCart(paymentCartVM);
            }
        }

        public bool Exists()
        {
            var paymentCart = GetPaymentCartViewModel();
            return paymentCart != default(PaymentCartViewModel);
        }

        public PaymentCartViewModel GetPaymentCartViewModel()
        {
            return _session.GetObjectFromJson<PaymentCartViewModel>(paymentCartCookieKey);
        }

        public void UpdateCart(PaymentCartViewModel paymentCartViewModel)
        {
            _session.SetObjectAsJson(paymentCartCookieKey, paymentCartViewModel);
        }
    }
}
