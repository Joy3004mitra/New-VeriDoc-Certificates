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
   public class BaDtlsQuickFact : IDisposable
    {
        Int16 vCount = 0;
        DataSet ds = null;
        DataTable dt = null;
        SqlParameter[] para = null;
        //public void BindDdl(ref DropDownList ddl, Int32 pKey, ref String pMsg, String pAccYr, Int16? pCompany_key)
        //{
        //    try
        //    {
        //        vCount = 0;
        //        para = new SqlParameter[1];
        //        para[vCount] = new SqlParameter("@GET_REC_TYPE", SqlDbType.VarChar, 20);
        //        para[vCount++].Value = "DDL";

        //        using (CommonOp oCommonOp = new CommonOp())
        //        {
        //            oCommonOp.BindDropDownList("SP_GET_DTLS_QUICK_FACT", ref ddl, "QUICK_FACT_KEY", "QUICK_FACT_DESC", ref pMsg, pAccYr, pCompany_key, para);
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

                using (sqlhelper oDBC = new sqlhelper("SP_GET_DTLS_QUICK_FACT", CommandType.StoredProcedure))
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

        public Byte SaveChanges<T1, T2>(String pMode, EntityDtlsQuickFact oEntity, T1 pValue, ref T2 pKey, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            Byte vRef = 0;
            try
            {
                vCount = 0;
                para = new SqlParameter[8];
                para[vCount] = new SqlParameter("@SELECT_ACTION", SqlDbType.VarChar, 20);
                para[vCount++].Value = pMode;
                para[vCount] = new SqlParameter("@RETURN_KEY", SqlDbType.TinyInt);
                para[vCount].Direction = ParameterDirection.InputOutput;
                para[vCount++].Value = 0;

                para[vCount] = new SqlParameter("@QUICK_FACT_KEY", SqlDbType.Int);
                para[vCount++].Value = oEntity.QUICK_FACT_KEY;
                para[vCount] = new SqlParameter("@QUICK_FACT_DESC", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.QUICK_FACT_DESC;


                para[vCount] = new SqlParameter("@ENT_USER_KEY", SqlDbType.Int);
                para[vCount++].Value = oEntity.ENT_USER_KEY;
                para[vCount] = new SqlParameter("@EDIT_USER_KEY", SqlDbType.Int);
                para[vCount++].Value = oEntity.EDIT_USER_KEY;
                para[vCount] = new SqlParameter("@TAG_ACTIVE", SqlDbType.TinyInt);
                para[vCount++].Value = oEntity.TAG_ACTIVE;
                para[vCount] = new SqlParameter("@TAG_DELETE", SqlDbType.TinyInt);
                para[vCount++].Value = oEntity.TAG_DELETE;

                using (sqlhelper oDBC = new sqlhelper("SP_SAVE_DTLS_QUICK_FACT", CommandType.StoredProcedure))
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

        public byte QuickFactDelete<T>(String pMode, T pValue, ref T pKey, ref String pMsg, Int32 pUser, String pAccYr, Int16? pCompany_key)
        {
            byte retKey = 0;
            try
            {
                vCount = 0;
                para = new SqlParameter[4];
                para[vCount] = new SqlParameter("@SELECT_ACTION", SqlDbType.VarChar, 20);
                para[vCount++].Value = pMode;
                para[vCount] = new SqlParameter("@RETURN_KEY", SqlDbType.TinyInt);
                para[vCount].Direction = ParameterDirection.InputOutput;
                para[vCount++].Value = 0;
                para[vCount] = new SqlParameter("@QUICK_FACT_KEY", SqlDbType.Int);
                para[vCount].Direction = ParameterDirection.InputOutput;
                para[vCount++].Value = pValue;
                para[vCount] = new SqlParameter("@EDIT_USER_KEY", SqlDbType.Int);
                para[vCount++].Value = pUser;

                using (sqlhelper oDBC = new sqlhelper("SP_DELETE_DTLS_QUICK_FACT", CommandType.StoredProcedure))
                {
                    oDBC.ExecuteNonQuery(pAccYr, pCompany_key, para, ref pMsg);
                    retKey = Convert.ToByte(oDBC.GetParameterValue("@RETURN_KEY", ref pMsg));
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return retKey;
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
