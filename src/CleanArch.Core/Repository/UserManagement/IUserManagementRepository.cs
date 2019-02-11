using CleanArch.Core.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Repository.UserManagement
{
    public interface IUserManagementRepository
    {
        UserEntity GetUserDetails(string Email, int mode);
        TokenDetailsEntity GenerateToken(string ConfigName);
        long Save(UserEntity mDBEntity);
       

        



    }
}
