using CleanArch.Core.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Repository.SalesForce
{
   public interface ISalesForcePushObject
    {
        void UserSignUpDetailsMapping(UserEntity model);
    }
}
