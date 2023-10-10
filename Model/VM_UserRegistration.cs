using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TenantCompany.Models
{
    public class VM_UserRegistration
    {

        public int BusinessUserID { get; set; }

        public int TenantID { get; set; }
        public string CompanyGroupName { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ClientName { get; set; }
        [DataType(DataType.Date)]
        public string ClientDOB { get; set; }
        [DataType(DataType.EmailAddress)]
        public string ClientEmailId { get; set; }
        [MinLength(10)]
        [MaxLength(10)]
        public string ClientContactNo { get; set; }
        public int ClientUniqueCodeTypeId { get; set; }
        public string ClientUniqueCode { get; set; }
        public string TenantProductKey { get; set; }



    }
    public class VM_UserLogin
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class VM_UserLoginResponse
    {
        public int UserID { get; set; }
        public int Employee_Master_Key { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int JobRoleId { get; set; }
        public int UserTypeId { get; set; } 


    }
    //public class CustomSelectListItem : SelectListItem
    //{
    //    public string Description { get; set; }
    //}
}
