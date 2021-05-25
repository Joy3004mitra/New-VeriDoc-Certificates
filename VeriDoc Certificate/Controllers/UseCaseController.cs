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
    public class UseCaseController : Controller
    {
        // GET: UseCase
        EntitySysUser oSysUser = null;
        String errMsg = String.Empty;
        DataTable dt;
        DataSet ds;

        // GET: Benefit
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
                ds = new DataSet();
                using (BaUseCase oBMC = new BaUseCase())
                {
                    dt = oBMC.Get<Int32>("ALL", 0, "",ref errMsg, "2019", 1);

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
            Int32 vKey = 0; Int32 vRef = 0; String vDelMsg = String.Empty;
            Int32 pKey = 0;
            EntityUseCase oBMAST = null;
            try
            {
                if (ModelState.IsValid)
                {
                    string edit = form[0];
                    errMsg = String.Empty;
                    oBMAST = new EntityUseCase();
                    //GridViewRow gvr = (GridViewRow)((HtmlAnchor)sender).NamingContainer;
                    oBMAST.MAST_USE_CASES_KEY = Convert.ToInt32(edit);
                    oBMAST.ENT_USER_KEY = Convert.ToInt32(Session["USER_KEY"]);
                    oBMAST.EDIT_USER_KEY = Convert.ToInt32(Session["USER_KEY"]);
                    oBMAST.TAG_DELETE = 0;
                    using (BaUseCase oBMC = new BaUseCase())
                    {
                        vRef = oBMC.UseCaseDelete<Int32>("DELETE", oBMAST.MAST_USE_CASES_KEY, ref pKey, ref errMsg, oBMAST.EDIT_USER_KEY,"2021",1);
                    }
                    if (vRef > 0)
                    {
                        if (vRef == 1)
                        {
                            TempData["JavaScriptFunction"] = string.Format("ShowSuccess();");
                            // MessageBox(1, Message.msgSaveDelete, "");
                            //FillUserGrid();
                        }
                        else
                        {
                            TempData["JavaScriptFunction"] = string.Format("ShowErrMsg();");
                            //  MessageBox(2, Message.msgSaveErr, errMsg);
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

            return Redirect("~/UseCase/Index");
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
                    //   MessageBox(1, Message.msgErrEditPop, errMsg);
                }

            }
            catch (Exception ex)
            {
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
                ds = new DataSet();
                using (BaUseCase oBMC = new BaUseCase())
                {
                    dt = oBMC.Get<Int32>("SRH_KEY", pMAST_BENEFITS_KEY, "", ref errMsg, "2019", 1);

                }
           

                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewBag.txt_HEADING = Convert.ToString(dt.Rows[0]["USE_CASES_HEADING"]);
                    ViewBag.txt_DESCRIPTION = Convert.ToString(dt.Rows[0]["USE_CASES_DESC"]);
                    ViewBag.img_benefit = ConfigurationManager.AppSettings["USE_CASES"].ToString() + Convert.ToString(dt.Rows[0]["USE_CASES_IMAGE"]);
                    ViewBag.hf_BENEFITS_IMG = Convert.ToString(dt.Rows[0]["USE_CASES_IMAGE"]);
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
                //TempData["JavaScriptFunction"] = string.Format("OpenTab2();");
                //ClearControl();
                //MessageBox(2, "", "");
            }
            catch (Exception ex)
            {
                //  MessageBox(1, Message.msgErrCommon, ex.Message);
            }

            return View("Index", dt.Rows);
        }

        [HttpPost]


        public ActionResult btn_Head_Save_Click(FormCollection form, HttpPostedFileBase fu_benefit)
        {
            Int32 vRef = 0; Int32 vKey = 0;
            String LABELS = String.Empty;
            EntityUseCase oBMAST = null;
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
                    oBMAST = new EntityUseCase();
                    oBMAST.MAST_USE_CASES_KEY = Convert.ToInt32(form["hf_MAST_BENEFITS_KEY"]);

                    oBMAST.USE_CASES_HEADING = form["txt_HEADING"];
                    oBMAST.USE_CASES_DESC = form["txt_DESCRIPTION"];
                    oBMAST.USE_CASES_IMAGE = ViewBag.hf_BENEFITS_IMG;

                    oBMAST.ENT_USER_KEY = Convert.ToInt32(Session["USER_KEY"]);
                    oBMAST.EDIT_USER_KEY = Convert.ToInt32(Session["USER_KEY"]);
                    oBMAST.TAG_ACTIVE = 0;
                    oBMAST.TAG_DELETE = 0;
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                    using (BaUseCase oBMC = new BaUseCase())
                    { 
                        if (oBMAST.MAST_USE_CASES_KEY == 0)
                        {
                        vRef = oBMC.SaveChanges<Object, Int32>("INSERT", oBMAST, null, ref vKey, ref errMsg, "2019", 1);
                            if (vRef == 1)
                            {
                            TempData["JavaScriptFunction"] = string.Format("ShowSuccess();");
                            //MessageBox(2, Message.msgSaveNew, "");
                            //errMsg = FillMastBenefitGrid();
                            //hf_MAST_BENEFITS_KEY.Value = Convert.ToString(vKey);
                        }
                            else if (vRef == 2)
                            {
                            TempData["JavaScriptFunction"] = string.Format("ShowErrMsg();");
                            //  MessageBox(2, Message.msgValidation, errMsg);
                        }

                            else
                            {
                            TempData["JavaScriptFunction"] = string.Format("ShowErrMsg();");
                            // MessageBox(2, Message.msgSaveErr, errMsg);
                        }

                        }
                        else
                        {
                            vRef = oBMC.SaveChanges<Object, Int32>("UPDATE", oBMAST, null, ref vKey, ref errMsg, "2019", 1);
                            if (vRef == 1)
                            {
                            TempData["JavaScriptFunction"] = string.Format("ShowSuccess();");
                            // MessageBox(2, Message.msgSaveEdit, "");
                            // FillMastBenefitEdit(oBMAST.MAST_BENEFITS_KEY);
                        }
                            else if (vRef == 2)
                            {
                            TempData["JavaScriptFunction"] = string.Format("ShowErrMsg();");
                            //  MessageBox(2, Message.msgSaveDuplicate, errMsg);
                        }

                            else
                            {
                            TempData["JavaScriptFunction"] = string.Format("ShowErrMsg();");
                            // MessageBox(2, Message.msgSaveErr, errMsg);
                        }

                        }
                    }

                    //  oSysUser.TAG_PAGE_REFRESH = Server.UrlEncode(System.DateTime.Now.ToString());
                }
                else
                {
                    //  MessageBox(2, Message.msgPageInvalid, "");
                }

            }
            catch (Exception ex)
            {
                //  MessageBox(2, Message.msgErrCommon, ex.Message);
            }
            finally
            {
                if (oBMAST != null)
                    oBMAST = null;
            }

            return Redirect("~/UseCase/Index");

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
                String DOC_PATH = "~" + ConfigurationManager.AppSettings["USE_CASES"].ToString();
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