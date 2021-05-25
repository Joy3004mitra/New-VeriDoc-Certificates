using MyApp.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.db.MyAppBal
{
   public class BaHowItWorkUser : IDisposable
    {
        Int16 vCount = 0;
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

                using (sqlhelper oDBC = new sqlhelper("SP_GET_HOW_IT_WORK_USER", CommandType.StoredProcedure))
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

                using (sqlhelper oDBC = new sqlhelper("SP_GET_HOW_IT_WORK_USER", CommandType.StoredProcedure))
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

        public Byte SaveChanges<T1, T2>(String pMode, EntityHowItWorkUser oEntity, T1 pValue, ref T2 pKey, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            Byte vRef = 0;
            try
            {
                vCount = 0;
                para = new SqlParameter[18];
                para[vCount] = new SqlParameter("@SELECT_ACTION", SqlDbType.VarChar, 20);
                para[vCount++].Value = pMode;
                para[vCount] = new SqlParameter("@RETURN_KEY", SqlDbType.TinyInt);
                para[vCount].Direction = ParameterDirection.InputOutput;
                para[vCount++].Value = 0;
                para[vCount] = new SqlParameter("@HEADING_ONE", SqlDbType.VarChar, 50);
                para[vCount++].Value = oEntity.HEADING_ONE;
                para[vCount] = new SqlParameter("@DESC_ONE", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.DESC_ONE;
                para[vCount] = new SqlParameter("@IMAGE_ONE", SqlDbType.VarChar, 50);
                para[vCount++].Value = oEntity.IMAGE_ONE;

                para[vCount] = new SqlParameter("@HEADING_TWO", SqlDbType.VarChar, 50);
                para[vCount++].Value = oEntity.HEADING_TWO;
                para[vCount] = new SqlParameter("@DESC_TWO", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.DESC_TWO;
                para[vCount] = new SqlParameter("@IMAGE_TWO", SqlDbType.VarChar, 50);
                para[vCount++].Value = oEntity.IMAGE_TWO;

                para[vCount] = new SqlParameter("@HEADING_THREE", SqlDbType.VarChar, 50);
                para[vCount++].Value = oEntity.HEADING_THREE;
                para[vCount] = new SqlParameter("@DESC_THREE", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.DESC_THREE;
                para[vCount] = new SqlParameter("@IMAGE_THREE", SqlDbType.VarChar, 50);
                para[vCount++].Value = oEntity.IMAGE_THREE;

                para[vCount] = new SqlParameter("@HEADING_FOUR", SqlDbType.VarChar, 50);
                para[vCount++].Value = oEntity.HEADING_FOUR;
                para[vCount] = new SqlParameter("@DESC_FOUR", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.DESC_FOUR;
                para[vCount] = new SqlParameter("@IMAGE_FOUR", SqlDbType.VarChar, 50);
                para[vCount++].Value = oEntity.IMAGE_FOUR;

                para[vCount] = new SqlParameter("@ENT_USER_KEY", SqlDbType.Int);
                para[vCount++].Value = oEntity.ENT_USER_KEY;
                para[vCount] = new SqlParameter("@EDIT_USER_KEY", SqlDbType.Int);
                para[vCount++].Value = oEntity.EDIT_USER_KEY;
                para[vCount] = new SqlParameter("@TAG_ACTIVE", SqlDbType.TinyInt);
                para[vCount++].Value = oEntity.TAG_ACTIVE;
                para[vCount] = new SqlParameter("@TAG_DELETE", SqlDbType.TinyInt);
                para[vCount++].Value = oEntity.TAG_DELETE;

                using (sqlhelper oDBC = new sqlhelper("SP_SAVE_HOW_IT_WORK_USER", CommandType.StoredProcedure))
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
