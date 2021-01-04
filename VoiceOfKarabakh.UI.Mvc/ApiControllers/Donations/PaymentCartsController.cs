using Microsoft.AspNetCore.Mvc;
using VoiceOfKarabakh.Application.Interfaces.PaymentCart;
using VoiceOfKarabakh.Application.ViewModels.Donation_Shopping;

namespace VoiceOfKarabakh.UI.Mvc.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentCartsController : ControllerBase
    {
        private readonly IPaymentCartService _paymentCartService;

        public PaymentCartsController(IPaymentCartService paymentCartService)
        {
            _paymentCartService = paymentCartService;
        }

        [HttpGet]
        public IActionResult GetCart()
        {
            if (!_paymentCartService.Exists())
            {
                return NotFound();
            }

            return Ok(_paymentCartService.GetPaymentCartViewModel());
        }

        [HttpGet, Route("[action]")]
        public IActionResult CartExists()
        {
            return Ok(_paymentCartService.Exists());
        }

        [HttpPost, Route("[action]")]
        public IActionResult AddCartItemToCart(CartItemViewModel cartItemViewModel)
        {
            _paymentCartService.AddCartItem(cartItemViewModel);

            return CreatedAtAction(nameof(GetCart), _paymentCartService.GetPaymentCartViewModel());
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCartItem(int id)
        {
            _paymentCartService.DeleteCartItem(id);

            return CreatedAtAction(nameof(GetCart), _paymentCartService.GetPaymentCartViewModel());
        }

        [HttpPost, Route("[action]")]
        public IActionResult AddNewCart()
        {
            _paymentCartService.CreateNewCart();

            return CreatedAtAction(nameof(GetCart), _paymentCartService.GetPaymentCartViewModel());
        }
    }
}
