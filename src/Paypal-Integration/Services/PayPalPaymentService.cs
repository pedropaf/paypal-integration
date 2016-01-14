using PayPal.Api;
using System;
using System.Collections.Generic;

namespace Paypal_Integration.Services
{
    public static class PayPalPaymentService
    {
        public static Payment CreatePayment(string baseUrl, string intent)
        {
            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            var apiContext = PayPalConfiguration.GetAPIContext();

            // Payment Resource
            var payment = new Payment()
            {
                intent = intent,    // `sale` or `authorize`
                payer = new Payer() { payment_method = "paypal" },
                transactions = GetTransactionsList(),
                redirect_urls = GetReturnUrls(baseUrl, intent)
            };

            // Create a payment using a valid APIContext
            var createdPayment = payment.Create(apiContext);

            return createdPayment;
        }

        private static List<Transaction> GetTransactionsList()
        {
            // A transaction defines the contract of a payment
            // what is the payment for and who is fulfilling it. 
            var transactionList = new List<Transaction>();

            // The Payment creation API requires a list of Transaction; 
            // add the created Transaction to a List
            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                invoice_number = GetRandomInvoiceNumber(),
                amount = new Amount()
                {
                    currency = "USD",
                    total = "100.00",       // Total must be equal to sum of shipping, tax and subtotal.
                    details = new Details() // Details: Let's you specify details of a payment amount.
                    {
                        tax = "15",
                        shipping = "10",
                        subtotal = "75"
                    }
                },
                item_list = new ItemList()
                {
                    items = new List<Item>()
                    {
                        new Item()
                        {
                            name = "Item Name",
                            currency = "USD",
                            price = "15",
                            quantity = "5",
                            sku = "sku"
                        }
                    }
                }
            });
            return transactionList;
        }

        private static RedirectUrls GetReturnUrls(string baseUrl, string intent)
        {
            var returnUrl = intent == "sale" ? "/Home/PaymentSuccessful" : "/Home/AuthorizeSuccessful";

            // Redirect URLS
            // These URLs will determine how the user is redirected from PayPal 
            // once they have either approved or canceled the payment.
            return new RedirectUrls()
            {
                cancel_url = baseUrl + "/Home/PaymentCancelled",
                return_url = baseUrl + returnUrl
            };
        }

        public static Payment ExecutePayment(string paymentId, string payerId)
        {
            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            var apiContext = PayPalConfiguration.GetAPIContext();
            
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            var payment = new Payment() { id = paymentId };

            // Execute the payment.
            var executedPayment = payment.Execute(apiContext, paymentExecution);

            return executedPayment;
        }

        public static Capture CapturePayment(string paymentId)
        {
            var apiContext = PayPalConfiguration.GetAPIContext();

            var payment = Payment.Get(apiContext, paymentId);
            var auth = payment.transactions[0].related_resources[0].authorization;

            // Specify an amount to capture.  By setting 'is_final_capture' to true, all remaining funds held by the authorization will be released from the funding instrument.
            var capture = new Capture()
            {
                amount = new Amount()
                {
                    currency = "USD",
                    total = "4.54"
                },
                is_final_capture = true
            };

            // Capture an authorized payment by POSTing to
            // URI v1/payments/authorization/{authorization_id}/capture
            var responseCapture = auth.Capture(apiContext, capture);

            return responseCapture;
        }
        
        public static string GetRandomInvoiceNumber()
        {
            return new Random().Next(999999).ToString();
        }
    }
}
