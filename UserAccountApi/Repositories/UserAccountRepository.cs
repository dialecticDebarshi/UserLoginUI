using Microsoft.Extensions.Configuration;
using System.CodeDom.Compiler;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using TechnoconGroupUserModule.Models;
using UserAccountApi.Utility;

namespace UserAccountApi.Repositories
{
    public class UserAccountRepository
    {
        IConfiguration _configuration;
        DbAccess _DbAccess;
        public UserAccountRepository(IConfiguration configuration)
        {
            _configuration= configuration; 
            _DbAccess = new DbAccess(_configuration);
        }

        public List<object> GetClientUniqueCodeTypes()
        {


            try
            {
                string[] pname = {};
                string[] pvalue = {};
                //return Int_Process("SP_BUSINESS_USER_REGISTRATION", pname, pvalue);
                DataSet DS= _DbAccess.Ds_Process("SP_GET_CLIENT_UNIQUECODE_TYPES", pname, pvalue);
                List<object> types = new List<object>();
                foreach (DataRow item in DS.Tables[0].Rows)
                {
                    types.Add(
                        new  { value = Convert.ToInt32( item["ClientUniqueCodeTypeId"] ), text = item["ClientUniqueCodeTypeDesc"].ToString(), Description = item["ClientCodeValidationRules"] }
                        );
                        
                
                }

                return types;
                
                //if (ds != null)
                //{

                //    var result = new 
                //    {
                //        Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ClientUniqueCodeTypeId"]),
                //        Value = ds.Tables[0].Rows[0]["ClientUniqueCodeTypeDesc"].ToString(),
                //        ValidationRule = ds.Tables[0].Rows[0]["ClientCodeValidationRules"].ToString()
                //    };
                //    return result;
                //}



            }
            catch (Exception ex)
            {

                throw;
            }

           

        }

