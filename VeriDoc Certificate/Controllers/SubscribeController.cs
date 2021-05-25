using MyApp.db.Encryption;
using MyApp.db.MyAppBal;
using MyApp.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace VeriDoc_Certificate.Controllers
{
    public class SubscribeController : Controller
    {
        // GET: Subscribe
        DataTable dt;
        String errMsg = String.Empty;
        string[] planname;
        public ActionResult Plan(string id)
        {
           if(id != null)
            {

            
            planname = id.Split('-');
            ViewBag.SelectedProductId = id != null ? id.Split('-')[1] : 1.ToString();
            ViewBag.PlanName = id != null ? id.Split('-')[0] : "";
            ViewBag.PlanPrice = id != null ? id.Split('-')[1] : 1.ToString();
            FillSiteSettingEdit();
            FillDdPackage(Convert.ToInt32(planname[1]));
            FillDdCountry();
            FillDdCountryCountry();
            return View();
            }
            else
            {
                return RedirectToAction("Index", "Customer");
            }
        }


        private String FillDdCountry()
        {
            try
            {
                errMsg = String.Empty;

                using (BaCountry oBMC = new BaCountry())
                {
                    dt = oBMC.BindDdl(0, ref errMsg, null, 0);

                }
                List<EntityCountry> countryname = new List<EntityCountry>();
                if (dt.Rows.Count > 0)
                {
                    
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        EntityCountry oBmast = new EntityCountry();


                        oBmast.CountryISO = Convert.ToString(dt.Rows[i]["ISO"]);
                        oBmast.CountryName = Convert.ToString(dt.Rows[i]["NICENAME"]);
                        

                        countryname.Add(oBmast);

                    }

                    var getcountryname = countryname.ToList();

                    SelectList list = new SelectList(getcountryname, "CountryISO", "CountryName","AU");
                    ViewBag.Country = list;
                }
                return errMsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        private String FillDdCountryCountry()
        {
            try
            {
                errMsg = String.Empty;

                using (BaCountry oBMC = new BaCountry())
                {
                    dt = oBMC.BindDdl(0, ref errMsg, null, 0);

                }
                List<EntityCountry> countryname = new List<EntityCountry>();
                if (dt.Rows.Count > 0)
                {
                    
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        EntityCountry oBmast = new EntityCountry();

                        oBmast.CountryISO = Convert.ToString(dt.Rows[i]["ISO"]);
                        oBmast.CountryName = Convert.ToString(dt.Rows[i]["NICENAME"]) + " (+" + Convert.ToInt32(dt.Rows[i]["PHONECODE"]) + ")";
                        

                        countryname.Add(oBmast);

                    }

                    var getcountryname = countryname.ToList();

                    SelectList list = new SelectList(getcountryname, "CountryISO", "CountryName","AU");
                    ViewBag.Countrycode = list;
                }
                return errMsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        private String FillDdPackage(int planname)
        {
            try
            {
                errMsg = String.Empty;
                DataTable dt2 = new DataTable();
                using (BaPricing oBDTP = new BaPricing())
                {
                    dt2 = oBDTP.GetData("GET", "", ref errMsg, "2019", 1);
                }
                List<EntityPricing> page = new List<EntityPricing>();
                if (dt2.Rows.Count > 0)
                {
                    EntityPricing oBmast = new EntityPricing();
                    oBmast.DTLS_PACKAGE_KEY = 0;
                    oBmast.PACKAGE_DESC = "-- Select Your Plan --";
                    page.Add(oBmast);
                    DataTable dt3 = new DataTable();
                    using (BaPricing oBDTP = new BaPricing())
                    {
                        string PRICE = Convert.ToString(planname);
                        dt3 = oBDTP.GetData("GET_ID", PRICE, ref errMsg, "2019", 1);
                        TempData["packagekey"] = Convert.ToInt32(dt3.Rows[0]["DTLS_PACKAGE_KEY"]);
                        Session["package"] = dt3.Rows[0]["PACKAGE_NAME"].ToString();
                    }

                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {

                        oBmast = new EntityPricing();
                        oBmast.DTLS_PACKAGE_KEY = Convert.ToInt32(dt2.Rows[i]["PACKAGE_AMOUNT"]);
                        oBmast.PACKAGE_DESC = dt2.Rows[i]["PACKAGE_DESC"].ToString();
                        oBmast.PACKAGE_NAME = dt2.Rows[i]["PACKAGE_NAME"].ToString();
                        page.Add(oBmast);
                        Session["packagename"] = oBmast.PACKAGE_NAME;
                    }

                    var getpackage = page.ToList();
                    ViewBag.ddl_DTLS_PACKAGE_KEY = planname;
                    SelectList list = new SelectList(getpackage, "DTLS_PACKAGE_KEY", "PACKAGE_DESC", ViewBag.ddl_DTLS_PACKAGE_KEY);
                    ViewBag.Package = list;

                }
                return errMsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

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

        [HttpPost]
        public ActionResult btn_submit_Click(FormCollection form)
        {
            EntityRegister oBMST = null;
            Byte vRef = 0; Int32 vKey = 0;
            errMsg = String.Empty;
            DataTable dt4 = new DataTable();
            //Int32 pricingkey = Convert.ToInt32(Session["price"]);
            ViewBag.hf_CUSTOMER_ID = "1234";
            EntityPricing oBMSTP = new EntityPricing();
            try
            {
                oBMST = new EntityRegister();

                oBMST.DTLS_REGISTER_KEY = Convert.ToInt32(0);
                oBMST.FIRSTNAME = form["FistName"];
                oBMST.LASTNAME = form["LastName"];
                oBMST.EMAIL = form["EamilId"];
                oBMST.PHONE = form["PhoneNumber"];
                oBMST.COMPANY_NAME = form["CompanyName"];
                oBMST.COMPANY_ADDRESS = form["CompanyAddress"];
                oBMST.COUNTRY = form["Country1"];
                oBMST.STATE = form["CompanyState"];
                oBMST.CITY = form["CompanyCity"];
                oBMST.ZIP = form["CompanyZIP"];
                oBMST.DTLS_PACKAGE_KEY = Convert.ToInt32(form["ddl_DTLS_PACKAGE_KEY"]);
                //oBMST.DTLS_PACKAGE_KEY = 1;

                oBMST.ALTER_COMPANY_NAME = form["BillingCompanyName"];
                oBMST.ALTER_COMPANY_ADDRESS = form["BillingCompanyAddress"];
                oBMST.ALTER_COUNTRY = form["AlterCountry"];
                oBMST.ALTER_STATE = form["BillingCompanyState"];
                oBMST.ALTER_CITY = form["BillingCompanyCity"];
                oBMST.ALTER_ZIP = form["BillingCompanyZIPCode"];

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
                Session["ALTER_COUNTRY"] = oBMST.ALTER_COUNTRY;

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
                                        using (BaPricing oBDTP = new BaPricing())
                                        {

                                            dt4 = oBDTP.Get<Int32>("SRH_KEY", oBMST.DTLS_PACKAGE_KEY, "", ref errMsg, "2019", 1);
                                            oBMSTP.PACKAGE_NAME = Convert.ToString(dt4.Rows[0]["PACKAGE_NAME"]);
                                        }
                                        if (oBMSTP.PACKAGE_NAME == "FREE TRIAL")
                                        {
                                            Session["popup"] = "1";
                                            Response.Redirect("/Home/Index", false);

                                        }
                                        else if (oBMSTP.PACKAGE_NAME == "PRO")
                                        {
                                            Response.Redirect("#", false);

                                        }
                                        else if (oBMSTP.PACKAGE_NAME == "STANDARD")
                                        {
                                            Response.Redirect("#", false);

                                        }

                                        send(form["EamilId"].ToString());
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
                                if (form["Country1"] != "")
                                {
                                    vRef = oBDT.SaveChanges<Object, Int32>("INSERT", oBMST, null, ref vKey, ref errMsg, "2019", 1);
                                    if (vRef == 1)
                                    {
                                        //MessageBox(2, Message.msgSaveNew, "");
                                        //ClientScript.RegisterStartupScript(GetType(), "popup", "popup();", true);

                                        using (BaPricing oBDTP = new BaPricing())
                                        {

                                            dt4 = oBDTP.Get<Int32>("SRH_KEY", oBMST.DTLS_PACKAGE_KEY, "", ref errMsg, "2019", 1);
                                            oBMSTP.PACKAGE_NAME = Convert.ToString(dt4.Rows[0]["PACKAGE_NAME"]);
                                        }
                                        if (oBMSTP.PACKAGE_NAME == "FREE TRIAL")
                                        {
                                            Session["popup"] = "1";
                                            Response.Redirect("/Home/Index", false);

                                        }
                                        else if (oBMSTP.PACKAGE_NAME == "PRO")
                                        {

                                            Response.Redirect("/Home/Index", false);

                                        }
                                        else if (oBMSTP.PACKAGE_NAME == "STANDARD")
                                        {

                                            Response.Redirect("/Home/Index", false);

                                        }

                                        send(form["EamilId"].ToString());
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
            }
            catch (Exception ex)
            {
                // //ClientScript.RegisterStartupScript(GetType(), "popupservererror", "popupservererror(); console.log('" + ex.Message + "');", true);


            }

            return Redirect("~/Subscribe/Plan");

        }

        protected void send(string recepientEmail)
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
                    string template = string.Empty;
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/main-assets/mailtemplate.html")))
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
                        template = template.Replace("PRICINGPLAN", Session["packagename"].ToString());
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
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/main-assets/regmailtemplate.html")))
                    {
                        templatebody = reader.ReadToEnd();
                        templatebody = templatebody.Replace("FULLNAME", Session["FIRSTNAME"] + " " + ViewBag.txt_LastName);
                        templatebody = templatebody.Replace("PRICINGPLAN", Session["packagename"].ToString());

                    }
                    mail1.From = new MailAddress(form_username);
                    mail1.To.Add(Session["EMAIL"].ToString());
                    mail1.Subject = "Thank you for registering on VeriDoc Certificates";
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
            }
            catch (Exception ex)
            {
                // ClientScript.RegisterStartupScript(GetType(), "popupservererror", "popupservererror(); console.log('" + ex.Message + "');", true);
            }
            TempData["JavaScriptFunction"] = string.Format("popup();");
            //return Redirect("~/Register/Index");
        }

    }
}