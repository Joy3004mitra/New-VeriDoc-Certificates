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
   public class BaRegister : IDisposable
    {

        Int16 vCount = 0;
        DataTable dt = null;
        DataSet ds = null;
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

                using (sqlhelper oDBC = new sqlhelper("SP_GET_DTLS_REGISTER", CommandType.StoredProcedure))
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

        public byte SaveChanges<T1, T2>(String pMode, EntityRegister oEntity, T1 pValue, ref T2 pKey, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            Byte vRef = 0;
            try
            {
                vCount = 0;
                para = new SqlParameter[27];
                para[vCount] = new SqlParameter("@SELECT_ACTION", SqlDbType.VarChar, 20);
                para[vCount++].Value = pMode;
                para[vCount] = new SqlParameter("@RETURN_KEY", SqlDbType.TinyInt);
                para[vCount].Direction = ParameterDirection.InputOutput;
                para[vCount++].Value = 0;
                para[vCount] = new SqlParameter("@DTLS_REGISTER_KEY", SqlDbType.Int);
                para[vCount].Direction = ParameterDirection.InputOutput;
                para[vCount++].Value = oEntity.DTLS_REGISTER_KEY;
                para[vCount] = new SqlParameter("@FIRSTNAME", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.FIRSTNAME;
                para[vCount] = new SqlParameter("@LASTNAME", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.LASTNAME;
                para[vCount] = new SqlParameter("@EMAIL", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.EMAIL;
                para[vCount] = new SqlParameter("@PHONE", SqlDbType.VarChar, 50);
                para[vCount++].Value = oEntity.PHONE;
                para[vCount] = new SqlParameter("@COMPANY_NAME", SqlDbType.VarChar, 300);
                para[vCount++].Value = oEntity.COMPANY_NAME;
                para[vCount] = new SqlParameter("@COMPANY_ADDRESS", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.COMPANY_ADDRESS;
                para[vCount] = new SqlParameter("@DTLS_PACKAGE_KEY", SqlDbType.Int);
                para[vCount++].Value = oEntity.DTLS_PACKAGE_KEY;

                para[vCount] = new SqlParameter("@COUNTRY", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.COUNTRY;
                para[vCount] = new SqlParameter("@STATE", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.STATE;
                para[vCount] = new SqlParameter("@CITY", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.CITY;
                para[vCount] = new SqlParameter("@ZIP", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.ZIP;



                para[vCount] = new SqlParameter("@ALTER_NAME", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.ALTER_NAME;
                para[vCount] = new SqlParameter("@ALTER_EMAIL", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.ALTER_EMAIL;
                para[vCount] = new SqlParameter("@ALTER_PHONE", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.ALTER_PHONE;
                para[vCount] = new SqlParameter("@ALTER_COMPANY_NAME", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.ALTER_COMPANY_NAME;
                para[vCount] = new SqlParameter("@ALTER_COMPANY_ADDRESS", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.ALTER_COMPANY_ADDRESS;
                para[vCount] = new SqlParameter("@ALTER_COUNTRY", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.ALTER_COUNTRY;
                para[vCount] = new SqlParameter("@ALTER_STATE", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.ALTER_STATE;
                para[vCount] = new SqlParameter("@ALTER_CITY", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.ALTER_CITY;
                para[vCount] = new SqlParameter("@ALTER_ZIP", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.ALTER_ZIP;


                para[vCount] = new SqlParameter("@ENT_USER_KEY", SqlDbType.Int);
                para[vCount++].Value = oEntity.ENT_USER_KEY;
                para[vCount] = new SqlParameter("@EDIT_USER_KEY", SqlDbType.Int);
                para[vCount++].Value = oEntity.EDIT_USER_KEY;
                para[vCount] = new SqlParameter("@TAG_ACTIVE", SqlDbType.TinyInt);
                para[vCount++].Value = oEntity.TAG_ACTIVE;
                para[vCount] = new SqlParameter("@TAG_DELETE", SqlDbType.TinyInt);
                para[vCount++].Value = oEntity.TAG_DELETE;

                using (sqlhelper oDBC = new sqlhelper("SP_SAVE_DTLS_REGISTER", CommandType.StoredProcedure))
                {
                    oDBC.ExecuteNonQuery(pAccYr, pCompany_key, para, ref pMsg);
                    vRef = Convert.ToByte(oDBC.GetParameterValue("@RETURN_KEY", ref pMsg));
                    pKey = (T2)Convert.ChangeType(oDBC.GetParameterValue("@DTLS_REGISTER_KEY", ref pMsg), typeof(T2));
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return vRef;
        }


        public byte SaveAlterChanges<T1, T2>(String pMode, EntityRegister oEntity, T1 pValue, ref T2 pKey, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            Byte vRef = 0;
            try
            {
                vCount = 0;
                para = new SqlParameter[24];
                para[vCount] = new SqlParameter("@SELECT_ACTION", SqlDbType.VarChar, 20);
                para[vCount++].Value = pMode;
                para[vCount] = new SqlParameter("@RETURN_KEY", SqlDbType.TinyInt);
                para[vCount].Direction = ParameterDirection.InputOutput;
                para[vCount++].Value = 0;
                para[vCount] = new SqlParameter("@DTLS_REGISTER_KEY", SqlDbType.Int);
                para[vCount].Direction = ParameterDirection.InputOutput;
                para[vCount++].Value = oEntity.DTLS_REGISTER_KEY;
                para[vCount] = new SqlParameter("@FIRSTNAME", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.FIRSTNAME;
                para[vCount] = new SqlParameter("@LASTNAME", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.LASTNAME;
                para[vCount] = new SqlParameter("@EMAIL", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.EMAIL;
                para[vCount] = new SqlParameter("@PHONE", SqlDbType.VarChar, 50);
                para[vCount++].Value = oEntity.PHONE;
                para[vCount] = new SqlParameter("@COMPANY_NAME", SqlDbType.VarChar, 300);
                para[vCount++].Value = oEntity.COMPANY_NAME;
                para[vCount] = new SqlParameter("@COMPANY_ADDRESS", SqlDbType.VarChar, 9000);
                para[vCount++].Value = oEntity.COMPANY_ADDRESS;
                para[vCount] = new SqlParameter("@DTLS_PACKAGE_KEY", SqlDbType.Int);
                para[vCount++].Value = oEntity.DTLS_PACKAGE_KEY;

                para[vCount] = new SqlParameter("@ALTER_NAME", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.ALTER_NAME;
                para[vCount] = new SqlParameter("@ALTER_EMAIL", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.ALTER_EMAIL;
                para[vCount] = new SqlParameter("@ALTER_PHONE", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.ALTER_PHONE;
                para[vCount] = new SqlParameter("@ALTER_COMPANY_NAME", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.ALTER_COMPANY_NAME;
                para[vCount] = new SqlParameter("@ALTER_COMPANY_ADDRESS", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.ALTER_COMPANY_ADDRESS;
                para[vCount] = new SqlParameter("@ALTER_POSITION", SqlDbType.VarChar, 500);
                para[vCount++].Value = oEntity.ALTER_POSITION;


                para[vCount] = new SqlParameter("@ENT_USER_KEY", SqlDbType.Int);
                para[vCount++].Value = oEntity.ENT_USER_KEY;
                para[vCount] = new SqlParameter("@EDIT_USER_KEY", SqlDbType.Int);
                para[vCount++].Value = oEntity.EDIT_USER_KEY;
                para[vCount] = new SqlParameter("@TAG_ACTIVE", SqlDbType.TinyInt);
                para[vCount++].Value = oEntity.TAG_ACTIVE;
                para[vCount] = new SqlParameter("@TAG_DELETE", SqlDbType.TinyInt);
                para[vCount++].Value = oEntity.TAG_DELETE;

                using (sqlhelper oDBC = new sqlhelper("SP_SAVE_REGISTER_ALTER_VALUE", CommandType.StoredProcedure))
                {
                    oDBC.ExecuteNonQuery(pAccYr, pCompany_key, para, ref pMsg);
                    vRef = Convert.ToByte(oDBC.GetParameterValue("@RETURN_KEY", ref pMsg));
                    pKey = (T2)Convert.ChangeType(oDBC.GetParameterValue("@DTLS_REGISTER_KEY", ref pMsg), typeof(T2));
                }
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return vRef;
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

                using (sqlhelper oDBC = new sqlhelper("SP_GET_DTLS_REGISTER", CommandType.StoredProcedure))
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


        public Byte SaveDelete<T1, T2>(String pMode, EntityRegister oEntity, T1 pValue, ref T2 pKey, ref String pMsg, ref String pDelMsg, String pAccYr, Int16? pCompany_key)
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
                para[vCount] = new SqlParameter("@DTLS_REGISTER_KEY", SqlDbType.Int);
                para[vCount].Direction = ParameterDirection.InputOutput;
                para[vCount++].Value = oEntity.DTLS_REGISTER_KEY;

                para[vCount] = new SqlParameter("@EDIT_USER_KEY", SqlDbType.SmallInt);
                para[vCount++].Value = oEntity.ENT_USER_KEY;

                using (sqlhelper oDBC = new sqlhelper("SP_DELETE_DTLS_REGISTER", CommandType.StoredProcedure))
                {
                    oDBC.ExecuteNonQuery(pAccYr, pCompany_key, para, ref pMsg);
                    vRef = Convert.ToByte(oDBC.GetParameterValue("@RETURN_KEY", ref pMsg));
                    pKey = (T2)Convert.ChangeType(oDBC.GetParameterValue("@DTLS_REGISTER_KEY", ref pMsg), typeof(T2));
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
