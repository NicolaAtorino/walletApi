using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WalletApi.Repositories.Account;
using WalletApi.DataLayer;
using System.Collections.Generic;
using WalletApi.ServiceLayer;
using WalletApi.Utilities;
using System.Linq;

namespace WalletApi.Tests
{

    /// <summary>
    /// all the relevant tests are done on the service, since the controllers 
    /// are very straightforward and the repository requires integration tests.
    /// </summary>
    [TestClass]
    public class AccountService_Disable_Tests
    {
        [TestMethod]
        public void Disable_AccountCanBeClosed_ReturnsSuccess()
        {
            var testAccount = new Account()
            {
                UserId = 123,
                Balance = 0,
                Enabled = true
            };

            var mockRepo = new Mock<IAccountRepository>();

            mockRepo
                .Setup(x => x.Get(123))
                .Returns(testAccount);

            mockRepo
                .Setup(x => x.Update(It.IsAny<Account>()))
                .Verifiable();


            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.DisableAccount(123);

            //assert
            Assert.IsTrue(opResult.Success);

            Mock.Verify(mockRepo);
        }

        [TestMethod]
        public void Disable_AccountIdNotValid_ReturnsErrorMessage()
        {
            var mockRepo = new Mock<IAccountRepository>();

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.DisableAccount(-1); //not valid

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(ErrorMessages.NotValidAccountId, opResult.ErrorMessage);
        }


        [TestMethod]
        public void Disable_AccountAlreadyClosed_ReturnsErrorMessage()
        {
            var testAccount = new Account()
            {
                UserId = 123,
                Balance = 0,
                Enabled = false
            };

            var mockRepo = new Mock<IAccountRepository>();

            mockRepo
                .Setup(x => x.Get(123))
                .Returns(testAccount);

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.DisableAccount(123);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(ErrorMessages.AccountIsDisabled, opResult.ErrorMessage);
        }

        [TestMethod]
        public void Disable_AccountHasMoney_ReturnsErrorMessage()
        {
            var testAccount = new Account()
            {
                UserId = 123,
                Balance = 15,
                Enabled = true
            };

            var mockRepo = new Mock<IAccountRepository>();

            mockRepo
                .Setup(x => x.Get(123))
                .Returns(testAccount);

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.DisableAccount(123);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(ErrorMessages.AccountHasMoney, opResult.ErrorMessage);
        }

        [TestMethod]
        public void Disable_DBThrowsException_ReturnsErrorMessage()
        {
            
            var mockRepo = new Mock<IAccountRepository>();

            mockRepo
                .Setup(x => x.Get(123))
                .Throws<Exception>();

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.DisableAccount(123);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(ErrorMessages.GenericError, opResult.ErrorMessage);
        }

    }
}
