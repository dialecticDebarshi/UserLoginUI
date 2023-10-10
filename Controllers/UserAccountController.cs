using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using TenantCompany.Models;



namespace TenantCompany.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string BaseUrl;
        public UserAccountController(IConfiguration configuration)
        {
            _configuration = configuration;

            BaseUrl = configuration["BaseUrlLogin"];
        }
        public IActionResult Index()
        {
            return View();

        }

        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<JsonResult> UserLogin(VM_UserLogin login)
        {
            string urlParameters = "UserLoginValidation";
            // HttpClient setup
            var httpClient = new HttpClient();
            //httpClient.BaseAddress = new Uri("http://localhost:5017/"); // Replace with the actual API base URL

            // Create the request body

            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var response=new HttpResponseMessage();
            try
            {
                response = await httpClient.PostAsync(BaseUrl+ urlParameters, content);
                if (response.IsSuccessStatusCode)
                {
                    // The login was successful, handle the response
                    var responseData = await response.Content.ReadAsStringAsync();
                    VM_UserLoginResponse loginresponseData = JsonConvert.DeserializeObject<VM_UserLoginResponse>(responseData);
                    // Process the response data as needed
                    var result = new { UserID = loginresponseData.UserID };
                    HttpContext.Session.SetString("UserData", responseData);
                    
                    string jsonses = HttpContext.Session.GetString("UserData");

                    VM_UserLoginResponse loginresponseData2 = JsonConvert.DeserializeObject<VM_UserLoginResponse>(jsonses);

                    

                    return Json(result);
                    //return responseData; // Return the response data
                }
                else
                {
                    // The login failed, handle the error
                    var errorData = await response.Content.ReadAsStringAsync();
                    var result = new { ds = errorData };
                    return Json(result);
                    // Process the error data as needed

                    //   return errorData; // Return the error data
                }

            }
            catch (Exception ex)
            {
                var result = new { ds = ex.Message };
                return Json(result);
            }
            // Make the API call
         

            // Check the response status
          
        }
        public async Task< IActionResult> UserRegistration()
        {
            // HttpClient setup
            String UrlParameters = "UserRegistration";
            using (var httpClient = new HttpClient())
            {
                //httpClient.BaseAddress = new Uri("http://localhost:5017/"); // Replace with the actual API base URL

                var response = await httpClient.GetAsync(BaseUrl+ UrlParameters);
                if (response.IsSuccessStatusCode)
                {

                    var responseData = await response.Content.ReadAsStringAsync();
                    var pDataTypeList = JsonConvert.DeserializeObject<List<CustomSelectListItem>>(responseData);
                    //var pDataTypeList = JsonConvert.DeserializeObject<List<object>>(responseData);

                    ViewBag.PDataTypeList = pDataTypeList;

                    // Process the list of DataRow objects as needed

                    return View();
                }
                else
                {
                    // Handle the error response
                    var responseData = await response.Content.ReadAsStringAsync();
                    return View("Error");
                }
            }



            // Make the API call


            // Check the response status


            return View();
            
        }

        [HttpPost]
        public async Task<JsonResult> UserNameValidation(string UserName)
        {
            string UrlParameters = "UserNameValidation";
            // HttpClient setup
            var httpClient = new HttpClient();
            //httpClient.BaseAddress = new Uri("http://localhost:5017/"); // Replace with the actual API base URL

            // Create the request body
            var req = new { UserName = UserName };
            var content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            // Make the API call
            var response = await httpClient.PostAsync(BaseUrl+ UrlParameters, content);

            // Check the response status
            if (response.IsSuccessStatusCode)
            {
                // The login was successful, handle the response
                var responseData = await response.Content.ReadAsStringAsync();
                // Process the response data as needed
                var result = new { id = responseData };
                if( responseData =="1")
                {
                    ModelState.AddModelError("UserName", "oioioi");

                }
                return Json(result);
                //return result; // Return the response data
            }
            else
            {
                // The login failed, handle the error
                var errorData = await response.Content.ReadAsStringAsync();
                var result = new { id = errorData };
                return Json(result);
                // Process the error data as needed

                //   return errorData; // Return the error data
            }

        }
        [HttpPost]
        public async Task<JsonResult> UserTenantValidation(string TenantProductKey)
        {
            string UrlParameters = "UserTenantValidation";
            // HttpClient setup
            var httpClient = new HttpClient();
            //httpClient.BaseAddress = new Uri("http://localhost:5017/"); // Replace with the actual API base URL

            // Create the request body
            var req = new { TenantProductKey = TenantProductKey };
            var content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            // Make the API call
            var response = await httpClient.PostAsync(BaseUrl+ UrlParameters, content);

            // Check the response status
            if (response.IsSuccessStatusCode)
            {
                // The login was successful, handle the response
                var responseData = await response.Content.ReadAsStringAsync();
                // Process the response data as needed
                var result = new { id= responseData };
                return Json(result);
                //return result; // Return the response data
            }
            else
            {
                // The login failed, handle the error
                var errorData = await response.Content.ReadAsStringAsync();
                var result = new { id = errorData };
                return Json(result);
                // Process the error data as needed

                //   return errorData; // Return the error data
            }

        }
        [HttpPost]
        public async Task<JsonResult> UserRegistration(VM_UserRegistration model)
        {
            string UrlParameters = "UserRegistration";
            model.TenantID = 2;
            // HttpClient setup
            var httpClient = new HttpClient();
            //httpClient.BaseAddress = new Uri("http://localhost:5017/"); // Replace with the actual API base URL

            // Create the request body
           
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            // Make the API call
            var response = await httpClient.PostAsync(BaseUrl + UrlParameters, content);

            // Check the response status
            if (response.IsSuccessStatusCode)
            {
                // The login was successful, handle the response
                var responseData = await response.Content.ReadAsStringAsync();
                // Process the response data as needed
                var result = new { id = responseData };
                return Json(result);
                //return responseData; // Return the response data
            }
            else
            {
                // The login failed, handle the error
                var errorData = await response.Content.ReadAsStringAsync();
                var result = new { id = errorData };
                return Json(result);
                // Process the error data as needed

                //   return errorData; // Return the error data
            }
           
        }

    }
}
