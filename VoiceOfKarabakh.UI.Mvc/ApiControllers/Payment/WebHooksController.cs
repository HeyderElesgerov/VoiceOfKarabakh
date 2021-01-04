using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceOfKarabakh.UI.Mvc.ApiControllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebHooksController : ControllerBase
    {
        public IActionResult StripeWebHook()
        {
            try
            {
                var json = new StreamReader(HttpContext.Request.Body).ReadToEnd();

                // validate webhook called by stripe only
                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], "---REPLACE STRIPE WEBHOOK SECRET---");

                switch (stripeEvent.Type)
                {
                    case "customer.created":
                        var customer = stripeEvent.Data.Object as Customer;
                        // do work

                        break;

                    case "customer.subscription.created":
                    case "customer.subscription.updated":
                    case "customer.subscription.deleted":
                    case "customer.subscription.trial_will_end":
                        var subscription = stripeEvent.Data.Object as Subscription;
                        // do work

                        break;

                    case "invoice.created":
                        var newinvoice = stripeEvent.Data.Object as Invoice;
                        // do work

                        break;

                    case "invoice.upcoming":
                    case "invoice.payment_succeeded":
                    case "invoice.payment_failed":
                        var invoice = stripeEvent.Data.Object as Invoice;
                        // do work

                        break;

                    case "coupon.created":
                    case "coupon.updated":
                    case "coupon.deleted":
                        var coupon = stripeEvent.Data.Object as Coupon;
                        // do work

                        break;

                        // DO SAME FOR OTHER EVENTS
                }

                return Ok();
            }
            catch (StripeException ex)
            {
                //_logger.LogError(ex, $"StripWebhook: {ex.Message}");
                return BadRequest();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"StripWebhook: {ex.Message}");
                return BadRequest();
            }
        }
    }
}
