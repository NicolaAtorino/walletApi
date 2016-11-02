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
    /// all the relevant tests are done on the services, since the controllers 
    /// are very straightforward and the repository requires integration tests.
    /// 
    /// These are very simple tests that cover the basic requirements and also 
    /// what will happen in case of db connection failure.
    /// 
    /// </summary>
    [TestClass]
    public class AccountService_GetAccount_Tests
    {
        //get account tests
        [TestMethod]
        public void GetAccount_CorrectUserID_ReturnsAccountId()
        {
            //arrange
            var testAccount = new Account()
            {
                Id = 1
            };

            var mockAccount = new Mock<IAccountRepository>();
            mockAccount
                .Setup(x => x.GetFiltered(It.IsAny<Func<Account, bool>>()))
                .Returns(new List<Account>() { testAccount });

            var service = new AccountService(mockAccount.Object);

            //act
            var opResult = service.GetAccountId(1);

            //assert
            Assert.IsTrue(opResult.Success);
            Assert.AreEqual(1, opResult.Result);
        }

        [TestMethod]
        public void GetAccount_NotValidUserId_ReturnsAccountNotFound()
        {
            var mockAccount = new Mock<IAccountRepository>();

            var service = new AccountService(mockAccount.Object);

            //act
            var opResult = service.GetAccountId(-1); //not valid

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(0, opResult.Result);
            Assert.AreEqual(ErrorMessages.NotValidUserId, opResult.ErrorMessage);
        }


        [TestMethod]
        public void GetAccount_UserIDNotExists_ReturnsAccountNotFound()
        {
            var mockAccount = new Mock<IAccountRepository>();
            mockAccount
                .Setup(x => x.GetFiltered(It.IsAny<Func<Account, bool>>()))
                .Returns(Enumerable.Empty<Account>());

            var service = new AccountService(mockAccount.Object);

            //act
            var opResult = service.GetAccountId(1);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(0,opResult.Result);
            Assert.AreEqual(ErrorMessages.AccountNotFound, opResult.ErrorMessage);
        }

        [TestMethod]
        public void GetAccount_DbNotAvailable_ReturnsErrorMessage()
        {
            var mockAccount = new Mock<IAccountRepository>();
            mockAccount
                .Setup(x => x.GetFiltered(It.IsAny<Func<Account, bool>>()))
                .Throws<Exception>();

            var service = new AccountService(mockAccount.Object);

            //act
            var opResult = service.GetAccountId(1);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(0,opResult.Result);
            Assert.AreEqual(ErrorMessages.GenericError, opResult.ErrorMessage);
        }
    }
}
