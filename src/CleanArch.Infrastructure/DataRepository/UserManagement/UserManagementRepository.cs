using CleanArch.Infrastructure.Core;
using CleanArch.Core.Repository.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;
using CleanArch.Core.Entities.UserManagement;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Oracle.ManagedDataAccess.Types;
using Microsoft.Extensions.Configuration;
using System.Linq;
using CleanArch.Infrastructure.ShipaFreightComponents;

namespace CleanArch.Infrastructure.DataRepository.UserManagement
{
    public class UserManagementRepository : DBFactoryCore,  IUserManagementRepository 
    {
        IConfiguration configuration;
        OracleConnection mConnection;
        OracleTransaction mOracleTransaction = null;
        public UserManagementRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        
        public UserEntity GetUserDetails(string UserId, int mode)
        {
            int? Rownumber = 0;
            OracleDataReader readeruser = null;
            UserEntity uentity = new UserEntity();
            using (mConnection = new OracleConnection(GetConnectionString(configuration, DBConnectionMode.SSP)))
            {
                if (mConnection.State == ConnectionState.Closed) mConnection.Open();
                mOracleTransaction = mConnection.BeginTransaction();
                using (OracleCommand mCommand = mConnection.CreateCommand())
                {
                    mCommand.BindByName = true;
                    mCommand.Transaction = mOracleTransaction;
                    mCommand.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        mCommand.Parameters.Clear();
                        mCommand.Parameters.Add("IP_USERID", UserId.ToLower());
                        mCommand.Parameters.Add("IP_ROWNUMBER", Rownumber);
                        mCommand.Parameters.Add("IP_MODE", mode);
                        mCommand.Parameters.Add("OUT_RESULT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        mCommand.CommandText = "USP_SSP_GETUSERDETAILSBYUSERID";
                        mCommand.CommandType = CommandType.StoredProcedure;
                        mCommand.ExecuteNonQuery();
                        readeruser = ((OracleRefCursor)mCommand.Parameters["OUT_RESULT"].Value).GetDataReader();
                        DataTable dt = new DataTable();
                        dt.Load(readeruser);
                        uentity = MyClass.ConvertToEntity<UserEntity>(dt);
                        return uentity;
                    }
                    catch (Exception ex)
                    {
                        mOracleTransaction.Rollback();
                       //pavan throw new ShipaDbException(ex.ToString());
                    }
                    finally
                    {
                        mConnection.Dispose();
                        mCommand.Dispose();
                        mOracleTransaction.Dispose();
                        if (readeruser != null)
                        {
                            readeruser.Dispose();
                        }
                    }
                    return uentity;
                }
            }
        }

        public TokenDetailsEntity GenerateToken(string ConfigName)
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
           

