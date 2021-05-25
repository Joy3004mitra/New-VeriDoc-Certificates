using System;
using System.Data;
using System.Web;
using System.Web.Mvc;
using MyApp.db;
using MyApp.db.Encryption;
using MyApp.db.MyAppBal;
using MyApp.Entity;

namespace VeriDoc_Certificate.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        EntitySysUser oSysUser = null;
        String errMsg = String.Empty;
        // GET: Admin
        #region login

        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["__ubc"];
          
            if (cookie != null)
            {
                string username = cookie["a"];
                string password = cookie["p"];

                DataTable dt = null;
                Int32 vRef = 0;
                using (BaSysuser oBSU = new BaSysuser())
                {
                    dt = oBSU.Get<Int16>("TYPE_1", 0, Encrypted.Decryptdata(username), 1, ref errMsg, null, null);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (password == Encrypted.EncryptASCII(Encrypted.Decryptdata(Convert.ToString(dr["USER_PASSWORD"]))))
                        {
                            setUserSession(dr);
                            vRef = 1;
                        }
                    }

                    if (vRef == 1)
                    {
                        return Redirect("~/Admin/Dashboard");

                    }


                }
            }

            return View();
        }



        [HttpPost]

        public void login(FormCollection form)
        {
            DataTable dt = null;
            Int32 vRef = 0;
         
            try
            {




                using (BaSysuser oBSU = new BaSysuser())
                {
                    dt = oBSU.Get<Int16>("TYPE_1", 0, form["txt_USERNAME"].ToString(), 1, ref errMsg, null, null);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Encrypted.Encryptdata(Convert.ToString(form["txt_PASSWORD"])) == Convert.ToString(dr["USER_PASSWORD"]))
                        {
                            setUserSession(dr);

                            HttpCookie objCookie = new HttpCookie("__ubc");
                            objCookie["a"] = Encrypted.Encryptdata(form["txt_USERNAME"].ToString()).ToString();
                            objCookie["p"] = Encrypted.EncryptASCII(Convert.ToString(form["txt_PASSWORD"])).ToString();
                            objCookie.Expires = DateTime.Now.AddDays(60);
                            Response.Cookies.Add(objCookie);
                            vRef = 1;
                        }
                    }
                    if (vRef == 1)
                    {
                        Response.Redirect("~/Admin/Dashboard");
                        // return "1";
                    }




                    else
                    {
                        TempData["JavaScriptFunction"] = string.Format("ShowErrMsg();");
                        Response.Redirect("~/Admin/Index");
                        // return "0";

                    }
                    // MessageBox(message.msgErr401, errMsg);
                    // ViewBag.JavaScriptFunction = string.Format("ShowErrMsg();");
                }
                else
                {
                    TempData["JavaScriptFunction"] = string.Format("ShowErrMsg();");
                    Response.Redirect("~/Admin/Index");
                    //  return "0";
                }
                // MessageBox(Message.msgErrPage, errMsg);

            }
            catch (Exception ex)
            {
                // MessageBox(Message.msgErrCommon, ex.Message);
            }
            finally
            {
                if (oSysUser != null)
                    oSysUser = null;
                if (dt != null)
                {
                    dt.Dispose(); dt = null;
                }
            }

            // return "0";

            // return "0";
            //return "sucess";
        }

        private void setUserSession(DataRow dr)
        {
            oSysUser = new EntitySysUser();
            oSysUser.USER_KEY = Convert.ToInt16(dr["USER_KEY"]);
            oSysUser.USER_NAME = Convert.ToString(dr["USER_NAME"]);
            oSysUser.USER_TYPE_KEY = Convert.ToInt32(dr["USER_TYPE_KEY"]);
            oSysUser.USER_TYPE = Convert.ToString(dr["USER_TYPE"]);
            oSysUser.MAST_HRD_PERSONNEL_KEY = Convert.ToInt32(dr["MAST_HRD_PERSONNEL_KEY"]);
            Session["USER_KEY"] = Convert.ToInt16(dr["USER_KEY"]);

            Session["oSysUser"] = oSysUser;
        }


        #endregion login


        #region dashboard
        public ActionResult Dashboard()
        {
            return View();
        }

        #endregion dashboard


        #region logout
        public ActionResult logout()
        {

            Session.Abandon();
            Session.Clear();
            if (Session["oSysUser"] != null)
                Session["oSysUser"] = null;

            HttpCookie cookie = Request.Cookies["__ubc"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            return Redirect("~/Admin/Index");

        }

        #endregion logout
    }
}