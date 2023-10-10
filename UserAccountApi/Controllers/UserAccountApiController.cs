using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Net;
using TechnoconGroupUserModule.Models;
using UserAccountApi.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserAccountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountApiController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly UserAccountRepository _AccountRepository;
        public UserAccountApiController(IConfiguration configuration, UserAccountRepository accountRepository)
        {
            _configuration = configuration;
            _AccountRepository = accountRepository;
        }
        [HttpGet]
        [Route("UserRegistration")]
        //public HttpResponseMessage UserRegistration()
        //{
        //    DataTable dt = _AccountRepository.GetClientUniqueCodeTypes().Tables[0];
        //    List<DataRow> rows = dt.AsEnumerable().ToList();
        //    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
        //    response.Content = new ObjectContent<List<DataRow>>(rows, new JsonMediaTypeFormatter(), "application/json");
        //    return response;
        //}

        public List<object> UserRegistration()
        {
            List<object> dt = _AccountRepository.GetClientUniqueCodeTypes();
            return dt;



        }
        [HttpPost]
        [Route("UserLoginValidation")]
        public VM_UserLoginResponse UserLoginValidation(VM_UserLogin login)
        {
            return _AccountRepository.UserLoginValidation(login);
          
        }
        [HttpPost]
        [Route("UserRegistration")]
        public int UserRegistration(VM_UserRegistration model)
        {
            int r = _AccountRepository.UserAccountRegistration(model);
            return r;

        }

        [HttpPost]
        [Route("UserNameValidation")]
        public int UserNameValidation(VM_UserNameValidation UNV)
        {
            int r = _AccountRepository.UserNameValidation(UNV.UserName);
            return r;
        }

        [HttpPost]
        [Route("UserTenantValidation")]
        public int UserTenantValidation(VM_TenantValidation VTV)
        {
            int r = _AccountRepository.UserTenantValidation(VTV.TenantProductKey);
            return r;
        }
        //public UserAccountApiController(IConfiguration configuration, UserAccountRepository accountRepository)
        //{
        //    _configuration = configuration;
        //    _AccountRepository = accountRepository;
        //}
        // GET: api/<UserAccountApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserAccountApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserAccountApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserAccountApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserAccountApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

}
