using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer.EntityModel.PayPal;
using PayPal;
using PayPal.Api.Payments;

namespace Feromon.Web.Services
{
    public class PaymentService
    {
        private Payment payment;
        public Payment CreatePayment(APIContext apiContext, string redirectUrl, PayPalPaymentModel paymentModel)
        {
            var itemList = new ItemList() { items = new List<Item>() };
            //TO-DO: add to itemList;
            itemList.items.Add(new Item()
            {
                name = paymentModel.ProductName,
                currency = paymentModel.Currency,
                price = paymentModel.Price,
                quantity = paymentModel.Quantity,
                sku = "sku"
            });

            var payer = new Payer() { payment_method = "paypal" };
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            var total = (Convert.ToInt16(paymentModel.Price)*Convert.ToInt16(paymentModel.Quantity)).ToString();

            //TO-DO: add to Details: new Details(){};
            var details = new Details();
            details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = total
            };

            //TO-DO: add to Amount: new Amount(){};
            var amount = new Amount(); 
            amount = new Amount()
            {
                currency = paymentModel.Currency,
                total = total, 
                details = details
            };

            var transactionList = new List<Transaction>
                                  {
                                      new Transaction()
                                      {
                                          description = paymentModel.Description,
                                          amount = amount,
                                          item_list = itemList
                                      }
                                  };

            //Payment
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a valid APIContext
            return this.payment.Create(apiContext);
        }

        public Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }
    }

    public static class Configuration
    {
        public static readonly string ClientId = "AUC5ahAtBWQLtE8bdCQzabpUyUCjp4wsfUPcqp0BVd9FHNaGJH7ASbYbE5kD";
        public static readonly string ClientSecret = "EPq-3hBgBUzMsfxHENPDMeAYjQTopdP2lQKS50eb1JoM361VbppVvXdF4iTA";

        // Create the configuration map that contains mode and other optional configuration details.
        public static Dictionary<string, string> GetConfig()
        {
            var config = new Dictionary<string, string>();

            // Endpoints are varied depending on whether sandbox OR live is chosen for mode
            config["mode"] = "sandbox";
            config["endpoint"] = "https://api.sandbox.paypal.com/";

            // These values are defaulted in SDK. If you want to override default values, uncomment it and add your value
            // config["connectionTimeout"] = "360000";
            // config["requestRetries"] = "1";
            return config;
        }

        // Create accessToken
        private static string GetAccessToken()
        {              
            var accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }

        // Returns APIContext object
        public static APIContext GetAPIContext()
        {
            var apiContext = new APIContext(GetAccessToken()) {Config = GetConfig()};
            return apiContext;
        }

    }
}