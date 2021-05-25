using System;
using System.Data;
using System.Web.Mvc;
using MyApp.db;
using MyApp.db.Encryption;
using MyApp.db.MyAppBal;
using MyApp.Entity;


namespace VeriDoc_Certificate.Controllers
{
    public class PricingController : Controller
    {
        EntitySysUser oSysUser = null;
        String errMsg = String.Empty;
        DataTable dt;
        DataSet ds;

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
                    dt = FillMastPackageGrid();
                    if (!String.IsNullOrEmpty(errMsg))
                    {
                        //  MessageBox(1, "Activity " + Message.msgErrDdlPop, errMsg);
                    }
                    ViewBag.JavaScriptFunction = string.Format("OpenTab1();");
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
                    //gvpricing.DataSource = dt;
                    //gvpricing.DataBind();
                    //MessageBox(1, "", "");
                    // return errMsg;
                }
            }
            catch (Exception ex)
            {
                //return ex.Message;
            }

            return dt;

        }

        [HttpPost]
        public ActionResult delete(FormCollection form)
        {
            Int32 vKey = 0; Byte vRef = 0; String vDelMsg = String.Empty;
            EntityPricing oBMAST = null;
            try
            {
                if (ModelState.IsValid)
                {
                    string edit = form[0];
                    errMsg = String.Empty;
                    oBMAST = new EntityPricing();
                    //GridViewRow gvr = (GridViewRow)((HtmlAnchor)sender).NamingContainer;
                    oBMAST.DTLS_PACKAGE_KEY = Convert.ToInt32(edit);
                    oBMAST.ENT_USER_KEY = Convert.ToInt32(Session["USER_KEY"]);
                    oBMAST.EDIT_USER_KEY = Convert.ToInt32(Session["USER_KEY"]);
                    oBMAST.TAG_DELETE = 0;

                    using (BaPricing oBMC = new BaPricing())
                    {
                        vRef = oBMC.SaveDelete<Object, Int32>("DELETE", oBMAST, null, ref vKey, ref errMsg, ref vDelMsg, "2019", 1);
                        if (vRef > 0)
                        {
                            if (vRef == 2)
                            {
                                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgDelete + "');");
                                //  MessageBox(1, "Data exists in We Do Master", errMsg);
                            }
                            else if (vRef == 1)
                            {

                                return Redirect("~/Pricing/Index");
                                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgSaveDelete + "');");
                                //MessageBox(1, Message.msgSaveDelete, "");
                                // FillMastBenefitGrid();
                            }
                            else

                            {
                                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgSaveErr + "');");
                                // MessageBox(2, Message.msgSaveErr, errMsg);
                            }

                        }
                    }
                }
                else
                {
                    //   MessageBox(2, Message.msgPageInvalid, "");
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (oBMAST != null)
                    oBMAST = null;
            }

            return Redirect("~/Pricing/Index");
        }

        [HttpPost]
        public ActionResult edit(FormCollection form)
        {
            try
            {
                string edit = form[0];
                errMsg = String.Empty;

                ViewBag.hf_DTLS_PACKAGE_KEY = edit.ToString();
                errMsg = FillMastPackageEdit(Convert.ToInt32(edit));
                FillMastPackageGrid();

                if (String.IsNullOrEmpty(errMsg))
                {
                    //  aPageName.InnerText = Message.modName21 + "(Edit)";
                    ViewBag.JavaScriptFunction = string.Format("OpenTab2();");
                }
                else
                {
                    TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgErrEditPop + "');");
                    //   MessageBox(1, Message.msgErrEditPop, errMsg);
                }

            }
            catch (Exception ex)
            {
                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgErrCommon + "');");
                //  MessageBox(1, Message.msgErrCommon, ex.Message);
            }

            return View("Index", dt.Rows);
        }

        private String FillMastPackageEdit(Int32 pDTLS_PACKAGE_KEY)
        {
            try
            {
                errMsg = String.Empty;
                dt = new DataTable();
                using (BaPricing oBMG = new BaPricing())
                {
                    dt = oBMG.Get<Int32>("SRH_KEY", pDTLS_PACKAGE_KEY, "", ref errMsg, "2019", 1);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewBag.txt_PACKAGE_NAME = Convert.ToString(dt.Rows[0]["PACKAGE_NAME"]);
                    ViewBag.txt_PACKAGE_DESC = Convert.ToString(dt.Rows[0]["PACKAGE_DESC"]);
                    ViewBag.txt_PACKAGE_AMOUNT = Convert.ToString(dt.Rows[0]["PACKAGE_AMOUNT"]);
                    ViewBag.txt_MONTHLY_PACKAGE = Convert.ToString(dt.Rows[0]["MONTHLY_PACKAGE"]);
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
        public ActionResult Add()
        {
            try
            {
                // errMsg = String.Empty;
                ViewBag.hf_DTLS_PACKAGE_KEY = "0";

                ViewBag.JavaScriptFunction = string.Format("OpenTab2();");
                dt = FillMastPackageGrid();
                //TempData["JavaScriptFunction"] = string.Format("OpenTab2();");
                //ClearControl();
                //MessageBox(2, "", "");
            }
            catch (Exception ex)
            {
                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgErrCommon + "');");
                //  MessageBox(1, Message.msgErrCommon, ex.Message);
            }

            return View("Index", dt.Rows);
        }

        [HttpPost]
        public ActionResult btn_Head_Save_Click(FormCollection form)
        {

            Byte vRef = 0; Int32 vKey = 0;
            String LABELS = String.Empty;
            EntityPricing oBMAST = null;
            try
            {
                if (ModelState.IsValid)
                {

                    errMsg = String.Empty;
                    oBMAST = new EntityPricing();
                    oBMAST.DTLS_PACKAGE_KEY = Convert.ToInt32(form["hf_DTLS_PACKAGE_KEY"]);

                    oBMAST.PACKAGE_NAME = form["txt_PACKAGE_NAME"];
                    oBMAST.PACKAGE_DESC = form["txt_PACKAGE_DESC"];
                    oBMAST.PACKAGE_AMOUNT = Convert.ToDouble(form["txt_PACKAGE_AMOUNT"]);
                    oBMAST.MONTHLY_PACKAGE = form["txt_MONTHLY_PACKAGE"];

                    oBMAST.ENT_USER_KEY = Convert.ToInt32(Session["USER_KEY"]);
                    oBMAST.EDIT_USER_KEY = Convert.ToInt32(Session["USER_KEY"]);
                    oBMAST.TAG_ACTIVE = 0;
                    oBMAST.TAG_DELETE = 0;
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");

                    using (BaPricing oBMC = new BaPricing())
                    {
                        if (oBMAST.DTLS_PACKAGE_KEY == 0)
                        {
                            vRef = oBMC.SaveChanges<Object, Int32>("INSERT", oBMAST, null, ref vKey, ref errMsg, "2019", 1);
                            if (vRef == 1)
                            {
                                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgSaveNew + "');");
                                //MessageBox(2, Message.msgSaveNew, "");
                                //errMsg = FillMastPackageGrid();
                                //  hf_DTLS_PACKAGE_KEY.Value = Convert.ToString(vKey);
                            }
                            else if (vRef == 2)
                            {
                                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgValidation + "');");
                                // MessageBox(2, Message.msgValidation, errMsg);
                            }

                            else
                            {
                                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgSaveErr + "');");
                                // MessageBox(2, Message.msgSaveErr, errMsg);
                            }

                        }
                        else
                        {
                            vRef = oBMC.SaveChanges<Object, Int32>("UPDATE", oBMAST, null, ref vKey, ref errMsg, "2019", 1);
                            if (vRef == 1)
                            {
                                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgSaveEdit + "');");
                                // MessageBox(2, Message.msgSaveEdit, "");

                            }
                            else if (vRef == 2)
                            {
                                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgSaveDuplicate + "');");
                                //MessageBox(2, Message.msgSaveDuplicate, errMsg);
                            }

                            else
                            {
                                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgSaveErr + "');");
                                // MessageBox(2, Message.msgSaveErr, errMsg);
                            }

                        }
                    }
                    // oSysUser.TAG_PAGE_REFRESH = Server.UrlEncode(System.DateTime.Now.ToString());
                }
                else
                {
                    TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgPageInvalid + "');");
                    //  MessageBox(2, Message.msgPageInvalid, "");
                }

            }
            catch (Exception ex)
            {
                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgErrCommon + "');");
                //  MessageBox(2, Message.msgErrCommon, ex.Message);
            }
            finally
            {
                if (oBMAST != null)
                    oBMAST = null;
            }
            return Redirect("~/Pricing/Index");
        }
    }
}