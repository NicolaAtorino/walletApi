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
    public class AccountService_GetBalance_Tests
    {
        [TestMethod]
        public void GetBalance_CorrectAccount_ReturnsExactBalance()
        {
            var testAccount = new Account()
            {
                Balance = 12
            };

            var mockRepo = new Mock<IAccountRepository>();
            mockRepo
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(testAccount);

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.GetBalance(1);

            //assert
            Assert.IsTrue(opResult.Success);
            Assert.AreEqual(12, opResult.Result);
        }

        [TestMethod]
        public void GetBalance_AccountDoesNotExists_ReturnsErrorMessage()
        {
            var mockAccount = new Mock<IAccountRepository>();
            mockAccount
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns<Account>(null);

            var service = new AccountService(mockAccount.Object);

            //act
            var opResult = service.GetBalance(1);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(default(decimal), opResult.Result);
            Assert.AreEqual(ErrorMessages.AccountNotFound, opResult.ErrorMessage);

        }

        [TestMethod]
        public void GetBalance_AccountIdNotValid_ReturnsErrorMessage()
        {
            var mockAccount = new Mock<IAccountRepository>();
            
            var service = new AccountService(mockAccount.Object);

            //act
            var opResult = service.GetBalance(-1); //not valid

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(default(decimal), opResult.Result);
            Assert.AreEqual(ErrorMessages.NotValidAccountId, opResult.ErrorMessage);
        }


        [TestMethod]
        public void GetBalance_DBThrowsException_ReturnsErrorMessage()
        {
            var mockAccount = new Mock<IAccountRepository>();
            mockAccount
                .Setup(x => x.Get(It.IsAny<int>()))
                .Throws<Exception>();

            var service = new AccountService(mockAccount.Object);

            //act
            var opResult = service.GetBalance(1);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(default(decimal),opResult.Result);
            Assert.AreEqual(ErrorMessages.GenericError, opResult.ErrorMessage);

        }
    }
}