        public int UserNameValidation(string UserName)
        {

            try
            {
                string[] pname = { "@ID" };
                string[] pvalue = { UserName };
                return _DbAccess.ExecuteNonQuery("CHECK_USERNAME_VALIDATION", pname, pvalue);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public VM_UserLoginResponse UserLoginValidation(VM_UserLogin login)
        {


            try
            {
                string[] pname = { "@UserName", "@Password" };
                string[] pvalue = { login.UserName,login.Password};
                //return Int_Process("SP_BUSINESS_USER_REGISTRATION", pname, pvalue);
                DataSet ds= _DbAccess.Ds_Process("SP_LOGIN_VALIDATION", pname, pvalue);
                if (ds.Tables.Count>0)
                {

                    var result = new VM_UserLoginResponse
                    {
                        UserID = Convert.ToInt32(ds.Tables[0].Rows[0]["BusinessUserID"]),
                        UserName = ds.Tables[0].Rows[0]["BusinessUserLoginId"].ToString(),
                        Employee_Master_Key = Convert.ToInt32(ds.Tables[0].Rows[0]["EMPLOYEE_MASTER_KEY"]),
                        Password = ds.Tables[0].Rows[0]["BusinessUserPwd"].ToString() ,
                        UserTypeId =Convert.ToInt32( ds.Tables[0].Rows[0]["BusinessUserCategoryId"]),
                        JobRoleId = Convert.ToInt32(ds.Tables[0].Rows[0]["UserTypeId"])
                    };
                    return result;
                }
               


            }
            catch (Exception ex)
            {

                throw;
            }

            return new VM_UserLoginResponse { };
                  
        }
        public int UserAccountRegistration(VM_UserRegistration model)
        {
            //var Password = GeneratePassword();

            try
            {
                string[] pname = { "@BusinessUserID", "@TenantID", "@CompanyGroupName", "@ClientName", "@ClientDOB", "@ClientEmailId", "@ClientContactNo", "@ClientUniqueCodeTypeId", "@ClientUniqueCode", "@UserName", "@Password", "@TenantProductKey" };
                string[] pvalue = { model.BusinessUserID.ToString(), model.TenantID.ToString(), model.CompanyGroupName, model.ClientName, Convert.ToDateTime(model.ClientDOB).ToString("yyyy-MM-dd"), model.ClientEmailId, model.ClientContactNo, model.ClientUniqueCodeTypeId.ToString(), model.ClientUniqueCode, model.UserName, model.Password, model.TenantProductKey };
                //return Int_Process("SP_BUSINESS_USER_REGISTRATION", pname, pvalue);
                int result = _DbAccess.ExecuteNonQuery("SP_SAVE_BUSINESS_USER_REGISTRATION", pname, pvalue);
                if (result > 0)
                {
                    //string emailBody = "Your registration is successful! Attached is your System generated User Password"+Password; // You can customize the email body
                    //_DbAccess.SendEmail(model.ClientEmailId, "Registration Successful", emailBody);
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //public int UserAccountRegistration(VM_UserRegistration model)
        //{
        //    //var Password = GeneratePassword();

        //    try
        //    {
        //        string[] pname = { "@BusinessUserID","@CompanyGroupName" ,"@ClientName", "@ClientDOB", "@ClientEmailId", "@ClientContactNo", "@ClientUniqueCodeTypeId", "@ClientUniqueCode" ,"@UserName","@Password","@TenantProductKey"};
        //        string[] pvalue = { model.BusinessUserID.ToString(),model.CompanyGroupName, model.ClientName, Convert.ToDateTime(model.ClientDOB).ToString("yyyy-MM-dd"), model.ClientEmailId, model.ClientContactNo, model.ClientUniqueCodeTypeId.ToString(), model.ClientUniqueCode, model.UserName, Password, model.TenantProductKey };
        //        //return Int_Process("SP_BUSINESS_USER_REGISTRATION", pname, pvalue);
        //        int result= _DbAccess.ExecuteNonQuery("SP_BUSINESS_USER_REGISTRATION", pname, pvalue);
        //        if (result > 0 )
        //        {
        //            //string emailBody = "Your registration is successful! Attached is your System generated User Password"+Password; // You can customize the email body
        //            //_DbAccess.SendEmail(model.ClientEmailId, "Registration Successful", emailBody);
        //            return result;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //    //SqlCommand sqlCommand = new SqlCommand("SP_BUSINESS_USER_REGISTRATION");
        //    //sqlCommand.Parameters.AddWithValue("@ClientName", ClientName);
        //    //sqlCommand.Parameters.AddWithValue("@ClientDOB", Reg_Date);
        //    //sqlCommand.Parameters.AddWithValue("@ClientEmailId",ClientEmailId);
        //    //sqlCommand.Parameters.AddWithValue("@ClientContactNo",ClientContactNo);
        //    //sqlCommand.Parameters.AddWithValue("@ClientUniqueCodeTypeId",ClientUniqueCodeTypeId);
        //    //sqlCommand.Parameters.AddWithValue("@ClientUniqueCode",ClientUniqueCode);
        //    //sqlCommand.Parameters.Add("@BusinessUserID" , SqlDbType.Int);
        //    //sqlCommand.Parameters["@BusinessUserID"].Direction = ParameterDirection.Output;
        //    // _DbAccess.ExecuteNonQuery(sqlCommand);
        //    //int r =Convert.ToInt32(sqlCommand.Parameters["@BusinessUserID"].Value);
        //    //if (r > 0)
        //    //{
        //    //    return r;
        //    //}
        //    //else
        //    //{
        //    //    return 0;
        //    //}

        //}
        public string GeneratePassword()
        {
            string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";
            int passwordLength = 8; // Length of the generated password
            Random random = new Random();

            StringBuilder password = new StringBuilder(passwordLength);
            for (int i = 0; i < passwordLength; i++)
            {
                int randomIndex = random.Next(allowedChars.Length);
                password.Append(allowedChars[randomIndex]);
            }

            return password.ToString();
        }
        public int UserTenantValidation(string TenantProductKey)
        {

            try
            {
                string[] pname = { "@ID" };
                string[] pvalue = { TenantProductKey };
                return _DbAccess.ExecuteNonQuery("CheckTenantValidation", pname, pvalue);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        }
}
