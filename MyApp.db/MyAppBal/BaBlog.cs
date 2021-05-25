using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Entity;



namespace MyApp.db.MyAppBal
{

  
    public class BaBlog
   {
        Int16 vCount = 0;
        DataTable dt = null;
        DataSet ds = null;
        SqlParameter[] para = null;

        //public void BindDdl(ref DropDownList ddl, int pKey, ref string pMsg, string pAccYr, short? pCompany_key)
        //{
        //    try
        //    {
        //        vCount = 0;
        //        para = new SqlParameter[1];
        //        para[vCount] = new SqlParameter("@GET_REC_TYPE", SqlDbType.VarChar, 20);
        //        para[vCount++].Value = "DDL";

        //        using (CommonOp oCommonOp = new CommonOp())
        //        {
        //            oCommonOp.BindDropDownList("SP_GET_MAST_MODEL_CATEGORY", ref ddl, "MAST_MODEL_CATEGORY_KEY", "CATEGORY_TYPE", ref pMsg, pAccYr, pCompany_key, para);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        pMsg = ex.Message;
        //    }
        //}

        public DataTable Get<T>(String pMode, T pKey, String pSEARCH_TEXT, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[3];
                para[vCount] = new SqlParameter("@GET_REC_TYPE", SqlDbType.VarChar, 20);
                para[vCount++].Value = pMode;
                para[vCount] = new SqlParameter("@SEARCH_KEY", SqlDbType.Int);
                para[vCount++].Value = pKey;
                para[vCount] = new SqlParameter("@SEARCH_TEXT", SqlDbType.VarChar, 50);
                para[vCount++].Value = pSEARCH_TEXT;

                using (sqlhelper oDBC = new sqlhelper("SP_GET_MAST_BLOG", CommandType.StoredProcedure))
                {
                    dt = oDBC.GetDataTable(pAccYr, pCompany_key, para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return dt;
        }

        public byte SaveChanges<T1, T2>(String pMode,T1 pValue, EntityBlog oEntity, ref T2 pKey, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            Byte vRef = 0;
            try
            {
                vCount = 0;
                para = new SqlParameter[16];
                para[vCount] = new SqlParameter("@SELECT_ACTION", SqlDbType.VarChar, 20);
                para[vCount++].Value = pMode;
                para[vCount] = new SqlParameter("@RETURN_KEY", SqlDbType.TinyInt);
                para[vCount].Direction = ParameterDirection.InputOutput;
                para[vCount++].Value = 0;
                para[vCount] = new SqlParameter("@MAST_BLOG_KEY", SqlDbType.Int);
                para[vCount].Direction = ParameterDirection.InputOutput;
                para[vCount++].Value = oEntity.MAST_BLOG_KEY;
                para[vCount] = new SqlParameter("@MAST_BLOG_TAG_KEY", SqlDbType.VarChar, 100);
                para[vCount++].Value = oEntity.MAST_BLOG_TAG_KEY;
                para[vCount] = new SqlParameter("@MAST_BLOG_CATEGORY_KEY", SqlDbType.VarChar, 100);
                para[vCount++].Value = oEntity.MAST_BLOG_CATEGORY_KEY;
                para[vCount] = new SqlParameter("@BLOG_HEADING", SqlDbType.VarChar, 200);
                para[vCount++].Value = oEntity.BLOG_HEADING;
                para[vCount] = new SqlParameter("@BLOG_IMAGE", SqlDbType.VarChar, 50);
                para[vCount++].Value = oEntity.BLOG_IMAGE;
                para[vCount] = new SqlParameter("@BLOG_DATE", SqlDbType.SmallDateTime);
                para[vCount++].Value = oEntity.BLOG_DATE;
                para[vCount] = new SqlParameter("@DESCRIPTION", SqlDbType.VarChar);
                para[vCount++].Value = oEntity.DESCRIPTION;
                para[vCount] = new SqlParameter("@BLOG_QUESTION", SqlDbType.VarChar, 300);
                para[vCount++].Value = oEntity.BLOG_QUESTION;
                para[vCount] = new SqlParameter("@META_DESC", SqlDbType.VarChar, 9000);
                para[vCount++].Value = oEntity.META_DESC;
                para[vCount] = new SqlParameter("@META_KEYWORD", SqlDbType.VarChar, 9000);
                para[vCount++].Value = oEntity.META_KEYWORD;

                para[vCount] = new SqlParameter("@ENT_USER_KEY", SqlDbType.Int);
                para[vCount++].Value = oEntity.ENT_USER_KEY;
                para[vCount] = new SqlParameter("@EDIT_USER_KEY", SqlDbType.Int);
                para[vCount++].Value = oEntity.EDIT_USER_KEY;
                para[vCount] = new SqlParameter("@TAG_ACTIVE", SqlDbType.TinyInt);
                para[vCount++].Value = oEntity.TAG_ACTIVE;
                para[vCount] = new SqlParameter("@TAG_DELETE", SqlDbType.TinyInt);
                para[vCount++].Value = oEntity.TAG_DELETE;

                using (sqlhelper oDBC = new sqlhelper("SP_SAVE_MAST_BLOG", CommandType.StoredProcedure))
                {
                    oDBC.ExecuteNonQuery(pAccYr, pCompany_key, para, ref pMsg);
                    vRef = Convert.ToByte(oDBC.GetParameterValue("@RETURN_KEY", ref pMsg));
                    pKey = (T2)Convert.ChangeType(oDBC.GetParameterValue("@MAST_BLOG_KEY", ref pMsg), typeof(T2));
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return vRef;
        }

        public Byte SaveDelete<T1, T2>(String pMode, EntityBlog oEntity, T1 pValue, ref T2 pKey, ref String pMsg, ref String pDelMsg, String pAccYr, Int16? pCompany_key)
        {
            Byte vRef = 0;
            try
            {
                vCount = 0;
                para = new SqlParameter[4];
                para[vCount] = new SqlParameter("@SELECT_ACTION", SqlDbType.VarChar, 30);
                para[vCount++].Value = "DELETE";
                para[vCount] = new SqlParameter("@RETURN_KEY", SqlDbType.TinyInt);
                para[vCount].Direction = ParameterDirection.InputOutput;
                para[vCount++].Value = 0;
                para[vCount] = new SqlParameter("@MAST_BLOG_KEY", SqlDbType.Int);
                para[vCount].Direction = ParameterDirection.InputOutput;
                para[vCount++].Value = oEntity.MAST_BLOG_KEY;

                para[vCount] = new SqlParameter("@EDIT_USER_KEY", SqlDbType.SmallInt);
                para[vCount++].Value = oEntity.ENT_USER_KEY;

                using (sqlhelper oDBC = new sqlhelper("SP_DELETE_MAST_BLOG", CommandType.StoredProcedure))
                {
                    oDBC.ExecuteNonQuery(pAccYr, pCompany_key, para, ref pMsg);
                    vRef = Convert.ToByte(oDBC.GetParameterValue("@RETURN_KEY", ref pMsg));
                    pKey = (T2)Convert.ChangeType(oDBC.GetParameterValue("@MAST_BLOG_KEY", ref pMsg), typeof(T2));
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return vRef;
        }


        public DataSet GetData<T>(String pMode, T pKey, String pSEARCH_TEXT, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[3];
                para[vCount] = new SqlParameter("@GET_REC_TYPE", SqlDbType.VarChar, 20);
                para[vCount++].Value = pMode;
                para[vCount] = new SqlParameter("@SEARCH_KEY", SqlDbType.Int);
                para[vCount++].Value = pKey;
                para[vCount] = new SqlParameter("@SEARCH_TEXT", SqlDbType.VarChar, 50);
                para[vCount++].Value = pSEARCH_TEXT;

                using (sqlhelper oDBC = new sqlhelper("SP_GET_MAST_BLOG", CommandType.StoredProcedure))
                {
                    ds = oDBC.GetDataSet(pAccYr, pCompany_key, para, ref pMsg);
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }


        public void Dispose()
        {
            if (dt != null)
            {
                dt.Dispose(); dt = null;
            }
            if (para != null)
                para = null;
        }














    }
}