            string ConfigValue = GetUMConfigValue(ConfigName);
            TokenDetailsEntity tobj = new TokenDetailsEntity();
            tobj.Token = token;
            tobj.TokenExpiry = DateTime.UtcNow.AddHours(int.Parse(ConfigValue));
            return tobj;

        }
        public string GetUMConfigValue(string ConfigName)
        {
            UMConfigurationEntity mEntity = new UMConfigurationEntity();
            ReadText(GetConnectionString(configuration, DBConnectionMode.SSP), "SELECT CONFIGVALUE FROM UMCONFIGURATIONS WHERE CONFIGNAME = :CONFIGNAME",
                new OracleParameter[] { new OracleParameter { OracleDbType = OracleDbType.Varchar2, ParameterName = "CONFIGNAME", Value = ConfigName } }
            , delegate (IDataReader r)
            {
                mEntity = DataReaderMapToObject<UMConfigurationEntity>(r);
            });
            return mEntity.CONFIGVALUE;
        }
        public long Save(UserEntity entity)
        {
            string PREFERREDCURRENCY = ""; string PREFERREDCURRENCYCODE = "";
            OracleConnection mFocisConnection;
            OracleTransaction mfocisOracleTransaction = null;
            using (mFocisConnection = new OracleConnection(GetConnectionString(configuration, DBConnectionMode.FOCiS)))
            {
                if (mFocisConnection.State == ConnectionState.Closed) mFocisConnection.Open();
                mfocisOracleTransaction = mFocisConnection.BeginTransaction();
                using (OracleCommand mCommand = mFocisConnection.CreateCommand())
                {
                    mCommand.BindByName = true;
                    mCommand.Transaction = mfocisOracleTransaction;
                    mCommand.CommandType = CommandType.Text;
                    try
                    {
                        mCommand.CommandText = " SELECT CURRENCYNAME,CURRENCYCODE FROM FOCIS.CURRENCIES WHERE CURRENCYID IN (SELECT BASECURRENCYID FROM FOCIS.SMDM_COUNTRY WHERE countryid=" + entity.COUNTRYID + ")";
                        DataTable dt = ReturnDatatable(DBConnectionMode.FOCiS, mCommand.CommandText, null);
                        if (dt.Rows.Count > 0)
                        {
                            PREFERREDCURRENCY = dt.Rows[0]["CURRENCYNAME"].ToString();
                            PREFERREDCURRENCYCODE = dt.Rows[0]["CURRENCYCODE"].ToString();
                        }
                        else
                        {
                            PREFERREDCURRENCY = "US Dollar";
                            PREFERREDCURRENCYCODE = "USD";
                        }
                    }
                    catch (Exception ex)
                    {
                        mfocisOracleTransaction.Rollback();
                       // throw new ShipaDbException(ex.ToString());
                    }
                }
            }
            using (mConnection = new OracleConnection(SSPConnectionString))
            {
                if (mConnection.State == ConnectionState.Closed) mConnection.Open();
                mOracleTransaction = mConnection.BeginTransaction();
                using (OracleCommand mCommand = mConnection.CreateCommand())
                {
                    mCommand.BindByName = true;
                    mCommand.Transaction = mOracleTransaction;
                    mCommand.CommandType = CommandType.Text;
                    try
                    {
                        if (string.IsNullOrEmpty(entity.COUNTRYCODE))
                        {
                            entity.COUNTRYCODE = "";
                        }
                        mCommand.CommandText = @"INSERT INTO USERS (USERID,USERTYPE,EMAILID,PASSWORD,DATECREATED,FIRSTNAME,LASTNAME,COMPANYNAME,
                        ISDID,ISDCODE,MOBILENUMBER,ISTERMSAGREED,COUNTRYID,ACCOUNTSTATUS,TIMEZONEID,USERCULTUREID,TOKEN,TOKENEXPIRY,COUNTRYCODE,COUNTRYNAME,
                        PREFERREDCURRENCY,PREFERREDCURRENCYCODE,NOTIFICATIONSUBSCRIPTION,SHIPMENTPROCESS,SHIPPINGEXPERIENCE) VALUES 
                        (:USERID,:USERTYPE,:EMAILID,:PASSWORD,:DATECREATED,:FIRSTNAME,:LASTNAME,:COMPANYNAME,:ISDID,:ISDCODE,:MOBILENUMBER,:ISTERMSAGREED,
                        :COUNTRYID,:ACCOUNTSTATUS,:TIMEZONEID,:USERCULTUREID,:TOKEN,:TOKENEXPIRY,:COUNTRYCODE,:COUNTRYNAME,:PREFERREDCURRENCY,
                        :PREFERREDCURRENCYCODE,:NOTIFICATIONSUBSCRIPTION,:SHIPMENTPROCESS,:SHIPPINGEXPERIENCE)";
                        mCommand.Parameters.Add("USERID", entity.USERID.ToLower());
                        mCommand.Parameters.Add("USERTYPE", entity.USERTYPE);
                        mCommand.Parameters.Add("EMAILID", entity.EMAILID);
                        mCommand.Parameters.Add("PASSWORD", entity.PASSWORD);
                        mCommand.Parameters.Add("DATECREATED", DateTime.Now);
                        mCommand.Parameters.Add("FIRSTNAME", entity.FIRSTNAME);
                        mCommand.Parameters.Add("LASTNAME", entity.LASTNAME);
                        mCommand.Parameters.Add("COMPANYNAME", entity.COMPANYNAME);
                        mCommand.Parameters.Add("ISDID", entity.ISDID);
                        mCommand.Parameters.Add("ISDCODE", entity.ISDCODE);
                        mCommand.Parameters.Add("MOBILENUMBER", entity.MOBILENUMBER);
                        mCommand.Parameters.Add("ISTERMSAGREED", entity.ISTERMSAGREED);
                        mCommand.Parameters.Add("COUNTRYID", entity.COUNTRYID);
                        mCommand.Parameters.Add("ACCOUNTSTATUS", entity.ACCOUNTSTATUS);
                        mCommand.Parameters.Add("TIMEZONEID", entity.TIMEZONEID);
                        mCommand.Parameters.Add("USERCULTUREID", entity.USERCULTUREID);
                        mCommand.Parameters.Add("TOKEN", entity.TOKEN);
                        mCommand.Parameters.Add("TOKENEXPIRY", entity.TOKENEXPIRY);
                        mCommand.Parameters.Add("COUNTRYCODE", entity.COUNTRYCODE.ToUpper());
                        mCommand.Parameters.Add("COUNTRYNAME", entity.COUNTRYNAME);
                        mCommand.Parameters.Add("PREFERREDCURRENCY", PREFERREDCURRENCY);
                        mCommand.Parameters.Add("PREFERREDCURRENCYCODE", PREFERREDCURRENCYCODE);
                        mCommand.Parameters.Add("NOTIFICATIONSUBSCRIPTION", entity.NOTIFICATIONSUBSCRIPTION.Equals(true) ? 1 : 0);
                        mCommand.Parameters.Add("SHIPMENTPROCESS", entity.SHIPMENTPROCESS);
                        mCommand.Parameters.Add("SHIPPINGEXPERIENCE", entity.SHIPPINGEXPERIENCE);
                        mCommand.ExecuteNonQuery();
                        mOracleTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        mOracleTransaction.Rollback();
                       // throw new ShipaDbException(ex.ToString());
                    }
                }
                //SSP NOTIFICATIONS ADDED BY SIVA
                if (mConnection.State == ConnectionState.Closed) mConnection.Open();
                mOracleTransaction = mConnection.BeginTransaction();
                using (OracleCommand mCommand = mConnection.CreateCommand())
                {
                    mCommand.BindByName = true;
                    mCommand.Transaction = mOracleTransaction;
                    mCommand.CommandType = CommandType.Text;
                    try
                    {
                        mCommand.CommandText = "SELECT SSP_NOTIFICATIONS_SEQ.NEXTVAL FROM DUAL";
                        entity.NOTIFICATIONID = Convert.ToInt64(mCommand.ExecuteScalar());
                        mCommand.CommandText = "INSERT INTO SSPNOTIFICATIONS (NOTIFICATIONID,NOTIFICATIONTYPEID,USERID,DATECREATED,DATEREQUESTED,NOTIFICATIONCODE) VALUES (:NOTIFICATIONID,:NOTIFICATIONTYPEID,:USERID,:DATECREATED,:DATEREQUESTED,:NOTIFICATIONCODE)";
                        mCommand.Parameters.Add("NOTIFICATIONID", Convert.ToInt32(entity.NOTIFICATIONID));
                        mCommand.Parameters.Add("NOTIFICATIONTYPEID", 5111);
                        mCommand.Parameters.Add("USERID", entity.USERID.ToUpper());
                        mCommand.Parameters.Add("DATECREATED", DateTime.Now);
                        mCommand.Parameters.Add("DATEREQUESTED", DateTime.Now);
                        mCommand.Parameters.Add("NOTIFICATIONCODE", "Profile Incomplete");
                        mCommand.ExecuteNonQuery();
                        mOracleTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        mOracleTransaction.Rollback();
                      //  throw new ShipaDbException(ex.ToString());
                    }
                }
            }
            //--------------------Sales Force---------------
            using (mConnection = new OracleConnection(FOCISConnectionString))
            {
                if (mConnection.State == ConnectionState.Closed) mConnection.Open();
                mOracleTransaction = mConnection.BeginTransaction();
                using (OracleCommand mCommand = mConnection.CreateCommand())
                {
                    mCommand.Parameters.Clear();
                    mCommand.BindByName = true;
                    mCommand.Transaction = mOracleTransaction;
                    mCommand.CommandType = CommandType.StoredProcedure;
                    mCommand.CommandText = "USP_SSP_CEPREQUOTEPROFILE";
                    try
                    {
                        mCommand.Parameters.Add("PRM_GUESTNAME", Convert.ToString(entity.FIRSTNAME));
                        mCommand.Parameters.Add("PRM_GUESTCOUNTRYCODE", entity.COUNTRYCODE);
                        mCommand.Parameters.Add("PRM_GUESTCOUNTRYNAME", entity.COUNTRYNAME);
                        mCommand.Parameters.Add("PRM_GUESTCOMPANY", entity.COMPANYNAME);
                        mCommand.Parameters.Add("PRM_GUESTEMAIL", entity.USERID);
                        mCommand.Parameters.Add("PRM_TELEPHONENUMBER", Convert.ToInt64(entity.MOBILENUMBER));
                        mCommand.Parameters.Add("OP_RESULTSET", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        mCommand.ExecuteNonQuery();
                        mOracleTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        mOracleTransaction.Rollback();
                       // throw new ShipaDbException(ex.ToString());
                    }
                    finally
                    {
                        mConnection.Dispose();
                        mCommand.Dispose();
                        mOracleTransaction.Dispose();
                    }
                }
            }
            //-----------------------------------------------
            return 1;
        }
    }
}
