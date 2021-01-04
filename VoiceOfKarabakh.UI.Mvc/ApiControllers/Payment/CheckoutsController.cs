using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using VoiceOfKarabakh.Application.Interfaces.Donation;
using VoiceOfKarabakh.Application.Options;
using VoiceOfKarabakh.Application.ViewModels.Donation_Shopping;

namespace VoiceOfKarabakh.UI.Mvc.Controllers.API.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutsController : ControllerBase
    {
        private readonly PaymentOption _paymentOption;
        private IDonationService _donationService { get; set; }
        private string defaultCultureCode;

        public CheckoutsController(IOptions<PaymentOption> paymentOption, IDonationService donationService, IOptions<RequestLocalizationOptions> cultureOptions)
        {
            _paymentOption = paymentOption.Value;
            _donationService = donationService;
            defaultCultureCode = cultureOptions.Value.DefaultRequestCulture.Culture.TwoLetterISOLanguageName;
        }

        [HttpPost]
        public ActionResult Create(PaymentCartViewModel paymentCart)
        {
            var domain = "https://" + HttpContext.Request.Host.Value;

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + _paymentOption.SuccessPath,
                CancelUrl = domain + _paymentOption.CanclePath
            };

            foreach(var cartItem in paymentCart.CartItems)
            {
                int donationId = cartItem.DonationId;
                var donationVM = _donationService.GetDonationViewModel(
                    donationId, defaultCultureCode, "TitleLocalizationSet.Localizations");
                string cartDonationName = donationVM.Title;

                options.LineItems.Add(new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = cartItem.Price * 100,
                        Currency = "azn",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = cartDonationName
                        },
                    },
                    Quantity = 1
                });
            }

            var service = new SessionService();
            Session session = service.Create(options, new Stripe.RequestOptions()
            {
                ApiKey = _paymentOption.SecretKey
            });

            return new JsonResult(new { id = session.Id });
        }
    }
}
