using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MyApp.db
{
    public static class ConstantVariables
    {
        #region User Validation Messages

        /// <summary>
        /// Invalid user name message
        /// </summary>
        public const string InvalidUserNameMessage = @"Invalid email address or password";

        /// <summary>
        /// Inactive user message
        /// </summary>
        public const string InactiveUserMessage = @"We are sorry, but your account has expired.";

        /// <summary>
        /// User block message
        /// </summary>
        public const string UserBlockedMesaage = @"Account has beed locked. Please click on forgot password to reset the password.";

        #endregion User Validation Messages

        #region Master Admin Email 

        /// <summary>
        /// Master Admin Recover password Email Body
        /// </summary>
        public const string MasterAdminRecoverPasswordEmailBody = @"You recently requested a change of password for your account at VeriDoc Certificates.<br/>To verify that this is correct and to change your password, please <a href='[RecoverLink]' target='_blank'>Click here</a>.<br/><br/>Regards,<br/>The Team at VeriDoc Certificates.";

        /// <summary>
        /// Password Recovery Subject
        /// </summary>
        public const string MasterAdminRecoverPasswordEmailSubject = @"VeriDoc Certificates Account Password recovery";

        /// <summary>
        /// From email address
        /// </summary>
        public const string MasterAdminFromEmail = @"admin@veridocglobal.com";

        #endregion Master Admin Email

        #region Master Admin Site
        public static readonly string MasterAdminSiteURL = ConfigurationManager.AppSettings["SiteURL"];
        public static readonly string MasterAdminEmailUsername = ConfigurationManager.AppSettings["EmailUsername"];
        public static readonly string MasterAdminEmailPassword = ConfigurationManager.AppSettings["EmailPassword"];
        #endregion Master Admin Site

        #region AWS Keys
        public static readonly string AWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        public static readonly string AWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
        #endregion AWS Keys

        #region Admin Email 

        /// <summary>
        /// Admin Recover password Email Body
        /// </summary>
        public const string AdminRecoverPasswordEmailBody = @"You recently requested a change of password for your account at VeriDoc Certificates.<br/>To verify that this is correct and to change your password, please <a href='[RecoverLink]' target='_blank'>Click here</a>.<br/><br/>Regards,<br/>The Team at VeriDoc Certificates.";

        /// <summary>
        /// Password Recovery Subject
        /// </summary>
        public const string AdminRecoverPasswordEmailSubject = @"VeriDoc Certificates Account Password recovery";

        /// <summary>
        /// From email address
        /// </summary>
        public const string AdminFromEmail = @"admin@veridocglobal.com";

        #endregion Admin Email

        #region Document Email 

        /// <summary>
        /// Admin Recover password Email Body
        /// </summary>
        public const string DocumentEmailBody = @"Hi,<br/><br/>[AdminName] has shared a VeriDoc Global secured document with you. Please visit the link below to view or download the document.<br/><br/>[DocumentURL]<br/><br/>Please Note:<br/><br/>1. The QR code embedded in the document can be scanned using any QR code scanner to verify the authenticity of this document using VeriDoc Global's blockchain verification protocol. Please visit our website to learn more: <a href='https://veridocglobal.com' target='_blank'>https://veridocglobal.com</a>.<br/><br/>2. If the document has been set to private and you don't have permission to view the document, please contact document issuer to arrange access for your email.<br/><br/>Regards,<br/>The Team at VeriDoc Certificates<br/><br/>Please note: The content of this email is confidential. If you have received it by mistake, please inform us by an email reply and follow with its deletion.";

        /// <summary>
        /// Password Recovery Subject
        /// </summary>
        public const string DocumentEmailSubject = @"A secured document has been shared with you";

        /// <summary>
        /// From email address
        /// </summary>
        public const string DocumentEmailFromEmail = @"admin@veridocglobal.com";

        #endregion Document Email

        #region Admin Site
        public static readonly string AdminSiteURL = ConfigurationManager.AppSettings["SiteURL"];

        public static readonly string SubmitFileURL = ConfigurationManager.AppSettings["BucketFilePath"];
        #endregion Admin Site

        #region Square Payment
        public static readonly bool UseSandboxAccount = Convert.ToBoolean(ConfigurationManager.AppSettings["UseSandboxAccount"]);
        public static readonly string SquareAccessToken = UseSandboxAccount ? ConfigurationManager.AppSettings["SquareAccessTokenSandbox"]
                                                            : ConfigurationManager.AppSettings["SquareAccessToken"];
        public static readonly string SquareLocationId = UseSandboxAccount ? ConfigurationManager.AppSettings["SquareLocationIdSandbox"]
                                                            : ConfigurationManager.AppSettings["SquareLocationId"];
        public static readonly string SquareCurrancy = ConfigurationManager.AppSettings["SquareCurrancy"];
        public static readonly string CurrancyConvertorAPI = ConfigurationManager.AppSettings["CurrancyConvertorAPI"];
        public static readonly string FreeCurrancyConvertorAPI = ConfigurationManager.AppSettings["FreeCurrancyConvertorAPI"];
        #endregion

        #region Master Admin Site
        public static readonly string VeridocAdminEmailId = ConfigurationManager.AppSettings["VeridocAdminEmailId"];
        public static readonly string SquareEmailId = ConfigurationManager.AppSettings["SquareEmailId"];
        #endregion Master Admin Site

        public static readonly string AdminSiteURLForCustomer = ConfigurationManager.AppSettings["AdminSiteURLForCustomer"];

        /// <summary>
        /// Customer Added Email Body
        /// </summary>
        public const string CustomerAddedEmailBody = @"<html><head><body>
                                                    <h3>Contact Details</h3><hr/>
                                                    <table style='font-family: arial, sans-serif; border-collapse: collapse;'>
                                                    <tr><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>First Name</td><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>[FirstName]</td></tr>
                                                    <tr><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Last Name</td><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>[LastName]</td></tr>
                                                    <tr><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Email</td><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>[EmailId]</td></tr>
                                                    <tr><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Phone Number</td><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>[PhoneNumber]</td></tr></table>
                                                    <br/><br/><h3>Company Details</h3><hr/>
                                                    <table style='font-family: arial, sans-serif; border-collapse: collapse;'>
                                                    <tr><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Company Name</td><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>[CompanyName]</td></tr>
                                                    <tr><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Company Address</td><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>[CompanyAddress]</td></tr>
                                                    <tr><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Country</td><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>[Country]</td></tr>
                                                    <tr><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>City</td><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>[City]</td></tr>
                                                    <tr><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>State</td><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>[State]</td></tr>
                                                    <tr><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Zip</td><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>[Zip]</td></tr></table>
                                                    <br/><br/><h3>Billing Details</h3><hr/>
                                                    <table style='font-family: arial, sans-serif; border-collapse: collapse;'>
                                                    <tr><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Company Name</td><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>[BillingCompanyName]</td></tr>
                                                    <tr><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Company Address</td><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>[BillingCompanyAddress]</td></tr>
                                                    <tr><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Country</td><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>[BillingCountry]</td></tr>
                                                    <tr><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>City</td><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>[BillingCity]</td></tr>
                                                    <tr><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>State</td><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>[BillingState]</td></tr>
                                                    <tr><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Zip</td><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>[BillingZip]</td></tr></table>
                                                    <br/><br/>Regards,<br/>The Team at VeriDoc Certificates.
                                                    </body></head></html>";

        /// <summary>
        /// Customer Added Email Subject
        /// </summary>
        public const string CustomerAddedEmailSubject = @"Customer Added.";

        /// <summary>
        /// User Email Body
        /// </summary>
        public const string EmailBodyToUser = @"<html><head><body>
                                                <table><tr><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>First Name</td><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>[FirstName]</td></tr>
                                                <tr><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Last Name</td><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>[LastName]</td></tr>
                                                <tr><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Email</td><td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>[EmailId]</td></tr></table>
                                                <br/><br/>Regards,<br/>The Team at VeriDoc Certificates.
                                                </body></head></html>";

        /// <summary>
        /// User Email Subject
        /// </summary>
        public const string EmailSubjectToUser = @"You are added in VeriDoc.";

        public static readonly bool SkipDuplicateEmailValidation = Convert.ToBoolean(ConfigurationManager.AppSettings["SkipDuplicateEmailValidation"]);

    }
}
