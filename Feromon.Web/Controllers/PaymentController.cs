using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.EntityModel.PayPal;
using Feromon.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PayPal;

namespace Feromon.Web.Controllers
{
    public class PaymentController : Controller
    {
        private static PaymentService _paymentService = null;

        public PaymentController()
        {
            _paymentService = new PaymentService();
        }
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PaymentWithPayPal(PayPalPaymentModel model, String guid, string token, string PayerID)
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
                    var createdPayment = _paymentService.CreatePayment(apiContext, baseURI + "guid=" + _guid, model);

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