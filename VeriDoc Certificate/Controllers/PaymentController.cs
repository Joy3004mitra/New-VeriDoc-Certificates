using MyApp.db;
using MyApp.db.Encryption;
using MyApp.db.MyAppBal;
using MyApp.Entity;
using Square;
using Square.Exceptions;
using Square.Models;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace VeriDoc_Certificate.Controllers
{
    public class PaymentController : Controller
    {
        private SquareClient client;
        private string locationId;

        #region Models

        public class CardModel
        {
            public string customerId { get; set; }
            public string cardNonce { get; set; }
            public string planName { get; set; }
            public string planPriceAU { get; set; }
        }

        #endregion

        public ActionResult Index()
        {
            if (TempData.ContainsKey("customerId"))
                ViewBag.customerId = TempData["customerId"].ToString();
            else
                return RedirectToAction("Index", "Customer");
            if (TempData.ContainsKey("customerEmail"))
                ViewBag.customerEmail = TempData["customerEmail"].ToString();
            else
                return RedirectToAction("Index", "Customer");
            if (TempData.ContainsKey("customerName"))
                ViewBag.customerName = TempData["customerName"].ToString();
            else
                return RedirectToAction("Index", "Customer");
            if (TempData.ContainsKey("customerId"))
                ViewBag.customerId = TempData["customerId"].ToString();
            else
                return RedirectToAction("Index", "Customer");
            if (TempData.ContainsKey("PlanName"))
                ViewBag.PlanName = TempData["PlanName"].ToString();
            else
                return RedirectToAction("Index", "Customer");
            if (TempData.ContainsKey("PlanPrice"))
                ViewBag.PlanPrice = TempData["PlanPrice"].ToString();
            else
                return RedirectToAction("Index", "Customer");
            if (TempData.ContainsKey("PlanPriceAU"))
                ViewBag.PlanPriceAU = TempData["PlanPriceAU"].ToString();
            else
                return RedirectToAction("Index", "Customer");

            return View();
        }

        public ActionResult Success()
        {
            if (TempData.ContainsKey("sPlanName"))
                ViewBag.PlanName = TempData["sPlanName"].ToString();
            if (TempData.ContainsKey("sPlanPriceAU"))
                ViewBag.PlanPriceAU = TempData["sPlanPriceAU"].ToString();

            return View();
        }

        public PaymentController()
        {
            Square.Environment environment = ConfigurationManager.AppSettings["SquareEnvironment"] == "sandbox" ?
                Square.Environment.Sandbox : Square.Environment.Production;
            client = new SquareClient.Builder()
                .Environment(environment)
                .AccessToken(ConfigurationManager.AppSettings["SquareAccessToken"])
                .Build();

            locationId = ConfigurationManager.AppSettings["SquareLocationId"];
        }

        private static string NewIdempotencyKey()
        {
            return Guid.NewGuid().ToString();
        }

        private async Task<double> getAUValue()
        {
            var priceInAUD = 1.30;
            var freeCurrencyConvertor = await VeriDocHttpHandler.Get<FreeCurrencyConvertor>(new Uri(ConstantVariables.CurrancyConvertorAPI), 300);
            if (freeCurrencyConvertor != null)
                priceInAUD = Math.Round(freeCurrencyConvertor.USD_AUD, 2);

            return priceInAUD;                
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> initSquarePaymentAsync(FormCollection customer)
        {
            string uuid = NewIdempotencyKey();
            string AddressLine2 = customer["BillingCompanyCity"].ToString() + ", " + customer["BillingCompanyState"].ToString();
            string PlanName = customer["hfPlanName"].ToString();
            string PlanPrice = customer["hfPlanPrice"].ToString();
            string FullName = customer["FirstName"].ToString() + " " + customer["LastName"].ToString();
            var address = new Address.Builder()
                .AddressLine1(customer["BillingCompanyAddress"].ToString())
                .AddressLine2(AddressLine2)
                .PostalCode(customer["BillingZipCode"].ToString())
                .Country(customer["Country1"].ToString())
                .FirstName(customer["FirstName"].ToString())
                .LastName(customer["LastName"].ToString())
                .Organization(customer["BillingCompanyName"].ToString())
                .Build();

            var body = new CreateCustomerRequest.Builder()
              .IdempotencyKey(uuid)
              .GivenName(customer["FirstName"].ToString())
              .FamilyName(customer["LastName"].ToString())
              .CompanyName(customer["BillingCompanyName"].ToString())
              .EmailAddress(customer["EmailId"].ToString())
              .Address(address)
              .PhoneNumber(customer["Phone-Number"].ToString() + customer["PhoneNumber"].ToString())
              .Build();

            try
            {
                var res = await client.CustomersApi.CreateCustomerAsync(body);
                TempData["customerId"] = res.Customer.Id;
                TempData["customerEmail"] = customer["EmailId"].ToString();
                TempData["customerName"] = FullName;
                TempData["PlanName"] = PlanName;
                TempData["PlanPrice"] = PlanPrice;
                ViewBag.FirstName = customer["FirstName"].ToString();
                ViewBag.Email = customer["EmailId"].ToString();
                var multiplier = await getAUValue();
                var PlanPriceAU = Convert.ToDouble(PlanPrice) * multiplier;
                TempData["PlanPriceAU"] = PlanPriceAU;


                if (btn_submit_Click(customer, res.Customer.Id) == "1")
                {
                    return RedirectToAction("Index", "Payment");
                }
                else
                {
                    TempData["JavaScriptFunction"] = string.Format("error();");
                    return RedirectToAction("Index", "Customer");


                }




            }
            catch (ApiException e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction("Plan", "Subscribe");
            }
        }

        [HttpPost]
        public async Task<JsonResult> sqAddCardonFile(CardModel card)
        {
            string result, cardId, error = String.Empty;
            var body = new CreateCustomerCardRequest.Builder(cardNonce: card.cardNonce)
                .Build();
            try
            {
                var res = await client.CustomersApi.CreateCustomerCardAsync(customerId: card.customerId, body: body);
                cardId = res.Card.Id;
                string plan = card.planName == "BASIC" ? ConfigurationManager.AppSettings["BasicPlan"] : card.planName == "STANDARD" ? ConfigurationManager.AppSettings["StandardPlan"] : ConfigurationManager.AppSettings["ProPlan"];
                result = await sqGenerateSubscription(plan, card.customerId, cardId);

                if (result != "error")
                {
                    TempData["sPlanName"] = card.planName;
                    TempData["sPlanPriceAU"] = card.planPriceAU;

                    send(Session["EMAIL"].ToString(), Convert.ToBoolean(TempData["samebilling"]));
                    return Json(new { success = result });
                }
                else
                    return Json(new { error = result });
            }
            catch (ApiException e)
            {
                return Json(new { error = e.Message });
            }
        }

        private async Task<string> sqGenerateSubscription(string planId, string customerId, string cardId)
        {
            string result, uuid = NewIdempotencyKey();
            var body = new CreateSubscriptionRequest.Builder(
                idempotencyKey: uuid,
                locationId: locationId,
                planId: planId,
                customerId: customerId)
              .CardId(cardId)
              .Build();
            try
            {
                var res = await client.SubscriptionsApi.CreateSubscriptionAsync(body);
                result = res.Subscription.Id;
            }
            catch (ApiException e)
            {
                Console.WriteLine($"Exception: {e.Message}");
                result = "error";
            }
            return result;
        }

        public string btn_submit_Click(FormCollection form, string customerid)
        {
            EntityRegister oBMST = null;
            Byte vRef = 0; Int32 vKey = 0;
            String errMsg = String.Empty;
            DataTable dt4 = new DataTable();
            //Int32 pricingkey = Convert.ToInt32(Session["price"]);
            ViewBag.hf_CUSTOMER_ID = customerid;
            EntityPricing oBMSTP = new EntityPricing();
            try
            {
                oBMST = new EntityRegister();

                oBMST.DTLS_REGISTER_KEY = Convert.ToInt32(0);
                oBMST.FIRSTNAME = ViewBag.FirstName;
                oBMST.LASTNAME = form["LastName"];
                oBMST.EMAIL = ViewBag.Email;
                oBMST.PHONE = form["PhoneNumber"];
                oBMST.COMPANY_NAME = form["CompanyName"];
                oBMST.COMPANY_ADDRESS = form["CompanyAddress"];
                oBMST.COUNTRY = form["AlterCountry"];
                oBMST.STATE = form["CompanyState"];
                oBMST.CITY = form["CompanyCity"];
                oBMST.ZIP = form["CompanyZIP"];
                oBMST.DTLS_PACKAGE_KEY = Convert.ToInt32(TempData["packagekey"]);
                //oBMST.DTLS_PACKAGE_KEY = 1;

                oBMST.ALTER_COMPANY_NAME = form["BillingCompanyName"];
                oBMST.ALTER_COMPANY_ADDRESS = form["BillingCompanyAddress"];
                oBMST.ALTER_COUNTRY = form["Country"];
                oBMST.ALTER_STATE = form["BillingCompanyState"];
                oBMST.ALTER_CITY = form["BillingCompanyCity"];
                oBMST.ALTER_ZIP = form["BillingZIPCode"];

                oBMST.CUSTOMER_ID = ViewBag.hf_CUSTOMER_ID;

                oBMST.ENT_USER_KEY = oBMST.DTLS_REGISTER_KEY;
                oBMST.EDIT_USER_KEY = oBMST.DTLS_REGISTER_KEY;
                oBMST.TAG_ACTIVE = 1;
                oBMST.TAG_DELETE = 0;

                Session["FIRSTNAME"] = oBMST.FIRSTNAME;
                Session["LASTNAME"] = oBMST.LASTNAME;
                Session["EMAIL"] = oBMST.EMAIL;
                Session["PHONE"] = oBMST.PHONE;
                Session["COMPANY_NAME"] = oBMST.COMPANY_NAME;
                Session["COMPANY_ADDRESS"] = oBMST.COMPANY_ADDRESS;
                Session["COUNTRY"] = oBMST.COUNTRY;
                Session["STATE"] = oBMST.STATE;
                Session["CITY"] = oBMST.CITY;
                Session["ZIP"] = oBMST.ZIP;
                Session["COUNTRY"] = oBMST.COUNTRY;


                Session["FIRSTNAME"] = oBMST.FIRSTNAME;
                Session["LASTNAME"] = oBMST.LASTNAME;
                Session["EMAIL"] = oBMST.EMAIL;
                Session["PHONE"] = oBMST.PHONE;
                Session["ALTER_COMPANY_NAME"] = oBMST.ALTER_COMPANY_NAME;
                Session["ALTER_COMPANY_ADDRESS"] = oBMST.ALTER_COMPANY_ADDRESS;
                Session["ALTER_COUNTRY"] = oBMST.ALTER_COUNTRY;
                Session["ALTER_STATE"] = oBMST.ALTER_STATE;
                Session["ALTER_CITY"] = oBMST.ALTER_CITY;
                Session["ALTER_ZIP"] = oBMST.ALTER_ZIP;



                using (BaRegister oBDT = new BaRegister())
                {

                    if (oBMST.DTLS_REGISTER_KEY == 0)
                    {
                        if (form["SameBillingDetails"] == "yes")
                        {
                            if (form["FistName"] != "" && form["LastName"] != "" && form["Eamil"] != "" && form["Phonenumber"] != "")
                            {
                                if (form["AlterCountry"] != "" && form["CompanyName"] != "" && form["CompanyAddress"] != "")
                                {
                                    vRef = oBDT.SaveChanges<Object, Int32>("INSERT", oBMST, null, ref vKey, ref errMsg, "2019", 1);
                                    if (vRef == 1)
                                    {
                                        //MessageBox(2, Message.msgSaveNew, "");
                                        //ClientScript.RegisterStartupScript(GetType(), "popup", "popup();", true);
                                        //Response.Redirect("/Index/Index", false);


                                        oBMSTP.PACKAGE_NAME = TempData["PlanName"].ToString();

                                        //if (oBMSTP.PACKAGE_NAME == "FREE TRIAL")
                                        //{
                                        //    Session["popup"] = "1";
                                        //    Response.Redirect("/Home/Index", false);

                                        //}
                                        //else if (oBMSTP.PACKAGE_NAME == "PRO")
                                        //{
                                        //    Response.Redirect("#", false);

                                        //}
                                        //else if (oBMSTP.PACKAGE_NAME == "STANDARD")
                                        //{
                                        //    Response.Redirect("#", false);

                                        //}


                                        //  ClearControl(form);
                                    }
                                }
                                else
                                {
                                    //ClientScript.RegisterStartupScript(GetType(), "popuperror", "popuperror();", true);
                                    // //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Fill All * mark filed ');", true);
                                }
                            }
                            else
                            {
                                //ClientScript.RegisterStartupScript(GetType(), "popuperror", "popuperror();", true);
                                ////ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Fill All * mark filed ');", true);
                            }
                        }
                        else if (form["SameBillingDetails"] == "no")
                        {
                            if (form["BillingCompanyName"] != "" && form["BillingCompanyAddress"] != "" && form["BillingCompanyCity"] != "")
                            {
                                if (form["Country"] != "")
                                {
                                    vRef = oBDT.SaveChanges<Object, Int32>("INSERT", oBMST, null, ref vKey, ref errMsg, "2019", 1);
                                    if (vRef == 1)
                                    {
                                        //MessageBox(2, Message.msgSaveNew, "");
                                        //ClientScript.RegisterStartupScript(GetType(), "popup", "popup();", true);


                                        oBMSTP.PACKAGE_NAME = TempData["PlanName"].ToString();

                                        //if (oBMSTP.PACKAGE_NAME == "FREE TRIAL")
                                        //{
                                        //    Session["popup"] = "1";
                                        //    Response.Redirect("/Home/Index", false);

                                        //}
                                        //else if (oBMSTP.PACKAGE_NAME == "PRO")
                                        //{

                                        //    Response.Redirect("/Home/Index", false);

                                        //}
                                        //else if (oBMSTP.PACKAGE_NAME == "STANDARD")
                                        //{

                                        //    Response.Redirect("/Home/Index", false);

                                        //}

                                        // ClearControl(form);
                                    }
                                }
                                else
                                {
                                    //ClientScript.RegisterStartupScript(GetType(), "popuperror", "popuperror();", true);
                                    // //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Fill All * mark filed ');", true);
                                }
                            }
                            else
                            {
                                //ClientScript.RegisterStartupScript(GetType(), "popuperror", "popuperror();", true);
                                ////ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Fill All * mark filed ');", true);

                            }
                        }
                        else if (vRef == 2)
                        {
                            //MessageBox(2, Message.msgValidation, errMsg);
                            //ClientScript.RegisterStartupScript(GetType(), "popuperror", "popuperror();", true);
                        }
                        else
                        {
                            //MessageBox(2, Message.msgSaveErr, errMsg);
                        }
                    }

                }

                return "1";

            }
            catch (Exception ex)
            {
                return "0";
                // //ClientScript.RegisterStartupScript(GetType(), "popupservererror", "popupservererror(); console.log('" + ex.Message + "');", true);


            }


        }

        protected string send(string recepientEmail, Boolean sameaddressbilling)
        {
            try
            {
                string to_username = ConfigurationManager.AppSettings["to_username"].ToString();
                string form_username = ConfigurationManager.AppSettings["form_username"].ToString();
                string form_password = ConfigurationManager.AppSettings["form_password"].ToString();
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true; //false

                using (MailMessage mail = new MailMessage())
                {
                    string template = string.Empty;
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/mailtemplate.html")))
                    {
                        template = reader.ReadToEnd();
                        template = template.Replace("FULLNAME", Session["FIRSTNAME"] + " " + Session["LASTNAME"]);
                        template = template.Replace("EMAILID", Session["EMAIL"].ToString());
                        template = template.Replace("MOBILENO", Session["PHONE"].ToString());
                        template = template.Replace("COMNAME", Session["COMPANY_NAME"].ToString());
                        template = template.Replace("COMADDRESS", Session["COMPANY_NAME"].ToString());
                        template = template.Replace("COUNTRY", Session["COUNTRY"].ToString());
                        template = template.Replace("STATE", Session["STATE"].ToString());
                        template = template.Replace("CITY", Session["CITY"].ToString());
                        template = template.Replace("ZIP", Session["ZIP"].ToString());
                        template = template.Replace("PRICINGPLAN", Session["package"].ToString());
                        if (sameaddressbilling)
                        {
                            template = template.Replace("ALTER_COMPANY_NAME", "N/A");
                            template = template.Replace("ALTER_COMPANY_ADDRESS", "N/A");
                            template = template.Replace("ALTER_" + Session["COUNTRY"].ToString() + "", "N/A");
                            template = template.Replace("ALTER_" + Session["CITY"].ToString() + "", "N/A");
                            template = template.Replace("ALTER_" + Session["STATE"].ToString() + "", "N/A");
                            template = template.Replace("ALTER_" + Session["ZIP"].ToString() + "", "N/A");
                        }
                        else
                        {
                            template = template.Replace("ALTER_COMPANY_NAME", Session["ALTER_COMPANY_NAME"].ToString());
                            template = template.Replace("ALTER_COMPANY_ADDRESS", Session["ALTER_COMPANY_NAME"].ToString());
                            template = template.Replace("ALTER_" + Session["COUNTRY"].ToString() + "", Session["ALTER_COUNTRY"].ToString());
                            template = template.Replace("ALTER_" + Session["CITY"].ToString() + "", Session["ALTER_CITY"].ToString());
                            template = template.Replace("ALTER_" + Session["STATE"].ToString() + "", Session["ALTER_STATE"].ToString());
                            template = template.Replace("ALTER_" + Session["ZIP"].ToString() + "", Session["ALTER_ZIP"].ToString());
                        }
                    }
                    mail.From = new MailAddress(form_username);
                    mail.To.Add(to_username);
                    mail.Subject = "New registration from " + Session["FIRSTNAME"] + " " + ViewBag.txt_LastName;
                    mail.Body = template.ToString();
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(form_username, Encrypted.Decryptdata(form_password));
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

                using (MailMessage mail1 = new MailMessage())
                {
                    string templatebody = string.Empty;
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/regmailtemplate.html")))
                    {
                        templatebody = reader.ReadToEnd();
                        templatebody = templatebody.Replace("FULLNAME", Session["FIRSTNAME"] + " " + ViewBag.txt_LastName);
                        templatebody = templatebody.Replace("PRICINGPLAN", Session["package"].ToString());

                    }
                    mail1.From = new MailAddress(form_username);
                    mail1.To.Add(Session["EMAIL"].ToString());
                    mail1.Subject = "Thank you for registering on Veridoc HR";
                    mail1.Body = templatebody.ToString();
                    mail1.IsBodyHtml = true;

                    using (SmtpClient smtp1 = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp1.UseDefaultCredentials = false;
                        smtp1.Credentials = new NetworkCredential(form_username, Encrypted.Decryptdata(form_password));
                        smtp1.EnableSsl = enableSSL;
                        smtp1.Send(mail1);
                    }
                }

                return "1";

            }
            catch (Exception ex)
            {
                return "0";
                // ClientScript.RegisterStartupScript(GetType(), "popupservererror", "popupservererror(); console.log('" + ex.Message + "');", true);
            }

            //return Redirect("~/Register/Index");
        }

        #region Webhook from Square

        [HttpPost]
        public ActionResult sqProcessInvoice()
        {
            string path = Server.MapPath("~/TestWebhook.txt");
            try
            {
                Stream req = Request.InputStream;
                req.Seek(0, System.IO.SeekOrigin.Begin);
                string json = new StreamReader(req).ReadToEnd();
                using (StreamWriter sw = System.IO.File.CreateText(path)) sw.WriteLine(json);

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        #endregion

    }
}