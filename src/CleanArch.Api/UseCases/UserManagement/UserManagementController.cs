using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArch.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CleanArch.Core.Interface.UserManagement;
using CleanArch.Core.Utilities;
using System.Net;

namespace CleanArch.Api.UseCases.UserManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    { 
      private readonly  IUserManagementService _usersemanagervice;
        
        public UserManagementController(IUserManagementService usersemanagervice)
        {
            _usersemanagervice = usersemanagervice;
        }
        [Route("PostSignUp")]
        [HttpPost]
        public dynamic SignUp([FromBody]SignupModel request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(_usersemanagervice.IsUserExist(request));
                }
                else
                {
                    return "Invalid Details";// ResponseMessage.BaseResponse(GetModelErrormessages(), HttpStatusCode.ExpectationFailed);
                }
                //return SafeRun<dynamic>(() =>
                //{
                //    if (ModelState.IsValid)
                //    {
                //        return IsUserExist(request);
                //    }
                //    else
                //    {
                //        return BaseResponse(GetModelErrormessages(), HttpStatusCode.ExpectationFailed);
                //    }
                //});
            }
            catch (Exception ex)
            {
                // LogError("api/UserManagement", "PostSignUp", ex.Message, ex.StackTrace);
                // return BaseResponse(ex.Message, HttpStatusCode.ExpectationFailed);
                return "";
            }
        }
    }
}