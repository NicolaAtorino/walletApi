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
    public class AccountService_Deposit_Tests
    {
        //depositTests
        [TestMethod]
        public void Deposit_AccountIsOK_ReturnsNewBalance()
        {
            var testAccount = new Account()
            {
                Balance = 12,
                Enabled = true
            };

            var mockRepo = new Mock<IAccountRepository>();

            mockRepo
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(testAccount);

            mockRepo
                .Setup(x => x.Update(It.IsAny<Account>()))
                .Verifiable();


            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.Deposit(1, 1);

            //assert
            Assert.IsTrue(opResult.Success);
            Assert.AreEqual(12 + 1, opResult.Result);

            Mock.Verify(mockRepo);
        }

        [TestMethod]
        public void Deposit_AccountDoesNotExists_ReturnsErrorMessage()
        {
            var mockRepo = new Mock<IAccountRepository>();

            mockRepo
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns<Account>(null);

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.Deposit(1, 1);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(default(decimal), opResult.Result);
            Assert.AreEqual(ErrorMessages.AccountNotFound, opResult.ErrorMessage);
        }

        [TestMethod]
        public void Deposit_AccountIdNotValid_ReturnsErrorMessage()
        {
            var mockRepo = new Mock<IAccountRepository>();

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.Deposit(-1, 1); //account not valid

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(default(decimal), opResult.Result);
            Assert.AreEqual(ErrorMessages.NotValidAccountId, opResult.ErrorMessage);
        }

        [TestMethod]
        public void Deposit_AmountIsNotValid_ReturnsErrorMessage()
        {
            var mockRepo = new Mock<IAccountRepository>();

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.Deposit(1, -1); //amount not valid

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(default(decimal), opResult.Result);
            Assert.AreEqual(ErrorMessages.NotValidAmount, opResult.ErrorMessage);
        }


        [TestMethod]
        public void Deposit_AccountIsClosed_ReturnsErrorMessage()
        {
            var mockRepo = new Mock<IAccountRepository>();

            var testAccount = new Account()
            {
                Balance = 12,
                Enabled = false
            };

            mockRepo
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(testAccount);

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.Deposit(1, 1);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(default(decimal), opResult.Result);
            Assert.AreEqual(ErrorMessages.AccountIsDisabled, opResult.ErrorMessage);
        }

        [TestMethod]
        public void Deposit_DBThrowsException_ReturnsErrorMessage()
        {
            var mockRepo = new Mock<IAccountRepository>();
            var testAccount = new Account()
            {
                Balance = 12,
                Enabled = true
            };
            mockRepo
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(testAccount);

            mockRepo
                .Setup(x => x.Update(It.IsAny<Account>()))
                .Throws<Exception>();

            var service = new AccountService(mockRepo.Object);

            //act
            var opResult = service.Deposit(1, 1);

            //assert
            Assert.IsFalse(opResult.Success);
            Assert.AreEqual(default(decimal), opResult.Result);
            Assert.AreEqual(ErrorMessages.GenericError, opResult.ErrorMessage);
        }
        
    }
}
