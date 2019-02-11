using System;
using System.Collections.Generic;
using System.Text;
using CleanArch.Core.Model;
using CleanArch.Core.Model.UserManagement;

namespace CleanArch.Core.Interface.UserManagement
{
    public interface IUserManagementService
    {
        dynamic IsUserExist(SignupModel model);
        
    }
}
