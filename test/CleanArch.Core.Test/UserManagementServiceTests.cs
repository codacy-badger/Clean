using System;
using Xunit;
using Moq;
using AutoMapper;
using CleanArch.Core.Model;
using CleanArch.Core.Service.UserManagement;
using CleanArch.Core.Interface.UserManagement;
using CleanArch.Core.Repository.UserManagement;
using CleanArch.Core.Repository.Communication;
using CleanArch.Core.Repository.SalesForce;
using CleanArch.Core.Entities.UserManagement;

namespace CleanArch.Core.Test
{
    public class UserManagementServiceTests
    {
        private readonly Mock<IUserManagementRepository> _userRepository;
        private readonly Mock<ICommunicationService> _sendMail;
        private readonly Mock<ISalesForcePushObject> _salesForcePushObject;
        private readonly Mock<IMapper> _mapper;
        private readonly IUserManagementService _userManagementService;
        public UserManagementServiceTests()
        {
            _userRepository = new Mock<IUserManagementRepository>(MockBehavior.Loose);
            _mapper = new Mock<IMapper>(MockBehavior.Loose);
            _sendMail = new Mock<ICommunicationService>(MockBehavior.Loose);
            _salesForcePushObject = new Mock<ISalesForcePushObject>(MockBehavior.Loose);
            _userManagementService = new UserManagementService(_userRepository.Object, _mapper.Object, _sendMail.Object, _salesForcePushObject.Object);
             
        }
        [Fact]
        public void Check_User_AlreadyExist()
        {
            //
            // Arrange
            SignupModel request = new SignupModel()
            {
                Email = "successpavan510@gmail.com"
            };
            UserEntity userEntity = new UserEntity()
            {
                USERID = "successpavan510@gmail.com"
            };
            _userRepository
                .Setup(m => m.GetUserDetails(request.Email, 0))
                .Returns(userEntity);
            //
            // Act
            var response = _userManagementService.IsUserExist(request);

            //
            // Assert            
            Assert.NotEmpty(response.USERID);
        }

        [Fact]
        public void SaveLogic_Is_Mail_Send()
        {
            //
            // Arrange
            bool isSent = true;
            UserEntity userEntity = new UserEntity()
            {
                USERID = "successpavan510@gmail.com"
            };
            TokenDetailsEntity tokenEntity = new TokenDetailsEntity()
            {
                Token = "fU9A9IKR1kgwQR+HutPBTovLJKH+WbZX"
            };
            _userRepository
                .Setup(m => m.GenerateToken("USER_ACTIVATION_TOKEN_EXPIRY"))
                .Returns(tokenEntity);
            _sendMail
                .Setup(m => m.Sendmail(userEntity,tokenEntity.Token))
                .Returns(isSent);

            //
            // Act
            bool response = _userManagementService.SendMail(userEntity, tokenEntity);

            //
            // Assert            
            Assert.True(response);

        }
    }
}
