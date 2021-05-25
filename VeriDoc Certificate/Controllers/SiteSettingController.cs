using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.db;
using MyApp.db.Encryption;
using MyApp.db.MyAppBal;
using MyApp.Entity;


namespace VeriDoc_Certificate.Controllers
{
    public class SiteSettingController : Controller
    {
        // GET: SiteSetting
        EntitySysUser oSysUser = null;
        String errMsg = String.Empty;
        DataTable dt;
        DataSet ds;
        DataSet ds1;
        DataTable dt1;
        public ActionResult Index()
        {
            try
            {
                if (Session["oSysUser"] != null)
                {
                    oSysUser = (EntitySysUser)Session["oSysUser"];
                    oSysUser.USER_KEY = Convert.ToInt32(Session["USER_KEY"]);

                    oSysUser.TAG_PAGE_REFRESH = Server.UrlEncode(System.DateTime.Now.ToString());
                    errMsg = String.Empty;
                    FillSiteSettingEdit();
                    errMsg = FillDdPageName();
                    if (!String.IsNullOrEmpty(errMsg))
                    {
                        //  MessageBox(1, "Page Name" + Message.msgErrDdlPop, errMsg);
                    }
                    dt = FillPageSettingGrid();
                    ViewBag.hf_DTLS_PAGE_SETTING_KEY = "0";
                    ds1 = FillTechnologyGrid();
                    ViewBag.hf_MAST_TECHNOLOGY_KEY = "0";
                }
                else
                {

                    return Redirect("~/Admin/Index");
                }
            }
            catch (Exception ex)
            {
                // MessageBox(1, Message.msgErrCommon, ex.Message);
            }
            return View(dt.Rows);
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

        private String FillDdPageName()
        {
            try
            {
                errMsg = String.Empty;

                using (BaSiteSetting oBMC = new BaSiteSetting())
                {
                    dt = oBMC.BindDdl(0, ref errMsg, null, 0);

                }
                List<EntitySiteSetting> page = new List<EntitySiteSetting>();
                if (dt.Rows.Count > 0)
                {
                    EntitySiteSetting oBmast = new EntitySiteSetting();
                    oBmast.MAST_PAGE_KEY = 0;
                    oBmast.PAGE_NAME = "-SELECT AN OPTION-";
                    page.Add(oBmast);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        oBmast = new EntitySiteSetting();
                        oBmast.MAST_PAGE_KEY = Convert.ToInt32(dt.Rows[i]["MAST_PAGE_KEY"]);
                        oBmast.PAGE_NAME = dt.Rows[i]["PAGE_NAME"].ToString();

                        page.Add(oBmast);

                    }

                    var getpagename = page.ToList();

                    SelectList list = new SelectList(getpagename, "MAST_PAGE_KEY", "PAGE_NAME", ViewBag.ddl_MAST_PAGE_KEY);
                    ViewBag.PageName = list;
                }
                return errMsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private DataTable FillPageSettingGrid()
        {
            try
            {
                errMsg = String.Empty;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                dt = new DataTable();
             
                using (BaSiteSetting oBMC = new BaSiteSetting())
                {
                    dt = oBMC.GetPageSetting<Int32>("GET", 0, "", ref errMsg, "2019", 1);
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                }

            }
            catch (Exception ex)
            {
                // return ex.Message;
            }
            return dt;

        }

        [HttpPost]
        public ActionResult edit(FormCollection form)
        {
            string edit = form[0];

            try
            {
                EntitySiteSetting oBMAST = null;
                errMsg = String.Empty;
                oBMAST = new EntitySiteSetting();
                ViewBag.hf_DTLS_PAGE_SETTING_KEY = edit;

                errMsg = FillPageSettingEdit(Convert.ToInt32(edit));
                if (String.IsNullOrEmpty(errMsg))
                {
                    errMsg = FillTechnologyEdit(Convert.ToInt32(edit));
                    if (String.IsNullOrEmpty(errMsg))
                    {
                        FillSiteSettingEdit();
                        errMsg = FillDdPageName();
                        if (!String.IsNullOrEmpty(errMsg))
                        {
                            TempData["JavaScriptFunction"] = string.Format("ShowSuccess();");
                        }
                        dt = FillPageSettingGrid();
                        FillTechnologyGrid();
                    }

                }
                else
                {
                    TempData["JavaScriptFunction"] = string.Format("ShowErrMsg();");
                }

            }
            catch (Exception ex)
            {
                TempData["JavaScriptFunction"] = string.Format("ShowErrMsg();");
            }
            return View("Index", dt.Rows);
        }

        private String FillPageSettingEdit(Int32 pDTLS_PAGE_SETTING_KEY)
        {
            try
            {
                errMsg = String.Empty;
               
                dt = new DataTable();
                using (BaSiteSetting oBMC = new BaSiteSetting())
                {
                    dt = oBMC.GetPageSetting<Int32>("SRH_KEY", pDTLS_PAGE_SETTING_KEY, "", ref errMsg, "2019", 1);
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewBag.ddl_MAST_PAGE_KEY = Convert.ToString(dt.Rows[0]["MAST_PAGE_KEY"]);
                    ViewBag.txt_PAGE_TITLE = Convert.ToString(dt.Rows[0]["PAGE_TITLE"]);
                    ViewBag.txt_META_DESCRIPTION = Convert.ToString(dt.Rows[0]["META_DESCRIPTION"]);
                    ViewBag.txt_META_KEYWORD = Convert.ToString(dt.Rows[0]["META_KEYWORD"]);
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
        public ActionResult btn_Page_Settings_Save_Click(FormCollection form)
        {
            Int32 vRef = 0; Int32 vKey = 0;
            EntitySiteSetting oBMAST = null;
            try
            {
                if (ModelState.IsValid)
                {
                    errMsg = String.Empty;
                    oBMAST = new EntitySiteSetting();
                    oBMAST.DTLS_PAGE_SETTING_KEY = Convert.ToInt32(form["hf_DTLS_PAGE_SETTING_KEY"]);
                    oBMAST.MAST_PAGE_KEY = Convert.ToInt32(form["ddl_MAST_PAGE_KEY"]);
                    oBMAST.PAGE_TITLE = form["txt_PAGE_TITLE"];
                    oBMAST.META_DESCRIPTION = form["txt_META_DESCRIPTION"];
                    oBMAST.META_KEYWORD = form["txt_META_KEYWORD"];

                    oBMAST.ENT_USER_KEY = Convert.ToInt32(Session["USER_KEY"]);
                    oBMAST.EDIT_USER_KEY = Convert.ToInt32(Session["USER_KEY"]);
                    oBMAST.TAG_ACTIVE = 0;
                    oBMAST.TAG_DELETE = 0;
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");

                    using (BaSiteSetting oBMC = new BaSiteSetting())
                    {
                        if (oBMAST.DTLS_PAGE_SETTING_KEY == 0)
                        {
                            vRef = oBMC.SavePageSetting<Object, Int32>("INSERT", oBMAST, null, ref vKey, ref errMsg, "2019", 1);
                            if (vRef == 1)
                            {
                                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgSaveNew + "');");
                                //MessageBox(2, Message.msgSaveNew, "");
                                //FillPageSettingGrid();
                                //ClearPageSetting();
                            }
                            else if (vRef == 2)
                            {
                                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgSaveDuplicate + "');");
                                //  MessageBox(2, Message.msgSaveDuplicate, errMsg);
                            }

                            else
                            {
                                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgSaveErr + "');");
                                // MessageBox(2, Message.msgSaveErr, errMsg);
                            }

                        }
                        else
                        {
                            vRef = oBMC.SavePageSetting<Object, Int32>("UPDATE", oBMAST, null, ref vKey, ref errMsg, "2019", 1);
                            if (vRef == 1)
                            {
                                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgSaveEdit + "');");
                                //  FillPageSettingGrid();
                                //ClearPageSetting();
                                //MessageBox(1, Message.msgSaveEdit, "");
                            }
                            else if (vRef == 2)
                            {
                                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgSaveDuplicate + "');");
                                //  MessageBox(1, Message.msgSaveDuplicate, errMsg);
                            }

                            else
                            {
                                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgSaveErr + "');");
                                //  MessageBox(1, Message.msgSaveErr, errMsg);
                            }

                        }
                    }

                    //  oSysUser.TAG_PAGE_REFRESH = Server.UrlEncode(System.DateTime.Now.ToString());
                }
                else
                {
                    TempData["JavaScriptFunction"] = string.Format("ShowErrMsg();");
                }

            }
            catch (Exception ex)
            {
                TempData["JavaScriptFunction"] = string.Format("ShowErrMsg();");
            }
            finally
            {
                if (oBMAST != null)
                    oBMAST = null;
            }

