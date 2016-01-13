using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal_Integration.Services
{
    public static class PayPalPaymentService
    {
        //public static void CreatePayment(string payerId)
        //{
        //    // ### Api Context
        //    // Pass in a `APIContext` object to authenticate 
        //    // the call and to send a unique request id 
        //    // (that ensures idempotency). The SDK generates
        //    // a request id if you do not pass one explicitly. 
        //    var apiContext = PayPalConfiguration.GetAPIContext();

        //    if (string.IsNullOrEmpty(payerId))
        //    {
        //        // Items within a transaction.
        //        var itemList = new ItemList()
        //        {
        //            items = new List<Item>()
        //            {
        //                new Item()
        //                {
        //                    name = "Item Name",
        //                    currency = "USD",
        //                    price = "15",
        //                    quantity = "5",
        //                    sku = "sku"
        //                }
        //            }
        //        };

        //        // A resource representing a Payer that funds a payment
        //        var payer = new Payer() { payment_method = "paypal" };

        //        // Redirect URLS
        //        // These URLs will determine how the user is redirected from PayPal 
        //        // once they have either approved or canceled the payment.

        //        var baseURI = "URL where your customer will be redirected from PayPal";

        //        var guid = Convert.ToString((new Random()).Next(100000));
        //        var redirectUrl = baseURI + "guid=" + guid;
        //        var redirUrls = new RedirectUrls()
        //        {
        //            cancel_url = redirectUrl + "&cancel=true",
        //            return_url = redirectUrl
        //        };

        //        // Details: Let's you specify details of a payment amount.
        //        var details = new Details()
        //        {
        //            tax = "15",
        //            shipping = "10",
        //            subtotal = "75"
        //        };

        //        // Amount: Let's you specify a payment amount.
        //        var amount = new Amount()
        //        {
        //            currency = "USD",
        //            total = "100.00", // Total must be equal to sum of shipping, tax and subtotal.
        //            details = details
        //        };

        //        // A transaction defines the contract of a payment
        //        // what is the payment for and who is fulfilling it. 
        //        var transactionList = new List<Transaction>();

        //        // The Payment creation API requires a list of Transaction; 
        //        // add the created Transaction to a List
        //        transactionList.Add(new Transaction()
        //        {
        //            description = "Transaction description.",
        //            invoice_number = Common.GetRandomInvoiceNumber(),
        //            amount = amount,
        //            item_list = itemList
        //        });

        //        // Payment
        //        // A Payment Resource; create one using the above types 
        //        // and intent as `sale` or `authorize`
        //        var payment = new Payment()
        //        {
        //            intent = "sale",
        //            payer = payer,
        //            transactions = transactionList,
        //            redirect_urls = redirUrls
        //        };
                
        //        // Create a payment using a valid APIContext
        //        var createdPayment = payment.Create(apiContext);


        //        // Using the `links` provided by the `createdPayment` object, 
        //        // we can give the user the option to redirect to PayPal to approve the payment.
        //        var links = createdPayment.links.GetEnumerator();
        //        while (links.MoveNext())
        //        {
        //            var link = links.Current;
        //            if (link.rel.ToLower().Trim().Equals("approval_url"))
        //            {
        //                // Redirect to PayPal to approve the payment...":  link.href;
        //                // TODO
        //            }
        //        }

        //        // Save payment info in DB or Session 
        //        // Session.Add(guid, createdPayment.id);
        //        // Session.Add("flow-" + guid, this.flow);
        //    }
        //    else
        //    {
        //        // Retrieve payment ID on page return
        //        var guid = Request.Params["guid"];
                
        //        // Using the information from the redirect, setup the payment to execute.
        //        var paymentId = Session[guid] as string;
        //        var paymentExecution = new PaymentExecution() { payer_id = payerId };
        //        var payment = new Payment() { id = paymentId };
                
        //        // Execute the payment.
        //        var executedPayment = payment.Execute(apiContext, paymentExecution);
                
        //        // For more information, please visit [PayPal Developer REST API Reference](https://developer.paypal.com/docs/api/).
        //    }
        //}

        public static void CapturePayment(string paymentId)
        {
            throw new NotImplementedException();
        }

        public static void RefundPayment(string paymentId)
        {
            throw new NotImplementedException();
        }
    }
}
