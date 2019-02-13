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
using CleanArch.Core.Entities.UserManagement;
using CleanArch.Core.Model.Response;
using System.Net.Http;

namespace CleanArch.Api.UseCases.UserManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _usersemanagervice;

        public UserManagementController(IUserManagementService usersemanagervice)
        {
            _usersemanagervice = usersemanagervice;
        }
        [Route("PostSignUp")]
        [HttpPost]
        public async Task<dynamic> SignUp([FromBody]SignupModel request)
        {
            try
            {
                Response response  = new Response();
                if (ModelState.IsValid)
                {
                    UserEntity mDBEntity = await _usersemanagervice.IsUserExist(request);
                    if (!string.IsNullOrEmpty(mDBEntity.USERID))
                    {

                        // return ResponseMessage.BaseResponse("User Already Exist!.", HttpStatusCode.NotAcceptable);
                        return new HttpResponseMessage()
                        {
                            Content = new JsonContent(new
                            {
                                StatusCode = HttpStatusCode.NotAcceptable,
                                StatusMessage = "User Already Exist!."
                            })
                        };
                    }
                    else
                    {
                        mDBEntity = _usersemanagervice.SaveLogic(request);
                        _usersemanagervice.SalesForce(mDBEntity);
                        //return "";
                       // return ResponseMessage.BaseResponse("Registration successful. Please activate your account by clicking on the activation link sent to your registered email.", HttpStatusCode.OK);
                     // return Request.CreateResponse(HttpStatusCode.OK);
                        return new HttpResponseMessage()
                        {
                            Content = new JsonContent(new
                            {
                                StatusCode = HttpStatusCode.OK,
                                StatusMessage = "Registration successful. Please activate your account by clicking on the activation link sent to your registered email."
                            })
                        };

                    }
                   
                }
                else
                {
                    return new HttpResponseMessage()
                    {
                        Content = new JsonContent(new
                        {
                            StatusCode = HttpStatusCode.NotAcceptable,
                            StatusMessage = "Invalid input."
                        })
                    };
                }
               
            }
            catch (Exception ex)
            {
                // LogError("api/UserManagement", "PostSignUp", ex.Message, ex.StackTrace);
                // return BaseResponse(ex.Message, HttpStatusCode.ExpectationFailed);
                return new HttpResponseMessage();
            }
        }
    }
}