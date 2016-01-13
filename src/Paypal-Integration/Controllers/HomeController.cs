using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Paypal_Integration.Services;
using PayPal.Api;

namespace Paypal_Integration.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();

        }

        #region Billing Plan and subscription
        // Create a billing plan and subscribe to it
        public IActionResult Subscribe()
        {
            var plan = PayPalSubscriptionsService.CreateBillingPlan("Tuts+ Plan", "Test plan for this article", GetBaseUrl());

            var subscription = PayPalSubscriptionsService.CreateBillingAgreement(plan.id, 
                new PayPal.Api.ShippingAddress
                {
                    city = "London", 
                    line1 = "line 1",
                    postal_code = "SW1A 1AA",
                    country_code = "GB"
                }, "Pedro Alonso", "Tuts+", DateTime.Now);
            
            return Redirect(subscription.GetApprovalUrl());
        }

        public IActionResult SubscribeSuccess(string token)
        {
            // Execute approved agreement
            PayPalSubscriptionsService.ExecuteBillingAgreement(token);

            return View();
        }

        public IActionResult SubscribeCancel(string token)
        {
            // TODO: Handle cancelled payment
            return RedirectToAction("Error");
        }
        #endregion

        public string GetBaseUrl()
        {
            return Request.Scheme + "://" + Request.Host;
        }
    }
}
