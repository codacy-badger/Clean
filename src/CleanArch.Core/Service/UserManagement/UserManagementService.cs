using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using AutoMapper;
using CleanArch.Core.Entities.UserManagement;
using CleanArch.Core.Interface.UserManagement;
using CleanArch.Core.Model;
using CleanArch.Core.Model.UserManagement;
using CleanArch.Core.Repository.Communication;
using CleanArch.Core.Repository.SalesForce;
using CleanArch.Core.Repository.UserManagement;
using CleanArch.Core.Utilities;


namespace CleanArch.Core.Service.UserManagement
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IMapper _mapper;
        IUserManagementRepository _userRepository;
        ICommunicationService _mail;
        ISalesForcePushObject _salesForcePushObject;
        public UserManagementService(IUserManagementRepository userRepository, IMapper mapper, ICommunicationService sendMail, ISalesForcePushObject salesForcePushObject)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _mail = sendMail;
            _salesForcePushObject = salesForcePushObject;
        }
        public dynamic IsUserExist(SignupModel model)
        {
            UserEntity mDBEntity = _userRepository.GetUserDetails(model.Email, 0);
            //if (!string.IsNullOrEmpty(mDBEntity.USERID))
            //{
            //    return ResponseMessage.BaseResponse("User Already Exist!.", HttpStatusCode.NotAcceptable);
            //}
            //else {
            //}
            // return  SaveLogic(model);
            return mDBEntity;
        }
        public dynamic SaveLogic(SignupModel model) {

            TokenDetailsEntity tokenEntity = _userRepository.GenerateToken("USER_ACTIVATION_TOKEN_EXPIRY");           
            UserEntity mDBEntity = Map_Request_Model(model, tokenEntity);
            SendMail(mDBEntity, tokenEntity);            
            return mDBEntity;
        }
        public dynamic SendMail(UserEntity mDBEntity, TokenDetailsEntity tokenEntity)
        {
            return _mail.Sendmail(mDBEntity, tokenEntity.Token);
        }
        public dynamic SalesForce(UserEntity mDBEntity)
        {
            //SafeRun<bool>(() =>
            //{
            //    _SalesForceObject.UserSignUpDetailsMapping(mDBEntity);
            //    return true;
            //});
            //  SalesForcePushObject obj = new SalesForcePushObject();
            _salesForcePushObject.UserSignUpDetailsMapping(mDBEntity);
            return ResponseMessage.BaseResponse("Registration successful. Please activate your account by clicking on the activation link sent to your registered email.", HttpStatusCode.OK);
        }
        private UserEntity Map_Request_Model(SignupModel request, TokenDetailsEntity token)
        {
            UserEntity mDBEntity = new UserEntity();
            mDBEntity.USERID = request.Email;
            mDBEntity.FIRSTNAME = request.FirstName;
            mDBEntity.LASTNAME = request.LastName;
            mDBEntity.EMAILID = EncryptDecrypt.EncryptStringAES(request.Email);
            mDBEntity.PASSWORD = EncryptDecrypt.EncryptStringAES(request.Password); ;
            mDBEntity.ISDCODE = request.ISDCode;
            mDBEntity.MOBILENUMBER = request.ContactNo;
            mDBEntity.COMPANYNAME = request.CompanyName;
            mDBEntity.TOKEN = token.Token;
            mDBEntity.TOKENEXPIRY = token.TokenExpiry;
            mDBEntity.NOTIFICATIONSUBSCRIPTION = request.NotificationSubscription;
            mDBEntity.SHIPMENTPROCESS = request.ShipmentProcess;
            mDBEntity.SHIPPINGEXPERIENCE = request.ShippingExperience;
            mDBEntity.USERTYPE = 2;
            mDBEntity.ISTEMPPASSWORD = 0;
            mDBEntity.ACCOUNTSTATUS = 0;
            mDBEntity.ISTERMSAGREED = 1;
            mDBEntity.COUNTRYCODE = request.COUNTRYCODE;
            mDBEntity.COUNTRYNAME = request.CountryName;
            mDBEntity.COUNTRYID = request.UACountryId;
            _userRepository.Save(mDBEntity);
            return mDBEntity;
        }
    }
}
