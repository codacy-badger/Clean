using CleanArch.Core.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Repository.Communication
{
    public interface ICommunicationService
    {
        bool Sendmail(UserEntity mDBEntity, string Token);
    }
}
