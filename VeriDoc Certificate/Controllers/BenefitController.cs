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
    public class BenefitController : Controller
    {
        // GET: Benefit
        // GET: UseCase
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
                    //if (Request.QueryString["type"] != null && Request.QueryString["type"].ToString() == "1")
                    //{

                    //}
                    dt = FillMastBenefitGrid();
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
        private DataTable FillMastBenefitGrid()
        {
            try
            {
                errMsg = String.Empty;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                dt = new DataTable();
                using (BaBenefits oBMC = new BaBenefits())
                {
                    dt = oBMC.Get<Int32>("GET", 0, "", ref errMsg, "2019", 1);
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
        public ActionResult delete(FormCollection form)
        {
            Int32 vKey = 0; Byte vRef = 0; String vDelMsg = String.Empty;
            EntityBenefit oBMAST = null;
            try
            {
                if (ModelState.IsValid)
                {
                    string edit = form[0];
                    errMsg = String.Empty;
                    oBMAST = new EntityBenefit();
                    //GridViewRow gvr = (GridViewRow)((HtmlAnchor)sender).NamingContainer;
                    oBMAST.MAST_BENEFITS_KEY = Convert.ToInt32(edit);
                    oBMAST.ENT_USER_KEY = Convert.ToInt32(Session["USER_KEY"]);
                    oBMAST.EDIT_USER_KEY = Convert.ToInt32(Session["USER_KEY"]);
                    oBMAST.TAG_DELETE = 0;

                    using (BaBenefits oBMC = new BaBenefits())
                    {
                        vRef = oBMC.SaveDelete<Object, Int32>("DELETE", oBMAST, null, ref vKey, ref errMsg, ref vDelMsg, "2019", 1);
                        if (vRef > 0)
                        {
                            if (vRef == 2)
                            {
                                //  MessageBox(1, "Data exists in We Do Master", errMsg);
                            }
                            else if (vRef == 1)
                            {

                                return Redirect("~/Benefit/Index");
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
                    TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgPageInvalid + "');");
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

            return Redirect("~/Benefit/Index");
        }

        [HttpPost]
        public ActionResult edit(FormCollection form)
        {
            try
            {
                string edit = form[0];
                errMsg = String.Empty;

                ViewBag.hf_MAST_BENEFITS_KEY = edit.ToString();
                errMsg = FillMastBenefitEdit(Convert.ToInt32(edit));
                FillMastBenefitGrid();

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

        private String FillMastBenefitEdit(Int32 pMAST_BENEFITS_KEY)
        {
            try
            {
                errMsg = String.Empty;
                dt = new DataTable();
                using (BaBenefits oBMG = new BaBenefits())
                {
                    dt = oBMG.Get<Int32>("SRH_KEY", pMAST_BENEFITS_KEY, "", ref errMsg, "2019", 1);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewBag.txt_HEADING = Convert.ToString(dt.Rows[0]["BENEFITS_HEADING"]);
                    ViewBag.txt_DESCRIPTION = Convert.ToString(dt.Rows[0]["BENEFITS_DESC"]);
                    ViewBag.img_benefit = ConfigurationManager.AppSettings["BENEFITS"].ToString() + Convert.ToString(dt.Rows[0]["BENEFITS_IMAGE"]);
                    ViewBag.hf_BENEFITS_IMG = Convert.ToString(dt.Rows[0]["BENEFITS_IMAGE"]);
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
                ViewBag.hf_MAST_BENEFITS_KEY = "0";

                ViewBag.JavaScriptFunction = string.Format("OpenTab2();");
                dt = FillMastBenefitGrid();
                ViewBag.img_benefit = "~/Content/assets/images/no_image.jpg";
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


        public ActionResult btn_Head_Save_Click(FormCollection form, HttpPostedFileBase fu_benefit)
        {
            Byte vRef = 0; Int32 vKey = 0;
            String LABELS = String.Empty;
            EntityBenefit oBMAST = null;
            try
            {
                if (ModelState.IsValid)
                {
                    errMsg = ProcessImage(ref LABELS, fu_benefit);
                    if (!String.IsNullOrEmpty(errMsg))
                    {
                        //MessageBox(3, errMsg, "");
                        //return;
                    }
                    errMsg = String.Empty;
                    oBMAST = new EntityBenefit();
                    oBMAST.MAST_BENEFITS_KEY = Convert.ToInt32(form["hf_MAST_BENEFITS_KEY"]);

                    oBMAST.HEADING = form["txt_HEADING"];
                    oBMAST.DESCRIPTION = form["txt_DESCRIPTION"];
                    oBMAST.BENEFITS_IMG = ViewBag.hf_BENEFITS_IMG;

                    oBMAST.ENT_USER_KEY = Convert.ToInt32(Session["USER_KEY"]);
                    oBMAST.EDIT_USER_KEY = Convert.ToInt32(Session["USER_KEY"]);
                    oBMAST.TAG_ACTIVE = 0;
                    oBMAST.TAG_DELETE = 0;
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");

                    using (BaBenefits oBMC = new BaBenefits())
                    {
                        if (oBMAST.MAST_BENEFITS_KEY == 0)
                        {
                            vRef = oBMC.SaveChanges<Object, Int32>("INSERT", oBMAST, null, ref vKey, ref errMsg, "2019", 1);
                            if (vRef == 1)
                            {
                                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgSaveNew + "');");
                                //MessageBox(2, Message.msgSaveNew, "");
                                //errMsg = FillMastBenefitGrid();
                                //hf_MAST_BENEFITS_KEY.Value = Convert.ToString(vKey);
                            }
                            else if (vRef == 2)
                            {
                                TempData["JavaScriptFunction"] = string.Format("OpenTab1('" + Message.msgValidation + "');");
                                //  MessageBox(2, Message.msgValidation, errMsg);
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
                                // FillMastBenefitEdit(oBMAST.MAST_BENEFITS_KEY);
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
                    }
                    //  oSysUser.TAG_PAGE_REFRESH = Server.UrlEncode(System.DateTime.Now.ToString());
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

            return Redirect("~/Benefit/Index");

        }

        private String ProcessImage(ref String Doc_Name, HttpPostedFileBase file)
        {


            if (file == null)
            {
                ViewBag.hf_BENEFITS_IMG = Request["hf_BENEFITS_IMG"];
                return String.Empty;
            }
            else
            {




                String[] ImageAcceptedExtensions = null;
                String DOC_PATH = "~" + ConfigurationManager.AppSettings["BENEFITS"].ToString();
                try
                {

                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".webp", ".svg" };


                    var Image_url = file.ToString(); //getting complete url
                    var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                    var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = System.DateTime.Now.ToString("yyyyMMdd_hhmmss") + "-A" + ext; //appending the name with id  
                                                                                                      // store the file inside ~/project folder(Img)  
                        var path = Server.MapPath(DOC_PATH + myfile);
                        Image_url = path;

                        file.SaveAs(path);

                        ViewBag.hf_BENEFITS_IMG = myfile;
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
    }
}