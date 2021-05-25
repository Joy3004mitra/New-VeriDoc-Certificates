namespace VeriDoc_Certificate.Controllers
{
    using MyApp.db.Encryption;
    using MyApp.db.MyAppBal;
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using System.Web.Mvc;
   

    #endregion Using Directives

    /// <summary>
    /// Customer controller
    /// </summary>
    public class CustomerController : Controller
    {
        DataTable dt;
        DataSet ds;
        String errMsg = String.Empty;
        String vString = String.Empty;
        /// <summary>
        /// Bye Now Page
        /// </summary>
        /// <returns>Return Bye Now Page View</returns>
        public ActionResult Index()
        {
           // ViewBag.AdminSiteURLForCustomer = ConstantVariables.AdminSiteURLForCustomer;
            FillSiteSettingEdit();
            FillTechnologyGrid();
            FillMastBenefitGrid();
            FillMastusecaseGrid();
            FillMastPackageGrid();
            return View();
        }

        //[HttpGet]
        //public ActionResult AddCustomer(int id)
        //{
        //    FillSiteSettingEdit();
        //    int ausCountryCodeValue = 13;
        //    ViewBag.SelectedProductId = id;
        //    var countryList = MasterAdminBAL.GetContryCodes();
        //    List<ListItemDTO> countryCodes = GetCountryCodeList(countryList);
        //    List<ListItemDTO> countries = GetCountryList(countryList);
        //    //ViewBag.ProductList = new SelectList(GetProductList(), "Id", "Value", selectedValue: 1); //First product selected.
        //    ViewBag.CountryCodes = new SelectList(countryCodes, "Id", "Value", selectedValue: ausCountryCodeValue);//13 - Austrailia
        //    ViewBag.Countries = new SelectList(countries, "Id", "Value");
        //    return View();
        //}

        //[HttpPost]
        //public async Task<ActionResult> AddCustomer(CustomerDTO customer)
        //{
        //    var customerId = CustomerBAL.AddCustomer(customer);
        //    if(customerId > -1)
        //    {
        //        var checkoutLink = await CustomerBAL.GetCheckoutLink(customer.ProductId);
        //        CustomerBAL.UpdateSqaureCheckoutLink(checkoutLink, customerId);
        //        CustomerBAL.SendCustomerAddedEmail(ConstantVariables.VeridocAdminEmailId, customer);
        //        CustomerBAL.SendEmailToUser(customer);
        //        return Json(new { checkoutLink = checkoutLink, result = true }, JsonRequestBehavior.AllowGet);
        //    }
        //    return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// Get Contry Code Select List
        ///// </summary>
        ///// <param name="codeList">Code List</param>
        ///// <returns>Return select list</returns>
        //private List<ListItemDTO> GetCountryCodeList(List<CountryCodeDTO> codeList)
        //{
        //    List<ListItemDTO> items = codeList.Select(cl => new ListItemDTO
        //    {
        //        Id = cl.CountryId.ToString(),
        //        Value = cl.CountryName + " ( +" + cl.PhoneCode + " )"
        //    }).ToList();

        //    return items;
        //}

        ///// <summary>
        ///// Get Contry Select List
        ///// </summary>
        ///// <param name="codeList">Code List</param>
        ///// <returns>Return select list</returns>
        //private List<ListItemDTO> GetCountryList(List<CountryCodeDTO> countries)
        //{
        //    List<ListItemDTO> items = countries.Select(cl => new ListItemDTO
        //    {
        //        Id = cl.CountryName.ToString(),
        //        Value = cl.CountryName
        //    }).ToList();

        //    return items;
        //}

        public ActionResult Sitemap()
        {
            return View();
        }

        //private List<ListItemDTO> GetProductList()
        //{
        //    List<ListItemDTO> productList = new List<ListItemDTO>();
        //    productList.Add(new ListItemDTO()
        //    {
        //        Id = "1",
        //        Value = "The free trial includes a  maximum number of 10  employees"
        //    });
        //    productList.Add(new ListItemDTO()
        //    {
        //        Id = "2",
        //        Value = "Up to 10 active employees  per month."
        //    });
        //    productList.Add(new ListItemDTO()
        //    {
        //        Id = "3",
        //        Value = "Up to 30 active employees  per month."
        //    });
        //    return productList;
        //}
        private String FillSiteSettingEdit()
        {
            try
            {
                String vCOMPANY_ACCESS = String.Empty;
                errMsg = String.Empty;
                dt = new DataTable();
                using (BaSiteSetting oBME = new BaSiteSetting())
                {
                    dt = oBME.GetData("ALL", "", ref errMsg, null, 1);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewBag.txt_BANNER_HEADING_1 = Convert.ToString(dt.Rows[0]["BANNER_HEADING_1"]);
                    ViewBag.txt_BANNER_HEADING_2 = Convert.ToString(dt.Rows[0]["BANNER_HEADING_2"]);
                    ViewBag.txt_BANNER_HEADING_3 = Convert.ToString(dt.Rows[0]["BANNER_HEADING_3"]);
                    ViewBag.txt_CONTACT_NO = Convert.ToString(dt.Rows[0]["CONTACT_NO"]);
                    ViewBag.txt_MAIL = Convert.ToString(dt.Rows[0]["MAIL"]);
                    ViewBag.txt_ADDRESS = Convert.ToString(dt.Rows[0]["ADDRESS"]);
                    ViewBag.img_banner = ConfigurationManager.AppSettings["HOME"].ToString() + Convert.ToString(dt.Rows[0]["BANNER_IMAGE"]);
                    ViewBag.hf_BANNER_IMAGE = Convert.ToString(dt.Rows[0]["BANNER_IMAGE"]);
                    ViewBag.img_Logo = ConfigurationManager.AppSettings["HOME"].ToString() + Convert.ToString(dt.Rows[0]["LOGO_NAME"]);
                    ViewBag.hf_LOGO_NAME = Convert.ToString(dt.Rows[0]["LOGO_NAME"]);
                    ViewBag.img_TECHNOLOGY = ConfigurationManager.AppSettings["HOME"].ToString() + Convert.ToString(dt.Rows[0]["TECHNOLOGY_IMAGE"]);
                    ViewBag.hf_TECHNOLOGY_IMAGE = Convert.ToString(dt.Rows[0]["TECHNOLOGY_IMAGE"]);

                    ViewBag.txt_FACEBOOK_LINK = Convert.ToString(dt.Rows[0]["FACEBOOK_LINK"]);
                    ViewBag.txt_LINKEDIN_LINK = Convert.ToString(dt.Rows[0]["LINKEDIN_LINK"]);
                    ViewBag.txt_TWITTER_LINK = Convert.ToString(dt.Rows[0]["TWITTER_LINK"]);
                    ViewBag.txt_INSTAGRAM_LINK = Convert.ToString(dt.Rows[0]["INSTAGRAM_LINK"]);
                    ViewBag.txt_TELEGRAM_LINK = Convert.ToString(dt.Rows[0]["TELEGRAM_LINK"]);
                    ViewBag.txt_PRINTEREST_LINK = Convert.ToString(dt.Rows[0]["PRINTEREST_LINK"]);
                    ViewBag.txt_MEDIUM_LINK = Convert.ToString(dt.Rows[0]["MEDIUM_LINK"]);
                    ViewBag.txt_FULL_ADDRESS = Convert.ToString(dt.Rows[0]["FULL_ADDRESS"]);
                }
                return errMsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                dt = null;
            }
        }

        private String FillTechnologyGrid()
        {
            try
            {
                errMsg = String.Empty;
                vString = String.Empty;
                dt = new DataTable();
                ds = new DataSet();

                using (BaSiteSetting oBMC = new BaSiteSetting())
                {
                    dt = oBMC.GetTechnology("GET", 0, "", ref errMsg, "2019", 1);
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        vString += "<div class='col-xl-6 col-lg-6 col-md-6 col-sm-12 col-12'><ul><li>" + dt.Rows[i]["TECHNOLOGY_DESC"].ToString() + "</li></ul></div>";

                    }

                    ViewBag.txt_TECHNOLOGY_DESC = vString;
                }
               

                return errMsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                dt = null;
            }
        }

        private DataSet FillMastBenefitGrid()
        {
            try
            {
                errMsg = String.Empty;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                dt = new DataTable();
                ds = new DataSet();
                vString = String.Empty;
                using (BaBenefits oBMC = new BaBenefits())
                {
                    dt = oBMC.Get<Int32>("GET", 0, "", ref errMsg, "2019", 1);
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        vString += "<div class='col-xl-4 col-lg-4 col-md-6 col-sm-6 col-12'><div class='single_feature text-center'><div class='feature_img'><img src='" + ConfigurationManager.AppSettings["BENEFITS"].ToString() + Convert.ToString(dt.Rows[i]["BENEFITS_IMAGE"]) + "' alt='veridocklockbook' class='img-fluid' width='103' height='133' title='featured-img'></div><div class='featured_content'><h4>" + Convert.ToString(dt.Rows[i]["BENEFITS_HEADING"]) + "</h4><p>" + Convert.ToString(dt.Rows[i]["BENEFITS_DESC"]) + "</p></div></div></div>";

                    }
                    ViewBag.Benefits = vString;

                }


            }
            catch (Exception ex)
            {
                // return ex.Message;
            }
            return ds;

        }

        private DataSet FillMastusecaseGrid()
        {
            try
            {
                errMsg = String.Empty;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                vString = String.Empty;
                dt = new DataTable();
                ds = new DataSet();

               // ds = MasterAdminBAL.usecasedata("ALL", 0, "");
                using (BaUseCase oBMC = new BaUseCase())
                {
                    dt = oBMC.Get<Int32>("ALL", 0, "", ref errMsg, "2019", 1);

                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        vString += "<div class='col-xl-4 col-lg-4 col-md-6 col-sm-6 col-12'><div class='single_feature text-center'><div class='feature_img'><img src='" + ConfigurationManager.AppSettings["USE_CASES"].ToString() + Convert.ToString(dt.Rows[i]["USE_CASES_IMAGE"]) + "' alt='veridocklockbook' class='img-fluid' width='103' height='133' title='featured-img'></div><div class='featured_content'><h4>" + Convert.ToString(dt.Rows[i]["USE_CASES_HEADING"]) + "</h4><p>" + Convert.ToString(dt.Rows[i]["USE_CASES_DESC"]) + "</p></div></div></div>";

                    }
                    ViewBag.UseCase = vString;

                }

            }
            catch (Exception ex)
            {
                // return ex.Message;
            }
            return ds;

        }

        private DataTable FillMastPackageGrid()
        {

            try
            {
                errMsg = String.Empty;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                dt = new DataTable();
                using (BaPricing oBAD = new BaPricing())
                {
                    dt = oBAD.Get<Int32>("GET", 0, "", ref errMsg, "2019", 1);
                }

                    if (dt != null && dt.Rows.Count > 0)
                {
                    ViewBag.PACKAGE_NAME_ONE = Convert.ToString(dt.Rows[0]["PACKAGE_NAME"]);
                    ViewBag.PACKAGE_DESC_ONE = Convert.ToString(dt.Rows[0]["PACKAGE_DESC"]);
                    ViewBag.PACKAGE_AMOUNT_ONE = Convert.ToString(dt.Rows[0]["PACKAGE_AMOUNT"]);
                    ViewBag.MONTHLY_PACKAGE_ONE = Convert.ToString(dt.Rows[0]["MONTHLY_PACKAGE"]);
                    ViewBag.BuyNowLinkONE = ViewBag.PACKAGE_NAME_ONE + "-" + ViewBag.PACKAGE_AMOUNT_ONE; 

                    ViewBag.PACKAGE_NAME_TWO = Convert.ToString(dt.Rows[1]["PACKAGE_NAME"]);
                    ViewBag.PACKAGE_DESC_TWO = Convert.ToString(dt.Rows[1]["PACKAGE_DESC"]);
                    ViewBag.PACKAGE_AMOUNT_TWO = Convert.ToString(dt.Rows[1]["PACKAGE_AMOUNT"]);
                    ViewBag.MONTHLY_PACKAGE_TWO = Convert.ToString(dt.Rows[1]["MONTHLY_PACKAGE"]);
                    ViewBag.BuyNowLinkTWO = ViewBag.PACKAGE_NAME_TWO + "-" + ViewBag.PACKAGE_AMOUNT_TWO;

                    ViewBag.PACKAGE_NAME_THREE = Convert.ToString(dt.Rows[2]["PACKAGE_NAME"]);
                    ViewBag.PACKAGE_DESC_THREE = Convert.ToString(dt.Rows[2]["PACKAGE_DESC"]);
                    ViewBag.PACKAGE_AMOUNT_THREE = Convert.ToString(dt.Rows[2]["PACKAGE_AMOUNT"]);
                    ViewBag.MONTHLY_PACKAGE_THREE = Convert.ToString(dt.Rows[2]["MONTHLY_PACKAGE"]);
                    ViewBag.BuyNowLinkTHREE = ViewBag.PACKAGE_NAME_THREE + "-" + ViewBag.PACKAGE_AMOUNT_THREE;
                }

            }
            catch (Exception ex)
            {
                //return ex.Message;
            }

            return dt;
        }

        public ActionResult btn_Contact_click(FormCollection form)
        {
            try
            {
                string to_username = ConfigurationManager.AppSettings["to_username"].ToString();
                string form_username = ConfigurationManager.AppSettings["form_username"].ToString();
                string form_password = ConfigurationManager.AppSettings["form_password"].ToString();
                string smtpAddress = "smtp.gmail.com"; //103.21.58.247
                int portNumber = 587;
                bool enableSSL = true; //false

                using (MailMessage mail = new MailMessage())
                {

                    //ValidEmail(txt_mail.Value);
                    if (form["txt_Name"] != "" && form["txt_Email"] != "" && form["txtMessage"] != "")
                    {
                        string body = string.Empty;
                        using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/contactmail.html")))
                        {
                            body = reader.ReadToEnd();
                            body = body.Replace("FULLNAME", form["txt_Name"].ToString());
                            body = body.Replace("EMAILID", form["txt_Email"]);
                            body = body.Replace("PHONE", "N/A");
                            body = body.Replace("MESSAGE", form["txtMessage"]);
                        }

                        mail.From = new MailAddress(form_username);
                        mail.To.Add(to_username);
                        mail.Subject = "New appointment query from " + form["txt_Name"] + " for VeriDoc Certificates";
                        mail.Body = body.ToString();
                        mail.IsBodyHtml = true;

                        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential(form_username, Encrypted.Decryptdata(form_password));
                            smtp.EnableSsl = enableSSL;
                            smtp.Send(mail);
                        }
                    }
                }
                using (MailMessage mail = new MailMessage())
                {
                    string body = string.Empty;
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/contactreply.html")))
                    {
                        body = reader.ReadToEnd();
                        body = body.Replace("FULLNAME", form["txt_Name"].ToString());

                    }
                    mail.From = new MailAddress(form_username);
                    mail.To.Add(form["txt_Email"]);
                    mail.Subject = "Thank you for contacting on VeriDoc Certificates";
                    mail.Body = body.ToString();
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(form_username, Encrypted.Decryptdata(form_password));
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            catch (Exception ex)
            {
                // ClientScript.RegisterStartupScript(GetType(), "popupservererror", "popupservererror(); console.log('" + ex.Message + "');", true);
            }

            TempData["JavaScriptFunction"] = string.Format("popup();");
            return Redirect("~/Customer/Index");

        }


        [HttpPost]
        public ActionResult btn_Submit_click(FormCollection form)
        {
            try
            {
                string to_username = ConfigurationManager.AppSettings["to_username"].ToString();
                string form_username = ConfigurationManager.AppSettings["form_username"].ToString();
                string form_password = ConfigurationManager.AppSettings["form_password"].ToString();
                string smtpAddress = "smtp.gmail.com"; //103.21.58.247
                int portNumber = 587;
                bool enableSSL = true; //false

                using (MailMessage mail = new MailMessage())
                {

                    //ValidEmail(txt_mail.Value);
                    if (form["txt_name1"] != "" && form["txtmail"] != "" && form["txt_msg"] != "")
                    {
                        string body = string.Empty;
                        using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/contactmail.html")))
                        {
                            body = reader.ReadToEnd();
                            body = body.Replace("FULLNAME", form["txt_name1"].ToString());
                            body = body.Replace("EMAILID", form["txtmail"]);
                            body = body.Replace("PHONE", "N/A");
                            body = body.Replace("MESSAGE", form["txt_msg"]);
                        }

                        mail.From = new MailAddress(form_username);
                        mail.To.Add(to_username);
                        mail.Subject = "New call appointment from " + form["txt_name1"] + " for VeriDoc Certificates";
                        mail.Body = body.ToString();
                        mail.IsBodyHtml = true;

                        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential(form_username, Encrypted.Decryptdata(form_password));
                            smtp.EnableSsl = enableSSL;
                            smtp.Send(mail);
                        }
                    }
                }
                using (MailMessage mail = new MailMessage())
                {
                    string body = string.Empty;
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/contactreply.html")))
                    {
                        body = reader.ReadToEnd();
                        body = body.Replace("FULLNAME", form["txt_name1"].ToString());

                    }
                    mail.From = new MailAddress(form_username);
                    mail.To.Add(form["txtmail"]);
                    mail.Subject = "Thank you for contacting on VeriDoc Certificates. Our Team will reach to you soon.";
                    mail.Body = body.ToString();
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(form_username, Encrypted.Decryptdata(form_password));
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                // ClientScript.RegisterStartupScript(GetType(), "popupservererror", "popupservererror(); console.log('" + ex.Message + "');", true);
            }
            TempData["JavaScriptFunction"] = string.Format("popup();");
            return Redirect("~/Customer/Index");
        }

    }
}