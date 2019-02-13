using System;
using System.Collections.Generic;
using System.Text;
using CleanArch.Core.Entities.UserManagement;
using CleanArch.Core.Model;
using CleanArch.Core.Model.UserManagement;

namespace CleanArch.Core.Interface.UserManagement
{
    public interface IUserManagementService
    {
        dynamic IsUserExist(SignupModel model);
        dynamic SaveLogic(SignupModel model);
        dynamic SalesForce(UserEntity mDBEntity);
        dynamic SendMail(UserEntity mDBEntity, TokenDetailsEntity tokenEntity);
    }
}
