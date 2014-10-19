using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.EntityModel.PayPal;
using DataLayer.Repositories;
using Feromon.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PayPal;

namespace Feromon.Web.Controllers
{
    [System.Web.Mvc.Authorize]
    public class PaymentController : Controller
    {
        private static AspNetUserRepository _aspNetUserRepository = null;
        private static PaymentRepository _paymentRepository = null;
        private static PaymentService _paymentService = null;

        public PaymentController()
        {
            _aspNetUserRepository = new AspNetUserRepository();
            _paymentRepository = new PaymentRepository();
            _paymentService = new PaymentService();
        }
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BuyCondom()
        {
            var model = new PayPalPaymentModel
            {
                Currency = "USD",
                Description = "Condom - transaction",
                Price = "1",
                ProductName = "condom",
                Quantity = "1"
            };
            return RedirectToAction("PaymentWithPayPal", model);
        }

        public ActionResult BuyArrow()
        {
            var model = new PayPalPaymentModel
            {
                Currency = "USD",
                Description = "Arrow - transaction",
                Price = "1",
                ProductName = "arrow",
                Quantity = "1"
            };
            return RedirectToAction("PaymentWithPayPal", model);
        }

        public ActionResult ConfirmPayment(string productType, string paymentId)
        {
            var currentUser = _aspNetUserRepository.FindAspNetUser(User.Identity.Name);
            var model = new Payment
            {
                AspNetUserId = currentUser.Id,
                ExpirationDate = DateTime.UtcNow.AddDays(30),
                PaymentDate = DateTime.UtcNow,
                PaymentId = paymentId,
                ProductName = productType,
                Quantity = 1,
                Usages = 0
            };
            _paymentRepository.Create(model);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult PaymentWithPayPal(PayPalPaymentModel model, String guid, String product, string token, string PayerID)
        {
            var apiContext = Feromon.Web.Services.Configuration.GetAPIContext();
            string returnUrl = null;
            try
            {
                if (string.IsNullOrEmpty(PayerID))
                {
                    // Creating a payment
                    var baseURI = string.Format("{0}://{1}{2}Payment/{3}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"), "PaymentWithPayPal?");
                    var _guid = Guid.NewGuid();
                    var createdPayment = _paymentService.CreatePayment(apiContext, baseURI + "guid=" + _guid + "&product=" + model.ProductName, model);

                    var links = createdPayment.links.GetEnumerator();

                    while (links.MoveNext())
                    {
                        var lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            returnUrl = lnk.href;
                        }
                    }
                    Session.Add(_guid.ToString(), createdPayment.id);
                }
                else
                {
                    // Executing a payment
                    var executedPayment = _paymentService.ExecutePayment(apiContext, PayerID,
                        Session[Request.Params["guid"]].ToString());

                }
                if (returnUrl != null)
                    return Redirect(returnUrl);

                        return RedirectToAction("ConfirmPayment",
                            new {productType = product, paymentId = Session[Request.Params["guid"]].ToString()});
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                //TO-DO: Log;
                return null;
            }
        }
    }
}