            return Redirect("~/SiteSetting/Index");

        }

        private String ProcessBanner(ref String Doc_Name, HttpPostedFileBase file)
        {
            if (file == null)
            {
                ViewBag.hf_BANNER_IMAGE = Request["hf_BANNER_IMAGE"];
                return String.Empty;
            }
            else
            {
                String[] ImageAcceptedExtensions = null;
                String DOC_PATH = "~" + ConfigurationManager.AppSettings["HOME"].ToString();
                try
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".webp", ".svg" };
                    var Image_url = file.ToString(); //getting complete url
                    var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                    var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = System.DateTime.Now.ToString("yyyyMMdd_hhmmss") + ext; //appending the name with id  
                                                                                               // store the file inside ~/project folder(Img)  
                        var path = Server.MapPath(DOC_PATH + myfile);
                        Image_url = path;

                        file.SaveAs(path);

                        ViewBag.hf_BANNER_IMAGE = myfile;
                    }
                    else
                    {
                        ViewBag.message = "Please choose only Image file";
                    }


                    return String.Empty;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    ImageAcceptedExtensions = null;
                }
            }
        }

        private String ProcessLogo(ref String Doc_Name, HttpPostedFileBase file)
        {
            if (file == null)
            {
                ViewBag.hf_LOGO_NAME = Request["hf_LOGO_NAME"];
                return String.Empty;
            }
            else
            {
                String[] ImageAcceptedExtensions = null;
                String DOC_PATH = "~" + ConfigurationManager.AppSettings["HOME"].ToString();
                try
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".webp", ".svg" };
                    var Image_url = file.ToString(); //getting complete url
                    var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                    var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = System.DateTime.Now.ToString("yyyyMMdd_hhmmss") + ext; //appending the name with id  
                                                                                               // store the file inside ~/project folder(Img)  
                        var path = Server.MapPath(DOC_PATH + myfile);
                        Image_url = path;

                        file.SaveAs(path);

                        ViewBag.hf_LOGO_NAME = myfile;
                    }
                    else
                    {
                        ViewBag.message = "Please choose only Image file";
                    }


                    return String.Empty;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    ImageAcceptedExtensions = null;
                }
            }
        }

        private String ProcessTechnology(ref String Doc_Name, HttpPostedFileBase file)
        {
            if (file == null)
            {
                ViewBag.hf_TECHNOLOGY_IMAGE = Request["hf_TECHNOLOGY_IMAGE"];
                return String.Empty;
            }
            else
            {
                String[] ImageAcceptedExtensions = null;
                String DOC_PATH = "~" + ConfigurationManager.AppSettings["HOME"].ToString();
                try
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".webp", ".svg" };
                    var Image_url = file.ToString(); //getting complete url
                    var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                    var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = System.DateTime.Now.ToString("yyyyMMdd_hhmmss") + ext; //appending the name with id  
                                                                                               // store the file inside ~/project folder(Img)  
                        var path = Server.MapPath(DOC_PATH + myfile);
                        Image_url = path;

                        file.SaveAs(path);

                        ViewBag.hf_TECHNOLOGY_IMAGE = myfile;
                    }
                    else
                    {
                        ViewBag.message = "Please choose only Image file";
                    }


                    return String.Empty;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    ImageAcceptedExtensions = null;
                }
            }
        }

        public ActionResult btn_Head_Save_Click(FormCollection form, HttpPostedFileBase fu_Banner, HttpPostedFileBase fu_Logo, HttpPostedFileBase fu_TECHNOLOGY)
        {
            Int32 vRef = 0; Int32 vKey = 0;
            EntitySiteSetting oBMAST = null;
            String LOGO_NAME = String.Empty;
            String BANNER = String.Empty;
            String TECHNOLOGY_IMAGE = String.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    errMsg = ProcessBanner(ref BANNER, fu_Banner);
                    if (String.IsNullOrEmpty(errMsg))
                    {
                        errMsg = ProcessLogo(ref LOGO_NAME, fu_Logo);
                        if (String.IsNullOrEmpty(errMsg))
                        {
                            errMsg = ProcessTechnology(ref TECHNOLOGY_IMAGE, fu_TECHNOLOGY);
                            if (!String.IsNullOrEmpty(errMsg))
                            {
                                
                            }
                        }

                    }
                    errMsg = String.Empty;
                    oBMAST = new EntitySiteSetting();
                    oBMAST.BANNER_IMAGE = ViewBag.hf_BANNER_IMAGE;
                    oBMAST.BANNER_HEADING_1 = form["txt_BANNER_HEADING_1"];
                    oBMAST.BANNER_HEADING_2 = form["txt_BANNER_HEADING_2"];
                    oBMAST.BANNER_HEADING_3 = form["txt_BANNER_HEADING_3"];
                    oBMAST.CONTACT_NO = form["txt_CONTACT_NO"];
                    oBMAST.MAIL = form["txt_MAIL"];
                    oBMAST.ADDRESS = form["txt_ADDRESS"];

                    oBMAST.FACEBOOK_LINK = form["txt_FACEBOOK_LINK"];
                    oBMAST.TWITTER_LINK = form["txt_TWITTER_LINK"];
                    oBMAST.LINKEDIN_LINK = form["txt_LINKEDIN_LINK"];
                    oBMAST.INSTAGRAM_LINK = form["txt_INSTAGRAM_LINK"];
                    oBMAST.TELEGRAM_LINK = form["txt_TELEGRAM_LINK"];
                    oBMAST.PRINTEREST_LINK = form["txt_PRINTEREST_LINK"];
                    oBMAST.MEDIUM_LINK = form["txt_MEDIUM_LINK"];

                    oBMAST.FULL_ADDRESS = form["txt_FULL_ADDRESS"];
                    oBMAST.TECHNOLOGY_IMAGE = ViewBag.hf_TECHNOLOGY_IMAGE;
                    oBMAST.LOGO_NAME = ViewBag.hf_LOGO_NAME;

                    oBMAST.ENT_USER_KEY = Convert.ToInt32(Session["USER_KEY"]);
                    oBMAST.EDIT_USER_KEY = Convert.ToInt32(Session["USER_KEY"]);
                    oBMAST.TAG_ACTIVE = 1;
                    oBMAST.TAG_DELETE = 0;
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");



                    using (BaSiteSetting oBMC = new BaSiteSetting())
                    {
                        vRef = oBMC.SaveChanges<Object, Int32>("UPDATE", oBMAST, null, ref vKey, ref errMsg, "2019", 1);
                        if (vRef == 1)
                        {
                            TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgSaveEdit + "');");
                            //MessageBox(2, Message.msgSaveEdit, "");
                            //FillSiteSettingEdit();
                        }
                        else if (vRef == 2)
                        {
                            TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgSaveDuplicate + "');");
                            //  MessageBox(2, Message.msgSaveDuplicate, errMsg);
                        }

                        else
                        {
                            TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgSaveErr + "');");
                            //   MessageBox(2, Message.msgSaveErr, errMsg);
                        }

                    }


                }
                // oSysUser.TAG_PAGE_REFRESH = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                TempData["JavaScriptFunction"] = string.Format("ShowErrMsg();");
            }
            finally
            {
                if (oBMAST != null)
                    oBMAST = null;
            }

            return Redirect("~/SiteSetting/Index");
        }

        #region Technology Section
        [HttpPost]
        private void ClearTechnology()
        {
            ViewBag.txt_TECHNOLOGY_DESC = "";
            ViewBag.hf_MAST_TECHNOLOGY_KEY.Value = "0";
        }

        public ActionResult btn_Technology_Save_Click(FormCollection form)
        {
            Int32 vRef = 0; Int32 vKey = 0;
            EntitySiteSetting oBMAST = null;
            try
            {
                if (ModelState.IsValid)
                {
                    errMsg = String.Empty;
                    oBMAST = new EntitySiteSetting();
                    oBMAST.MAST_TECHNOLOGY_KEY = Convert.ToInt32(form["hf_MAST_TECHNOLOGY_KEY"]);
                    oBMAST.TECHNOLOGY_DESC = form["txt_TECHNOLOGY_DESC"];

                    oBMAST.ENT_USER_KEY = Convert.ToInt32(Session["USER_KEY"]);
                    oBMAST.EDIT_USER_KEY = Convert.ToInt32(Session["USER_KEY"]);
                    oBMAST.TAG_ACTIVE = 0;
                    oBMAST.TAG_DELETE = 0;
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                    using (BaSiteSetting oBMC = new BaSiteSetting())
                    {
                        if (oBMAST.MAST_TECHNOLOGY_KEY == 0)
                        {
                            vRef = oBMC.SaveTechnology<Object, Int32>("INSERT", oBMAST, null, ref vKey, ref errMsg, "2019", 1);
                            if (vRef == 1)
                            {
                                TempData["JavaScriptFunction"] = string.Format("ShowSuccess();");
                            }
                            else if (vRef == 2)
                            {
                                TempData["JavaScriptFunction"] = string.Format("ShowErrMsg();");
                            }

                            else
                            {
                                TempData["JavaScriptFunction"] = string.Format("ShowErrMsg();");
                            }

                        }
                        else
                        {
                            vRef = oBMC.SaveTechnology<Object, Int32>("INSERT", oBMAST, null, ref vKey, ref errMsg, "2019", 1);
                            if (vRef == 1)
                            {
                                TempData["JavaScriptFunction"] = string.Format("ShowSuccess();");
                            }
                            else if (vRef == 2)
                            {
                                TempData["JavaScriptFunction"] = string.Format("ShowErrMsg();");
                            }
                            else
                            {
                                TempData["JavaScriptFunction"] = string.Format("ShowErrMsg();");
                            }

                        }
                    }
                    //  oSysUser.TAG_PAGE_REFRESH = Server.UrlEncode(System.DateTime.Now.ToString());
                }
                else
                {
                    TempData["JavaScriptFunction"] = string.Format("ShowErrMsg();");
                }

            }
            catch (Exception ex)
            {
                TempData["JavaScriptFunction"] = string.Format("ShowErrMsg();");
            }
            finally
            {
                if (oBMAST != null)
                    oBMAST = null;
            }

            return Redirect("~/SiteSetting/Index");

        }

        private DataSet FillTechnologyGrid()
        {
            try
            {
                errMsg = String.Empty;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                dt1 = new DataTable();
                using (BaSiteSetting oBMC = new BaSiteSetting())
                {
                    dt1 = oBMC.GetTechnology("GET",0,"", ref errMsg, "2019", 1);
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                }
               
                TempData["ds"] = dt1;

            }
            catch (Exception ex)
            {
                // return ex.Message;
            }
            return ds;

        }

        private String FillTechnologyEdit(Int32 pMAST_TECHNOLOGY_KEY)
        {
            try
            {
                errMsg = String.Empty;
                dt1 = new DataTable();
                ds1 = new DataSet();
                ViewBag.hf_MAST_TECHNOLOGY_KEY = pMAST_TECHNOLOGY_KEY;
                using (BaSiteSetting oBMC = new BaSiteSetting())
                {
                    dt1 = oBMC.GetTechnology("SRH_KEY", pMAST_TECHNOLOGY_KEY, "", ref errMsg, "2019", 1);
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                }

                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    ViewBag.txt_TECHNOLOGY_DESC = Convert.ToString(dt1.Rows[0]["TECHNOLOGY_DESC"]);
                }

                return errMsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                dt1 = null;
            }
        }
        #endregion
    }
}