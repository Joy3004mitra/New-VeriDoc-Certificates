using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using MyApp.Entity;

namespace MyApp.db.MyAppBal
{
   public class BAHomeSettings : IDisposable
    {
        Int16 vCount = 0;
        DataSet ds = null;
        DataTable dt = null;
        SqlParameter[] para = null;

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

                using (sqlhelper oDBC = new sqlhelper("SP_GET_HEAD_HOME_SETTING", CommandType.StoredProcedure))
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

        public DataTable GetData(String pMode, String pSEARCH_TEXT, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[3];
                para[vCount] = new SqlParameter("@GET_REC_TYPE", SqlDbType.VarChar, 20);
                para[vCount++].Value = pMode;
                para[vCount] = new SqlParameter("@SEARCH_KEY", SqlDbType.Int);
                para[vCount++].Value = 0;
                para[vCount] = new SqlParameter("@SEARCH_TEXT", SqlDbType.VarChar, 50);
                para[vCount++].Value = pSEARCH_TEXT;

                using (sqlhelper oDBC = new sqlhelper("SP_GET_HEAD_HOME_SETTING", CommandType.StoredProcedure))
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

        public DataSet GetAllList(String pMode, String pSEARCH_TEXT, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[3];
                para[vCount] = new SqlParameter("@GET_REC_TYPE", SqlDbType.VarChar, 20);
                para[vCount++].Value = pMode;
                para[vCount] = new SqlParameter("@SEARCH_KEY", SqlDbType.Int);
                para[vCount++].Value = 0;
                para[vCount] = new SqlParameter("@SEARCH_TEXT", SqlDbType.VarChar, 50);
                para[vCount++].Value = pSEARCH_TEXT;

                using (sqlhelper oDBC = new sqlhelper("SP_GET_HEAD_HOME_SETTING", CommandType.StoredProcedure))
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

        public Byte SaveChanges<T1, T2>(String pMode, EntityHomeSetting oEntity, T1 pValue, ref T2 pKey, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            Byte vRef = 0;
            try
            {
                vCount = 0;
                para = new SqlParameter[15];
                para[vCount] = new SqlParameter("@SELECT_ACTION", SqlDbType.VarChar, 20);
                para[vCount++].Value = pMode;
                para[vCount] = new SqlParameter("@RETURN_KEY", SqlDbType.TinyInt);
                para[vCount].Direction = ParameterDirection.InputOutput;
                para[vCount++].Value = 0;

                para[vCount] = new SqlParameter("@BANNER_HEADING", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.BANNER_HEADING;
                para[vCount] = new SqlParameter("@BANNER_DESC", SqlDbType.VarChar, 9000);
                para[vCount++].Value = oEntity.BANNER_DESC;
                para[vCount] = new SqlParameter("@BANNER_DESC_1", SqlDbType.VarChar, 9000);
                para[vCount++].Value = oEntity.BANNER_DESC_1;
                para[vCount] = new SqlParameter("@BANNER_IMG", SqlDbType.VarChar, 50);
                para[vCount++].Value = oEntity.BANNER_IMG;
                para[vCount] = new SqlParameter("@BANNER_NOTE", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.BANNER_NOTE;


                para[vCount] = new SqlParameter("@GOOGLEPLAY_LINK", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.GOOGLEPLAY_LINK;

                para[vCount] = new SqlParameter("@PATENTED_HEADING", SqlDbType.VarChar, 9000);
                para[vCount++].Value = oEntity.PATENTED_HEADING;
                para[vCount] = new SqlParameter("@PATENTED_IMG", SqlDbType.VarChar, 50);
                para[vCount++].Value = oEntity.PATENTED_IMG;

                para[vCount] = new SqlParameter("@APPSTORE_LINK", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.APPSTORE_LINK;


                para[vCount] = new SqlParameter("@ENT_USER_KEY", SqlDbType.Int);
                para[vCount++].Value = oEntity.ENT_USER_KEY;
                para[vCount] = new SqlParameter("@EDIT_USER_KEY", SqlDbType.Int);
                para[vCount++].Value = oEntity.EDIT_USER_KEY;
                para[vCount] = new SqlParameter("@TAG_ACTIVE", SqlDbType.TinyInt);
                para[vCount++].Value = oEntity.TAG_ACTIVE;
                para[vCount] = new SqlParameter("@TAG_DELETE", SqlDbType.TinyInt);
                para[vCount++].Value = oEntity.TAG_DELETE;

                using (sqlhelper oDBC = new sqlhelper("SP_SAVE_HEAD_HOME_SETTING", CommandType.StoredProcedure))
                {
                    oDBC.ExecuteNonQuery(pAccYr, pCompany_key, para, ref pMsg);
                    vRef = Convert.ToByte(oDBC.GetParameterValue("@RETURN_KEY", ref pMsg));
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return vRef;
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
